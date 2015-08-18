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
using System.Globalization;
using PigeonCms;
using PigeonCms.Core.Helpers;


public partial class Controls_Messages: BaseModuleControl
{
    //const int COL_ID_INDEX = 14;
    const int VIEW_GRID = 0;
    const int VIEW_INSERT = 1;


    private Utility.TristateBool sendEmailToRecipient = Utility.TristateBool.NotSet;
    public Utility.TristateBool SendEmailToRecipient
    {
        get
        {
            int res = (int)sendEmailToRecipient;
            res = GetIntParam("SendEmailToRecipient", res);
            return (PigeonCms.Utility.TristateBool)res;
        }
        set { sendEmailToRecipient = value; }
    }

    protected new void Page_Init(object sender, EventArgs e)
    {
        base.Page_Init(sender, e);

        ContentEditorProvider.Configuration contentEditorConfig = null;
        contentEditorConfig = new ContentEditorProvider.Configuration();
        contentEditorConfig.FilesUploadUrl = "";
        contentEditorConfig.EditorType = ContentEditorProvider.Configuration.EditorTypeEnum.BasicHtml;
        ContentEditorProvider.InitEditor(this, Upd1, contentEditorConfig);
        BtnRefresh.Text = base.GetLabel("Refresh");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        LblOk.Text = "";
        LblErr.Text = "";

        if (!Page.IsPostBack)
        {
            loadDropFolderFilter();
            loadDropIsReadFilter();
        }
    }

    protected void Filter_Changed(object sender, EventArgs e)
    {
        if (DropFolderFilter.SelectedValue == "outbox")
            MsgList.Folder = Controls_MessageControl.FolderType.Outbox;
        else
            MsgList.Folder = Controls_MessageControl.FolderType.Inbox;

        switch (DropIsReadFilter.SelectedValue)
        {
            case "1":
                MsgList.IsRead = Utility.TristateBool.True;
                break;
            case "0":
                MsgList.IsRead = Utility.TristateBool.False;
                break;
            default:
                MsgList.IsRead = Utility.TristateBool.NotSet;
                break;
        }

        MsgList.ReloadList();
    }

    protected void MsgList_AfterDelete(object sender, Message.MessageEventArgs e)
    {
        LblOk.Text = "";
        LblErr.Text = "";
        if (!e.Result)
            LblErr.Text = e.Message;
    }

    protected void MsgList_ItemSelected(object sender, Message.MessageEventArgs e)
    {
        try
        {
            this.CurrentId = e.MessageId;
            MsgShow.ShowMessage(e.MessageId);
        }
        catch (Exception ex) 
        {
            LblErr.Text = ex.Message;
        }
    }

    protected void MsgShow_MessageSent(object sender, Message.MessageEventArgs e)
    {
        LblOk.Text = "";
        LblErr.Text = "";
        MsgList.ReloadList();
        if (e.Result)
        {
            switchToSendButton(false);
            MsgShow.ViewMode = Controls_MessageControl.MessagesViewMode.ShowMessage;
        }
        else
            LblErr.Text = e.Message;
    }

    protected void BtnRefresh_Click(object sender, EventArgs e)
    {
        try
        {
            MsgList.ReloadList();
        }
        catch (Exception e1) { LblErr.Text = e1.Message; }
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        try
        {
            switchToSendButton(true);
            MsgShow.CreateMessage("", "", "");
        }
        catch (Exception e1) { LblErr.Text = e1.Message; }
    }

    protected void BtnReply_Click(object sender, EventArgs e)
    {
        try
        {
            switchToSendButton(true);
            var msg = new MessagesManager().GetByKey(this.CurrentId);
            MsgShow.CreateMessage(msg.FromUser,
                "Re: " + msg.Title, msg.GetOriginalMessageHeader() + msg.Description);
            
        }
        catch (Exception e1) { LblErr.Text = e1.Message; }
    }

    protected void BtnForward_Click(object sender, EventArgs e)
    {
        try
        {
            switchToSendButton(true);
            var msg = new MessagesManager().GetByKey(this.CurrentId);
            MsgShow.CreateMessage("",
                "Frw: " + msg.Title, 
                msg.GetOriginalMessageHeader()+ msg.Description);
        }
        catch (Exception e1) { LblErr.Text = e1.Message; }
    }

    protected void BtnSend_Click(object sender, EventArgs e)
    {
        try
        {
            MessageProvider.SendMessageEnum sendEmail = MessageProvider.SendMessageEnum.UserSetting;
            switch (this.SendEmailToRecipient)
            {
                case Utility.TristateBool.False:
                    sendEmail = MessageProvider.SendMessageEnum.Never;
                    break;
                case Utility.TristateBool.True:
                    sendEmail = MessageProvider.SendMessageEnum.Always;
                    break;
                case Utility.TristateBool.NotSet:
                default:
                    break;
            }

            MsgShow.SendMessage(MessageProvider.SendMessageEnum.Always,
                sendEmail);
        }
        catch (Exception e1) { LblErr.Text = e1.Message; }
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        LblErr.Text = "";
        LblOk.Text = "";
        switchToSendButton(false);
        MsgShow.ViewMode = Controls_MessageControl.MessagesViewMode.ShowMessage;
    }


    #region private methods

    private void switchToSendButton(bool sendButton)
    {
        BtnSend.Visible = sendButton;
        BtnCancel.Visible = sendButton;
        BtnReply.Visible = !sendButton;
        BtnForward.Visible = !sendButton;
    }

    private void loadDropFolderFilter()
    {
        DropFolderFilter.Items.Clear();
        DropFolderFilter.Items.Add(new ListItem(base.GetLabel("Inbox", "Inbox"), "inbox"));
        DropFolderFilter.Items.Add(new ListItem(base.GetLabel("Outbox", "Outbox"), "outbox"));

        Utility.SetDropByValue(DropFolderFilter, "inbox");
    }

    private void loadDropIsReadFilter()
    {
        DropIsReadFilter.Items.Clear();
        DropIsReadFilter.Items.Add(new ListItem(Utility.GetLabel("LblSelectState", "Select state"), ""));
        DropIsReadFilter.Items.Add(new ListItem(base.GetLabel("Read", "Read"), "1"));
        DropIsReadFilter.Items.Add(new ListItem(base.GetLabel("Unread", "Unread"), "0"));
    }

    #endregion
}
