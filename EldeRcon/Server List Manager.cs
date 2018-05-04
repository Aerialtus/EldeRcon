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
            LoadServerList();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LoadServerList()
        {
            // Path
            const string path = @".\servers.csv";

            // Stop now if there's no existing server list to read
            if (!File.Exists(path))
                return;

            // Read the file
            TextFieldParser csvdata = new TextFieldParser(path);
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
                    // Copy our values
                 //   lv_row_string[0] = String.Empty;
                 //   lv_row_string[1] = fields[0];
                 //   lv_row_string[2] = fields[1];
                 //   lv_row_string[3] = fields[2];

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


                   // lv_row_string[0] = fields[2];
                   // lv_row_string[1] = fields[3];
                   // lv_row_string[2] = fields[4];
                    //lv_row_string[3] = fields[5];
                }

                // Add this row to the list
                //int new_row = dgvServers.Rows.Add(lv_row_string);
                
                // Figure out if we need to check the box
                //if (fields.Count() == 5 && fields[0] == "1")
                //    dgvServers.Rows[new_row].Cells[0].Value = true;
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
                // Hold our cell values
                string auto;
                string nickname = row.Cells[1].Value.ToString();
                string hostname = row.Cells[2].Value.ToString();
                string port = row.Cells[3].Value.ToString();
                string password = row.Cells[4].Value.ToString();

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
                servers.Add("\"" + auto + "\"," + nickname + "\"," + hostname + "\"," + port + "\"," + password + "\"");
            }

            // Write our list to disk
            try
            {
                // Write our servers
                using (StreamWriter sw = File.CreateText(@".\servers.csv"))
                {
                    foreach (string line in servers)
                    {
                        sw.WriteLine(line);
                    }
                }

                MessageBox.Show("List saved!");
            }
            catch (Exception write_ex)
            {
                MessageBox.Show("Error writing server list to disk:\n\n" + write_ex.Message);
            }
        }
    }
}
