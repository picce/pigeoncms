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
using System.Net.Mail;
using System.Net;

public partial class Controls_MemberEditorControl : PigeonCms.MemberEditorControl
{
    protected string LitUsername = "";
    protected string LitEmail = "";
    protected string LitPassword = "";
    protected string LitPasswordControl = "";
    protected string LitOldPassword = "";
    protected string LitEnabled = "";
    protected string LitApproved = "";
    protected string LitCompanyName = "";
    protected string LitComment = "";
    protected string LitAccessCode = "";
    protected string LitAccessLevel = "";
    protected string LitSex = "";
    protected string LitVat = "";
    protected string LitSsn = "";
    protected string LitFirstName = "";
    protected string LitSecondName = "";
    protected string LitAddress1 = "";
    protected string LitAddress2 = "";
    protected string LitCity = "";
    protected string LitState = "";
    protected string LitZipCode = "";
    protected string LitNation = "";
    protected string LitTel1 = "";
    protected string LitMobile1 = "";
    protected string LitWebsite1 = "";
    protected string LitAllowMessages = "";
    protected string LitAllowEmails = "";


    public enum MemberEditorMode
    {
        NotSet = 0,
        InsertMode,
        UpdateMode,
        ChangePasswordMode,
        ChangeRolesMode
    }

    private bool isUserInAdminRole
    {
        get
        {
            bool res = false;
            if (PgnUserCurrent.IsAuthenticated)
                res = Roles.IsUserInRole("admin");
            return res;
        }
    }

    public string BaseModuleParams
    {
        get
        {
            string res = "";
            if (ViewState["BaseModuleParams"] != null)
                res = (string)ViewState["BaseModuleParams"];
            return res;
        }
        set
        {
            ViewState["BaseModuleParams"] = value;
            this.BaseModule.ModuleParams = value;
        }
    }

    private string lastMessage = "";
    public string LastMessage
    {
        get { return lastMessage; }
    }

    //private MemberEditorMode editorMode = MemberEditorMode.InsertMode;
    public MemberEditorMode EditorMode
    {
        get 
        {
            var res = MemberEditorMode.InsertMode;
            if (ViewState["EditorMode"] != null)
                res = (MemberEditorMode)ViewState["EditorMode"];
            return res;
        }
        set 
        {
            if (value == MemberEditorMode.ChangeRolesMode && !isUserInAdminRole)
                throw new ArgumentException("MemberEditorMode not allowed");

            ViewState["EditorMode"] = value;
            setPanels();
        }
    }

    /// <summary>
    /// pkey, current user
    /// </summary>
    public string CurrentUser
    {
        get
        {
            string res = PgnUserCurrent.UserName;
            if (ViewState["CurrentUser"] != null)
                res = (string)ViewState["CurrentUser"];
            return res;
        }
        set 
        { 
            string user = "";
            if (this.isUserInAdminRole)
                user = value;
            else
                user = PgnUserCurrent.UserName;

            ViewState["CurrentUser"] = user;
        }
    }


    public void SetDefaultUsernameValue(string value)
    {
        TxtInsUserName.Text = value;
    }

    public void SetDefaultPasswordValue(string value)
    {
        TxtInsPassword.Text = value;
        TxtInsPasswordControl.Text = value;
    }

    public void SetDefaultEmailValue(string value)
    {
        TxtInsEmail.Text = value;
    }

    public void SetDefaultVatlValue(string value)
    {
        TxtInsVat.Text = value;
    }

    public void SetDefaultSsnValue(string value)
    {
        TxtInsSsn.Text = value;
    }

    public void SetDefaultCompanyNamelValue(string value)
    {
        TxtInsCompanyName.Text = value;
    }

    public void SetDefaultFirstNamelValue(string value)
    {
        TxtInsFirstName.Text = value;
    }

    public void SetDefaultSecondNamelValue(string value)
    {
        TxtInsSecondName.Text = value;
    }

    public void SetDefaultAddress1Value(string value)
    {
        TxtInsAddress1.Text = value;
    }

    public void SetDefaultAddress2Value(string value)
    {
        TxtInsAddress2.Text = value;
    }

    public void SetDefaultCityValue(string value)
    {
        TxtInsCity.Text = value;
    }

    public void SetDefaultStateValue(string value)
    {
        TxtInsState.Text = value;
    }

    public void SetDefaultZipValue(string value)
    {
        TxtInsZipCode.Text = value;
    }

    public void SetDefaultNationValue(string value)
    {
        try
        {
            Utility.SetDropByValue(DropInsNation, value);
        }
        catch { }
    }

    public void SetDefaultTel1Value(string value)
    {
        TxtInsTel1.Text = value;
    }

    public void SetDefaultMobile1Value(string value)
    {
        TxtInsMobile1.Text = value;
    }

    public void SetDefaultWebsite1Value(string value)
    {
        TxtInsWebsite1.Text = value;
    }

    public void SetDefaultAllowMessagesValue(bool value)
    {
        ChkInsAllowMessages.Checked = value;
    }

    public void SetDefaultAllowEmailsValue(bool value)
    {
        ChkInsAllowEmails.Checked = value;
    }

    protected new void Page_Init(object sender, EventArgs e)
    {
        this.BaseModule = new Module();
        this.BaseModule.ModuleNamespace = "PigeonCms";
        this.BaseModule.ModuleName = "MemberEditorControl";

        if (this.isUserInAdminRole)
            initChangeRolesScript();

        if (!Page.IsPostBack)
        {
            loadDropNation();

            //PgnUserHelper.LoadListRoles(ListRoles);
            //PgnUserHelper.LoadListRoles(ListWriteRoles);
            //loadDropAccessType();
        }
        base.Page_Init(sender, e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.BaseModule.ModuleParams = this.BaseModuleParams;
        setPanels();
    }

    public void ClearForm()
    {
        //ins
        TxtInsUserName.Text = "";
        ChkInsEnabled.Checked = true;
        TxtInsEmail.Text = "";
        TxtInsPassword.Text = "";
        TxtInsPasswordControl.Text = ""; 
        TxtInsCompanyName.Text = "";
        TxtInsVat.Text = "";
        TxtInsSsn.Text = "";
        TxtInsFirstName.Text = "";
        TxtInsSecondName.Text = "";
        TxtInsAddress1.Text = "";
        TxtInsAddress2.Text = "";
        TxtInsCity.Text = "";
        TxtInsState.Text = "";
        TxtInsZipCode.Text = "";
        Utility.SetDropByValue(DropInsNation, "IT");
        TxtInsTel1.Text = "";
        TxtInsMobile1.Text = "";
        TxtInsWebsite1.Text = "";
        ChkInsAllowMessages.Checked = false;
        ChkInsAllowEmails.Checked = false;


        //upd
		TextUpdUserName.Text = "";
        ChkUpdEnabled.Checked = true;
        ChkUpdApproved.Checked = false;
        TxtUpdEmail.Text = "";
        TxtUpdComment.Text = "";
        TxtUpdAccessCode.Text = "";
        TxtUpdAccessLevel.Text = "";
        TxtUpdCompanyName.Text = "";
        TxtUpdVat.Text = "";
        TxtUpdSsn.Text = "";
        TxtUpdFirstName.Text = "";
        TxtUpdSecondName.Text = "";
        TxtUpdAddress1.Text = "";
        TxtUpdAddress2.Text = "";
        TxtUpdCity.Text = "";
        TxtUpdState.Text = "";
        TxtUpdZipCode.Text = "";
        Utility.SetDropByValue(DropUpdNation, "IT");
        TxtUpdTel1.Text = "";
        TxtUpdMobile1.Text = "";
        TxtUpdWebsite1.Text = "";
        ChkUpdAllowMessages.Checked = false;
        ChkUpdAllowEmails.Checked = false;

        //pwd
		TxtPwdUsername.Text = "";
        TxtPwdPassword.Text = "";
        TxtPwdPasswordControl.Text = "";
        TxtPwdOldPassword.Text = "";
    }

    public bool SaveForm()
    {
        this.lastMessage = "";
        bool res = false;

        try
        {

            if (this.EditorMode == MemberEditorMode.InsertMode)
            {
                res = createUser();
                if (res)
                {
                    LogProvider.Write(this.BaseModule, "user " + getInsUsername() + " created");

                    if (base.SendEmailNotificationToUser)
                        sendNotificationEmail(getInsUsername(), TxtInsPassword.Text, TxtInsEmail.Text, TxtInsEmail.Text);
                    if (base.SendEmailNotificationToAdmin)
                        sendNotificationEmail(getInsUsername(), TxtInsPassword.Text, base.AdminNotificationEmail, TxtInsEmail.Text);
                    if (base.LoginAfterCreate)
                        loginUser(getInsUsername(), TxtInsPassword.Text);
                }
            }
            else if (this.EditorMode == MemberEditorMode.UpdateMode)
            {
                res = updateUser();
                if (res)
                    LogProvider.Write(this.BaseModule, "user " + this.CurrentUser + " updated");
            }
            else if (this.EditorMode == MemberEditorMode.ChangePasswordMode)
            {
                res = changePassword();
                if (res)
                    LogProvider.Write(this.BaseModule, "user " + this.CurrentUser + " password changed");

            }
            else if (this.EditorMode == MemberEditorMode.ChangeRolesMode)
            {
                res = changeRoles();
                if (res)
                    LogProvider.Write(this.BaseModule, "user " + this.CurrentUser + " roles changed");
            }
            //if (res)
            //{
            //    if (!string.IsNullOrEmpty(base.RedirectUrl))
            //        Response.Redirect(base.RedirectUrl, false);        
            //}
        }
        catch (Exception e1)
        {
            res = false;
            LogProvider.Write(this.BaseModule, " saveform() error: "+ e1.Message.ToString(), TracerItemType.Error);
            this.lastMessage = e1.Message.ToString();
        }
        finally
        {
        }
        return res;
    }

    public void Obj2form()
    {
        PgnUser obj = null;
        obj = (PgnUser)Membership.GetUser(CurrentUser);
        if (obj == null)
            obj = new PgnUser();

        //insview

        //update
		TextUpdUserName.Text = obj.UserName;
        ChkUpdEnabled.Checked = obj.Enabled;
        ChkUpdApproved.Checked = obj.IsApproved;
        TxtUpdEmail.Text = obj.Email;
        TxtUpdComment.Text = obj.Comment;
        TxtUpdAccessCode.Text = obj.AccessCode;
        TxtUpdAccessLevel.Text = obj.AccessLevel.ToString();
        TxtUpdCompanyName.Text = obj.CompanyName;
        TxtUpdVat.Text = obj.Vat;
        TxtUpdSsn.Text = obj.Ssn;
        TxtUpdFirstName.Text = obj.FirstName;
        TxtUpdSecondName.Text = obj.SecondName;
        TxtUpdAddress1.Text = obj.Address1;
        TxtUpdAddress2.Text = obj.Address2;
        TxtUpdCity.Text = obj.City;
        TxtUpdState.Text = obj.State;
        TxtUpdZipCode.Text = obj.ZipCode;
        Utility.SetDropByValue(DropUpdNation, obj.Nation);
        TxtUpdTel1.Text = obj.Tel1;
        TxtUpdMobile1.Text = obj.Mobile1;
        TxtUpdWebsite1.Text = obj.Website1;
        ChkUpdAllowMessages.Checked = obj.AllowMessages;
        ChkUpdAllowEmails.Checked = obj.AllowEmails;

        TxtPwdUsername.Text = obj.UserName;

		TxtRolesUsername.Text = obj.UserName;
        PgnUserHelper.LoadListRolesInUser(ListRolesInUser, CurrentUser);
        PgnUserHelper.LoadListRolesNotInUser(ListRolesNotInUser, CurrentUser);
        //load hidden field with current users in rol
        HiddenRolesInUser.Value = "";
        foreach (ListItem item in ListRolesInUser.Items)
        {
            HiddenRolesInUser.Value += item.Value + "|";
        }
        if (HiddenRolesInUser.Value.Length > 0)
            HiddenRolesInUser.Value =
                HiddenRolesInUser.Value.Remove(HiddenRolesInUser.Value.Length - 1);
    }

    public bool CheckForm()
    {
        this.lastMessage = "";
        bool res = true;

        if (this.EditorMode == MemberEditorMode.InsertMode)  
        {
            if (string.IsNullOrEmpty(TxtInsUserName.Text) ||
                string.IsNullOrEmpty(TxtInsPassword.Text ) ||
                string.IsNullOrEmpty(TxtInsPasswordControl.Text)
                )
            {
                res = false;
                this.lastMessage += base.GetLabel("LblManadatoryFields", "please fill mandatory fields") + "<br />";
            }
            if (TxtInsPassword.Text != TxtInsPasswordControl.Text)
            {
                res = false;
                this.lastMessage += base.GetLabel("LblPasswordNotMatching", "passwords do not match") + "<br />";
            }
            if (!Utility.IsValidEmail(TxtInsEmail.Text))
            {
                res = false;
                this.lastMessage += base.GetLabel("LblInvalidEmail", "invalid email") + "<br />";
            }
        }
        else if (this.EditorMode == MemberEditorMode.UpdateMode)    //update
        {
            if (!Utility.IsValidEmail(TxtUpdEmail.Text))
            {
                res = false;
                this.lastMessage += base.GetLabel("LblInvalidEmail", "invalid email") + "<br />";
            }
        }
        else if (this.EditorMode == MemberEditorMode.ChangePasswordMode)    //change password
        {
            if (string.IsNullOrEmpty(TxtPwdPassword.Text) ||
                string.IsNullOrEmpty(TxtPwdPasswordControl.Text))
            {
                res = false;
                this.lastMessage += base.GetLabel("LblManadatoryFields", "please fill mandatory fields") + "<br />";
            }
            if (TxtPwdPassword.Text != TxtPwdPasswordControl.Text)
            {
                res = false;
                this.lastMessage += base.GetLabel("LblPasswordNotMatching", "passwords do not match") + "<br />";
            }
        }
        return res;
    }

    private void setPanels()
    {
        PanelInsert.Visible = false;
        PanelInsert.Enabled = false;
        PanelUpdate.Visible = false;
        PanelUpdate.Enabled = false;
        PanelChangePassword.Visible = false;
        PanelChangePassword.Enabled = false;
        PanelRoles.Visible = false;
        PanelRoles.Enabled = false;

        switch (this.EditorMode)
        {
            case MemberEditorMode.InsertMode:
                PanelInsert.Visible = true;
                PanelInsert.Enabled = true;
                break;
            case MemberEditorMode.UpdateMode:
                PanelUpdate.Visible = true;
                PanelUpdate.Enabled = true;
                break;
            case MemberEditorMode.ChangePasswordMode:
                PanelChangePassword.Visible = true;
                PanelChangePassword.Enabled = true;
                break;
            case MemberEditorMode.ChangeRolesMode:
                PanelRoles.Visible = true;
                PanelRoles.Enabled = true;
                break;
        }

        LitUsername = base.GetLabel("LblUsername", "Username");
        LitEmail = base.GetLabel("LblEmail", "E-mail");
        LitPassword = base.GetLabel("LblPassword", "Password");
        LitPasswordControl = base.GetLabel("LblPasswordControl", "Repeat password");
        LitOldPassword = base.GetLabel("LblOldPassword", "Old password");


        if (this.isUserInAdminRole)
        {
            LitEnabled = base.GetLabel("LblEnabled", "Enabled");
            LitApproved = base.GetLabel("LblApproved", "Approved");
            LitComment = base.GetLabel("LblComment", "Notes");
            LitAllowMessages = base.GetLabel("LblAllowMessages", "Allow messages");
            LitAllowEmails = base.GetLabel("LblAllowEmails", "Allow emails");
            ChkInsEnabled.Visible = true;
            ChkUpdEnabled.Visible = true;
            ChkUpdApproved.Visible = true;
            TxtUpdComment.Visible = true;
            ChkInsAllowMessages.Visible = true;
            ChkUpdAllowMessages.Visible = true;
            ChkInsAllowEmails.Visible = true;
            ChkUpdAllowEmails.Visible = true;

            LitAccessCode = base.GetLabel("LblAccessCode", "Access code");
            LitAccessLevel = base.GetLabel("LblAccessLevel", "Access level");
            TxtUpdAccessCode.Visible = true;
            TxtUpdAccessLevel.Visible = true;
        }

        if (this.ShowFieldSex)
        {
            LitSex = base.GetLabel("LblSex", "Gender");
            //TxtInsSex.Visible = true;
        }
        if (this.ShowFieldCompanyName)
        {
            LitCompanyName = base.GetLabel("LblCompanyName", "Company name");
            TxtInsCompanyName.Visible = true;
            TxtUpdCompanyName.Visible = true;
        }
        if (this.ShowFieldVat)
        {
            LitVat = base.GetLabel("LblVat", "Vat");
            TxtInsVat.Visible = true;
            TxtUpdVat.Visible = true;
        }
        if (this.ShowFieldSsn)
        {
            LitSsn = base.GetLabel("LblSsn", "Ssn");
            TxtInsSsn.Visible = true;
            TxtUpdSsn.Visible = true;
        }
        if (this.ShowFieldFirstName)
        {
            LitFirstName = base.GetLabel("LblFirstName", "First name");
            TxtInsFirstName.Visible = true;
            TxtUpdFirstName.Visible = true;
        }
        if (this.ShowFieldSecondName)
        {
            LitSecondName = base.GetLabel("LblSecondName", "Second name");
            TxtInsSecondName.Visible = true;
            TxtUpdSecondName.Visible = true;
        }
        if (this.ShowFieldAddress1)
        {
            LitAddress1 = base.GetLabel("LblAddress1", "Address");
            TxtInsAddress1.Visible = true;
            TxtUpdAddress1.Visible = true;
        }
        if (this.ShowFieldAddress2)
        {
            LitAddress2 = base.GetLabel("LblAddress2", "");
            TxtInsAddress2.Visible = true;
            TxtUpdAddress2.Visible = true;
        }
        if (this.ShowFieldCity)
        {
            LitCity = base.GetLabel("LblCity", "City");
            TxtInsCity.Visible = true;
            TxtUpdCity.Visible = true;
        }
        if (this.ShowFieldState)
        {
            LitState = base.GetLabel("LblState", "State");
            TxtInsState.Visible = true;
            TxtUpdState.Visible = true;
        }
        if (this.ShowFieldZipCode)
        {
            LitZipCode = base.GetLabel("LblZipCode", "Zip");
            TxtInsZipCode.Visible = true;
            TxtUpdZipCode.Visible = true;
        }
        if (this.ShowFieldNation)
        {
            LitNation = base.GetLabel("LblNation", "Nation");
            DropInsNation.Visible = true;
            DropUpdNation.Visible = true;
            if (!this.EnabledFieldNation)
            {
                DropInsNation.Enabled = false;
                DropUpdNation.Enabled = false;
            }
        }
        if (this.ShowFieldTel1)
        {
            LitTel1 = base.GetLabel("LblTel1", "Telephone");
            TxtInsTel1.Visible = true;
            TxtUpdTel1.Visible = true;
        }
        if (this.ShowFieldMobile1)
        {
            LitMobile1 = base.GetLabel("LblMobile1", "Mobile phone");
            TxtInsMobile1.Visible = true;
            TxtUpdMobile1.Visible = true;
        }
        if (this.ShowFieldWebsite1)
        {
            LitWebsite1 = base.GetLabel("LblWebsite1", "Website");
            TxtInsWebsite1.Visible = true;
            TxtUpdWebsite1.Visible = true;
        }
    }

    private bool createUser()
    {
        bool res = false;
        PgnUser obj = null;
        bool isApproved = true;
        MembershipCreateStatus status = MembershipCreateStatus.Success;

        if (base.NeedApprovation)
            isApproved = false;

        //WARN: non esegue il metodo del mio provider!
        //obj = (PgnUser)Membership.CreateUser(
        //    getInsUsername(), TxtInsPassword.Text,
        //    TxtInsEmail.Text, "", "", isApproved, new Object(), out status);

        string username = TxtInsUserName.Text;
        if (!username.EndsWith(this.NewUserSuffix, StringComparison.CurrentCultureIgnoreCase))
            username += this.NewUserSuffix;

        obj = (PgnUser)Membership.CreateUser(
            username, TxtInsPassword.Text,
            TxtInsEmail.Text);

        /*switch (status)
        {
            case MembershipCreateStatus.DuplicateEmail:
                this.lastMessage = "duplicate email";
                break;
            case MembershipCreateStatus.DuplicateProviderUserKey:
                this.lastMessage = "duplicate user Id";
                break;
            case MembershipCreateStatus.DuplicateUserName:
                this.lastMessage = "duplicate username";
                break;
            case MembershipCreateStatus.InvalidAnswer:
                this.lastMessage = "invalid answer";
                break;
            case MembershipCreateStatus.InvalidEmail:
                this.lastMessage = "invalid email";
                break;
            case MembershipCreateStatus.InvalidPassword:
                this.lastMessage = "invalid password";
                break;
            case MembershipCreateStatus.InvalidProviderUserKey:
                this.lastMessage = "InvalidProviderUserKey";
                break;
            case MembershipCreateStatus.InvalidQuestion:
                this.lastMessage = "Invalid question";
                break;
            case MembershipCreateStatus.InvalidUserName:
                this.lastMessage = "Invalid username";
                break;
            case MembershipCreateStatus.ProviderError:
                this.lastMessage = "ProviderError";
                break;
            case MembershipCreateStatus.Success:
                res = true;
                break;
            case MembershipCreateStatus.UserRejected:
                break;
            default:
                break;
        }*/

        obj.AccessCode = this.DefaultAccessCode;
        obj.AccessLevel = this.DefaultAccessLevel;
        obj.Enabled = ChkInsEnabled.Checked;
        obj.CompanyName = TxtInsCompanyName.Text;
        obj.Vat = TxtInsVat.Text;
        obj.Ssn = TxtInsSsn.Text;
        obj.FirstName = TxtInsFirstName.Text;
        obj.SecondName = TxtInsSecondName.Text;
        obj.Address1 = TxtInsAddress1.Text;
        obj.Address2 = TxtInsAddress2.Text;
        obj.City = TxtInsCity.Text;
        obj.State = TxtInsState.Text;
        obj.ZipCode = TxtInsZipCode.Text;
        obj.Nation = DropInsNation.SelectedValue;
        obj.Tel1 = TxtInsTel1.Text;
        obj.Mobile1 = TxtInsMobile1.Text;
        obj.Website1 = TxtInsWebsite1.Text;
        obj.AllowMessages = ChkInsAllowMessages.Checked;
        obj.AllowEmails = ChkInsAllowEmails.Checked;
        Membership.UpdateUser(obj);

        //roles
        if (!string.IsNullOrEmpty(this.DefaultRoles))
        {
            string[] rolesToAdd = this.DefaultRoles.Split(',');
            string[] users = { obj.UserName };
            Roles.AddUsersToRoles(users, rolesToAdd);
        }
        if (base.NewRoleAsUser)
        {
            if (!Roles.RoleExists(obj.UserName))
                Roles.CreateRole(obj.UserName);

            string[] rolesToAdd = { obj.UserName };
            string[] users = { obj.UserName };
            Roles.AddUsersToRoles(users, rolesToAdd);
        }

        res = true;

        return res;
    }

    private bool updateUser()
    {
        bool res = false;
        PgnUser obj = null;

        obj = (PgnUser)Membership.GetUser(CurrentUser);
        obj.Email = TxtUpdEmail.Text;
        obj.Comment = TxtUpdComment.Text;
        obj.Enabled = ChkUpdEnabled.Checked;
        obj.IsApproved = ChkUpdApproved.Checked;
        obj.AccessCode = TxtUpdAccessCode.Text;
        int acccessLevel = 0;
        int.TryParse(TxtUpdAccessLevel.Text, out acccessLevel);
        obj.AccessLevel = acccessLevel;
        obj.CompanyName = TxtUpdCompanyName.Text;
        obj.Vat = TxtUpdVat.Text;
        obj.Ssn = TxtUpdSsn.Text;
        obj.FirstName = TxtUpdFirstName.Text;
        obj.SecondName = TxtUpdSecondName.Text;
        obj.Address1 = TxtUpdAddress1.Text;
        obj.Address2 = TxtUpdAddress2.Text;
        obj.City = TxtUpdCity.Text;
        obj.State = TxtUpdState.Text;
        obj.ZipCode = TxtUpdZipCode.Text;
        obj.Nation = DropUpdNation.SelectedValue;
        obj.Tel1 = TxtUpdTel1.Text;
        obj.Mobile1 = TxtUpdMobile1.Text;
        obj.Website1 = TxtUpdWebsite1.Text;
        obj.AllowMessages = ChkUpdAllowMessages.Checked;
        obj.AllowEmails = ChkUpdAllowEmails.Checked;

        Membership.UpdateUser(obj);
        res = true;
        return res;
    }

    private bool changeRoles()
    {
        bool res = false;
        PgnUser obj = null;

        obj = (PgnUser)Membership.GetUser(CurrentUser);
        //remove all roles for the user
        string[] rolesToRemove = Roles.GetRolesForUser(CurrentUser);
        if (rolesToRemove.Length > 0)
            Roles.RemoveUserFromRoles(CurrentUser, rolesToRemove);

        //add selected roles to user
        if (HiddenRolesInUser.Value.Length > 0)
        {
            string[] rolesToAdd = HiddenRolesInUser.Value.Split('|');
            string[] users = { CurrentUser };
            Roles.AddUsersToRoles(users, rolesToAdd);
        }

        res = true;
        return res;
    }

    private bool changePassword()
    {
        bool res = false;
        PgnUser obj = null;

        obj = (PgnUser)Membership.GetUser(CurrentUser);
        res = obj.ChangePassword(TxtPwdOldPassword.Text, TxtPwdPassword.Text);
        if (!res)
        {
            this.lastMessage = base.GetLabel("LblInvalidPassword", "invalid password");
        }
        return res;
    }

    private void initChangeRolesScript()
    {
        Utility.Script.RegisterStartupScript(this, "initChangeRolesScript", @"
                function moveListItem(list1, list2) {
                    var i;
                    for (i = list1.length - 1; i >= 0; i--) {
                        if (list1.options[i].selected) {
                            var opt = document.createElement('option');
                            opt.value = list1.options[i].value;
                            opt.text = list1.options[i].text;

                            list2.add(opt, null);
                            list1.remove(i);
                        }
                    }
                }

                function refreshHidden() {
                    var hidden = document.getElementById('"+ HiddenRolesInUser.ClientID + @"');
                    var list1 = document.getElementById('"+ ListRolesInUser.ClientID +@"');
                    var i;
                    hidden.value = '';
                    for (i = list1.length - 1; i >= 0; i--) {
                        hidden.value += list1.options[i].value;
                        if (i > 0) hidden.value += '|';
                    }
                }

                function addRole() {
                    moveListItem(
                        document.getElementById('" + ListRolesNotInUser.ClientID + @"'),
                        document.getElementById('" + ListRolesInUser.ClientID + @"')
                        );
                    refreshHidden();
                }

                function removeRole() {
                    moveListItem(
                        document.getElementById('" + ListRolesInUser.ClientID + @"'),
                        document.getElementById('" + ListRolesNotInUser.ClientID + @"')
                        );
                    refreshHidden();
                }
            ");
    }

    private void loginUser(string username, string pwd)
    {
        if (Membership.ValidateUser(username, pwd))
        {
            PgnUser user = (PgnUser)Membership.GetUser(username, true);
            if (user.Enabled && user.IsApproved)
            {
                FormsAuthentication.RedirectFromLoginPage(username, false);
                LogProvider.Write(this.BaseModule, username + " logged in");

                if (!string.IsNullOrEmpty(base.RedirectUrl))
                    Response.Redirect(base.RedirectUrl, false);
            }
        }
    }

    private void sendNotificationEmail(string username, string pwd, string toEmail, string userEmail)
    {
        var emailPage = new StaticPage();

        if (string.IsNullOrEmpty(base.NotificationEmailPageName))
            return;

        try
        {
            emailPage = new StaticPagesManager().GetStaticPageByName(base.NotificationEmailPageName);
            if (string.IsNullOrEmpty(emailPage.PageName))
                throw new ArgumentException("invalid NotificationEmailPageName");

            var smtp = new SmtpClient(AppSettingsManager.GetValue("SmtpServer"));
            using (smtp as IDisposable)
            {
                smtp.EnableSsl = false;
                if (!string.IsNullOrEmpty(AppSettingsManager.GetValue("SmtpUseSSL")))
                {
                    bool useSsl = false;
                    bool.TryParse(AppSettingsManager.GetValue("SmtpUseSSL"), out useSsl);
                    smtp.EnableSsl = useSsl;
                }
                if (!string.IsNullOrEmpty(AppSettingsManager.GetValue("SmtpPort")))
                {
                    int port = 25;
                    int.TryParse(AppSettingsManager.GetValue("SmtpPort"), out port);
                    smtp.Port = port;
                }
                if (!string.IsNullOrEmpty(AppSettingsManager.GetValue("SmtpUser")))
                {
                    smtp.Credentials = new NetworkCredential(
                        AppSettingsManager.GetValue("SmtpUser"),
                        AppSettingsManager.GetValue("SmtpPassword"));
                }

                MailMessage mail1 = new MailMessage();
                mail1.From = new MailAddress(AppSettingsManager.GetValue("EmailSender"));
                mail1.To.Add(toEmail);
                //mail1.Bcc = this.EmailAddressBcc;   //debug controllo formato email
                mail1.Subject = emailPage.PageTitle;
                mail1.IsBodyHtml = true;
                //available placeholders [[NewUsername]],[[NewUserPassword]],[[NewUserEmail]]
                mail1.Body = emailPage.PageContent
                    .Replace("[[NewUsername]]", username)
                    .Replace("[[NewUserPassword]]", pwd)
                    .Replace("[[NewUserEmail]]", userEmail);

                smtp.Send(mail1);
            }
        }
        catch (Exception e1)
        {
            LogProvider.Write(this.BaseModule, "sendNotificationEmail("+ username +",,"+ toEmail +","+ userEmail +") failed: " +e1.ToString(), TracerItemType.Error);
            Tracer.Log("sendNotificationEmail() failed " + e1.ToString(), TracerItemType.Error);
        }
    }

    private string getInsUsername()
    {
        return TxtInsUserName.Text + this.NewUserSuffix;
    }

    private void loadDropNation()
    {
        DropInsNation.Items.Clear();
        DropUpdNation.Items.Clear();

        foreach (var item in Utility.StaticData.Nations)
        {
            DropInsNation.Items.Add(new ListItem(item.Value, item.Key));
            DropUpdNation.Items.Add(new ListItem(item.Value, item.Key));
        }
    }
}