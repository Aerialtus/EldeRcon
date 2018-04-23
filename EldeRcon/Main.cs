using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSocketSharp;
using Microsoft.VisualBasic.FileIO;
using System.IO;

namespace EldeRcon
{
    public partial class Main : Form
    {
        // Hold our websocket
        WebSocket ws;

        
        public Main()
        {
            
            InitializeComponent();

            // Set the combobox to "Load"
            cmbLoadExisting.SelectedIndex = 0;

            // Grab our server list (if it exists)
            LoadRecentList();
        }

        // Send our command to the server
        private void btnSend_Click(object sender, EventArgs e)
        {
            // Only send if we have a command AND an open connection
            if (txtCommand.Text.Trim() != String.Empty && ws.ReadyState == WebSocketState.Open)
            {
                // Send the command
                ws.Send(txtCommand.Text.Trim());

                // Print our command in the console
                UpdateConsole("\n" + txtCommand.Text);

                // Blank our command box
                txtCommand.Text = "";
            }
        }

        // Invoke function for our console
        private void UpdateConsole(string text_to_append)
        {
            // Invoke if needed
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(UpdateConsole), new object[] { text_to_append });
                return;
            }
            
            // Get the time
            string time = GetTime();
            
            // Write our output
            txtConsole.AppendText("\r\n" + text_to_append.Replace("\n", Environment.NewLine));
            
        }

        // Rename the window on connecting
        private void UpdateWindowTitle (string title_text)
        {
            // Invoke if needed
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(UpdateWindowTitle), new object[] { title_text });
                return;
            }

            // Update the text
            Text = title_text;
        }

        // Add an item to the combobox
        private void AddToComboBox(string text)
        {
            // Invoke if needed
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(AddToComboBox), new object[] { text });
                return;
            }

            // Add the item
            cmbLoadExisting.Items.Add(text);
        }

        // Connect to the server async 
        private void ConnectToServer (string hostname,int port,string password)
        {
            // Create the socket
            try
            {
                ws = new WebSocket("ws://" + hostname + ":" + port, "dew-rcon");
            }
            catch
            {   // Happens with bad port #s and other oddities
                UpdateConsole("Error creating websocket. Please check your hostname/port!");
                return;
            }

            // When we get something back, print it
            ws.OnMessage += (sender1, e1) =>
            {
                UpdateConsole(e1.Data);
            };

            // When it's opened, send our password
            ws.OnOpen += (sender2, e2) =>
            {
                // The password has to be our first line to the server
                ws.Send(password);

                // Update our window title
                UpdateWindowTitle("EldeRcon - " + hostname + ":" + port);

                // Save the server to the recent list
                SaveServerToRecent(txtHostname.Text, txtPort.Text, txtPassword.Text, cbSavePass.Checked);
            };

            // Attempt to connect
            try
            {
                UpdateConsole("\nConnecting to " + hostname + ":" + port + "...");

                // Use Async to not freeze up if we timeout
                ws.ConnectAsync();
            }
            catch (Exception connect_ex)
            {
                // Nicely print errors
                UpdateConsole("\nError connecting:\n\n" + connect_ex.Message);
            }

            
        }

        // Formatted time
        private string GetTime ()
        {
            return DateTime.Now.ToString("HH:mm:ss");
        }

        // Connect!
        private void btnConnect_Click(object sender, EventArgs e)
        {
            // Close the existing connection if needed
            if (ws != null && ws.ReadyState == WebSocketState.Open)
                ws.Close();
                        
            // Do it live!
            ConnectToServer(txtHostname.Text,Int32.Parse(txtPort.Text),txtPassword.Text);
            
        }

        // If we're closing the form, also check if we need to close the connection
        private void Form1_FormClosed(object sender, FormClosingEventArgs e)
        {
            if (ws != null && ws.ReadyState == WebSocketState.Open)
                ws.CloseAsync();
        }

       

        // Load recent list
        private void LoadRecentList()
        {
            // Path
            const string path = @".\servers.csv";

            // Stop now if there's no existing server list to read
            if (!File.Exists(path))
                return;            

            // Read the file
            TextFieldParser csvdata = new TextFieldParser(path);
            csvdata.Delimiters = new string[] { "," };

            // Go through the file
            bool top_line = true;
            while (true)
            {
                // Break up row into fields
                string[] fields = csvdata.ReadFields();

                // Bail out if we hit the end
                if (fields == null)
                    break;

                // Build the base string
                string server_string = fields[0] + "," + fields[1] + ",";

               // Add the PW if we have it
                if (fields.Count() == 3)
                    server_string += fields[2];               

                // Add it to our list
                AddToComboBox(server_string);
            }
        }


        // Add this server to our saved list
        private void SaveServerToRecent (string hostname, string port, string password, bool save_password)
        {
            // Check if it's already on our list
            if (cmbLoadExisting.Items.Contains(hostname + "," + port + "," + password))
                return;

            // Check if the no password version is on our list
            else if (cmbLoadExisting.Items.Contains(hostname + "," + port + ","))
                if (!save_password) // If we were asked to not save a pasword, we're set
                    return;
                else // If we were asked to save the PW this time, remove the old version
                    cmbLoadExisting.Items.Remove(hostname + "," + port + ",");
            
            // Append it to our control
            AddToComboBox(hostname + "," + port + "," + password);

            // Set our path
            const string logpath = @".\servers.csv";

            // Build our server's string
            string server_string = "\"" + hostname + "\",\"" + port + "\",";

            // Add password (if desired)
            if (save_password)
                server_string += "\"" + password + "\"";

            // Append the server
            try
            {
                // Write our headers
                using (StreamWriter sw = File.AppendText(logpath))
                {
                    sw.WriteLine(server_string);
                }
            }
            catch (Exception header_ex)
            {
                MessageBox.Show("Error writing server file:\n\n" + header_ex.Message);
                return;
            }

        }

        // Connect to server from recent list
        private void cmbLoadExisting_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Don't process our built-in item
            if (cmbLoadExisting.SelectedItem.ToString().Contains("Load Existing..."))
                return;            

            // Load the fields
            string[] selected_server = cmbLoadExisting.SelectedItem.ToString().Split(',');

            // Copy the fields
            txtHostname.Text = selected_server[0];
            txtPort.Text = selected_server[1];
            txtPassword.Text = selected_server[2];

            // If we have the PW, try to connect now
            if (selected_server[2] != String.Empty)
                ConnectToServer(txtHostname.Text, Int32.Parse(txtPort.Text), txtPassword.Text);
            
        }
    }
}
