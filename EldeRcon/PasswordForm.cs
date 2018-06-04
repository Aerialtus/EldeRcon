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
                lblTop.Text = "Enter a password to protect the server information.";
                lblBottom.Text = "If you lose or forget this password, you'll have to delete the server files and start over.";
            }
        }


        // No password? No go!
        private void btnCancel_Click(object sender, EventArgs e)
        {
            // If we're starting up and the user attempts to bail out
            if (!setting_password)
            {
                Application.Exit();
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

            // If we did, encrypt it and send it back
            Main.enc_password = new SecureString();
            
            foreach (char c in txtPW.Text)
                Main.enc_password.AppendChar(c);

            // Close window
            Close();
        }
    }
}
