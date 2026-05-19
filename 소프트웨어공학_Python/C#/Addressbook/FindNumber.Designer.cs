namespace Addressbook
{
    partial class FindNumber
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
            this.display = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // display
            // 
            this.display.AutoSize = true;
            this.display.Location = new System.Drawing.Point(158, 81);
            this.display.Name = "display";
            this.display.Size = new System.Drawing.Size(253, 12);
            this.display.TabIndex = 0;
            this.display.Text = "게임을 시작하려면 게임시작 버튼을 누르세요.";
            this.display.Click += new System.EventHandler(this.label1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("굴림", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox1.Location = new System.Drawing.Point(160, 213);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(274, 35);
            this.textBox1.TabIndex = 1;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnStart_KeyDown);
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.MistyRose;
            this.btnStart.Location = new System.Drawing.Point(0, 415);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(803, 35);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "게임시작";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.button1_Click);
            this.btnStart.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnStart_KeyDown);
            // 
            // FindNumber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.display);
            this.Name = "FindNumber";
            this.Text = "FindNumber";
            this.Load += new System.EventHandler(this.FindNumber_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label display;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnStart;
    }
}