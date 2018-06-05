using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WebSocketSharp;
using Microsoft.VisualBasic.FileIO;
using System.IO;
using System.Security;
using System.Reflection;

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
        //List<SecureString> passwords = new List<SecureString>();

        // Hold our encrypted file password / iv
        public static SecureString enc_password = null;// { get; set; }
        public static byte[] iv = null;

        // Flag on wether our data is encrypted or not
        public static bool encrypted = false;

        // Set our paths
        public const string csv_path = @".\servers.csv";
        public const string iv_path = @".\servers.iv";

        // Hold our LV arrays
        List<ListViewItem[]> player_lv_items = new List<ListViewItem[]>();
        List<ListViewItem[]> teamscore_lv_items = new List<ListViewItem[]>();

        // Hold servers
        List<rcon_server> rcon_server_list;

        // Hold server name to index
        Dictionary<string, int> server_dictionary;

        // Color by number
        Dictionary<int, string> team_colors;

        // Track which server is connected in which tab
        List<rcon_server> server_in_tab_list = new List<rcon_server>();

        // Store the index of our "New..." tab, since clicking on that makes a new one
        int new_tab_index = 1;

        // Help background threads figure out what tab we're on
        int selected_tab_index = 0;

        // Stop our asyncs from trying to write fields when we're trying to close
        bool form_closing = false;

        // Flag to not refresh the player LV if we're in the right click menu
        bool playerLV_cmsOpen = false;

        public Main()
        {

            InitializeComponent();

            // Load our saved name from disk
            LoadName();

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
            //SecureString pw = null;
            //passwords.Add(pw);

            // Create a server entry for the tab
            rcon_server server = new rcon_server();
            server_in_tab_list.Add(server);

            // Create a bgresults string for our first tab
            String str = null;
            bg_command_results.Add(str);

            // Create an empty player list for the tab
            ListViewItem[] players = new ListViewItem[0];
            ListViewItem[] scores = new ListViewItem[0];
            player_lv_items.Add(players);
            teamscore_lv_items.Add(scores);

            // Set up our teamcolors dictionary
            team_colors = EldewritoJsonAPI.GetTeamColors();


        }


        private void Main_Shown(object sender, EventArgs e)
        {

            // Prevents flickering in playerLV by enabling doublebuffering through reflection
            // https://stackoverflow.com/a/15268338
            var doubleBufferPropertyInfo = lvPlayers.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            doubleBufferPropertyInfo.SetValue(lvPlayers, true, null);

            // Prevents flickering in team score LV by enabling doublebuffering through reflection
            // https://stackoverflow.com/a/15268338
            doubleBufferPropertyInfo = lvTeamScore.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            doubleBufferPropertyInfo.SetValue(lvTeamScore, true, null);

            // Grab our server list (if it exists)
            LoadServerList(true);
        }

        // Send our command to the server
        private void btnSend_Click(object sender, EventArgs e)
        {
            // Only send if we have a command AND an open connection
            if (txtCommand.Text.Trim() != String.Empty && websockets[tabServers.SelectedIndex].ReadyState == WebSocketState.Open)
            {
                // Split the command up
                string[] split_command = txtCommand.Text.Trim().Split(' ');
                

                // Intercept say commands to put our name in them
                if (!txtName.Text.IsNullOrEmpty() && split_command[0].ToLower() == "say" || split_command[0].ToLower() == "server.say")
                {
                    split_command[0] = "say " + txtName.Text.Trim() + ": ";

                    string command = null;

                    foreach (string part in split_command)
                    {
                        command += " " + part;
                    }

                    // Send the command
                    websockets[tabServers.SelectedIndex].Send(command.Trim());
                }
                else
                {
                    // Send the command
                    websockets[tabServers.SelectedIndex].Send(txtCommand.Text.Trim());
                }

                
                //ws.Send(txtCommand.Text.Trim());

                // Print our command in the console
                UpdateConsole("\n" + txtCommand.Text, tabServers.SelectedIndex);

                // Blank our command box
                txtCommand.Text = "";
            }
        }

        // Function to send a command and process the response in the background
        private void SendBGCommand(string command, int tab_index)
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
                bg_websockets[tab_index].Send(new System.Net.NetworkCredential(string.Empty,server_in_tab_list[tab_index].password).Password);

                
            };

            // Connect
            bg_websockets[tab_index].ConnectAsync();


        }


        // Function to launch in a background thread to update server info from the
        private void UpdateServerInfo(object sender, DoWorkEventArgs e)
        {

            // Figure out what tab we came from
            int tab_index = Int32.Parse(e.Argument.ToString());

            //try
            // {
            // Blank out our bg result
            bg_command_results[tab_index] = null;

            // Blank out our current mode
            //server_tabs[tab_index].current_map_mode = null;

            // Blank the player list
            //server_tabs[tab_index].players = null

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

            // Add that port to our server's connection string
            rcon_server_list[tab_index].connect_string = "Server.Connect " + rcon_server_list[tab_index].hostname + ":" + port.ToString();
            
            // Set that if we're in the current tab
            if (selected_tab_index == tab_index)
            {
                SetConnectText(rcon_server_list[tab_index].connect_string);
            }

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
                    string server_name;


                    // If we do have a nickname, use it
                    if (!server_in_tab_list[tab_index].nickname.IsNullOrEmpty())
                        server_name = server_in_tab_list[tab_index].nickname;

                    // If we don't have a nickname, user the server's name
                    else
                        server_name = server_info.name;


                    // Build the tab label
                    string current_mode;
                    const string lobby_mode = "Entering the Lobby";
                    if (server_info.status == "InLobby")
                    {
                        tab_label = server_name + ": In Lobby " + server_info.numPlayers + "/" + server_info.maxPlayers;
                        current_mode = lobby_mode;
                    }
                    else
                    {
                        tab_label = server_name + ": " + server_info.map + " - " + server_info.variant + " " + server_info.numPlayers + "/" + server_info.maxPlayers;
                        current_mode = "Currently Playing: " + server_info.map + " - " + server_info.variant;
                    }


                    // If we're reporting joins/leaves, compare the current/last list
                    // https://stackoverflow.com/a/3944816
                    // When we first connect, we don't have a player list to compare to yet (check for null)
                    if (cbReportJoinsLeaves.Checked && server_in_tab_list[tab_index].players != null)
                    {
                        // Flag to see if we had to report
                        //bool user_change = false;

                        // Get the time
                        string time = DateTime.Now.ToUniversalTime().ToString("MM/dd/yy HH:mm:ss");



                        // If we have anyone to report, insert a space before and after
                        //if (leavers.Count() > 0 || new_friends.Count() > 0)
                        //    UpdateConsole("", tab_index);

                        // Check who has left
                        var leavers = server_in_tab_list[tab_index].players.Where(p => !server_info.players.Any(p2 => p2.uid == p.uid));

                        // Report
                        foreach (player leaver in leavers)
                        {
                            if (!leaver.name.IsNullOrEmpty())
                            {
                                UpdateConsole("[" + time + "] " + leaver.name + " has left the server.", tab_index);
                               // user_change = true;
                            }
                        }

                        // If we have players in the server...
                        if (server_info.players.Count > 0)
                        { 
                            // ...Check who has arrived
                            var new_friends = server_info.players.Where(p => !server_in_tab_list[tab_index].players.Any(p2 => p2.uid == p.uid));

                            // Report
                            foreach (player friend in new_friends)
                            {
                                if (!friend.name.IsNullOrEmpty())
                                {
                                    UpdateConsole("[" + time + "] " + friend.name + " has joined the server.", tab_index);
                                    //user_change = true;
                                }

                            }
                        }
                        // If we have anyone to report, insert a space before and after
                        //if (user_change)
                         //   UpdateConsole("", tab_index);
                    }

                    // Copy the server's player list
                    server_in_tab_list[tab_index].players = server_info.players;


                    // Check if it's a new game or if a game has ended
                    // If so, print it in chat (if desired)
                    if (server_in_tab_list[tab_index].current_map_mode != current_mode)
                    {
                        // Hold on to our previous status
                        //string prev_status = server_tabs[tab_index].current_map_mode;

                        // Update our stored state regardless of the checkbox
                        server_in_tab_list[tab_index].current_map_mode = current_mode;

                        // If we came from the lobby, skip the first mode update to avoid a loading bug where the variant hasn't switched yet
                        //if (prev_status == lobby_mode)
                        //    return;


                        // If we wanted a report, include it
                        if (cbReportGameChange.Checked)
                        {
                            // https://blog.nicholasrogoff.com/2012/05/05/c-datetime-tostring-formats-quick-reference/
                            string time = DateTime.Now.ToUniversalTime().ToString("MM/dd/yy HH:mm:ss");

                            UpdateConsole("",tab_index);
                            UpdateConsole("[" + time + "] " + current_mode, tab_index);
                            UpdateConsole("", tab_index);
                        }
                    }


                    // Update the tab's title
                    UpdateTabTitle(tab_label, tab_index);



                    // Sort
                    if (server_info.teams)
                        server_info.players = server_info.players.OrderBy(a => a.team).ThenByDescending(a => a.score).ToList();
                    else
                        server_info.players = server_info.players.OrderByDescending(a => a.score).ToList();

                    // If we have players, get them ready for the LV
                    if (server_info.players.Count > 0)
                    {
                        // Set up an array of LV items
                        ListViewItem[] players = new ListViewItem[server_info.numPlayers];
                        ListViewItem[] scores = new ListViewItem[8];

                        // Go through each player
                        for (int ctr = 0; ctr < server_info.numPlayers; ctr++)
                        {
                            // Create our array
                            String[] lv_array = new string[8];


                            // Copy our values into the right players
                            lv_array[0] = String.Empty;
                            lv_array[1] = server_info.players[ctr].name;
                            lv_array[2] = server_info.players[ctr].score.ToString();
                            lv_array[3] = server_info.players[ctr].kills.ToString();
                            lv_array[4] = server_info.players[ctr].deaths.ToString();
                            lv_array[5] = server_info.players[ctr].assists.ToString();
                            lv_array[6] = server_info.players[ctr].betrayals.ToString();
                            lv_array[7] = server_info.players[ctr].uid;

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

                        // Team scores LV!
                        if (server_info.teams)
                        {
                            // Send an empty array to clear the list out
                            scores = new ListViewItem[0];
                            //Dictionary<int, int> teamscores = new Dictionary<int, int>();
                            List<teamscore> teamscores = new List<teamscore>();

                            // Add the scores to the dictionary
                            for (int ctr = 0; ctr < 8; ctr++)
                            {
                                // Create a teamscore
                                teamscore current_team = new teamscore();

                                // Add our properties
                                current_team.team_num = ctr;

                                // Check if we can find a single team member
                                var team_member = server_info.players.Find(a => a.team == ctr);

                                // Empty teams have a score of -1
                                if (team_member == null)// || current_team.team_score == -1)
                                    current_team.team_score = -1;
                                else
                                    current_team.team_score = Int32.Parse(server_info.teamScores[ctr]);

                                // Add it to the list
                                teamscores.Add(current_team);
                            }

                            // Sort 
                            // https://stackoverflow.com/questions/3062513/how-can-i-sort-generic-list-desc-and-asc#3062524
                            teamscores.Sort(); // descending sort

                            // create lv array
                            scores = new ListViewItem[4];

                            // Prepare our rows
                            ListViewItem row1 = new ListViewItem();
                            row1.UseItemStyleForSubItems = false;
                            ListViewItem row2 = new ListViewItem();
                            row2.UseItemStyleForSubItems = false;
                            ListViewItem row3 = new ListViewItem();
                            row3.UseItemStyleForSubItems = false;
                            ListViewItem row4 = new ListViewItem();
                            row4.UseItemStyleForSubItems = false;

                            // Put our stuff in our cells
                            row1 = BuildTwoColumnTeamScore(teamscores, 0, 4);
                            row2 = BuildTwoColumnTeamScore(teamscores, 1, 5);
                            row3 = BuildTwoColumnTeamScore(teamscores, 2, 6);
                            row4 = BuildTwoColumnTeamScore(teamscores, 3, 7);


                            // Add the rows to the array
                            scores[0] = row1;
                            scores[1] = row2;
                            scores[2] = row3;
                            scores[3] = row4;
                        }
                        else
                        {
                            // Send an empty array to clear the list out
                            scores = new ListViewItem[0];
                        }

                        // If we're the current tab, update now
                        if (selected_tab_index == tab_index)
                        {
                            UpdatePlayerLV(players, tab_index);
                            UpdateScoreLV(scores, tab_index);
                        }

                        // Update the stored info regardless
                        player_lv_items[tab_index] = players;
                        teamscore_lv_items[tab_index] = scores;
                    }
                    else
                    {
                        // Send an empty array to clear the list out
                        ListViewItem[] players = new ListViewItem[0];
                        ListViewItem[] scores = new ListViewItem[0];

                        // If this is the active tab, update the lv now
                        if (selected_tab_index == tab_index)
                        {
                            UpdatePlayerLV(players, tab_index);
                            UpdateScoreLV(scores, tab_index);
                        }

                        // Update the stored info regardless
                        player_lv_items[tab_index] = players;
                        teamscore_lv_items[tab_index] = players;
                    }


                }


                // Wait until our next scheduled refresh
                // Attempt to parse the text in the field
                if (!Int32.TryParse(txtRefreshSeconds.Text, out int wait_time))
                    wait_time = 5; // If we don't get a sensible value, use the default

                // Sleep it off!
                System.Threading.Thread.Sleep(1000 * wait_time);
            }
            /*}

           catch (Exception update_ex)
           {
               // If something goes wrong, at least report it

               // Build the error
               ListViewItem sad_row = new ListViewItem();
               sad_row.Text = "Unable to connect or process HTTP json";

               // Add it to our standard lv arrangement
               ListViewItem[] lv = new ListViewItem[1];
               lv[0] = sad_row;

               // Copy it to our multi-tab lv list
               player_lv_items[tab_index] = lv;

           }*/
        }

        // Function to construct a 2 column team score LV item
        private ListViewItem BuildTwoColumnTeamScore(List<teamscore> teamscores, int team1, int team2)
        {
            // Create the LV item
            String[] lv_row = new String[4];
            ListViewItem row = new ListViewItem(lv_row);
            row.UseItemStyleForSubItems = false;

            // Assemble the row
            if (teamscores[team1].team_score != -1)
            {
                row.SubItems[0].BackColor = ColorTranslator.FromHtml("#" + team_colors[teamscores[team1].team_num]);
                row.SubItems[1].Text = teamscores[team1].team_score.ToString();
            }
            if (teamscores[team2].team_score != -1)
            {
                row.SubItems[2].BackColor = ColorTranslator.FromHtml("#" + team_colors[teamscores[team2].team_num]);
                row.SubItems[3].Text = teamscores[team2].team_score.ToString();
            }

            // Send it back
            return row;

        }

        // Clear the combobox from a thread
        private void ClearComboBox()
        {

            // Bail out if the form is closing
            if (form_closing)
                return;

            // Invoke if needed
            if (InvokeRequired)
            {
                this.Invoke(new Action(ClearComboBox), new object[] { });
                return;
            }

            cmbLoadExisting.Items.Clear();
        }

        // Set the connect command text from a thread
        private void SetConnectText(string text_to_set)
        {

            // Bail out if the form is closing
            if (form_closing)
                return;

            // Invoke if needed
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(SetConnectText), new object[] { text_to_set });
                return;
            }

            txtConnectCommand.Text = text_to_set;
        }

        // Set the combobox item from a thread?
        // Not sure if we need this
        private void SetComboBoxIndex(int index)
        {

            // Bail out if the form is closing
            if (form_closing)
                return;

            // Invoke if needed
            if (InvokeRequired)
            {
                this.Invoke(new Action<int>(SetComboBoxIndex), new object[] { index });
                return;
            }

            // Set the index
            cmbLoadExisting.SelectedIndex = index; 
    }

        // Invoke function for our player listview
        private void UpdatePlayerLV (ListViewItem[] players, int tab_index)
        {

            // Bail out if the form is closing
            if (form_closing)
                return;

            // Invoke if needed
            if (InvokeRequired)
            {
                this.Invoke(new Action<ListViewItem[], int>(UpdatePlayerLV), new object[] { players, tab_index });
                return;
            }

            if (playerLV_cmsOpen)
                return;

            // Prepare the LV
            lvPlayers.BeginUpdate();
            lvPlayers.Items.Clear();

            // Add our items
            lvPlayers.Items.AddRange(players);

            // Resize to fit contents and headers
            lvPlayers.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            //lvPlayers.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            lvPlayers.Columns[7].Width = 0;

            // End the lv work
           lvPlayers.EndUpdate();
        }

        // Invoke function for our score listview
        private void UpdateScoreLV(ListViewItem[] team_scores, int tab_index)
        {

            // Bail out if the form is closing
            if (form_closing)
                return;

            // Invoke if needed
            if (InvokeRequired)
            {
                this.Invoke(new Action<ListViewItem[], int>(UpdateScoreLV), new object[] { team_scores, tab_index });
                return;
            }

            // If there are no teams or scores, clear/hide the LV
            if (team_scores.Count() == 0)
            {
                lvTeamScore.Items.Clear();
                lvTeamScore.Enabled = false;
            }
            else
            {
                // Enable
                lvTeamScore.Enabled = true;

                // Prepare the LV
                lvTeamScore.BeginUpdate();
                lvTeamScore.Items.Clear();

                // Add our items
                lvTeamScore.Items.AddRange(team_scores);

                // Resize to fit contents and headers
                lvTeamScore.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                //lvPlayers.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                //lvPlayers.Columns[7].Width = 0;

                // End the lv work
                lvTeamScore.EndUpdate();
            }

        }



        // Invoke function for our console
        private void UpdateConsole(string text_to_append,int target_console)
        {
            // Bail out if the form is closing
            if (form_closing)
                return;

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
            // Bail out if the form is closing
            if (form_closing)
                return;

            // Invoke if needed
            if (InvokeRequired)
            {
                this.Invoke(new Action<string, int>(UpdateTabTitle), new object[] { title_text, tab_index });
                return;              
            }

            // Figure out which tab we need to update
            string tab_name = "tab" + tab_index.ToString();
            TabPage target_tab = tabServers.Controls[tab_name] as TabPage;

            // Update the text if needed
            if (target_tab.Text != title_text)
                target_tab.Text = title_text;
        }

        // Add an item to the combobox
        private void AddToComboBox(string text)
        {
            // Bail out if the form is closing
            if (form_closing)
                return;

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
            // Bail out if the form is closing
            if (form_closing)
                return;

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
        private void ConnectToServer (rcon_server server,int tab_index,bool from_combobox)
        {

            // Close the existing connection if needed
            if (websockets[tabServers.SelectedIndex] != null && websockets[tabServers.SelectedIndex].ReadyState != WebSocketState.Open)
                websockets[tabServers.SelectedIndex].Close();

            // Create the socket
            try
            {
                // Prepare the socket
                websockets[tabServers.SelectedIndex] = new WebSocket("ws://" + server.hostname + ":" + server.port, "dew-rcon");
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
                // If we got an incoming chat message and we're set to clean those up
                if (cbTrimChat.Checked && e1.Data.Length > 0 && e1.Data.Substring(0,1).Equals("["))
                {   
                    // Confirm it with a ] at 18
                    if (e1.Data.Substring(18,1).Equals("]"))
                    {
                        // Figure out where some of our important points are
                        // Doing it this way allows for forward slashes in names and messages without breaking the feature
                        int closing_arrow = e1.Data.IndexOf('>');
                        int last_slash = e1.Data.LastIndexOf('/',closing_arrow);
                        int second_last_slash = e1.Data.LastIndexOf('/', last_slash - 1 );

                        // Split it up into parts
                        string part1 = e1.Data.Substring(0, second_last_slash);
                        string part2 = e1.Data.Substring(closing_arrow);
                        
                        // Print it
                        UpdateConsole(part1 + part2,tab_index);
                    }

                    // If the message that started with a bracket doesn't appear to be a time message, print it as is
                    else
                    {
                        UpdateConsole(e1.Data,tab_index);
                    }
                }

                // If we've just authenticated...
                else if (e1.Data.Equals("accept"))
                {
                    // This was a triumph!
                    UpdateConsole("Connected!", tab_index);

                    // Securely hold the pw in memory
                    //passwords[tab_index] = null;
                    //SecureString sec_password = new SecureString();
                    //foreach (char c in server.password)
                    //    sec_password.AppendChar(c);
                    

                    // Add it to our list
                    //passwords[tab_index] = sec_password;

                    // Stop any existing backgroundworker for this tab
                    if (bg_workers[tab_index] != null && bg_workers[tab_index].IsBusy)
                    {   
                        // Stop the worker
                        bg_workers[tab_index].CancelAsync();

                        // Wait for it to stop (this shouldn't take long)
                        while (bg_workers[tab_index].IsBusy)
                            System.Threading.Thread.Sleep(50);
                        
                    }

                    // Store the tab's server info in our list
                    server_in_tab_list[tab_index] = server;

                    // Start our new background worker
                    bg_workers[tab_index] = new BackgroundWorker();
                    bg_workers[tab_index].WorkerSupportsCancellation = true;
                    bg_workers[tab_index].DoWork += new DoWorkEventHandler(UpdateServerInfo);
                    bg_workers[tab_index].RunWorkerAsync(tab_index);

                }

                // If we got a deny
                else if (e1.Data.Equals("deny"))
                {
                    UpdateConsole("Access Denied! Check your settings and try again.",tab_index);
                }

                // Most of the time, just pass along our response
                else
                    UpdateConsole(e1.Data, tab_index);


            };

            // When it's opened, send our password
            websockets[tab_index].OnOpen += (sender2, e2) =>
            {

                // The password has to be our first line to the server
                websockets[tab_index].Send(new System.Net.NetworkCredential(string.Empty, server.password).Password);

                // Save the server to the recent list
                // Only save it if we didn't come from the combobox, which means it's already been saved
                if (!from_combobox)
                    SaveRecentServer(server.hostname,server.port.ToString(),server.password, cbSavePass.Checked);

            };

            websockets[tab_index].OnClose += (sender3, e3) =>
            {
                UpdateConsole("\nDisconnected from server!",tab_index);


                // If we do have a nickname, use it
                string server_name;
                if (rcon_server_list[tab_index].nickname != String.Empty)
                    server_name = rcon_server_list[tab_index].nickname;

                // If we don't have a nickname, user the server's name
                else
                    server_name = "Server " + tab_index;

                UpdateTabTitle( server_name + ": Disconnected", tab_index);
            };

            // Attempt to connect
            try
            {
                UpdateConsole("\nConnecting to " + server.hostname + ":" + server.port + "...", tab_index);

                // Use Async to not freeze up if we time out
                websockets[tabServers.SelectedIndex].ConnectAsync();
            }
            catch (Exception connect_ex)
            {
                // Nicely print errors
                UpdateConsole("\nError connecting:\n\n" + connect_ex.Message, tab_index);
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

            // Create a server object to connect to
            rcon_server server_to_connect_to = new rcon_server();
            server_to_connect_to.hostname = txtHostname.Text;
            server_to_connect_to.port = Int32.Parse(txtPort.Text);

            //SecureString sec_password = new SecureString();
            foreach (char c in txtPassword.Text)
                server_to_connect_to.password.AppendChar(c);

            //server_to_connect_to.password = txtPassword.Text;

            // Do it live!
            ConnectToServer(server_to_connect_to, tabServers.SelectedIndex,false);
            
        }

        // If we're closing the form, also check if we need to close the connection
        private void Form1_FormClosed(object sender, FormClosingEventArgs e)
        {
            // Let our threads know it's time to stop
            form_closing = true;

            // End the BW/Connections
            foreach (BackgroundWorker bw in bg_workers)
                if (bw != null && bw.IsBusy)
                    bw.CancelAsync();
            foreach (WebSocket socket in websockets)
                if (socket != null && socket.ReadyState == WebSocketState.Open)
                    socket.CloseAsync();
            foreach (WebSocket socket in bg_websockets)
                if (socket != null && socket.ReadyState == WebSocketState.Open)
                    socket.CloseAsync();

            // Save our name to disk, probably not needed, but better to be safe
            SaveName();

           }        



        // Load recent list
        private void LoadServerList(bool autoconnect = false)
        {
            // Remove any existing servers from our lists
            //            cmbLoadExisting.Items.Clear();
            ClearComboBox();
            AddToComboBox("Load Existing...");
            SetComboBoxIndex(0);

            server_dictionary = new Dictionary<string, int>();
            rcon_server_list = new List<rcon_server>();            

            // Stop now if there's no existing server list to read
            if (!File.Exists(csv_path))
                return;

            // Prepare a textfieldparser
            TextFieldParser csvdata = null;

            // Check for AES IV file
            if (File.Exists(iv_path))
            {
                // Set our flag
                encrypted = true;

                // Read files
                byte[] server_bytes = File.ReadAllBytes(csv_path);
                iv = File.ReadAllBytes(iv_path);

                // Stay in a loop until we've decrypted
                while (true)
                {
                    // Prompt for a password (if needed)
                    if (enc_password == null)
                    {
                        Form PWForm = new PasswordForm();
                        PWForm.ShowDialog(this);                    
                    }

                    // Byte our key together
                    byte[] key = Encoding.ASCII.GetBytes((new System.Net.NetworkCredential(string.Empty, Main.enc_password).Password));

                    // Attempt to decrypt
                    try
                    {
                        // Decrypt
                        string decrypted_string = MS_AES.MS_AES.DecryptStringFromBytes_Aes(server_bytes, key, iv);

                        // Send the stream to our TFP
                        csvdata = new TextFieldParser(new StringReader(decrypted_string));
                        break;
                    }
                    catch
                    {
                        enc_password = null;
                        MessageBox.Show("Error decrypting information. Please try again!","EldeRcon",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    }
                }
            }

            // If we're not encrypted, read as plain text
            else
            {
                csvdata = new TextFieldParser(csv_path);
            }

            // Read the file
            csvdata.Delimiters = new string[] { "," };

            // Go through the file
            int line = 0;
            int num_autoconnects = 0;
            while (true)
            {
                // Break up row into fields
                string[] fields = csvdata.ReadFields();

                // Bail out if we hit the end
                if (fields == null)
                    break;

                // Add it to our list
                rcon_server current_server = new rcon_server();

                // Grab the name
                string server_name = String.Empty;
                string server_string = String.Empty; 

                // Old format
                if (fields.Count() == 3)
                {
                    // Make the name the host:port
                    server_name = fields[0] + ":" + fields[1];

                    // Old format defaults
                    current_server.autoconnect = 0;
                    current_server.nickname = String.Empty;

                    // Copy the other settings
                    current_server.hostname = fields[0];
                    current_server.port = Int32.Parse(fields[1]);

                    // Check if we have a saved password
                    if (fields[2].IsNullOrEmpty())
                        current_server.password = null;
                    else
                    {
                        // Encrypt password in memory
                        current_server.password = new SecureString();

                        foreach (char c in fields[2])
                            current_server.password.AppendChar(c);
                    }

                    
                } 
                
                // New format
                else if (fields.Count() == 5)
                {
                    // Copy the name
                    if (fields[1] != String.Empty)
                        server_name = fields[1];
                    else
                        server_name = fields[2] + ":" + fields[3];

                    // Set our other fields
                    current_server.autoconnect = Int32.Parse(fields[0]);
                    current_server.nickname = fields[1];
                    current_server.hostname = fields[2];
                    current_server.port = Int32.Parse(fields[3]);


                    // Check if we have a password
                    if (fields[4].IsNullOrEmpty())
                        current_server.password = null;
                    else
                    {
                        // Encrypt password in memory
                        current_server.password = new SecureString();

                        foreach (char c in fields[4])
                            current_server.password.AppendChar(c);
                    }
                    

                    //current_server.password = fields[4];
                }

                // Add the server to our dictionary
                //server_dictionary.Add(server_name, line);

                // Add it to our list
                rcon_server_list.Add(current_server);

                // Add it to our combobox
                AddToComboBox(server_name);

                // If we're set to autoconnect, do it now
                if (autoconnect && current_server.autoconnect == 1)
                {

                    // Switch to the "New..." tab if we're past the first tab
                    if (num_autoconnects > 0)
                        tabServers.SelectedIndex = new_tab_index;

                    // Change to that entry in the combobox
                    cmbLoadExisting.SelectedIndex = line + 1;

                    // Increment our # of autoconnects
                    num_autoconnects++;

                }

                // Increment our index
                line++;
            }

            // If we autoconnected, go back to the first tab/combo entry when we're done
            if (autoconnect)
            {
                tabServers.SelectedIndex = 0;
                cmbLoadExisting.SelectedIndex = 0;
            }
        }


        // Add this server to our saved list
        private void SaveRecentServer (string hostname, string port, SecureString password, bool save_password)
        {
            // Prepare a new rcon server object
            rcon_server new_server = new rcon_server();
            
            // Check if this item is in our list
            int existing_index = -1;
            for (int server_index = 0;server_index < rcon_server_list.Count;server_index++)
            {
                // Check for a hostname and port match
                if (rcon_server_list[server_index].hostname == hostname)
                {
                    if (rcon_server_list[server_index].port == Int32.Parse(port))
                    {
                        // Check if our saved copy matches the passed checkbox
                        // If it does, bail out now
                        if (rcon_server_list[server_index].password == null && save_password == false)
                            return;
                        else if (rcon_server_list[server_index].password != null && save_password == true)
                            return;
                        else // If we got a hostname/port match and we need to update if the password should be stored or not, copy it
                            existing_index = server_index;
                    }
                }
            }

            // If there were no matches, copy what was passed to us
            if (existing_index == -1)
            {
                // Create our server object
                new_server.autoconnect = 0;
                new_server.nickname = String.Empty;
                new_server.hostname = hostname;
                new_server.port = Int32.Parse(port);

                // Include the password (if desired)
                if (save_password)
                    new_server.password = password;
                else 
                    new_server.password = null;

                // Add it to our list
                rcon_server_list.Add(new_server);
            }

            // If we have an existing server, update that entry
            else
            {
                // Include the password (if desired)
                if (save_password)
                    rcon_server_list[existing_index].password = password;

                // Erase it if it's already there
                else
                    rcon_server_list[existing_index].password = null;
            }

            // Update our copy on disk
            

            // Time to actually write it
            if (encrypted == false)
            {
                try
                {
                    // Write our servers
                    using (StreamWriter sw = File.CreateText(csv_path))
                    {
                        // Go through the list
                        foreach (rcon_server current_server in rcon_server_list)
                        {
                            // Write it
                            sw.WriteLine("\"" + current_server.autoconnect + "\",\"" + current_server.nickname + "\",\"" + current_server.hostname + "\",\"" + current_server.port + "\",\"" + (new System.Net.NetworkCredential(string.Empty, current_server.password).Password) + "\"");
                        }
                    }

                    // Update the combobox
                    LoadServerList();
                }
                catch (Exception write_ex)
                {
                    MessageBox.Show("Error writing server file:\n\n" + write_ex.Message);
                    return;
                }
            }

            // If we're encrypted, build the string we were going to save
            else
            {
                // String to hold our file
                String server_file = String.Empty;

                // Go through the list
                foreach (rcon_server current_server in rcon_server_list)
                {
                    // Append
                    server_file += "\"" + current_server.autoconnect + "\",\"" + current_server.nickname + "\",\"" + current_server.hostname + "\",\"" + current_server.port + "\",\"" + (new System.Net.NetworkCredential(string.Empty, current_server.password).Password) + "\"\r\n";
                }

                // Byte our key together
                byte[] key = Encoding.ASCII.GetBytes((new System.Net.NetworkCredential(string.Empty, Main.enc_password).Password));

                // Send it to our encryption function
                byte[] encrypted_server_file = MS_AES.MS_AES.EncryptStringToBytes_Aes(server_file, key,iv);
                key = null;

                // Write our CSV file to disk
                try
                {
                    // CSV
                    File.WriteAllBytes(Main.csv_path, encrypted_server_file);

                }
                catch (Exception enc_write_ex)
                {
                    MessageBox.Show("Error writing encrypted files to disk. Please try saving again though the server manager:\n\n" + enc_write_ex.Message);
                }
            }
        }

        // Connect to server from recent list
        private void cmbLoadExisting_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Don't process our built-in item
            if (cmbLoadExisting.SelectedItem.ToString().Contains("Load Existing..."))
                return;

            // Load the fields
            rcon_server selected_server = rcon_server_list[cmbLoadExisting.SelectedIndex - 1];// ; server_dictionary[selected_name].Split(',');

            // If we have the PW, try to connect now
            if (selected_server.password != null)
            {
                ConnectToServer(selected_server, tabServers.SelectedIndex, true);
            }                

            // If we don't have a PW, uncheck the save box to avoid writing one on accident
            else
            {   // Copy values
                txtHostname.Text = selected_server.hostname;
                txtPort.Text = selected_server.port.ToString();
                cbSavePass.Checked = false;
            }

                
            
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
            // Bail out if nobody is selected
            if (lvPlayers.SelectedItems.Count == 0)
                return;

            // Grab our selected row
            ListViewItem row = lvPlayers.SelectedItems[0];
            string player_name = row.SubItems[1].Text;

            // Force an update
            UpdatePlayerLV(player_lv_items[tabServers.SelectedIndex], tabServers.SelectedIndex);

            // Set up the PM command
            if (txtName.Text.IsNullOrEmpty())
                txtCommand.Text = "Server.PM " + player_name + " \"   \"";
            else
                txtCommand.Text = "Server.PM " + player_name + " \"" + txtName.Text.Trim() + ": \"";

            // FOCUS
            txtCommand.Focus();
        }

        // Function to send kick commands
        // Ban types are:
        //  0: None
        //  1: Temp
        //  2: Perm
        private void KickPlayer(int tab_index, int ban_type)
        {
            // Only allow kicking with an admin name
            if (txtName.Text.Trim() == String.Empty)
            {
                MessageBox.Show("Please enter a name in the 'name' box near the bottom left corner before kicking!");
                return;
            }

            // Bail out if nobody is selected
            if (lvPlayers.SelectedItems.Count == 0)
                return;

            // Grab our selected row
            ListViewItem row = lvPlayers.SelectedItems[0];

            // Figure out the identity of our player
            string player_name = row.SubItems[1].Text;
            string player_uid = row.SubItems[7].Text;

            // Force an update after we unpaused
            UpdatePlayerLV(player_lv_items[tabServers.SelectedIndex], tabServers.SelectedIndex);

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
                    kick_prompt = "kick and IP ban for 2 games";
                    kick_command = "Server.KickTempBanUid";
                    break;

                case 2:
                    kick_prompt = "kick and IP ban";
                    kick_command = "Server.KickBanUid";
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
                websockets[tabServers.SelectedIndex].Send("say " + txtName.Text + " has kicked " + player_name + " from the server.");
                websockets[tabServers.SelectedIndex].Send(command);
                
                // Print our command in the console
                UpdateConsole("\n" + txtCommand.Text, tabServers.SelectedIndex);

            }

        }

        private void btnManage_Click(object sender, EventArgs e)
        {
            // Open the server management form
            Form ServerManager = new Server_List_Manager();
            ServerManager.ShowDialog(this);

            // Update the combobox
            LoadServerList();
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
                //SecureString pw = null;
                //passwords.Add(pw);

                // Create a bgresults string for our tab
                String str = null;
                bg_command_results.Add(str);

                // Create a server entry for the tab
                rcon_server server = new rcon_server();
                server_in_tab_list.Add(server);

                // Create an empty player list for the tab
                ListViewItem[] players = new ListViewItem[0];
                player_lv_items.Add(players);
                ListViewItem[] scores = new ListViewItem[0];
                teamscore_lv_items.Add(scores);

                // Increment our new tab index
                // Incrementing before activating prevents this function from firing again
                new_tab_index++;

                // Activate our new tab
                tabServers.SelectedTab = tabServers.TabPages[new_tab_index - 1];
                
            }
            else
            {


                // Store the index for our BG threads
                selected_tab_index = tabServers.SelectedIndex;

                // Send an empty array to clear the list out
                // Probably not needed anymore
                //ListViewItem[] players = new ListViewItem[0];
                //UpdatePlayerLV(players, tabServers.SelectedIndex);

                // Switch the LV to that tab's info
                txtConnectCommand.Text = rcon_server_list[tabServers.SelectedIndex].connect_string;
                UpdatePlayerLV(player_lv_items[tabServers.SelectedIndex], tabServers.SelectedIndex);
                UpdateScoreLV(teamscore_lv_items[tabServers.SelectedIndex], tabServers.SelectedIndex);
            }
        }


        // Custom class to hold server information
        public class rcon_server
        {
            public int index;
            public int autoconnect;
            public string nickname;
            public string hostname;
            public int port;
            public SecureString password;// = new SecureString();
            public string current_map_mode = null;
            public List<player> players = null;
            public string connect_string = null;
        }

        private void lvPlayers_MouseUp(object sender, MouseEventArgs e)
        {
            // If we right clicked in the LV
            // Based on: http://stackoverflow.com/questions/1718389/right-click-context-menu-for-datagridview
            if (e.Button == MouseButtons.Right)
            {
                // Disable PM option if we don't have typing rights
                if (txtCommand.Enabled == false)
                {
                    cmsPlayerLV.Items[0].Enabled = false;
                }

                // Show our menu                
                cmsPlayerLV.Show(lvPlayers, new Point(e.X, e.Y));
               
            }
        }

        // Tool strip menu equivalents of the buttons
        private void sendMessageToolStripMenuItem_Click(object sender, EventArgs e) {   SendMessage(tabServers.SelectedIndex) ; }
        private void kickNoBanToolStripMenuItem_Click(object sender, EventArgs e) {     KickPlayer(tabServers.SelectedIndex, 0); }
        private void kickTempBanToolStripMenuItem_Click(object sender, EventArgs e) {   KickPlayer(tabServers.SelectedIndex, 1); }
        private void kickPermaBanToolStripMenuItem_Click(object sender, EventArgs e) {  KickPlayer(tabServers.SelectedIndex, 2); }

        private void btnShuffle_Click(object sender, EventArgs e)
        {
            // Only send if we have a command AND an open connection
            if (websockets[tabServers.SelectedIndex].ReadyState == WebSocketState.Open)
            {
                // Send the command
                websockets[tabServers.SelectedIndex].Send("Server.ShuffleTeams");
                //ws.Send(txtCommand.Text.Trim());

                // Print our command in the console
                UpdateConsole("\nServer.ShuffleTeams", tabServers.SelectedIndex);
            }
        }

        private void btnEndGame_Click(object sender, EventArgs e)
        {
            // Confirm first
            DialogResult result = MessageBox.Show("Are you sure you want to end this game?", "EldeRcon",MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Bail out if we didn't get a yes
            if (result != DialogResult.Yes)
                return;

            // Only send if we have a command AND an open connection
            if (websockets[tabServers.SelectedIndex].ReadyState == WebSocketState.Open)
            {
                // Send the command
                websockets[tabServers.SelectedIndex].Send("Game.Stop");
                //ws.Send(txtCommand.Text.Trim());

                // Print our command in the console
                UpdateConsole("\nGame.Stop", tabServers.SelectedIndex);
            }
        }

        private void btnReloadVotingJson_Click(object sender, EventArgs e)
        {
            // Only send if we have a command AND an open connection
            if (websockets[tabServers.SelectedIndex].ReadyState == WebSocketState.Open)
            {
                // Send the command
                websockets[tabServers.SelectedIndex].Send("Server.ReloadVotingJson");
                //ws.Send(txtCommand.Text.Trim());

                // Print our command in the console
                UpdateConsole("\nServer.ReloadVotingJson", tabServers.SelectedIndex);
            }
        }

        // Custom class to hold contacts, users, and group information
        // http://stackoverflow.com/questions/4188013/how-to-implement-icomparable-interface#4188041
        public class teamscore: IComparable<teamscore>
        {
            public int team_num;
            public int team_score;

            // Define how we want comparisons (such as sort) to work
            public int CompareTo(teamscore that)
            {
                // When sorting, do it by name
                if (this.team_score > that.team_score) return -1;
                if (this.team_score == that.team_score) return 0;
                return 1;
            }
        }

        // Disable LV updates while the right click menu is open
        private void cmsPlayerLV_Opening(object sender, CancelEventArgs e)
        {
            playerLV_cmsOpen = true;
        }

        // Re-enable player LV updates after and force an update immediately
        private void cmsPlayerLV_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            playerLV_cmsOpen = false;
        }

        // We need to have a special type of thread to put stuff on the clipboard
        // This spawns the thread
        // https://stackoverflow.com/a/13977171
        private void btnCopy_Click(object sender, EventArgs e)
        {
            // Only half understand how this works, but it's way more reliable
            new SetClipboardHelper(DataFormats.Text, rcon_server_list[selected_tab_index].connect_string).Go();

            // Show confirmation that we copied
            // https://stackoverflow.com/a/11885952
            ToolTip tt = new ToolTip();
            IWin32Window win = this;
            tt.Show("Copied!", btnCopy);
        }

        // Goes with StaHelper to copy stuff to the clipboard
        class SetClipboardHelper : StaHelper
        {
            readonly string _format;
            readonly object _data;

            public SetClipboardHelper(string format, object data)
            {
                _format = format;
                _data = data;
            }

            protected override void Work()
            {
                var obj = new System.Windows.Forms.DataObject(
                    _format,
                    _data
                );

                Clipboard.SetDataObject(obj, true);
            }
        }

        // Save the name when we leave the textbox
        private void txtName_Leave(object sender, EventArgs e)
        {
            SaveName();
        }

        // Save the name in the next box to disk
        private void SaveName ()
        {
            try
            {
                // Write our servers
                using (StreamWriter sw = File.CreateText(@".\name.txt"))
                {
                    // Write it
                    sw.Write(txtName.Text.Trim());
                }
            }
            catch (Exception name_save_ex)
            {
                MessageBox.Show("Error saving name to disk:\n\n" + name_save_ex.Message);
            }
        }

        // Load the saved name from disk
        private void LoadName()
        {
            // Path
            const string path = @".\name.txt";

            // Stop now if there's no existing server list to read
            if (!File.Exists(path))
                return;

            // Read the file into the box
            try
            {
                txtName.Text = File.ReadAllText(path);
            }
            catch (Exception name_read_ex)
            {
                MessageBox.Show("Error reading name from disk:\n\n" + name_read_ex.Message);
            }
        }
    }
}
