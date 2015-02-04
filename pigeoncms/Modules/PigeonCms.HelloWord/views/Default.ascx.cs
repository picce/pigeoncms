using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Caching;
using PigeonCms;
using PigeonCms.Core.Helpers;

public partial class Controls_HelloWord: PigeonCms.BaseModuleControl
{
    protected string LitOutput = "";

    private string yourName = "";
    public string YourName
    {
        get { return GetStringParam("YourName", yourName); }
        set { yourName = value; }
    }

    private string gender = "";
    public string Gender
    {
        get { return GetStringParam("Gender", gender); }
        set { gender = value; }
    }

    private int age = 0;
    public int Age
    {
        get { return GetIntParam("Age", age); }
        set { age = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        var cache = new CacheManager<string>("PigeonCms.HelloWord");
        if (cache.IsEmpty(this.BaseModule.Id.ToString()))
        {
            LitOutput = "This is an HelloWord module.<br />"
            + "Theese are the params you set for the module; <br />"
            + "your name: " + this.YourName + "<br />"
            + "gender: " + this.Gender + "<br />"
            + "age: " + this.Age.ToString() + "<br />";
            cache.Insert(this.BaseModule.Id.ToString(), LitOutput);
        }
        else
        {
            LitOutput = cache.GetValue(this.BaseModule.Id.ToString());
        }
    }
}
