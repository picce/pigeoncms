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

public partial class Controls_Default : PigeonCms.BaseModuleControl
{
	const int PANEL_SEE_IDX = 0;
	const int PANEL_INS_IDX = 1;
	const int PANEL_USERS_IDX = 2;

	/// <summary>
	/// pkey, current role
	/// </summary>
	protected int CurrentPanelIdx
	{
		get
		{
			int res = 0;
			if (ViewState["CurrentPanelIdx"] != null)
				res = (int)ViewState["CurrentPanelIdx"];
			return res;
		}
		set { ViewState["CurrentPanelIdx"] = value; }
	}

    /// <summary>
    /// pkey, current role
    /// </summary>
    protected string CurrentRole
    {
        get
        {
            string res = "";
            if (ViewState["CurrentRole"] != null)
                res = (string)ViewState["CurrentRole"];
            return res;
        }
        set { ViewState["CurrentRole"] = value; }
    }

    public class RoleName
    {
        public RoleName(string role)
        {
            this.Role = role;
        }
        public string Role { get; set; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
		if (!Roles.IsUserInRole("admin"))
			throw new HttpException(404, "Page not found");

        if (!Page.IsPostBack)
        {
			loadList();
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

        var currItem = (RoleName)e.Item.DataItem;

		var LnkRole = (LinkButton)e.Item.FindControl("LnkRole");
		var LitNumUsersInRole = (Literal)e.Item.FindControl("LitNumUsersInRole");
		var LitUsersInRole = (Literal)e.Item.FindControl("LitUsersInRole");

        LitNumUsersInRole.Text = Roles.GetUsersInRole(currItem.Role).Length.ToString();

        string usersInRole = "";
        foreach (string item in Roles.GetUsersInRole(currItem.Role))
        {
            usersInRole += item + ", ";
        }
        if (usersInRole.Length > 0)
        {
            usersInRole = usersInRole.Remove(usersInRole.Length - 2);
        }
        LitUsersInRole.Text = Utility.Html.GetTextPreview(usersInRole, 500, "");
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        editRow("");
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        if (checkForm())
        {
			if (saveForm())
				showPanel(PANEL_SEE_IDX);
        }
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
		setError();
		setSuccess();
		showPanel(PANEL_SEE_IDX);
    }


    #region private methods

    private bool checkForm()
    {
		setError();
		setSuccess();
        bool res = true;

		if (CurrentPanelIdx == PANEL_INS_IDX)    //insert
        {
            if (string.IsNullOrEmpty(TxtRolename.Text))
            {
                res = false;
				setError(base.GetLabel("InsRoleName", "Insert role name"));
            }
        }
        return res;
    }

    private bool saveForm()
    {
		setError();
		setSuccess();
        bool res = false;

        try
        {
			if (CurrentPanelIdx == PANEL_INS_IDX)    //insert
            {
                if (CurrentRole == "")
                {
                    Roles.CreateRole(TxtRolename.Text);
                    res = true;
                }
            }
			if (CurrentPanelIdx == PANEL_USERS_IDX)    //users in role
            {
                if (CurrentRole != "")
                {
                    //remove all users from role
                    string[] usersToRemove = Roles.GetUsersInRole(CurrentRole);
                    if (usersToRemove.Length > 0)
                        Roles.RemoveUsersFromRole(usersToRemove, CurrentRole);

                    //add selected users to role
                    if (HiddenUsersInRole.Value.Length > 0)
                    {
                        string[] usersToAdd = HiddenUsersInRole.Value.Split('|');
                        Roles.AddUsersToRole(usersToAdd, CurrentRole);
                    }
                    res = true;
                }
            }

            if (res)
            {
                loadList();
				setSuccess(Utility.GetLabel("RECORD_SAVED_MSG"));
				showPanel(PANEL_SEE_IDX);
            }
        }
        catch (Exception e1)
        {
			setError(Utility.GetLabel("RECORD_ERR_MSG") + "<br />" + e1.Message);
        }
        finally
        {
        }
        return res;
    }

    private void form2obj(string rolename)
    {
        throw new NotImplementedException();
    }

    private void editRow(string rolename)
    {
		setError();
		setSuccess();

        clearForm();
        CurrentRole = rolename;

        if (rolename == string.Empty)
        {
			showPanel(PANEL_INS_IDX);
        }
        else
        {
			TxtRolenameUser.Text = CurrentRole;
            PgnUserHelper.LoadListUsersInRole(ListUsersInRole, CurrentRole);
            PgnUserHelper.LoadListUsersNotInRole(ListUsersNotInRole, CurrentRole);
            //load hidden field with current roles
            HiddenUsersInRole.Value = "";
            foreach (ListItem item in ListUsersInRole.Items)
            {
                HiddenUsersInRole.Value += item.Value + "|";
            }
            if (HiddenUsersInRole.Value.Length > 0)
                HiddenUsersInRole.Value =
                    HiddenUsersInRole.Value.Remove(HiddenUsersInRole.Value.Length - 1);

			showPanel(PANEL_USERS_IDX);
        }
    }

    private void deleteRow(string rolename)
    {
		setError();
		setSuccess();

        try
        {
            Roles.DeleteRole(rolename);
        }
        catch (Exception e)
        {
            setError( e.Message);
        }
        loadList();
    }

    private void clearForm()
    {
        TxtRolename.Text = "";
		TxtRolenameUser.Text = "";
    }

    private void loadList()
    {
		string[] roles = Roles.GetAllRoles();
		var list = new List<RoleName>();
		foreach (string role in roles)
		{
			list.Add(new RoleName(role));
		}



		var ds = new PagedDataSource();
		ds.DataSource = list;
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

	/// function for display insert panel
	/// <summary>
	/// </summary>
	private void showPanel(int panelIdx)
	{
		bool toShow = (panelIdx != PANEL_SEE_IDX);

		PigeonCms.Utility.Script.RegisterStartupScript(Upd1, 
			"bodyBlocked", "bodyBlocked(" + toShow.ToString().ToLower() + ");");

		CurrentPanelIdx = panelIdx;

		PanelInsert.Visible = false;
		PanelUsers.Visible = false;

		if (panelIdx == PANEL_INS_IDX)
			PanelInsert.Visible = true;
		else if (panelIdx == PANEL_USERS_IDX)
			PanelUsers.Visible = true;

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
