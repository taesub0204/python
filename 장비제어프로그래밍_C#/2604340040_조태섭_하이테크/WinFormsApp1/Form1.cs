namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();


            //myButton.Text = "코드에서변경";
        }

        private void button1_Click(object sender, EventArgs e)
        {

            int width = 100;
            int height = 30;
            int margin = 6;


            myButton.Text = "코드에서 변경";
            myButton.Width = width;
            myButton.Height = height;
            //Button button = new Button();
            //Controls.Add(button);

            for (int i = 0; i < 5; i++)
            { 
                Button button = new Button();
                Controls.Add(button);
                button.Location = new Point(margin, (height + margin) * (i + 1) + margin);
                button.Text = "동적생성" + i + "번째";
                button.Width = width;
                button.Height = height;

            }



        }
    }
}
