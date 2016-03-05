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


public partial class Controls_Label : PigeonCms.BaseModuleControl
{

    private string resourceSet = "";
    public string ResourceSet
    {
        get { return GetStringParam("ResourceSet", resourceSet); }
        set { resourceSet = value; }
    }

    private string resourceId = "";
    public string ResourceId
    {
        get { return GetStringParam("ResourceId", resourceId); }
        set { resourceId = value; }
    }

    private string defaultValue = "";
    public string DefaultValue
    {
        get { return GetStringParam("DefaultValue", defaultValue); }
        set { resourceId = value; }
    }

    protected string LabelContent = "";


    protected void Page_Load(object sender, EventArgs e)
    {
        LabelContent = "";
        if (!string.IsNullOrEmpty(this.ResourceSet) && !string.IsNullOrEmpty(this.ResourceId))
        {
            var page = new PigeonCms.Engine.BasePage();
            LabelContent = page.GetLabel(this.ResourceSet, this.ResourceId, this.DefaultValue);
        }
    }
}
