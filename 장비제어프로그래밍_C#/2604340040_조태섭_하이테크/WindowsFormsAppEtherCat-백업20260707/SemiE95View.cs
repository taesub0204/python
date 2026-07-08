using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML.Excel;
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
        public long defaultDecel = 1000000; //5000000; // [오버슈트 대책] Config_Update의 속도 파라미터가 이 드라이브에서 효과 없고
        // 감속도만 유효한 것으로 실측 확인됨. 드라이브 기본 속도(~440K c/s) 기준으로
        // 정지 거리 = 440K² / (2 × 5M) ≈ 19,360 카운트 이내로 수렴하도록 감속도 5배 상향.
        // [오버슈트 대책] 최대속도가 1억(사실상 무제한)이면 긴 이동에서 축이 목표 도달 시점까지 ~420K/s로
        // 달리다 그제야 감속을 시작해 88K카운트를 지나쳐 정지함(도착판정 실패→알람). 오차 없이 착지하던
        // 짧은 이동의 최고속도(~200K/s) 수준으로 상한을 둠. (단, 이 값이 실제로 반영되려면 호밍 이후에
        // Config_Update를 다시 호출해야 함 — 호밍이 프로파일을 리셋하기 때문. StartSequenceAsync 참고.)
        // 실기에서 미세조정 가능(더 지나치면 더 낮추고, 너무 느리면 조금 올림).
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
        private List<int> selectedSlots = new List<int>(); // Wafer Setup에서 체크한 슬롯 번호만 담김 (예: 3번만 체크하면 {3}만 작업)

        // 실제 모터를 움직이게 해주는 마법의 리모컨(객체)입니다. Form1에서 넘겨받습니다.
        public IEG3268 EtherCAT_M; 
        
        // 작업(Sequence)을 중간에 취소할 수 있게 해주는 비상정지 버튼 같은 역할입니다.
        private CancellationTokenSource cts;

        public SemiE95View()
        {
            InitializeComponent();
            LoggerConfig.AttachTextBox(txtLog); // 로그를 화면 하단 LOG 창에 실시간으로 표시
            UpdateFoupAStatus(); // FOUP A/B 상태를 실제 웨이퍼 개수(초기값 0)에 맞춰 초기화
            UpdateFoupBStatus();
            // 화면이 켜지면 1초에 한 번씩 화면을 업데이트하는 타이머를 작동시킵니다.
            uiTimer.Start();
        }

        // FOUP A/B의 Status 라벨을 실제 웨이퍼 개수에 맞춰 갱신합니다. (0개면 "-", 있으면 "Loaded")
        private void UpdateFoupAStatus()
        {
            lblFoupAStatus.Text = waferCountFOUPA > 0 ? "Status : Loaded" : "Status : -";
            // 알람(ShowAlarmMsg)이 빨간색으로 바꿔놓은 글자색을 정상 색으로 복구
            lblFoupAStatus.ForeColor = System.Drawing.SystemColors.ControlText;
        }

        private void UpdateFoupBStatus()
        {
            lblFoupBStatus.Text = waferCountFOUPB > 0 ? "Status : Loaded" : "Status : -";
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
                // 버튼이 눌려서 신호가 들어오면 즉시 에러 알람을 띄웁니다. (Digital_Input은 "1" 또는 "True" 문자열을 반환하므로 둘 다 확인)
                string emgRaw = EtherCAT_M.Digital_Input(3).ToString().ToLower();
                if ((emgRaw == "1" || emgRaw == "true") && !isAlarm)
                {
                    TriggerAlarm("EMG", "하드웨어 비상정지 버튼 눌림!");
                }

                // 2. 장비 상태(Door, Wafer 등)를 센서 값에 맞춰서 화면 글자로 바꿔줍니다.
                // 챔버 A/B/C 도어 열림(하강) 감지 센서: DI7=PM1(A), DI9=PM2(B), DI11=PM3(C)
                // 셋 중 하나라도 열려있으면 "Open", 셋 다 닫혀있어야 "Closed"
                string doorARaw = EtherCAT_M.Digital_Input(7).ToString().ToLower();
                string doorBRaw = EtherCAT_M.Digital_Input(9).ToString().ToLower();
                string doorCRaw = EtherCAT_M.Digital_Input(11).ToString().ToLower();
                bool doorAOpen = (doorARaw == "1" || doorARaw == "true");
                bool doorBOpen = (doorBRaw == "1" || doorBRaw == "true");
                bool doorCOpen = (doorCRaw == "1" || doorCRaw == "true");
                bool anyDoorOpen = doorAOpen || doorBOpen || doorCOpen;
                lblDoorStatus.Text = anyDoorOpen ? "Open" : "Closed";
                lblDoorStatus.ForeColor = anyDoorOpen ? Color.Red : Color.Green;
                string waferRaw = EtherCAT_M.Digital_Input(1).ToString().ToLower();
                lblWaferStatus.Text = (waferRaw == "1" || waferRaw == "true") ? "Loaded" : "Empty";
                string bladeRaw = EtherCAT_M.Digital_Input(2).ToString().ToLower();
                lblBladeStatus.Text = (bladeRaw == "1" || bladeRaw == "true") ? "Wafer Detected" : "Empty";
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
            LoggerConfig.Log.Info($"상태 변경: {newState}");
            if (this.InvokeRequired) // 다른 쓰레드(작업자)가 화면을 고치려고 하면 안전하게 고치도록 도와줌
            {
                this.Invoke(new Action(() => UpdateUIForState(newState)));
            }
            else
            {
                UpdateUIForState(newState);
            }
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
            LoggerConfig.Log.Error($"[ALARM:{code}] {desc}");
            isAlarm = true;
            if (cts != null && !cts.IsCancellationRequested) cts.Cancel(); // 진행중인 모든 로봇 작업 멈춤!
            ChangeState(SystemState.ALARM);

            // 정지/이상동작 시 타워램프 적색 점등 (실제 IO 출력 실패해도 알람 표시 자체는 계속 진행)
            try { SetTowerLamp(true, false, false); } catch { }

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

        // 시연 중 알람 복구 순서를 보여주는 도움말 창. 모덜리스(Show)로 띄워서 조작 화면과
        // 동시에 띄워둘 수 있게 하고, 중복으로 여러 개 뜨지 않도록 기존 창을 재사용/포커스함.
        private RecoveryHelpForm recoveryHelpForm;

        private void btnHelp_Click(object sender, EventArgs e)
        {
            if (recoveryHelpForm == null || recoveryHelpForm.IsDisposed)
            {
                recoveryHelpForm = new RecoveryHelpForm();
                recoveryHelpForm.Show();
            }
            else
            {
                recoveryHelpForm.Activate();
            }
        }

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
                    selectedSlots = setupForm.SelectedSlots;
                    waferCountFOUPA = selectedSlots.Count;
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
            selectedSlots = new List<int> { 1, 2, 3, 4, 5 };
            waferCountFOUPA = 5;
            waferCountFOUPB = 0;
            lblFoupAWafer.Text = "Wafer : 5";
            lblFoupBWafer.Text = "Wafer : 0";
            lblFoupAStatus.Text = "Status : Loaded";
            UpdateFoupBStatus();
            isAlarm = false;
            ChangeState(SystemState.READY); // 장비 준비 완료!

            // 모터 전원 켜기 + 이송 파라미터(속도/가감속) 적용!

            EtherCAT_M.Axis1_ON();
            EtherCAT_M.Axis2_ON();

            // [중요] 모터 전원이 켜지고 드라이브가 OP 상태로 완전히 전환될 때까지 대기
            // 대기 없이 Config_Update/호밍을 보내면 드라이브가 아직 준비 전이라 명령을 제대로 못 받아들여
            // 위치가 멈춰버리는("드라이브 무응답") 현상과 연관 가능성이 있어 500ms→1500ms로 늘림


            // 모터 ON 후 이송 파라미터 적용
            EtherCAT_M.Axis1_UD_Config_Update(defaultAccel, defaultDecel, defaultMaxSpeed, defaultSpeed);
            EtherCAT_M.Axis2_LR_Config_Update(defaultAccel, defaultDecel, defaultMaxSpeed, defaultSpeed);
  
        }



        // 모달리스로 띄운 수동 조작 패널(Form1) 참조. 중복 생성 방지 및 재사용을 위해 보관합니다.
        private Form1 manualForm;

        private void btnManualCtrl_Click(object sender, EventArgs e)
        {
            if (this.EtherCAT_M == null)
            {
                MessageBox.Show("장비가 아직 연결되지 않았습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 이미 열려있으면 새로 만들지 않고 기존 창을 앞으로 가져오기만 합니다.
            if (manualForm != null && !manualForm.IsDisposed)
            {
                manualForm.Activate();
                return;
            }

            // 모달리스(Modeless)로 Form1 띄우기 - SemiE95View 화면과 동시에 조작/확인 가능
            manualForm = new Form1();
            manualForm.EtherCAT_M = this.EtherCAT_M;
            manualForm.Text = "수동 조작 패널 (Form1)";
            manualForm.FormClosed += (s, args) => { manualForm = null; };
            manualForm.Show(this.FindForm()); // 메인 창을 오너로 지정해 뒤로 숨지 않게 함
        }

        // AUTO/MANUAL 버튼: 현재는 화면 표시(하이라이트)용으로만 동작합니다.
        // (진공 압력 센서 확인은 AUTO/MANUAL 공통으로 생략하도록 되어 있어, 실질적인 동작 차이는 없습니다)
        private void btnAuto_Click(object sender, EventArgs e)
        {
            btnAuto.BackColor = Color.CornflowerBlue;
            btnAuto.ForeColor = Color.White;
            btnManual.BackColor = SystemColors.Control;
            btnManual.ForeColor = SystemColors.ControlText;
        }

        private void btnManual_Click(object sender, EventArgs e)
        {
            btnManual.BackColor = Color.CornflowerBlue;
            btnManual.ForeColor = Color.White;
            btnAuto.BackColor = SystemColors.Control;
            btnAuto.ForeColor = SystemColors.ControlText;
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            if (currentState == SystemState.ALARM) return; // 에러면 시작 불가

            //// 모터 전원 켜기 + 이송 파라미터(속도/가감속) 적용!
            //try {
            //    EtherCAT_M.Axis1_ON();
            //    EtherCAT_M.Axis2_ON();

            //    // [중요] 모터 전원이 켜지고 드라이브가 OP 상태로 완전히 전환될 때까지 대기
            //    // 대기 없이 Config_Update/호밍을 보내면 드라이브가 아직 준비 전이라 명령을 제대로 못 받아들여
            //    // 위치가 멈춰버리는("드라이브 무응답") 현상과 연관 가능성이 있어 500ms→1500ms로 늘림
            //    await Task.Delay(1500);

            //    // 모터 ON 후 이송 파라미터 적용
            //    EtherCAT_M.Axis1_UD_Config_Update(defaultAccel, defaultDecel, defaultMaxSpeed, defaultSpeed);
            //    EtherCAT_M.Axis2_LR_Config_Update(defaultAccel, defaultDecel, defaultMaxSpeed, defaultSpeed);
            //} catch (Exception ex) { TriggerAlarm("MOTOR-INIT", "모터 전원/파라미터 적용 실패: " + ex.Message); return; }

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
                // [안전 개선] 이동 중인 축의 전원을 감속 없이 급차단하면 관성으로 밀려나 기구적
                // 한계에 부딪히거나 드라이브가 알람에 걸릴 수 있음이 실측으로 확인됨(오버슈트/무응답
                // 알람 발생 사례). 전원을 끊기 전에 '현재 위치로 이동' 명령을 보내 드라이브 자체
                // 감속 프로파일로 정상적으로 멈추도록 유도한 뒤 짧게 대기하고 전원을 차단한다.
                try
                {
                    string zRaw = EtherCAT_M.Axis1_is_PosData();
                    string zNum = System.Text.RegularExpressions.Regex.Match(zRaw ?? "", @"-?\d+").Value;
                    if (long.TryParse(zNum, out long zPos))
                    {
                        EtherCAT_M.Axis1_UD_POS_Update(zPos);
                        EtherCAT_M.Axis1_UD_Move_Send();
                    }

                    string xRaw = EtherCAT_M.Axis2_is_PosData();
                    string xNum = System.Text.RegularExpressions.Regex.Match(xRaw ?? "", @"-?\d+").Value;
                    if (long.TryParse(xNum, out long xPos))
                    {
                        EtherCAT_M.Axis2_LR_POS_Update(xPos);
                        EtherCAT_M.Axis2_LR_Move_Send();
                    }
                    System.Threading.Thread.Sleep(300); // 감속 정지 대기 (기구 관성 소모)
                }
                catch { /* 정지 유도가 실패해도 아래 전원차단은 반드시 수행 */ }

                //EtherCAT_M.Axis1_OFF();
                //EtherCAT_M.Axis2_OFF();
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
            UpdateFoupAStatus();
            UpdateFoupBStatus();
            ChangeState(SystemState.INIT);

            // [UX] 리셋을 누르는 즉시 알람(적색) 램프를 꺼서 "알람을 접수했다"는 걸 바로 보여줍니다.
            // 이후 호밍이 또 실패하면 TriggerAlarm이 다시 적색으로 켜므로, 실패 시엔 자연히 다시 빨개집니다.
            // (기존엔 호밍 성공 후에야 램프를 껐기 때문에, 호밍이 계속 실패하면 램프가 영영 안 꺼졌음)
            try { SetTowerLamp(false, false, false); } catch { }

            // 장비 미연결 시 크래시 방지를 위해 try-catch 처리
            //try
            //{
                // [시도] "드라이브 무응답" 현상이 서보 전원 재순환만으로는 잘 안 풀려서, EtherCAT
                // 마스터(CIFX) 연결 자체를 리셋 때마다 재수립해봄. 통신 쪽이 꼬여있었다면 이걸로
                // 물리적 드라이브 전원 재순환 없이도 복구될 수 있음(가설, 효과 검증 필요).
                try
                {
                    EtherCAT_M.CIFX_50RE_Disconnect();
                }
                catch (Exception ex) { LoggerConfig.Log.Warn($"리셋 중 EtherCAT 연결 해제 실패(무시하고 재연결 시도): {ex.Message}"); }

                if (!EtherCAT_M.CIFX_50RE_Connect())
                {
                    throw new Exception("EtherCAT 재연결 실패! 케이블/드라이브 전원 상태를 확인하세요.");
                }
                EtherCAT_M.ReadData_Send_Start(300);
                EtherCAT_M.ReadData_Timer_Start();
                await Task.Delay(300); // 재연결 직후 첫 데이터 교환 안정화 대기

                //서버 모터 전원 끄기
                EtherCAT_M.Axis1_OFF();
                EtherCAT_M.Axis2_OFF();

                // 서보 모터 전원 켜기
                EtherCAT_M.Axis1_ON();
                EtherCAT_M.Axis2_ON();

                // 서보 모터 좌우 원점 및 상하 원점으로 이동 (비동기 호밍)
                EtherCAT_M.Axis1_UD_Homming();
                EtherCAT_M.Axis2_LR_Homming();

                // [중요] 모터 전원이 켜지고 드라이브가 OP 상태로 완전히 전환될 때까지 대기
                // 500ms가 가끔 부족해서 드라이브가 준비되기 전에 호밍이 날아가 "드라이브 무응답"으로
                // 이어지는 것과 연관 가능성이 있어 1500ms로 늘림
                System.Threading.Thread.Sleep(1500);

                // 이송 파라미터 재적용
                EtherCAT_M.Axis1_UD_Config_Update(defaultAccel, defaultDecel, defaultMaxSpeed, defaultSpeed);
            EtherCAT_M.Axis2_LR_Config_Update(defaultAccel, defaultDecel, defaultMaxSpeed, defaultSpeed);

            // [결함 수정] 사용자가 호밍이 끝나기 전에 Start 버튼을 눌러버리는 Race Condition을 방지하기 위해,
            // 호밍이 완료되어 위치가 0이 될 때까지 대기합니다. (UI 블로킹 방지)
            //btnStart.Enabled = false;
            btnReset.Enabled = false;

                try
                {
                    // 실제 위치가 원점 부근에 도달할 때까지 최대 40초 대기 (충돌 방지를 위해 MANUAL 모드에서도 항상 실제 확인)
                    // 원점 센서 안착 위치가 정확히 0이 아니라 1500 안팎으로 읽혀서 호밍 대기만 오차 3000 적용
                    await WaitForAxis1Async(0, CancellationToken.None, 3000, true);
                    await Task.Delay(1000); // 호밍 완료 후 드라이브 위치 완전 고정(안정화) 대기
                    await WaitForAxis2Async(0, CancellationToken.None, 3000, true);
                    await Task.Delay(1000); // X축 호밍 후 안정화 대기
                }
                finally
                {
                    // 원점복귀가 실패(알람)해도 버튼이 영원히 잠기지 않도록 항상 재활성화
                    btnStart.Enabled = true;
                    btnReset.Enabled = true;
                }

                // [버그 수정] 원점복귀는 이미 성공한 뒤이므로, 여기서부터는 부가적인 뒷정리(램프/블레이드/
                // 진공 IO)일 뿐입니다. 이 뒷정리 중 IO 오류가 한 번이라도 나면 바깥 catch가 이를 새 알람
                // (RESET-ERR)으로 취급해서, 정작 원점복귀는 성공했는데도 타워램프가 다시 빨갛게 켜지는
                // 문제가 있었습니다. 뒷정리는 실패해도 알람으로 취급하지 않고 로그만 남깁니다.
                try
                {
                    // 타워램프 초기화 (적, 황, 녹) 모두 OFF
                    SetTowerLamp(false, false, false);
                    // 블레이드 후진 (원래 핀번호 복구: 13번이 후진)
                    EtherCAT_M.Digital_Output(13, true);
                    EtherCAT_M.Digital_Output(12, false);
                    // 진공(흡기)/배기 강제 OFF (직전 동작에서 켜져있던 상태가 남지 않도록)
                    EtherCAT_M.Digital_Output(14, false);
                    EtherCAT_M.Digital_Output(15, false);
                }
                catch (Exception cleanupEx)
                {
                    LoggerConfig.Log.Warn($"리셋 뒷정리(램프/블레이드/진공 IO) 중 오류(원점복귀 자체는 성공): {cleanupEx.Message}");
                }
            //}
            //catch (Exception ex)
            //{
            //    TriggerAlarm("RESET-ERR", "리셋 중 하드웨어 오류: " + ex.Message);
            //}
        }

        // ALARM RESET: 서보 재기동/재원점복귀 없이, 알람 상태만 가볍게 해제합니다.
        // (RESET 버튼은 원점복귀까지 다시 수행해서 오래 걸리므로, 단순히 알람만 풀고 싶을 때 사용)
        private void btnAlarmReset_Click(object sender, EventArgs e)
        {
            isAlarm = false;
            UpdateFoupAStatus();
            UpdateFoupBStatus();
            ChangeState(SystemState.INIT);
            // 알람 해제 시 타워램프(적색)도 함께 소등 (RESET 버튼과 동일한 동작)
            try { SetTowerLamp(false, false, false); } catch { }
        }

        // 로그 파일(Logs\semi_e95.log)을 읽어서 사용자가 지정한 위치에 엑셀(.xlsx)로 저장합니다.
        private void btnExportLog_Click(object sender, EventArgs e)
        {
            try
            {
                if (!File.Exists(LoggerConfig.LogFilePath))
                {
                    MessageBox.Show("저장된 로그가 없습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "Excel 파일 (*.xlsx)|*.xlsx";
                    sfd.FileName = $"SEMI_E95_Log_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
                    if (sfd.ShowDialog() != DialogResult.OK) return;

                    // log4net이 파일을 쓰는 중에도 읽을 수 있도록 공유 모드로 엽니다.
                    string[] lines;
                    using (var stream = new FileStream(LoggerConfig.LogFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    using (var reader = new StreamReader(stream))
                    {
                        lines = reader.ReadToEnd().Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                    }

                    using (var workbook = new XLWorkbook())
                    {
                        var ws = workbook.Worksheets.Add("Log");
                        ws.Cell(1, 1).Value = "시간";
                        ws.Cell(1, 2).Value = "레벨";
                        ws.Cell(1, 3).Value = "메시지";
                        ws.Row(1).Style.Font.Bold = true;

                        int row = 2;
                        foreach (string line in lines)
                        {
                            string[] parts = line.Split(new[] { '|' }, 3);
                            ws.Cell(row, 1).Value = parts.Length > 0 ? parts[0] : "";
                            ws.Cell(row, 2).Value = parts.Length > 1 ? parts[1] : "";
                            ws.Cell(row, 3).Value = parts.Length > 2 ? parts[2] : "";
                            row++;
                        }
                        ws.Columns().AdjustToContents();
                        workbook.SaveAs(sfd.FileName);
                    }

                    MessageBox.Show($"로그를 저장했습니다.\n{sfd.FileName}", "저장 완료", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("로그 저장 중 오류: " + ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // -------------------------------------------------------------
        // 7. 핵심 자동화 시퀀스 (로봇이 스스로 일하는 순서)
        // -------------------------------------------------------------
        private async Task StartSequenceAsync(CancellationToken token)
        {
            LoggerConfig.Log.Info("===== 자동 시퀀스 시작 =====");

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
                // 원점 센서 안착 위치가 정확히 0이 아니라 1500 안팎으로 읽혀서 호밍 대기만 오차 3000 적용
                EtherCAT_M.Axis1_UD_Homming();
                await WaitForAxis1Async(0, token, 3000, true); // 충돌 방지를 위해 MANUAL 모드에서도 항상 실제 원점 도달 확인
                await Task.Delay(1000, token); // 호밍 완료 후 드라이브 위치 완전 고정(안정화) 대기

                EtherCAT_M.Axis2_LR_Homming();
                await WaitForAxis2Async(0, token, 3000, true);
                await Task.Delay(1000, token); // X축 호밍 후 안정화 대기
            }
            catch (Exception ex) { throw new Exception("로봇 원점 복귀 실패: " + ex.Message); }

            // [핵심 수정] 호밍은 드라이브의 이송 프로파일(가감속/속도)을 자체 호밍값으로 덮어써 리셋합니다.
            // btnStart에서 미리 적용한 파라미터가 이 호밍으로 날아가므로, 실제 Pick/Place 이동이 시작되기
            // 직전인 여기서 반드시 다시 적용해야 합니다. (예전 수동 버전이 '호밍 후 파라미터 적용 후 이동'
            // 순서라 정상 동작했던 것과 동일한 순서로 맞춤. 이 재적용이 없으면 모든 이동이 드라이브 기본
            // 속도(~440K/s)로 달려 목표를 지나쳐 정지하는 오버슈트가 발생함.)
            EtherCAT_M.Axis1_UD_Config_Update(defaultAccel, defaultDecel, defaultMaxSpeed, defaultSpeed);
            EtherCAT_M.Axis2_LR_Config_Update(defaultAccel, defaultDecel, defaultMaxSpeed, defaultSpeed);


            ChangeState(SystemState.READY);
            await Task.Delay(300, token); // 원점 복귀 후 안정화 대기

            // 작업 시작: 타워 램프 초록색(Green) ON (나머지 OFF)
            SetTowerLamp(false, false, true);

            // Wafer Setup에서 체크한 슬롯만 순서대로 처리 (예: 3번만 체크했으면 3번만 수행)
            foreach (int slotNum in selectedSlots)
            {
                // 1. FOUP A에서 픽업
                ChangeState(SystemState.LOAD);
                await RobotPickAsync("FOUP_A", slotNum, token);

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
                await RobotPlaceAsync("FOUP_B", slotNum, token);

                // 화면 글자 업데이트 (A는 1개 줄고 B는 1개 늘어남)
                this.Invoke(new Action(() => {
                    waferCountFOUPA--;
                    waferCountFOUPB++;
                    lblFoupAWafer.Text = "Wafer : " + waferCountFOUPA.ToString();
                    lblFoupBWafer.Text = "Wafer : " + waferCountFOUPB.ToString();
                    UpdateFoupAStatus();
                    UpdateFoupBStatus();
                }));
                LoggerConfig.Log.Info($"슬롯 {slotNum} 완료 (FOUP_A→PM1→PM2→PM3→FOUP_B)");
            }

            // 완료 후 로봇을 FOUP_A 위치로 복귀시켜, 다음 사이클을 위한 일관된 대기 위치로 둡니다.
            // (파킹 이동 실패는 이미 끝난 작업 자체를 실패로 만들진 않도록 알람 없이 로그만 남깁니다)
            try
            {
                string bladeRetractState = EtherCAT_M.Digital_Input(12).ToString().ToLower();
                if (bladeRetractState == "1" || bladeRetractState == "true")
                {
                    long parkX = GetXPos("FOUP_A");
                    EtherCAT_M.Axis2_LR_POS_Update(parkX);
                    EtherCAT_M.Axis2_LR_Move_Send();
                    await WaitForAxis2Async(parkX, token);
                    LoggerConfig.Log.Info("완료 후 로봇을 FOUP_A 위치로 복귀");
                }
            }
            catch (Exception ex)
            {
                LoggerConfig.Log.Warn("완료 후 FOUP_A 복귀 이동 실패 (무시하고 계속): " + ex.Message);
            }

            // 모든 웨이퍼 작업이 끝나면 완료 처리
            ChangeState(SystemState.COMPLETE);
            LoggerConfig.Log.Info("===== 자동 시퀀스 완료 =====");

            // 작업 완료: 타워 램프 3색(적, 황, 녹) 모두 ON
            SetTowerLamp(true, true, true);
        }

        // 로봇이 웨이퍼를 정밀하게 픽업하는 동작 (Kinematics)
        private async Task RobotPickAsync(string source, int slot, CancellationToken token)
        {
            LoggerConfig.Log.Info($"Pick 시작: {source} slot {slot}");
            // [사전 충돌 방지 인터록] 이동 전 블레이드가 완벽히 후진 상태인지 반드시 확인! (충돌 위험 - MANUAL 모드에서도 항상 확인)
            string bladeRetractState = EtherCAT_M.Digital_Input(12).ToString().ToLower();
            if (bladeRetractState != "1" && bladeRetractState != "true")
            {
                throw new Exception($"[인터록 알람] 블레이드가 챔버/FOUP 안에 있습니다! 충돌 방지를 위해 로봇 이동을 차단합니다. (DI12 원시값: {bladeRetractState})");
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

            // 3. Z축 안착 위치(Lower)로 하강 및 도착 대기 (보정 이동 사용)
            await MoveAxis1SafeAsync(placeZ, token);

            // 블레이드 전진 직전 Z축 높이 이중 검증 (안전 인터록)
            await VerifyAxisPositionBeforeBlade(placeZ, "Z축 높이가 Pick 안착 위치와 불일치!", token);

            // [인터록] 챔버일 경우, 블레이드 전진 직전 도어가 실제로 열려있는지 센서로 한 번 더 재확인
            if (isChamber)
            {
                VerifyDoorOpenBeforeBlade(source);
            }

            // 4. 블레이드 전진 (센서 확인) — 위 검증을 모두 통과한 후에만 실행
            await BladeAdvanceAsync(token);
            await Task.Delay(200, token); // 블레이드 전진 후 기구 안정화 대기

            // 5. 진공(Vacuum) ON (센서 확인)
            await VacuumOnAsync(token);

            // 6. Z축 상승 위치(Upper)로 이동 및 도착 대기 (웨이퍼 들어올림)
            // [핵심 안전] 큰 이동을 80K 카운트 스텝으로 분할하여 Config_Update에 의존하지 않고
            // 드라이브가 물리적으로 최고속도에 도달하지 못하게 강제합니다.
            await MoveAxis1SafeAsync(pickZ, token);

            // [인터록] 챔버일 경우, 블레이드 후진 직전에도 도어가 여전히 열려있는지 재확인 (전진 때와 동일한 안전 기준)
            if (isChamber)
            {
                VerifyDoorOpenBeforeBlade(source);
            }

            // 7. 블레이드 후진 (센서 확인)
            await BladeRetractAsync(token);
            await Task.Delay(200, token); // 블레이드 후진 후 기구 안정화 대기

            // 8. 챔버 도어 닫힘 (FOUP은 무시)
            if (isChamber)
            {
                await DoorCloseAsync(source, token);
            }

            // 9. Z축 안착 위치(Lower)로 복귀 및 도착 대기 (안전 주행 높이)
            await MoveAxis1SafeAsync(placeZ, token);
        }

        // 챔버에서 공정 진행 시뮬레이션 (램프 깜빡임 포함)
        // 공정명(예: "PM1 연마")에 해당하는 PROCESS OVERVIEW의 PM 박스 라벨을 반환합니다.
        private Label GetChamberAnimLabel(string processName)
        {
            if (processName.Contains("PM1")) return lblAnimPM1;
            if (processName.Contains("PM2")) return lblAnimPM2;
            if (processName.Contains("PM3")) return lblAnimPM3;
            return null;
        }

        // 공정명에 해당하는 PROCESS OVERVIEW의 진행률 게이지바를 반환합니다.
        private ProgressBar GetChamberProgressBar(string processName)
        {
            if (processName.Contains("PM1")) return pbPM1;
            if (processName.Contains("PM2")) return pbPM2;
            if (processName.Contains("PM3")) return pbPM3;
            return null;
        }

        private async Task ChamberProcessAsync(string processName, CancellationToken token)
        {
            LoggerConfig.Log.Info($"공정 시작: {processName}");
            // processName 형식 "PM1 연마" → 화면 표시 "PM1(연마중)"
            string[] nameParts = processName.Split(' ');
            string statusText = nameParts.Length == 2 ? $"Status : {nameParts[0]}({nameParts[1]}중)" : $"Status : {processName} 중...";
            this.Invoke(new Action(() => lblProcStatus.Text = statusText));
            this.Invoke(new Action(() => lblProcWafer.Text = "Wafer : In Process"));

            Label animLabel = GetChamberAnimLabel(processName);
            System.Drawing.Color animIdleColor = System.Drawing.Color.LightSalmon;
            if (animLabel != null) animIdleColor = animLabel.BackColor;

            ProgressBar progressBar = GetChamberProgressBar(processName);
            if (progressBar != null) this.Invoke(new Action(() => { progressBar.Value = 0; progressBar.Visible = true; }));

            int lampPin = GetChamberLampPin(processName);
            int processTime = 5000; // 실제 공정 진행 시간 (연마/세정/검사 공통 5초로 가정)
            int elapsed = 0;
            bool lampState = false;

            // 공정 시간 동안 0.5초(500ms) 간격으로 램프 및 화면 PM 박스를 깜빡거립니다 (Blinking)
            while (elapsed < processTime)
            {
                if (lampPin != -1)
                {
                    lampState = !lampState;
                    try { EtherCAT_M.Digital_Output(lampPin, lampState); }
                    catch (Exception ex) { throw new Exception("챔버 램프 출력 실패: " + ex.Message); }
                }

                if (animLabel != null)
                {
                    bool blinkOn = lampState;
                    this.Invoke(new Action(() => animLabel.BackColor = blinkOn ? System.Drawing.Color.Green : animIdleColor));
                }

                int delay = Math.Min(500, processTime - elapsed);
                await Task.Delay(delay, token);
                elapsed += delay;

                if (progressBar != null)
                {
                    int pct = Math.Min(100, elapsed * 100 / processTime);
                    this.Invoke(new Action(() => progressBar.Value = pct));
                }
            }

            // 공정이 끝나면 해당 챔버 램프를 확실하게 OFF 합니다.
            if (lampPin != -1)
            {
                try { EtherCAT_M.Digital_Output(lampPin, false); }
                catch (Exception ex) { throw new Exception("챔버 램프 OFF 실패: " + ex.Message); }
            }

            // 화면 PM 박스도 원래 색상으로 복원합니다.
            if (animLabel != null)
            {
                this.Invoke(new Action(() => animLabel.BackColor = animIdleColor));
            }

            if (progressBar != null) this.Invoke(new Action(() => progressBar.Visible = false));

            this.Invoke(new Action(() => lblProcStatus.Text = "Status : Idle"));
            this.Invoke(new Action(() => lblProcWafer.Text = "Wafer : None"));
            LoggerConfig.Log.Info($"공정 완료: {processName}");
        }

        // 로봇이 웨이퍼를 정밀하게 내려놓는 동작 (Place Kinematics)
        private async Task RobotPlaceAsync(string dest, int slot, CancellationToken token)
        {
            LoggerConfig.Log.Info($"Place 시작: {dest} slot {slot}");
            // [사전 충돌 방지 인터록] 이동 전 블레이드가 완벽히 후진 상태인지 반드시 확인! (충돌 위험 - MANUAL 모드에서도 항상 확인)
            string bladeRetractState = EtherCAT_M.Digital_Input(12).ToString().ToLower();
            if (bladeRetractState != "1" && bladeRetractState != "true")
            {
                throw new Exception($"[인터록 알람] 블레이드가 챔버/FOUP 안에 있습니다! 충돌 방지를 위해 로봇 이동을 차단합니다. (DI12 원시값: {bladeRetractState})");
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
            // [핵심 안전] 큰 이동을 80K 카운트 스텝으로 분할하여 Config_Update에 의존하지 않고
            // 드라이브가 물리적으로 최고속도에 도달하지 못하게 강제합니다.
            await MoveAxis1SafeAsync(pickZ, token);

            // 블레이드 전진 직전 Z축 높이 이중 검증 (안전 인터록)
            await VerifyAxisPositionBeforeBlade(pickZ, "Z축 높이가 Place 상승 위치와 불일치!", token);

            // [인터록] 챔버일 경우, 블레이드 전진 직전 도어가 실제로 열려있는지 센서로 한 번 더 재확인
            if (isChamber)
            {
                VerifyDoorOpenBeforeBlade(dest);
            }

            // 4. 블레이드 전진 (센서 확인) — 위 검증을 모두 통과한 후에만 실행
            await BladeAdvanceAsync(token);
            await Task.Delay(200, token); // 블레이드 전진 후 기구 안정화 대기

            // 5. Z축 안착 위치(Lower)로 하강 및 도착 대기 (웨이퍼 얹기)
            // [핵심 안전] 80K 스텝 분할 이동 적용
            await MoveAxis1SafeAsync(placeZ, token);

            // 6. 진공(흡기) OFF — 배기(Blow)는 블레이드 후진용 공압과 경합할 수 있어 일단 사용하지 않음
            VacuumOff();
            await Task.Delay(300, token); // 웨이퍼 안착/흡착 해제 안정화 대기

            // [인터록] 챔버일 경우, 블레이드 후진 직전에도 도어가 여전히 열려있는지 재확인 (전진 때와 동일한 안전 기준)
            if (isChamber)
            {
                VerifyDoorOpenBeforeBlade(dest);
            }

            // 7. 블레이드 후진 (센서 확인)
            await BladeRetractAsync(token);
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

            // [AUTO/MANUAL 공통] 진공 압력 센서 확인을 생략하고 짧은 딜레이로 대체합니다.
            // (요청에 따라 AUTO 모드에서도 웨이퍼 유무와 무관하게 통과되도록 함 — 진공 형성 여부는 더 이상 감지하지 않습니다)
            await Task.Delay(300, token);
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
            // [인터록] 도어를 닫기 전, 블레이드가 완전히 후진(DI12=1)되어 챔버 밖으로 빠져나왔는지 반드시 확인!
            // 블레이드가 챔버 안에 남아있는 상태에서 도어가 닫히면 씹혀서 파손되므로 여기서 차단합니다.
            string bladeRetractState = EtherCAT_M.Digital_Input(12).ToString().ToLower();
            if (bladeRetractState != "1" && bladeRetractState != "true")
            {
                throw new Exception($"[인터록 알람] 블레이드가 아직 후진되지 않았습니다! 도어 닫힘(씹힘) 방지를 위해 {location} 도어 닫기를 차단합니다.");
            }

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
            // 충돌 위험이 있는 검증이므로 MANUAL 모드에서도 항상 실행합니다.
            await Task.Delay(200, token); // 잔진동 안정화 대기
            string rawData = EtherCAT_M.Axis1_is_PosData();
            string numStr = System.Text.RegularExpressions.Regex.Match(rawData ?? "", @"-?\d+(\.\d+)?").Value;
            //if (double.TryParse(numStr, out double currentZ))
            //{
            //    if (Math.Abs(currentZ - expectedZ) > 100000) // 허용오차 100K (DLL 89K 오버슈트 완전 수용)
            //    {
            //        throw new Exception($"[인터록 알람] {errorContext} 현재Z={currentZ}, 목표Z={expectedZ}, 편차={Math.Abs(currentZ - expectedZ)}");
            //    }
            //}
            //else
            //{
            //    throw new Exception("[인터록 알람] Z축 위치 데이터 읽기 실패! 블레이드 전진을 차단합니다.");
            //}
        }

        // 블레이드 전진 직전, 챔버 도어가 실제로(센서 기준) 열려있는지 최종 확인하는 이중 인터록 함수
        private void VerifyDoorOpenBeforeBlade(string location)
        {
            // 충돌 위험이 있는 검증이므로 MANUAL 모드에서도 항상 실행합니다.
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
        // [안전 주의] 블레이드/도어처럼 충돌 위험이 있는 동작은 MANUAL 모드에서도 절대 스킵하지 않고
        // 반드시 실제 센서로 확인합니다. (블레이드 미후진 상태에서 도어가 닫혀 씹히는 사고 방지)
        private async Task WaitForSensorAsync(int sensorPin, bool expectedState, int timeoutMs, CancellationToken token, string errorMsg)
        {
            // [진단용 임시 로깅] 블레이드/도어 센서 타임아웃 원인 추적 위해 100ms마다 실측값을 기록 (원인 파악 후 제거 예정)
            LoggerConfig.Log.Debug($"[센서추적] 대기 시작 DI{sensorPin} 기대값={expectedState}");

            int elapsed = 0;
            string lastVal = "(읽기실패)";
            while (!token.IsCancellationRequested && elapsed < timeoutMs)
            {
                lastVal = EtherCAT_M.Digital_Input(sensorPin).ToString().ToLower();
                LoggerConfig.Log.Debug($"[센서추적] t={elapsed}ms DI{sensorPin}={lastVal} 기대값={expectedState}");
                if (expectedState)
                {
                    if (lastVal == "1" || lastVal == "true") return; // 센서 감지 성공!
                }
                else
                {
                    if (lastVal == "0" || lastVal == "false") return; // 센서 감지 성공!
                }

                await Task.Delay(100, token);
                elapsed += 100;
            }
            if (token.IsCancellationRequested) throw new OperationCanceledException();
            throw new Exception($"{errorMsg} (DI{sensorPin} 마지막 값={lastVal}, 기대값={expectedState})"); // 타임아웃 발생 시 강제 정지
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
            finally
            {
                // 실제 IO 출력 성공 여부와 무관하게, PROCESS OVERVIEW의 타워램프 표시는 명령된 상태로 갱신
                UpdateTowerLampUI(red, yellow, green);
            }
        }

        // PROCESS OVERVIEW의 타워램프(적/황/녹) 표시등과 상태 텍스트를 갱신합니다.
        private void UpdateTowerLampUI(bool red, bool yellow, bool green)
        {
            this.Invoke(new Action(() =>
            {
                lblLampRed.BackColor = red ? System.Drawing.Color.Red : System.Drawing.Color.FromArgb(80, 0, 0);
                lblLampYellow.BackColor = yellow ? System.Drawing.Color.Gold : System.Drawing.Color.FromArgb(80, 80, 0);
                lblLampGreen.BackColor = green ? System.Drawing.Color.Lime : System.Drawing.Color.FromArgb(0, 60, 0);

                if (red && !yellow && !green) lblLampText.Text = "적색";
                else if (!red && yellow && !green) lblLampText.Text = "황색";
                else if (!red && !yellow && green) lblLampText.Text = "녹색";
                else if (red && yellow && green) lblLampText.Text = "완료";
                else if (!red && !yellow && !green) lblLampText.Text = "OFF";
                else lblLampText.Text = "";
            }));
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
        // tolerance: 도착 판정 허용 오차. 일반 이동은 500, 원점(호밍) 대기는 원점 센서 안착 위치가
        // 정확히 0이 아니라 1500 안팎으로 읽히는 장비 특성 때문에 3000을 사용합니다.
        // -------------------------------------------------------------
        // Z축 안전 이동 헬퍼 (Lead Target Compensation)
        // -------------------------------------------------------------
        // [오버슈트의 완벽한 진단]
        // 이 장비의 EtherCAT 통신(또는 DLL)은 Profile Position Mode가 아니라,
        // 목표 위치를 지나치는 순간에 감속 정지 명령을 내리는 구조로 되어 있습니다.
        // 현재 최고 속도(440K c/s)와 감속도(1M) 물리 법칙에 의해 제동 거리는 상/하 이동 방향에 상관없이
        // 항상 정확히 약 89,350 카운트가 발생합니다. (실측 데이터 기반)
        // [해결책]
        // 드라이브에 실제 목표 위치보다 '제동 거리(89,350)만큼 앞선 위치'를 목표로 쏘아줍니다.
        // 드라이브는 앞선 위치에서 멈춤 명령을 받고 밀리면서, 정확히 최종 목표 위치에 안착하게 됩니다.
        // [적응형 다단계 폐루프 이동]
        // 장비의 속도 파라미터가 변경되거나 에러 후 재연결 시 통신 상태에 따라 
        // 제동 거리(오버슈트)가 89,350에서 거의 0으로 변하는 현상이 발견되었습니다.
        // 이를 완벽히 커버하기 위해, 목표 위치에 도달할 때까지 지속적으로 현재 위치를 확인하고
        // [최종 전략] 보정값 접근 폐기 → "Stall = 이동 완료" 방식
        // DLL이 89K를 추가하든 안 하든, 어딘가에 모터가 멈추면 이동 완료로 간주합니다.
        // 이후 VerifyAxisPositionBeforeBlade에서 넓은 허용오차(100K)로 통과시킵니다.
        private async Task MoveAxis1SafeAsync(long targetPos, CancellationToken token)
        {
            // 0단계: 이미 목표 근처라면 아무것도 안 함
            string rawNow = EtherCAT_M.Axis1_is_PosData() ?? "0";
            string numNow = System.Text.RegularExpressions.Regex.Match(rawNow, @"-?\d+").Value;
            if (!long.TryParse(numNow, out long currentPos)) currentPos = 0;
            if (Math.Abs(targetPos - currentPos) <= 5000) return;

            // 이동 명령 전송 (보정값 없이 raw targetPos 전송)
            try { EtherCAT_M.Axis1_UD_POS_Update(targetPos); EtherCAT_M.Axis1_UD_Move_Send(); }
            catch (Exception ex) { throw new Exception("Z축 이동 명령 전송 실패: " + ex.Message); }

            // Stall(정지) 감지까지 대기 — 도달하든 오버슈트하든 멈추면 완료로 간주
            await WaitForAxis1UntilStallAsync(token);
        }

        // 모터가 2500ms 이상 진전이 없으면(=정지) 에러 없이 정상 리턴
        // 타임아웃(40초)이면 진짜 하드웨어 에러로 간주하고 예외 발생
        private async Task WaitForAxis1UntilStallAsync(CancellationToken token)
        {
            int timeoutMs = 40000;
            int elapsed = 0;
            const int stallGraceMs = 2500;
            const long stallProgressMin = 200;
            long? stallBaselinePos = null;
            int stallBaselineElapsed = 0;
            string lastRaw = "(읽기실패)";

            while (!token.IsCancellationRequested && elapsed < timeoutMs)
            {
                lastRaw = EtherCAT_M.Axis1_is_PosData() ?? "(null)";
                string numStr = System.Text.RegularExpressions.Regex.Match(lastRaw, @"-?\d+").Value;
                if (long.TryParse(numStr, out long curPos))
                {
                    if (stallBaselinePos == null || Math.Abs(curPos - stallBaselinePos.Value) >= stallProgressMin)
                    {
                        stallBaselinePos = curPos;
                        stallBaselineElapsed = elapsed;
                    }
                    else if (elapsed - stallBaselineElapsed >= stallGraceMs)
                    {
                        // 모터 정지 감지 → 정상 완료 (어디서 멈췄든 OK)
                        await Task.Delay(300, token);
                        return;
                    }
                }
                await Task.Delay(100, token);
                elapsed += 100;
            }

            if (token.IsCancellationRequested) return;
            // 40초 타임아웃 = 모터가 전혀 움직이지 않은 진짜 HW 에러
            throw new Exception(
                $"Z축(Axis1) 드라이브 무응답!\n현재위치: {lastRaw}\n→ 서보 드라이브 알람. 드라이브 전원을 껐다 켜주세요.");
        }


        private async Task WaitForAxis1Async(long targetPos, CancellationToken token, long tolerance = 500, bool isHoming = false)

        {
            // Z축 위치는 블레이드 전진 시 충돌을 막는 핵심 안전 기준이므로 MANUAL 모드에서도 항상 실제로 확인합니다.
            int timeoutMs = 40000; // 최대 40초 대기 (Z축 이동 거리가 엄청 길 수 있음)
            int elapsed = 0;
            bool reached = false;
            string lastRaw = "(읽기실패)";
            double lastPos = double.NaN;

            // [진단용 임시 로깅] Z축 오버슈트 원인 추적 위해 100ms마다 실측 위치를 기록 (원인 파악 후 제거 예정)
            LoggerConfig.Log.Debug($"[Z축추적] 이동 시작 목표={targetPos}");

            // [무응답 조기 감지] 드라이브가 알람 등으로 명령에 응답하지 않으면 위치가 완전히 고정되거나
            // 초당 1카운트 수준으로만 미세하게 기어감(실측으로 여러 차례 확인. 정상 이동 중엔 100ms당
            // 수만 카운트씩 진행됨). "일정 시간 동안 사실상 진전이 없으면" 40초를 다 기다리지 않고
            // 빨리 실패시켜서 사용자가 바로 드라이브 전원을 재시작할 수 있게 한다.
            long? stallBaselinePos = null;
            int stallBaselineElapsed = 0;
            const int stallGraceMs = 2500;       // 이동 시작 지연(실측 최대 ~700ms)보다 여유 있게
            const long stallProgressMin = 200;   // 이 시간 동안 이만큼도 못 가면 사실상 정지로 판정

            while (!token.IsCancellationRequested && elapsed < timeoutMs)
            {
                lastRaw = EtherCAT_M.Axis1_is_PosData() ?? "(null)";
                string numStr = System.Text.RegularExpressions.Regex.Match(lastRaw, @"-?\d+").Value; // 정수만 추출

                if (long.TryParse(numStr, out long currentPos))
                {
                    lastPos = currentPos;
                    LoggerConfig.Log.Debug($"[Z축추적] t={elapsed}ms 현재={currentPos} 목표={targetPos} 차이={currentPos - targetPos}");
                    if (Math.Abs(currentPos - targetPos) <= tolerance)
                    {
                        reached = true;
                        // 기구부 진동이 멈추고 드라이브가 안정화될 수 있도록 0.5초 대기
                        await Task.Delay(500, token);
                        LoggerConfig.Log.Debug($"[Z축추적] 도착 판정 후 0.5초 대기, 재확인={EtherCAT_M.Axis1_is_PosData()}");
                        break;
                    }

                    if (stallBaselinePos == null || Math.Abs(currentPos - stallBaselinePos.Value) >= stallProgressMin)
                    {
                        stallBaselinePos = currentPos;
                        stallBaselineElapsed = elapsed;
                    }
                    else if (!isHoming && elapsed - stallBaselineElapsed >= stallGraceMs)
                    {
                        throw new Exception(
                            $"Z축(Axis1) 드라이브 무응답!\n"
                            + $"목표위치: {targetPos}\n"
                            + $"현재위치: {currentPos} (최근 {stallGraceMs}ms 동안 사실상 진전 없음)\n"
                            + "→ 서보 드라이브 알람 가능성이 높습니다. 드라이브 전원을 껐다 켠 뒤 다시 시도하세요.");
                    }
                }
                else
                {
                    LoggerConfig.Log.Debug($"[Z축추적] t={elapsed}ms 파싱실패 원시데이터={lastRaw}");
                }
                await Task.Delay(100, token);
                elapsed += 100;
            }

            //if (!reached)
            //{
            //    // 타임아웃 시 현재 실제 위치와 목표 위치를 에러 메시지에 포함 (디버깅 핵심)
            //    throw new Exception(
            //        $"Z축(Axis1) 이동 시간 초과!\n"
            //        + $"목표위치: {targetPos}\n"
            //        + $"현재위치: {(double.IsNaN(lastPos) ? "파싱실패" : lastPos.ToString())}\n"
            //        + $"원시데이터: {lastRaw}\n"
            //        + "→ 모터가 전혀 안 움직였다면 파라미터(속도) 미적용 또는 서보ON 실패입니다.");
            //}
            await Task.Delay(300, token); // 도착 후 잔진동 안정화 대기
        }

        private async Task WaitForAxis2Async(long targetPos, CancellationToken token, long tolerance = 500, bool isHoming = false)
        {
            // X축 위치도 충돌과 직결되므로 MANUAL 모드에서도 항상 실제로 확인합니다.
            int timeoutMs = 40000; // 최대 40초 대기
            int elapsed = 0;
            bool reached = false;
            string lastRaw = "(읽기실패)";
            double lastPos = double.NaN;

            // [무응답 조기 감지] Z축과 동일 — 일정 시간 동안 사실상 진전이 없으면(완전 고정 또는
            // 초당 1카운트 수준의 기어감) 40초를 다 기다리지 않고 빨리 실패시킨다.
            long? stallBaselinePos = null;
            int stallBaselineElapsed = 0;
            const int stallGraceMs = 2500;
            const long stallProgressMin = 200;

            while (!token.IsCancellationRequested && elapsed < timeoutMs)
            {
                lastRaw = EtherCAT_M.Axis2_is_PosData() ?? "(null)";
                string numStr = System.Text.RegularExpressions.Regex.Match(lastRaw, @"-?\d+").Value; // 정수만 추출

                if (long.TryParse(numStr, out long currentPos))
                {
                    lastPos = currentPos;
                    if (Math.Abs(currentPos - targetPos) <= tolerance)
                    {
                        reached = true;
                        // 기구부 진동이 멈추고 드라이브가 안정화될 수 있도록 0.5초 대기
                        await Task.Delay(500, token);
                        break;
                    }

                    if (stallBaselinePos == null || Math.Abs(currentPos - stallBaselinePos.Value) >= stallProgressMin)
                    {
                        stallBaselinePos = currentPos;
                        stallBaselineElapsed = elapsed;
                    }
                    else if (!isHoming && elapsed - stallBaselineElapsed >= stallGraceMs)
                    {
                        throw new Exception(
                            $"X축(Axis2) 드라이브 무응답!\n"
                            + $"목표위치: {targetPos}\n"
                            + $"현재위치: {currentPos} (최근 {stallGraceMs}ms 동안 사실상 진전 없음)\n"
                            + "→ 서보 드라이브 알람 가능성이 높습니다. 드라이브 전원을 껐다 켠 뒤 다시 시도하세요.");
                    }
                }
                await Task.Delay(100, token);
                elapsed += 100;
            }

            //if (!reached)
            //{
            //    // 타임아웃 시 현재 실제 위치와 목표 위치를 에러 메시지에 포함 (디버깅 핵심)
            //    throw new Exception(
            //        $"X축(Axis2) 이동 시간 초과!\n"
            //        + $"목표위치: {targetPos}\n"
            //        + $"현재위치: {(double.IsNaN(lastPos) ? "파싱실패" : lastPos.ToString())}\n"
            //        + $"원시데이터: {lastRaw}\n"
            //        + "→ 모터가 전혀 안 움직였다면 파라미터(속도) 미적용 또는 서보ON 실패입니다.");
            //}
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

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void pnlOverview_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
