import re

with open('SemiE95View.Designer.cs', 'r', encoding='utf-8') as f:
    content = f.read()

replacement = '''            this.tlpHeader.TabIndex = 0;
            // 
            // lblLogo
            // 
            this.lblLogo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLogo.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.lblLogo.Location = new System.Drawing.Point(3, 0);
            this.lblLogo.Name = "lblLogo";
            this.lblLogo.Size = new System.Drawing.Size(232, 64);
            this.lblLogo.TabIndex = 0;
            this.lblLogo.Text = "SEMI E95 Compliant";
            this.lblLogo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblUser
            // 
            this.lblUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblUser.Font = new System.Drawing.Font("Arial", 12F);
            this.lblUser.Location = new System.Drawing.Point(241, 0);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(232, 64);
            this.lblUser.TabIndex = 1;
            this.lblUser.Text = "User\\n-";
            this.lblUser.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSystem
            // 
            this.lblSystem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSystem.Font = new System.Drawing.Font("Arial", 12F);
            this.lblSystem.Location = new System.Drawing.Point(479, 0);
            this.lblSystem.Name = "lblSystem";
            this.lblSystem.Size = new System.Drawing.Size(232, 64);
            this.lblSystem.TabIndex = 2;
            this.lblSystem.Text = "System\\nEQU-KIT";
            this.lblSystem.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDateTime
            // 
            this.lblDateTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDateTime.Font = new System.Drawing.Font("Arial", 12F);
            this.lblDateTime.Location = new System.Drawing.Point(717, 0);
            this.lblDateTime.Name = "lblDateTime";
            this.lblDateTime.Size = new System.Drawing.Size(232, 64);
            this.lblDateTime.TabIndex = 3;
            this.lblDateTime.Text = "Date / Time\\nYYYY/MM/DD HH:MM:SS";
            this.lblDateTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlHeaderBtns
            // 
            this.pnlHeaderBtns.Controls.Add(this.btnLogin);
            this.pnlHeaderBtns.Controls.Add(this.btnAlarmHeader);
            this.pnlHeaderBtns.Controls.Add(this.btnHelp);
            this.pnlHeaderBtns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHeaderBtns.Location = new System.Drawing.Point(955, 3);
            this.pnlHeaderBtns.Name = "pnlHeaderBtns";
            this.pnlHeaderBtns.Size = new System.Drawing.Size(236, 58);
            this.pnlHeaderBtns.TabIndex = 4;
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(10, 5);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(65, 50);
            this.btnLogin.TabIndex = 0;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnAlarmHeader
            // 
            this.btnAlarmHeader.Location = new System.Drawing.Point(85, 5);
            this.btnAlarmHeader.Name = "btnAlarmHeader";
            this.btnAlarmHeader.Size = new System.Drawing.Size(65, 50);
            this.btnAlarmHeader.TabIndex = 1;
            this.btnAlarmHeader.Text = "Alarm";
            this.btnAlarmHeader.UseVisualStyleBackColor = true;
            // 
            // btnHelp
            // 
            this.btnHelp.Location = new System.Drawing.Point(160, 5);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(65, 50);
            this.btnHelp.TabIndex = 2;
            this.btnHelp.Text = "Help";
            this.btnHelp.UseVisualStyleBackColor = true;
            // 
            // tlpContent'''

pattern = r'            this\.tlpHeader\.TabIndex = 0;.*?// tlpContent'
new_content = re.sub(pattern, replacement, content, flags=re.DOTALL)

with open('SemiE95View.Designer.cs', 'w', encoding='utf-8') as f:
    f.write(new_content)
