using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Hello
{
    public partial class Form2 : Form  // form을 상속 받아서 만들도록 지원이 되어 있음 . form2는 form의 자식 클래스가 된다. form2는 form의 기능을 모두 사용할 수 있다.
    {
        private int findNum = 0;
        private int chance = 10;

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            //바는 초기화해야되고 전역변수로 쓸 수 없어
            var rand = new Random();
            findNum = rand.Next(1, 101); // 1~100사이의 랜덤한 숫자를 만들어주는 메서드
            //label1.Text = findNum.ToString();


            chance = 10;
            display.Text = "숫자를 입력하세요.";

            textBox1.Enabled = true;
            textBox1.Focus();

            btnStart.Enabled = false;
            btnStart.Text = "시작하기";




        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
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

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
