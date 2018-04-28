# EldeRcon
EldeRcon is a simple tool I made to help me admin my [Eldewrito](https://github.com/ElDewrito/ElDorito/) servers. I thought I'd share!

## So, what does it look like?
<img src="https://imgur.com/a/JKOlWR2" width="1193" height="634" align="left">

Please keep in mind it doesn't normally look squished and funny like this. I took the screenshot too wide! [Click here for the unsquished version](https://imgur.com/a/JKOlWR2).

## How do I use it?
Put in the hostname/IP address of your server, the RCON port (look for "Game.RconPort"  in your "dewrito_prefs.cfg"), the password, click Connect, and you're good to go.

By default, it will save servers you've connected to in a "servers.csv" file. If you uncheck the "Save PW" box, the file will only have your hostname and port. You can reconnect to servers by clicking the dropdown box in the top left corner and choosing your server.

If you have "Server.SendChatToRconClients" enabled in your "dewrito_prefs.cfg", you'll see player chat on the server.

The program attempts to connect to your server's HTTP port to get detailed player activity. It will indicate player or team color depending on the mode. Please note that I've only tested this on my team servers so far.

I've recently added tabs! Click on the "New..." tab to open a tab that lets you connect to another server inside the same window.

## What other tools do you know of?
Pauwlo has written a web based tool over at https://eldewrito.pauwlo.fr/rcon . If web management is your thing or you want something more elaborate that what's here, you should definitely check it out!

## What's next?
In no particular order, I'd like to:
* Improve the saving/loading feature a bit. 
* Add buttons for common game commands. 
* Server Nicknames
* Server list file protection
* Highlight server tabs with new messages?
* Learn not to fail at git ;)
