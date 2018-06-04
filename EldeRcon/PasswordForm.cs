using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            else
            {
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
                //Process.GetCurrentProcess().Kill();
                Environment.Exit(1);
               // Application.Exit();
                //Environment.FailFast();

                //Close();
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

            // If we're set, encrypt it and send it back
            Main.enc_password = new SecureString();
            
            foreach (char c in txtPW.Text)
                Main.enc_password.AppendChar(c);

            // Close window
            Close();
        }
    }
}
