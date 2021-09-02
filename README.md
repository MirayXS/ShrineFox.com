# ShrineFox.com
![](https://i.imgur.com/ssQNbji.gif)
## About
[ShrineFox.com](https://shrinefox.com) is an ``ASP.NET`` web application with several features. Mainly, it serves as a browser for the Amicitia modding community's mods, tools, guides and cheats. 
It automatically updates its listing via Gamebanana's API every 6 hours. 
Additional utilities for creating game patches and searching for specific game files are included. 
This repository also ties the website to its phpbb froum and wordpress blog/news/guides sites, sharing ``.CSS``/``.JS`` and generating HTML layouts for them.
## How it works
- A file named ``amicitia.tsv`` must be placed in the ``./App_Data/`` folder. It is a spreadsheet that can be opened/exported by Google Sheets. Here is a sample.  
- A file named ``pw.txt`` also goes in the ``./App_Data/`` folder. It contains the password for the Admin page, where you can update ``amicitia.tsv`` on demand, manage a Discord bot, and re-generate HTML on demand.

# Credits
## Gamebanana Webscraper
Code from [Aemulus Package Manager](https://github.com/TekkaGB/AemulusModManager) was used to achieve webscraping using [GBAPIv4](https://gamebanana.com/apiv4/).  
As such, this project is licensed under GPL-3.0.
