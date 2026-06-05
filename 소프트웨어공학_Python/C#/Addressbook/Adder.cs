using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Addressbook
{
    public partial class Adder : Form
    {
        double currentNum = 0;              //입력받은 수를 저장
        double resultNum = 0;               //현재까지 계산된 결과
        string operation = "";              //눌린 연산자 저장, + - ...
        bool isOperationClicked = false;    // 연산자가 선택되어 있는 상태인지...
        string expressText = "";            // 디스플레이 내용
        string resultText = "";
        double beforeResult = 0;          // 계산 직전의 결과 임시저장
 
        public Adder()
        {
            InitializeComponent();
            this.Location = new Point (1200, 100);
            updateDisplay();
        }

        private void updateDisplay()
        {
            txtAnswer.Text = expressText + Environment.NewLine + resultText;
        }

        private void Number_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            if (currentNum == 0)
            {
                if (expressText.EndsWith("."))
                {
                    currentNum = Convert.ToDouble(btn.Text) * 0.1;
                    resultNum = beforeResult;
                }
                else
                    currentNum = Convert.ToDouble(btn.Text);
            }

            else
            {
                if (expressText.EndsWith("."))
                    currentNum = Convert.ToDouble(currentNum.ToString() + "." + btn.Text);
                else
                    currentNum = Convert.ToDouble(currentNum.ToString() + btn.Text);
                    resultNum = beforeResult;


            }

            expressText += btn.Text;
            if (isOperationClicked)
            {
                Calculate(currentNum);
            }
            updateDisplay();
        }

        private void Calculate(double num)
        {
            beforeResult = resultNum;

            switch (operation)
            {
                case "＋": resultNum += num; break;
                case "－": resultNum -= num; break;
                case "×": resultNum *= num; break;
                case "÷":
                    if (num == 0)
                    {
                        MessageBox.Show("0으로 나눌수 없어~");
                        return;
                    }
                    resultNum /= num;
                    break;
            }
            resultText = resultNum.ToString();

        }
        private void Operator_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            operation = btn.Text;

            if (expressText.Length == 0) return;

            char lastChar = expressText[expressText.Length - 1];
            if ("+-×÷".Contains(lastChar))
                expressText = expressText.Substring(0, expressText.Length - 1) + btn.Text;
            else
                expressText += btn.Text;

            if (resultNum == 0 && currentNum != 0)
            {
                resultNum = currentNum;

            }
            currentNum = 0;
            isOperationClicked = true;
            resultText = "";
            updateDisplay();
        }

        private void Equal_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            expressText += btn.Text;
            currentNum = 0;
            operation = "";
            isOperationClicked = false;
            updateDisplay();
            expressText = resultText;
        }

        private void btnAC_Click(object sender, EventArgs e)
        {
            txtAnswer.Text = "0";
            currentNum = 0;
            resultNum = 0;
            operation = "";
            expressText = "";
            resultText = "";
            beforeResult = 0;
            isOperationClicked = false;
            
        }

        private void btnPM_Click(object sender, EventArgs e)
        {
            resultNum = beforeResult;

            if (currentNum != 0)
            {
                currentNum *= -1;
                Calculate(currentNum);
            }

            string currentNumStr = currentNum.ToString();
            if (currentNum < 0)
            {
                expressText = expressText.Substring(0, expressText.Length - currentNumStr.Length);
                expressText += currentNum.ToString();
            }
            else
            {
                expressText = expressText.Substring(0, expressText.Length - currentNumStr.Length - 1);
                expressText += "+" + currentNum.ToString();
            }

            updateDisplay();
        }



        private void btnDot_Click_1(object sender, EventArgs e)
        {
            string currentStr = currentNum.ToString();
         
           
            if (expressText.EndsWith(".")|| currentStr.Contains("."))  // 이미 소수점이 있으면 무시
                return;

            expressText += ".";
            updateDisplay();

        }

        private void btnSQ_Click(object sender, EventArgs e)
        {

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if(expressText.Length == 0) return;

            char del = expressText[expressText.Length - 1];


            expressText = expressText.Substring(0, expressText.Length - 1);

            if (char.IsDigit(del))
            {
                resultNum = beforeResult;
                string currentNumStr = currentNum.ToString();
                if (currentNumStr.Length > 1)
                {
                    currentNumStr = currentNumStr.Substring(0, currentNumStr.Length - 1);
                    currentNum = Convert.ToDouble(currentNumStr);
                }
                else
                {
                    currentNum = 0;
                }
                if (isOperationClicked)
                {
                    Calculate(currentNum);
                }



            }
            else if ("+-×÷".Contains(del))
            {

                currentNum = 0;
                isOperationClicked = false;
                resultText = "";

            }
            
            updateDisplay() ;



        }
    }
}
