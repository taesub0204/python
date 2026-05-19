using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Addressbook
{
    public partial class FindNumber : Form
    {
        private int findNum = 0;
        private int chance = 10;

        public FindNumber()
        {
            InitializeComponent();
        }

        private void FindNumber_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var rand = new Random();
            findNum = rand.Next(1, 101 );

            chance = 10;
            display.Text = "숫자를 입력하세요.";

            textBox1.Enabled = true;
            textBox1.Focus();

            btnStart.Enabled = false;
            btnStart.Text = "시작하기";



        }

        private void btnStart_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                int inputNum = Int32.Parse(textBox1.Text);
                textBox1.Text = null;
                chance--;
                if (inputNum < findNum)
                {
                    display.Text = $"{inputNum} 보다 커야 해, 남은 기회 : {chance}";
                }
                else if (inputNum > findNum)
                {
                    display.Text = $"{inputNum} 보다 작아야 해, 남은 기회 : {chance}";
                }
                else
                {
                    display.Text = $"정답입니다! {10 - chance}번 만에 맞췄어요!";
                    textBox1.Enabled = false;
                    btnStart.Enabled = true;

                }

                if (chance <= 0)
                {
                    display.Text = $"기회를 모두 소진했습니다. 정답은 {findNum}입니다.다시할래?";
                    btnStart.Enabled = true;
                    textBox1.Enabled = false;
                    btnStart.Text = "다시하기";
                }





            }








        }
    }
}

