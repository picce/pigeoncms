using System;
using System.Collections.Generic;
using System.Linq;
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

public partial class Controls_Default : PigeonCms.MemberEditorControl
{
	const int PANEL_SEE_IDX = 0;
	const int PANEL_INS_IDX = 1;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Roles.IsUserInRole("admin"))
            throw new HttpException(404, "Page not found");

        if (!Page.IsPostBack)
        {
            loadList();
        }
        else
        {
            string eventArg = HttpContext.Current.Request["__EVENTARGUMENT"];
            if (eventArg.StartsWith("search.pigeon|"))
            {
                //event triggered by PigeonModern.master js
                //event listener needed in module
                string data = eventArg.Split('|').ToList()[1];
                this.MasterFilter.Value = data;
                loadList();
            }
        }
    }

    protected void TxtUserNameFilter_TextChanged(object sender, EventArgs e)
    {
		try { loadList(); }
        catch (Exception ex)
        {
            setError(ex.Message);
        }
    }

	protected void RepPaging_ItemDataBound(object sender, RepeaterItemEventArgs e)
	{
		if (e.Item.ItemType == ListItemType.Header)
		{
			return;
		}

		int page = int.Parse(e.Item.DataItem.ToString());
		if (page - 1 == base.ListCurrentPage)
		{
			var BtnPage = (LinkButton)e.Item.FindControl("BtnPage");
			BtnPage.CssClass = "selected";
		}
	}

	protected void RepPaging_ItemCommand(object source, RepeaterCommandEventArgs e)
	{
		if (e.CommandName == "Page")
		{
			base.ListCurrentPage = int.Parse(e.CommandArgument.ToString()) - 1;
			loadList();
		}
	}

	protected void Rep1_ItemCommand(object source, RepeaterCommandEventArgs e)
	{
		if (e.CommandName == "Select")
		{
			editRow(e.CommandArgument.ToString());
		}
		if (e.CommandName == "Password")
		{
			editPwd(e.CommandArgument.ToString());
		}
		if (e.CommandName == "Roles")
		{
			editRoles(e.CommandArgument.ToString());
		}
		if (e.CommandName == "DeleteRow")
		{
			deleteRow(e.CommandArgument.ToString());
		}
	}

	protected void Rep1_ItemDataBound(object sender, RepeaterItemEventArgs e)
	{
		if (e.Item.ItemType == ListItemType.Header)
		{
			return;
		}

		var currItem = PgnUserCurrent.GetUser(((MembershipUser)e.Item.DataItem).UserName);

		//var LitEdit = (Literal)e.Item.FindControl("LitEdit");
		//LitEdit.Text = ""
		//+ "<a href='javascript:void(0)' onclick=\"editRow('edit__" + item.ResourceSet + "|" + item.ResourceId + "');\" class='table-modern--media' data-title-mobile='edit'>"
		//+ "  <div class='table-modern--media--wrapper'>"
		//+ "    <div class='table-modern--media--modify'></div>"
		//+ "  </div>"
		//+ "</a>";


		var LitMoreInfo = (Literal)e.Item.FindControl("LitMoreInfo");
        LitMoreInfo.Text = "";

        if (!string.IsNullOrEmpty(currItem.Email))
        {
            LitMoreInfo.Text = "<a href='mailto:" + currItem.Email + "'>"
            + Utility.Html.GetTextPreview(currItem.Email, 30, "")
            + "</a><br>";
        }

        if (!string.IsNullOrEmpty(currItem.CompanyName))
            LitMoreInfo.Text += currItem.CompanyName + "<br />";
		if (!string.IsNullOrEmpty(currItem.FirstName + currItem.SecondName))
            LitMoreInfo.Text += currItem.FirstName + " " + currItem.SecondName + "<br />";
        if (!string.IsNullOrEmpty(currItem.NickName))
            LitMoreInfo.Text += "[" + currItem.NickName + "]<br />";

        //if (!string.IsNullOrEmpty(LitMoreInfo.Text))
        //    LitMoreInfo.Text = "<br>" + LitMoreInfo.Text;

        //meta info
        var LitMeta = (Literal)e.Item.FindControl("LitMeta");
        var metaMan = new PgnUserMetaManager();
        var metaFilter = new PngUserMetaFilter();
        metaFilter.Username = currItem.UserName;
        var metaList = metaMan.GetByFilter(metaFilter, "");
        string metaText = "";
        foreach(var meta in metaList)
        {
            metaText += "{MetaKey}: {MetaValue}<br>"
                .Replace("{MetaKey}", meta.MetaKey)
                .Replace("{MetaValue}", meta.MetaValue);
        }
        LitMeta.Text = metaText;

        //permissions
        var LitPermissions = (Literal)e.Item.FindControl("LitPermissions");
		string rolesForUser = "";
		foreach (string item in Roles.GetRolesForUser(currItem.UserName))
		{
			rolesForUser += item + ", ";
		}
		if (rolesForUser.Length > 0)
		{
			rolesForUser = rolesForUser.Remove(rolesForUser.Length - 2);
		}
		LitPermissions.Text = rolesForUser + "<br>";
		LitPermissions.Text += currItem.AccessCode;
		if (currItem.AccessLevel > 0)
		{
			LitPermissions.Text += " " + currItem.AccessLevel.ToString();
		}

		{
			var LitEnabled = (Literal)e.Item.FindControl("LitEnabled");
			string chkClass = "";
			if (currItem.Enabled)
				chkClass = "checked";
			LitEnabled.Text = "<span class='table-modern--checkbox--square " + chkClass + "'></span>";
		}

		{
			var LitApproved = (Literal)e.Item.FindControl("LitApproved");
			string chkClass = "";
			if (currItem.IsApproved)
				chkClass = "checked";
			LitApproved.Text = "<span class='table-modern--checkbox--square " + chkClass + "'></span>";
		}

		{
			var LitIsCore = (Literal)e.Item.FindControl("LitIsCore");
			string chkClass = "";
			if (currItem.IsCore)
				chkClass = "checked";
			LitIsCore.Text = "<span class='table-modern--checkbox--square " + chkClass + "'></span>";
		}

		if (currItem.IsCore)
		{
			//var ColDelete = (HtmlAnchor)e.Item.FindControl("ColDelete");
			//ColDelete.Visible = false;
		}

	}

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        editRow("");
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
		setSuccess("");
		setError("");

        initMemberEditor();
        if (MemberEditor1.CheckForm())
        {
            if (MemberEditor1.SaveForm())
            {
                loadList();
				showInsertPanel(false);
            }
            else
                setError(MemberEditor1.LastMessage);
        }
        else
            setError(MemberEditor1.LastMessage);
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
		setError();
		setSuccess();
		showInsertPanel(false);
    }


    #region private methods


    private void editRow(string userName)
    {
		setError();
		setSuccess();

		if (!PgnUserCurrent.IsAuthenticated)
			throw new Exception("user not authenticated");

        initMemberEditor();
        MemberEditor1.ClearForm();
        MemberEditor1.CurrentUser = userName;

        if (userName == string.Empty)
        {
            LitTitle.Text = base.GetLabel("LblNewUser", "New user");
            MemberEditor1.EditorMode = Controls_MemberEditorControl.MemberEditorMode.InsertMode;
        }
        else
        {
            LitTitle.Text = base.GetLabel("LblUpdateUser", "Update user");
            MemberEditor1.EditorMode = Controls_MemberEditorControl.MemberEditorMode.UpdateMode;
        }
		MemberEditor1.Obj2form();
		showInsertPanel(true);
    }

    private void editPwd(string userName)
    {
		setSuccess();
		setError();

        initMemberEditor();
        MemberEditor1.ClearForm();
        MemberEditor1.CurrentUser = userName;

        if (userName != string.Empty)
        {
            LitTitle.Text = base.GetLabel("LblChangePassword", "Change password");
            MemberEditor1.EditorMode = Controls_MemberEditorControl.MemberEditorMode.ChangePasswordMode;
            MemberEditor1.Obj2form();
			showInsertPanel(true);
        }
    }

    private void editRoles(string userName)
    {
		setSuccess();
		setError();

        initMemberEditor();
        MemberEditor1.ClearForm();
        MemberEditor1.CurrentUser = userName;

        if (userName != string.Empty)
        {
            LitTitle.Text = base.GetLabel("LblChangeRoles", "Change roles");
            MemberEditor1.EditorMode = Controls_MemberEditorControl.MemberEditorMode.ChangeRolesMode;
            MemberEditor1.Obj2form();
			showInsertPanel(true);
        }
    }

    private void deleteRow(string userName)
    {
		setSuccess();
		setError();

        try
        {
            //remove user from all roles
            string[] rolesToRemove = Roles.GetRolesForUser(userName);
            if (rolesToRemove.Length > 0)
                Roles.RemoveUserFromRoles(userName, rolesToRemove);
            //remove user
            Membership.DeleteUser(userName);
        }
        catch (Exception e)
        {
            setError(e.Message);
        }
        loadList();
    }


    private void loadList()
    {
		MembershipUserCollection list;

        if (!string.IsNullOrEmpty(TxtUserNameFilter.Text))
            list = Membership.FindUsersByName(TxtUserNameFilter.Text);
        else
            list = Membership.GetAllUsers();


        //MasterFilter generic filter
        var listOf = list.Cast<MembershipUser>().Select(m => m).ToList();
        if (!string.IsNullOrEmpty(this.MasterFilter.Value))
        {
            listOf = (listOf.Where(i =>
            {
                return (
                    i.Email.Contains(this.MasterFilter.Value) ||
                    Roles.GetRolesForUser(i.UserName).Contains(this.MasterFilter.Value) );
            })).ToList();
        }

        var ds = new PagedDataSource();
		ds.DataSource = listOf;
		ds.AllowPaging = true;
		ds.PageSize = base.ListPageSize;
		ds.CurrentPageIndex = base.ListCurrentPage;

		RepPaging.Visible = false;
		if (ds.PageCount > 1)
		{
			RepPaging.Visible = true;
			ArrayList pages = new ArrayList();
			for (int i = 0; i <= ds.PageCount - 1; i++)
			{
				pages.Add((i + 1).ToString());
			}
			RepPaging.DataSource = pages;
			RepPaging.DataBind();
		}

		Rep1.DataSource = ds;
		Rep1.DataBind();
    }

    private void initMemberEditor()
    {
        var me = MemberEditor1;

        //me.BaseModule.ModuleParams = base.BaseModule.ModuleParams;
        me.BaseModule.Id = base.BaseModule.Id;
        me.BaseModuleParams = base.BaseModule.ModuleParams;
        me.BaseModule.CssClass = base.BaseModule.CssClass;
        me.BaseModule.UseLog = base.BaseModule.UseLog;
    }

	/// function for display insert panel
	/// <summary>
	/// </summary>
	private void showInsertPanel(bool toShow)
	{

		PigeonCms.Utility.Script.RegisterStartupScript(Upd1, "bodyBlocked", "bodyBlocked(" + toShow.ToString().ToLower() + ");");

		if (toShow)
			PanelInsert.Visible = true;
		else
			PanelInsert.Visible = false;
	}

	private void setError(string content = "")
	{
		LblErrInsert.Text = LblErrSee.Text = RenderError(content);
	}

	private void setSuccess(string content = "")
	{
		LblOkInsert.Text = LblOkSee.Text = RenderSuccess(content);
	}

    #endregion
}
