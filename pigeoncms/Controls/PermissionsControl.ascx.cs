using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
using PigeonCms;

public partial class Controls_PermissionsControl : PigeonCms.Controls.PermissionsControl
{
    protected new void Page_Init(object sender, EventArgs e)
    {
        this.BaseModule = new Module();
        this.BaseModule.ModuleNamespace = "PigeonCms";
        this.BaseModule.ModuleName = "PermissionsControl";
        if (!Page.IsPostBack)
        {
            PgnUserHelper.LoadListRoles(ListRoles);
            PgnUserHelper.LoadListRoles(ListWriteRoles);
            loadDropAccessType();
        }
        base.Page_Init(sender, e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

	public override void ClearForm()
    {
        LitId.Text = "";
        LitWriteId.Text = "";

        TxtAccessLevel.Text = "";
        TxtAccessCode.Text = "";

        TxtWriteAccessCode.Text = "";
        TxtWriteAccessLevel.Text = "";
    }

	public override void Form2obj(ITableWithPermissions obj)
    {
        int acccessLevel = 0;
        int.TryParse(TxtAccessLevel.Text, out acccessLevel);

        int writeAccessLevel = 0;
        int.TryParse(TxtWriteAccessLevel.Text, out writeAccessLevel);

        obj.ReadAccessLevel = acccessLevel;
        obj.ReadAccessCode = TxtAccessCode.Text;
        obj.ReadAccessType = (MenuAccesstype)int.Parse(DropAccessType.SelectedValue);

        obj.WriteAccessLevel = writeAccessLevel;
        obj.WriteAccessCode = TxtWriteAccessCode.Text;
        obj.WriteAccessType = (MenuAccesstype)int.Parse(DropWriteAccessType.SelectedValue);

        //read roles
        obj.ReadRolenames.Clear();
        foreach (ListItem item in ListRoles.Items)
        {
            if (item.Selected)
                obj.ReadRolenames.Add(item.Value);
        }

        //write roles
        obj.WriteRolenames.Clear();
        foreach (ListItem item in ListWriteRoles.Items)
        {
            if (item.Selected)
                obj.WriteRolenames.Add(item.Value);
        }
    }

	public override void Obj2form(ITableWithPermissions obj)
    {
        LitId.Text = obj.ReadPermissionId.ToString();
        LitWriteId.Text = obj.WritePermissionId.ToString();

        TxtAccessLevel.Text = obj.ReadAccessLevel.ToString();
        TxtWriteAccessLevel.Text = obj.WriteAccessLevel.ToString();

        TxtAccessCode.Text = obj.ReadAccessCode;
        TxtWriteAccessCode.Text = obj.WriteAccessCode;

        Utility.SetDropByValue(DropAccessType, ((int)obj.ReadAccessType).ToString());
        Utility.SetDropByValue(DropWriteAccessType, ((int)obj.WriteAccessType).ToString());

        Utility.SetListBoxByValues(ListRoles, obj.ReadRolenames, true);
        Utility.SetListBoxByValues(ListWriteRoles, obj.WriteRolenames, true);
    }

    private void loadDropAccessType()
    {
        DropAccessType.Items.Clear();
        DropWriteAccessType.Items.Clear();
        foreach (string item in Enum.GetNames(typeof(PigeonCms.MenuAccesstype)))
        {
            int value = (int)Enum.Parse(typeof(PigeonCms.MenuAccesstype), item);
            ListItem listItem = new ListItem(item, value.ToString());
            DropAccessType.Items.Add(listItem);
            DropWriteAccessType.Items.Add(listItem);
        }
    }
}
