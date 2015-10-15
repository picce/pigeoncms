using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Caching;
using System.Collections.Generic;
using PigeonCms;
using System.IO;

public partial class Controls_Default : PigeonCms.BaseModuleControl
{
    protected class SettingsGruopAdapter
    {
        public int Row { get; set; }
        public string Title { get; set; }
        public string Abstract { get; set; }
        public string IconClass { get; set; }
        public string PanelClass { get; set; }
        public string CollapseClass { get; set; }
    }

    /// <summary>
    /// Allow to update database version for components
    /// only if enabled and admin users
    /// </summary>
    public bool AllowVersionUpdate
    {
        get
        {
            bool res = false;
            bool allowVersionUpdate = GetBoolParam("AllowVersionUpdate", false);
            if (allowVersionUpdate && Roles.IsUserInRole("admin"))
            {
                res = true;
            }
            return res;
        }
    }

    FormField currentXmlType = null;
    protected FormField CurrentXmlType
    {
        get
        {
            if (currentXmlType == null && !string.IsNullOrEmpty(this.CurrentKey))
            {
                string keySet = getKeySetFromArgument(this.CurrentKey);
                string keyName = getKeyNameFromArgument(this.CurrentKey);

                var man = new AppSettingsManager2();
                var xmlType = man.GetKeySetXmlType(keySet, false);
                if (!string.IsNullOrEmpty(xmlType.Name))
                {
                    var firstMatch = xmlType.Params.Where(
                        o => o.Name.Equals(keyName)).FirstOrDefault();
                    if (firstMatch != null)
                        currentXmlType = firstMatch;
                }
                if (currentXmlType == null || string.IsNullOrEmpty(currentXmlType.Name))
                {
                    currentXmlType = new FormField();
                    currentXmlType.Name = keyName;
                    currentXmlType.Type = FormFieldTypeEnum.Text;
                }
            }
            if (currentXmlType == null)
                currentXmlType = new FormField();

            return currentXmlType;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        LblOk.Text = "";
        LblErr.Text = "";

        if (!Page.IsPostBack)
        {
            loadGroupsList("");
        }
        else
        {
            PanelValue.Controls.Clear();
            Control control2Add = FormBuilder.RenderControl(
                this.CurrentXmlType, "", "form-control");
            PanelValue.Controls.Add(control2Add);
        }
    }

    protected void RepGroups_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Header)
        {
            return;
        }

        var item = (SettingsGruopAdapter)e.Item.DataItem;

        var LitVersionInfo = (Literal)e.Item.FindControl("LitVersionInfo");
        var dbManProvider = new PigeonCms.DatabaseUpdateProvider(item.Title);
        if (dbManProvider.LastVersionInstalled.VersionId > 0)
            LitVersionInfo.Text += dbManProvider.LastVersionInstalled.ToString() + "<br>";
        if (dbManProvider.UpdatesListPending.Count > 0)
        {
            int lastIdx = dbManProvider.UpdatesListPending.Count - 1;
            var upgradeVersion = dbManProvider.UpdatesListPending[lastIdx];
            string versionSummary = 
                "versionId: " + upgradeVersion.VersionId.ToString() + "; "
                + "versionDate: " + upgradeVersion.VersionDate.ToShortDateString() + "; "
                + "versionDev: " + upgradeVersion.VersionDev + "; "
                + "versionNotes: " + upgradeVersion.VersionNotes;

            LitVersionInfo.Text += "<strong>UPGRADE AVAILABLE TO</strong><br>"
                + versionSummary + "<br>";

            var BtnUpdateDbVersion = (Button)e.Item.FindControl("BtnUpdateDbVersion");
            BtnUpdateDbVersion.Visible = true;
            BtnUpdateDbVersion.CommandArgument = item.Title;
        }

        var RepSettings = (Repeater)e.Item.FindControl("RepSettings");
        var man = new AppSettingsManager2();
        var list = man.GetByKeySet(item.Title);
        RepSettings.DataSource = list;
        RepSettings.DataBind();
    }

    protected void RepGroups_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        LblOk.Text = "";
        LblErr.Text = "";

        if (e.CommandName == "UpdateVersion")
        {
            bool res = false;
            string logResult = "";
            string componentFullName = (string)e.CommandArgument;

            var dbManProvider = new PigeonCms.DatabaseUpdateProvider(componentFullName);
            res = dbManProvider.ApplyPendingUpdates(out logResult);

            if (res)
            {
                LblOk.Text = RenderSuccess("Upgrade completed successfully!");
            }
            else
            {
                LblErr.Text = RenderError("Upgrade not completed. Check logs.<br>" + logResult);
            }
            loadGroupsList("");
        }
    }

    protected void RepSettings_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Header)
        {
            return;
        }

        var item = (AppSetting)e.Item.DataItem;

        var LnkName = (LinkButton)e.Item.FindControl("LnkName");
        LnkName.CommandArgument = item.KeySet + "|" + item.KeyName;
        LnkName.Text = "<i class='fa fa-pgn_edit fa-fw'></i>";
        LnkName.Text += Utility.Html.GetTextPreview(item.KeyName, 30, "");
        if (string.IsNullOrEmpty(item.KeyName))
            LnkName.Text += Utility.GetLabel("NO_VALUE", "<no value>");

        var LblKeyValue = (Literal)e.Item.FindControl("LblKeyValue");
        LblKeyValue.Text = Utility.Html.GetTextPreview(item.KeyValue, 40, "");

        var LnkDel = (LinkButton)e.Item.FindControl("LnkDel");
        LnkDel.CommandArgument = item.KeySet + "|" + item.KeyName;
    }

    protected void RepSettings_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string keySet = getKeySetFromArgument((string)e.CommandArgument);
        string keyName = getKeyNameFromArgument((string)e.CommandArgument);

        if (e.CommandName == "Select")
        {
            editRow(keySet, keyName);
        }
        if (e.CommandName == "DeleteRow")
        {
            deleteRow(keySet, keyName);
        }
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        LblErr.Text = "";
        LblOk.Text = "";

        try
        {
            var man = new AppSettingsManager2();
            var item = new AppSetting();

            string keySet = getKeySetFromArgument(this.CurrentKey);
            string keyName = getKeyNameFromArgument(this.CurrentKey);

            item = man.GetByKey(keySet, keyName);

            form2obj(item);
            if (string.IsNullOrEmpty(keyName))
            {
                item = man.Insert(item);
            }
            else
            {
                man.Update(item);
            }
            loadGroupsList(item.KeySet);
            LblOk.Text = RenderSuccess(Utility.GetLabel("RECORD_SAVED_MSG"));
            MultiView1.ActiveViewIndex = 0;
        }
        catch (Exception e1)
        {
            LblErr.Text = RenderError(Utility.GetLabel("RECORD_ERR_MSG") + "<br />" + e1.ToString());
        }
        finally
        {
        }
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 0;
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        editRow("", "");
    }

    protected void BtnApply_Click(object sender, EventArgs e)
    {
        LblErr.Text = "";
        LblOk.Text = "";

        try
        {
            //merge with xml settings to db
            var man = new AppSettingsManager2();
            int countNewEntries = man.MergeXmlSettings2Db();

            //cause settings refresh
            AppSettingsProvider.InvalidateAll();

            loadGroupsList("");

            string successString = "";
            successString += countNewEntries.ToString() + " " + GetLabel("NewSettingsAdded", "new settings added") + "<br>";
            successString += GetLabel("SettingsRefreshed", "Settings refreshed");
            LblOk.Text = RenderSuccess(successString);
        }
        catch (Exception e1)
        {
            LblErr.Text = RenderError(e1.ToString());
        }
        finally
        {
        }
    }

    /// <summary>
    /// load groups and drop
    /// </summary>
    /// <param name="currentKeySet"></param>
    private void loadGroupsList(string currentKeySet)
    {
        var man = new AppSettingsManager2();
        var settingsGroups = man.GetKetSetGroupsInstalled();
        var list = new List<SettingsGruopAdapter>();

        int row = 0;
        DropKeySet.Items.Clear();
        foreach (var item in settingsGroups)
        {
            var type = man.GetKeySetXmlType(item, true);
            var group = new SettingsGruopAdapter();
            group.Row = ++row;
            group.Title = item;
            group.Abstract = type.Description;
            group.IconClass = type.IconClass;
            group.PanelClass = type.PanelClass;
            if (item.Equals(currentKeySet, StringComparison.InvariantCultureIgnoreCase))
                group.CollapseClass = "collapse in";
            else
                group.CollapseClass = "collapse";

            list.Add(group);

            DropKeySet.Items.Add(new ListItem(item, item));
        }

        RepGroups.DataSource = list;
        RepGroups.DataBind();
    }

    private void clearForm()
    {
        Utility.SetDropByValue(DropKeySet, "");
        TxtKeyName.Text = "";
        TxtKeyTitle.Text = "";
        //TxtKeyValue.Text = "";
        TxtKeyInfo.Text = "";
    }

    private void form2obj(AppSetting obj)
    {
        obj.KeySet = DropKeySet.SelectedValue;
        obj.KeyName = TxtKeyName.Text;
        obj.KeyTitle = TxtKeyTitle.Text;
        //obj.KeyValue = TxtKeyValue.Text;
        obj.KeyInfo = TxtKeyInfo.Text;

        obj.KeyValue = FormBuilder.GetControlValue(this.CurrentXmlType, PanelValue);
    }

    private void obj2form(AppSetting obj)
    {
        Utility.SetDropByValue(DropKeySet, obj.KeySet);
        TxtKeyName.Text = obj.KeyName;
        TxtKeyTitle.Text = obj.KeyTitle;
        //TxtKeyValue.Text = obj.KeyValue;
        TxtKeyInfo.Text = obj.KeyInfo;

        PanelValue.Controls.Clear();
        Control control2Add = FormBuilder.RenderControl(
            this.CurrentXmlType, obj.KeyValue, "form-control");
        PanelValue.Controls.Add(control2Add);

    }

    private void editRow(string keySet, string keyName)
    {
        LblOk.Text = "";
        LblErr.Text = "";

        var man = new AppSettingsManager2();
        var obj = new AppSetting();

        clearForm();
        this.currentXmlType = null;
        this.CurrentKey = keySet + "|" + keyName;
        if (keyName != string.Empty)
        {
            obj = man.GetByKey(keySet, keyName);
            DropKeySet.Enabled = false;
            TxtKeyName.Enabled = false;
        }
        else
        {
            DropKeySet.Enabled = true;
            TxtKeyName.Enabled = true;
        }
        obj2form(obj);

        MultiView1.ActiveViewIndex = 1;
    }

    private void deleteRow(string keySet, string keyName)
    {
        LblOk.Text = "";
        LblErr.Text = "";
        try
        {
            var man = new AppSettingsManager2();
            man.DeleteByKey(keySet, keyName);
        }
        catch (Exception e)
        {
            LblErr.Text = RenderError(e.Message);
        }
        loadGroupsList(keySet);
    }

    private string getKeySetFromArgument(string commandArgument)
    {
        return commandArgument.Split('|')[0];
    }

    private string getKeyNameFromArgument(string commandArgument)
    {
        return commandArgument.Split('|')[1];
    }

}
