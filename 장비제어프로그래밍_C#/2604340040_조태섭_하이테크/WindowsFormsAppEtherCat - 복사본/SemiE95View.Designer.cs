namespace WindowsFormsAppEtherCat
{
    partial class SemiE95View
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

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.uiTimer = new System.Windows.Forms.Timer(this.components);
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.tlpHeader = new System.Windows.Forms.TableLayoutPanel();
            this.lblLogo = new System.Windows.Forms.Label();
            this.lblUser = new System.Windows.Forms.Label();
            this.lblSystem = new System.Windows.Forms.Label();
            this.lblDateTime = new System.Windows.Forms.Label();
            this.pnlHeaderBtns = new System.Windows.Forms.Panel();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnAlarmHeader = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.tlpContent = new System.Windows.Forms.TableLayoutPanel();
            this.tlpLeft = new System.Windows.Forms.TableLayoutPanel();
            this.gbStatus = new System.Windows.Forms.GroupBox();
            this.tlpStatus = new System.Windows.Forms.TableLayoutPanel();
            this.lblStatusBig = new System.Windows.Forms.Label();
            this.lblStatusIndIdle = new System.Windows.Forms.Label();
            this.lblStatusIndRunning = new System.Windows.Forms.Label();
            this.lblStatusIndComplete = new System.Windows.Forms.Label();
            this.lblStatusIndAlarm = new System.Windows.Forms.Label();
            this.gbEqStatus = new System.Windows.Forms.GroupBox();
            this.tlpEq = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblRobotXPos = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblRobotZPos = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblBladeStatus = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblWaferStatus = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblDoorStatus = new System.Windows.Forms.Label();
            this.tlpCenter = new System.Windows.Forms.TableLayoutPanel();
            this.gbOverview = new System.Windows.Forms.GroupBox();
            this.pnlOverview = new System.Windows.Forms.Panel();
            this.lblAnimProcessStation = new System.Windows.Forms.Label();
            this.lblAnimFoupB = new System.Windows.Forms.Label();
            this.lblAnimRobot = new System.Windows.Forms.Label();
            this.lblAnimFoupA = new System.Windows.Forms.Label();
            this.gbPortStatus = new System.Windows.Forms.GroupBox();
            this.tlpPorts = new System.Windows.Forms.TableLayoutPanel();
            this.pnlPortA = new System.Windows.Forms.Panel();
            this.lblFoupAWafer = new System.Windows.Forms.Label();
            this.lblFoupAStatus = new System.Windows.Forms.Label();
            this.pnlPortProc = new System.Windows.Forms.Panel();
            this.lblProcWafer = new System.Windows.Forms.Label();
            this.lblProcStatus = new System.Windows.Forms.Label();
            this.pnlPortB = new System.Windows.Forms.Panel();
            this.lblFoupBWafer = new System.Windows.Forms.Label();
            this.lblFoupBStatus = new System.Windows.Forms.Label();
            this.tlpRight = new System.Windows.Forms.TableLayoutPanel();
            this.gbOp = new System.Windows.Forms.GroupBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnWaferSet = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnManualCtrl = new System.Windows.Forms.Button();
            this.btnHome = new System.Windows.Forms.Button();
            this.gbSysCtrl = new System.Windows.Forms.GroupBox();
            this.btnAuto = new System.Windows.Forms.Button();
            this.btnManual = new System.Windows.Forms.Button();
            this.btnAlarmReset = new System.Windows.Forms.Button();
            this.btnSysMgr = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.tlpFooter = new System.Windows.Forms.TableLayoutPanel();
            this.lblFooterLeft = new System.Windows.Forms.Label();
            this.lblFooterCenter = new System.Windows.Forms.Label();
            this.lblFooterRight = new System.Windows.Forms.Label();
            this.tlpMain.SuspendLayout();
            this.tlpHeader.SuspendLayout();
            this.pnlHeaderBtns.SuspendLayout();
            this.tlpContent.SuspendLayout();
            this.tlpLeft.SuspendLayout();
            this.gbStatus.SuspendLayout();
            this.tlpStatus.SuspendLayout();
            this.gbEqStatus.SuspendLayout();
            this.tlpEq.SuspendLayout();
            this.tlpCenter.SuspendLayout();
            this.gbOverview.SuspendLayout();
            this.pnlOverview.SuspendLayout();
            this.gbPortStatus.SuspendLayout();
            this.tlpPorts.SuspendLayout();
            this.pnlPortA.SuspendLayout();
            this.pnlPortProc.SuspendLayout();
            this.pnlPortB.SuspendLayout();
            this.tlpRight.SuspendLayout();
            this.gbOp.SuspendLayout();
            this.gbSysCtrl.SuspendLayout();
            this.tlpFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiTimer
            // 
            this.uiTimer.Interval = 1000;
            this.uiTimer.Tick += new System.EventHandler(this.uiTimer_Tick);
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.tlpHeader, 0, 0);
            this.tlpMain.Controls.Add(this.tlpContent, 0, 1);
            this.tlpMain.Controls.Add(this.tlpFooter, 0, 2);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 3;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpMain.Size = new System.Drawing.Size(1200, 800);
            this.tlpMain.TabIndex = 0;
            // 
            // tlpHeader
            // 
            this.tlpHeader.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tlpHeader.ColumnCount = 5;
            this.tlpHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpHeader.Controls.Add(this.lblLogo, 0, 0);
            this.tlpHeader.Controls.Add(this.lblUser, 1, 0);
            this.tlpHeader.Controls.Add(this.lblSystem, 2, 0);
            this.tlpHeader.Controls.Add(this.lblDateTime, 3, 0);
            this.tlpHeader.Controls.Add(this.pnlHeaderBtns, 4, 0);
            this.tlpHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpHeader.Location = new System.Drawing.Point(3, 3);
            this.tlpHeader.Name = "tlpHeader";
            this.tlpHeader.RowCount = 1;
            this.tlpHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpHeader.Size = new System.Drawing.Size(1194, 64);
            this.tlpHeader.TabIndex = 0;
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
            this.lblUser.Text = "User\n-";
            this.lblUser.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblUser.Click += new System.EventHandler(this.lblUser_Click);
            // 
            // lblSystem
            // 
            this.lblSystem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSystem.Font = new System.Drawing.Font("Arial", 12F);
            this.lblSystem.Location = new System.Drawing.Point(479, 0);
            this.lblSystem.Name = "lblSystem";
            this.lblSystem.Size = new System.Drawing.Size(232, 64);
            this.lblSystem.TabIndex = 2;
            this.lblSystem.Text = "System\nEQU-KIT";
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
            this.lblDateTime.Text = "Date / Time\nYYYY/MM/DD HH:MM:SS";
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
            // tlpContent
            // 
            this.tlpContent.ColumnCount = 3;
            this.tlpContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tlpContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpContent.Controls.Add(this.tlpLeft, 0, 0);
            this.tlpContent.Controls.Add(this.tlpCenter, 1, 0);
            this.tlpContent.Controls.Add(this.tlpRight, 2, 0);
            this.tlpContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpContent.Location = new System.Drawing.Point(3, 73);
            this.tlpContent.Name = "tlpContent";
            this.tlpContent.RowCount = 1;
            this.tlpContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpContent.Size = new System.Drawing.Size(1194, 684);
            this.tlpContent.TabIndex = 1;
            // 
            // tlpLeft
            // 
            this.tlpLeft.ColumnCount = 1;
            this.tlpLeft.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpLeft.Controls.Add(this.gbStatus, 0, 0);
            this.tlpLeft.Controls.Add(this.gbEqStatus, 0, 1);
            this.tlpLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpLeft.Location = new System.Drawing.Point(3, 3);
            this.tlpLeft.Name = "tlpLeft";
            this.tlpLeft.RowCount = 2;
            this.tlpLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpLeft.Size = new System.Drawing.Size(232, 678);
            this.tlpLeft.TabIndex = 0;
            // 
            // gbStatus
            // 
            this.gbStatus.Controls.Add(this.tlpStatus);
            this.gbStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbStatus.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.gbStatus.Location = new System.Drawing.Point(3, 3);
            this.gbStatus.Name = "gbStatus";
            this.gbStatus.Size = new System.Drawing.Size(226, 333);
            this.gbStatus.TabIndex = 0;
            this.gbStatus.TabStop = false;
            this.gbStatus.Text = "STATUS";
            // 
            // tlpStatus
            // 
            this.tlpStatus.ColumnCount = 1;
            this.tlpStatus.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpStatus.Controls.Add(this.lblStatusBig, 0, 0);
            this.tlpStatus.Controls.Add(this.lblStatusIndIdle, 0, 1);
            this.tlpStatus.Controls.Add(this.lblStatusIndRunning, 0, 2);
            this.tlpStatus.Controls.Add(this.lblStatusIndComplete, 0, 3);
            this.tlpStatus.Controls.Add(this.lblStatusIndAlarm, 0, 4);
            this.tlpStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpStatus.Location = new System.Drawing.Point(3, 22);
            this.tlpStatus.Name = "tlpStatus";
            this.tlpStatus.RowCount = 5;
            this.tlpStatus.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tlpStatus.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tlpStatus.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tlpStatus.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tlpStatus.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tlpStatus.Size = new System.Drawing.Size(220, 308);
            this.tlpStatus.TabIndex = 0;
            // 
            // lblStatusBig
            // 
            this.lblStatusBig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStatusBig.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold);
            this.lblStatusBig.ForeColor = System.Drawing.Color.Green;
            this.lblStatusBig.Location = new System.Drawing.Point(3, 0);
            this.lblStatusBig.Name = "lblStatusBig";
            this.lblStatusBig.Size = new System.Drawing.Size(214, 123);
            this.lblStatusBig.TabIndex = 0;
            this.lblStatusBig.Text = "> RUNNING";
            this.lblStatusBig.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStatusIndIdle
            // 
            this.lblStatusIndIdle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStatusIndIdle.ForeColor = System.Drawing.Color.Gray;
            this.lblStatusIndIdle.Location = new System.Drawing.Point(3, 123);
            this.lblStatusIndIdle.Name = "lblStatusIndIdle";
            this.lblStatusIndIdle.Size = new System.Drawing.Size(214, 46);
            this.lblStatusIndIdle.TabIndex = 1;
            this.lblStatusIndIdle.Text = "● IDLE";
            this.lblStatusIndIdle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStatusIndRunning
            // 
            this.lblStatusIndRunning.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStatusIndRunning.ForeColor = System.Drawing.Color.LightGray;
            this.lblStatusIndRunning.Location = new System.Drawing.Point(3, 169);
            this.lblStatusIndRunning.Name = "lblStatusIndRunning";
            this.lblStatusIndRunning.Size = new System.Drawing.Size(214, 46);
            this.lblStatusIndRunning.TabIndex = 2;
            this.lblStatusIndRunning.Text = "● RUNNING";
            this.lblStatusIndRunning.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStatusIndComplete
            // 
            this.lblStatusIndComplete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStatusIndComplete.ForeColor = System.Drawing.Color.LightGray;
            this.lblStatusIndComplete.Location = new System.Drawing.Point(3, 215);
            this.lblStatusIndComplete.Name = "lblStatusIndComplete";
            this.lblStatusIndComplete.Size = new System.Drawing.Size(214, 46);
            this.lblStatusIndComplete.TabIndex = 3;
            this.lblStatusIndComplete.Text = "● COMPLETE";
            this.lblStatusIndComplete.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStatusIndAlarm
            // 
            this.lblStatusIndAlarm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStatusIndAlarm.ForeColor = System.Drawing.Color.LightGray;
            this.lblStatusIndAlarm.Location = new System.Drawing.Point(3, 261);
            this.lblStatusIndAlarm.Name = "lblStatusIndAlarm";
            this.lblStatusIndAlarm.Size = new System.Drawing.Size(214, 47);
            this.lblStatusIndAlarm.TabIndex = 4;
            this.lblStatusIndAlarm.Text = "● ALARM";
            this.lblStatusIndAlarm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gbEqStatus
            // 
            this.gbEqStatus.Controls.Add(this.tlpEq);
            this.gbEqStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbEqStatus.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.gbEqStatus.Location = new System.Drawing.Point(3, 342);
            this.gbEqStatus.Name = "gbEqStatus";
            this.gbEqStatus.Size = new System.Drawing.Size(226, 333);
            this.gbEqStatus.TabIndex = 1;
            this.gbEqStatus.TabStop = false;
            this.gbEqStatus.Text = "EQUIPMENT STATUS";
            // 
            // tlpEq
            // 
            this.tlpEq.ColumnCount = 2;
            this.tlpEq.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpEq.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpEq.Controls.Add(this.label1, 0, 0);
            this.tlpEq.Controls.Add(this.lblRobotXPos, 1, 0);
            this.tlpEq.Controls.Add(this.label2, 0, 1);
            this.tlpEq.Controls.Add(this.lblRobotZPos, 1, 1);
            this.tlpEq.Controls.Add(this.label3, 0, 2);
            this.tlpEq.Controls.Add(this.lblBladeStatus, 1, 2);
            this.tlpEq.Controls.Add(this.label4, 0, 3);
            this.tlpEq.Controls.Add(this.lblWaferStatus, 1, 3);
            this.tlpEq.Controls.Add(this.label5, 0, 4);
            this.tlpEq.Controls.Add(this.lblDoorStatus, 1, 4);
            this.tlpEq.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpEq.Location = new System.Drawing.Point(3, 22);
            this.tlpEq.Name = "tlpEq";
            this.tlpEq.RowCount = 5;
            this.tlpEq.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpEq.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpEq.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpEq.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpEq.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpEq.Size = new System.Drawing.Size(220, 308);
            this.tlpEq.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Robot X";
            // 
            // lblRobotXPos
            // 
            this.lblRobotXPos.AutoSize = true;
            this.lblRobotXPos.Location = new System.Drawing.Point(113, 0);
            this.lblRobotXPos.Name = "lblRobotXPos";
            this.lblRobotXPos.Size = new System.Drawing.Size(58, 19);
            this.lblRobotXPos.TabIndex = 1;
            this.lblRobotXPos.Text = "Ready";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "Robot Z";
            // 
            // lblRobotZPos
            // 
            this.lblRobotZPos.AutoSize = true;
            this.lblRobotZPos.Location = new System.Drawing.Point(113, 61);
            this.lblRobotZPos.Name = "lblRobotZPos";
            this.lblRobotZPos.Size = new System.Drawing.Size(31, 19);
            this.lblRobotZPos.TabIndex = 3;
            this.lblRobotZPos.Text = "Up";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 122);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 38);
            this.label3.TabIndex = 4;
            this.label3.Text = "Blade Status";
            // 
            // lblBladeStatus
            // 
            this.lblBladeStatus.AutoSize = true;
            this.lblBladeStatus.Location = new System.Drawing.Point(113, 122);
            this.lblBladeStatus.Name = "lblBladeStatus";
            this.lblBladeStatus.Size = new System.Drawing.Size(73, 19);
            this.lblBladeStatus.TabIndex = 5;
            this.lblBladeStatus.Text = "Forward";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 183);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 38);
            this.label4.TabIndex = 6;
            this.label4.Text = "Wafer Status";
            // 
            // lblWaferStatus
            // 
            this.lblWaferStatus.AutoSize = true;
            this.lblWaferStatus.Location = new System.Drawing.Point(113, 183);
            this.lblWaferStatus.Name = "lblWaferStatus";
            this.lblWaferStatus.Size = new System.Drawing.Size(67, 19);
            this.lblWaferStatus.TabIndex = 7;
            this.lblWaferStatus.Text = "Loaded";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 244);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 19);
            this.label5.TabIndex = 8;
            this.label5.Text = "Door Status";
            // 
            // lblDoorStatus
            // 
            this.lblDoorStatus.AutoSize = true;
            this.lblDoorStatus.Location = new System.Drawing.Point(113, 244);
            this.lblDoorStatus.Name = "lblDoorStatus";
            this.lblDoorStatus.Size = new System.Drawing.Size(63, 19);
            this.lblDoorStatus.TabIndex = 9;
            this.lblDoorStatus.Text = "Closed";
            // 
            // tlpCenter
            // 
            this.tlpCenter.ColumnCount = 1;
            this.tlpCenter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpCenter.Controls.Add(this.gbOverview, 0, 0);
            this.tlpCenter.Controls.Add(this.gbPortStatus, 0, 1);
            this.tlpCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpCenter.Location = new System.Drawing.Point(241, 3);
            this.tlpCenter.Name = "tlpCenter";
            this.tlpCenter.RowCount = 2;
            this.tlpCenter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tlpCenter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tlpCenter.Size = new System.Drawing.Size(710, 678);
            this.tlpCenter.TabIndex = 1;
            // 
            // gbOverview
            // 
            this.gbOverview.Controls.Add(this.pnlOverview);
            this.gbOverview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbOverview.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.gbOverview.Location = new System.Drawing.Point(3, 3);
            this.gbOverview.Name = "gbOverview";
            this.gbOverview.Size = new System.Drawing.Size(704, 468);
            this.gbOverview.TabIndex = 0;
            this.gbOverview.TabStop = false;
            this.gbOverview.Text = "PROCESS OVERVIEW";
            // 
            // pnlOverview
            // 
            this.pnlOverview.BackColor = System.Drawing.Color.White;
            this.pnlOverview.Controls.Add(this.lblAnimProcessStation);
            this.pnlOverview.Controls.Add(this.lblAnimFoupB);
            this.pnlOverview.Controls.Add(this.lblAnimRobot);
            this.pnlOverview.Controls.Add(this.lblAnimFoupA);
            this.pnlOverview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlOverview.Location = new System.Drawing.Point(3, 22);
            this.pnlOverview.Name = "pnlOverview";
            this.pnlOverview.Size = new System.Drawing.Size(698, 443);
            this.pnlOverview.TabIndex = 0;
            //
            // lblAnimProcessStation
            //
            this.lblAnimProcessStation.BackColor = System.Drawing.Color.LightSalmon;
            this.lblAnimProcessStation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAnimProcessStation.Location = new System.Drawing.Point(260, 300);
            this.lblAnimProcessStation.Name = "lblAnimProcessStation";
            this.lblAnimProcessStation.Size = new System.Drawing.Size(180, 80);
            this.lblAnimProcessStation.TabIndex = 3;
            this.lblAnimProcessStation.Text = "Process Station";
            this.lblAnimProcessStation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblAnimFoupB
            //
            this.lblAnimFoupB.BackColor = System.Drawing.Color.LightBlue;
            this.lblAnimFoupB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAnimFoupB.Location = new System.Drawing.Point(550, 150);
            this.lblAnimFoupB.Name = "lblAnimFoupB";
            this.lblAnimFoupB.Size = new System.Drawing.Size(100, 100);
            this.lblAnimFoupB.TabIndex = 2;
            this.lblAnimFoupB.Text = "FOUP B\n\nEmpty";
            this.lblAnimFoupB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblAnimRobot
            //
            this.lblAnimRobot.BackColor = System.Drawing.Color.LightGreen;
            this.lblAnimRobot.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAnimRobot.Location = new System.Drawing.Point(300, 150);
            this.lblAnimRobot.Name = "lblAnimRobot";
            this.lblAnimRobot.Size = new System.Drawing.Size(100, 100);
            this.lblAnimRobot.TabIndex = 1;
            this.lblAnimRobot.Text = "2-Axis Robot\n\n(IDLE)";
            this.lblAnimRobot.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblAnimFoupA
            //
            this.lblAnimFoupA.BackColor = System.Drawing.Color.LightBlue;
            this.lblAnimFoupA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAnimFoupA.Location = new System.Drawing.Point(50, 150);
            this.lblAnimFoupA.Name = "lblAnimFoupA";
            this.lblAnimFoupA.Size = new System.Drawing.Size(100, 100);
            this.lblAnimFoupA.TabIndex = 0;
            this.lblAnimFoupA.Text = "FOUP A\n\nLoaded";
            this.lblAnimFoupA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gbPortStatus
            // 
            this.gbPortStatus.Controls.Add(this.tlpPorts);
            this.gbPortStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbPortStatus.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.gbPortStatus.Location = new System.Drawing.Point(3, 477);
            this.gbPortStatus.Name = "gbPortStatus";
            this.gbPortStatus.Size = new System.Drawing.Size(704, 198);
            this.gbPortStatus.TabIndex = 1;
            this.gbPortStatus.TabStop = false;
            this.gbPortStatus.Text = "PORT STATUS";
            // 
            // tlpPorts
            // 
            this.tlpPorts.ColumnCount = 3;
            this.tlpPorts.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tlpPorts.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tlpPorts.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tlpPorts.Controls.Add(this.pnlPortA, 0, 0);
            this.tlpPorts.Controls.Add(this.pnlPortProc, 1, 0);
            this.tlpPorts.Controls.Add(this.pnlPortB, 2, 0);
            this.tlpPorts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpPorts.Location = new System.Drawing.Point(3, 22);
            this.tlpPorts.Name = "tlpPorts";
            this.tlpPorts.RowCount = 1;
            this.tlpPorts.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPorts.Size = new System.Drawing.Size(698, 173);
            this.tlpPorts.TabIndex = 0;
            // 
            // pnlPortA
            // 
            this.pnlPortA.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlPortA.Controls.Add(this.lblFoupAWafer);
            this.pnlPortA.Controls.Add(this.lblFoupAStatus);
            this.pnlPortA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPortA.Location = new System.Drawing.Point(5, 5);
            this.pnlPortA.Margin = new System.Windows.Forms.Padding(5);
            this.pnlPortA.Name = "pnlPortA";
            this.pnlPortA.Size = new System.Drawing.Size(222, 163);
            this.pnlPortA.TabIndex = 0;
            // 
            // lblFoupAWafer
            // 
            this.lblFoupAWafer.AutoSize = true;
            this.lblFoupAWafer.Location = new System.Drawing.Point(10, 60);
            this.lblFoupAWafer.Name = "lblFoupAWafer";
            this.lblFoupAWafer.Size = new System.Drawing.Size(124, 19);
            this.lblFoupAWafer.TabIndex = 1;
            this.lblFoupAWafer.Text = "Wafer : Loaded";
            // 
            // lblFoupAStatus
            // 
            this.lblFoupAStatus.AutoSize = true;
            this.lblFoupAStatus.Location = new System.Drawing.Point(10, 20);
            this.lblFoupAStatus.Name = "lblFoupAStatus";
            this.lblFoupAStatus.Size = new System.Drawing.Size(121, 19);
            this.lblFoupAStatus.TabIndex = 0;
            this.lblFoupAStatus.Text = "Status : Ready";
            // 
            // pnlPortProc
            // 
            this.pnlPortProc.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlPortProc.Controls.Add(this.lblProcWafer);
            this.pnlPortProc.Controls.Add(this.lblProcStatus);
            this.pnlPortProc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPortProc.Location = new System.Drawing.Point(237, 5);
            this.pnlPortProc.Margin = new System.Windows.Forms.Padding(5);
            this.pnlPortProc.Name = "pnlPortProc";
            this.pnlPortProc.Size = new System.Drawing.Size(222, 163);
            this.pnlPortProc.TabIndex = 1;
            // 
            // lblProcWafer
            // 
            this.lblProcWafer.AutoSize = true;
            this.lblProcWafer.Location = new System.Drawing.Point(10, 60);
            this.lblProcWafer.Name = "lblProcWafer";
            this.lblProcWafer.Size = new System.Drawing.Size(147, 19);
            this.lblProcWafer.TabIndex = 1;
            this.lblProcWafer.Text = "Wafer : In Process";
            // 
            // lblProcStatus
            // 
            this.lblProcStatus.AutoSize = true;
            this.lblProcStatus.Location = new System.Drawing.Point(10, 20);
            this.lblProcStatus.Name = "lblProcStatus";
            this.lblProcStatus.Size = new System.Drawing.Size(138, 19);
            this.lblProcStatus.TabIndex = 0;
            this.lblProcStatus.Text = "Status : Running";
            // 
            // pnlPortB
            // 
            this.pnlPortB.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlPortB.Controls.Add(this.lblFoupBWafer);
            this.pnlPortB.Controls.Add(this.lblFoupBStatus);
            this.pnlPortB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPortB.Location = new System.Drawing.Point(469, 5);
            this.pnlPortB.Margin = new System.Windows.Forms.Padding(5);
            this.pnlPortB.Name = "pnlPortB";
            this.pnlPortB.Size = new System.Drawing.Size(224, 163);
            this.pnlPortB.TabIndex = 2;
            // 
            // lblFoupBWafer
            // 
            this.lblFoupBWafer.AutoSize = true;
            this.lblFoupBWafer.Location = new System.Drawing.Point(10, 60);
            this.lblFoupBWafer.Name = "lblFoupBWafer";
            this.lblFoupBWafer.Size = new System.Drawing.Size(115, 19);
            this.lblFoupBWafer.TabIndex = 1;
            this.lblFoupBWafer.Text = "Wafer : Empty";
            // 
            // lblFoupBStatus
            // 
            this.lblFoupBStatus.AutoSize = true;
            this.lblFoupBStatus.Location = new System.Drawing.Point(10, 20);
            this.lblFoupBStatus.Name = "lblFoupBStatus";
            this.lblFoupBStatus.Size = new System.Drawing.Size(121, 19);
            this.lblFoupBStatus.TabIndex = 0;
            this.lblFoupBStatus.Text = "Status : Ready";
            // 
            // tlpRight
            // 
            this.tlpRight.ColumnCount = 1;
            this.tlpRight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRight.Controls.Add(this.gbOp, 0, 0);
            this.tlpRight.Controls.Add(this.gbSysCtrl, 0, 1);
            this.tlpRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpRight.Location = new System.Drawing.Point(957, 3);
            this.tlpRight.Name = "tlpRight";
            this.tlpRight.RowCount = 2;
            this.tlpRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tlpRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tlpRight.Size = new System.Drawing.Size(234, 678);
            this.tlpRight.TabIndex = 2;
            // 
            // gbOp
            // 
            this.gbOp.Controls.Add(this.btnReset);
            this.gbOp.Controls.Add(this.btnStop);
            this.gbOp.Controls.Add(this.btnStart);
            this.gbOp.Controls.Add(this.btnWaferSet);
            this.gbOp.Controls.Add(this.btnLoad);
            this.gbOp.Controls.Add(this.btnManualCtrl);
            this.gbOp.Controls.Add(this.btnHome);
            this.gbOp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbOp.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.gbOp.Location = new System.Drawing.Point(3, 3);
            this.gbOp.Name = "gbOp";
            this.gbOp.Size = new System.Drawing.Size(228, 366);
            this.gbOp.TabIndex = 0;
            this.gbOp.TabStop = false;
            this.gbOp.Text = "OPERATION";
            // 
            // btnReset
            //
            this.btnReset.Location = new System.Drawing.Point(14, 305);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(200, 45);
            this.btnReset.TabIndex = 5;
            this.btnReset.Text = "RESET";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            //
            // btnManualCtrl
            // 
            this.btnManualCtrl.Location = new System.Drawing.Point(14, 360);
            this.btnManualCtrl.Name = "btnManualCtrl";
            this.btnManualCtrl.Size = new System.Drawing.Size(200, 45);
            this.btnManualCtrl.TabIndex = 6;
            this.btnManualCtrl.Text = "수동 조작 (Form1)";
            this.btnManualCtrl.UseVisualStyleBackColor = true;
            this.btnManualCtrl.Click += new System.EventHandler(this.btnManualCtrl_Click);
            // 
            // btnStop
            //
            this.btnStop.BackColor = System.Drawing.Color.LightCoral;
            this.btnStop.Location = new System.Drawing.Point(14, 250);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(200, 45);
            this.btnStop.TabIndex = 4;
            this.btnStop.Text = "STOP";
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            //
            // btnStart
            //
            this.btnStart.BackColor = System.Drawing.Color.LightGreen;
            this.btnStart.Location = new System.Drawing.Point(14, 195);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(200, 45);
            this.btnStart.TabIndex = 3;
            this.btnStart.Text = "START";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnWaferSet
            // 
            this.btnWaferSet.Location = new System.Drawing.Point(14, 140);
            this.btnWaferSet.Name = "btnWaferSet";
            this.btnWaferSet.Size = new System.Drawing.Size(200, 45);
            this.btnWaferSet.TabIndex = 2;
            this.btnWaferSet.Text = "WAFER SET";
            this.btnWaferSet.UseVisualStyleBackColor = true;
            this.btnWaferSet.Click += new System.EventHandler(this.btnWaferSet_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(14, 85);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(200, 45);
            this.btnLoad.TabIndex = 1;
            this.btnLoad.Text = "LOAD";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnHome
            // 
            this.btnHome.Location = new System.Drawing.Point(14, 30);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(200, 45);
            this.btnHome.TabIndex = 0;
            this.btnHome.Text = "HOME";
            this.btnHome.UseVisualStyleBackColor = true;
            this.btnHome.Click += new System.EventHandler(this.btnHome_Click);
            // 
            // gbSysCtrl
            // 
            this.gbSysCtrl.Controls.Add(this.btnAuto);
            this.gbSysCtrl.Controls.Add(this.btnManual);
            this.gbSysCtrl.Controls.Add(this.btnAlarmReset);
            this.gbSysCtrl.Controls.Add(this.btnSysMgr);
            this.gbSysCtrl.Controls.Add(this.btnExit);
            this.gbSysCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbSysCtrl.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.gbSysCtrl.Location = new System.Drawing.Point(3, 375);
            this.gbSysCtrl.Name = "gbSysCtrl";
            this.gbSysCtrl.Size = new System.Drawing.Size(228, 300);
            this.gbSysCtrl.TabIndex = 1;
            this.gbSysCtrl.TabStop = false;
            this.gbSysCtrl.Text = "SYSTEM CONTROL";
            // 
            // btnAuto
            // 
            this.btnAuto.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnAuto.ForeColor = System.Drawing.Color.White;
            this.btnAuto.Location = new System.Drawing.Point(14, 30);
            this.btnAuto.Name = "btnAuto";
            this.btnAuto.Size = new System.Drawing.Size(95, 45);
            this.btnAuto.TabIndex = 0;
            this.btnAuto.Text = "AUTO";
            this.btnAuto.UseVisualStyleBackColor = false;
            this.btnAuto.Click += new System.EventHandler(this.btnAuto_Click);
            //
            // btnManual
            //
            this.btnManual.Location = new System.Drawing.Point(119, 30);
            this.btnManual.Name = "btnManual";
            this.btnManual.Size = new System.Drawing.Size(95, 45);
            this.btnManual.TabIndex = 1;
            this.btnManual.Text = "MANUAL";
            this.btnManual.UseVisualStyleBackColor = true;
            this.btnManual.Click += new System.EventHandler(this.btnManual_Click);
            // 
            // btnAlarmReset
            // 
            this.btnAlarmReset.Location = new System.Drawing.Point(14, 90);
            this.btnAlarmReset.Name = "btnAlarmReset";
            this.btnAlarmReset.Size = new System.Drawing.Size(200, 45);
            this.btnAlarmReset.TabIndex = 2;
            this.btnAlarmReset.Text = "ALARM RESET";
            this.btnAlarmReset.UseVisualStyleBackColor = true;
            // 
            // btnSysMgr
            // 
            this.btnSysMgr.Location = new System.Drawing.Point(14, 150);
            this.btnSysMgr.Name = "btnSysMgr";
            this.btnSysMgr.Size = new System.Drawing.Size(200, 45);
            this.btnSysMgr.TabIndex = 3;
            this.btnSysMgr.Text = "SYSTEM MANAGER";
            this.btnSysMgr.UseVisualStyleBackColor = true;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(14, 210);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(200, 45);
            this.btnExit.TabIndex = 4;
            this.btnExit.Text = "EXIT SYSTEM";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // tlpFooter
            // 
            this.tlpFooter.ColumnCount = 3;
            this.tlpFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tlpFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tlpFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tlpFooter.Controls.Add(this.lblFooterLeft, 0, 0);
            this.tlpFooter.Controls.Add(this.lblFooterCenter, 1, 0);
            this.tlpFooter.Controls.Add(this.lblFooterRight, 2, 0);
            this.tlpFooter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpFooter.Location = new System.Drawing.Point(3, 763);
            this.tlpFooter.Name = "tlpFooter";
            this.tlpFooter.RowCount = 1;
            this.tlpFooter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpFooter.Size = new System.Drawing.Size(1194, 34);
            this.tlpFooter.TabIndex = 2;
            // 
            // lblFooterLeft
            // 
            this.lblFooterLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFooterLeft.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.lblFooterLeft.Location = new System.Drawing.Point(3, 0);
            this.lblFooterLeft.Name = "lblFooterLeft";
            this.lblFooterLeft.Size = new System.Drawing.Size(392, 34);
            this.lblFooterLeft.TabIndex = 0;
            this.lblFooterLeft.Text = "EQU-KIT HMI";
            this.lblFooterLeft.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblFooterCenter
            // 
            this.lblFooterCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFooterCenter.Font = new System.Drawing.Font("Arial", 10F);
            this.lblFooterCenter.Location = new System.Drawing.Point(401, 0);
            this.lblFooterCenter.Name = "lblFooterCenter";
            this.lblFooterCenter.Size = new System.Drawing.Size(392, 34);
            this.lblFooterCenter.TabIndex = 1;
            this.lblFooterCenter.Text = "SEMI E95 / HSMS Compliant";
            this.lblFooterCenter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblFooterRight
            // 
            this.lblFooterRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFooterRight.Font = new System.Drawing.Font("Arial", 10F);
            this.lblFooterRight.Location = new System.Drawing.Point(799, 0);
            this.lblFooterRight.Name = "lblFooterRight";
            this.lblFooterRight.Size = new System.Drawing.Size(392, 34);
            this.lblFooterRight.TabIndex = 2;
            this.lblFooterRight.Text = "v1.0.0";
            this.lblFooterRight.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // SemiE95View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpMain);
            this.Name = "SemiE95View";
            this.Size = new System.Drawing.Size(1200, 800);
            this.tlpMain.ResumeLayout(false);
            this.tlpHeader.ResumeLayout(false);
            this.pnlHeaderBtns.ResumeLayout(false);
            this.tlpContent.ResumeLayout(false);
            this.tlpLeft.ResumeLayout(false);
            this.gbStatus.ResumeLayout(false);
            this.tlpStatus.ResumeLayout(false);
            this.gbEqStatus.ResumeLayout(false);
            this.tlpEq.ResumeLayout(false);
            this.tlpEq.PerformLayout();
            this.tlpCenter.ResumeLayout(false);
            this.gbOverview.ResumeLayout(false);
            this.pnlOverview.ResumeLayout(false);
            this.gbPortStatus.ResumeLayout(false);
            this.tlpPorts.ResumeLayout(false);
            this.pnlPortA.ResumeLayout(false);
            this.pnlPortA.PerformLayout();
            this.pnlPortProc.ResumeLayout(false);
            this.pnlPortProc.PerformLayout();
            this.pnlPortB.ResumeLayout(false);
            this.pnlPortB.PerformLayout();
            this.tlpRight.ResumeLayout(false);
            this.gbOp.ResumeLayout(false);
            this.gbSysCtrl.ResumeLayout(false);
            this.tlpFooter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer uiTimer;
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.TableLayoutPanel tlpHeader;
        private System.Windows.Forms.Label lblLogo;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Label lblSystem;
        private System.Windows.Forms.Label lblDateTime;
        private System.Windows.Forms.Panel pnlHeaderBtns;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnAlarmHeader;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.TableLayoutPanel tlpContent;
        private System.Windows.Forms.TableLayoutPanel tlpLeft;
        private System.Windows.Forms.GroupBox gbStatus;
        private System.Windows.Forms.TableLayoutPanel tlpStatus;
        private System.Windows.Forms.Label lblStatusBig;
        private System.Windows.Forms.Label lblStatusIndIdle;
        private System.Windows.Forms.Label lblStatusIndRunning;
        private System.Windows.Forms.Label lblStatusIndComplete;
        private System.Windows.Forms.Label lblStatusIndAlarm;
        private System.Windows.Forms.GroupBox gbEqStatus;
        private System.Windows.Forms.TableLayoutPanel tlpEq;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblRobotXPos;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblRobotZPos;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblBladeStatus;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblWaferStatus;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblDoorStatus;
        private System.Windows.Forms.TableLayoutPanel tlpCenter;
        private System.Windows.Forms.GroupBox gbOverview;
        private System.Windows.Forms.Panel pnlOverview;
        private System.Windows.Forms.Label lblAnimFoupA;
        private System.Windows.Forms.Label lblAnimRobot;
        private System.Windows.Forms.Label lblAnimFoupB;
        private System.Windows.Forms.Label lblAnimProcessStation;
        private System.Windows.Forms.GroupBox gbPortStatus;
        private System.Windows.Forms.TableLayoutPanel tlpPorts;
        private System.Windows.Forms.Panel pnlPortA;
        private System.Windows.Forms.Label lblFoupAStatus;
        private System.Windows.Forms.Label lblFoupAWafer;
        private System.Windows.Forms.Panel pnlPortProc;
        private System.Windows.Forms.Label lblProcStatus;
        private System.Windows.Forms.Label lblProcWafer;
        private System.Windows.Forms.Panel pnlPortB;
        private System.Windows.Forms.Label lblFoupBStatus;
        private System.Windows.Forms.Label lblFoupBWafer;
        private System.Windows.Forms.TableLayoutPanel tlpRight;
        private System.Windows.Forms.GroupBox gbOp;
        private System.Windows.Forms.Button btnHome;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnWaferSet;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnManualCtrl;
        private System.Windows.Forms.GroupBox gbSysCtrl;
        private System.Windows.Forms.Button btnAuto;
        private System.Windows.Forms.Button btnManual;
        private System.Windows.Forms.Button btnAlarmReset;
        private System.Windows.Forms.Button btnSysMgr;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.TableLayoutPanel tlpFooter;
        private System.Windows.Forms.Label lblFooterLeft;
        private System.Windows.Forms.Label lblFooterCenter;
        private System.Windows.Forms.Label lblFooterRight;
    }
}
