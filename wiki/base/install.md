#Installation

###Manual installation
For developers or deployment.

####Download source or binaries

Download the stable **website only** version from [download area](http://www.pigeoncms.com). 
This is the best and easiest way to start with Pigeon to create your modern website or intranet solution.

You can also download **full solution** from GitHub repository.
Choose this option if you want the full source code, including website and projects for `PigeonCms.Core` and `PigeonCms.Shop` assembly.
The repository may contains unstable version of the project. https://github.com/picce/pigeoncms

####Unzip files

Open zip file and save files in your favourite location (example: `c:\sites\pigeon`).

**Website** only version contains the folder `pigeoncms`

**Full solution** version contains the following folders:  
`pigeoncms` the website folder  
`projects` the folder contains the solution file `pigeoncms.sln` and the core project.

####Choose and configure webserver

**IIS Express with Visual Studio**

This is the easy way because it is used by default by Visual Studio. 
Just open solution file `pigeoncms.sln` or open as website only.

**IIS7 or higher**

You need to configure IIS if you want to setup a deployment enviroment or if you prefer to use it as web server in Visual Studio. 

- Create a new website and choose a name for it. 
- Choose ASP.NET v4.0 as application pool.
- Set website phisical path (example: `c:\sites\pigeon\pigeoncms`)
- Set website port (default is 80)
- Set files permissions
  - `/web.config` read/write. Once installaion completed should be set to read-only.
  - `/public` read/write.

####Configure SQL Server or higher

You need to prepare an empty database or use an existing one. The second option could be useful in sharing hosting to install multiple instances of Pigeon on the same database.
Keep in mind db credentials during installation wizar (host name, db name, user and password)

---

###Webmatrix installation
For frontenders or evaluation purpose.
Webmatrix enviroment has all that you need to run Pigeon.

**Coming soon**

