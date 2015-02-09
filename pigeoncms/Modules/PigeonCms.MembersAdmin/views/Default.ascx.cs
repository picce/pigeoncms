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

public partial class Controls_Default : PigeonCms.MemberEditorControl
{
    private const int View_Grid_Index = 0;
    private const int View_Insert_Index = 1;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Roles.IsUserInRole("admin"))
            throw new HttpException(404, "Page not found");

        if (!Page.IsPostBack)
        {
            loadGrid();
        }
    }

    protected void TxtUserNameFilter_TextChanged(object sender, EventArgs e)
    {
        try { loadGrid(); }
        catch (Exception ex)
        {
            LblErr.Text = RenderError(ex.Message);
        }
    }

    protected void Grid1_RowCommand(object sender, GridViewCommandEventArgs e)
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

    protected void Grid1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
            Utility.AddGlyph(Grid1, e.Row);
    }

    protected void Grid1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            var currItem = PgnUserCurrent.GetUser(((MembershipUser)e.Row.DataItem).UserName);

            CheckBox ChkEnabled = (CheckBox)e.Row.FindControl("ChkEnabled");
            CheckBox ChkIsCore = (CheckBox)e.Row.FindControl("ChkIsCore");
            LinkButton LnkUserName = (LinkButton)e.Row.FindControl("LnkUserName");
            Literal LitEmail = (Literal)e.Row.FindControl("LitEmail");
            Literal LitName = (Literal)e.Row.FindControl("LitName");
            Literal LitRolesForUser = (Literal)e.Row.FindControl("LitRolesForUser");

            LnkUserName.Text = "<i class='fa fa-pgn_edit fa-fw'></i>";
            LnkUserName.Text += Utility.Html.GetTextPreview(currItem.UserName, 30, "");

            ChkEnabled.Checked = currItem.Enabled;
            ChkIsCore.Checked = currItem.IsCore;
            LitEmail.Text = "<a href='mailto:" + currItem.Email + "'>" 
                + Utility.Html.GetTextPreview(currItem.Email, 30, "") 
                + "</a>";

            LitName.Text = "";
            if (!string.IsNullOrEmpty(currItem.CompanyName))
                LitName.Text += currItem.CompanyName + "<br />";
            if (!string.IsNullOrEmpty(currItem.FirstName+currItem.SecondName))
                LitName.Text += currItem.FirstName + " " + currItem.SecondName + "<br />";

            string rolesForUser = "";
            foreach (string item in Roles.GetRolesForUser(currItem.UserName))
            {
                rolesForUser += item + ", ";
            }
            if (rolesForUser.Length > 0)
            {
                rolesForUser = rolesForUser.Remove(rolesForUser.Length - 2);
            }
            LitRolesForUser.Text = rolesForUser;

            Literal LitAccessLevel = (Literal)e.Row.FindControl("LitAccessLevel");
            LitAccessLevel.Text = currItem.AccessCode;
            if (currItem.AccessLevel > 0)
            {
                LitAccessLevel.Text += " " + currItem.AccessLevel.ToString();
            }

            //Delete            
            if (currItem.IsCore)
            {
                var img1 = e.Row.FindControl("LnkDel");
                img1.Visible = false;
            }
            else
            {
                var img1 = e.Row.FindControl("LnkDel");
                img1.Visible = true;
            }
        }
    }

    protected void Grid1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Grid1.PageIndex = e.NewPageIndex;
        loadGrid();
    }

    protected void Grid1_Sorting(object sender, GridViewSortEventArgs e)
    {
        //DataTable dt = GridView1.DataSource as DataTable;
        //if (dt != null)
        //{
        //    DataView dv = new DataView(dt);
        //    dv.Sort = String.Format("{0} {1}", e.SortExpression, ConvertSort(e.SortDirection));
        //    GridView1.DataSource = dv;
        //    GridView1.DataBind();
        //}
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        editRow("");
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        LblOk.Text = "";
        LblErr.Text = "";

        initMemberEditor();
        if (MemberEditor1.CheckForm())
        {
            if (MemberEditor1.SaveForm())
            {
                loadGrid();
                MultiView1.ActiveViewIndex = 0;
            }
            else
                LblErr.Text = RenderError(MemberEditor1.LastMessage);
        }
        else
            LblErr.Text = RenderError(MemberEditor1.LastMessage);
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        LblErr.Text = "";
        LblOk.Text = "";
        MultiView1.ActiveViewIndex = View_Grid_Index;
    }

    protected void MultiView1_ActiveViewChanged(object sender, EventArgs e)
    { }

    #region private methods


    private void editRow(string userName)
    {
        LblOk.Text = "";
        LblErr.Text = "";

        initMemberEditor();
        MemberEditor1.ClearForm();
        MemberEditor1.CurrentUser = userName;

        if (userName == string.Empty)
        {
            LitTitle.Text = base.GetLabel("LblNewUser", "New user");
            MemberEditor1.EditorMode = Controls_MemberEditorControl.MemberEditorMode.InsertMode;
            MemberEditor1.Obj2form();
            MultiView1.ActiveViewIndex = View_Insert_Index;
        }
        else
        {
            LitTitle.Text = base.GetLabel("LblUpdateUser", "Update user");
            MemberEditor1.EditorMode = Controls_MemberEditorControl.MemberEditorMode.UpdateMode;
            MemberEditor1.Obj2form();
            MultiView1.ActiveViewIndex = View_Insert_Index;
        }
    }

    private void editPwd(string userName)
    {
        LblOk.Text = "";
        LblErr.Text = "";

        initMemberEditor();
        MemberEditor1.ClearForm();
        MemberEditor1.CurrentUser = userName;

        if (userName != string.Empty)
        {
            LitTitle.Text = base.GetLabel("LblChangePassword", "Change password");
            MemberEditor1.EditorMode = Controls_MemberEditorControl.MemberEditorMode.ChangePasswordMode;
            MemberEditor1.Obj2form();
            MultiView1.ActiveViewIndex = View_Insert_Index;
        }
    }

    private void editRoles(string userName)
    {
        LblOk.Text = "";
        LblErr.Text = "";

        initMemberEditor();
        MemberEditor1.ClearForm();
        MemberEditor1.CurrentUser = userName;

        if (userName != string.Empty)
        {
            LitTitle.Text = base.GetLabel("LblChangeRoles", "Change roles");
            MemberEditor1.EditorMode = Controls_MemberEditorControl.MemberEditorMode.ChangeRolesMode;
            MemberEditor1.Obj2form();
            MultiView1.ActiveViewIndex = View_Insert_Index;
        }
    }

    private void deleteRow(string userName)
    {
        LblOk.Text = "";
        LblErr.Text = "";

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
            LblErr.Text = RenderError(e.Message);
        }
        loadGrid();
    }


    private void loadGrid()
    {
        if (!string.IsNullOrEmpty(TxtUserNameFilter.Text))
            Grid1.DataSource = Membership.FindUsersByName(TxtUserNameFilter.Text);
        else
            Grid1.DataSource = Membership.GetAllUsers();
        Grid1.DataBind();
    }

    private string convertSort(SortDirection sortDirection)
    {
        string m_SortDirection = String.Empty;
        switch (sortDirection)
        {
            case SortDirection.Ascending:
                m_SortDirection = "ASC";
                break;
            case SortDirection.Descending:
                m_SortDirection = "DESC";
                break;
        }
        return m_SortDirection;
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

    #endregion
}
