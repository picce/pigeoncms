using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using PigeonCms;
using System.Collections.Generic;

namespace PigeonCms
{
    /// <summary>
    /// Useful static functions to users and roles
    /// </summary>
    public static class PgnUserHelper
    {
        public static void LoadListRoles(ListBox list)
        {
            list.Items.Clear();
            list.Items.Add(new ListItem("",""));    //to allow no roles
            foreach (string item in Roles.GetAllRoles())
            {
                ListItem listItem = new ListItem();
                listItem.Value = item;
                listItem.Text = item;
                listItem.Enabled = true;

                list.Items.Add(listItem);
            }
        }

        public static void LoadListRolesInUser(ListBox list, string username)
        {
            list.Items.Clear();
            foreach (string item in Roles.GetRolesForUser(username))
            {
                ListItem listItem = new ListItem();
                listItem.Value = item;
                listItem.Text = item;
                listItem.Enabled = true;

                list.Items.Add(listItem);
            }
        }

        public static void LoadListRolesNotInUser(ListBox list, string username)
        {
            list.Items.Clear();
            foreach (string item in Roles.GetAllRoles())
            {
                if (!Roles.IsUserInRole(username, item))
                {
                    ListItem listItem = new ListItem();
                    listItem.Value = item;
                    listItem.Text = item;
                    listItem.Enabled = true;

                    list.Items.Add(listItem);
                }
            }
        }

        public static void LoadListUsersInRole(ListBox list, string rolename)
        {
            list.Items.Clear();
            foreach (string item in Roles.GetUsersInRole(rolename))
            {
                ListItem listItem = new ListItem();
                listItem.Value = item;
                listItem.Text = item;
                listItem.Enabled = true;

                list.Items.Add(listItem);
            }
        }

        public static void LoadListUsersNotInRole(ListBox list, string rolename)
        {
            list.Items.Clear();
            foreach (MembershipUser user in Membership.GetAllUsers())
            {
                if (!Roles.IsUserInRole(user.UserName, rolename))
                {
                    ListItem listItem = new ListItem();
                    listItem.Value = user.UserName;
                    listItem.Text = user.UserName;
                    listItem.Enabled = true;

                    list.Items.Add(listItem);
                }
            }
        }
    }
}