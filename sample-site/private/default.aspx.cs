using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PigeonCms;
using System.Web.Security;

public partial class private_default : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
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


        Lit1.Text = "This area is reserved. <i>System.Web.Security</i> namespace is available.<br><br>";
        Lit1.Text += "<strong>PgnUserCurrent.UserName</strong>: " + PgnUserCurrent.UserName + "<br>";
        Lit1.Text += "<strong>PgnUserCurrent.IsAuthenticated</strong>: " + PgnUserCurrent.IsAuthenticated.ToString() + "<br>";
        Lit1.Text += "<strong>Roles.IsUserInRole(\"admin\")</strong>: " + Roles.IsUserInRole("admin").ToString() + "<br>";
        Lit1.Text += "<strong>Roles.GetRolesForUser()</strong>: " + roles + "<br><br>";
        Lit1.Text += "You can use user context <strong>(checkUserContext = true)</strong> in classes that implements <strong>ITableManagerWithPermission</strong>";


    }

}