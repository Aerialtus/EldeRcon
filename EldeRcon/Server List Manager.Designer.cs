namespace EldeRcon
{
    partial class Server_List_Manager
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
            this.btnUp = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.dgvServers = new System.Windows.Forms.DataGridView();
            this.clmAutoconnect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.clmNick = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmHostname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmPortNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmPass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblHelp = new System.Windows.Forms.Label();
            this.lblAdd = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvServers)).BeginInit();
            this.SuspendLayout();
            // 
            // btnUp
            // 
            this.btnUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUp.Location = new System.Drawing.Point(12, 239);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(75, 23);
            this.btnUp.TabIndex = 1;
            this.btnUp.Text = "Move Up";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnDown
            // 
            this.btnDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDown.Location = new System.Drawing.Point(94, 239);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(75, 23);
            this.btnDown.TabIndex = 2;
            this.btnDown.Text = "Move Down";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnRemove.Location = new System.Drawing.Point(268, 239);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 23);
            this.btnRemove.TabIndex = 5;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(524, 239);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(443, 239);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dgvServers
            // 
            this.dgvServers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvServers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvServers.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvServers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvServers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmAutoconnect,
            this.clmNick,
            this.clmHostname,
            this.clmPortNum,
            this.clmPass});
            this.dgvServers.Location = new System.Drawing.Point(12, 53);
            this.dgvServers.Name = "dgvServers";
            this.dgvServers.Size = new System.Drawing.Size(583, 180);
            this.dgvServers.TabIndex = 8;
            // 
            // clmAutoconnect
            // 
            this.clmAutoconnect.HeaderText = "Autoconnect";
            this.clmAutoconnect.Name = "clmAutoconnect";
            this.clmAutoconnect.Width = 74;
            // 
            // clmNick
            // 
            this.clmNick.HeaderText = "Nickname";
            this.clmNick.Name = "clmNick";
            this.clmNick.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clmNick.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.clmNick.Width = 61;
            // 
            // clmHostname
            // 
            this.clmHostname.HeaderText = "Hostname";
            this.clmHostname.Name = "clmHostname";
            this.clmHostname.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clmHostname.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.clmHostname.Width = 61;
            // 
            // clmPortNum
            // 
            this.clmPortNum.HeaderText = "Port";
            this.clmPortNum.Name = "clmPortNum";
            this.clmPortNum.Width = 51;
            // 
            // clmPass
            // 
            this.clmPass.HeaderText = "Password";
            this.clmPass.Name = "clmPass";
            this.clmPass.Width = 78;
            // 
            // lblHelp
            // 
            this.lblHelp.AutoSize = true;
            this.lblHelp.Location = new System.Drawing.Point(9, 29);
            this.lblHelp.Name = "lblHelp";
            this.lblHelp.Size = new System.Drawing.Size(528, 13);
            this.lblHelp.TabIndex = 9;
            this.lblHelp.Text = "To edit a row, double click the cell you want to edit. DON\'T FORGET TO CLICK SAVE" +
    " BEFORE YOU CLOSE!";
            // 
            // lblAdd
            // 
            this.lblAdd.AutoSize = true;
            this.lblAdd.Location = new System.Drawing.Point(9, 9);
            this.lblAdd.Name = "lblAdd";
            this.lblAdd.Size = new System.Drawing.Size(276, 13);
            this.lblAdd.TabIndex = 10;
            this.lblAdd.Text = "To add a server, click on the bottom row and start typing.";
            // 
            // Server_List_Manager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(611, 279);
            this.Controls.Add(this.lblAdd);
            this.Controls.Add(this.lblHelp);
            this.Controls.Add(this.dgvServers);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnDown);
            this.Controls.Add(this.btnUp);
            this.Name = "Server_List_Manager";
            this.Text = "Server List Manager";
            ((System.ComponentModel.ISupportInitialize)(this.dgvServers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridView dgvServers;
        private System.Windows.Forms.DataGridViewCheckBoxColumn clmAutoconnect;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmNick;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmHostname;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmPortNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmPass;
        private System.Windows.Forms.Label lblHelp;
        private System.Windows.Forms.Label lblAdd;
    }
}