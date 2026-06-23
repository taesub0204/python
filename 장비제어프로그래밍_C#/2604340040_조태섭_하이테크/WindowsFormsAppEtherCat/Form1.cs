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
        IEG3268 EtherCAT_M = new IEG3268();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //연결
            if (EtherCAT_M.CIFX_50RE_Connect() == true)
            {
                label2.Text = "Connect OK!!!";
                EtherCAT_M.ReadData_Send_Start(300); //Timer Interval Set
                EtherCAT_M.ReadData_Timer_Start(); //Timer Start

                /*test
                 // 초기에 모든 동작 1회 이상 실행하여 정상적으로 작동하는지 확인
                // 연결과 동시에 타워램프 깜빡
                // 색깔 별로 ON/OFF를 반복하여 깜빡임을 구현
                for (int i = 0; i < 3; i++)
                {
                    System.Threading.Thread.Sleep(500);
                    EtherCAT_M.Digital_Output(0, false);
                    System.Threading.Thread.Sleep(500);
                    EtherCAT_M.Digital_Output(0, true);
                    System.Threading.Thread.Sleep(500);
                    EtherCAT_M.Digital_Output(0, false);

                    System.Threading.Thread.Sleep(500);
                    EtherCAT_M.Digital_Output(1, false);
                    System.Threading.Thread.Sleep(500);
                    EtherCAT_M.Digital_Output(1, true);
                    System.Threading.Thread.Sleep(500);
                    EtherCAT_M.Digital_Output(1, false);

                    System.Threading.Thread.Sleep(500);
                    EtherCAT_M.Digital_Output(2, false);
                    System.Threading.Thread.Sleep(500);
                    EtherCAT_M.Digital_Output(2, true);
                    System.Threading.Thread.Sleep(500);
                    EtherCAT_M.Digital_Output(2, false);

                }
                // 챔버 램프 깜빡
                for (int i = 0; i < 3; i++)
                {
                    System.Threading.Thread.Sleep(500);
                    EtherCAT_M.Digital_Output(3, false);
                    System.Threading.Thread.Sleep(500);
                    EtherCAT_M.Digital_Output(3, true);
                    System.Threading.Thread.Sleep(500);
                    EtherCAT_M.Digital_Output(3, false);
                    System.Threading.Thread.Sleep(500);
                    EtherCAT_M.Digital_Output(6, false);
                    System.Threading.Thread.Sleep(500);
                    EtherCAT_M.Digital_Output(6, true);
                    System.Threading.Thread.Sleep(500);
                    EtherCAT_M.Digital_Output(6, false);
                    System.Threading.Thread.Sleep(500);
                    EtherCAT_M.Digital_Output(9, false);
                    System.Threading.Thread.Sleep(500);
                    EtherCAT_M.Digital_Output(9, true);
                    System.Threading.Thread.Sleep(500);
                    EtherCAT_M.Digital_Output(9, false);
                }

                // 챔버 도어 상승/하강 테스트
                // 마지막엔 닫혀야대
                for (int i = 0; i < 5; i++)
                {
                    System.Threading.Thread.Sleep(500);
                    EtherCAT_M.Digital_Output(4, true);
                    EtherCAT_M.Digital_Output(5, false);
                    System.Threading.Thread.Sleep(500);
                    EtherCAT_M.Digital_Output(4, false);
                    EtherCAT_M.Digital_Output(5, true);
                    System.Threading.Thread.Sleep(500);
                    EtherCAT_M.Digital_Output(4, true);
                    EtherCAT_M.Digital_Output(5, false);



                    System.Threading.Thread.Sleep(500);
                    EtherCAT_M.Digital_Output(7, true);
                    EtherCAT_M.Digital_Output(8, false);
                    System.Threading.Thread.Sleep(500);
                    EtherCAT_M.Digital_Output(7, false);
                    EtherCAT_M.Digital_Output(8, true);
                    System.Threading.Thread.Sleep(500);
                    EtherCAT_M.Digital_Output(7, true);
                    EtherCAT_M.Digital_Output(8, false);


                    System.Threading.Thread.Sleep(500);
                    EtherCAT_M.Digital_Output(10, true);
                    EtherCAT_M.Digital_Output(11, false);
                    System.Threading.Thread.Sleep(500);
                    EtherCAT_M.Digital_Output(10, false);
                    EtherCAT_M.Digital_Output(11, true);
                    System.Threading.Thread.Sleep(500);
                    EtherCAT_M.Digital_Output(10, true);
                    EtherCAT_M.Digital_Output(11, false);


                }
                */


            }
            else
            {
                label2.Text = "NG!!!";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
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
            EtherCAT_M.Digital_Output(12, false);
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
    }


}
