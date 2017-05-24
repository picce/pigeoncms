using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PigeonCms;
using System.Web.Security;
using Acme;

public partial class private_default : Acme.BasePage
{
    protected string Description;

    protected void Page_Load(object sender, EventArgs e)
    {
        CurrentMasterPage.DataSection = "private";
        CurrentMasterPage.LinkFooter = "javascript:history.back()";
        CurrentMasterPage.TextLinkFooter = "back";

        string roles = "";
        string[] rolesArr = Roles.GetRolesForUser();
        if (rolesArr != null && rolesArr.Length > 0)
        {
            foreach(string r in rolesArr) 
            {
                roles += r + ", ";
            }
            if (roles.EndsWith(", "))
                roles = roles.Substring(0, roles.Length - 2);
        }

        Description = "This area is reserved. <i>System.Web.Security</i> namespace is available.<br><br>" +
                "<strong>PgnUserCurrent.UserName</strong>: " + PgnUserCurrent.UserName + "<br>" +
                "<strong>PgnUserCurrent.IsAuthenticated</strong>: " + PgnUserCurrent.IsAuthenticated.ToString() + "<br>" +
                "<strong>Roles.IsUserInRole(\"admin\")</strong>: " + Roles.IsUserInRole("admin").ToString() + "<br>" +
                "<strong>Roles.GetRolesForUser()</strong>: " + roles + "<br><br>" +
                "You can use user context <strong>(checkUserContext = true)</strong> in classes that implements <strong>ITableManagerWithPermission</strong>";

    }

}