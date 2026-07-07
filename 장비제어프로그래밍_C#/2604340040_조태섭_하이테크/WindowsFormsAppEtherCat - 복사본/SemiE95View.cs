using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using IEG3268_Dll; // 이더캣 하드웨어 제어 라이브러리

namespace WindowsFormsAppEtherCat
{
    public partial class SemiE95View : UserControl
    {
        // -------------------------------------------------------------
        // 1. 상태를 나타내는 '단어장(Enum)' 만들기
        // -------------------------------------------------------------
        // 설비가 지금 어떤 상태인지 기억하기 위해 이름표를 만들어둡니다.
        // INIT(초기화), HOME(원점복귀), READY(준비완료), LOAD(가져오기), PROCESS(작업중), UNLOAD(내려놓기), COMPLETE(작업완료), ALARM(에러발생)
        public enum SystemState { INIT, HOME, READY, LOAD, PROCESS, UNLOAD, COMPLETE, ALARM }
        
        // -------------------------------------------------------------
        // 모터 기본 파라미터 전역 변수 세팅 (가속도, 감속도, 최대속도, 속도)
        // -------------------------------------------------------------
        public long defaultAccel = 1000000;
        public long defaultDecel = 1000000;
        public long defaultMaxSpeed = 100000000;
        public long defaultSpeed = 1000000;

        // -------------------------------------------------------------
        // 아키텍처: 하위 모듈 객체 선언 (로봇, FOUP, 챔버)
        // -------------------------------------------------------------
        public TR_Robot robot = new TR_Robot();
        public Foup foupA = new Foup(0);
        public Foup foupB = new Foup(0);
        public Chamber pm1 = new Chamber(); // 연마
        public Chamber pm2 = new Chamber(); // 세정
        public Chamber pm3 = new Chamber(); // 검사

        // 현재 설비의 상태를 저장하는 변수 (처음에는 무조건 초기화 상태)
        private SystemState currentState = SystemState.INIT;

        // 에러가 났는지 기억하는 스위치 (true면 에러, false면 정상)
        private bool isAlarm = false;

        // -------------------------------------------------------------
        // 2. 웨이퍼(반도체 판) 개수 기억하기
        // -------------------------------------------------------------
        private int waferCountFOUPA = 0; // 시작 지점(FOUP A)에 있는 웨이퍼 개수
        private int waferCountFOUPB = 0; // 도착 지점(FOUP B)에 있는 웨이퍼 개수
        private int currentWaferSlot = 1; // 지금 몇 번째 웨이퍼를 작업 중인지 번호

        // 실제 모터를 움직이게 해주는 마법의 리모컨(객체)입니다. Form1에서 넘겨받습니다.
        public IEG3268 EtherCAT_M; 
        
        // 작업(Sequence)을 중간에 취소할 수 있게 해주는 비상정지 버튼 같은 역할입니다.
        private CancellationTokenSource cts;

        // [AUTO/MANUAL 모드] AUTO=실제 웨이퍼로 운전(진공 센서 인터록 필수),
        // MANUAL=웨이퍼 없이 동작 시퀀스만 점검(진공 센서 대기를 짧게 완화). 기본값은 안전한 AUTO.
        private bool isManualTestMode = false;

        public SemiE95View()
        {
            InitializeComponent();
            // 화면이 켜지면 1초에 한 번씩 화면을 업데이트하는 타이머를 작동시킵니다.
            uiTimer.Start();
        }

        // -------------------------------------------------------------
        // 3. 타이머 (1초마다 반복해서 실행되는 함수)
        // -------------------------------------------------------------
        private void uiTimer_Tick(object sender, EventArgs e)
        {
            // 우측 상단 시계 업데이트
            lblDateTime.Text = "Date / Time\n" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            
            // X축, Z축 모터의 현재 위치를 라이브러리를 통해 읽어와서 화면에 표시합니다.
            try {
                // X축, Z축 모터의 현재 위치 업데이트
                lblRobotXPos.Text = EtherCAT_M.Axis2_is_PosData();
                lblRobotZPos.Text = EtherCAT_M.Axis1_is_PosData();

                // [새로 추가된 하드웨어 센서(I/O) 연동 파트]
                // 1. 비상정지(EMG SW - P003) 감지
                // 버튼이 눌려서 신호가 들어오면 즉시 에러 알람을 띄웁니다. (설비에 따라 "0"일때 눌린 것일 수도 있습니다)
                if (EtherCAT_M.Digital_Input(3).ToString() == "1" && !isAlarm)
                {
                    TriggerAlarm("EMG", "하드웨어 비상정지 버튼 눌림!");
                }

                // 2. 장비 상태(Door, Wafer 등)를 센서 값에 맞춰서 화면 글자로 바꿔줍니다.
                // 예: SW-1(P000)을 Door 센서로, SW-2(P001)를 Wafer 센서로 가정하고 연결한 코드입니다.
                lblDoorStatus.Text = (EtherCAT_M.Digital_Input(0).ToString() == "1") ? "Closed" : "Open";
                lblWaferStatus.Text = (EtherCAT_M.Digital_Input(1).ToString() == "1") ? "Loaded" : "Empty";
                lblBladeStatus.Text = (EtherCAT_M.Digital_Input(2).ToString() == "1") ? "Wafer Detected" : "Empty";
            }
            catch { /* 하드웨어 연결이 안 되어 있을 땐 무시하고 넘어갑니다 */ }

            // 만약 현재 상태가 '에러(ALARM)'라면, 좌측 상단 상태표시창을 빨간색으로 깜빡이게 만듭니다.
            if (currentState == SystemState.ALARM)
            {
                lblStatusBig.Text = "> ALARM";
                lblStatusBig.ForeColor = System.Drawing.Color.Red;
                lblStatusIndIdle.ForeColor = System.Drawing.Color.Gray;
                lblStatusIndRunning.ForeColor = System.Drawing.Color.Gray;
                lblStatusIndComplete.ForeColor = System.Drawing.Color.Gray;
                lblStatusIndAlarm.ForeColor = System.Drawing.Color.Red;
            }
        }

        // -------------------------------------------------------------
        // 4. 상태 변경 함수 (가장 중요한 부분!)
        // -------------------------------------------------------------
        // 설비의 상태가 바뀔 때마다 이 함수를 부르면, 화면의 글자와 색상이 알맞게 바뀝니다.
        private void ChangeState(SystemState newState)
        {
            currentState = newState;
            if (this.InvokeRequired) // 다른 쓰레드(작업자)가 화면을 고치려고 하면 안전하게 고치도록 도와줌
            {
                this.Invoke(new Action(() => UpdateUIForState(newState)));
            }
            else
            {
                UpdateUIForState(newState);
            }
        }

        // [임시 디버그용] 지금 정확히 어느 단계를 실행 중인지 화면에 표시합니다. 원인 파악되면 제거 예정.
        private void DebugStep(string msg)
        {
            if (this.InvokeRequired)
                this.Invoke(new Action(() => lblAnimRobot.Text = "[DEBUG] " + msg));
            else
                lblAnimRobot.Text = "[DEBUG] " + msg;
        }

        private void UpdateUIForState(SystemState state)
        {
            // 일단 모든 표시등 불을 끕니다 (회색으로 만듦)
            lblStatusIndIdle.ForeColor = System.Drawing.Color.Gray;
            lblStatusIndRunning.ForeColor = System.Drawing.Color.Gray;
            lblStatusIndComplete.ForeColor = System.Drawing.Color.Gray;
            lblStatusIndAlarm.ForeColor = System.Drawing.Color.Gray;
            
            // 로봇 그림 안의 글자 업데이트
            lblAnimRobot.Text = "2-Axis Robot\n(" + state.ToString() + ")";

            // 상태에 따라 표시등 색상을 켜줍니다.
            switch (state)
            {
                case SystemState.INIT: // 아무것도 안 하는 초기 상태
                    lblStatusBig.Text = "> IDLE";
                    lblStatusBig.ForeColor = System.Drawing.Color.Gray;
                    lblStatusIndIdle.ForeColor = System.Drawing.Color.Green;
                    lblAnimRobot.BackColor = System.Drawing.Color.LightGray;
                    break;
                case SystemState.READY: // 일할 준비 완료
                    lblStatusBig.Text = "> READY";
                    lblStatusBig.ForeColor = System.Drawing.Color.Blue;
                    lblStatusIndIdle.ForeColor = System.Drawing.Color.Green;
                    lblAnimRobot.BackColor = System.Drawing.Color.LightGreen;
                    break;
                case SystemState.COMPLETE: // 일 다 끝남!
                    lblStatusBig.Text = "> COMPLETE";
                    lblStatusBig.ForeColor = System.Drawing.Color.Blue;
                    lblStatusIndComplete.ForeColor = System.Drawing.Color.Blue;
                    lblAnimRobot.BackColor = System.Drawing.Color.LightGreen;
                    break;
                case SystemState.HOME:
                case SystemState.LOAD:
                case SystemState.PROCESS:
                case SystemState.UNLOAD: // 로봇이 열심히 움직이는 중일 때
                    lblStatusBig.Text = "> RUNNING";
                    lblStatusBig.ForeColor = System.Drawing.Color.Green;
                    lblStatusIndRunning.ForeColor = System.Drawing.Color.Green;
                    lblAnimRobot.BackColor = System.Drawing.Color.Orange;
                    break;
                case SystemState.ALARM: // 삐용삐용! 에러 발생!
                    lblStatusBig.Text = "> ALARM";
                    lblStatusBig.ForeColor = System.Drawing.Color.Red;
                    lblStatusIndAlarm.ForeColor = System.Drawing.Color.Red;
                    lblAnimRobot.BackColor = System.Drawing.Color.Red;
                    break;
            }
        }

        // -------------------------------------------------------------
        // 5. 알람(에러) 발생기
        // -------------------------------------------------------------
        // 문제가 생기면 이 함수를 불러서 모든 작업을 멈추고 에러 메시지를 띄웁니다.
        private void TriggerAlarm(string code, string desc)
        {
            isAlarm = true;
            if (cts != null && !cts.IsCancellationRequested) cts.Cancel(); // 진행중인 모든 로봇 작업 멈춤!
            ChangeState(SystemState.ALARM);

            if (this.InvokeRequired)
                this.Invoke(new Action(() => ShowAlarmMsg(code, desc)));
            else
                ShowAlarmMsg(code, desc);
        }

        private void ShowAlarmMsg(string code, string desc)
        {
            lblFoupAStatus.Text = "ALARM";
            lblFoupAStatus.ForeColor = Color.Red;
            lblAnimRobot.Text = $"ALARM\n{code}\n{desc}";
        }

        // -------------------------------------------------------------
        // 6. 사용자가 누르는 버튼 기능들
        // -------------------------------------------------------------

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("시스템을 종료하시겠습니까?", "종료 확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Application.Exit(); // 프로그램 끄기
            }
        }

        private void btnWaferSet_Click(object sender, EventArgs e)
        {
            if (currentState == SystemState.ALARM) return;

            using (WaferSetupForm setupForm = new WaferSetupForm())
            {
                if (setupForm.ShowDialog() == DialogResult.OK)
                {
                    waferCountFOUPA = setupForm.SelectedWaferCount;
                    lblFoupAWafer.Text = $"Wafer : {waferCountFOUPA}";
                    if (waferCountFOUPA > 0)
                    {
                        lblFoupAStatus.Text = "Status : Mapped";
                        MessageBox.Show($"{waferCountFOUPA}장의 웨이퍼가 맵핑(준비) 되었습니다!", "Wafer Setup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        lblFoupAStatus.Text = "Status : Empty";
                    }
                }
            }
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            if (isAlarm) return; // 에러 상태면 안 움직임
            ChangeState(SystemState.HOME);
            // Z축, X축 모터를 차례로 영점(시작위치)으로 보냅니다.
            // 초기화 알람 처리
            try {
                EtherCAT_M.Axis1_UD_Homming();
                EtherCAT_M.Axis2_LR_Homming();
                // 호밍 명령은 비동기로 동작하므로, 상태는 일단 HOME으로 유지
                // (실제 원점 도달 여부는 Start 시 위치로 판단)
            }
            catch (Exception ex) { TriggerAlarm("HOME-ERR", "원점복귀 실패: " + ex.Message); }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            // 가상으로 웨이퍼 5개를 A에 넣습니다. (시뮬레이션 용도)
            waferCountFOUPA = 5;
            waferCountFOUPB = 0;
            currentWaferSlot = 1;
            lblFoupAWafer.Text = "Wafer : 5";
            lblFoupBWafer.Text = "Wafer : 0";
            lblFoupAStatus.Text = "Status : Loaded";
            isAlarm = false;
            ChangeState(SystemState.READY); // 장비 준비 완료!
        }



        private void btnManualCtrl_Click(object sender, EventArgs e)
        {
            if (this.EtherCAT_M == null) 
            {
                MessageBox.Show("장비가 아직 연결되지 않았습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 모달(팝업)로 Form1 띄우기
            using (Form1 f1 = new Form1())
            {
                // 기존 연결을 유지하기 위해 동일한 객체 참조 전달
                f1.EtherCAT_M = this.EtherCAT_M;
                f1.Text = "수동 조작 패널 (Form1)";
                f1.ShowDialog(); // 다른 창을 막고 모달로 실행됨
            }
        }

        // AUTO: 실제 웨이퍼로 운전 — 진공 압력 센서(DI14) 확인을 반드시 기다립니다.
        private void btnAuto_Click(object sender, EventArgs e)
        {
            isManualTestMode = false;
            btnAuto.BackColor = Color.CornflowerBlue;
            btnAuto.ForeColor = Color.White;
            btnManual.BackColor = SystemColors.Control;
            btnManual.ForeColor = SystemColors.ControlText;
        }

        // MANUAL: 웨이퍼 없이 동작 시퀀스만 점검하는 모드 — 웨이퍼가 없으면 진공 압력 센서가
        // 절대 감지되지 않으므로, 진공 ON 단계에서 센서 대기를 짧은 딜레이로 완화합니다.
        private void btnManual_Click(object sender, EventArgs e)
        {
            isManualTestMode = true;
            btnManual.BackColor = Color.CornflowerBlue;
            btnManual.ForeColor = Color.White;
            btnAuto.BackColor = SystemColors.Control;
            btnAuto.ForeColor = SystemColors.ControlText;
            MessageBox.Show("MANUAL(점검) 모드입니다.\n웨이퍼 없이 동작 시퀀스만 검증하며, 진공 센서 대기가 짧은 딜레이로 완화됩니다.\n실제 운전 시에는 반드시 AUTO 모드로 전환하세요.",
                "MANUAL 모드", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            if (currentState == SystemState.ALARM) return; // 에러면 시작 불가

            // 모터 전원 켜기 + 이송 파라미터(속도/가감속) 적용!
            try {
                EtherCAT_M.Axis1_ON();
                EtherCAT_M.Axis2_ON();

                // [중요] 모터 전원이 켜지고 드라이브가 OP 상태로 완전히 전환될 때까지 대기
                // 대기 없이 Config_Update를 보내면 드라이브가 파라미터를 무시하여 속도가 0이 될 수 있음
                await Task.Delay(500);

                // 모터 ON 후 이송 파라미터 적용
                EtherCAT_M.Axis1_UD_Config_Update(defaultAccel, defaultDecel, defaultMaxSpeed, defaultSpeed);
                EtherCAT_M.Axis2_LR_Config_Update(defaultAccel, defaultDecel, defaultMaxSpeed, defaultSpeed);
            } catch (Exception ex) { TriggerAlarm("MOTOR-INIT", "모터 전원/파라미터 적용 실패: " + ex.Message); return; }

            cts = new CancellationTokenSource();
            try
            {
                btnStart.Enabled = false; // 작업 중에 또 시작 누르지 못하게 막기
                await StartSequenceAsync(cts.Token); // 실제 작업 시퀀스 출발!
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                TriggerAlarm("SYS-ERR", ex.Message);
            }
            finally
            {
                btnStart.Enabled = true;
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            TriggerAlarm("USER-STOP", "사용자 정지");
            // 모터 전원 강제 차단!
            try {
                EtherCAT_M.Axis1_OFF();
                EtherCAT_M.Axis2_OFF();
                // 블레이드 후진 (원래 핀번호 복구: 13번이 후진)
                EtherCAT_M.Digital_Output(13, true);
                EtherCAT_M.Digital_Output(12, false);
                // 진공(흡기)/배기 강제 OFF (직전 동작에서 켜져있던 상태가 남지 않도록)
                EtherCAT_M.Digital_Output(14, false);
                EtherCAT_M.Digital_Output(15, false);
            }
            catch (Exception ex) { MessageBox.Show("비상정지 모터 차단 실패: " + ex.Message, "STOP 오류", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private async void btnReset_Click(object sender, EventArgs e)
        {
            // 에러를 풀고 상태를 초기화합니다.
            isAlarm = false;
            // [수정] Reset을 누른다고 매핑된 웨이퍼 정보가 날아가면 매번 다시 세팅해야 하므로 주석 처리
            // waferCountFOUPA = 0;
            // waferCountFOUPB = 0;
            // lblFoupAWafer.Text = "Wafer : 0";
            // lblFoupBWafer.Text = "Wafer : 0";
            lblFoupAStatus.Text = "Status : Ready";
            ChangeState(SystemState.INIT);

            // 장비 미연결 시 크래시 방지를 위해 try-catch 처리
            try
            {
                //서버 모터 전원 끄기
                EtherCAT_M.Axis1_OFF(); EtherCAT_M.Axis2_OFF();

                // 서보 모터 전원 켜기
                EtherCAT_M.Axis1_ON();
                EtherCAT_M.Axis2_ON();

                // [중요] 모터 전원이 켜지고 드라이브가 OP 상태로 완전히 전환될 때까지 대기
                System.Threading.Thread.Sleep(500);

                // 이송 파라미터 재적용
                EtherCAT_M.Axis1_UD_Config_Update(defaultAccel, defaultDecel, defaultMaxSpeed, defaultSpeed);
                EtherCAT_M.Axis2_LR_Config_Update(defaultAccel, defaultDecel, defaultMaxSpeed, defaultSpeed);

                // 서보 모터 좌우 원점 및 상하 원점으로 이동 (비동기 호밍)
                EtherCAT_M.Axis1_UD_Homming();
                EtherCAT_M.Axis2_LR_Homming();
                
                // [결함 수정] 사용자가 호밍이 끝나기 전에 Start 버튼을 눌러버리는 Race Condition을 방지하기 위해,
                // 호밍이 완료되어 위치가 0이 될 때까지 대기합니다. (UI 블로킹 방지)
                btnStart.Enabled = false;
                btnReset.Enabled = false;

                if (isManualTestMode)
                {
                    // [MANUAL 점검 모드] 정밀 원점(위치=0) 확인 대신 고정 딜레이로 대체 (빠른 로직 검증용)
                    await Task.Delay(3000, CancellationToken.None);
                }
                else
                {
                    // 실제 위치가 0(원점)에 도달할 때까지 최대 40초 대기
                    await WaitForAxis1Async(0, CancellationToken.None);
                    await WaitForAxis2Async(0, CancellationToken.None);
                }

                btnStart.Enabled = true;
                btnReset.Enabled = true;

                // 타워램프 초기화 (적, 황, 녹) 모두 OFF
                EtherCAT_M.Digital_Output(0, false); // 적색 OFF
                EtherCAT_M.Digital_Output(1, false); // 황색 OFF
                EtherCAT_M.Digital_Output(2, false); // 녹색 OFF
                // 블레이드 후진 (원래 핀번호 복구: 13번이 후진)
                EtherCAT_M.Digital_Output(13, true);
                EtherCAT_M.Digital_Output(12, false);
                // 진공(흡기)/배기 강제 OFF (직전 동작에서 켜져있던 상태가 남지 않도록)
                EtherCAT_M.Digital_Output(14, false);
                EtherCAT_M.Digital_Output(15, false);
            }
            catch (Exception ex)
            {
                TriggerAlarm("RESET-ERR", "리셋 중 하드웨어 오류: " + ex.Message);
            }
        }

        // -------------------------------------------------------------
        // 7. 핵심 자동화 시퀀스 (로봇이 스스로 일하는 순서)
        // -------------------------------------------------------------
        private async Task StartSequenceAsync(CancellationToken token)
        {
            if (waferCountFOUPA <= 0)
            {
                TriggerAlarm("AL-01", "FOUP A에 웨이퍼가 없습니다.");
                return;
            }

            ChangeState(SystemState.HOME);
            // [로봇 원점 복귀] 반드시 상/하(Z, Axis1)를 먼저 원점 복귀시키고,
            // 완료된 뒤에만 좌/우(X, Axis2)를 원점 복귀합니다.
            // (Z축이 원점 복귀되지 않은 상태에서는 X축 원점 복귀가 불가능하다는 장비 규칙 반영)
            try
            {
                EtherCAT_M.Axis1_UD_Homming();
                if (isManualTestMode)
                    await Task.Delay(3000, token); // [MANUAL 점검 모드] 정밀 원점 확인 대신 고정 딜레이
                else
                    await WaitForAxis1Async(0, token);

                EtherCAT_M.Axis2_LR_Homming();
                if (isManualTestMode)
                    await Task.Delay(3000, token);
                else
                    await WaitForAxis2Async(0, token);
            }
            catch (Exception ex) { throw new Exception("로봇 원점 복귀 실패: " + ex.Message); }

            ChangeState(SystemState.READY);
            await Task.Delay(300, token); // 원점 복귀 후 안정화 대기

            // 작업 시작: 타워 램프 초록색(Green) ON (나머지 OFF)
            SetTowerLamp(false, false, true);

            while (waferCountFOUPA > 0 && currentWaferSlot <= 5)
            {
                // 1. FOUP A에서 픽업
                ChangeState(SystemState.LOAD);
                await RobotPickAsync("FOUP_A", currentWaferSlot, token);

                // 2. PM1 연마
                ChangeState(SystemState.PROCESS);
                await RobotPlaceAsync("PM1", 1, token);
                await ChamberProcessAsync("PM1 연마", token);
                await RobotPickAsync("PM1", 1, token);

                // 3. PM2 세정
                await RobotPlaceAsync("PM2", 1, token);
                await ChamberProcessAsync("PM2 세정", token);
                await RobotPickAsync("PM2", 1, token);

                // 4. PM3 검사
                await RobotPlaceAsync("PM3", 1, token);
                await ChamberProcessAsync("PM3 검사", token);
                await RobotPickAsync("PM3", 1, token);

                // 5. FOUP B 적재
                ChangeState(SystemState.UNLOAD);
                await RobotPlaceAsync("FOUP_B", currentWaferSlot, token);

                // 화면 글자 업데이트 (A는 1개 줄고 B는 1개 늘어남)
                this.Invoke(new Action(() => {
                    waferCountFOUPA--;
                    waferCountFOUPB++;
                    lblFoupAWafer.Text = "Wafer : " + waferCountFOUPA.ToString();
                    lblFoupBWafer.Text = "Wafer : " + waferCountFOUPB.ToString();
                }));
                currentWaferSlot++; // 다음 칸 작업 준비
            }
            
            // 모든 웨이퍼 작업이 끝나면 완료 처리
            ChangeState(SystemState.COMPLETE);

            // 작업 완료: 타워 램프 3색(적, 황, 녹) 모두 ON
            SetTowerLamp(true, true, true);
        }

        // 로봇이 웨이퍼를 정밀하게 픽업하는 동작 (Kinematics)
        private async Task RobotPickAsync(string source, int slot, CancellationToken token)
        {
            // [사전 충돌 방지 인터록] 이동 전 블레이드가 완벽히 후진 상태인지 반드시 확인! (MANUAL 모드에서는 스킵)
            if (!isManualTestMode)
            {
                string bladeRetractState = EtherCAT_M.Digital_Input(12).ToString().ToLower();
                if (bladeRetractState != "1" && bladeRetractState != "true")
                {
                    throw new Exception($"[인터록 알람] 블레이드가 챔버/FOUP 안에 있습니다! 충돌 방지를 위해 로봇 이동을 차단합니다.");
                }
            }

            long targetX = GetXPos(source);
            long placeZ = GetZPlacePos(source, slot);
            long pickZ = GetZPickPos(source, slot);
            bool isChamber = !source.Contains("FOUP");

            // 1. X축(좌우) 이동 및 도착 대기 (In-Position Check)
            // 명령 전송 실패 시 즉시 예외 발생
            try { EtherCAT_M.Axis2_LR_POS_Update(targetX); EtherCAT_M.Axis2_LR_Move_Send(); }
            catch (Exception ex) { throw new Exception("X축 이동 명령 전송 실패: " + ex.Message); }
            await WaitForAxis2Async(targetX, token);

            // 2. 챔버 도어 열림 (FOUP은 도어가 없으므로 챔버일 때만 엽니다)
            // [인터록] 블레이드가 챔버 안으로 들어가려면 반드시 도어가 열려있어야 하므로
            // Z축이 움직이기 전에 미리 열어둡니다.
            if (isChamber)
            {
                await DoorOpenAsync(source, token);
                await Task.Delay(200, token); // 도어 개방 후 기구 진동 안정화 대기
            }

            // 3. Z축 안착 위치(Lower)로 하강 및 도착 대기
            try { EtherCAT_M.Axis1_UD_POS_Update(placeZ); EtherCAT_M.Axis1_UD_Move_Send(); }
            catch (Exception ex) { throw new Exception("Z축 하강 명령 전송 실패: " + ex.Message); }
            await WaitForAxis1Async(placeZ, token);

            // 블레이드 전진 직전 Z축 높이 이중 검증 (안전 인터록)
            await VerifyAxisPositionBeforeBlade(placeZ, "Z축 높이가 Pick 안착 위치와 불일치!", token);

            // [인터록] 챔버일 경우, 블레이드 전진 직전 도어가 실제로 열려있는지 센서로 한 번 더 재확인
            if (isChamber)
            {
                VerifyDoorOpenBeforeBlade(source);
            }

            // 4. 블레이드 전진 (센서 확인) — 위 검증을 모두 통과한 후에만 실행
            DebugStep("Pick: 블레이드 전진 호출 전");
            await BladeAdvanceAsync(token);
            DebugStep("Pick: 블레이드 전진 완료");
            await Task.Delay(200, token); // 블레이드 전진 후 기구 안정화 대기

            // 5. 진공(Vacuum) ON (센서 확인)
            DebugStep("Pick: 진공ON 호출 전");
            await VacuumOnAsync(token);
            DebugStep("Pick: 진공ON 완료");

            // 6. Z축 상승 위치(Upper)로 이동 및 도착 대기 (웨이퍼 들어올림)
            try { EtherCAT_M.Axis1_UD_POS_Update(pickZ); EtherCAT_M.Axis1_UD_Move_Send(); }
            catch (Exception ex) { throw new Exception("Z축 상승 명령 전송 실패: " + ex.Message); }
            DebugStep("Pick: Z축 상승 대기 중");
            await WaitForAxis1Async(pickZ, token);
            DebugStep("Pick: Z축 상승 완료");

            // 7. 블레이드 후진 (센서 확인)
            DebugStep("Pick: 블레이드 후진 호출 전");
            await BladeRetractAsync(token);
            DebugStep("Pick: 블레이드 후진 완료");
            await Task.Delay(200, token); // 블레이드 후진 후 기구 안정화 대기

            // 8. 챔버 도어 닫힘 (FOUP은 무시)
            if (isChamber)
            {
                await DoorCloseAsync(source, token);
            }

            // 9. Z축 안착 위치(Lower)로 복귀 및 도착 대기 (안전 주행 높이)
            try { EtherCAT_M.Axis1_UD_POS_Update(placeZ); EtherCAT_M.Axis1_UD_Move_Send(); }
            catch (Exception ex) { throw new Exception("Z축 복귀 명령 전송 실패: " + ex.Message); }
            await WaitForAxis1Async(placeZ, token);
        }

        // 챔버에서 공정 진행 시뮬레이션 (램프 깜빡임 포함)
        private async Task ChamberProcessAsync(string processName, CancellationToken token)
        {
            this.Invoke(new Action(() => lblProcStatus.Text = $"Status : {processName} 중..."));
            this.Invoke(new Action(() => lblProcWafer.Text = "Wafer : In Process"));

            int lampPin = GetChamberLampPin(processName);
            int processTime = 5000; // 실제 공정 진행 시간 (연마/세정/검사 공통 5초로 가정)
            int elapsed = 0;
            bool lampState = false;

            // 공정 시간 동안 0.5초(500ms) 간격으로 램프를 깜빡거립니다 (Blinking)
            while (elapsed < processTime)
            {
                if (lampPin != -1)
                {
                    lampState = !lampState;
                    try { EtherCAT_M.Digital_Output(lampPin, lampState); }
                    catch (Exception ex) { throw new Exception("챔버 램프 출력 실패: " + ex.Message); }
                }

                int delay = Math.Min(500, processTime - elapsed);
                await Task.Delay(delay, token);
                elapsed += delay;
            }

            // 공정이 끝나면 해당 챔버 램프를 확실하게 OFF 합니다.
            if (lampPin != -1)
            {
                try { EtherCAT_M.Digital_Output(lampPin, false); }
                catch (Exception ex) { throw new Exception("챔버 램프 OFF 실패: " + ex.Message); }
            }

            this.Invoke(new Action(() => lblProcStatus.Text = "Status : Idle"));
            this.Invoke(new Action(() => lblProcWafer.Text = "Wafer : None"));
        }

        // 로봇이 웨이퍼를 정밀하게 내려놓는 동작 (Place Kinematics)
        private async Task RobotPlaceAsync(string dest, int slot, CancellationToken token)
        {
            // [사전 충돌 방지 인터록] 이동 전 블레이드가 완벽히 후진 상태인지 반드시 확인! (MANUAL 모드에서는 스킵)
            if (!isManualTestMode)
            {
                string bladeRetractState = EtherCAT_M.Digital_Input(12).ToString().ToLower();
                if (bladeRetractState != "1" && bladeRetractState != "true")
                {
                    throw new Exception($"[인터록 알람] 블레이드가 챔버/FOUP 안에 있습니다! 충돌 방지를 위해 로봇 이동을 차단합니다.");
                }
            }

            long targetX = GetXPos(dest);
            long placeZ = GetZPlacePos(dest, slot);
            long pickZ = GetZPickPos(dest, slot);
            bool isChamber = !dest.Contains("FOUP");

            // 1. X축(좌우) 이동 및 도착 대기
            // 명령 전송 실패 시 즉시 예외 발생
            try { EtherCAT_M.Axis2_LR_POS_Update(targetX); EtherCAT_M.Axis2_LR_Move_Send(); }
            catch (Exception ex) { throw new Exception("X축 이동 명령 전송 실패: " + ex.Message); }
            await WaitForAxis2Async(targetX, token);

            // 2. 챔버 도어 열림 (FOUP은 무시)
            // [인터록] 블레이드가 챔버 안으로 들어가려면 반드시 도어가 열려있어야 하므로
            // Z축이 움직이기 전에 미리 열어둡니다.
            if (isChamber)
            {
                await DoorOpenAsync(dest, token);
                await Task.Delay(200, token); // 도어 개방 후 기구 진동 안정화 대기
            }

            // 3. Z축 상승 위치(Upper)로 이동 및 도착 대기
            try { EtherCAT_M.Axis1_UD_POS_Update(pickZ); EtherCAT_M.Axis1_UD_Move_Send(); }
            catch (Exception ex) { throw new Exception("Z축 상승 명령 전송 실패: " + ex.Message); }
            await WaitForAxis1Async(pickZ, token);

            // 블레이드 전진 직전 Z축 높이 이중 검증 (안전 인터록)
            await VerifyAxisPositionBeforeBlade(pickZ, "Z축 높이가 Place 상승 위치와 불일치!", token);

            // [인터록] 챔버일 경우, 블레이드 전진 직전 도어가 실제로 열려있는지 센서로 한 번 더 재확인
            if (isChamber)
            {
                VerifyDoorOpenBeforeBlade(dest);
            }

            // 4. 블레이드 전진 (센서 확인) — 위 검증을 모두 통과한 후에만 실행
            DebugStep("Place: 블레이드 전진 호출 전");
            await BladeAdvanceAsync(token);
            DebugStep("Place: 블레이드 전진 완료");
            await Task.Delay(200, token); // 블레이드 전진 후 기구 안정화 대기

            // 5. Z축 안착 위치(Lower)로 하강 및 도착 대기 (웨이퍼 얹기)
            try { EtherCAT_M.Axis1_UD_POS_Update(placeZ); EtherCAT_M.Axis1_UD_Move_Send(); }
            catch (Exception ex) { throw new Exception("Z축 하강 명령 전송 실패: " + ex.Message); }
            DebugStep("Place: Z축 하강 대기 중");
            await WaitForAxis1Async(placeZ, token);
            DebugStep("Place: Z축 하강 완료");

            // 6. 진공(흡기) OFF — 배기(Blow)는 블레이드 후진용 공압과 경합할 수 있어 일단 사용하지 않음
            VacuumOff();
            await Task.Delay(300, token); // 웨이퍼 안착/흡착 해제 안정화 대기

            // 7. 블레이드 후진 (센서 확인)
            DebugStep("Place: 블레이드 후진 호출 전");
            await BladeRetractAsync(token);
            DebugStep("Place: 블레이드 후진 완료");
            await Task.Delay(200, token); // 블레이드 후진 후 기구 안정화 대기

            // 8. 배기(Blow) 잔류 방지용 OFF (혹시 켜져 있었을 경우 대비)
            BlowOff();

            // 9. 챔버 도어 닫힘 (FOUP은 무시) — 닫힘 완료 후 공정(연마/세정/검사)이 시작됨
            if (isChamber)
            {
                await DoorCloseAsync(dest, token);
            }
        }

        private void lblUser_Click(object sender, EventArgs e)
        {

        }

        // -------------------------------------------------------------
        // 8. 하드웨어 IO 제어 (블레이드, 진공, 배기, 도어) - 센서 기반 완벽 인터록 (S/W Interlock)
        // -------------------------------------------------------------
        // 하드웨어 IO 제어 (블레이드, 진공, 배기, 도어) - 센서 기반 완벽 인터록(S/W Interlock)
        private async Task BladeAdvanceAsync(CancellationToken token) 
        { 
            try { EtherCAT_M.Digital_Output(12, true); EtherCAT_M.Digital_Output(13, false); }
            catch (Exception ex) { throw new Exception("블레이드 전진 출력 실패: " + ex.Message); }
            await WaitForSensorAsync(13, true, 10000, token, "블레이드 전진 타임아웃!"); // DI(13): 전진 센서
        }
        private async Task BladeRetractAsync(CancellationToken token) 
        { 
            try { EtherCAT_M.Digital_Output(13, true); EtherCAT_M.Digital_Output(12, false); }
            catch (Exception ex) { throw new Exception("블레이드 후진 출력 실패: " + ex.Message); }
            await WaitForSensorAsync(12, true, 10000, token, "블레이드 후진 타임아웃"); // DI(12): 후진 센서
        }
        private async Task VacuumOnAsync(CancellationToken token) 
        { 
            try { 
                EtherCAT_M.Digital_Output(15, false); // 배기(Blow) 확실히 끄기
                EtherCAT_M.Digital_Output(14, true);  // 진공(Suction) 켜기
            }
            catch (Exception ex) { throw new Exception("진공 ON 출력 실패: " + ex.Message); }

            if (isManualTestMode)
            {
                // [MANUAL 점검 모드 전용 예외] 웨이퍼가 없으면 진공 압력 센서는 절대 감지될 수 없고,
                // 이 대기는 충돌과 무관(단순 흡착 확인)하므로 여기서만 짧은 딜레이로 대체합니다.
                await Task.Delay(300, token);
                return;
            }

            await WaitForSensorAsync(14, true, 10000, token, "진공 형성 타임아웃! (웨이퍼가 실제로 안착되었는지 확인하세요)"); // DI(14): 진공 압력 센서
        }
        private void VacuumOff() 
        { 
            try { EtherCAT_M.Digital_Output(14, false); }
            catch (Exception ex) { throw new Exception("진공 OFF 출력 실패: " + ex.Message); }
        }
        private void BlowOn() 
        { 
            try { 
                EtherCAT_M.Digital_Output(14, false); // 진공(Suction) 확실히 끄기
                EtherCAT_M.Digital_Output(15, true);  // 배기(Blow) 켜기
            }
            catch (Exception ex) { throw new Exception("배기 ON 출력 실패: " + ex.Message); }
        }
        private void BlowOff() 
        { 
            try { EtherCAT_M.Digital_Output(15, false); }
            catch (Exception ex) { throw new Exception("배기 OFF 출력 실패: " + ex.Message); }
        }
        private async Task DoorOpenAsync(string location, CancellationToken token) 
        { 
            int sensorPin = -1;
            try {
                // 도어 하강 (Open)
                if (location.Contains("PM1")) { EtherCAT_M.Digital_Output(4, false); EtherCAT_M.Digital_Output(5, true); sensorPin = 7; }
                else if (location.Contains("PM2")) { EtherCAT_M.Digital_Output(7, false); EtherCAT_M.Digital_Output(8, true); sensorPin = 9; }
                else if (location.Contains("PM3")) { EtherCAT_M.Digital_Output(10, false); EtherCAT_M.Digital_Output(11, true); sensorPin = 11; }
            }
            catch (Exception ex) { throw new Exception($"{location} 도어 열림(하강) 출력 실패: " + ex.Message); }

            if (sensorPin == -1) throw new Exception($"[인터록 알람] {location} 도어 열림 센서 핀 매핑 실패!");
            
            // 물리 센서를 통해 도어가 완전히 열렸는지 확인 (타임아웃 10초)
            await WaitForSensorAsync(sensorPin, true, 10000, token, $"{location} 도어 열림 센서 감지 타임아웃!");
        }
        private async Task DoorCloseAsync(string location, CancellationToken token)
        {
            int sensorPin = -1;
            try {
                // 도어 상승 (Close)
                if (location.Contains("PM1")) { EtherCAT_M.Digital_Output(5, false); EtherCAT_M.Digital_Output(4, true); sensorPin = 6; }
                else if (location.Contains("PM2")) { EtherCAT_M.Digital_Output(8, false); EtherCAT_M.Digital_Output(7, true); sensorPin = 8; }
                else if (location.Contains("PM3")) { EtherCAT_M.Digital_Output(11, false); EtherCAT_M.Digital_Output(10, true); sensorPin = 10; }
            }
            catch (Exception ex) { throw new Exception($"{location} 도어 닫힘(상승) 출력 실패: " + ex.Message); }

            if (sensorPin == -1) throw new Exception($"[인터록 알람] {location} 도어 닫힘 센서 핀 매핑 실패!");
            
            // 물리 센서를 통해 도어가 완전히 닫혔는지 확인 (타임아웃 10초)
            await WaitForSensorAsync(sensorPin, true, 10000, token, $"{location} 도어 닫힘 센서 감지 타임아웃!");
        }

        // 블레이드 전진 직전 Z축 높이 이중 검증 헬퍼 함수
        private async Task VerifyAxisPositionBeforeBlade(long expectedZ, string errorContext, CancellationToken token)
        {
            if (isManualTestMode) return; // [MANUAL 점검 모드] Z축 재검증 스킵

            await Task.Delay(200, token); // 잔진동 안정화 대기
            string rawData = EtherCAT_M.Axis1_is_PosData();
            string numStr = System.Text.RegularExpressions.Regex.Match(rawData ?? "", @"-?\d+(\.\d+)?").Value;
            if (double.TryParse(numStr, out double currentZ))
            {
                if (Math.Abs(currentZ - expectedZ) > 1000) // 허용 오차 1000 (블레이드 전진 전 안전 마진)
                {
                    throw new Exception($"[인터록 알람] {errorContext} 현재Z={currentZ}, 목표Z={expectedZ}, 편차={Math.Abs(currentZ - expectedZ)}");
                }
            }
            else
            {
                throw new Exception("[인터록 알람] Z축 위치 데이터 읽기 실패! 블레이드 전진을 차단합니다.");
            }
        }

        // 블레이드 전진 직전, 챔버 도어가 실제로(센서 기준) 열려있는지 최종 확인하는 이중 인터록 함수
        private void VerifyDoorOpenBeforeBlade(string location)
        {
            if (isManualTestMode) return; // [MANUAL 점검 모드] 도어 센서 재확인 스킵

            int sensorPin;
            if (location.Contains("PM1")) sensorPin = 7;       // A챔버 도어 하강(열림) 감지
            else if (location.Contains("PM2")) sensorPin = 9;  // B챔버 도어 하강(열림) 감지
            else if (location.Contains("PM3")) sensorPin = 11; // C챔버 도어 하강(열림) 감지
            else return; // 매핑되지 않은 위치(FOUP 등)는 도어가 없으므로 스킵

            string val = EtherCAT_M.Digital_Input(sensorPin).ToString().ToLower();
            if (val != "1" && val != "true")
            {
                throw new Exception($"[인터록 알람] {location} 도어가 열려있지 않습니다! 충돌 방지를 위해 블레이드 전진을 차단합니다.");
            }
        }


        // 디지털 입력(센서) 대기 전용 헬퍼 함수
        private async Task WaitForSensorAsync(int sensorPin, bool expectedState, int timeoutMs, CancellationToken token, string errorMsg)
        {
            if (isManualTestMode)
            {
                // [MANUAL 점검 모드] 센서 확인 없이 짧은 딜레이만 두고 통과 (빠른 로직 검증용)
                await Task.Delay(300, token);
                return;
            }

            int elapsed = 0;
            while (!token.IsCancellationRequested && elapsed < timeoutMs)
            {
                string val = EtherCAT_M.Digital_Input(sensorPin).ToString().ToLower();
                if (expectedState)
                {
                    if (val == "1" || val == "true") return; // 센서 감지 성공!
                }
                else
                {
                    if (val == "0" || val == "false") return; // 센서 감지 성공!
                }
                
                await Task.Delay(100, token);
                elapsed += 100;
            }
            if (token.IsCancellationRequested) throw new OperationCanceledException();
            throw new Exception(errorMsg); // 타임아웃 발생 시 강제 정지
        }

        // -------------------------------------------------------------
        // 9. 타워 램프 및 챔버 램프(IO) 매핑 함수
        // -------------------------------------------------------------
        private void SetTowerLamp(bool red, bool yellow, bool green)
        {
            // 센서 타임아웃 감지 로직
            try
            {
                EtherCAT_M.Digital_Output(0, red);
                EtherCAT_M.Digital_Output(1, yellow);
                EtherCAT_M.Digital_Output(2, green);
            }
            catch (Exception ex) { throw new Exception("타워 램프 출력 실패: " + ex.Message); }
        }

        private int GetChamberLampPin(string chamberName)
        {
            if (chamberName.Contains("PM1")) return 3;
            if (chamberName.Contains("PM2")) return 6;
            if (chamberName.Contains("PM3")) return 9;
            return -1;
        }


        // -------------------------------------------------------------
        // 10. 모터 이동 대기 (In-Position Check) 헬퍼 함수 - 무한루프 방지 및 예외처리 강화
        // -------------------------------------------------------------
        private async Task WaitForAxis1Async(long targetPos, CancellationToken token)
        {
            // [MANUAL 점검 모드] 정밀 도착 확인 없이 8초만 대기하고, 못 도달해도 알람 없이 그냥 진행 (빠른 로직 검증용)
            int timeoutMs = isManualTestMode ? 8000 : 40000;
            int elapsed = 0;
            bool reached = false;
            string lastRaw = "(읽기실패)";
            double lastPos = double.NaN;

            while (!token.IsCancellationRequested && elapsed < timeoutMs)
            {
                lastRaw = EtherCAT_M.Axis1_is_PosData() ?? "(null)";
                string numStr = System.Text.RegularExpressions.Regex.Match(lastRaw, @"-?\d+").Value; // 정수만 추출

                if (long.TryParse(numStr, out long currentPos))
                {
                    lastPos = currentPos;
                    // 허용 오차 500 (실제 장비 기구부 오차 반영)
                    if (Math.Abs(currentPos - targetPos) <= 500)
                    {
                        reached = true;
                        // 기구부 진동이 멈추고 드라이브가 안정화될 수 있도록 0.5초 대기
                        await Task.Delay(500, token);
                        break;
                    }
                }
                await Task.Delay(100, token);
                elapsed += 100;
            }

            if (!reached)
            {
                if (isManualTestMode)
                {
                    // 알람 없이 그냥 다음 단계로 진행 (MANUAL 모드는 로직 검증이 목적)
                    return;
                }
                // 타임아웃 시 현재 실제 위치와 목표 위치를 에러 메시지에 포함 (디버깅 핵심)
                throw new Exception(
                    $"Z축(Axis1) 이동 시간 초과!\n"
                    + $"목표위치: {targetPos}\n"
                    + $"현재위치: {(double.IsNaN(lastPos) ? "파싱실패" : lastPos.ToString())}\n"
                    + $"원시데이터: {lastRaw}\n"
                    + "→ 모터가 전혀 안 움직였다면 파라미터(속도) 미적용 또는 서보ON 실패입니다.");
            }
            await Task.Delay(300, token); // 도착 후 잔진동 안정화 대기
        }

        private async Task WaitForAxis2Async(long targetPos, CancellationToken token)
        {
            // [MANUAL 점검 모드] 정밀 도착 확인 없이 8초만 대기하고, 못 도달해도 알람 없이 그냥 진행 (빠른 로직 검증용)
            int timeoutMs = isManualTestMode ? 8000 : 40000;
            int elapsed = 0;
            bool reached = false;
            string lastRaw = "(읽기실패)";
            double lastPos = double.NaN;

            while (!token.IsCancellationRequested && elapsed < timeoutMs)
            {
                lastRaw = EtherCAT_M.Axis2_is_PosData() ?? "(null)";
                string numStr = System.Text.RegularExpressions.Regex.Match(lastRaw, @"-?\d+").Value; // 정수만 추출

                if (long.TryParse(numStr, out long currentPos))
                {
                    lastPos = currentPos;
                    // 허용 오차 500 (실제 장비 기구부 오차 반영)
                    if (Math.Abs(currentPos - targetPos) <= 500)
                    {
                        reached = true;
                        // 기구부 진동이 멈추고 드라이브가 안정화될 수 있도록 0.5초 대기
                        await Task.Delay(500, token);
                        break;
                    }
                }
                await Task.Delay(100, token);
                elapsed += 100;
            }

            if (!reached)
            {
                if (isManualTestMode)
                {
                    // 알람 없이 그냥 다음 단계로 진행 (MANUAL 모드는 로직 검증이 목적)
                    return;
                }
                // 타임아웃 시 현재 실제 위치와 목표 위치를 에러 메시지에 포함 (디버깅 핵심)
                throw new Exception(
                    $"X축(Axis2) 이동 시간 초과!\n"
                    + $"목표위치: {targetPos}\n"
                    + $"현재위치: {(double.IsNaN(lastPos) ? "파싱실패" : lastPos.ToString())}\n"
                    + $"원시데이터: {lastRaw}\n"
                    + "→ 모터가 전혀 안 움직였다면 파라미터(속도) 미적용 또는 서보ON 실패입니다.");
            }
            await Task.Delay(300, token);
        }

        // -------------------------------------------------------------
        // 11. 티칭 데이터 (Teaching Data) 관리 함수
        // -------------------------------------------------------------
        private long GetZPlacePos(string location, int slot)
        {
            if (location == "FOUP_A")
            {
                switch (slot) {
                    case 1: return 102379;
                    case 2: return 782378;
                    case 3: return 1432388;
                    case 4: return 2119399;
                    case 5: return 2818463;
                }
            }
            else if (location == "FOUP_B")
            {
                switch (slot) {
                    case 1: return 102379;
                    case 2: return 782378;
                    case 3: return 1432388;
                    case 4: return 2119399;
                    case 5: return 2818463;
                }
            }
            else // CHAMBER (PM1, PM2, PM3)
            {
                return 806931; // 안착위치
            }
            return 0;
        }

        private long GetZPickPos(string location, int slot)
        {
            if (location == "FOUP_A" || location == "FOUP_B")
            {
                switch (slot) {
                    case 1: return 302380;
                    case 2: return 982378;
                    case 3: return 1627604;
                    case 4: return 2332102;
                    case 5: return 3018457;
                }
            }
            else // CHAMBER (PM1, PM2, PM3)
            {
                return 1156931; // 상승위치
            }
            return 0;
        }

        private long GetXPos(string location)
        {
            switch (location) {
                case "FOUP_A": return 12000;
                case "PM1": return -60000;
                case "PM2": return -190823;
                case "PM3": return -322000;
                case "FOUP_B": return -395690;
                default: return 0;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            using (LoginForm loginForm = new LoginForm())
            {
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    if (loginForm.IsAuthenticated)
                    {
                        lblUser.Text = "User\nadmin";
                        MessageBox.Show("장비 연결 완료", "로그인 성공", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (EtherCAT_M.CIFX_50RE_Connect() == true)
                        {
                            lblSystem.Text = "System\nConnected";
                            EtherCAT_M.ReadData_Send_Start(300); //Timer Interval Set
                            EtherCAT_M.ReadData_Timer_Start(); //Timer Start

                            // 타임아웃 해제 후 파라미터(속도 등) 재적용 
                            EtherCAT_M.Axis1_UD_Config_Update(defaultAccel, defaultDecel, defaultMaxSpeed, defaultSpeed);
                            EtherCAT_M.Axis2_LR_Config_Update(defaultAccel, defaultDecel, defaultMaxSpeed, defaultSpeed);

                            // UI 갱신 타이머 설정 및 시작 (타이머 이름: uiTimer)
                            uiTimer.Interval = 300;
                            uiTimer.Start();
                        }
                        else
                        {
                            lblSystem.Text = "System\nDisconn";
                        }
                    }
                }
            }
        }
    }
}
