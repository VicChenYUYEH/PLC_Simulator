namespace WinLFT_Test
{
    partial class frmMain
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.tmMainProc = new System.Windows.Forms.Timer(this.components);
            this.lblConn_PLC_Name = new System.Windows.Forms.Label();
            this.lblConn_PLC1 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.picbxMirle = new System.Windows.Forms.PictureBox();
            this.tbpProcList = new System.Windows.Forms.TabPage();
            this.cboStageSelect = new System.Windows.Forms.ComboBox();
            this.lblStage = new System.Windows.Forms.Label();
            this.btnTRU2PresentOnOff = new System.Windows.Forms.Button();
            this.btnTRU1PresentOnOff = new System.Windows.Forms.Button();
            this.btnLFTPresentOnOff = new System.Windows.Forms.Button();
            this.btnPresentOn = new System.Windows.Forms.Button();
            this.btnResetPort = new System.Windows.Forms.Button();
            this.btnPortAlarm = new System.Windows.Forms.Button();
            this.btnTRU1Interlock = new System.Windows.Forms.Button();
            this.btnTRU2Interlock = new System.Windows.Forms.Button();
            this.btnLFTInterlock = new System.Windows.Forms.Button();
            this.btnTRU1Alarm = new System.Windows.Forms.Button();
            this.btnTRU2Alarm = new System.Windows.Forms.Button();
            this.btnLFTAlarm = new System.Windows.Forms.Button();
            this.cboSourceDest = new System.Windows.Forms.ComboBox();
            this.btnInitLFT = new System.Windows.Forms.Button();
            this.btnInitTRU1 = new System.Windows.Forms.Button();
            this.button31 = new System.Windows.Forms.Button();
            this.lblCSTID = new System.Windows.Forms.Label();
            this.btnInitTRU2 = new System.Windows.Forms.Button();
            this.lblPortSelect = new System.Windows.Forms.Label();
            this.cboPortSelect = new System.Windows.Forms.ComboBox();
            this.btnModeChange = new System.Windows.Forms.Button();
            this.btnInitAll = new System.Windows.Forms.Button();
            this.btnTRU1Reset = new System.Windows.Forms.Button();
            this.btnTRU2Reset = new System.Windows.Forms.Button();
            this.btnLFTReset = new System.Windows.Forms.Button();
            this.btnRemoved = new System.Windows.Forms.Button();
            this.picPortFunc = new System.Windows.Forms.PictureBox();
            this.picEQInit = new System.Windows.Forms.PictureBox();
            this.picProcSelect = new System.Windows.Forms.PictureBox();
            this.btnWaitOut = new System.Windows.Forms.Button();
            this.cboWhere = new System.Windows.Forms.ComboBox();
            this.rdbtnAbnormal = new System.Windows.Forms.RadioButton();
            this.rdbtnNormal = new System.Windows.Forms.RadioButton();
            this.txtCSTID = new System.Windows.Forms.TextBox();
            this.cboAbnormal = new System.Windows.Forms.ComboBox();
            this.btnBCRReadDown = new System.Windows.Forms.Button();
            this.tbpTRU2 = new System.Windows.Forms.TabPage();
            this.txtTRU2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TRU2 = new PLC.TRU();
            this.tbpTRU1 = new System.Windows.Forms.TabPage();
            this.txtTRU1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TRU1 = new PLC.TRU();
            this.tbpLFT = new System.Windows.Forms.TabPage();
            this.txtLFT = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.LFT1 = new PLC.LFT();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.chkBxTM2Word = new System.Windows.Forms.CheckBox();
            this.panel5.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picbxMirle)).BeginInit();
            this.tbpProcList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPortFunc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEQInit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picProcSelect)).BeginInit();
            this.tbpTRU2.SuspendLayout();
            this.tbpTRU1.SuspendLayout();
            this.tbpLFT.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tmMainProc
            // 
            this.tmMainProc.Interval = 300;
            this.tmMainProc.Tick += new System.EventHandler(this.tmMainProc_Tick);
            // 
            // lblConn_PLC_Name
            // 
            this.lblConn_PLC_Name.BackColor = System.Drawing.SystemColors.Control;
            this.lblConn_PLC_Name.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConn_PLC_Name.ForeColor = System.Drawing.Color.Black;
            this.lblConn_PLC_Name.Location = new System.Drawing.Point(435, 18);
            this.lblConn_PLC_Name.Name = "lblConn_PLC_Name";
            this.lblConn_PLC_Name.Size = new System.Drawing.Size(122, 18);
            this.lblConn_PLC_Name.TabIndex = 11;
            this.lblConn_PLC_Name.Text = "PLC Connection";
            this.lblConn_PLC_Name.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblConn_PLC1
            // 
            this.lblConn_PLC1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblConn_PLC1.Location = new System.Drawing.Point(413, 19);
            this.lblConn_PLC1.Name = "lblConn_PLC1";
            this.lblConn_PLC1.Size = new System.Drawing.Size(16, 16);
            this.lblConn_PLC1.TabIndex = 10;
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.textBox1);
            this.panel5.Controls.Add(this.label9);
            this.panel5.Controls.Add(this.btnStart);
            this.panel5.Location = new System.Drawing.Point(96, 7);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(297, 39);
            this.panel5.TabIndex = 113;
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(165, 7);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(38, 23);
            this.textBox1.TabIndex = 108;
            this.textBox1.Text = "1";
            // 
            // label9
            // 
            this.label9.CausesValidation = false;
            this.label9.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(11, 8);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(148, 20);
            this.label9.TabIndex = 107;
            this.label9.Text = "PLC StationNumber";
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.Location = new System.Drawing.Point(223, 5);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(58, 25);
            this.btnStart.TabIndex = 106;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.picbxMirle);
            this.panel3.Location = new System.Drawing.Point(0, -1);
            this.panel3.Margin = new System.Windows.Forms.Padding(2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(92, 46);
            this.panel3.TabIndex = 179;
            // 
            // picbxMirle
            // 
            this.picbxMirle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picbxMirle.Image = ((System.Drawing.Image)(resources.GetObject("picbxMirle.Image")));
            this.picbxMirle.Location = new System.Drawing.Point(0, 0);
            this.picbxMirle.Margin = new System.Windows.Forms.Padding(1);
            this.picbxMirle.Name = "picbxMirle";
            this.picbxMirle.Size = new System.Drawing.Size(92, 46);
            this.picbxMirle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picbxMirle.TabIndex = 7;
            this.picbxMirle.TabStop = false;
            // 
            // tbpProcList
            // 
            this.tbpProcList.Controls.Add(this.cboStageSelect);
            this.tbpProcList.Controls.Add(this.lblStage);
            this.tbpProcList.Controls.Add(this.btnTRU2PresentOnOff);
            this.tbpProcList.Controls.Add(this.btnTRU1PresentOnOff);
            this.tbpProcList.Controls.Add(this.btnLFTPresentOnOff);
            this.tbpProcList.Controls.Add(this.btnPresentOn);
            this.tbpProcList.Controls.Add(this.btnResetPort);
            this.tbpProcList.Controls.Add(this.btnPortAlarm);
            this.tbpProcList.Controls.Add(this.btnTRU1Interlock);
            this.tbpProcList.Controls.Add(this.btnTRU2Interlock);
            this.tbpProcList.Controls.Add(this.btnLFTInterlock);
            this.tbpProcList.Controls.Add(this.btnTRU1Alarm);
            this.tbpProcList.Controls.Add(this.btnTRU2Alarm);
            this.tbpProcList.Controls.Add(this.btnLFTAlarm);
            this.tbpProcList.Controls.Add(this.cboSourceDest);
            this.tbpProcList.Controls.Add(this.btnInitLFT);
            this.tbpProcList.Controls.Add(this.btnInitTRU1);
            this.tbpProcList.Controls.Add(this.button31);
            this.tbpProcList.Controls.Add(this.lblCSTID);
            this.tbpProcList.Controls.Add(this.btnInitTRU2);
            this.tbpProcList.Controls.Add(this.lblPortSelect);
            this.tbpProcList.Controls.Add(this.cboPortSelect);
            this.tbpProcList.Controls.Add(this.btnModeChange);
            this.tbpProcList.Controls.Add(this.btnInitAll);
            this.tbpProcList.Controls.Add(this.btnTRU1Reset);
            this.tbpProcList.Controls.Add(this.btnTRU2Reset);
            this.tbpProcList.Controls.Add(this.btnLFTReset);
            this.tbpProcList.Controls.Add(this.btnRemoved);
            this.tbpProcList.Controls.Add(this.picPortFunc);
            this.tbpProcList.Controls.Add(this.picEQInit);
            this.tbpProcList.Controls.Add(this.picProcSelect);
            this.tbpProcList.Controls.Add(this.btnWaitOut);
            this.tbpProcList.Controls.Add(this.cboWhere);
            this.tbpProcList.Controls.Add(this.rdbtnAbnormal);
            this.tbpProcList.Controls.Add(this.rdbtnNormal);
            this.tbpProcList.Controls.Add(this.txtCSTID);
            this.tbpProcList.Controls.Add(this.cboAbnormal);
            this.tbpProcList.Controls.Add(this.btnBCRReadDown);
            this.tbpProcList.Location = new System.Drawing.Point(4, 22);
            this.tbpProcList.Name = "tbpProcList";
            this.tbpProcList.Padding = new System.Windows.Forms.Padding(3);
            this.tbpProcList.Size = new System.Drawing.Size(872, 409);
            this.tbpProcList.TabIndex = 2;
            this.tbpProcList.Text = "Process List";
            this.tbpProcList.UseVisualStyleBackColor = true;
            // 
            // cboStageSelect
            // 
            this.cboStageSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStageSelect.Font = new System.Drawing.Font("微軟正黑體", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cboStageSelect.FormattingEnabled = true;
            this.cboStageSelect.Location = new System.Drawing.Point(378, 226);
            this.cboStageSelect.Margin = new System.Windows.Forms.Padding(2);
            this.cboStageSelect.Name = "cboStageSelect";
            this.cboStageSelect.Size = new System.Drawing.Size(142, 26);
            this.cboStageSelect.TabIndex = 198;
            // 
            // lblStage
            // 
            this.lblStage.AutoSize = true;
            this.lblStage.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStage.Location = new System.Drawing.Point(269, 231);
            this.lblStage.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblStage.Name = "lblStage";
            this.lblStage.Size = new System.Drawing.Size(92, 19);
            this.lblStage.TabIndex = 197;
            this.lblStage.Text = "Stage Select";
            // 
            // btnTRU2PresentOnOff
            // 
            this.btnTRU2PresentOnOff.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold);
            this.btnTRU2PresentOnOff.Location = new System.Drawing.Point(394, 364);
            this.btnTRU2PresentOnOff.Name = "btnTRU2PresentOnOff";
            this.btnTRU2PresentOnOff.Size = new System.Drawing.Size(126, 31);
            this.btnTRU2PresentOnOff.TabIndex = 196;
            this.btnTRU2PresentOnOff.Text = "TRU2 荷有 on/off";
            this.btnTRU2PresentOnOff.UseVisualStyleBackColor = true;
            this.btnTRU2PresentOnOff.Click += new System.EventHandler(this.btnTRU2PresentOnOff_Click);
            // 
            // btnTRU1PresentOnOff
            // 
            this.btnTRU1PresentOnOff.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold);
            this.btnTRU1PresentOnOff.Location = new System.Drawing.Point(262, 364);
            this.btnTRU1PresentOnOff.Name = "btnTRU1PresentOnOff";
            this.btnTRU1PresentOnOff.Size = new System.Drawing.Size(126, 31);
            this.btnTRU1PresentOnOff.TabIndex = 195;
            this.btnTRU1PresentOnOff.Text = "TRU1 荷有 on/off";
            this.btnTRU1PresentOnOff.UseVisualStyleBackColor = true;
            this.btnTRU1PresentOnOff.Click += new System.EventHandler(this.btnTRU1PresentOnOff_Click);
            // 
            // btnLFTPresentOnOff
            // 
            this.btnLFTPresentOnOff.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold);
            this.btnLFTPresentOnOff.Location = new System.Drawing.Point(262, 322);
            this.btnLFTPresentOnOff.Name = "btnLFTPresentOnOff";
            this.btnLFTPresentOnOff.Size = new System.Drawing.Size(126, 31);
            this.btnLFTPresentOnOff.TabIndex = 194;
            this.btnLFTPresentOnOff.Text = "LFT 荷有 on/off";
            this.btnLFTPresentOnOff.UseVisualStyleBackColor = true;
            this.btnLFTPresentOnOff.Click += new System.EventHandler(this.btnLFTPresentOnOff_Click);
            // 
            // btnPresentOn
            // 
            this.btnPresentOn.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold);
            this.btnPresentOn.Location = new System.Drawing.Point(394, 273);
            this.btnPresentOn.Name = "btnPresentOn";
            this.btnPresentOn.Size = new System.Drawing.Size(126, 31);
            this.btnPresentOn.TabIndex = 193;
            this.btnPresentOn.Text = "Port 荷有 on/off";
            this.btnPresentOn.UseVisualStyleBackColor = true;
            this.btnPresentOn.Click += new System.EventHandler(this.btnPresentOn_Click);
            // 
            // btnResetPort
            // 
            this.btnResetPort.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnResetPort.Location = new System.Drawing.Point(716, 353);
            this.btnResetPort.Name = "btnResetPort";
            this.btnResetPort.Size = new System.Drawing.Size(109, 29);
            this.btnResetPort.TabIndex = 188;
            this.btnResetPort.Text = "Reset Port";
            this.btnResetPort.UseVisualStyleBackColor = true;
            this.btnResetPort.Click += new System.EventHandler(this.btnResetPort_Click);
            // 
            // btnPortAlarm
            // 
            this.btnPortAlarm.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPortAlarm.Location = new System.Drawing.Point(601, 353);
            this.btnPortAlarm.Name = "btnPortAlarm";
            this.btnPortAlarm.Size = new System.Drawing.Size(109, 29);
            this.btnPortAlarm.TabIndex = 187;
            this.btnPortAlarm.Text = "Port Alarm";
            this.btnPortAlarm.UseVisualStyleBackColor = true;
            this.btnPortAlarm.Click += new System.EventHandler(this.btnPortAlarm_Click);
            // 
            // btnTRU1Interlock
            // 
            this.btnTRU1Interlock.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTRU1Interlock.Location = new System.Drawing.Point(666, 261);
            this.btnTRU1Interlock.Name = "btnTRU1Interlock";
            this.btnTRU1Interlock.Size = new System.Drawing.Size(87, 29);
            this.btnTRU1Interlock.TabIndex = 186;
            this.btnTRU1Interlock.Text = "Interlock";
            this.btnTRU1Interlock.UseVisualStyleBackColor = true;
            this.btnTRU1Interlock.Click += new System.EventHandler(this.btnTRU1Interlock_Click);
            // 
            // btnTRU2Interlock
            // 
            this.btnTRU2Interlock.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTRU2Interlock.Location = new System.Drawing.Point(667, 299);
            this.btnTRU2Interlock.Name = "btnTRU2Interlock";
            this.btnTRU2Interlock.Size = new System.Drawing.Size(87, 29);
            this.btnTRU2Interlock.TabIndex = 185;
            this.btnTRU2Interlock.Text = "Interlock";
            this.btnTRU2Interlock.UseVisualStyleBackColor = true;
            this.btnTRU2Interlock.Click += new System.EventHandler(this.btnTRU2Interlock_Click);
            // 
            // btnLFTInterlock
            // 
            this.btnLFTInterlock.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLFTInterlock.Location = new System.Drawing.Point(665, 224);
            this.btnLFTInterlock.Name = "btnLFTInterlock";
            this.btnLFTInterlock.Size = new System.Drawing.Size(88, 29);
            this.btnLFTInterlock.TabIndex = 184;
            this.btnLFTInterlock.Text = "Interlock";
            this.btnLFTInterlock.UseVisualStyleBackColor = true;
            this.btnLFTInterlock.Click += new System.EventHandler(this.btnLFTInterlock_Click);
            // 
            // btnTRU1Alarm
            // 
            this.btnTRU1Alarm.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTRU1Alarm.Location = new System.Drawing.Point(565, 261);
            this.btnTRU1Alarm.Name = "btnTRU1Alarm";
            this.btnTRU1Alarm.Size = new System.Drawing.Size(108, 29);
            this.btnTRU1Alarm.TabIndex = 183;
            this.btnTRU1Alarm.Text = "TRU1 Alarm";
            this.btnTRU1Alarm.UseVisualStyleBackColor = true;
            this.btnTRU1Alarm.Click += new System.EventHandler(this.btnTRU1Alarm_Click);
            // 
            // btnTRU2Alarm
            // 
            this.btnTRU2Alarm.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTRU2Alarm.Location = new System.Drawing.Point(565, 299);
            this.btnTRU2Alarm.Name = "btnTRU2Alarm";
            this.btnTRU2Alarm.Size = new System.Drawing.Size(108, 29);
            this.btnTRU2Alarm.TabIndex = 182;
            this.btnTRU2Alarm.Text = "TRU2 Alarm";
            this.btnTRU2Alarm.UseVisualStyleBackColor = true;
            this.btnTRU2Alarm.Click += new System.EventHandler(this.btnTRU2Alarm_Click);
            // 
            // btnLFTAlarm
            // 
            this.btnLFTAlarm.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLFTAlarm.Location = new System.Drawing.Point(564, 224);
            this.btnLFTAlarm.Name = "btnLFTAlarm";
            this.btnLFTAlarm.Size = new System.Drawing.Size(109, 29);
            this.btnLFTAlarm.TabIndex = 181;
            this.btnLFTAlarm.Text = "LFT Alarm";
            this.btnLFTAlarm.UseVisualStyleBackColor = true;
            this.btnLFTAlarm.Click += new System.EventHandler(this.btnLFTAlarm_Click);
            // 
            // cboSourceDest
            // 
            this.cboSourceDest.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSourceDest.Font = new System.Drawing.Font("微軟正黑體", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cboSourceDest.FormattingEnabled = true;
            this.cboSourceDest.Items.AddRange(new object[] {
            "Source",
            "Dest."});
            this.cboSourceDest.Location = new System.Drawing.Point(410, 86);
            this.cboSourceDest.Margin = new System.Windows.Forms.Padding(2);
            this.cboSourceDest.Name = "cboSourceDest";
            this.cboSourceDest.Size = new System.Drawing.Size(71, 26);
            this.cboSourceDest.TabIndex = 180;
            this.cboSourceDest.Visible = false;
            // 
            // btnInitLFT
            // 
            this.btnInitLFT.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInitLFT.Location = new System.Drawing.Point(632, 62);
            this.btnInitLFT.Name = "btnInitLFT";
            this.btnInitLFT.Size = new System.Drawing.Size(103, 32);
            this.btnInitLFT.TabIndex = 101;
            this.btnInitLFT.Text = "Initial LFT";
            this.btnInitLFT.UseVisualStyleBackColor = true;
            this.btnInitLFT.Click += new System.EventHandler(this.btnInitLFT_Click);
            // 
            // btnInitTRU1
            // 
            this.btnInitTRU1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInitTRU1.Location = new System.Drawing.Point(750, 62);
            this.btnInitTRU1.Name = "btnInitTRU1";
            this.btnInitTRU1.Size = new System.Drawing.Size(102, 32);
            this.btnInitTRU1.TabIndex = 102;
            this.btnInitTRU1.Text = "Initial TRU 1";
            this.btnInitTRU1.UseVisualStyleBackColor = true;
            this.btnInitTRU1.Click += new System.EventHandler(this.btnInitTRU1_Click);
            // 
            // button31
            // 
            this.button31.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.button31.Location = new System.Drawing.Point(632, 102);
            this.button31.Name = "button31";
            this.button31.Size = new System.Drawing.Size(103, 30);
            this.button31.TabIndex = 179;
            this.button31.Text = "Initial CONV";
            this.button31.UseVisualStyleBackColor = true;
            this.button31.Click += new System.EventHandler(this.InitCONV_Click);
            // 
            // lblCSTID
            // 
            this.lblCSTID.AutoSize = true;
            this.lblCSTID.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCSTID.Location = new System.Drawing.Point(16, 273);
            this.lblCSTID.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCSTID.Name = "lblCSTID";
            this.lblCSTID.Size = new System.Drawing.Size(73, 19);
            this.lblCSTID.TabIndex = 132;
            this.lblCSTID.Text = "Carrier ID";
            // 
            // btnInitTRU2
            // 
            this.btnInitTRU2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInitTRU2.Location = new System.Drawing.Point(750, 100);
            this.btnInitTRU2.Name = "btnInitTRU2";
            this.btnInitTRU2.Size = new System.Drawing.Size(102, 32);
            this.btnInitTRU2.TabIndex = 103;
            this.btnInitTRU2.Text = "Initial TRU 2";
            this.btnInitTRU2.UseVisualStyleBackColor = true;
            this.btnInitTRU2.Click += new System.EventHandler(this.btnInitTRU2_Click);
            // 
            // lblPortSelect
            // 
            this.lblPortSelect.AutoSize = true;
            this.lblPortSelect.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPortSelect.Location = new System.Drawing.Point(14, 228);
            this.lblPortSelect.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPortSelect.Name = "lblPortSelect";
            this.lblPortSelect.Size = new System.Drawing.Size(84, 19);
            this.lblPortSelect.TabIndex = 131;
            this.lblPortSelect.Text = "Port Select";
            // 
            // cboPortSelect
            // 
            this.cboPortSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPortSelect.Font = new System.Drawing.Font("微軟正黑體", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cboPortSelect.FormattingEnabled = true;
            this.cboPortSelect.Location = new System.Drawing.Point(110, 226);
            this.cboPortSelect.Margin = new System.Windows.Forms.Padding(2);
            this.cboPortSelect.Name = "cboPortSelect";
            this.cboPortSelect.Size = new System.Drawing.Size(142, 26);
            this.cboPortSelect.TabIndex = 130;
            this.cboPortSelect.SelectedIndexChanged += new System.EventHandler(this.cboPortSelect_SelectedIndexChanged);
            // 
            // btnModeChange
            // 
            this.btnModeChange.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModeChange.Location = new System.Drawing.Point(137, 366);
            this.btnModeChange.Name = "btnModeChange";
            this.btnModeChange.Size = new System.Drawing.Size(100, 25);
            this.btnModeChange.TabIndex = 125;
            this.btnModeChange.Text = "In<>Out";
            this.btnModeChange.UseVisualStyleBackColor = true;
            this.btnModeChange.Click += new System.EventHandler(this.btnModeChange_Click_1);
            // 
            // btnInitAll
            // 
            this.btnInitAll.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInitAll.Location = new System.Drawing.Point(690, 11);
            this.btnInitAll.Name = "btnInitAll";
            this.btnInitAll.Size = new System.Drawing.Size(114, 32);
            this.btnInitAll.TabIndex = 100;
            this.btnInitAll.Text = "Initial All";
            this.btnInitAll.UseVisualStyleBackColor = true;
            this.btnInitAll.Click += new System.EventHandler(this.btnInitAll_Click);
            // 
            // btnTRU1Reset
            // 
            this.btnTRU1Reset.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTRU1Reset.Location = new System.Drawing.Point(750, 299);
            this.btnTRU1Reset.Name = "btnTRU1Reset";
            this.btnTRU1Reset.Size = new System.Drawing.Size(99, 29);
            this.btnTRU1Reset.TabIndex = 129;
            this.btnTRU1Reset.Text = "TRU1Reset";
            this.btnTRU1Reset.UseVisualStyleBackColor = true;
            this.btnTRU1Reset.Click += new System.EventHandler(this.btnTRU1Reset_Click);
            // 
            // btnTRU2Reset
            // 
            this.btnTRU2Reset.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTRU2Reset.Location = new System.Drawing.Point(749, 261);
            this.btnTRU2Reset.Name = "btnTRU2Reset";
            this.btnTRU2Reset.Size = new System.Drawing.Size(99, 29);
            this.btnTRU2Reset.TabIndex = 128;
            this.btnTRU2Reset.Text = "TRU2Reset";
            this.btnTRU2Reset.UseVisualStyleBackColor = true;
            this.btnTRU2Reset.Click += new System.EventHandler(this.btnTRU2Reset_Click);
            // 
            // btnLFTReset
            // 
            this.btnLFTReset.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLFTReset.Location = new System.Drawing.Point(749, 226);
            this.btnLFTReset.Name = "btnLFTReset";
            this.btnLFTReset.Size = new System.Drawing.Size(99, 29);
            this.btnLFTReset.TabIndex = 127;
            this.btnLFTReset.Text = "LFT Reset";
            this.btnLFTReset.UseVisualStyleBackColor = true;
            this.btnLFTReset.Click += new System.EventHandler(this.btnLFTReset_Click);
            // 
            // btnRemoved
            // 
            this.btnRemoved.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoved.Location = new System.Drawing.Point(20, 366);
            this.btnRemoved.Name = "btnRemoved";
            this.btnRemoved.Size = new System.Drawing.Size(97, 25);
            this.btnRemoved.TabIndex = 121;
            this.btnRemoved.Text = "Removed";
            this.btnRemoved.UseVisualStyleBackColor = true;
            this.btnRemoved.Click += new System.EventHandler(this.btnRemoved_Click);
            // 
            // picPortFunc
            // 
            this.picPortFunc.Image = ((System.Drawing.Image)(resources.GetObject("picPortFunc.Image")));
            this.picPortFunc.Location = new System.Drawing.Point(2, 163);
            this.picPortFunc.Margin = new System.Windows.Forms.Padding(2);
            this.picPortFunc.Name = "picPortFunc";
            this.picPortFunc.Size = new System.Drawing.Size(151, 47);
            this.picPortFunc.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picPortFunc.TabIndex = 126;
            this.picPortFunc.TabStop = false;
            // 
            // picEQInit
            // 
            this.picEQInit.Image = ((System.Drawing.Image)(resources.GetObject("picEQInit.Image")));
            this.picEQInit.Location = new System.Drawing.Point(485, 5);
            this.picEQInit.Margin = new System.Windows.Forms.Padding(2);
            this.picEQInit.Name = "picEQInit";
            this.picEQInit.Size = new System.Drawing.Size(133, 136);
            this.picEQInit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picEQInit.TabIndex = 125;
            this.picEQInit.TabStop = false;
            // 
            // picProcSelect
            // 
            this.picProcSelect.Image = ((System.Drawing.Image)(resources.GetObject("picProcSelect.Image")));
            this.picProcSelect.Location = new System.Drawing.Point(5, 20);
            this.picProcSelect.Margin = new System.Windows.Forms.Padding(2);
            this.picProcSelect.Name = "picProcSelect";
            this.picProcSelect.Size = new System.Drawing.Size(145, 113);
            this.picProcSelect.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picProcSelect.TabIndex = 124;
            this.picProcSelect.TabStop = false;
            // 
            // btnWaitOut
            // 
            this.btnWaitOut.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWaitOut.Location = new System.Drawing.Point(137, 324);
            this.btnWaitOut.Name = "btnWaitOut";
            this.btnWaitOut.Size = new System.Drawing.Size(97, 25);
            this.btnWaitOut.TabIndex = 117;
            this.btnWaitOut.Text = "Wait Out";
            this.btnWaitOut.UseVisualStyleBackColor = true;
            this.btnWaitOut.Click += new System.EventHandler(this.btnWaitOut_Click_1);
            // 
            // cboWhere
            // 
            this.cboWhere.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboWhere.Font = new System.Drawing.Font("微軟正黑體", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cboWhere.FormattingEnabled = true;
            this.cboWhere.Items.AddRange(new object[] {
            "Pick up",
            "Deposite"});
            this.cboWhere.Location = new System.Drawing.Point(316, 86);
            this.cboWhere.Margin = new System.Windows.Forms.Padding(2);
            this.cboWhere.Name = "cboWhere";
            this.cboWhere.Size = new System.Drawing.Size(71, 26);
            this.cboWhere.TabIndex = 123;
            this.cboWhere.Visible = false;
            // 
            // rdbtnAbnormal
            // 
            this.rdbtnAbnormal.AutoSize = true;
            this.rdbtnAbnormal.Font = new System.Drawing.Font("微軟正黑體", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rdbtnAbnormal.Location = new System.Drawing.Point(154, 53);
            this.rdbtnAbnormal.Margin = new System.Windows.Forms.Padding(2);
            this.rdbtnAbnormal.Name = "rdbtnAbnormal";
            this.rdbtnAbnormal.Size = new System.Drawing.Size(166, 23);
            this.rdbtnAbnormal.TabIndex = 1;
            this.rdbtnAbnormal.Text = "Abnormal  Transfer";
            this.rdbtnAbnormal.UseVisualStyleBackColor = true;
            this.rdbtnAbnormal.Click += new System.EventHandler(this.rdbtnAbnormal_Click);
            // 
            // rdbtnNormal
            // 
            this.rdbtnNormal.AutoSize = true;
            this.rdbtnNormal.Checked = true;
            this.rdbtnNormal.Font = new System.Drawing.Font("微軟正黑體", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rdbtnNormal.Location = new System.Drawing.Point(154, 20);
            this.rdbtnNormal.Margin = new System.Windows.Forms.Padding(2);
            this.rdbtnNormal.Name = "rdbtnNormal";
            this.rdbtnNormal.Size = new System.Drawing.Size(148, 23);
            this.rdbtnNormal.TabIndex = 1;
            this.rdbtnNormal.TabStop = true;
            this.rdbtnNormal.Text = "Normal  Transfer";
            this.rdbtnNormal.UseVisualStyleBackColor = true;
            this.rdbtnNormal.Click += new System.EventHandler(this.rdbtnNormal_Click);
            // 
            // txtCSTID
            // 
            this.txtCSTID.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCSTID.Location = new System.Drawing.Point(110, 273);
            this.txtCSTID.Name = "txtCSTID";
            this.txtCSTID.Size = new System.Drawing.Size(142, 23);
            this.txtCSTID.TabIndex = 111;
            this.txtCSTID.Text = "CST_ID";
            // 
            // cboAbnormal
            // 
            this.cboAbnormal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAbnormal.Font = new System.Drawing.Font("微軟正黑體", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cboAbnormal.FormattingEnabled = true;
            this.cboAbnormal.Items.AddRange(new object[] {
            "TRU_1(Interlock)",
            "TRU_1(Alarm)",
            "TRU_2(Interlock)",
            "TRU_2(Alarm)"});
            this.cboAbnormal.Location = new System.Drawing.Point(164, 86);
            this.cboAbnormal.Margin = new System.Windows.Forms.Padding(2);
            this.cboAbnormal.Name = "cboAbnormal";
            this.cboAbnormal.Size = new System.Drawing.Size(142, 26);
            this.cboAbnormal.TabIndex = 122;
            this.cboAbnormal.Visible = false;
            // 
            // btnBCRReadDown
            // 
            this.btnBCRReadDown.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBCRReadDown.Location = new System.Drawing.Point(18, 324);
            this.btnBCRReadDown.Name = "btnBCRReadDown";
            this.btnBCRReadDown.Size = new System.Drawing.Size(97, 25);
            this.btnBCRReadDown.TabIndex = 109;
            this.btnBCRReadDown.Text = "Wait In";
            this.btnBCRReadDown.UseVisualStyleBackColor = true;
            this.btnBCRReadDown.Click += new System.EventHandler(this.btnBCRReadDown_Click);
            // 
            // tbpTRU2
            // 
            this.tbpTRU2.Controls.Add(this.txtTRU2);
            this.tbpTRU2.Controls.Add(this.label2);
            this.tbpTRU2.Controls.Add(this.TRU2);
            this.tbpTRU2.Location = new System.Drawing.Point(4, 22);
            this.tbpTRU2.Margin = new System.Windows.Forms.Padding(2);
            this.tbpTRU2.Name = "tbpTRU2";
            this.tbpTRU2.Size = new System.Drawing.Size(872, 409);
            this.tbpTRU2.TabIndex = 3;
            this.tbpTRU2.Text = "TRU2";
            this.tbpTRU2.UseVisualStyleBackColor = true;
            // 
            // txtTRU2
            // 
            this.txtTRU2.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTRU2.Location = new System.Drawing.Point(83, 7);
            this.txtTRU2.Name = "txtTRU2";
            this.txtTRU2.Size = new System.Drawing.Size(38, 23);
            this.txtTRU2.TabIndex = 112;
            this.txtTRU2.Text = "1";
            // 
            // label2
            // 
            this.label2.CausesValidation = false;
            this.label2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 20);
            this.label2.TabIndex = 111;
            this.label2.Text = "TRU 2";
            // 
            // TRU2
            // 
            this.TRU2._iTRU = 0;
            this.TRU2.Location = new System.Drawing.Point(4, 33);
            this.TRU2.Margin = new System.Windows.Forms.Padding(4);
            this.TRU2.Name = "TRU2";
            this.TRU2.Size = new System.Drawing.Size(841, 375);
            this.TRU2.TabIndex = 2;
            // 
            // tbpTRU1
            // 
            this.tbpTRU1.Controls.Add(this.txtTRU1);
            this.tbpTRU1.Controls.Add(this.label1);
            this.tbpTRU1.Controls.Add(this.TRU1);
            this.tbpTRU1.Location = new System.Drawing.Point(4, 22);
            this.tbpTRU1.Name = "tbpTRU1";
            this.tbpTRU1.Padding = new System.Windows.Forms.Padding(3);
            this.tbpTRU1.Size = new System.Drawing.Size(872, 409);
            this.tbpTRU1.TabIndex = 1;
            this.tbpTRU1.Text = "TRU1";
            this.tbpTRU1.UseVisualStyleBackColor = true;
            // 
            // txtTRU1
            // 
            this.txtTRU1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTRU1.Location = new System.Drawing.Point(77, 6);
            this.txtTRU1.Name = "txtTRU1";
            this.txtTRU1.Size = new System.Drawing.Size(38, 23);
            this.txtTRU1.TabIndex = 109;
            this.txtTRU1.Text = "1";
            // 
            // label1
            // 
            this.label1.CausesValidation = false;
            this.label1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 20);
            this.label1.TabIndex = 98;
            this.label1.Text = "TRU 1";
            // 
            // TRU1
            // 
            this.TRU1._iTRU = 0;
            this.TRU1.Location = new System.Drawing.Point(4, 30);
            this.TRU1.Margin = new System.Windows.Forms.Padding(4);
            this.TRU1.Name = "TRU1";
            this.TRU1.Size = new System.Drawing.Size(844, 375);
            this.TRU1.TabIndex = 0;
            // 
            // tbpLFT
            // 
            this.tbpLFT.Controls.Add(this.txtLFT);
            this.tbpLFT.Controls.Add(this.label10);
            this.tbpLFT.Controls.Add(this.LFT1);
            this.tbpLFT.Location = new System.Drawing.Point(4, 22);
            this.tbpLFT.Name = "tbpLFT";
            this.tbpLFT.Padding = new System.Windows.Forms.Padding(3);
            this.tbpLFT.Size = new System.Drawing.Size(872, 409);
            this.tbpLFT.TabIndex = 0;
            this.tbpLFT.Text = "LFT";
            this.tbpLFT.UseVisualStyleBackColor = true;
            // 
            // txtLFT
            // 
            this.txtLFT.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLFT.Location = new System.Drawing.Point(73, 7);
            this.txtLFT.Name = "txtLFT";
            this.txtLFT.Size = new System.Drawing.Size(38, 23);
            this.txtLFT.TabIndex = 111;
            this.txtLFT.Text = "1";
            // 
            // label10
            // 
            this.label10.CausesValidation = false;
            this.label10.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(6, 10);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(75, 20);
            this.label10.TabIndex = 110;
            this.label10.Text = "LFT";
            // 
            // LFT1
            // 
            this.LFT1.Location = new System.Drawing.Point(6, 33);
            this.LFT1.Margin = new System.Windows.Forms.Padding(4);
            this.LFT1.Name = "LFT1";
            this.LFT1.Size = new System.Drawing.Size(836, 327);
            this.LFT1.TabIndex = 2;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tbpProcList);
            this.tabControl1.Controls.Add(this.tbpLFT);
            this.tabControl1.Controls.Add(this.tbpTRU1);
            this.tabControl1.Controls.Add(this.tbpTRU2);
            this.tabControl1.Location = new System.Drawing.Point(0, 52);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(880, 435);
            this.tabControl1.TabIndex = 3;
            // 
            // chkBxTM2Word
            // 
            this.chkBxTM2Word.AutoSize = true;
            this.chkBxTM2Word.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkBxTM2Word.Location = new System.Drawing.Point(549, 18);
            this.chkBxTM2Word.Name = "chkBxTM2Word";
            this.chkBxTM2Word.Size = new System.Drawing.Size(164, 23);
            this.chkBxTM2Word.TabIndex = 180;
            this.chkBxTM2Word.Text = "TM 2Word(Fork type)";
            this.chkBxTM2Word.UseVisualStyleBackColor = true;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(893, 498);
            this.Controls.Add(this.chkBxTM2Word);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.lblConn_PLC_Name);
            this.Controls.Add(this.lblConn_PLC1);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmM_Load);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picbxMirle)).EndInit();
            this.tbpProcList.ResumeLayout(false);
            this.tbpProcList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPortFunc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEQInit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picProcSelect)).EndInit();
            this.tbpTRU2.ResumeLayout(false);
            this.tbpTRU2.PerformLayout();
            this.tbpTRU1.ResumeLayout(false);
            this.tbpTRU1.PerformLayout();
            this.tbpLFT.ResumeLayout(false);
            this.tbpLFT.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer tmMainProc;
        private System.Windows.Forms.Label lblConn_PLC_Name;
        private System.Windows.Forms.Label lblConn_PLC1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox picbxMirle;
        private System.Windows.Forms.TabPage tbpProcList;
        private System.Windows.Forms.Button btnRemoved;
        private System.Windows.Forms.Button btnWaitOut;
        private System.Windows.Forms.TextBox txtCSTID;
        private System.Windows.Forms.Button btnBCRReadDown;
        private System.Windows.Forms.TabPage tbpTRU2;
        private System.Windows.Forms.TextBox txtTRU2;
        private System.Windows.Forms.Label label2;
        private PLC.TRU TRU2;
        private System.Windows.Forms.TabPage tbpTRU1;
        private System.Windows.Forms.TextBox txtTRU1;
        private System.Windows.Forms.Label label1;
        private PLC.TRU TRU1;
        private System.Windows.Forms.TabPage tbpLFT;
        private System.Windows.Forms.TextBox txtLFT;
        private System.Windows.Forms.Label label10;
        private PLC.LFT LFT1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.ComboBox cboAbnormal;
        private System.Windows.Forms.RadioButton rdbtnAbnormal;
        private System.Windows.Forms.RadioButton rdbtnNormal;
        private System.Windows.Forms.Button btnModeChange;
        private System.Windows.Forms.ComboBox cboWhere;
        private System.Windows.Forms.PictureBox picProcSelect;
        private System.Windows.Forms.PictureBox picEQInit;
        private System.Windows.Forms.PictureBox picPortFunc;
        private System.Windows.Forms.Button btnTRU1Reset;
        private System.Windows.Forms.Button btnTRU2Reset;
        private System.Windows.Forms.Button btnLFTReset;
        private System.Windows.Forms.CheckBox chkBxTM2Word;
        private System.Windows.Forms.ComboBox cboPortSelect;
        private System.Windows.Forms.Label lblCSTID;
        private System.Windows.Forms.Label lblPortSelect;
        private System.Windows.Forms.Button btnInitLFT;
        private System.Windows.Forms.Button btnInitTRU1;
        private System.Windows.Forms.Button button31;
        private System.Windows.Forms.Button btnInitTRU2;
        private System.Windows.Forms.Button btnInitAll;
        private System.Windows.Forms.ComboBox cboSourceDest;
        private System.Windows.Forms.Button btnTRU1Interlock;
        private System.Windows.Forms.Button btnTRU2Interlock;
        private System.Windows.Forms.Button btnLFTInterlock;
        private System.Windows.Forms.Button btnTRU1Alarm;
        private System.Windows.Forms.Button btnTRU2Alarm;
        private System.Windows.Forms.Button btnLFTAlarm;
        private System.Windows.Forms.Button btnPortAlarm;
        private System.Windows.Forms.Button btnResetPort;
        private System.Windows.Forms.ComboBox cboStageSelect;
        private System.Windows.Forms.Label lblStage;
        private System.Windows.Forms.Button btnTRU2PresentOnOff;
        private System.Windows.Forms.Button btnTRU1PresentOnOff;
        private System.Windows.Forms.Button btnLFTPresentOnOff;
        private System.Windows.Forms.Button btnPresentOn;



    }
}

