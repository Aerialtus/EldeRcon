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
        // Dictionary to hold team colors
        public Dictionary<int, string> teamColors = new Dictionary<int, string>();

        // Colors according to https://gist.github.com/tyler-vs/193757be5904a89d258b
        // Tested and seem to be okay
        private void prepare_colors()
        {
            teamColors.Add(0, "9B3332"); // Red
            teamColors.Add(1, "325992"); // Blue
            teamColors.Add(2, "90A560"); // Green
            teamColors.Add(3, "DB8B00"); // Orange
            teamColors.Add(4, "553E8F"); // Purple
            teamColors.Add(5, "CCAE2C"); // Gold
            teamColors.Add(6, "513714"); // Brown
            teamColors.Add(7, "FC8BB9"); // Pink
        }

    }

    // Class for restsharp to parse core json with
    public class server
    {
        // Basic details
        public string name;
        public int port;
        public string hostPlayer;
        public string status;

        // Toggled player abilities
        public int sprintEnabled;
        public int sprintUnlimitedEnabled;
        public int dualWielding;
        public int assassinationEnabled;

        // Server settings
        public Boolean isDedicated;
        public Boolean votingEnabled;
        public Boolean teams;

        // Map/gamemode info
        public string map;
        public string mapFile;
        public string variant;
        public string variantType;

        // Player counts
        public int numPlayers;
        public int maxPlayers;

        // Mods/IDs
        public string[] mods;
        public string xnkid;
        public string xnaddr;

        // Holds scores for teams 0-7
        public string[] teamScores;

        // Player list
        public List<player> players;

        // Version strings
        public string gameVersion;
        public string eldewritoVersion;
    }

    // Class to hold player information
    public class player
    {
        public string name;
        public string serviceTag;
        public int team;
        public string uid;
        public string primaryColor; // Hex
        public Boolean isAlive;
        public int score;
        public int kills;
        public int assists;
        public int deaths;
        public int betrayals;
        public int timeSpendAlive;
        public int suicides;
        public int bestStreak;
    }

    

}
