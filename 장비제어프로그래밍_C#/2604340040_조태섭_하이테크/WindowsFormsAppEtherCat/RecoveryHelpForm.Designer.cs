namespace WindowsFormsAppEtherCat
{
    partial class RecoveryHelpForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblBody = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // lblTitle
            //
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(180, 0, 0);
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(250, 21);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "알람 발생 시 복구 순서";
            //
            // lblBody
            //
            this.lblBody.Font = new System.Drawing.Font("Arial", 10F);
            this.lblBody.Location = new System.Drawing.Point(20, 50);
            this.lblBody.Name = "lblBody";
            this.lblBody.Size = new System.Drawing.Size(400, 300);
            this.lblBody.TabIndex = 1;
            this.lblBody.Text = "■ 정상 완료(COMPLETE) 후 재시연:\r\n" +
                "    STOP/INIT 없이 Wafer Setup에서\r\n    슬롯 체크 후 바로 START.\r\n\r\n" +
                "■ 중간에 알람이 뜨면:\r\n    STOP → INIT\r\n    (대부분 이것만으로 재호밍 성공)\r\n\r\n" +
                "■ INIT에서도 \"드라이브 무응답\"\r\n   알람이 또 뜨면:\r\n" +
                "    1) 서보 드라이브 전원을 껐다 켠다.\r\n" +
                "    2) 로그인(연결) 버튼을 다시 눌러 재연결.\r\n" +
                "    3) INIT을 다시 누른다.\r\n\r\n" +
                "당황하지 말고 이 순서대로 진행하면 됩니다.";
            //
            // btnClose
            //
            this.btnClose.Location = new System.Drawing.Point(170, 360);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 35);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "닫기";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            //
            // RecoveryHelpForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 415);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblBody);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RecoveryHelpForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "복구 도움말";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblBody;
        private System.Windows.Forms.Button btnClose;
    }
}
