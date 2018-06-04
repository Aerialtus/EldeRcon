using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;
using System.IO;

namespace EldeRcon
{
    public partial class Server_List_Manager : Form
    {

        public Server_List_Manager()
        {
            InitializeComponent();

            // Check the box if we're encrypted
            if (Main.encrypted)
            {
                // Temporarily remove the evend handler
                cbEncrypt.CheckedChanged -= new System.EventHandler(this.cbEncrypt_CheckedChanged);

                // Check the box
                cbEncrypt.Checked = true;

                // Put the handler back on
                cbEncrypt.CheckedChanged += new System.EventHandler(this.cbEncrypt_CheckedChanged);
            }


            // Load our list
            LoadServerList();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LoadServerList()
        {
            // Path
            const string path = Main.csv_path;

            // Stop now if there's no existing server list to read
            if (!File.Exists(path))
                return;

            // Prepare a textfieldparser
            TextFieldParser csvdata = null;

            // Check for AES IV file
            // If we're encrypted
            if (Main.encrypted)
            {

                // Read files
                byte[] server_bytes = File.ReadAllBytes(Main.csv_path);
                byte[] iv_bytes = File.ReadAllBytes(Main.iv_path);

                // Byte our key together
                byte[] key = new byte[32];
                key = Encoding.ASCII.GetBytes((new System.Net.NetworkCredential(string.Empty, Main.enc_password).Password).PadRight(32).Substring(0, 32));

                // Decrypt
                string decrypted_string = MS_AES.MS_AES.DecryptStringFromBytes_Aes(server_bytes, key, iv_bytes);

                // Change response into a string
                //string server_strings = System.Text.Encoding.Default.GetString(decrypted_file;

                // Send the stream to TFP
                csvdata = new TextFieldParser(new StringReader(decrypted_string));
            }

            // If we're not encrypted, read as plain text
            else
            {
                csvdata = new TextFieldParser(Main.csv_path);
            }

            csvdata.Delimiters = new string[] { "," };

            // Checkbox idea from:
            // https://bytes.com/topic/visual-basic-net/insights/880195-putting-checkboxes-any-cell-listview
            // Wingdings 168 = Checked, 254 = unchecked
            // Unicode:
            // Unchecked = U+2610, 9744
            // Checked = U+2611, 9745

            // Go through the file
            while (true)
            {
                // Break up row into fields
                string[] fields = csvdata.ReadFields();

                // Bail out if we hit the end
                if (fields == null)
                    break;

                // Hold our row
                string[] lv_row_string = new String[4];

                // If we're in 3 field format, load it into 5 field style
                if (fields.Count() == 3)
                {

                    dgvServers.Rows.Add(
                        false, 
                        String.Empty, 
                        fields[0], 
                        fields[1], 
                        fields[2]);
                }

                // If we're in the newer format
                else
                {
                    bool checkbox = false;

                    if (fields[0] == "1")
                        checkbox = true;

                    // Copy our values
                    dgvServers.Rows.Add(
                        checkbox,
                        fields[1],
                        fields[2],
                        fields[3],
                        fields[4]);

                }

            }
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            // https://stackoverflow.com/a/9930585
            DataGridView dgv = dgvServers;
            try
            {
                int totalRows = dgv.Rows.Count;
                // get index of the row for the selected cell
                int rowIndex = dgv.SelectedCells[0].OwningRow.Index;
                if (rowIndex == 0)
                    return;
                // get index of the column for the selected cell
                int colIndex = dgv.SelectedCells[0].OwningColumn.Index;
                DataGridViewRow selectedRow = dgv.Rows[rowIndex];
                dgv.Rows.Remove(selectedRow);
                dgv.Rows.Insert(rowIndex - 1, selectedRow);
                dgv.ClearSelection();
                dgv.Rows[rowIndex - 1].Cells[colIndex].Selected = true;
            }
            catch { }

        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            // https://stackoverflow.com/a/9930585
            DataGridView dgv = dgvServers;
            try
            {
                int totalRows = dgv.Rows.Count;
                // get index of the row for the selected cell
                int rowIndex = dgv.SelectedCells[0].OwningRow.Index;
                if (rowIndex == totalRows - 1)
                    return;
                // get index of the column for the selected cell
                int colIndex = dgv.SelectedCells[0].OwningColumn.Index;
                DataGridViewRow selectedRow = dgv.Rows[rowIndex];
                dgv.Rows.Remove(selectedRow);
                dgv.Rows.Insert(rowIndex + 1, selectedRow);
                dgv.ClearSelection();
                dgv.Rows[rowIndex + 1].Cells[colIndex].Selected = true;
            }
            catch { }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            // get index of the row for the selected cell
            DataGridView dgv = dgvServers;
            int rowIndex = dgv.SelectedCells[0].OwningRow.Index;

            // Remove it
            DataGridViewRow selectedRow = dgv.Rows[rowIndex];
            dgv.Rows.Remove(selectedRow);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Use a list to hold the strings
            List<string> servers = new List<string>();

            // Go through each row
            foreach (DataGridViewRow row in dgvServers.Rows)
            {
                // No hostname = no save
                if (row.Cells[2].Value == null)
                    continue;

                // Hold our cell values
                string auto;
                string nickname = row.Cells[1].Value.ToString();
                string hostname = row.Cells[2].Value.ToString();
                string port = row.Cells[3].Value.ToString();
                string password = String.Empty;

                // Save the password if needed
                if (row.Cells[4].Value != null)
                    password = row.Cells[4].Value.ToString();

                // Make sure port is a number
                try
                {
                    Int32.Parse(port);
                }
                catch
                {
                    MessageBox.Show("'" + port + "' is not a valid port number!");
                    return;
                }

                // Store autoconnect as 0/1
                if (Convert.ToBoolean(row.Cells[0].Value) == false)
                {
                    auto = "0";
                }
                else
                {
                    auto = "1";
                }

                // Add it to our list
                servers.Add("\"" + auto + "\",\"" + nickname + "\",\"" + hostname + "\",\"" + port + "\",\"" + password + "\"");
            }

            // Write our list to disk
            // If we're not encrypted...
            if (Main.encrypted == false)
            {
                // Delete the iv file if it exists
                if (File.Exists(Main.iv_path))
                {
                    File.Delete(Main.iv_path);
                }

                // Save the servers
                try
                {
                    // Write our servers
                    using (StreamWriter sw = File.CreateText(Main.csv_path))
                    {
                        foreach (string line in servers)
                        {
                            sw.WriteLine(line);
                        }
                    }

                    //MessageBox.Show("List saved!");
                    Close();
                }
                catch (Exception write_ex)
                {
                    MessageBox.Show("Error writing server list to disk:\n\n" + write_ex.Message);
                }
            }

            // If we are encrypted
            else
            {
                // String to hold our file
                String server_file = String.Empty;

                // Go through the list
                foreach (string line in servers)
                {
                    // Append
                    server_file += line + "\r\n";
                }

                // Create an IV
                byte[] iv = MS_AES.MS_AES.CreateIV(Main.enc_password);

                // Byte our key together
                byte[] key = new byte[32];
                key = Encoding.ASCII.GetBytes((new System.Net.NetworkCredential(string.Empty, Main.enc_password).Password).PadRight(32).Substring(0,32));

                // Send it to our encryption function
                byte[] encrypted_server_file = MS_AES.MS_AES.EncryptStringToBytes_Aes(server_file, key, iv);
                key = null;

                // Write both files to disk
                try
                {                    
                    // CSV
                    File.WriteAllBytes(Main.csv_path, encrypted_server_file);

                    // IV
                    File.WriteAllBytes(Main.iv_path, iv);

                    Close();
                }
                catch (Exception enc_write_ex)
                {
                    MessageBox.Show("Error writing encrypted files to disk. Please try saving again:\n\n" + enc_write_ex.Message);
                }
            }

        }

        private void cbEncrypt_CheckedChanged(object sender, EventArgs e)
        {
            // If we're now checked
            if (cbEncrypt.Checked)
            {
                // Prompt
                DialogResult result = MessageBox.Show("Are you sure you want to encrypt your information?", "EldeRcon", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // If yes
                if (result == DialogResult.Yes)
                {
                    // If we got a yes, get a password
                    Form PWForm = new PasswordForm();
                    PWForm.ShowDialog(this);

                    // If we got a password                    
                    if (Main.enc_password != null)
                    {
                        // Set the flag
                        Main.encrypted = true;

                        // Reminder
                        MessageBox.Show("The change will take finish taking effect when you click save.", "EldeRcon", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    // If they cancelled on us without a password, go back to decrypted
                    else
                    {
                        MessageBox.Show("Encryption cancelled. Please try again if interested.");

                        // Temporarily remove the event handler
                        cbEncrypt.CheckedChanged -= new System.EventHandler(this.cbEncrypt_CheckedChanged);

                        // Check the box
                        cbEncrypt.Checked = false;

                        // Put the handler back on
                        cbEncrypt.CheckedChanged += new System.EventHandler(this.cbEncrypt_CheckedChanged);

                    }
                }

                // If we don't want to encrypt, uncheck the box and stop here
                else
                {
                    cbEncrypt.Checked = false;
                    return;
                }
            }

            // If we're now unchecked
            else
            {
                // Prompt
                DialogResult result = MessageBox.Show("Are you sure you want to decrypt your information?", "EldeRcon", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // If yes
                if (result == DialogResult.Yes)
                {
                    // Set the flag
                    Main.encrypted = false;

                    // Reminder
                    MessageBox.Show("The change will take effect when you click save.", "EldeRcon", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
