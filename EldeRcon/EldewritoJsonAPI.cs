using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace EldeRcon
{
    class EldewritoJsonAPI
    {
        

        // Colors according to https://gist.github.com/tyler-vs/193757be5904a89d258b
        // Tested and seem to be okay
        static public Dictionary<int,string> GetTeamColors ()
        {
            // Dictionary to hold team colors
            Dictionary<int, string> teamColors = new Dictionary<int, string>();

            teamColors.Add(0, "9B3332"); // Red
            teamColors.Add(1, "325992"); // Blue
            teamColors.Add(2, "90A560"); // Green
            teamColors.Add(3, "DB8B00"); // Orange
            teamColors.Add(4, "553E8F"); // Purple
            teamColors.Add(5, "CCAE2C"); // Gold
            teamColors.Add(6, "513714"); // Brown
            teamColors.Add(7, "FC8BB9"); // Pink

            return teamColors;
        }

        // Get the server's json
        static public server GetServerInfo(string hostname, int port)
        {

            // Set our target address
            string server_url = "http://" + hostname + ":" + port;

            // Set up our restsharp client
            var rest_client = new RestClient(server_url);

            // Prepare the request
            var request = new RestRequest("/", Method.GET);

            // Run it!
            try
            {
                IRestResponse<server> server_response = rest_client.Execute<server>(request);

                // Send it back
                return server_response.Data;
            }
            catch (Exception server_info_ex)
            {
                System.Windows.Forms.MessageBox.Show("Error getting server information:\n\n" + server_info_ex.Message);
                return null;
            }
            
            
            
        }

    }



    // Class for restsharp to parse core json with
    public class server
    {
        // Basic details
        public string name{ get; set; }
        public string port { get; set; }
        public string hostPlayer { get; set; }
        public string status { get; set; }

        // Toggled player abilities
        public int sprintEnabled { get; set; }
        public int sprintUnlimitedEnabled { get; set; }
        public int dualWielding { get; set; }
        public int assassinationEnabled { get; set; }

        // Server settings
        public Boolean isDedicated { get; set; }
        public Boolean votingEnabled { get; set; }
        public Boolean teams { get; set; }

        // Map/gamemode info
        public string map { get; set; }
        public string mapFile { get; set; }
        public string variant { get; set; }
        public string variantType { get; set; }

        // Player counts
        public int numPlayers { get; set; }
        public int maxPlayers { get; set; }

        // Mods/IDs
        public List<String> mods { get; set; }
        public string xnkid { get; set; }
        public string xnaddr { get; set; }

        // Holds scores for teams 0-7
        public List<String> teamScores { get; set; }

        // Player list
        public List<player> players { get; set; }

        // Version strings
        public string gameVersion { get; set; }
        public string eldewritoVersion { get; set; }
    }

    // Class to hold player information
    public class player
    {
        public string name { get; set; }
        public string serviceTag { get; set; }
        public int team { get; set; }
        public string uid { get; set; }
        public string primaryColor { get; set; } // Hex
        public Boolean isAlive { get; set; }
        public int score { get; set; }
        public int kills { get; set; }
        public int assists { get; set; }
        public int deaths { get; set; }
        public int betrayals { get; set; }
        public int timeSpentAlive { get; set; }
        public int suicides { get; set; }
        public int bestStreak { get; set; }
    }

    

}
