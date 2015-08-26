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
using System.Collections.Generic;
using PigeonCms;
using PigeonCms.Core.Offline;

public partial class Controls_Default : PigeonCms.BaseModuleControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LblOk.Text = "";
        LblErr.Text = "";

        if (!Page.IsPostBack)
        {
            ChkOffline.Attributes.Add("onchange", "offlineWarning();");
            loadDropTemplates();
            loadData();
        }
        else
        {

        }

    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        LblErr.Text = "";
        LblOk.Text = "";

        try
        {
            var o1 = new OfflineManager();
            form2obj(o1);
            o1.SaveData();
            loadData();
            OfflineProvider.ResetOfflineStatus();
            LblOk.Text = Utility.GetLabel("RECORD_SAVED_MSG");
        }
        catch (Exception e1)
        {
            LblErr.Text = Utility.GetLabel("RECORD_ERR_MSG") + "<br />" + e1.ToString();
        }
        finally
        {
        }
    }


    #region private methods
    private void form2obj(OfflineManager obj)
    {
        obj.Title = TxtTitle.Text;
        obj.Message = TxtMessage.Text;
        obj.Offline = ChkOffline.Checked;
        obj.Template = DropTemplates.SelectedValue;
        //obj.OfflineFileName = 
        //obj.OnlineFileName = 
        //obj.OfflineDateTime =
        //obj.OnlineDateTime =
    }

    private void obj2form(OfflineManager obj)
    {
        TxtTitle.Text = obj.Title;
        TxtMessage.Text = obj.Message;
        ChkOffline.Checked = obj.Offline;
        Utility.SetDropByValue(DropTemplates, obj.Template);
    }

    private void loadData()
    {
        var o1 = new OfflineManager();
        o1.GetData();
        obj2form(o1);
    }

    private void loadDropTemplates()
    {
        DropTemplates.Items.Clear();
        foreach (var item in OfflineProvider.GetTemplatesList())
        {
            DropTemplates.Items.Add(new ListItem(item, item));
        }
    }

    #endregion
}
