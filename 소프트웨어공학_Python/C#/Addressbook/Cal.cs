using MySqlX.XDevAPI.Common;
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
    public partial class Cal : Form
    {
        public Cal()
        {
            InitializeComponent();

            // textBox에서 Enter 키 처리
            textBox1.KeyDown += textBox1_KeyDown;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        // textBox에서 Enter 키 처리
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button14_Click(sender, e); // 계산 실행
                e.Handled = true; // Enter 키 소리 방지
                e.SuppressKeyPress = true; // 빵 소리 방지
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //button을 선택 했을 때 4 표시 textbox1에 4이 표시 되도록
            textBox1.Text += "4"; // 끝에 4 추가
            textBox1.Focus();
            textBox1.SelectionStart = textBox1.Text.Length;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //button을 선택 했을 때 3 표시 textbox1에 3이 표시 되도록
            textBox1.Text += "3"; // 끝에 3 추가
            textBox1.Focus();
            textBox1.SelectionStart = textBox1.Text.Length;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //button을 선택 했을 때 5 표시 textbox1에 5이 표시 되도록
            textBox1.Text += "5"; // 끝에 5 추가
            textBox1.Focus();
            textBox1.SelectionStart = textBox1.Text.Length;
        }

        private void button20_Click(object sender, EventArgs e)
        {
            // 뒤로 한칸 지우기 (Backspace)
            if (textBox1.Text.Length > 0) // 텍스트가 있을 때만
            {
                textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Length - 1);
                // 처음부터 (마지막 글자 - 1)까지만 가져오기
            }
            textBox1.Focus();
            textBox1.SelectionStart = textBox1.Text.Length;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //button을 선택 했을 때 1 표시 textbox1에 1이 표시 되도록
            textBox1.Text += "1"; // 끝에 1 추가
            textBox1.Focus();
            textBox1.SelectionStart = textBox1.Text.Length;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //button을 선택 했을 때 2 표시 textbox1에 2이 표시 되도록
            textBox1.Text += "2"; // 끝에 2 추가
            textBox1.Focus();
            textBox1.SelectionStart = textBox1.Text.Length;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Text += "6"; // 끝에 6 추가
            textBox1.Focus();
            textBox1.SelectionStart = textBox1.Text.Length;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox1.Text += "7"; // 끝에 7 추가
            textBox1.Focus();
            textBox1.SelectionStart = textBox1.Text.Length;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox1.Text += "8"; // 끝에 8 추가
            textBox1.Focus();
            textBox1.SelectionStart = textBox1.Text.Length;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBox1.Text += "9"; // 끝에 9 추가
            textBox1.Focus();
            textBox1.SelectionStart = textBox1.Text.Length;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            textBox1.Text += "0"; // 끝에 0 추가
            textBox1.Focus();
            textBox1.SelectionStart = textBox1.Text.Length;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            //괄호 버튼 - 괄호 기능이 필요 없으면 간단하게: textBox1.Text += "(";
            textBox1.Text += "(";
            textBox1.Focus();
            textBox1.SelectionStart = textBox1.Text.Length;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            textBox1.Text += "."; // 끝에 소수점 추가
            textBox1.Focus();
            textBox1.SelectionStart = textBox1.Text.Length;
        }

        private void button21_Click(object sender, EventArgs e)
        {
            //괄호 버튼 - 괄호 기능이 필요 없으면 간단하게: textBox1.Text += "(";
            textBox1.Text += ")";
            textBox1.Focus();
            textBox1.SelectionStart = textBox1.Text.Length;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            textBox1.Text += "+"; // 끝에 + 추가
            textBox1.Focus();
            textBox1.SelectionStart = textBox1.Text.Length;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            string input = textBox1.Text; // 입력된 수식 가져오기

            // 중요! "x"를 "*"로 변환 (사용자는 x를 보지만 컴퓨터는 *로 계산)
            input = input.Replace("x", "*");

            // % = 백분율로 변환 (5% → 5*0.01 = 0.05)
            input = input.Replace("%", "*0.01");

            DataTable dt = new DataTable(); // DataTable 객체 생성

            try
            {
                var result = dt.Compute(input, null); // 수식 계산

                textBox1.Text = result.ToString(); // 결과 표시
                display.Text = result.ToString();  // display에도 결과 표시
            }
            catch (Exception ex)
            {
                MessageBox.Show("잘못된 수식입니다: " + ex.Message); // 오류 처리
                textBox1.Text = "오류";
            }

            // 계산 후에도 포커스와 커서 유지
            textBox1.Focus();
            textBox1.SelectionStart = textBox1.Text.Length;
        }

        private void display_Click(object sender, EventArgs e)
        {
            display.Text = textBox1.Text;



        }

        private void button12_Click(object sender, EventArgs e)
        {
            textBox1.Text += "-"; // 끝에 - 추가
            textBox1.Focus();
            textBox1.SelectionStart = textBox1.Text.Length;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            textBox1.Text += "x"; // 끝에 x 추가
            textBox1.Focus();
            textBox1.SelectionStart = textBox1.Text.Length;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            textBox1.Text += "/"; // 끝에 / 추가
            textBox1.Focus();
            textBox1.SelectionStart = textBox1.Text.Length;
        }

        private void button19_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            // 계산기 초기화
            display.Text = "=";

            textBox1.Focus();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            // % = 백분율 (예: 5% → 0.05, 100% → 1)
            // 계산시 자동으로 *0.01 로 변환됨
            textBox1.Text += "%";
            textBox1.Focus();
            textBox1.SelectionStart = textBox1.Text.Length;
        }

        private void Cal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button14_Click(sender, e);
            }
        }
    }
}
