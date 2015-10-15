#Upgrade version
---
###Backup website and database
Before upgrading you need to backup you website files and database. If something go wrong you will be able to restore the previous version.

###Manual files upgrade
Download thw updated version from [download area](http://www.pigeoncms.com). If any, apply specific versione upgrade instructions.

**Website folder update**  
Copy and overwrite the following folders in your site:
- `/App_Code`
- `/App_Themes`
- `/Bin`
- `/Controls`
- `/Handlers`
- `/Js`
- `/offline`
- `/pgn-admin`
- `/Ws`
- `/Default.aspx`
- `/Default.aspx.cs`
- `/Global.asax`

Then remove `/pgn-admin/installation/install.txt` file from your website

**Projects folder update**  
Delete `/projects` folder content except your own projects and then copy the updated content from the zip file.

###Upgrade database
**Automatic mode**

You can go in admin area *Settings* and then check if *Upgrade* button is available in `PigeonCms.Core` section.

**Manual mode (if automatic mode is not available)**

You need to run the upgrade scripts manually (for example from Sql Server Management Studio).
- open `/pgn-admin/settings/PigeonCms.Core/updates.xml` file
- check current db version in `#__dbVersion` table
- copy and paste sql in sql console (for example from Sql Server Management Studio)
- replace all occurencies of table placeholder `#__` with your tables placeholder (for example `pgn_`)
- run the script
