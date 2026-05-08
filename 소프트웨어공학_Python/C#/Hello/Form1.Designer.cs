namespace Hello
{
    partial class For
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnin = new Button();
            lblOut = new Label();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            label1 = new Label();
            label2 = new Label();
            SuspendLayout();
            // 
            // btnin
            // 
            btnin.BackColor = Color.MistyRose;
            btnin.Font = new Font("궁서체", 10F);
            btnin.Location = new Point(0, -2);
            btnin.Name = "btnin";
            btnin.Size = new Size(107, 70);
            btnin.TabIndex = 0;
            btnin.Text = "출력";
            btnin.UseVisualStyleBackColor = false;
            btnin.Click += btnOut_Click;
            btnin.MouseEnter += btnin_MouseEnter;
            btnin.MouseLeave += btnin_MouseLeave;
            // 
            // lblOut
            // 
            lblOut.BackColor = SystemColors.Info;
            lblOut.Font = new Font("휴먼모음T", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            lblOut.Location = new Point(87, 352);
            lblOut.Margin = new Padding(5, 0, 5, 0);
            lblOut.Name = "lblOut";
            lblOut.Size = new Size(385, 75);
            lblOut.TabIndex = 1;
            lblOut.Text = "여기에 출력";
            lblOut.TextAlign = ContentAlignment.MiddleCenter;
            lblOut.Click += label1_Click;
            // 
            // textBox1
            // 
            textBox1.Font = new Font("맑은 고딕", 15F);
            textBox1.Location = new Point(190, 204);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(282, 34);
            textBox1.TabIndex = 2;
            textBox1.TextChanged += textBox1_TextChanged;
            textBox1.KeyDown += textBox1_KeyDown;
            // 
            // textBox2
            // 
            textBox2.Font = new Font("맑은 고딕", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox2.Location = new Point(190, 266);
            textBox2.Name = "textBox2";
            textBox2.PasswordChar = '*';
            textBox2.Size = new Size(282, 35);
            textBox2.TabIndex = 3;
            textBox2.TextChanged += textBox2_TextChanged;
            textBox2.KeyDown += textBox2_KeyDown;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("맑은 고딕", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(87, 208);
            label1.Name = "label1";
            label1.Size = new Size(76, 30);
            label1.TabIndex = 4;
            label1.Text = "아이디";
            label1.Click += label1_Click_1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("맑은 고딕", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(87, 271);
            label2.Name = "label2";
            label2.Size = new Size(97, 30);
            label2.TabIndex = 5;
            label2.Text = "패스워드";
            label2.Click += label2_Click;
            // 
            // For
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 510);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(lblOut);
            Controls.Add(btnin);
            Font = new Font("맑은 고딕", 10F);
            Name = "For";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnin;
        private Label lblOut;
        private TextBox textBox1;
        private TextBox textBox2;
        private Label label1;
        private Label label2;
    }
}
