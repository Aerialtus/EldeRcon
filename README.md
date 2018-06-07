# EldeRcon
EldeRcon is a simple tool I made to help me admin my [Eldewrito](https://github.com/ElDewrito/ElDorito/) servers. I thought I'd share!

## So, what does it look like?
These screenshots are outdated. I'll get new ones soon!
<br />
<img src="https://i.imgur.com/ZX1JO4W.png" width="1193" height="634">
<br />

Please keep in mind it doesn't normally look squished and funny like this. I took the screenshot too wide. [Click here for the unsquished version](https://i.imgur.com/ZX1JO4W.png).

The server list manager:
<br />
<img src="https://i.imgur.com/h8oX4Hy.png" width="611" height="310">
<br />

## Features

The main features of this client are:
* Tabbed server management
* Detailed game status (map, mode, number of players, names, scores, k/d/a/b)
* Buttons for some common commands
* Button to copy the connect command for your server (you don't have to wait on the server browser)
* See who joins/leaves your server
* See when games start/stop (useful when you read about a bug on a map after the game has ended or seeeing how long a match was)
* Right click a player to PM, kick, or ban a player
* Easily save, restore, and update the saved server list
* Automatically connect to servers at program startup
* Optionally encrypt your server information (hostname, port, and password) on disk to protect access to your servers


## How do I use it?
Put in the hostname/IP address of your server, the RCON port (look for "Game.RconPort"  in your "dewrito_prefs.cfg"), the password, click Connect, and you're good to go.

By default, it will save servers you've connected to in a "servers.csv" file. If you uncheck the "Save PW" box, the file will only have your hostname and port. You can reconnect to servers by clicking the dropdown box in the top left corner and choosing your server.

If you have "Server.SendChatToRconClients" enabled in your "dewrito_prefs.cfg", you'll see player chat on the server.

The program attempts to connect to your server's HTTP port to get detailed player activity. It will indicate player or team color depending on the mode. If you right click on a player, you can send them a PM (it prepares the command in the bottom box), kick them, or kick and ban them.

Click on the "New..." tab to open a tab that lets you connect to another server inside the same window.

Click on the "Manage" button near the top left corner to use the "Server Manager", which lets you easily add multiple servers and set some of them to automatically connect at program startup.

Click on the "Encrypt" checkbox to encrypt your server details. If you forget your password, delete the two "servers" files in the directory. I don't and will never have any of your passwords.

I'm working on buttons (or similar) for simple commands that might be useful to have quick access to. Please send any suggestions though the issue tracker.

## How do I not use it?
There's plenty of code that expects your port to be a number and could very well crash the program if you don't put in a number. I'll fix this at some point, but in the meantime, make sure your ports are numbers!

## What other tools do you know of?

Desktop:
* HORC: http://download.magicbennie.com/HaloOnline/HORC
* RconTool: https://github.com/jaron780/RconTool

Web:
* Pauwlo: https://eldewrito.pauwlo.fr/rcon 

## What's next?
In no particular order, I'd like to:
* Add buttons (or some other simple method) for common game commands. 
* Server list file protection (not a high priority right now).
* Highlight server tabs with new messages?
* Learn not to fail at git ;)
