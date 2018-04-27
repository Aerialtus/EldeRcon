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

        // Hold the workers
        List<BackgroundWorker> bg_workers = new List<BackgroundWorker>();

        // Background command results
        List<String> bg_command_results = new List<String>();

        // Hold our tab passwords for BG commands
        List<SecureString> passwords = new List<SecureString>();

        // Hold our player LV arrays
        List<ListViewItem[]> player_lv_items = new List<ListViewItem[]>();

        // Color by number
        Dictionary<int, string> team_colors;

        // Store the index of our "New..." tab, since clicking on that makes a new one
        int new_tab_index = 1;

        
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

            // Create a BG websocket for the first tab
            WebSocket bg_socket = null;
            bg_websockets.Add(bg_socket);

            // Create a BG worker for the first tab
            BackgroundWorker bg = null;
            bg_workers.Add(bg);

            // Create a password slot for our first tab
            SecureString pw = null;
            passwords.Add(pw);

            // Create a bgresults string for our first tab
            String str = null;
            bg_command_results.Add(str);

            // Create an empty player list for the tab
            ListViewItem[] players = new ListViewItem[0];
            player_lv_items.Add(players);

            // Set up our teamcolors dictionary
            team_colors = EldewritoJsonAPI.GetTeamColors();
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
        private void SendBGCommand(string command,int tab_index)
        {

            // Copy the hostname/port we need from the main websocket for that tab
            string hostname = websockets[tab_index].Url.Host;
            int port = websockets[tab_index].Url.Port;

            try
            {
                // Create our background websocket
                bg_websockets[tab_index] = new WebSocket("ws://" + hostname + ":" + port, "dew-rcon");
            }
            catch
            {   // Happens with bad port #s and other oddities
                UpdateConsole("Error creating bg websocket. Please check your hostname/port!", tab_index);
                return;  
            }


            // Set up our return handler
            bg_websockets[tab_index].OnMessage += (sender1, e1) =>
            {
                // Check if we've authenticated
                if (e1.Data == "accept")
                {
                    // If so, send our command
                    bg_websockets[tab_index].Send(command);
                }

                // If it's not an accept, it's our result! Copy it
                else
                    bg_command_results[tab_index] = e1.Data;
            };

            // Set up our authentication handler
            bg_websockets[tab_index].OnOpen += (sender2, e2) =>
            {
                // Get the password from our securestring
                // https://stackoverflow.com/a/25751722
                bg_websockets[tab_index].Send(new System.Net.NetworkCredential(string.Empty, passwords[tab_index]).Password);
            };

            // Connect
            bg_websockets[tab_index].ConnectAsync();


        }

        // Function to launch in a background thread to update server info from the
        private void UpdateServerInfo(object sender, DoWorkEventArgs e)
        {

            // Figure out what tab we came from
            int tab_index = Int32.Parse(e.Argument.ToString());

            try
            {
                // Blank out our bg result
                bg_command_results[tab_index] = null;

                // Send our command
                SendBGCommand("Server.Port", tab_index);

                // Wait for our result
                while (bg_command_results[tab_index] == null)
                {
                    // Wait between checks
                    System.Threading.Thread.Sleep(100);
                }

                // Copy the port
                int port = Int32.Parse(bg_command_results[tab_index]);

                // Copy the hostname we need from the main websocket for that tab
                string hostname = websockets[tab_index].Url.Host;

                // Figure out which control to read refresh time from
                // Build the tab/console names
                string tab_name = "tab" + tab_index.ToString();
                string console_name = "txtRefreshSeconds";// + tab_index.ToString();

                // Target our textbox, which is under a tabcontrol
                TabPage target_tab = tabServers.Controls[tab_name] as TabPage;
                TextBox target_textbox = target_tab.Controls[console_name] as TextBox;

                // If we're not supposed to stop
                while (bg_workers[tab_index].CancellationPending == false)
                {
                    // Ask the server for more detailed information
                    var server_info = EldewritoJsonAPI.GetServerInfo(hostname, port);

                    // If we get a real response back
                    if (server_info != null)
                    {
                        // Rename the tab
                        string tab_label = null;

                        if (server_info.status == "InLobby")
                        {
                            tab_label = server_info.name + ": In Lobby " + server_info.numPlayers + "/" + server_info.maxPlayers;
                        }
                        else
                        {
                            tab_label = server_info.name + ": " + server_info.map + " - " + server_info.variant + " " + server_info.numPlayers + "/" + server_info.maxPlayers;
                        }

                        // Update the tab's title
                        UpdateTabTitle(tab_label, tab_index);


                        //server_info.players = server_info.players.OrderBy(o => o.team).ToList();

                        server_info.players = server_info.players.OrderBy(a => a.team).ThenBy(b => b.kills).ToList();


                        // If we have players, get them ready for the LV
                        if (server_info.players != null)
                        {
                            // Set up an array of LV items
                            ListViewItem[] players = new ListViewItem[server_info.numPlayers];

                            // Go through each player
                            for (int ctr = 0; ctr < server_info.numPlayers; ctr++)
                            {
                                // Create our array
                                String[] lv_array = new string[7];


                                // Copy our values into the right players
                                lv_array[0] = String.Empty;
                                lv_array[1] = server_info.players[ctr].name;
                                lv_array[2] = server_info.players[ctr].kills.ToString();
                                lv_array[3] = server_info.players[ctr].deaths.ToString();
                                lv_array[4] = server_info.players[ctr].assists.ToString();
                                lv_array[5] = server_info.players[ctr].betrayals.ToString();
                                lv_array[6] = server_info.players[ctr].uid;

                                // Convert that to a LV item
                                ListViewItem row = new ListViewItem(lv_array);
                                row.UseItemStyleForSubItems = false;

                                // Set color based on if we're on teams
                                if (server_info.teams)
                                {
                                    row.SubItems[0].BackColor = ColorTranslator.FromHtml("#" + team_colors[server_info.players[ctr].team]);
                                }

                                // If we're not on a team, use our primarycolor
                                else
                                {
                                    row.SubItems[0].BackColor = ColorTranslator.FromHtml(server_info.players[ctr].primaryColor);
                                }

                                // Add that row to our list
                                players[ctr] = row;
                            }

                            // Send the array off to a better place
                            UpdatePlayerLV(players, tab_index);
                        }
                        else
                        {
                            // Send an empty array to clear the list out
                            ListViewItem[] players = new ListViewItem[0];

                            // If this is the active tab, update the lv now
                            if (tabServers.SelectedIndex == tab_index)
                            {
                                UpdatePlayerLV(players, tab_index);
                            }

                            // Update the stored info regardless
                            player_lv_items[tab_index] = players;

                        }
                    }


                    // Wait until our next scheduled refresh
                    // Attempt to parse the text in the field
                    if (!Int32.TryParse(txtRefreshSeconds.Text, out int wait_time))
                        wait_time = 5; // If we don't get a sensible value, use the default

                    // Sleep it off!
                    System.Threading.Thread.Sleep(1000 * wait_time);
                }
            }
            catch (Exception update_ex)
            {
                // If something goes wrong, at least report it

                // Build the error
                ListViewItem sad_row = new ListViewItem();
                sad_row.SubItems[1].Text = "Unable to connect or process HTTP json";

                // Add it to our standard lv arrangement
                ListViewItem[] lv = new ListViewItem[1];
                lv[0] = sad_row;

                // Copy it to our multi-tab lv list
                player_lv_items[tab_index] = lv;

            }
        }

        // Invoke function for our player listview
        private void UpdatePlayerLV (ListViewItem[] players, int tab_index)
        {
            // Invoke if needed
            if (InvokeRequired)
            {
                this.Invoke(new Action<ListViewItem[], int>(UpdatePlayerLV), new object[] { players, tab_index });
                return;
            }

            // Grab the appropriate tab's LV
            //string tab_name = "tab" + tab_index.ToString();
            //string lv_name = "lvPlayers" + tab_index.ToString();

            // Target our lv, which is under a tabcontrol
            //TabPage target_tab = tabServers.Controls[tab_name] as TabPage;
            //ListView lvPlayers = target_tab.Controls[lv_name] as ListView;

            // Prepare the LV
            lvPlayers.BeginUpdate();
            lvPlayers.Items.Clear();

            // Add our items
            lvPlayers.Items.AddRange(players);

            // Resize to fit contents and headers
            lvPlayers.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            lvPlayers.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            lvPlayers.Columns[6].Width = 0;

            // End the lv work
            lvPlayers.EndUpdate();
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
        private void UpdateTabTitle (string title_text, int tab_index)
        {
            // Invoke if needed
            if (InvokeRequired)
            {
                this.Invoke(new Action<string,int>(UpdateTabTitle), new object[] { title_text, tab_index });
                return;
            }

            // Figure out which tab we need to update
            string tab_name = "tab" + tab_index.ToString();
            TabPage target_tab = tabServers.Controls[tab_name] as TabPage;

            // Update the text
            target_tab.Text = title_text;
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
                UpdateTabTitle("Connecting...", tab_index);
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
                    passwords[tab_index] = null;
                    SecureString sec_password = new SecureString();
                    foreach (char c in txtPassword.Text)
                        sec_password.AppendChar(c);
                    

                    // Add it to our list
                    passwords[tab_index] = sec_password;

                    // Stop any existing backgroundworker for this tab
                    if (bg_workers[tab_index] != null && bg_workers[tab_index].IsBusy)
                    {   
                        // Stop the worker
                        bg_workers[tab_index].CancelAsync();

                        // Wait for it to stop (this shouldn't take long)
                        while (bg_workers[tab_index].IsBusy)
                            System.Threading.Thread.Sleep(50);
                        
                    }

                    // Start our new background worker
                    bg_workers[tab_index] = new BackgroundWorker();
                    bg_workers[tab_index].WorkerSupportsCancellation = true;
                    bg_workers[tab_index].DoWork += new DoWorkEventHandler(UpdateServerInfo);
                    bg_workers[tab_index].RunWorkerAsync(tab_index);

                }
                
            };

            // When it's opened, send our password
            websockets[tab_index].OnOpen += (sender2, e2) =>
            {
                // The password has to be our first line to the server
                websockets[tab_index].Send(password);

                // Save the server to the recent list
                UpdateServerList(txtHostname.Text, txtPort.Text, txtPassword.Text, cbSavePass.Checked);

            };

            // Attempt to connect
            try
            {
                UpdateConsole("\nConnecting to " + hostname + ":" + port + "...", tabServers.SelectedIndex);

                // Use Async to not freeze up if we time out
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
            foreach (BackgroundWorker bw in bg_workers)
                if (bw != null && bw.IsBusy)
                    bw.CancelAsync();
            foreach (WebSocket socket in websockets)
                if (socket != null && socket.ReadyState == WebSocketState.Open)
                    socket.CloseAsync();
            foreach (WebSocket socket in bg_websockets)
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

            // Now we can update our copy on disk
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

        // Message button handler
        private void btnSendMessage_Click(object sender, EventArgs e) { SendMessage(tabServers.SelectedIndex); }
        
        // Kick button handlers
        private void btnKick_Click(object sender, EventArgs e) {        KickPlayer(tabServers.SelectedIndex, 0); }
        private void btnKickTempBan_Click(object sender, EventArgs e) { KickPlayer(tabServers.SelectedIndex, 1); }
        private void btnKickPermBan_Click(object sender, EventArgs e) { KickPlayer(tabServers.SelectedIndex, 2); }
        
        // Function to send a PM to a player
        // We'll fill in the command and player name in the console
        private void SendMessage(int tab_index)
        {
            // Grab the appropriate tab's LV
            string tab_name = "tab" + tab_index.ToString();
            string lv_name = "lvPlayers" + tab_index.ToString();

            // Target our textbox, which is under a tabcontrol
            TabPage target_tab = tabServers.Controls[tab_name] as TabPage;
            ListView target_lv = target_tab.Controls[lv_name] as ListView;

            // Grab our selected row
            ListViewItem row = target_lv.SelectedItems[0];
            string player_name = row.SubItems[1].Text;

            // Set up the PM command
            txtCommand.Text = "Server.PM " + player_name + "\" \"";
        }

        // Function to send kick commands
        // Ban types are:
        //  0: None
        //  1: Temp
        //  2: Perm
        private void KickPlayer(int tab_index, int ban_type)
        {
            // Grab the appropriate tab's LV
            string tab_name = "tab" + tab_index.ToString();
            string lv_name = "lvPlayers" + tab_index.ToString();

            // Target our textbox, which is under a tabcontrol
            TabPage target_tab = tabServers.Controls[tab_name] as TabPage;
            ListView target_lv = target_tab.Controls[lv_name] as ListView;

            // Grab our selected row
            ListViewItem row = target_lv.SelectedItems[0];

            // Figure out the identity of our player
            string player_name = row.SubItems[1].Text;
            string player_uid = row.SubItems[6].Text;

            // Prepare our ban prompt/command
            string kick_prompt = null;
            string kick_command = null;

            // Figure out which prompt/command to use
            switch (ban_type)
            {
                case 0:
                    kick_prompt = "kick";
                    kick_command = "Server.KickUid";
                    break;

                case 1:
                    kick_prompt = "kick and temporarily ban (for 2 games by default)";
                    kick_command = "Server.TempBanUid";
                    break;

                case 2:
                    kick_prompt = "kick and permanently ban";
                    kick_command = "Server.BanUid";
                    break;
            }

            // Prompt
            DialogResult result = MessageBox.Show("Are you sure you want to " + kick_prompt + " " + player_name + " ?", "EldeRcon", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            // Execute Order 66
            if (result == DialogResult.Yes)
            {
                // Build the command
                string command = kick_command + " " + player_uid;

                // Send the command
                websockets[tabServers.SelectedIndex].Send(command);
                
                // Print our command in the console
                UpdateConsole("\n" + txtCommand.Text, tabServers.SelectedIndex);

            }

        }

        private void btnManage_Click(object sender, EventArgs e)
        {
            // Open the server management form
            Form ServerManager = new Server_List_Manager();
            ServerManager.ShowDialog();
        }

        // If someone tries to change tabs
        private void tabServers_SelectedIndexChanged(object sender, EventArgs e)
        {

            // If we need to, create a new tab, which is just a textbox with a few properties adjusted.
            if (tabServers.SelectedIndex == new_tab_index)
            {
                // Create the tabpage
                // https://stackoverflow.com/a/3737259
                TabPage new_page = new TabPage("Server " + (new_tab_index + 1));
                new_page.Name = "tab" + new_tab_index;

                // Create our control
                string console_name = "txtConsole" + new_tab_index.ToString();
                TextBox new_console = new TextBox();
                new_page.Controls.Add(new_console);

                // Set its properties
                new_console.Name = console_name;
                new_console.Multiline = true;
                new_console.Width = new_page.Width;
                new_console.Height = new_page.Height;
                new_console.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
                new_console.ScrollBars = ScrollBars.Both;

                // Add the page to the tabcontrol
                // Keep our "new tab"
                TabPage newtab = tabServers.TabPages[new_tab_index];

                // Remove the "new tab"
                tabServers.TabPages.Remove(tabServers.TabPages[new_tab_index]);

                // Add what we just built
                tabServers.TabPages.Add(new_page);

                // Add our "New..." back
                tabServers.TabPages.Add(newtab);                

                // Create a websocket for the tab
                WebSocket ws = null;
                websockets.Add(ws);

                // Create a BG websocket for the tab
                WebSocket bg_socket = null;
                bg_websockets.Add(bg_socket);

                // Create a BG worker for the tab
                BackgroundWorker bg = null;
                bg_workers.Add(bg);

                // Create a password slot for our tab
                SecureString pw = null;
                passwords.Add(pw);

                // Create a bgresults string for our tab
                String str = null;
                bg_command_results.Add(str);

                // Create an empty player list for the tab
                ListViewItem[] players = new ListViewItem[0];
                player_lv_items.Add(players);

                // Increment our new tab index
                // Incrementing before activating prevents this function from firing again
                new_tab_index++;

                // Activate our new tab
                tabServers.SelectedTab = tabServers.TabPages[new_tab_index - 1];
            }
            else
            {
                // Send an empty array to clear the list out
                ListViewItem[] players = new ListViewItem[0];
                UpdatePlayerLV(players, tabServers.SelectedIndex);

                // Switch the LV to that tab's info
                UpdatePlayerLV(player_lv_items[tabServers.SelectedIndex], tabServers.SelectedIndex);
            }
        }
    }
}
