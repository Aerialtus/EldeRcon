using System;
using System.Drawing;
using System.Windows.Forms;
using System.Security;

namespace EldeRcon
{
    public partial class PasswordForm : Form
    {
        // We're more likely to be collecting a password than setting one
        bool setting_password = false;

        public PasswordForm(bool passed_setting_password = false)
        {
            InitializeComponent();

            // Copy our passed value
            setting_password = passed_setting_password;

            // Change the wording if we're setting a password
            if (setting_password == true)
            {
                lblTop.Text = "Please enter your password in both boxes below. Going forward, you'll put this in when the program starts.";
                lblBottom.Text = "If you lose or forget this password, you'll have to delete the server files and start over.";
            }

            // If we're not setting a password
            else
            {
                // Move the buttons up and shrink the form
                btnGo.Location = new Point(btnGo.Location.X, btnGo.Location.Y - 40);
                btnCancel.Location = new Point(btnCancel.Location.X, btnCancel.Location.Y - 40);
                Height = Height - 40;

                // Hide PW box #2 if we're logging in
                txtPW2.Visible = false;
            }
        }


        // No password? No go!
        private void btnCancel_Click(object sender, EventArgs e)
        {
            // If we're starting up and the user attempts to bail out
            if (!setting_password)
            {
                // Exit
                Environment.Exit(1);
            }

            // Null the password if we're going back to the main form
            else
            { 
                // Code can check for our null and react to it appropriately
                Main.enc_password = null;
                Close();
            }
            
        }

        // Pass go!
        private void btnGo_Click(object sender, EventArgs e)
        {
            // Make sure we got a password
            if (txtPW.Text.Trim().Length == 0)
            {
                MessageBox.Show("Please enter your password before hitting OK.", "EldeRcon", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // If we're setting a password, make sure our first and second passwords are equal
            if (setting_password && !txtPW2.Text.Trim().Equals(txtPW.Text.Trim()))
            {
                MessageBox.Show("Your passwords don't match!\n\nPlease enter the same password twice.", "EldeRcon", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // If we're setting a password, generate an IV
            if (setting_password)
            {
                // Create it
                Main.iv = MS_AES.MS_AES.CreateIV(txtPW.Text);                                
            }

            // Give the user some indication of progress
            btnGo.Text = "Working...";
            btnGo.Enabled = false;
            btnCancel.Enabled = false;
            Refresh();

            // Hash the password
            byte[] hash = MS_AES.MS_AES.GetKeyFromPassword(txtPW.Text,Main.iv);

            // If we're set, encrypt it and send it back
            Main.enc_password = new SecureString();
            
            foreach (char c in hash)
                Main.enc_password.AppendChar(c);

            // Close window
            Close();
        }
    }
}
