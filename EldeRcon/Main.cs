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
using System.Security;

namespace EldeRcon
{
    public partial class Main : Form
    {
        // Hold our websockets
        List<WebSocket> websockets = new List<WebSocket>();
        List<WebSocket> bg_websockets = new List<WebSocket>();
        List<String> bg_command_results = new List<String>();

        // Hold our tab passwords for BG commands
        List<SecureString> passwords = new List<SecureString>();

        
        public Main()
        {
            
            InitializeComponent();

            // Set the combobox to "Load"
            cmbLoadExisting.SelectedIndex = 0;

            // Grab our server list (if it exists)
            LoadRecentList();

            // Create a websocket for the first tab
            WebSocket ws = null;
            websockets.Add(ws);
            
        }

        // Send our command to the server
        private void btnSend_Click(object sender, EventArgs e)
        {
            // Only send if we have a command AND an open connection
            if (txtCommand.Text.Trim() != String.Empty && websockets[tabServers.SelectedIndex].ReadyState == WebSocketState.Open)
            {
                // Send the command
                websockets[tabServers.SelectedIndex].Send(txtCommand.Text.Trim());
                //ws.Send(txtCommand.Text.Trim());

                // Print our command in the console
                UpdateConsole("\n" + txtCommand.Text, tabServers.SelectedIndex);

                // Blank our command box
                txtCommand.Text = "";
            }
        }

        // Function to send a command and process the response in the background
        private void SendBGCommand(string command,int tab_num)
        {

            // Copy the hostname/port we need from the main websocket for that tab
            string hostname = websockets[tab_num].Url.Host;
            int port = websockets[tab_num].Url.Port;

            try
            {
                // Create our background websocket
                bg_websockets[tab_num] = new WebSocket("ws://" + hostname + ":" + port, "dew-rcon");
            }
            catch
            {   // Happens with bad port #s and other oddities
                UpdateConsole("Error creating bg websocket. Please check your hostname/port!", tab_num);
                return;  
            }


            // Set up our return handler
            bg_websockets[tab_num].OnMessage += (sender1, e1) =>
            {
                // Check if we've authenticated
                if (e1.Data == "accept")
                {
                    // If so, send our command
                    bg_websockets[tab_num].Send(command);
                }

                // If it's not an accept, it's our result! Copy it
                else
                    bg_command_results[tab_num] = e1.Data;
            };

            // Set up our authentication handler
            bg_websockets[tab_num].OnOpen += (sender2, e2) =>
            {
                // Get the password from our securestring
                // https://stackoverflow.com/a/25751722
                bg_websockets[tab_num].Send(new System.Net.NetworkCredential(string.Empty, passwords[tab_num]).Password);
            };

            // Connect
            bg_websockets[tab_num].ConnectAsync();


        }

        // Invoke function for our console
        private void UpdateConsole(string text_to_append,int target_console)
        {
            // Invoke if needed
            if (InvokeRequired)
            {
                this.Invoke(new Action<string,int>(UpdateConsole), new object[] { text_to_append, target_console });
                return;
            }
            
            // Get the time
            string time = GetTime();

            // Figure out which control to write to
            // Build the tab/console names
            string tab_name = "tab" + target_console.ToString();
            string console_name = "txtConsole" + target_console.ToString();
            
            // Target our textbox, which is under a tabcontrol
            TabPage target_tab = tabServers.Controls[tab_name] as TabPage;
            TextBox target_textbox = target_tab.Controls[console_name] as TextBox;
            
            // Write our output
            target_textbox.AppendText("\r\n" + text_to_append.Replace("\n", Environment.NewLine));
            
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

        // Remove an item from the combobox
        private void RemoveFromComboBox(string text)
        {
            // Invoke if needed
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(RemoveFromComboBox), new object[] { text });
                return;
            }

            // Add the item
            cmbLoadExisting.Items.Remove(text);
        }


        // Connect to the server async 
        private void ConnectToServer (string hostname,int port,string password,int tab_index)
        {
            // Create the socket
            try
            {
                // Prepare the socket
                websockets[tabServers.SelectedIndex] = new WebSocket("ws://" + hostname + ":" + port, "dew-rcon");
            }
            catch
            {   // Happens with bad port #s and other oddities
                UpdateConsole("Error creating websocket. Please check your hostname/port!", tabServers.SelectedIndex);
                return;
            }

            // When we get something back, print it
            websockets[tab_index].OnMessage += (sender1, e1) =>
            {
                // Most of the time, just pass along our response
                if (e1.Data != "accept")
                    UpdateConsole(e1.Data, tab_index);

                // If we've just authenticated...
                else
                {
                    // This was a triumph!
                    UpdateConsole("Connected!", tab_index);

                    // Securely hold the pw in memory
                    SecureString sec_password = new SecureString();
                    foreach (char c in txtPassword.Text)
                    {
                        sec_password.AppendChar(c);
                    }

                    // Add it to our list
                    passwords[tab_index] = sec_password;
                }
                
            };

            // When it's opened, send our password
            websockets[tab_index].OnOpen += (sender2, e2) =>
            {
                // The password has to be our first line to the server
                websockets[tab_index].Send(password);

                // Update our window title
                UpdateWindowTitle("EldeRcon - " + hostname + ":" + port);

                // Save the server to the recent list
                UpdateServerList(txtHostname.Text, txtPort.Text, txtPassword.Text, cbSavePass.Checked);
            };

            // Attempt to connect
            try
            {
                UpdateConsole("\nConnecting to " + hostname + ":" + port + "...", tabServers.SelectedIndex);

                // Use Async to not freeze up if we timeout
                websockets[tabServers.SelectedIndex].ConnectAsync();
            }
            catch (Exception connect_ex)
            {
                // Nicely print errors
                UpdateConsole("\nError connecting:\n\n" + connect_ex.Message, tabServers.SelectedIndex);
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
            if (websockets[tabServers.SelectedIndex] != null && websockets[tabServers.SelectedIndex].ReadyState == WebSocketState.Open)
                    websockets[tabServers.SelectedIndex].Close();
                        
            // Do it live!
            ConnectToServer(txtHostname.Text,Int32.Parse(txtPort.Text),txtPassword.Text, tabServers.SelectedIndex);
            
        }

        // If we're closing the form, also check if we need to close the connection
        private void Form1_FormClosed(object sender, FormClosingEventArgs e)
        {
            foreach (WebSocket socket in websockets)
            if (socket != null && socket.ReadyState == WebSocketState.Open)
                socket.CloseAsync();
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
        private void UpdateServerList (string hostname, string port, string password, bool save_password)
        {
            // Check if it's already on our list
            if (cmbLoadExisting.Items.Contains(hostname + "," + port + "," + password))
                return;

            // Check if the no password version is on our list
            else if (cmbLoadExisting.Items.Contains(hostname + "," + port + ","))
                if (!save_password) // If we were asked to not save a pasword, we're set
                    return;
                else // If we were asked to save the PW this time, remove the old version
                    RemoveFromComboBox(hostname + "," + port + ",");
            
            // Append it to our control
            if (save_password)
                AddToComboBox(hostname + "," + port + "," + password);
            else
                AddToComboBox(hostname + "," + port + ",");

            // Set our path
            const string logpath = @".\servers.csv";

            // Build our server's csv string
            string server_string = "\"" + hostname + "\",\"" + port + "\",";

            // Add password (if desired)
            if (save_password)
                server_string += "\"" + password + "\"";

            // Time to actually write it
            try
            {
                // Write our servers
                using (StreamWriter sw = File.CreateText(logpath))
                {
                    // Go through the combobox
                    foreach (string server in cmbLoadExisting.Items)
                    {
                        // Skip our dummy entry
                        if (server == "Load Existing...")
                            continue;
                        else
                        {   // In theory, there's a chance a password will have a comma in it (though I haven't tested this)
                            // Since the hostname/port can't have commas (or I pray they don't), use them to figure out where the password starts

                            // Split the line up 
                            string[] server_parts = server.Split(',');

                            // Figure out where the second comma is based on length
                            int second_comma = server_parts[0].Length + server_parts[1].Length + 2;

                            // Assign our values
                            string cmb_host = server_parts[0];
                            string cmb_port = server_parts[1];

                            // If the second comma is at the end of the string, we don't have a password
                            string cmb_pass = String.Empty;
                            if (second_comma < server.Length)
                                cmb_pass = server.Substring(second_comma);

                            // Write it
                            sw.WriteLine("\"" + cmb_host + "\",\"" + cmb_port + "\",\"" + cmb_pass + "\"");
                        }
                    }
                }
            }
            catch (Exception write_ex)
            {
                MessageBox.Show("Error writing server file:\n\n" + write_ex.Message);
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
                ConnectToServer(txtHostname.Text, Int32.Parse(txtPort.Text), txtPassword.Text, tabServers.SelectedIndex);

            // If we don't have a PW, uncheck the save box to avoid writing one on accident
            else
                cbSavePass.Checked = false;
            
        }
    }
}
