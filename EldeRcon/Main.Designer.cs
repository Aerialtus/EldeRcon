namespace EldeRcon
{
    partial class Main
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.txtConsole0 = new System.Windows.Forms.TextBox();
            this.txtCommand = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.lblHostname = new System.Windows.Forms.Label();
            this.txtHostname = new System.Windows.Forms.TextBox();
            this.lblPort = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.cmbLoadExisting = new System.Windows.Forms.ComboBox();
            this.cbSavePass = new System.Windows.Forms.CheckBox();
            this.tabServers = new System.Windows.Forms.TabControl();
            this.tab0 = new System.Windows.Forms.TabPage();
            this.tabNew = new System.Windows.Forms.TabPage();
            this.btnManage = new System.Windows.Forms.Button();
            this.lblSeconds0 = new System.Windows.Forms.Label();
            this.txtRefreshSeconds = new System.Windows.Forms.TextBox();
            this.lblRefresh0 = new System.Windows.Forms.Label();
            this.lvPlayers = new System.Windows.Forms.ListView();
            this.clmColor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmScore = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmKills = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmDeaths = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmAssists = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmBetrayals = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmUID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmsPlayerLV = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.sendMessageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kickNoBanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kickTempBanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kickPermaBanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cbTrimChat = new System.Windows.Forms.CheckBox();
            this.btnEndGame = new System.Windows.Forms.Button();
            this.btnShuffle = new System.Windows.Forms.Button();
            this.lvTeamScore = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnReloadVotingJson = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cbReportGameChange = new System.Windows.Forms.CheckBox();
            this.cbReportJoinsLeaves = new System.Windows.Forms.CheckBox();
            this.txtConnectCommand = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCopy = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.tabServers.SuspendLayout();
            this.tab0.SuspendLayout();
            this.cmsPlayerLV.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtConsole0
            // 
            this.txtConsole0.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtConsole0.Location = new System.Drawing.Point(0, 0);
            this.txtConsole0.Multiline = true;
            this.txtConsole0.Name = "txtConsole0";
            this.txtConsole0.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtConsole0.Size = new System.Drawing.Size(638, 495);
            this.txtConsole0.TabIndex = 0;
            // 
            // txtCommand
            // 
            this.txtCommand.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCommand.Location = new System.Drawing.Point(4, 583);
            this.txtCommand.Name = "txtCommand";
            this.txtCommand.Size = new System.Drawing.Size(786, 20);
            this.txtCommand.TabIndex = 2;
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSend.Location = new System.Drawing.Point(796, 581);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 3;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // lblHostname
            // 
            this.lblHostname.Location = new System.Drawing.Point(205, 5);
            this.lblHostname.Name = "lblHostname";
            this.lblHostname.Size = new System.Drawing.Size(58, 20);
            this.lblHostname.TabIndex = 4;
            this.lblHostname.Text = "Hostname:";
            this.lblHostname.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtHostname
            // 
            this.txtHostname.Location = new System.Drawing.Point(269, 6);
            this.txtHostname.Name = "txtHostname";
            this.txtHostname.Size = new System.Drawing.Size(166, 20);
            this.txtHostname.TabIndex = 5;
            // 
            // lblPort
            // 
            this.lblPort.Location = new System.Drawing.Point(441, 5);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(29, 20);
            this.lblPort.TabIndex = 6;
            this.lblPort.Text = "Port:";
            this.lblPort.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(476, 6);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(51, 20);
            this.txtPort.TabIndex = 7;
            this.txtPort.Text = "11776";
            // 
            // lblPassword
            // 
            this.lblPassword.Location = new System.Drawing.Point(533, 5);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(56, 20);
            this.lblPassword.TabIndex = 8;
            this.lblPassword.Text = "Password:";
            this.lblPassword.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(596, 6);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(118, 20);
            this.txtPassword.TabIndex = 9;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(720, 4);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 10;
            this.btnConnect.Text = "Connect!";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // cmbLoadExisting
            // 
            this.cmbLoadExisting.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLoadExisting.FormattingEnabled = true;
            this.cmbLoadExisting.Items.AddRange(new object[] {
            "Load Existing..."});
            this.cmbLoadExisting.Location = new System.Drawing.Point(4, 5);
            this.cmbLoadExisting.MaxDropDownItems = 100;
            this.cmbLoadExisting.Name = "cmbLoadExisting";
            this.cmbLoadExisting.Size = new System.Drawing.Size(114, 21);
            this.cmbLoadExisting.TabIndex = 12;
            this.cmbLoadExisting.SelectedIndexChanged += new System.EventHandler(this.cmbLoadExisting_SelectedIndexChanged);
            // 
            // cbSavePass
            // 
            this.cbSavePass.AutoSize = true;
            this.cbSavePass.Checked = true;
            this.cbSavePass.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSavePass.Location = new System.Drawing.Point(801, 8);
            this.cbSavePass.Name = "cbSavePass";
            this.cbSavePass.Size = new System.Drawing.Size(72, 17);
            this.cbSavePass.TabIndex = 13;
            this.cbSavePass.Text = "Save PW";
            this.cbSavePass.UseVisualStyleBackColor = true;
            // 
            // tabServers
            // 
            this.tabServers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabServers.Controls.Add(this.tab0);
            this.tabServers.Controls.Add(this.tabNew);
            this.tabServers.Location = new System.Drawing.Point(4, 33);
            this.tabServers.Name = "tabServers";
            this.tabServers.SelectedIndex = 0;
            this.tabServers.Size = new System.Drawing.Size(646, 521);
            this.tabServers.TabIndex = 14;
            this.tabServers.SelectedIndexChanged += new System.EventHandler(this.tabServers_SelectedIndexChanged);
            // 
            // tab0
            // 
            this.tab0.Controls.Add(this.txtConsole0);
            this.tab0.Location = new System.Drawing.Point(4, 22);
            this.tab0.Name = "tab0";
            this.tab0.Padding = new System.Windows.Forms.Padding(3);
            this.tab0.Size = new System.Drawing.Size(638, 495);
            this.tab0.TabIndex = 0;
            this.tab0.Text = "Server 1";
            this.tab0.UseVisualStyleBackColor = true;
            // 
            // tabNew
            // 
            this.tabNew.Location = new System.Drawing.Point(4, 22);
            this.tabNew.Name = "tabNew";
            this.tabNew.Padding = new System.Windows.Forms.Padding(3);
            this.tabNew.Size = new System.Drawing.Size(638, 495);
            this.tabNew.TabIndex = 1;
            this.tabNew.Text = "New...";
            this.tabNew.UseVisualStyleBackColor = true;
            // 
            // btnManage
            // 
            this.btnManage.Location = new System.Drawing.Point(124, 4);
            this.btnManage.Name = "btnManage";
            this.btnManage.Size = new System.Drawing.Size(75, 23);
            this.btnManage.TabIndex = 15;
            this.btnManage.Text = "Manage...";
            this.btnManage.UseVisualStyleBackColor = true;
            this.btnManage.Click += new System.EventHandler(this.btnManage_Click);
            // 
            // lblSeconds0
            // 
            this.lblSeconds0.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSeconds0.AutoSize = true;
            this.lblSeconds0.Location = new System.Drawing.Point(796, 381);
            this.lblSeconds0.Name = "lblSeconds0";
            this.lblSeconds0.Size = new System.Drawing.Size(50, 13);
            this.lblSeconds0.TabIndex = 20;
            this.lblSeconds0.Text = "seconds.";
            // 
            // txtRefreshSeconds
            // 
            this.txtRefreshSeconds.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRefreshSeconds.Location = new System.Drawing.Point(761, 378);
            this.txtRefreshSeconds.Name = "txtRefreshSeconds";
            this.txtRefreshSeconds.Size = new System.Drawing.Size(29, 20);
            this.txtRefreshSeconds.TabIndex = 19;
            this.txtRefreshSeconds.Text = "5";
            // 
            // lblRefresh0
            // 
            this.lblRefresh0.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRefresh0.AutoSize = true;
            this.lblRefresh0.Location = new System.Drawing.Point(681, 381);
            this.lblRefresh0.Name = "lblRefresh0";
            this.lblRefresh0.Size = new System.Drawing.Size(79, 13);
            this.lblRefresh0.TabIndex = 18;
            this.lblRefresh0.Text = "Refresh every: ";
            // 
            // lvPlayers
            // 
            this.lvPlayers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvPlayers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmColor,
            this.clmName,
            this.clmScore,
            this.clmKills,
            this.clmDeaths,
            this.clmAssists,
            this.clmBetrayals,
            this.clmUID});
            this.lvPlayers.FullRowSelect = true;
            this.lvPlayers.GridLines = true;
            this.lvPlayers.Location = new System.Drawing.Point(656, 55);
            this.lvPlayers.Name = "lvPlayers";
            this.lvPlayers.Size = new System.Drawing.Size(217, 317);
            this.lvPlayers.TabIndex = 17;
            this.lvPlayers.UseCompatibleStateImageBehavior = false;
            this.lvPlayers.View = System.Windows.Forms.View.Details;
            this.lvPlayers.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lvPlayers_MouseUp);
            // 
            // clmColor
            // 
            this.clmColor.Text = "   ";
            this.clmColor.Width = 23;
            // 
            // clmName
            // 
            this.clmName.Text = "Name";
            this.clmName.Width = 90;
            // 
            // clmScore
            // 
            this.clmScore.Text = "S";
            this.clmScore.Width = 21;
            // 
            // clmKills
            // 
            this.clmKills.Text = "K";
            this.clmKills.Width = 28;
            // 
            // clmDeaths
            // 
            this.clmDeaths.Text = "D";
            this.clmDeaths.Width = 27;
            // 
            // clmAssists
            // 
            this.clmAssists.Text = "A";
            this.clmAssists.Width = 22;
            // 
            // clmBetrayals
            // 
            this.clmBetrayals.Text = "B";
            this.clmBetrayals.Width = 22;
            // 
            // clmUID
            // 
            this.clmUID.Text = "UID";
            // 
            // cmsPlayerLV
            // 
            this.cmsPlayerLV.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sendMessageToolStripMenuItem,
            this.kickNoBanToolStripMenuItem,
            this.kickTempBanToolStripMenuItem,
            this.kickPermaBanToolStripMenuItem});
            this.cmsPlayerLV.Name = "cmsPlayerLV";
            this.cmsPlayerLV.ShowImageMargin = false;
            this.cmsPlayerLV.Size = new System.Drawing.Size(140, 92);
            this.cmsPlayerLV.Closing += new System.Windows.Forms.ToolStripDropDownClosingEventHandler(this.cmsPlayerLV_Closing);
            this.cmsPlayerLV.Opening += new System.ComponentModel.CancelEventHandler(this.cmsPlayerLV_Opening);
            // 
            // sendMessageToolStripMenuItem
            // 
            this.sendMessageToolStripMenuItem.Name = "sendMessageToolStripMenuItem";
            this.sendMessageToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.sendMessageToolStripMenuItem.Text = "Send Message";
            this.sendMessageToolStripMenuItem.Click += new System.EventHandler(this.sendMessageToolStripMenuItem_Click);
            // 
            // kickNoBanToolStripMenuItem
            // 
            this.kickNoBanToolStripMenuItem.Name = "kickNoBanToolStripMenuItem";
            this.kickNoBanToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.kickNoBanToolStripMenuItem.Text = "Kick (No Ban)";
            this.kickNoBanToolStripMenuItem.Click += new System.EventHandler(this.kickNoBanToolStripMenuItem_Click);
            // 
            // kickTempBanToolStripMenuItem
            // 
            this.kickTempBanToolStripMenuItem.Name = "kickTempBanToolStripMenuItem";
            this.kickTempBanToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.kickTempBanToolStripMenuItem.Text = "Kick (Temp Ban)";
            this.kickTempBanToolStripMenuItem.Click += new System.EventHandler(this.kickTempBanToolStripMenuItem_Click);
            // 
            // kickPermaBanToolStripMenuItem
            // 
            this.kickPermaBanToolStripMenuItem.Name = "kickPermaBanToolStripMenuItem";
            this.kickPermaBanToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.kickPermaBanToolStripMenuItem.Text = "Kick (Perma Ban)";
            this.kickPermaBanToolStripMenuItem.Click += new System.EventHandler(this.kickPermaBanToolStripMenuItem_Click);
            // 
            // cbTrimChat
            // 
            this.cbTrimChat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbTrimChat.Checked = true;
            this.cbTrimChat.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbTrimChat.Location = new System.Drawing.Point(536, 555);
            this.cbTrimChat.Name = "cbTrimChat";
            this.cbTrimChat.Size = new System.Drawing.Size(110, 22);
            this.cbTrimChat.TabIndex = 25;
            this.cbTrimChat.Text = "Remove UID/IP";
            this.cbTrimChat.UseVisualStyleBackColor = true;
            // 
            // btnEndGame
            // 
            this.btnEndGame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEndGame.Location = new System.Drawing.Point(798, 481);
            this.btnEndGame.Name = "btnEndGame";
            this.btnEndGame.Size = new System.Drawing.Size(75, 23);
            this.btnEndGame.TabIndex = 26;
            this.btnEndGame.Text = "End Game";
            this.btnEndGame.UseVisualStyleBackColor = true;
            this.btnEndGame.Click += new System.EventHandler(this.btnEndGame_Click);
            // 
            // btnShuffle
            // 
            this.btnShuffle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShuffle.Location = new System.Drawing.Point(798, 413);
            this.btnShuffle.Name = "btnShuffle";
            this.btnShuffle.Size = new System.Drawing.Size(75, 23);
            this.btnShuffle.TabIndex = 27;
            this.btnShuffle.Text = "Shuffle";
            this.btnShuffle.UseVisualStyleBackColor = true;
            this.btnShuffle.Click += new System.EventHandler(this.btnShuffle_Click);
            // 
            // lvTeamScore
            // 
            this.lvTeamScore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lvTeamScore.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.lvTeamScore.GridLines = true;
            this.lvTeamScore.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvTeamScore.Location = new System.Drawing.Point(656, 430);
            this.lvTeamScore.MultiSelect = false;
            this.lvTeamScore.Name = "lvTeamScore";
            this.lvTeamScore.Scrollable = false;
            this.lvTeamScore.Size = new System.Drawing.Size(118, 74);
            this.lvTeamScore.TabIndex = 29;
            this.lvTeamScore.UseCompatibleStateImageBehavior = false;
            this.lvTeamScore.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "   ";
            this.columnHeader1.Width = 23;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "";
            this.columnHeader2.Width = 33;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "   ";
            this.columnHeader3.Width = 23;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "";
            this.columnHeader4.Width = 33;
            // 
            // btnReloadVotingJson
            // 
            this.btnReloadVotingJson.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReloadVotingJson.Location = new System.Drawing.Point(798, 442);
            this.btnReloadVotingJson.Name = "btnReloadVotingJson";
            this.btnReloadVotingJson.Size = new System.Drawing.Size(75, 23);
            this.btnReloadVotingJson.TabIndex = 30;
            this.btnReloadVotingJson.Text = "RL Voting";
            this.btnReloadVotingJson.UseVisualStyleBackColor = true;
            this.btnReloadVotingJson.Click += new System.EventHandler(this.btnReloadVotingJson_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(653, 413);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 31;
            this.label1.Text = "Team Scores:";
            // 
            // cbReportGameChange
            // 
            this.cbReportGameChange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbReportGameChange.Checked = true;
            this.cbReportGameChange.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbReportGameChange.Location = new System.Drawing.Point(389, 555);
            this.cbReportGameChange.Name = "cbReportGameChange";
            this.cbReportGameChange.Size = new System.Drawing.Size(138, 22);
            this.cbReportGameChange.TabIndex = 25;
            this.cbReportGameChange.Text = "Report Game Changes";
            this.cbReportGameChange.UseVisualStyleBackColor = true;
            // 
            // cbReportJoinsLeaves
            // 
            this.cbReportJoinsLeaves.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbReportJoinsLeaves.Checked = true;
            this.cbReportJoinsLeaves.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbReportJoinsLeaves.Location = new System.Drawing.Point(256, 555);
            this.cbReportJoinsLeaves.Name = "cbReportJoinsLeaves";
            this.cbReportJoinsLeaves.Size = new System.Drawing.Size(127, 22);
            this.cbReportJoinsLeaves.TabIndex = 25;
            this.cbReportJoinsLeaves.Text = "Report Joins/Leaves";
            this.cbReportJoinsLeaves.UseVisualStyleBackColor = true;
            // 
            // txtConnectCommand
            // 
            this.txtConnectCommand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtConnectCommand.Location = new System.Drawing.Point(656, 529);
            this.txtConnectCommand.Name = "txtConnectCommand";
            this.txtConnectCommand.Size = new System.Drawing.Size(168, 20);
            this.txtConnectCommand.TabIndex = 32;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(653, 513);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 13);
            this.label2.TabIndex = 33;
            this.label2.Text = "Connect command:";
            // 
            // btnCopy
            // 
            this.btnCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopy.Location = new System.Drawing.Point(831, 527);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(42, 23);
            this.btnCopy.TabIndex = 34;
            this.btnCopy.Text = "Copy";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1, 559);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 35;
            this.label3.Text = "Name:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(41, 556);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(134, 20);
            this.txtName.TabIndex = 36;
            // 
            // Main
            // 
            this.AcceptButton = this.btnSend;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(879, 612);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtConnectCommand);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnReloadVotingJson);
            this.Controls.Add(this.lvTeamScore);
            this.Controls.Add(this.btnShuffle);
            this.Controls.Add(this.btnEndGame);
            this.Controls.Add(this.cbReportJoinsLeaves);
            this.Controls.Add(this.cbReportGameChange);
            this.Controls.Add(this.cbTrimChat);
            this.Controls.Add(this.lblSeconds0);
            this.Controls.Add(this.txtRefreshSeconds);
            this.Controls.Add(this.lblRefresh0);
            this.Controls.Add(this.lvPlayers);
            this.Controls.Add(this.btnManage);
            this.Controls.Add(this.tabServers);
            this.Controls.Add(this.cbSavePass);
            this.Controls.Add(this.cmbLoadExisting);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.lblPort);
            this.Controls.Add(this.txtHostname);
            this.Controls.Add(this.lblHostname);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtCommand);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EldeRcon";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosed);
            this.Shown += new System.EventHandler(this.Main_Shown);
            this.tabServers.ResumeLayout(false);
            this.tab0.ResumeLayout(false);
            this.tab0.PerformLayout();
            this.cmsPlayerLV.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtConsole0;
        private System.Windows.Forms.TextBox txtCommand;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Label lblHostname;
        private System.Windows.Forms.TextBox txtHostname;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.ComboBox cmbLoadExisting;
        private System.Windows.Forms.CheckBox cbSavePass;
        private System.Windows.Forms.TabControl tabServers;
        private System.Windows.Forms.TabPage tab0;
        private System.Windows.Forms.TabPage tabNew;
        private System.Windows.Forms.Button btnManage;
        private System.Windows.Forms.Label lblSeconds0;
        private System.Windows.Forms.TextBox txtRefreshSeconds;
        private System.Windows.Forms.Label lblRefresh0;
        private System.Windows.Forms.ListView lvPlayers;
        private System.Windows.Forms.ColumnHeader clmColor;
        private System.Windows.Forms.ColumnHeader clmName;
        private System.Windows.Forms.ColumnHeader clmKills;
        private System.Windows.Forms.ColumnHeader clmDeaths;
        private System.Windows.Forms.ColumnHeader clmAssists;
        private System.Windows.Forms.ColumnHeader clmBetrayals;
        private System.Windows.Forms.ColumnHeader clmUID;
        private System.Windows.Forms.ContextMenuStrip cmsPlayerLV;
        private System.Windows.Forms.ToolStripMenuItem sendMessageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kickNoBanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kickTempBanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kickPermaBanToolStripMenuItem;
        private System.Windows.Forms.CheckBox cbTrimChat;
        private System.Windows.Forms.ColumnHeader clmScore;
        private System.Windows.Forms.Button btnEndGame;
        private System.Windows.Forms.Button btnShuffle;
        private System.Windows.Forms.ListView lvTeamScore;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Button btnReloadVotingJson;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbReportGameChange;
        private System.Windows.Forms.CheckBox cbReportJoinsLeaves;
        private System.Windows.Forms.TextBox txtConnectCommand;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtName;
    }
}

