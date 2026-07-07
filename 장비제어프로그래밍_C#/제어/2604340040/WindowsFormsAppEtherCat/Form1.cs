using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Schema;
using IEG3268_Dll;

namespace WindowsFormsAppEtherCat
{
    public partial class Form1 : Form
    {
        public IEG3268 EtherCAT_M;

        public Form1()
        {
            InitializeComponent();

            // 위치 초기 세팅 (값 입력 시 에러 방지)
            numericUpDown1.Maximum = decimal.MaxValue;
            numericUpDown1.Minimum = decimal.MinValue;

            // 가속도 초기값 세팅
            numericUpDown2.Maximum = decimal.MaxValue;
            numericUpDown2.Value = 1000000;

            // 감속도 초기값 세팅
            numericUpDown3.Maximum = decimal.MaxValue;
            numericUpDown3.Value = 1000000;

            // 최대속도 초기값 세팅
            numericUpDown4.Maximum = decimal.MaxValue;
            numericUpDown4.Value = 100000000;

            // 속도 초기값 세팅
            numericUpDown5.Maximum = decimal.MaxValue;
            numericUpDown5.Value = 1000000;

            // 로깅 시작 지점 파일에 기록
            try { System.IO.File.AppendAllText("ManualSequenceLog.txt", $"\n\n=========================================\n[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 새 세션 시작\n=========================================\n"); } catch {}
            
            // 폼 내의 모든 버튼에 로깅 이벤트 부착
            AddClickLogging(this);
        }

        private void AddClickLogging(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if (c is Button btn)
                {
                    btn.Click += (s, ev) => 
                    {
                        try {
                            string log = $"[{DateTime.Now:HH:mm:ss.fff}] 버튼 클릭됨: '{btn.Text}' (버튼이름: {btn.Name})\n";
                            log += $"    -> [현재 파라미터] 목표위치: {numericUpDown1.Value}, 가속도: {numericUpDown2.Value}, 감속도: {numericUpDown3.Value}, 최대속도: {numericUpDown4.Value}, 설정속도: {numericUpDown5.Value}\n\n";
                            System.IO.File.AppendAllText("ManualSequenceLog.txt", log);
                        } catch {}
                    };
                }
                else if (c.HasChildren)
                {
                    AddClickLogging(c);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //연결
            //라벨 10 false로 초기화
            label10.Text = "false";
            label11.Text = "true";


            if (EtherCAT_M.CIFX_50RE_Connect() == true)
            {
                label2.Text = "OK";
                EtherCAT_M.ReadData_Send_Start(300); //Timer Interval Set
                EtherCAT_M.ReadData_Timer_Start(); //Timer Start
                
                // UI 갱신 타이머 설정 및 시작
                timer1.Interval = 300;
                timer1.Start();
            }
            else
            {
                label2.Text = "NG!!!";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // UI 갱신 타이머 정지
            timer1.Stop();

            // 연결 해제
            EtherCAT_M.CIFX_50RE_Disconnect();
            label2.Text = "Disconnect";

        }

        private void button3_Click(object sender, EventArgs e)
        {
            // 타원램프 적색 깜빡깜빡 ON

            EtherCAT_M.Digital_Output(0, true);
            for (int i = 0;i < 5 ; i++)
            {
                System.Threading.Thread.Sleep(500);
                EtherCAT_M.Digital_Output(0, false);
                System.Threading.Thread.Sleep(500);
                EtherCAT_M.Digital_Output(0, true);
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            // 타원램프 적색 OFF
            EtherCAT_M.Digital_Output(0, false);

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            //타원램프 노란색 ON
            EtherCAT_M.Digital_Output(1, true);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //타원램프 노란색 OFF
            EtherCAT_M.Digital_Output(1, false);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //타원램프 녹색 ON
            EtherCAT_M.Digital_Output(2, true);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //타원램프 녹색 OFF
            EtherCAT_M.Digital_Output(2, false);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            // 챔버 A 램프ON
            EtherCAT_M.Digital_Output(3, true);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            // 챔버 A 램프OFF
            EtherCAT_M.Digital_Output(3, false);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            // 챔버 A 도어 하강
            EtherCAT_M.Digital_Output(5, true);
            EtherCAT_M.Digital_Output(4, false);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            // 챔버 A 도어 상승
            EtherCAT_M.Digital_Output(4, true);
            EtherCAT_M.Digital_Output(5, false);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            // 챔버 B 램프ON
            EtherCAT_M.Digital_Output(6, true);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            // 챔버 B 램프OFF
            EtherCAT_M.Digital_Output(6, false);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            // 챔버 B 도어 하강
            EtherCAT_M.Digital_Output(8, true);
            EtherCAT_M.Digital_Output(7, false);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            // 챔버 B 도어 상승
            EtherCAT_M.Digital_Output(7, true);
            EtherCAT_M.Digital_Output(8, false);
        }

        private void button20_Click(object sender, EventArgs e)
        {
            // 챔버 C 램프ON
            EtherCAT_M.Digital_Output(9, true);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            // 챔버 C 램프ON
            EtherCAT_M.Digital_Output(9, false);

        }

        private void button17_Click(object sender, EventArgs e)
        {
            // 챔버 C 도어 하강
            EtherCAT_M.Digital_Output(11, true);
            EtherCAT_M.Digital_Output(10, false);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            // 챔버 C 도어 하강
            EtherCAT_M.Digital_Output(10, true);
            EtherCAT_M.Digital_Output(11, false);
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void button21_Click(object sender, EventArgs e)
        {
            // 챔버 램프 전체  ALL ON
            EtherCAT_M.Digital_Output(3, true);
            EtherCAT_M.Digital_Output(6, true);
            EtherCAT_M.Digital_Output(9, true);



        }

        private void button22_Click(object sender, EventArgs e)
        {
            //챔버    램프 전체 ALL OFF
            EtherCAT_M.Digital_Output(3, false);
            EtherCAT_M.Digital_Output(6, false);
            EtherCAT_M.Digital_Output(9, false);

        }

        private void button23_Click(object sender, EventArgs e)
        {
            //챔버 도어 전체 상승
            EtherCAT_M.Digital_Output(4, true);
            EtherCAT_M.Digital_Output(5, false);
            EtherCAT_M.Digital_Output(7, true);
            EtherCAT_M.Digital_Output(8, false);
            EtherCAT_M.Digital_Output(10, true);
            EtherCAT_M.Digital_Output(11, false);
        }

        private void button24_Click(object sender, EventArgs e)
        {
            //챔버 도어 전체 하강
            EtherCAT_M.Digital_Output(5, true);
            EtherCAT_M.Digital_Output(4, false);
            EtherCAT_M.Digital_Output(8, true);
            EtherCAT_M.Digital_Output(7, false);
            EtherCAT_M.Digital_Output(11, true);
            EtherCAT_M.Digital_Output(10, false);
        }

        private void button25_Click(object sender, EventArgs e)
        {
            // 타워 램프 전체 ON
            EtherCAT_M.Digital_Output(0, true);
            EtherCAT_M.Digital_Output(1, true);
            EtherCAT_M.Digital_Output(2, true);

        }

        private void button26_Click(object sender, EventArgs e)
        {
            // 타워램프 전체 OFF
            EtherCAT_M.Digital_Output(0, false);
            EtherCAT_M.Digital_Output(1, false);
            EtherCAT_M.Digital_Output(2, false);
        }

        private void button27_Click(object sender, EventArgs e)
        {
            // Servo ON
            EtherCAT_M.Axis1_ON(); // UP
            EtherCAT_M.Axis2_ON();
        }

        private void button28_Click(object sender, EventArgs e)
        {
            // Servo OFF
            EtherCAT_M.Axis1_OFF(); 
            EtherCAT_M.Axis2_OFF();
        }

        private void button29_Click(object sender, EventArgs e)
        {// 상 하 원점 복귀
            if (label10.Text == "false") //블레이드 전진 OFF
            {
                EtherCAT_M.Axis1_UD_Homming();
            }
            else 
            {
                MessageBox.Show("블레이드 전진 상태에서는 홈포지션 이동이 불가합니다. 블레이드를 후퇴시킨 후 다시 시도해주세요.");
            }
   

        }

        private void button30_Click(object sender, EventArgs e)
        {// 좌 우 원점 복귀
            if (label10.Text == "false")// 블레이드 전진 OFF
            {
                EtherCAT_M.Axis2_LR_Homming();
            }
            else
            { 
                MessageBox.Show("블레이드 전진 상태에서는 홈포지션 이동이 불가합니다. 블레이드를 후퇴시킨 후 다시 시도해주세요.");
            }
            

        }

        private void button33_Click(object sender, EventArgs e)
        {
            // 웨이퍼 이송 로봇 로드레스 실린더 전진
            
            {
                EtherCAT_M.Digital_Output(12, true);
                EtherCAT_M.Digital_Output(13, false);
                // 전진 시 True
                label10.Text = "true";
                label11.Text = "false";









            }
        }

        private void button34_Click(object sender, EventArgs e)
        {
            // 웨이퍼 이송 로봇 로드레스 실린더 후퇴
            EtherCAT_M.Digital_Output(13,true);
            EtherCAT_M.Digital_Output(12,false);
            label10.Text = "false";
            label11.Text = "true";

        }

        private void groupBox7_Enter(object sender, EventArgs e)
        {

        }

        private void button35_Click(object sender, EventArgs e)
        {
            // 흡기 시작
            EtherCAT_M.Digital_Output(14,true);
        }

        private void button36_Click(object sender, EventArgs e)
        {
            // 흡기 정지
            EtherCAT_M.Digital_Output(14,false);
        }

        private void button37_Click(object sender, EventArgs e)
        {
            //배기 시작
             EtherCAT_M.Digital_Output(15,true);
        }

        private void button38_Click(object sender, EventArgs e)
        {
            //배기 정지
            EtherCAT_M.Digital_Output(15,false);
        }

        private async void button31_Click(object sender, EventArgs e)
        {
            // 상하 타켓 위치 이동
            if (label10.Text == "false")
            {
                long target = (Int64)numericUpDown1.Value;
                string beforePos = EtherCAT_M.Axis1_is_PosData();
                LoggerConfig.Log.Info($"[수동이동추적] 상하(Z) 이동 시작 — 이동 전={beforePos}, 목표={target}");

                EtherCAT_M.Axis1_UD_POS_Update(target);
                EtherCAT_M.Axis1_UD_Move_Send();
                // numericUpDown1 값만큼 이동 합니다.

                // [진단용] INIT 자동 로직과 실제 하드웨어 거동을 비교하기 위해, 이동 후 3초간
                // 200ms 간격으로 실제 위치를 추적해 로그(화면 LOG창 + 파일)에 남깁니다.
                await TrackAxisMoveAsync("상하(Z)", () => EtherCAT_M.Axis1_is_PosData(), target);

                MessageBox.Show("상하 이동 명령 전송됨!\n목표 값: " + numericUpDown1.Value + "\n최종 위치: " + EtherCAT_M.Axis1_is_PosData());
            }

            else
            {
                MessageBox.Show("블레이드 전진 상태에서는 상하 이동이 불가합니다. 블레이드를 후퇴시킨 후 다시 시도해주세요.");
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private async void button32_Click(object sender, EventArgs e)
        {
            //  좌우 타겟 위치 이동
            if (label10.Text == "false")
            {
                long target = (Int64)numericUpDown1.Value;
                string beforePos = EtherCAT_M.Axis2_is_PosData();
                LoggerConfig.Log.Info($"[수동이동추적] 좌우(X) 이동 시작 — 이동 전={beforePos}, 목표={target}");

                EtherCAT_M.Axis2_LR_POS_Update(target);
                EtherCAT_M.Axis2_LR_Move_Send();

                // [진단용] INIT 자동 로직과 실제 하드웨어 거동을 비교하기 위해, 이동 후 3초간
                // 200ms 간격으로 실제 위치를 추적해 로그(화면 LOG창 + 파일)에 남깁니다.
                await TrackAxisMoveAsync("좌우(X)", () => EtherCAT_M.Axis2_is_PosData(), target);

                // 상태 진단을 위해 현재 위치를 팝업으로 출력합니다.
                string currentPos = EtherCAT_M.Axis2_is_PosData();
                MessageBox.Show("좌우 이동 명령 전송됨!\n목표 값: " + numericUpDown1.Value + "\n현재 위치(Pos): " + currentPos);
            }

            else
            {
                MessageBox.Show("블레이드 전진 상태에서는 좌우 이동이 불가합니다. 블레이드를 후퇴시킨 후 다시 시도해주세요.");
            }

        }

        // 버튼 클릭 후 실제로 축이 목표까지 도달하는 과정을 200ms 간격으로 3초간 추적해서 로그로 남깁니다.
        // (수동 이동과 INIT 자동 로직의 실제 하드웨어 거동을 비교 진단하기 위한 용도)
        private async Task TrackAxisMoveAsync(string axisTag, Func<string> readPos, long target)
        {
            for (int elapsed = 0; elapsed <= 3000; elapsed += 200)
            {
                string raw = readPos();
                string numStr = System.Text.RegularExpressions.Regex.Match(raw ?? "", @"-?\d+").Value;
                long.TryParse(numStr, out long cur);
                LoggerConfig.Log.Info($"[수동이동추적] {axisTag} t={elapsed}ms 현재={cur} 목표={target} 차이={cur - target}");
                await Task.Delay(200);
            }
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            // 가속도
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            //감속도
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            // 최대속도
        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            //속도
        }

        private void button39_Click(object sender, EventArgs e)
        {
            //이송로봇 파라미터 적용
            EtherCAT_M.Axis1_UD_Config_Update((Int64)numericUpDown2.Value, (Int64)numericUpDown3.Value, (Int64)numericUpDown4.Value, (Int64)numericUpDown5.Value);
            EtherCAT_M.Axis2_LR_Config_Update((Int64)numericUpDown2.Value, (Int64)numericUpDown3.Value, (Int64)numericUpDown4.Value, (Int64)numericUpDown5.Value);
            
            // 전송이 성공적으로 수행되었음을 사용자에게 팝업으로 알립니다.
            MessageBox.Show("이송로봇 파라메타 전송 완료!\n" +
                            "- 가속도 (Acc): " + numericUpDown2.Value + "\n" +
                            "- 감속도 (Dec): " + numericUpDown3.Value + "\n" +
                            "- 최대속도 (Max Vel): " + numericUpDown4.Value + "\n" +
                            "- 속도 (Vel): " + numericUpDown5.Value);
        }

        private void label10_Click(object sender, EventArgs e)
        {
            // 라벨 기본 상태 
            label10.Text = "false";
        }

        private void label11_Click(object sender, EventArgs e)
        {
            // 라벨 기본 상태 
            label11.Text = "true";

        }

        private void label13_Click(object sender, EventArgs e)
        {
            // UP /Down Axis Status
            // 상/하 현재 위치 라벨에 출력


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (label2.Text == "OK")
            {
                label13.Text = EtherCAT_M.Axis1_is_PosData(); // 상하 현재 위치 라벨에 출력 
                label18.Text = EtherCAT_M.Axis1_Status("PP_M").ToString(); // 상하 상태 PP모드 라벨에 출력  PP 모드 (위치 제어 - Profile Position)
                label19.Text = EtherCAT_M.Axis1_Status("HOME_M").ToString(); // 상하 상태 HOME모드 라벨에 출력 Home 모드 (원점 복귀 - Homing)
                label20.Text = EtherCAT_M.Axis1_Status("PP_D").ToString(); // 상하 상태 PP 위치 결정 라벨에 출력
                label21.Text = EtherCAT_M.Axis1_Status("HOME_D").ToString(); // 상하 상태 HOME 위치 결정 라벨에 출력

                label32.Text = EtherCAT_M.Axis2_is_PosData(); // 좌우 현재 위치 라벨에 출력
                label33.Text = EtherCAT_M.Axis2_Status("PP_M").ToString(); // 좌우 상태 PP모드 라벨에 출력  PP 모드 (위치 제어 - Profile Position)
                label34.Text = EtherCAT_M.Axis2_Status("HOME_M").ToString(); // 좌우 상태 HOME모드 라벨에 출력 Home 모드 (원점 복귀 - Homing)
                label35.Text = EtherCAT_M.Axis2_Status("PP_D").ToString(); // 좌우 상태 PP 위치 결정 라벨에 출력
                label36.Text = EtherCAT_M.Axis2_Status("HOME_D").ToString(); // 좌우 상태 HOME 위치 결정 라벨에 출력

                label42.Text = EtherCAT_M.Digital_Input(0).ToString(); // P000 SW-1 입력 상태 라벨에 출력
                label43.Text = EtherCAT_M.Digital_Input(1).ToString(); // P001 SW-2 입력 상태 라벨에 출력
                label44.Text = EtherCAT_M.Digital_Input(2).ToString(); // P002 Select SW 입력 상태 라벨에 출력
                label45.Text = EtherCAT_M.Digital_Input(3).ToString(); // P003 EMG SW(비상정지) 입력 상태 라벨에 출력
                label46.Text = EtherCAT_M.Digital_Input(5).ToString(); // P005 Main Process 입력 상태 라벨에 출력

                // SEMI E95 UI 갱신 (UserControl 내부 Timer로 이관됨)
            }
        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void button40_Click(object sender, EventArgs e)
        {
            // jog 왼쪽 이동
            // numericupdown6 값을 가져와서 좌 이동 거리로 사용

            if (label10.Text == "false")
            {
                if (Int64.TryParse(EtherCAT_M.Axis2_is_PosData(), out Int64 currentPos)) {
                    Int64 targetPos = currentPos - (Int64)numericUpDown6.Value;
                    EtherCAT_M.Axis2_LR_POS_Update(targetPos);
                    EtherCAT_M.Axis2_LR_Move_Send();
                    MessageBox.Show("좌측 조그 이동 명령 전송됨!\n현재 위치: " + currentPos + "\n목표 값: " + targetPos);
                } else {
                    MessageBox.Show("현재 위치를 읽을 수 없습니다.");
                }
            }
            else
            { 
                MessageBox.Show("블레이드 전진 상태에서는 좌우 이동이 불가합니다. 블레이드를 후퇴시킨 후 다시 시도해주세요.");
            }

        }

        private void button41_Click(object sender, EventArgs e)
        {
            // jog 오른쪽 이동
            // numericupdown6 값을 가져와서 좌 이동 거리로 사용

            if (label10.Text == "false")
            {
                if (Int64.TryParse(EtherCAT_M.Axis2_is_PosData(), out Int64 currentPos)) {
                    Int64 targetPos = currentPos + (Int64)numericUpDown6.Value;
                    EtherCAT_M.Axis2_LR_POS_Update(targetPos);
                    EtherCAT_M.Axis2_LR_Move_Send();
                    MessageBox.Show("우측 조그 이동 명령 전송됨!\n현재 위치: " + currentPos + "\n목표 값: " + targetPos);
                } else {
                    MessageBox.Show("현재 위치를 읽을 수 없습니다.");
                }
            }
            else
            {
                MessageBox.Show("블레이드 전진 상태에서는 좌우 이동이 불가합니다. 블레이드를 후퇴시킨 후 다시 시도해주세요.");
            }

        }

        private void button42_Click(object sender, EventArgs e)
        {
            // jog 위쪽 이동
            // numericupdown6 값을 가져와서 상 이동 거리로 사용
            if (label10.Text == "false")
            {
                if (Int64.TryParse(EtherCAT_M.Axis1_is_PosData(), out Int64 currentPos)) {
                    Int64 targetPos = currentPos + (Int64)numericUpDown6.Value;
                    EtherCAT_M.Axis1_UD_POS_Update(targetPos);
                    EtherCAT_M.Axis1_UD_Move_Send();
                    MessageBox.Show("상향 조그 이동 명령 전송됨!\n현재 위치: " + currentPos + "\n목표 값: " + targetPos);
                } else {
                    MessageBox.Show("현재 위치를 읽을 수 없습니다.");
                }
            }
            else
            { 
                MessageBox.Show("블레이드 전진 상태에서는 상하 이동이 불가합니다. 블레이드를 후퇴시킨 후 다시 시도해주세요.");
            }

        }

        private void button43_Click(object sender, EventArgs e)
        {

            // jog 아래쪽 이동
            // numericupdown6 값을 가져와서 상 이동 거리로 사용
            if (label10.Text == "false")
            {
                if (Int64.TryParse(EtherCAT_M.Axis1_is_PosData(), out Int64 currentPos)) {
                    Int64 targetPos = currentPos - (Int64)numericUpDown6.Value;
                    EtherCAT_M.Axis1_UD_POS_Update(targetPos);
                    EtherCAT_M.Axis1_UD_Move_Send();
                    MessageBox.Show("하향 조그 이동 명령 전송됨!\n현재 위치: " + currentPos + "\n목표 값: " + targetPos);
                } else {
                    MessageBox.Show("현재 위치를 읽을 수 없습니다.");
                }
            }
            else
            {
                MessageBox.Show("블레이드 전진 상태에서는 상하 이동이 불가합니다. 블레이드를 후퇴시킨 후 다시 시도해주세요.");
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Form1은 이제 수동 조작용 모달 창으로만 사용되므로,
            // 여기서 메인 UI(SemiE95View)를 로드해서 덮어씌우지 않습니다.
        }

        private void label47_Click(object sender, EventArgs e)
        {
            label47.Text = EtherCAT_M.Digital_Input(8).ToString();
           // label47.Text = EtherCAT_M.Digital_Input(7).ToString();
        }
    }
}
