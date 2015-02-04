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
using PigeonCms.Core.Helpers;

public partial class Controls_StaticPagesAdmin : PigeonCms.BaseModuleControl
{
    protected string PageName
    {
        get { return base.GetStringParam("PageName", "", "PageName"); }
    }

    int targetDocsUpload = 0;
    protected int TargetDocsUpload
    {
        get { return GetIntParam("TargetDocsUpload", targetDocsUpload); }
        set { targetDocsUpload = value; }
    }

    string docsUploadUrl = "";
    protected string DocsUploadUrl
    {
        get
        {
            if (string.IsNullOrEmpty(docsUploadUrl) && this.TargetDocsUpload > 0)
            {
                var menuMan = new MenuManager();
                var menuTarget = new PigeonCms.Menu();
                menuTarget = menuMan.GetByKey(this.TargetDocsUpload);
                docsUploadUrl = Utility.GetRoutedUrl(menuTarget);
            }
            return docsUploadUrl;
        }
    }

    protected new void Page_Init(object sender, EventArgs e)
    {
        base.Page_Init(sender, e);

        var config = new ContentEditorProvider.Configuration();
        config.FilesUploadUrl = this.DocsUploadUrl;
        config.PageBreakButton = false;
        config.ReadMoreButton = false;
        ContentEditorProvider.InitEditor(this, Upd1, config);

        foreach (KeyValuePair<string, string> item in Config.CultureList)
        {
            //title
            Panel pan1 = new Panel();
            pan1.CssClass = "form-group input-group";
            PanelPageTitle.Controls.Add(pan1);

            TextBox txt1 = new TextBox();
            txt1.ID = "TxtPageTitle" + item.Value;
            txt1.MaxLength = 200;
            txt1.CssClass = "form-control";
            txt1.ToolTip = item.Key;
            LabelsProvider.SetLocalizedControlVisibility(false, item.Key, txt1);
            pan1.Controls.Add(txt1);
            Literal lit1 = new Literal();
            lit1.Text = "<span class='input-group-addon'>" + item.Value + "</span>";
            pan1.Controls.Add(lit1);

            //description
            var txt2 = (Controls_ContentEditorControl)LoadControl("~/Controls/ContentEditorControl.ascx");
            txt2.ID = "HtmlText" + item.Value;
            txt2.Configuration = config;
            LabelsProvider.SetLocalizedControlVisibility(false, item.Key, txt2);
            PanelPageContent.Controls.Add(txt2);

            Literal lit2 = new Literal();
            lit2.Text = "&nbsp;[<i>" + item.Value + "</i>]<br /><br />";
            PanelPageContent.Controls.Add(lit2);


        }
        if (this.BaseModule.DirectEditMode)
        {
            BtnNew.Visible = false;
            BtnSave.Visible = false;
            BtnCancel.OnClientClick = "closePopup();";
            editPage(this.PageName);
        }

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        LblOk.Text = "";
        LblErr.Text = "";

        if (this.BaseModule.DirectEditMode)
        {
            if (string.IsNullOrEmpty(this.PageName))
                throw new ArgumentException();
            if (!StaticPagesManager.ExistPage(this.PageName))
                throw new ArgumentException();
        }
    }

    protected void ObjDs1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        var filter = new StaticPageFilter();
        filter.Visible = Utility.TristateBool.NotSet;
        if (!string.IsNullOrEmpty(this.PageName))
            filter.PageName = this.PageName;

        e.InputParameters["filter"] = filter;
        e.Arguments.SortExpression = "PageName";
    }

    protected void Grid1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            var item = new PigeonCms.StaticPage();
            item = (PigeonCms.StaticPage)e.Row.DataItem;

            LinkButton LnkSel = (LinkButton)e.Row.FindControl("LnkSel");
            LnkSel.Text = "<i class='fa fa-pgn_edit fa-fw'></i>";
            LnkSel.Text += Utility.Html.GetTextPreview(item.PageName, 30, "");
            if (string.IsNullOrEmpty(item.PageName))
                LnkSel.Text += Utility.GetLabel("NO_VALUE", "<no value>");
        }
    }

    protected void Grid1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            editPage(e.CommandArgument.ToString());
        }
        if (e.CommandName == "DeleteRow")
        {
            deletePage(e.CommandArgument.ToString());
        }
    }

    protected void Grid1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
            Utility.AddGlyph(Grid1, e.Row);
    }

    protected void MultiView1_ActiveViewChanged(object sender, EventArgs e)
    {
        if (this.BaseModule.DirectEditMode)
        {
            //list view not allowed (in case of js hacking)
            if (MultiView1.ActiveViewIndex == 0)
                MultiView1.ActiveViewIndex = 1;
               
        }
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        if (saveForm())
            MultiView1.ActiveViewIndex = 0;
    }

    protected void BtnApply_Click(object sender, EventArgs e)
    {
        saveForm();
        //ScriptManager.RegisterStartupScript(this, GetType(), ID, "closePopup();", true);
        //ScriptManager.RegisterClientScriptBlock(this, GetType(), ID, "closePopup();", true);
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 0;
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        editPage("");
    }


    #region private methods
    private void form2obj(StaticPage page1)
    {
        page1.PageName = TxtPageName.Text;
        page1.Visible = ChkVisibile.Checked;
        page1.ShowPageTitle = ChkShowPageTitle.Checked;
        page1.PageTitleTranslations.Clear();
        page1.PageContentTranslations.Clear();
        foreach (KeyValuePair<string, string> item in Config.CultureList)
        {
            TextBox t1 = new TextBox();
            t1 = (TextBox)PanelPageTitle.FindControl("TxtPageTitle" + item.Value);
            page1.PageTitleTranslations.Add(item.Key, t1.Text);

            var html1 = new Controls_ContentEditorControl();
            html1 = Utility.FindControlRecursive<Controls_ContentEditorControl>(this, "HtmlText"+item.Value);
            page1.PageContentTranslations.Add(item.Key, html1.Text);
        }
    }

    private void obj2form(StaticPage page1)
    {
        TxtPageName.Text = page1.PageName;
        ChkVisibile.Checked = page1.Visible;
        ChkShowPageTitle.Checked = page1.ShowPageTitle;
        foreach (KeyValuePair<string, string> item in Config.CultureList)
        {
            string sTitleTranslation = "";
            TextBox t1 = new TextBox();
            t1 = (TextBox)PanelPageTitle.FindControl("TxtPageTitle" + item.Value);
            page1.PageTitleTranslations.TryGetValue(item.Key, out sTitleTranslation);
            t1.Text = sTitleTranslation;

            string sPageContentTraslation = "";
            var html1 = new Controls_ContentEditorControl();
            html1 = Utility.FindControlRecursive<Controls_ContentEditorControl>(this, "HtmlText" + item.Value);
            page1.PageContentTranslations.TryGetValue(item.Key, out sPageContentTraslation);
            html1.Text = sPageContentTraslation;
        }
    }

    private void editPage(string pageName)
    {
        LblOk.Text = "";
        LblErr.Text = "";

        TxtPageName.Text = pageName;
        TxtPageName.Enabled = true;
        TxtId.Text = "";
        ChkShowPageTitle.Checked = true;
        foreach (KeyValuePair<string, string> item in Config.CultureList)
        {
            TextBox t1 = new TextBox();
            t1 = (TextBox)PanelPageTitle.FindControl("TxtPageTitle" + item.Value);
            t1.Text = "";

            var html1 = new Controls_ContentEditorControl();
            html1 = Utility.FindControlRecursive<Controls_ContentEditorControl>(this, "HtmlText" + item.Value);
            html1.Text = "";
        }
        if (pageName != "")
        {
            TxtId.Text = "1";
            TxtPageName.Enabled = false;
            StaticPage currPage = new StaticPage();
            currPage = new StaticPagesManager().GetStaticPageByName(pageName);
            obj2form(currPage);
        }
        MultiView1.ActiveViewIndex = 1;
    }

    private void deletePage(string pageName)
    {
        LblOk.Text = "";
        LblErr.Text = "";

        try
        {
            new StaticPagesManager().Delete(pageName);
            new CacheManager<StaticPage>("PigeonCms.StaticPage").Remove(pageName);
        }
        catch (Exception e)
        {
            LblErr.Text = RenderError(e.Message);
        }
        Grid1.DataBind();
    }

    private bool saveForm()
    {
        bool res = false;
        LblErr.Text = "";
        LblOk.Text = "";

        try
        {
            StaticPage p1 = new StaticPage();
            if (TxtId.Text == string.Empty)
            {
                form2obj(p1);
                p1 = new StaticPagesManager().Insert(p1);
            }
            else
            {
                p1 = new StaticPagesManager().GetStaticPageByName(TxtPageName.Text);//precarico i campi esistenti e nn gestiti dal form
                form2obj(p1);
                new StaticPagesManager().Update(p1);
            }
            new CacheManager<StaticPage>("PigeonCms.StaticPage").Remove(p1.PageName);

            Grid1.DataBind();
            LblOk.Text = RenderSuccess(Utility.GetLabel("RECORD_SAVED_MSG"));
            res = true;
        }
        catch (Exception e1)
        {
            LblErr.Text = RenderError(Utility.GetLabel("RECORD_ERR_MSG") + "<br />" + e1.ToString());
        }
        finally
        {
        }
        return res;
    }

    #endregion
}
