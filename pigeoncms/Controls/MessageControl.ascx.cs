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


public partial class Controls_MessageControl : PigeonCms.BaseModuleControl
{
    #region events

    public delegate void MessageDelegate(object sender, Message.MessageEventArgs e);
    public event MessageDelegate AfterDelete;
    public event MessageDelegate ItemSelected;
    public event MessageDelegate MessageSent;

    //public delegate void ItemSelectedDelegate(object sender, Message.MessageEventArgs e);

    #endregion

    const int COL_DELETE_INDEX = 5;
    const int VIEW_LIST_INDEX = 0;
    const int VIEW_MESSAGE_INDEX = 1;
    const int VIEW_NEW_INDEX = 2;

    public enum MessagesViewMode
    {
        ListMessages = 0,
        ShowMessage,
        InsertMessage
    }

    public enum FolderType
    {
        Inbox = 0,
        Outbox
    }

    public MessagesViewMode ViewMode
    {
        get
        {
            var res = MessagesViewMode.ListMessages;
            if (ViewState["ViewMode"] != null)
                res = (MessagesViewMode)ViewState["ViewMode"];
            return res;
        }
        set
        {
            ViewState["ViewMode"] = value;
            setViewMode();
        }
    }

    public FolderType Folder
    {
        get
        {
            var res = FolderType.Inbox;
            if (ViewState["Folder"] != null)
                res = (FolderType)ViewState["Folder"];
            return res;
        }
        set
        {
            ViewState["Folder"] = value;
        }
    }

    public Utility.TristateBool IsRead
    {
        get
        {
            var res = Utility.TristateBool.NotSet;
            if (ViewState["IsRead"] != null)
                res = (Utility.TristateBool)ViewState["IsRead"];
            return res;
        }
        set { ViewState["IsRead"] = value; }
    }

    private string lastMessage = "";
    public string LastMessage
    {
        get { return lastMessage; }
    }

    public bool AllowDelete
    {
        get 
        {
            var res = false;
            if (ViewState["AllowDelete"] != null)
                res = (bool)ViewState["AllowDelete"];
            return res;
        }
        set { ViewState["AllowDelete"] = value; }
    }

    public int NumberOfRowsPerPage
    {
        get
        {
            var res = 10;
            if (ViewState["NumberOfRowsPerPage"] != null)
                res = (int)ViewState["NumberOfRowsPerPage"];
            return res;
        }
        set
        {
            ViewState["NumberOfRowsPerPage"] = value;
        }
    }

    protected new void Page_Init(object sender, EventArgs e)
    {
        this.BaseModule = new Module();
        this.BaseModule.ModuleNamespace = "PigeonCms";
        this.BaseModule.ModuleName = "MessagesListControl";
        base.Page_Init(sender, e);

        Grid1.PageSize = this.NumberOfRowsPerPage;
        Grid1.Columns[COL_DELETE_INDEX].Visible = this.AllowDelete;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void ObjDs1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        var filter = new MessagesFilter();

        if (this.Folder == FolderType.Outbox)
            filter.FromUser = PgnUserCurrent.UserName;
        else
            filter.ToUserLike = PgnUserCurrent.UserName;
        filter.IsRead = this.IsRead;
        e.InputParameters["filter"] = filter;
        e.InputParameters["sort"] = "";
    }

    protected void Grid1_PreRender(object sender, EventArgs e)
    {
        Grid1.UseAccessibleHeader = true;
        if (Grid1.HeaderRow != null)
            Grid1.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    protected void Grid1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            selectRow(int.Parse(e.CommandArgument.ToString()));
        }
        if (e.CommandName == "DeleteRow")
        {
            deleteRow(int.Parse(e.CommandArgument.ToString()));
        }
        //Enabled
        if (e.CommandName == "ImgStarredOk")
        {
            setFlag(Convert.ToInt32(e.CommandArgument), false, "starred");
            Grid1.DataBind();
        }
        if (e.CommandName == "ImgStarredKo")
        {
            setFlag(Convert.ToInt32(e.CommandArgument), true, "starred");
            Grid1.DataBind();
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
            var item = new PigeonCms.Message();
            item = (PigeonCms.Message)e.Row.DataItem;

            LinkButton LnkSubject = (LinkButton)e.Row.FindControl("LnkSubject");
            string subject = Utility.Html.GetTextPreview(item.Title, 50, "");
            if (item.IsRead)
                LnkSubject.Text = subject;
            else
                LnkSubject.Text = "<b>" + subject + "</b>";

            Literal LitBody = (Literal)e.Row.FindControl("LitBody");
            LitBody.Text = Utility.Html.GetTextPreview(item.Description, 200, "");

            Literal LitDateInserted = (Literal)e.Row.FindControl("LitDateInserted");
            LitDateInserted.Text = item.DateInserted.ToString();

            //Starred
            if (item.IsStarred)
            {
                Image img1 = (Image)e.Row.FindControl("ImgStarredOk");
                img1.Visible = true;
            }
            else
            {
                Image img1 = (Image)e.Row.FindControl("ImgStarredKo");
                img1.Visible = true;
            }
        }
    }

    protected void MultiView1_ActiveViewChanged(object sender, EventArgs e)
    {
    }

    public void ReloadList()
    {
        Grid1.DataBind();
    }

    public void ShowMessage(int recordId)
    {
        var obj = new PigeonCms.Message();
        MultiView1.ActiveViewIndex = VIEW_MESSAGE_INDEX;

        clearMessage();
        CurrentId = recordId;
        if (CurrentId > 0)
        {
            obj = new MessagesManager().GetByKey(CurrentId);
            obj2message(obj);
        }
    }

    public void CreateMessage(string to, string subject, string body)
    {
        MultiView1.ActiveViewIndex = VIEW_NEW_INDEX;
        TxtTo.Text = to;
        TxtTitle.Text = subject;
        TxtDescription.Text = body;
    }

    public void SendMessage(MessageProvider.SendMessageEnum sendLocalMessage, 
        MessageProvider.SendMessageEnum sendEmail)
    {
        bool res = false;

        try
        {
            var o1 = new Message();
            form2obj(o1);
            MessageProvider.SendMessage(Utility.String2List(o1.ToUser, ";"), o1, 
                sendLocalMessage, sendEmail);
            res = true;
        }
        catch (Exception e1)
        {
            this.lastMessage = Utility.GetLabel("RECORD_ERR_MSG") + "<br />" + e1.ToString();
        }
        finally
        {
        }
        if (this.MessageSent != null)
            this.MessageSent(this,
                new Message.MessageEventArgs(0, "send", this.LastMessage, res));
    }


    private void selectRow(int recordId)
    {
        if (this.ItemSelected != null)
        {
            var man = new MessagesManager();
            var o1 = man.GetByKey(recordId);
            if (!o1.IsRead)
            {
                //flag as read
                o1.IsRead = true;
                man.Update(o1);
                Grid1.DataBind();
            }
            this.ItemSelected(this,
                new Message.MessageEventArgs(recordId, "select", "", true));
        }
    }

    private void clearMessage()
    {
        LitSubject.Text = "";
        LitCreated.Text = "";
        LitUser.Text = "";
        LitDescription.Text = "";
        LitImages.Text = "";
        LitFiles.Text = "";
    }

    private void obj2message(Message obj)
    {
        LitSubject.Text = obj.Title;
        LitCreated.Text = obj.DateInserted.ToString();
        LitUser.Text = "From " + obj.FromUser + " to " + obj.ToUser;
        LitDescription.Text = obj.Description;
        LitImages.Text = "";
        LitFiles.Text = "";
    }

    private void form2obj(Message obj)
    {
        obj.ToUser = TxtTo.Text;
        obj.Title = TxtTitle.Text;
        obj.Description = TxtDescription.Text;
    }

    private bool checkForm()
    {
        bool res = true;

        return res;
    }

    private void clearForm()
    {
        TxtTo.Text = "";
        TxtTitle.Text = "";
        TxtDescription.Text = "";
    }

    private void deleteRow(int recordId)
    {
        bool res = true;
        try
        {
            if (!PgnUserCurrent.IsAuthenticated)
                throw new Exception("user not authenticated");

            new MessagesManager().DeleteById(recordId);
            Grid1.DataBind();
        }
        catch (Exception e)
        {
            res = false;
            this.lastMessage = e.Message;
        }
        if (this.AfterDelete != null)
            this.AfterDelete(this, 
                new Message.MessageEventArgs(recordId, "delete", this.LastMessage, res));
    }

    private void setFlag(int recordId, bool value, string flagName)
    {
        try
        {
            var man = new MessagesManager();
            var o1 = man.GetByKey(recordId);
            switch (flagName.ToLower())
            {
                case "starred":
                    o1.IsStarred = value;
                    break;
                case "read":
                    o1.IsRead = value;
                    break;
                default:
                    break;
            }
            man.Update(o1);
        }
        catch (Exception e1)
        {
            this.lastMessage = Utility.GetLabel("RECORD_ERR_MSG") + "<br />" + e1.ToString();
        }
        finally { }
    }

    private void setViewMode()
    {
        switch (ViewMode)
        {
            case MessagesViewMode.ListMessages:
                MultiView1.ActiveViewIndex = VIEW_LIST_INDEX;
                break;
            case MessagesViewMode.ShowMessage:
                MultiView1.ActiveViewIndex = VIEW_MESSAGE_INDEX;
                break;
            case MessagesViewMode.InsertMessage:
                MultiView1.ActiveViewIndex = VIEW_NEW_INDEX;
                break;
            default:
                break;
        }

    }
}