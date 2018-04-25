﻿namespace EldeRcon
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.txtConsole0 = new System.Windows.Forms.TextBox();
            this.txtCommand = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtHostname = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.cmbLoadExisting = new System.Windows.Forms.ComboBox();
            this.cbSavePass = new System.Windows.Forms.CheckBox();
            this.tabServers = new System.Windows.Forms.TabControl();
            this.tab0 = new System.Windows.Forms.TabPage();
            this.lblSeconds0 = new System.Windows.Forms.Label();
            this.txtRefreshSeconds0 = new System.Windows.Forms.TextBox();
            this.lblRefresh0 = new System.Windows.Forms.Label();
            this.lvPlayers0 = new System.Windows.Forms.ListView();
            this.clmColor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmKills = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmDeaths = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmAssists = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmBetrayals = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmUID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lblSoonTM = new System.Windows.Forms.Label();
            this.btnKickTempBan0 = new System.Windows.Forms.Button();
            this.btnSendMessage0 = new System.Windows.Forms.Button();
            this.btnKick0 = new System.Windows.Forms.Button();
            this.btnKickPermBan0 = new System.Windows.Forms.Button();
            this.tabServers.SuspendLayout();
            this.tab0.SuspendLayout();
            this.tabPage2.SuspendLayout();
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
            this.txtConsole0.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtConsole0.Size = new System.Drawing.Size(672, 485);
            this.txtConsole0.TabIndex = 0;
            // 
            // txtCommand
            // 
            this.txtCommand.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCommand.Location = new System.Drawing.Point(4, 550);
            this.txtCommand.Name = "txtCommand";
            this.txtCommand.Size = new System.Drawing.Size(791, 20);
            this.txtCommand.TabIndex = 2;
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSend.Location = new System.Drawing.Point(801, 548);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 3;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(148, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Hostname:";
            // 
            // txtHostname
            // 
            this.txtHostname.Location = new System.Drawing.Point(212, 5);
            this.txtHostname.Name = "txtHostname";
            this.txtHostname.Size = new System.Drawing.Size(166, 20);
            this.txtHostname.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(384, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Port:";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(419, 5);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(51, 20);
            this.txtPort.TabIndex = 7;
            this.txtPort.Text = "11776";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(477, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Password:";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(538, 5);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(176, 20);
            this.txtPassword.TabIndex = 9;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(720, 3);
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
            this.cmbLoadExisting.Size = new System.Drawing.Size(141, 21);
            this.cmbLoadExisting.TabIndex = 12;
            this.cmbLoadExisting.SelectedIndexChanged += new System.EventHandler(this.cmbLoadExisting_SelectedIndexChanged);
            // 
            // cbSavePass
            // 
            this.cbSavePass.AutoSize = true;
            this.cbSavePass.Checked = true;
            this.cbSavePass.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSavePass.Location = new System.Drawing.Point(801, 7);
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
            this.tabServers.Controls.Add(this.tabPage2);
            this.tabServers.Location = new System.Drawing.Point(4, 33);
            this.tabServers.Name = "tabServers";
            this.tabServers.SelectedIndex = 0;
            this.tabServers.Size = new System.Drawing.Size(872, 511);
            this.tabServers.TabIndex = 14;
            // 
            // tab0
            // 
            this.tab0.Controls.Add(this.btnKickPermBan0);
            this.tab0.Controls.Add(this.btnKickTempBan0);
            this.tab0.Controls.Add(this.btnSendMessage0);
            this.tab0.Controls.Add(this.btnKick0);
            this.tab0.Controls.Add(this.lblSeconds0);
            this.tab0.Controls.Add(this.txtRefreshSeconds0);
            this.tab0.Controls.Add(this.lblRefresh0);
            this.tab0.Controls.Add(this.lvPlayers0);
            this.tab0.Controls.Add(this.txtConsole0);
            this.tab0.Location = new System.Drawing.Point(4, 22);
            this.tab0.Name = "tab0";
            this.tab0.Padding = new System.Windows.Forms.Padding(3);
            this.tab0.Size = new System.Drawing.Size(864, 485);
            this.tab0.TabIndex = 0;
            this.tab0.Text = "Server 1";
            this.tab0.UseVisualStyleBackColor = true;
            // 
            // lblSeconds0
            // 
            this.lblSeconds0.AutoSize = true;
            this.lblSeconds0.Location = new System.Drawing.Point(793, 326);
            this.lblSeconds0.Name = "lblSeconds0";
            this.lblSeconds0.Size = new System.Drawing.Size(50, 13);
            this.lblSeconds0.TabIndex = 4;
            this.lblSeconds0.Text = "seconds.";
            // 
            // txtRefreshSeconds0
            // 
            this.txtRefreshSeconds0.Location = new System.Drawing.Point(758, 323);
            this.txtRefreshSeconds0.Name = "txtRefreshSeconds0";
            this.txtRefreshSeconds0.Size = new System.Drawing.Size(29, 20);
            this.txtRefreshSeconds0.TabIndex = 3;
            this.txtRefreshSeconds0.Text = "5";
            // 
            // lblRefresh0
            // 
            this.lblRefresh0.AutoSize = true;
            this.lblRefresh0.Location = new System.Drawing.Point(678, 326);
            this.lblRefresh0.Name = "lblRefresh0";
            this.lblRefresh0.Size = new System.Drawing.Size(79, 13);
            this.lblRefresh0.TabIndex = 2;
            this.lblRefresh0.Text = "Refresh every: ";
            // 
            // lvPlayers0
            // 
            this.lvPlayers0.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvPlayers0.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmColor,
            this.clmName,
            this.clmKills,
            this.clmDeaths,
            this.clmAssists,
            this.clmBetrayals,
            this.clmUID});
            this.lvPlayers0.FullRowSelect = true;
            this.lvPlayers0.GridLines = true;
            this.lvPlayers0.Location = new System.Drawing.Point(678, 0);
            this.lvPlayers0.Name = "lvPlayers0";
            this.lvPlayers0.Size = new System.Drawing.Size(186, 319);
            this.lvPlayers0.TabIndex = 1;
            this.lvPlayers0.UseCompatibleStateImageBehavior = false;
            this.lvPlayers0.View = System.Windows.Forms.View.Details;
            // 
            // clmColor
            // 
            this.clmColor.Text = "   ";
            this.clmColor.Width = 23;
            // 
            // clmName
            // 
            this.clmName.Text = "Name";
            // 
            // clmKills
            // 
            this.clmKills.Text = "K";
            this.clmKills.Width = 25;
            // 
            // clmDeaths
            // 
            this.clmDeaths.Text = "D";
            this.clmDeaths.Width = 22;
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
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lblSoonTM);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(864, 485);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "New...";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lblSoonTM
            // 
            this.lblSoonTM.AutoSize = true;
            this.lblSoonTM.Font = new System.Drawing.Font("Microsoft Sans Serif", 32F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSoonTM.Location = new System.Drawing.Point(109, 217);
            this.lblSoonTM.Name = "lblSoonTM";
            this.lblSoonTM.Size = new System.Drawing.Size(647, 51);
            this.lblSoonTM.TabIndex = 0;
            this.lblSoonTM.Text = "Tabs hopefully coming soon(tm)!";
            // 
            // btnKickTempBan0
            // 
            this.btnKickTempBan0.Location = new System.Drawing.Point(712, 427);
            this.btnKickTempBan0.Name = "btnKickTempBan0";
            this.btnKickTempBan0.Size = new System.Drawing.Size(114, 23);
            this.btnKickTempBan0.TabIndex = 5;
            this.btnKickTempBan0.Text = "Kick + Temp Ban";
            this.btnKickTempBan0.UseVisualStyleBackColor = true;
            this.btnKickTempBan0.Click += new System.EventHandler(this.btnKickTempBan_Click);
            // 
            // btnSendMessage0
            // 
            this.btnSendMessage0.Location = new System.Drawing.Point(711, 349);
            this.btnSendMessage0.Name = "btnSendMessage0";
            this.btnSendMessage0.Size = new System.Drawing.Size(114, 23);
            this.btnSendMessage0.TabIndex = 5;
            this.btnSendMessage0.Text = "Send Message";
            this.btnSendMessage0.UseVisualStyleBackColor = true;
            this.btnSendMessage0.Click += new System.EventHandler(this.btnSendMessage_Click);
            // 
            // btnKick0
            // 
            this.btnKick0.Location = new System.Drawing.Point(711, 398);
            this.btnKick0.Name = "btnKick0";
            this.btnKick0.Size = new System.Drawing.Size(114, 23);
            this.btnKick0.TabIndex = 5;
            this.btnKick0.Text = "Kick (No Ban)";
            this.btnKick0.UseVisualStyleBackColor = true;
            this.btnKick0.Click += new System.EventHandler(this.btnKick_Click);
            // 
            // btnKickPermBan0
            // 
            this.btnKickPermBan0.Location = new System.Drawing.Point(711, 456);
            this.btnKickPermBan0.Name = "btnKickPermBan0";
            this.btnKickPermBan0.Size = new System.Drawing.Size(114, 23);
            this.btnKickPermBan0.TabIndex = 5;
            this.btnKickPermBan0.Text = "Kick + Perma Ban";
            this.btnKickPermBan0.UseVisualStyleBackColor = true;
            this.btnKickPermBan0.Click += new System.EventHandler(this.btnKickPermBan_Click);
            // 
            // Main
            // 
            this.AcceptButton = this.btnSend;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 579);
            this.Controls.Add(this.tabServers);
            this.Controls.Add(this.cbSavePass);
            this.Controls.Add(this.cmbLoadExisting);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtHostname);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtCommand);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.Text = "EldeRcon";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosed);
            this.tabServers.ResumeLayout(false);
            this.tab0.ResumeLayout(false);
            this.tab0.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtConsole0;
        private System.Windows.Forms.TextBox txtCommand;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtHostname;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.ComboBox cmbLoadExisting;
        private System.Windows.Forms.CheckBox cbSavePass;
        private System.Windows.Forms.TabControl tabServers;
        private System.Windows.Forms.TabPage tab0;
        private System.Windows.Forms.ListView lvPlayers0;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label lblSoonTM;
        private System.Windows.Forms.ColumnHeader clmColor;
        private System.Windows.Forms.ColumnHeader clmName;
        private System.Windows.Forms.ColumnHeader clmKills;
        private System.Windows.Forms.ColumnHeader clmDeaths;
        private System.Windows.Forms.ColumnHeader clmAssists;
        private System.Windows.Forms.ColumnHeader clmBetrayals;
        private System.Windows.Forms.ColumnHeader clmUID;
        private System.Windows.Forms.Label lblSeconds0;
        private System.Windows.Forms.TextBox txtRefreshSeconds0;
        private System.Windows.Forms.Label lblRefresh0;
        private System.Windows.Forms.Button btnKickPermBan0;
        private System.Windows.Forms.Button btnKickTempBan0;
        private System.Windows.Forms.Button btnSendMessage0;
        private System.Windows.Forms.Button btnKick0;
    }
}

