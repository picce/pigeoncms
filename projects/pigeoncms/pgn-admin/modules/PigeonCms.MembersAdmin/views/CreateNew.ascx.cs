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

public partial class Controls_CreateNew : PigeonCms.MemberEditorControl
{
    protected Controls_MemberEditorControl.MemberEditorMode MemberEditorMode
    {
        get
        {
            Controls_MemberEditorControl.MemberEditorMode res = Controls_MemberEditorControl.MemberEditorMode.InsertMode;
            try
            {
                int i = GetIntParam("MemberEditorMode", 0);
                res = (Controls_MemberEditorControl.MemberEditorMode)i;
            }
            catch{}
            return res;
        }
    }

    protected new void Page_Init(object sender, EventArgs e)
    {
        if (this.MemberEditorMode == Controls_MemberEditorControl.MemberEditorMode.InsertMode)
        {
            CaptchaControl1.Visible = true;
            CaptchaControl1.Enabled = true;
            CaptchaControl1.Text = base.GetLabel("LblCaptchaText", "enter the code shown");
            CaptchaControl1.CaptchaFontWarping = WebControlCaptcha.CaptchaImage.FontWarpFactor.Medium;
            CaptchaControl1.CaptchaBackgroundNoise = WebControlCaptcha.CaptchaImage.BackgroundNoiseLevel.High;
            CaptchaControl1.CaptchaLineNoise = WebControlCaptcha.CaptchaImage.LineNoiseLevel.Low;
            CaptchaControl1.CaptchaMaxTimeout = 180;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            switch (this.MemberEditorMode)
            {
                case Controls_MemberEditorControl.MemberEditorMode.ChangePasswordMode:
                    editPwd(PgnUserCurrent.UserName);
                    break;
                case Controls_MemberEditorControl.MemberEditorMode.UpdateMode:
                    editRow(PgnUserCurrent.UserName);
                    break;
                case Controls_MemberEditorControl.MemberEditorMode.InsertMode:
                default:
                    editRow("");
                    break;
            }
        }
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        LblOk.Text = "";
        LblErr.Text = "";

        initMemberEditor();

        if (!CaptchaControl1.UserValidated)
        {
            LblErr.Text = CaptchaControl1.ErrorMessage;
            return;
        }


        if (MemberEditor1.CheckForm())
        {
            if (MemberEditor1.SaveForm())
            {
                LblOk.Text = base.GetLabel("LblOperationCompleted", "Operation completed");
            }
            else
                LblErr.Text = MemberEditor1.LastMessage;
        }
        else
            LblErr.Text = MemberEditor1.LastMessage;
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        LblErr.Text = "";
        LblOk.Text = "";
    }


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
        }
        else
        {
            LitTitle.Text = base.GetLabel("LblUpdateUser", "Update user");
            MemberEditor1.EditorMode = Controls_MemberEditorControl.MemberEditorMode.UpdateMode;
            MemberEditor1.Obj2form();
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
        }
    }

    private void initMemberEditor()
    {
        var me = MemberEditor1;

        me.BaseModule.Id = base.BaseModule.Id;
        me.BaseModuleParams = base.BaseModule.ModuleParams;
        me.BaseModule.CssClass = base.BaseModule.CssClass;
        me.BaseModule.UseLog = base.BaseModule.UseLog;
    }

    #endregion
}
