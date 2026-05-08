namespace Hello
{
    public partial class For : Form //클래스이름이랑 똑같다 생성자 
    {
        public For()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnOut_Click(object sender, EventArgs e)
        {
            lblOut.Text = textBox1.Text;
            btnin.BackColor = Color.Coral;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnin_MouseEnter(object sender, EventArgs e)
        {
            btnin.BackColor = Color.LightPink;

        }

        private void btnin_MouseLeave(object sender, EventArgs e)
        {
            btnin.BackColor = Color.MistyRose;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) // Enter키를 누르면
            {
                lblOut.Text = textBox1.Text; // 텍스트박스의 내용을 라벨에 출력
                textBox1.Text = null;

            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            { 
                lblOut.Text = $"아이디는 {textBox1.Text}이고 패스워드는{textBox2.Text}입니다.";
                //lblOut.Text = "아이디" + textBox1.Text + "이고 패스워드는" + textBox2.Text;
            }
        }
    }
}
