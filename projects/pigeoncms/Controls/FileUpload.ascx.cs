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

public partial class Controls_FileUpload: PigeonCms.FileUploadControl
{
    protected string LitFolder = "";
    protected string LitRestrictions = "";

    //public string FilePath
    //{
    //    get { return base.FilePath; }
    //    set 
    //    {
    //        base.FilePath = value;
    //        if (base.ShowWorkingPath)
    //            LitFolder = base.GetLabel("Folder", "Folder") + ": " + value + "<br />";
    //    }
    //}

    protected new void Page_Init(object sender, EventArgs e)
    {
        this.BaseModule = new Module();
        this.BaseModule.ModuleNamespace = "PigeonCms";
        this.BaseModule.ModuleName = "FileUpload";
        base.Page_Init(sender, e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        base.AddFields(PanelFields);

        LitRestrictions = "";

        if (!string.IsNullOrEmpty(base.FileExtensions))
            LitRestrictions += base.FileExtensions + "<br />";

        if (base.FileSize > 0)
            LitRestrictions += GetLabel("MaxSize") + " " + base.FileSize + "KB";
        if (base.FileSize < 0)
            LitRestrictions += GetLabel("DiskQuotaExceeded", "disk quota exceeded");

        if (string.IsNullOrEmpty(LitRestrictions))
            LitRestrictions = GetLabel("all", "all");

        if (base.ShowWorkingPath)
            LitFolder = base.GetLabel("Folder", "Folder") + ": " + base.FilePath + "<br />";

        CmdConfirm.Text = base.ButtonText;
    }

    protected void CmdConfirm_Click(object sender, EventArgs e)
    {
        LblSuccess.Text = "";
        LblError.Text = "";

        try
        {
            base.UploadFiles(PanelFields);
            LblSuccess.Text = this.SuccessText;
        }
        catch (UnauthorizedAccessException)
        {
            LblError.Text = this.ErrorText + "<br />"
                + "System.UnauthorizedAccessException: please contact website admin to allow write permission to current folder";
        }
        catch (ArgumentException ex)
        {
            LblError.Text = this.ErrorText + "<br />" + ex.Message;
        }
        catch (Exception ex)
        {
            LblError.Text = this.ErrorText + "<br />" + ex.ToString();
        }
    }
}
