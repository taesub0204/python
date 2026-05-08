namespace Hello
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            display = new Label();
            textBox1 = new TextBox();
            btnStart = new Button();
            SuspendLayout();
            // 
            // display
            // 
            display.Anchor = AnchorStyles.Top;
            display.AutoSize = true;
            display.Location = new Point(126, 58);
            display.Name = "display";
            display.Size = new Size(254, 15);
            display.TabIndex = 0;
            display.Text = "게임을 시작하려면 게임시작 버튼을 누르세요.";
            display.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // textBox1
            // 
            textBox1.Enabled = false;
            textBox1.Font = new Font("맑은 고딕", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox1.Location = new Point(126, 188);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(328, 33);
            textBox1.TabIndex = 1;
            textBox1.KeyDown += textBox1_KeyDown;
            // 
            // btnStart
            // 
            btnStart.BackColor = SystemColors.GradientActiveCaption;
            btnStart.Dock = DockStyle.Bottom;
            btnStart.Location = new Point(0, 393);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(598, 57);
            btnStart.TabIndex = 2;
            btnStart.Text = "게임시작";
            btnStart.UseVisualStyleBackColor = false;
            btnStart.Click += btnStart_Click;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(598, 450);
            Controls.Add(btnStart);
            Controls.Add(textBox1);
            Controls.Add(display);
            Name = "Form2";
            Text = "Form2";
            Load += Form2_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label display;
        private TextBox textBox1;
        private Button btnStart;
    }
}