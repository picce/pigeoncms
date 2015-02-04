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
    private const int View_Grid_Index = 0;
    private const int View_Insert_Index = 1;
    private const int View_Users_Index = 2;

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
        if (!Page.IsPostBack)
        {
            loadGrid();
        }
    }

    protected void Grid1_RowCommand(object sender, GridViewCommandEventArgs e)
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

    protected void Grid1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
            Utility.AddGlyph(Grid1, e.Row);
    }

    protected void Grid1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            RoleName currItem = (RoleName)e.Row.DataItem;

            Literal LitNumUsersInRole = (Literal)e.Row.FindControl("LitNumUsersInRole");
            Literal LitUsersInRole = (Literal)e.Row.FindControl("LitUsersInRole");

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
    }

    protected void Grid1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Grid1.PageIndex = e.NewPageIndex;
        loadGrid();
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
                MultiView1.ActiveViewIndex = View_Grid_Index;
        }
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

    private bool checkForm()
    {
        LblErr.Text = "";
        LblOk.Text = "";
        bool res = true;

        if (MultiView1.ActiveViewIndex == View_Insert_Index)    //insert
        {
            if (string.IsNullOrEmpty(TxtRolename.Text))
            {
                res = false;
                LblErr.Text += "inserire in nome della role";
            }
        }
        else if (MultiView1.ActiveViewIndex == View_Users_Index)    //users in role
        {
        }
        return res;
    }

    private bool saveForm()
    {
        LblErr.Text = "";
        LblOk.Text = "";
        bool res = false;

        try
        {
            if (MultiView1.ActiveViewIndex == View_Insert_Index)    //insert
            {
                if (CurrentRole == "")
                {
                    Roles.CreateRole(TxtRolename.Text);
                    res = true;
                }
            }
            if (MultiView1.ActiveViewIndex == View_Users_Index)    //users in role
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
                loadGrid();
                LblOk.Text = Utility.GetLabel("RECORD_SAVED_MSG");
                MultiView1.ActiveViewIndex = View_Grid_Index;
            }
        }
        catch (Exception e1)
        {
            LblErr.Text = Utility.GetLabel("RECORD_ERR_MSG") + "<br />" + e1.Message;
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
        LblOk.Text = "";
        LblErr.Text = "";

        clearForm();
        CurrentRole = rolename;

        if (rolename == string.Empty)
        {
            MultiView1.ActiveViewIndex = View_Insert_Index;
        }
        else
        {
            LitRolename.Text = CurrentRole;
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

            MultiView1.ActiveViewIndex = View_Users_Index;
        }
    }

    private void deleteRow(string rolename)
    {
        LblOk.Text = "";
        LblErr.Text = "";

        try
        {
            Roles.DeleteRole(rolename);
        }
        catch (Exception e)
        {
            LblErr.Text = e.Message;
        }
        loadGrid();
    }

    private void clearForm()
    {
        TxtRolename.Text = "";
        LitRolename.Text = "";
    }

    private void loadGrid()
    {
        string[] roles = Roles.GetAllRoles();
        List<RoleName> rolenames = new List<RoleName>();
        foreach (string role in roles)
        {
            rolenames.Add(new RoleName(role));
        }
        Grid1.DataSource = rolenames;
        Grid1.DataBind();
    }

    #endregion
}
