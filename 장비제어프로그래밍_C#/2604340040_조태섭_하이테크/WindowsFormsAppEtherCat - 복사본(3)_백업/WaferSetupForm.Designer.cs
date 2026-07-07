namespace WindowsFormsAppEtherCat
{
    partial class WaferSetupForm
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
            this.chkSlot1 = new System.Windows.Forms.CheckBox();
            this.chkSlot2 = new System.Windows.Forms.CheckBox();
            this.chkSlot3 = new System.Windows.Forms.CheckBox();
            this.chkSlot4 = new System.Windows.Forms.CheckBox();
            this.chkSlot5 = new System.Windows.Forms.CheckBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(30, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(200, 19);
            this.lblTitle.TabIndex = 7;
            this.lblTitle.Text = "Select Wafers for Process";
            // 
            // chkSlot1
            // 
            this.chkSlot1.AutoSize = true;
            this.chkSlot1.Location = new System.Drawing.Point(40, 60);
            this.chkSlot1.Name = "chkSlot1";
            this.chkSlot1.Size = new System.Drawing.Size(56, 16);
            this.chkSlot1.TabIndex = 0;
            this.chkSlot1.Text = "Slot 1";
            this.chkSlot1.UseVisualStyleBackColor = true;
            // 
            // chkSlot2
            // 
            this.chkSlot2.AutoSize = true;
            this.chkSlot2.Location = new System.Drawing.Point(40, 90);
            this.chkSlot2.Name = "chkSlot2";
            this.chkSlot2.Size = new System.Drawing.Size(56, 16);
            this.chkSlot2.TabIndex = 1;
            this.chkSlot2.Text = "Slot 2";
            this.chkSlot2.UseVisualStyleBackColor = true;
            // 
            // chkSlot3
            // 
            this.chkSlot3.AutoSize = true;
            this.chkSlot3.Location = new System.Drawing.Point(40, 120);
            this.chkSlot3.Name = "chkSlot3";
            this.chkSlot3.Size = new System.Drawing.Size(56, 16);
            this.chkSlot3.TabIndex = 2;
            this.chkSlot3.Text = "Slot 3";
            this.chkSlot3.UseVisualStyleBackColor = true;
            // 
            // chkSlot4
            // 
            this.chkSlot4.AutoSize = true;
            this.chkSlot4.Location = new System.Drawing.Point(40, 150);
            this.chkSlot4.Name = "chkSlot4";
            this.chkSlot4.Size = new System.Drawing.Size(56, 16);
            this.chkSlot4.TabIndex = 3;
            this.chkSlot4.Text = "Slot 4";
            this.chkSlot4.UseVisualStyleBackColor = true;
            // 
            // chkSlot5
            // 
            this.chkSlot5.AutoSize = true;
            this.chkSlot5.Location = new System.Drawing.Point(40, 180);
            this.chkSlot5.Name = "chkSlot5";
            this.chkSlot5.Size = new System.Drawing.Size(56, 16);
            this.chkSlot5.TabIndex = 4;
            this.chkSlot5.Text = "Slot 5";
            this.chkSlot5.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(30, 220);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(80, 35);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "설정 완료";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(120, 220);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 35);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "취소";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // WaferSetupForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(250, 280);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.chkSlot5);
            this.Controls.Add(this.chkSlot4);
            this.Controls.Add(this.chkSlot3);
            this.Controls.Add(this.chkSlot2);
            this.Controls.Add(this.chkSlot1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WaferSetupForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Wafer Setup";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.CheckBox chkSlot1;
        private System.Windows.Forms.CheckBox chkSlot2;
        private System.Windows.Forms.CheckBox chkSlot3;
        private System.Windows.Forms.CheckBox chkSlot4;
        private System.Windows.Forms.CheckBox chkSlot5;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblTitle;
    }
}
