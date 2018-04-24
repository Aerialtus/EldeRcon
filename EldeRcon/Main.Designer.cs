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
            this.lvPlayers0 = new System.Windows.Forms.ListView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lblSoonTM = new System.Windows.Forms.Label();
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
            // lvPlayers0
            // 
            this.lvPlayers0.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvPlayers0.Location = new System.Drawing.Point(678, 0);
            this.lvPlayers0.Name = "lvPlayers0";
            this.lvPlayers0.Size = new System.Drawing.Size(186, 485);
            this.lvPlayers0.TabIndex = 1;
            this.lvPlayers0.UseCompatibleStateImageBehavior = false;
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
    }
}

