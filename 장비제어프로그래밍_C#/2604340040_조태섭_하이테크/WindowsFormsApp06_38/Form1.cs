using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp06_38
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            button1.Click += Button1_Click;
            FormClosed += Form1_FormClosed;
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
          
            // 로그 파일에 기록
            System.IO.File.AppendAllText("log.txt", $"[{DateTime.Now}] Form1이 닫혔습니다.{ Environment.NewLine}");

            // 메세지 박스 출력 테스트용
            MessageBox.Show("폼이 닫혔습니다.","알림",MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //textBox1.Text += '+';
            //label1.Text = "+";
            Button self = (Button)sender;
            self.Text = "저를 클릭했습니다.";

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            textBox1.Text += '+';
            label1.Text = "+";


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private int elapsedTime = 0; // 경과 시간 (초)

        private void timer1_Tick(object sender, EventArgs e)
        {
           elapsedTime++; // 경과 시간 증가
           textBox2.Text = elapsedTime + "초 경과";
           label2.Text = elapsedTime + "초 경과";




        }
    }
}
