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
using PigeonCms;
using System.Collections.Generic;
using System.Text;

public partial class Controls_TicketsList : PigeonCms.ItemsAdminControl //PigeonCms.ItemsControl<TicketItem, TicketItemFilter>
{
    const int VIEW_LIST = 0;
    const int VIEW_THREAD = 1;
    const int VIEW_INSERT = 2;

    const int COL_SUBJECT_INDEX = 0;
    const int COL_CUSTOMER_INDEX = 2;
    const int COL_STATUS_INDEX = 3;
    const int COL_PRIORITY_INDEX = 4;
    const int COL_FLAGGED_INDEX = 5;
    const int COL_OPERATOR_INDEX = 6;
    const int COL_DATEINSERTED_INDEX = 7;
    const int COL_LASTACTIVITY_INDEX = 8;
    const int COL_DELETE_INDEX = 9;
    const int COL_ID_INDEX = 10;

    const string SUPPORT_MANAGER_ROLE = "supportManager";   //see all tickets
    const string SUPPORT_OPERATOR_ROLE = "supportOperator"; //backend support operator
    const string SUPPORT_USER_ROLE = "supportUser";         //final user of support service (customer)


    private bool showCheckSendEmailToUserInserted = true;
    public bool ShowCheckSendEmailToUserInserted
    {
        get { return GetBoolParam("ShowCheckSendEmailToUserInserted", showCheckSendEmailToUserInserted); }
    }

    private bool showCustomer = true;
    public bool ShowCustomer
    {
        get { return GetBoolParam("ShowCustomer", showCustomer); }
    }

    private bool showTemplate = true;
    public bool ShowTemplate
    {
        get { return GetBoolParam("ShowTemplate", showTemplate); }
    }

    int messageTemplatesSectionId = 0;
    //specific params 
    protected int MessageTemplatesSectionId
    {
        get { return GetIntParam("MessageTemplatesSectionId", messageTemplatesSectionId); }
        set { messageTemplatesSectionId = value; }
    }

    string statusFilterDefaultValue = "0";
    protected string StatusFilterDefaultValue
    {
        get { return GetStringParam("StatusFilterDefaultValue", statusFilterDefaultValue); }
    }

    protected string ListString
    {
        get
        {
            string res = "";
            if (ViewState["ListString"] != null)
                res = (string)ViewState["ListString"];
            return res;
        }
        set { ViewState["ListString"] = value; }
    }

    protected int CurrentThreadId
    {
        get
        {
            int res = 0;
            if (ViewState["CurrentThreadId"] != null)
                res = (int)ViewState["CurrentThreadId"];
            return res;
        }
        set { ViewState["CurrentThreadId"] = value; }
    }

    protected string GridSortExpression
    {
        get 
        {
            string res = "";
            if (ViewState["GRID_SORT_EXPRESSION"] != null)
                res = (string)ViewState["GRID_SORT_EXPRESSION"];
            return res;
        }
        set { ViewState["GRID_SORT_EXPRESSION"] = value; }
    }

    protected SortDirection GridSortDirection
    {
        get
        {
            SortDirection res = System.Web.UI.WebControls.SortDirection.Ascending;
            if (ViewState["GRID_SORT_DIRECTION"] != null)
                res = (SortDirection)ViewState["GRID_SORT_DIRECTION"];
            return res;
        }
        set { ViewState["GRID_SORT_DIRECTION"] = value; }
    }

    protected SortDirection ThreadSortDirection
    {
        get
        {
            SortDirection res = System.Web.UI.WebControls.SortDirection.Ascending;
            if (ViewState["THREAD_SORT_DIRECTION"] != null)
                res = (SortDirection)ViewState["THREAD_SORT_DIRECTION"];
            return res;
        }
        set { ViewState["THREAD_SORT_DIRECTION"] = value; }
    }

    protected new void Page_Init(object sender, EventArgs e)
    {
        base.Page_Init(sender, e);

        loadLabelsText();

        ContentEditorProvider.Configuration contentEditorConfig = null;
        contentEditorConfig = new ContentEditorProvider.Configuration();
        contentEditorConfig.FilesUploadUrl = "";
        contentEditorConfig.EditorType = ContentEditorProvider.Configuration.EditorTypeEnum.BasicHtml;
        ContentEditorProvider.InitEditor(this, Upd1, contentEditorConfig);

        ChkSendEmailToUserInserted.Visible = this.ShowCheckSendEmailToUserInserted;

        DropCustomers.Visible = this.ShowCustomer;
        DropTemplates.Visible = this.ShowTemplate;

        //specific fields
        Grid1.Columns[COL_CUSTOMER_INDEX].HeaderText = base.GetLabel("Customer", "Customer");
        Grid1.Columns[COL_CUSTOMER_INDEX].Visible = (Roles.IsUserInRole("admin") 
            || Roles.IsUserInRole(SUPPORT_MANAGER_ROLE)
            || Roles.IsUserInRole(SUPPORT_OPERATOR_ROLE));
        Grid1.Columns[COL_DELETE_INDEX].Visible = (Roles.IsUserInRole("admin") || Roles.IsUserInRole(SUPPORT_MANAGER_ROLE));
        Grid1.Columns[COL_ID_INDEX].Visible = (Roles.IsUserInRole("admin") || Roles.IsUserInRole(SUPPORT_MANAGER_ROLE));

        DropChangeCustomer.Visible = (Roles.IsUserInRole("admin")
            || Roles.IsUserInRole(SUPPORT_MANAGER_ROLE)
            || Roles.IsUserInRole(SUPPORT_OPERATOR_ROLE));

        DropAssignedUser.Visible = (Roles.IsUserInRole("admin") 
            || Roles.IsUserInRole(SUPPORT_MANAGER_ROLE) 
            || Roles.IsUserInRole(SUPPORT_OPERATOR_ROLE));

        DropCustomersFilter.Visible = (Roles.IsUserInRole("admin")
            || Roles.IsUserInRole(SUPPORT_MANAGER_ROLE)
            || Roles.IsUserInRole(SUPPORT_OPERATOR_ROLE));

        DropAssignedUserFilter.Visible = (Roles.IsUserInRole("admin")
            || Roles.IsUserInRole(SUPPORT_MANAGER_ROLE)
            || Roles.IsUserInRole(SUPPORT_OPERATOR_ROLE));

        DropUserInsertedFilter.Visible = (Roles.IsUserInRole("admin")
            || Roles.IsUserInRole(SUPPORT_MANAGER_ROLE)
            || Roles.IsUserInRole(SUPPORT_OPERATOR_ROLE));

        ChkMyTickets.Visible = (Roles.IsUserInRole("admin")
            || Roles.IsUserInRole(SUPPORT_MANAGER_ROLE)
            || Roles.IsUserInRole(SUPPORT_OPERATOR_ROLE));
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            loadDropStatusFilter();
            loadDropCustomersFilter();
            loadDropPriority();
            loadDropDatesRangeFilter();
            loadDropCategoriesFilter(base.SectionId);
            loadDropThreadActions(0);
            loadDropTemplates();
            loadDropAssignedUserFilter();
            loadDropUserInsertedFilter();

            loadGrid();
        }
    }

    protected void DropTemplates_Changed(object sender, EventArgs e)
    {
        int itemId = 0;
        int.TryParse(DropTemplates.SelectedValue, out itemId);
        setTemplate(itemId);
    }

    protected void DropChangeCustomer_Changed(object sender, EventArgs e)
    {
        int customerId = 0;
        int.TryParse(DropChangeCustomer.SelectedValue, out customerId);
        changeCustomer(this.CurrentThreadId, customerId);
    }

    protected void DropAssignedUser_Changed(object sender, EventArgs e)
    {
        changeAssignedUser(this.CurrentThreadId, DropAssignedUser.SelectedValue.ToLower());
    }

    protected void DropThreadActions_Changed(object sender, EventArgs e)
    {
        changeThreadStatus(this.CurrentThreadId, DropThreadActions.SelectedValue.ToLower());
    }

    protected void DropThreadOrder_Changed(object sender, EventArgs e)
    {
        if (DropThreadOrder.SelectedValue.ToLower() == "desc")
            this.ThreadSortDirection = System.Web.UI.WebControls.SortDirection.Descending;
        else
            this.ThreadSortDirection = System.Web.UI.WebControls.SortDirection.Ascending;

        loadThread(this.CurrentThreadId);
    }

    protected void ChkMyTickets_Changed(Object sender, EventArgs e) 
    {
        string userAssigned = "";
        if (ChkMyTickets.Checked)
            userAssigned = PgnUserCurrent.UserName;
        Utility.SetDropByValue(DropAssignedUserFilter, userAssigned);
        loadGrid();
    }

    protected void Filter_Changed(object sender, EventArgs e)
    {
        loadGrid();
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
        try
        {
            editRow(0, 0);
        }
        catch (Exception e1) { LblErr.Text = e1.Message; }
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        if (checkForm())
        {
            if (saveForm(false))
                MultiView1.ActiveViewIndex = VIEW_THREAD;
        }
    }

    protected void BtnSaveClose_Click(object sender, EventArgs e)
    {
        if (checkForm())
        {
            if (saveForm(true))
                MultiView1.ActiveViewIndex = VIEW_THREAD;
        }
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        LblErr.Text = "";
        LblOk.Text = "";

        new FilesGallery().RemoveSessionTempFolder();  


        if (this.CurrentThreadId == 0)
            MultiView1.ActiveViewIndex = VIEW_LIST;
        else
            MultiView1.ActiveViewIndex = VIEW_THREAD;
    }

    protected void BtnBackToList_Click(object sender, EventArgs e)
    {
        loadGrid();
        MultiView1.ActiveViewIndex = VIEW_LIST;
    }

    protected void BtnReply_Click(object sender, EventArgs e)
    {
        try
        {
            editRow(this.CurrentThreadId, 0);
        }
        catch (Exception e1) { LblErr.Text = e1.Message; }
    }

    protected void Grid1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            try
            {
                int recordId = 0;
                int.TryParse(e.CommandArgument.ToString(), out recordId);
                loadThread(recordId);
                //editRow(recordId, recordId);
            }
            catch (Exception e1) { LblErr.Text = e1.Message; }
        }
        if (e.CommandName == "DeleteRow")
        {
            deleteRow(int.Parse(e.CommandArgument.ToString()));
        }
        if (e.CommandName == "ImgFlaggedOk")
        {
            setFlag(Convert.ToInt32(e.CommandArgument), false, "flagged");
            loadGrid();
        }
        if (e.CommandName == "ImgFlaggedKo")
        {
            setFlag(Convert.ToInt32(e.CommandArgument), true, "flagged");
            loadGrid();
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
            var currItem = (TicketItem)e.Row.DataItem;
            //string link = base.GetLinkAddress(currItem);

            LinkButton LnkTitle = (LinkButton)e.Row.FindControl("LnkTitle");
            Literal LitCategoryTitle = (Literal)e.Row.FindControl("LitCategoryTitle");
            Literal LitCustomer = (Literal)e.Row.FindControl("LitCustomer");
            Literal LitStatus = (Literal)e.Row.FindControl("LitStatus");
            Literal LitPriority = (Literal)e.Row.FindControl("LitPriority");
            Literal LitUserAssigned = (Literal)e.Row.FindControl("LitUserAssigned");
            Literal LitInserted = (Literal)e.Row.FindControl("LitInserted");
            Literal LitUpdated = (Literal)e.Row.FindControl("LitUpdated");

            LnkTitle.Text = Utility.Html.GetTextPreview(currItem.Title, 100, "");
            LitCategoryTitle.Text = currItem.Category.Title;
            LitCustomer.Text = Utility.Html.GetTextPreview(currItem.Customer.CompanyName, 30, "");

            //Flag
            if (currItem.Flagged)
            {
                ImageButton img1 = (ImageButton)e.Row.FindControl("ImgFlaggedOk");
                img1.Visible = true;
                if (!Roles.IsUserInRole("admin"))
                {
                    img1.Enabled = false;
                    img1.Style.Add("pointer-events", "none");
                }
                
            }
            else
            {
                ImageButton img1 = (ImageButton)e.Row.FindControl("ImgFlaggedKo");
                img1.Visible = true;
                //img1.Enabled = Roles.IsUserInRole("admin");
                if (!Roles.IsUserInRole("admin"))
                {
                    img1.Enabled = false;
                    img1.Style.Add("pointer-events", "none");
                }
            }

            string userAssigned = "<i>" + base.GetLabel("NotAssigned", "not assigned") + "</i>";
            if (!string.IsNullOrEmpty(currItem.UserAssigned))
                userAssigned =  currItem.UserAssigned;
            LitUserAssigned.Text = userAssigned;

            LitInserted.Text = currItem.DateInserted + " - " +  Utility.Html.GetTextPreview(currItem.UserInserted, 15, "");
            LitUpdated.Text = currItem.DateUpdated + " - " +  Utility.Html.GetTextPreview(currItem.UserUpdated, 15, "");

            try 
            {
                string val = Enum.GetName(typeof(TicketItem.TicketStatusEnum), currItem.Status);
                LitStatus.Text = base.GetLabel(val, val); 
            } 
            catch { };

            try 
            {
                string val = Enum.GetName(typeof(TicketItem.TicketPriorityEnum), currItem.Priority);
                LitPriority.Text = base.GetLabel(val, val);
            }
            catch { };

        }
    }

    protected void Grid1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Grid1.PageIndex = e.NewPageIndex;
        loadGrid();
    }

    protected void Grid1_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            if (this.GridSortExpression == e.SortExpression)
            {
                //change sort direction
                if (this.GridSortDirection == System.Web.UI.WebControls.SortDirection.Ascending)
                    this.GridSortDirection = System.Web.UI.WebControls.SortDirection.Descending;
                else
                    this.GridSortDirection = System.Web.UI.WebControls.SortDirection.Ascending;
            }
            else
                this.GridSortDirection = System.Web.UI.WebControls.SortDirection.Ascending;

            this.GridSortExpression = e.SortExpression;
            loadGrid();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void MultiView1_ActiveViewChanged(object sender, EventArgs e)
    {
        if (MultiView1.ActiveViewIndex == VIEW_LIST)
        { }
        else if (MultiView1.ActiveViewIndex == VIEW_THREAD)
        { }
        else if (MultiView1.ActiveViewIndex == VIEW_INSERT)
        { }
    }

    #region private methods

    private void loadGrid()
    {
        int status = -1;
        if (DropStatusFilter.SelectedValue != "")
            int.TryParse(DropStatusFilter.SelectedValue, out status);

        int priority = -1;
        if (DropPriorityFilter.SelectedValue != "")
            int.TryParse(DropPriorityFilter.SelectedValue, out priority);

        int catId = 0;
        int.TryParse(DropCategoriesFilter.SelectedValue, out catId);

        int customerId = 0;
        int.TryParse(DropCustomersFilter.SelectedValue, out customerId);

        DatesRange datesRange = new DatesRange();
        if (DropDatesRangeFilter.SelectedValue != "")
        {
            DatesRange.RangeType rangeType =
                (DatesRange.RangeType)Enum.Parse(typeof(DatesRange.RangeType), DropDatesRangeFilter.SelectedValue);
            datesRange = new DatesRange(rangeType);
        }

        string userAssigned = DropAssignedUserFilter.SelectedValue;
        if (ChkMyTickets.Checked)
        {
            userAssigned = PgnUserCurrent.UserName;
            Utility.SetDropByValue(DropAssignedUserFilter, userAssigned);
        }

        var filter = new TicketItemFilter();
        var list = new List<TicketItem>();
        
        filter.CategoryId = catId;
        filter.CustomerId = customerId;
        filter.TitleSearch = TxtTitleFilter.Text;
        filter.UserAssigned = userAssigned;
        filter.UserInserted = DropUserInsertedFilter.SelectedValue;
        filter.Status = status;
        filter.Priority = priority;
        filter.ItemInsertedRange = datesRange;

        list = new TicketItemsManager(true, false).GetByFilter(filter, "");
        var comparer = new TicketItem.TicketItemComparer(this.GridSortExpression, this.GridSortDirection);
        list.Sort(comparer);

        Grid1.DataSource = list;
        Grid1.DataBind();
    }

    private void loadThread(int ticketItemdId)
    {
        this.CurrentThreadId = ticketItemdId;
        loadDropThreadActions(ticketItemdId);
        loadDropAssignedUser(ticketItemdId);
        loadDropThreadOrder();

        var currItem = new TicketItem();
        if (ticketItemdId > 0)
            currItem = new ItemsManager<TicketItem, TicketItemFilter>(true, true)
                .GetByKey(ticketItemdId);

        if (currItem.Status == (int)TicketItem.TicketStatusEnum.Locked)
            BtnReply.Visible = false;
        else
            BtnReply.Visible = true;

        int counter = 0;
        string status = Enum.GetName(typeof(TicketItem.TicketStatusEnum), currItem.Status);
        status = base.GetLabel(status, status);
        string userAssigned = base.GetLabel("NotAssigned", "not assigned");
        if (!string.IsNullOrEmpty(currItem.UserAssigned))
            userAssigned = currItem.UserAssigned;

        ListString = base.GetLabel("Status", "Status") + ": <i>" + status + "</i><br />";
        ListString += base.GetLabel("AssignedTo", "Assigned to") + ": <i>" + userAssigned + "</i><br />";
        if (currItem.CustomerId > 0)
            ListString += base.GetLabel("Customer", "Customer") + ": <i>" + currItem.Customer.CompanyName + "</i><br />";
        ListString += base.GetLabel("TicketId", "Ticket ID") + ": <i>" + currItem.Id + "</i><br />";

        string flagged = "";
        if (currItem.Flagged)
            flagged += "<img src='"+ Utility.GetThemedImageSrc("starOn.png") +"' alt='flag' />&nbsp;";

        ListString += "<div class='itemTitle'>"+ flagged + base.GetLabel("Subject", "Subject") + ": " + currItem.Title + "</div>";
        
        var comparer = new Item.ItemComparer("", this.ThreadSortDirection);
        currItem.ThreadList.Sort(comparer);

        ListString += "<ul class='"+ base.BaseModule.CssClass +"'>";
        for (int i = 0; i < currItem.ThreadList.Count; i++)
        {
            var item = currItem.ThreadList[i];
            string imgUrl = "";
            string link = "";
            string sFiles = "";
            string sImages = "";
            string cssClass = base.BaseModule.CssClass;


            if (item.Files.Count > 0)
            {
                sFiles = "<ul class='" + base.BaseModule.CssClass + "'>";
                foreach (var file in item.Files)
                {
                    sFiles += "<li class='" + base.BaseModule.CssClass + "'>"
                    + "<a class='" + base.BaseModule.CssClass + "' href='" + file.FileUrl + "' "
                    + Utility.AddTracking(file.FileUrl, this.StaticFilesTracking) + " target='blank'>"
                    + "<img src='" + PhotoManager.GetFileIconSrc(file) + "' />"
                    + "<span>" + file.FileName + "</span>"
                    + "</a>"
                    + "</li>";
                }
                sFiles += "</ul>";
            }
            if (item.Images.Count > 0)
            {
                sImages = "<ul class='" + base.BaseModule.CssClass + "'>";
                foreach (var file in item.Images)
                {
                    sImages += "<li class='" + base.BaseModule.CssClass + "'>"
                    + "<a class='" + base.BaseModule.CssClass + "' href='" + file.FileUrl + "' "
                    + Utility.AddTracking(file.FileUrl, this.StaticFilesTracking) + " target='blank'>"
                    + "<span>" + file.FileName + "</span>"
                    + "</a>"
                    + "</li>";
                }
                sImages += "</ul>";
            }

            //link = base.GetLinkAddress(item);
            ListString += "<li class='" + cssClass + "'>"
            + "<div class='" + cssClass + " itemDate'>" + item.DateInserted + " - "+ item.UserInserted +"</div>"
            + "<div class='" + cssClass + " itemUser'></div>"
            + "<div class='" + cssClass + " itemTitle'></div>"
            + "<div class='" + cssClass + " itemDescription'>" + item.DescriptionParsed + "</div>"
            + "<div class='" + cssClass + " itemFiles'>" + sFiles + "</div>"
            + "<div class='" + cssClass + " itemImages'>" + sImages + "</div>"
            + "</li>";

            counter++;
        }
        ListString += "</ul>";
        MultiView1.ActiveViewIndex = VIEW_THREAD;
    }

    private void loadDropThreadOrder()
    {
        DropThreadOrder.Items.Clear();
        DropThreadOrder.Items.Add(new ListItem(base.GetLabel("Asc", "asc"), "asc"));
        DropThreadOrder.Items.Add(new ListItem(base.GetLabel("Desc", "desc"), "desc"));
        if (this.ThreadSortDirection == System.Web.UI.WebControls.SortDirection.Descending)
            Utility.SetDropByValue(DropThreadOrder, "desc");
    }

    private void loadDropThreadActions(int threadId)
    {
        DropThreadActions.Items.Clear();

        var o1 = new TicketItem();
        if (threadId > 0)
        {
            o1 = new ItemsManager<TicketItem, TicketItemFilter>(true, true).GetByKey(threadId);

            var status = TicketItem.TicketStatusEnum.Open;
            status = (TicketItem.TicketStatusEnum)int.Parse(o1.Status.ToString());

            DropThreadActions.Items.Add(new ListItem(base.GetLabel("Actions", "--actions--"), ""));

            if (Roles.IsUserInRole("admin")
                || Roles.IsUserInRole(SUPPORT_MANAGER_ROLE)
                || Roles.IsUserInRole(SUPPORT_OPERATOR_ROLE))
            {
                //DropThreadActions.Items.Add(new ListItem(base.GetLabel("Close", "close"), "close"));
                DropThreadActions.Items.Add(new ListItem(base.GetLabel("Working", "in progress"), "working"));
                DropThreadActions.Items.Add(new ListItem(base.GetLabel("Reopen", "re-open"), "open"));
                DropThreadActions.Items.Add(new ListItem(base.GetLabel("Lock", "lock"), "lock"));
            }
            else if (Roles.IsUserInRole(SUPPORT_USER_ROLE))
            {
                if (status == TicketItem.TicketStatusEnum.Locked) 
                { }
                else if (status == TicketItem.TicketStatusEnum.Open || status == TicketItem.TicketStatusEnum.WorkInProgress)
                { 
                    //DropThreadActions.Items.Add(new ListItem(base.GetLabel("Close", "close"), "close")); 
                }
                else if (status == TicketItem.TicketStatusEnum.Closed)
                {
                    DropThreadActions.Items.Add(new ListItem(base.GetLabel("Reopen", "re-open"), "open"));
                }
            }
        }
    }

    private void loadDropStatusFilter()
    {
        DropStatusFilter.Items.Clear();
        DropStatusFilter.Items.Add(
            new ListItem(base.GetLabel("StatusFilter", "--status--"), ""));
        foreach (string item in Enum.GetNames(typeof(TicketItem.TicketStatusEnum)))
        {
            int value = (int)Enum.Parse(typeof(TicketItem.TicketStatusEnum), item);
            string text = base.GetLabel(item, item);
            
            //if (value == (int)TicketItem.TicketStatusEnum.WorkInProgress) continue;

            ListItem listItem = new ListItem(text, value.ToString());
            DropStatusFilter.Items.Add(listItem);
        }
        Utility.SetDropByValue(DropStatusFilter, this.StatusFilterDefaultValue);
        //Utility.SetDropByValue(DropStatusFilter, ((int)TicketItem.TicketStatusEnum.Open).ToString());
    }

    private void loadDropPriority()
    {
        DropPriorityFilter.Items.Clear();
        DropPriorityFilter.Items.Add(
            new ListItem(base.GetLabel("PriorityFilter", "--priority--"), ""));

        DropPriority.Items.Clear();

        foreach (string item in Enum.GetNames(typeof(TicketItem.TicketPriorityEnum)))
        {
            int value = (int)Enum.Parse(typeof(TicketItem.TicketPriorityEnum), item);

            DropPriorityFilter.Items.Add( new ListItem(base.GetLabel(item, item), value.ToString()));
            DropPriority.Items.Add( new ListItem(base.GetLabel(item, item), value.ToString()));
        }
    }

    private void loadDropAssignedUser(int threadId)
    {
        DropAssignedUser.Items.Clear();
        DropAssignedUser.Items.Add(
            new ListItem(base.GetLabel("AssignTo", "--assign to--"), ""));

        //var o1 = new TicketItem();
        if (threadId > 0)
        {
            //o1 = new ItemsManager<TicketItem, TicketItemFilter>(true, true).GetByKey(threadId);
            var addCurrentAssignedUser = true;
            foreach (var user in Roles.GetUsersInRole(SUPPORT_OPERATOR_ROLE))
            {
                DropAssignedUser.Items.Add(new ListItem(user, user));
                if (user == PgnUserCurrent.UserName)
                    addCurrentAssignedUser = false;
            }
            if (addCurrentAssignedUser)
                DropAssignedUser.Items.Add(new ListItem(PgnUserCurrent.UserName, PgnUserCurrent.UserName));
        }
    }

    private void loadDropAssignedUserFilter()
    {
        DropAssignedUserFilter.Items.Clear();
        DropAssignedUserFilter.Items.Add(
            new ListItem(base.GetLabel("OperatorFilter", "--operator--"), ""));

        foreach (var user in Roles.GetUsersInRole(SUPPORT_OPERATOR_ROLE))
        {
            DropAssignedUserFilter.Items.Add(new ListItem(user, user));
        }
    }

    private void loadDropUserInsertedFilter()
    {
        DropUserInsertedFilter.Items.Clear();
        DropUserInsertedFilter.Items.Add(
            new ListItem(base.GetLabel("UserInsertedFilter", "--inserted--"), ""));

        foreach (var user in Roles.GetUsersInRole(SUPPORT_OPERATOR_ROLE))
        {
            DropUserInsertedFilter.Items.Add(new ListItem(user, user));
        }
        foreach (var user in Roles.GetUsersInRole(SUPPORT_USER_ROLE))
        {
            DropUserInsertedFilter.Items.Add(new ListItem(user, user));
        }
    }

    private void loadDropCategoriesFilter(int sectionId)
    {
        DropCategoriesFilter.Items.Clear();
        DropCategoriesFilter.Items.Add(new ListItem(base.GetLabel("CategoryFilter", "--category--"), "0"));
        DropCategories.Items.Clear();
        //DropCategories.Items.Add(new ListItem("", "0"));  //mandatory category

        var catFilter = new CategoriesFilter();
        var catList = new List<Category>();

        catFilter = new CategoriesFilter();
        catFilter.Enabled = Utility.TristateBool.True;
        catFilter.SectionId = sectionId;
        if (catFilter.SectionId == 0) catFilter.Id = -1;
        catList = new CategoriesManager(true, true).GetByFilter(catFilter, "");
        foreach (var cat in catList)
        {
            DropCategoriesFilter.Items.Add(new ListItem(cat.Title, cat.Id.ToString()));
            DropCategories.Items.Add(new ListItem(cat.Title, cat.Id.ToString()));
        }
    }

    private void loadDropTemplates()
    {
        DropTemplates.Items.Clear();
        DropTemplates.Items.Add(new ListItem(base.GetLabel("MessageTemplate", "--template--"), "0"));

        var filter = new ItemsFilter();
        var list = new List<Item>();

        filter.Enabled = Utility.TristateBool.True;
        filter.SectionId = this.MessageTemplatesSectionId;
        if (filter.SectionId == 0) filter.Id = -1;
        list = new ItemsManager<Item, ItemsFilter>(true, false).GetByFilter(filter, "");
        foreach (var item in list)
        {
            DropTemplates.Items.Add(new ListItem(item.Title, item.Id.ToString()));
        }
        DropTemplates.Enabled = list.Count > 0;

    }

    private void loadDropCustomersFilter()
    {
        DropCustomersFilter.Items.Clear();
        DropCustomersFilter.Items.Add(new ListItem(base.GetLabel("CustomerFilter", "--customer--"), "0"));

        DropChangeCustomer.Items.Clear();
        DropChangeCustomer.Items.Add(new ListItem(base.GetLabel("CustomerFilter", "--customer--"), "0"));

        DropCustomers.Items.Clear();

        var filter = new CustomersFilter();
        if (Roles.IsUserInRole(SUPPORT_USER_ROLE))
            filter.Vat = PgnUserCurrent.Current.Vat;
        else
        DropCustomers.Items.Add(new ListItem(base.GetLabel("CustomerFilter", "--customer--"), "0"));

        var list = new CustomersManager().GetByFilter(filter, "CompanyName");

        foreach (var item in list)
        {
            DropCustomersFilter.Items.Add(
                new ListItem(item.CompanyName, item.Id.ToString()));

            DropChangeCustomer.Items.Add(
                new ListItem(item.CompanyName, item.Id.ToString()));

            DropCustomers.Items.Add(
                new ListItem(item.CompanyName, item.Id.ToString()));
        }
    }

    private void loadDropDatesRangeFilter()
    {
        try
        {
            DropDatesRangeFilter.Items.Clear();
            DropDatesRangeFilter.Items.Add(new ListItem(base.GetLabel("Always", "Always"), "2"));
            DropDatesRangeFilter.Items.Add(new ListItem(base.GetLabel("Today", "Today"), "3"));
            DropDatesRangeFilter.Items.Add(new ListItem(base.GetLabel("Last week", "Last week"), "4"));
            DropDatesRangeFilter.Items.Add(new ListItem(base.GetLabel("Last month", "Last month"), "5"));
        }
        catch (Exception ex)
        {
            LblErr.Text = ex.ToString();
        }
    }

    private void loadLabelsText()
    {
        BtnNew.Text = base.GetLabel("NewTicket", "New ticket");
        BtnReply.Text = base.GetLabel("Reply", "Reply");
        BtnSaveClose.Text = base.GetLabel("SaveAndClose", "Save and close");
        BtnBackToList.Text = base.GetLabel("BackToList", "Back to list");
        ChkMyTickets.Text = base.GetLabel("MyTickets", "My tickets");
        WaterTitleFilter.WatermarkText = base.GetLabel("<subject>", "<subject>");

        //grid
        Grid1.Columns[COL_SUBJECT_INDEX].HeaderText = base.GetLabel("Subject", "Subject");
        Grid1.Columns[COL_STATUS_INDEX].HeaderText = base.GetLabel("Status", "Status");
        Grid1.Columns[COL_PRIORITY_INDEX].HeaderText = base.GetLabel("Priority", "Priority");
        Grid1.Columns[COL_OPERATOR_INDEX].HeaderText = base.GetLabel("Operator", "Operator");
        Grid1.Columns[COL_DATEINSERTED_INDEX].HeaderText = base.GetLabel("DateInserted", "Date inserted");
        Grid1.Columns[COL_DATEINSERTED_INDEX].Visible = false;
        Grid1.Columns[COL_LASTACTIVITY_INDEX].HeaderText = base.GetLabel("LastActivity", "Last activity");
    }

    private void clearForm()
    {
        TxtTitle.Text = "";
        TxtDescription.Text = "";
        //PermissionsControl1.ClearForm();
    }

    private void form2obj(TicketItem obj)
    {
        obj.ThreadId = this.CurrentThreadId;
        obj.Id = this.CurrentId;
        obj.Enabled = true;
        obj.CategoryId = int.Parse(DropCategories.SelectedValue);
        obj.Priority = int.Parse(DropPriority.SelectedValue);
        
        try { obj.CustomerId = int.Parse(DropCustomers.SelectedValue); }
        catch { }

        obj.TitleTranslations.Clear();
        obj.TitleTranslations.Add(Config.CultureDefault, TxtTitle.Text);

        obj.DescriptionTranslations.Clear();
        obj.DescriptionTranslations.Add(Config.CultureDefault, TxtDescription.Text);
    }

    private void obj2form(TicketItem obj)
    {
        Utility.SetDropByValue(DropCategories, obj.ThreadRoot.CategoryId.ToString());
        Utility.SetDropByValue(DropPriority, obj.ThreadRoot.CustomInt2.ToString());
        Utility.SetDropByValue(DropCustomers, obj.ThreadRoot.CustomInt3.ToString());
        Utility.SetDropByValue(DropTemplates, "0");

        TxtTitle.Text = obj.ThreadRoot.Title;

        string description = "";
        obj.DescriptionTranslations.TryGetValue(Config.CultureDefault, out description);
        TxtDescription.Text = description;

        ChkSendEmailToUserInserted.Text = "";
        if (string.IsNullOrEmpty(obj.ThreadRoot.UserInserted))
            ChkSendEmailToUserInserted.Text = PgnUserCurrent.Current.Email;
        else
        {
            var member = Membership.GetUser(obj.ThreadRoot.UserInserted);
            if (member != null)
            {
                if (Utility.IsValidEmail(member.Email))
                    ChkSendEmailToUserInserted.Text = member.Email;
            }
        }
    }

    private void editRow(int threadId, int recordId)
    {
        var obj = new PigeonCms.TicketItem();
        LblOk.Text = "";
        LblErr.Text = "";

        if (!PgnUserCurrent.IsAuthenticated)
            throw new Exception("user not authenticated");

        clearForm();
        this.CurrentThreadId = threadId;
        this.CurrentId = recordId;

        TxtTitle.Enabled = threadId == 0;
        DropCategories.Enabled = threadId == 0;
        DropPriority.Enabled = threadId == 0;
        DropCustomers.Enabled = threadId == 0;
        BtnSaveClose.Visible = threadId > 0;

        //init upload link
        LnkUploadFiles.NavigateUrl = this.FilesUploadUrl
            + "?type=temp&id=" + Utility._SessionID();
        if (this.IsMobileDevice == false)
            LnkUploadFiles.CssClass = "fancy";


        if (CurrentId == 0)
        {
            obj.ThreadId = this.CurrentThreadId;
            obj.ItemDate = DateTime.Now;
            obj.ValidFrom = DateTime.Now;
            obj.ValidTo = DateTime.MinValue;
            obj.Priority = (int)TicketItem.TicketPriorityEnum.Medium;
            //int defaultCategoryId = 0;
            //int.TryParse(DropCategories.SelectedValue, out defaultCategoryId);
            //obj.CategoryId = defaultCategoryId;
            obj2form(obj);
        }
        else
        {
            obj = new ItemsManager<TicketItem, TicketItemFilter>().GetByKey(CurrentId);
            obj2form(obj);
        }
        MultiView1.ActiveViewIndex = VIEW_INSERT;
    }

    private bool checkForm()
    {
        LblErr.Text = "";
        LblOk.Text = "";
        bool res = true;
        return res;
    }

    private bool saveForm(bool saveAndClose)
    {
        bool res = false;
        LblErr.Text = "";
        LblOk.Text = "";
        string extraMessage = "";
        bool sendMessageToUserInserted = false;

        try
        {
            var o1 = new TicketItem();
            var man = new ItemsManager<TicketItem, TicketItemFilter>();
            if (CurrentId == 0)
            {
                form2obj(o1);

                if (o1.IsThreadRoot)
                {
                    string role2Add = "";
                    if (Roles.IsUserInRole(SUPPORT_USER_ROLE))
                        role2Add = PgnUserCurrent.Current.UserName;  //role as username

                    o1.ReadAccessType = MenuAccesstype.Registered;
                    o1.ReadRolenames.Add(SUPPORT_OPERATOR_ROLE);
                    o1.ReadRolenames.Add(role2Add);
                    //o1.ReadRolenames.Add(SUPPORT_MANAGER_ROLE);
                }

                o1 = man.Insert(o1);
                if (!o1.IsThreadRoot)
                {
                    var root = new TicketItem();
                    root = man.GetByKey(o1.ThreadId);
                    man.Update(root);   //update dateUpdated
                    extraMessage = "new answer inserted";
                }
                else
                    extraMessage = "new ticket inserted";

                //copy temp attachments in item folder
                new FilesGallery("~/Public/Files/items/", o1.Id.ToString()).MoveTempFiles();  

                loadGrid();
            }
            else
            {
                o1 = man.GetByKey(CurrentId);
                form2obj(o1);
                man.Update(o1);
                extraMessage = "ticket updated";
            }

            if (saveAndClose)
            {
                changeThreadStatus(o1.ThreadId, "close");
                extraMessage = "ticket closed";
            }

            sendMessage(o1.UserAssigned, o1, "MessageTicketTitle", "MessageTicketDescription", extraMessage);
            sendMessage(filterSystemMessagesManager(o1.CategoryId), o1, "MessageTicketTitle", "MessageTicketDescription", extraMessage);

            if (ChkSendEmailToUserInserted.Visible == false)
                sendMessageToUserInserted = true;
            else
                sendMessageToUserInserted = ChkSendEmailToUserInserted.Checked;

            if (sendMessageToUserInserted && !o1.UserInserted.Equals(o1.UserAssigned))
                sendMessage(o1.UserInserted, o1, "MessageTicketTitle", "MessageTicketDescription", extraMessage);

            loadThread(o1.ThreadId);
            LblOk.Text = Utility.GetLabel("RECORD_SAVED_MSG");
            res = true;
        }
        catch (CustomException e1)
        {
            LblErr.Text = e1.CustomMessage;
        }
        catch (Exception e1)
        {
            LblErr.Text = Utility.GetLabel("RECORD_ERR_MSG") + "<br />" + e1.ToString();
        }
        finally
        {
        }
        return res;
    }

    private void setTemplate(int templateId)
    {
        LblOk.Text = "";
        LblErr.Text = "";

        try
        {
            var o1 = new Item();
            var man = new ItemsManager<Item, ItemsFilter>(true, false);
            o1 = man.GetByKey(templateId);  //precarico i campi esistenti e nn gestiti dal form
            if (o1.Id > 0)
                TxtDescription.Text = o1.Description;
            else
                TxtDescription.Text = "";
        }
        catch (Exception e)
        {
            LblErr.Text = e.Message;
        }
    }

    /// <summary>
    /// filters systemMessagesTo, keep users with access to ticketCategoryId category
    /// </summary>
    /// <param name="systemMessagesTo"></param>
    /// <param name="ticketCategoryId"></param>
    /// <returns></returns>
    private List<string> filterSystemMessagesManager(int ticketCategoryId)
    {
        var res = new List<string>();
        var toList = Utility.String2List(this.BaseModule.SystemMessagesTo, ";");
        var cat = new CategoriesManager().GetByKey(ticketCategoryId);
        var catRoles = new PermissionProvider().GetPermissionRoles(cat.ReadPermissionId);
        foreach (var user in toList)
        {
            bool foundRole = false;
            foreach (var catRole in catRoles)
            {
                foundRole = Roles.IsUserInRole(user, catRole);
                if (foundRole) break;
            }

            if (foundRole || Roles.IsUserInRole(user, "admin"))
                res.Add(user.Trim());
        }
        return res;
    }

    private void changeCustomer(int threadId, int customerId)
    {
        LblOk.Text = "";
        LblErr.Text = "";

        try
        {
            if (!PgnUserCurrent.IsAuthenticated)
                throw new Exception("user not authenticated");

            var o1 = new TicketItem();
            var man = new ItemsManager<TicketItem, TicketItemFilter>(true, true);
            o1 = man.GetByKey(threadId);  //precarico i campi esistenti e nn gestiti dal form
            o1.CustomerId = customerId;
            man.Update(o1);

            loadThread(threadId);
        }
        catch (Exception e)
        {
            LblErr.Text = e.Message;
        }
    }

    private void changeAssignedUser(int threadId, string userAssigned)
    {
        LblOk.Text = "";
        LblErr.Text = "";

        try
        {
            if (!PgnUserCurrent.IsAuthenticated)
                throw new Exception("user not authenticated");

            string previousUser = "";
            var o1 = new TicketItem();
            var man = new ItemsManager<TicketItem, TicketItemFilter>(true, true);
            o1 = man.GetByKey(threadId);  //precarico i campi esistenti e nn gestiti dal form
            previousUser = o1.UserAssigned;
            o1.UserAssigned = userAssigned;
            man.Update(o1);

            sendMessage(previousUser, o1, "MessageTicketTitle", "MessageTicketDescription", "user assigned changed from " + previousUser + " to " + userAssigned);
            sendMessage(o1.UserAssigned, o1, "MessageTicketTitle", "MessageTicketDescription", "user assigned changed from " + previousUser + " to " + userAssigned);
            sendMessage(filterSystemMessagesManager(o1.CategoryId), o1, "MessageTicketTitle", "MessageTicketDescription", "user assigned changed from " + previousUser + " to " + userAssigned);

            loadThread(threadId);
        }
        catch (Exception e)
        {
            LblErr.Text = e.Message;
        }
    }

    private void changeThreadStatus(int threadId, string statusCommand)
    {
        LblOk.Text = "";
        LblErr.Text = "";
        bool sendMessageToUserInserted = false;
        try
        {
            if (!PgnUserCurrent.IsAuthenticated)
                throw new Exception("user not authenticated");

            string previousStatus = "";
            string nextStatus = "";

            var o1 = new TicketItem();
            var man = new ItemsManager<TicketItem, TicketItemFilter>();
            o1 = man.GetByKey(threadId);  //precarico i campi esistenti e nn gestiti dal form

            try { previousStatus = Enum.GetName(typeof(TicketItem.TicketStatusEnum), o1.Status); }
            catch { }

            switch (statusCommand)
            {
                case "close":
                    o1.Status =  (int)TicketItem.TicketStatusEnum.Closed;
                    sendMessageToUserInserted = true;
                    break;
                case "open":
                    o1.Status =  (int)TicketItem.TicketStatusEnum.Open;
                    break;
                case "working":
                    o1.Status = (int)TicketItem.TicketStatusEnum.WorkInProgress;
                    sendMessageToUserInserted = true;
                    break;
                case "lock":
                    o1.Status =  (int)TicketItem.TicketStatusEnum.Locked;
                    break;
                default:
                    break;
            }
            man.Update(o1);

            try { nextStatus = Enum.GetName(typeof(TicketItem.TicketStatusEnum), o1.Status); }
            catch { }

            previousStatus = base.GetLabel(previousStatus, previousStatus);
            nextStatus = base.GetLabel(nextStatus, nextStatus);

            sendMessage(o1.UserAssigned, o1, "MessageTicketTitle", "MessageTicketDescription", "status changed from "+ previousStatus +" to "+ nextStatus);
            sendMessage(filterSystemMessagesManager(o1.CategoryId), o1, "MessageTicketTitle", "MessageTicketDescription", "status changed from " + previousStatus + " to " + nextStatus);
            if (sendMessageToUserInserted && !o1.UserInserted.Equals(o1.UserAssigned))
                sendMessage(o1.UserInserted, o1, "MessageTicketTitle", "MessageTicketDescription", "status changed from " + previousStatus + " to " + nextStatus);

            loadThread(threadId);
        }
        catch (Exception e)
        {
            LblErr.Text = e.Message;
        }
    }

    private void deleteRow(int recordId)
    {
        LblOk.Text = "";
        LblErr.Text = "";

        try
        {
            if (!PgnUserCurrent.IsAuthenticated)
                throw new Exception("user not authenticated");

            var man = new ItemsManager<TicketItem, TicketItemFilter>(true, true);
            var obj = man.GetByKey(recordId);
            sendMessage(obj.UserAssigned, obj, "MessageTicketTitle", "MessageTicketDescription", "ticket deleted");
            sendMessage(filterSystemMessagesManager(obj.CategoryId), obj, "MessageTicketTitle", "MessageTicketDescription", "ticket deleted");

            man.DeleteById(recordId);
            loadGrid();
        }
        catch (Exception e)
        {
            LblErr.Text = e.Message;
        }
    }

    private void setFlag(int recordId, bool value, string flagName)
    {
        try
        {
            var man = new ItemsManager<TicketItem, TicketItemFilter>(true, true);
            var o1 = man.GetByKey(recordId);
            switch (flagName.ToLower())
            {
                case "flagged":
                    o1.Flagged = value;
                    sendMessage(o1.UserAssigned, o1, "MessageTicketTitle", "MessageTicketDescription", "flag changed");
                    sendMessage(filterSystemMessagesManager(o1.CategoryId), o1, "MessageTicketTitle", "MessageTicketDescription", "flag changed");
                    break;
                default:
                    break;
            }
            man.Update(o1);
        }
        catch (Exception e1)
        {
            LblErr.Text = Utility.GetLabel("RECORD_ERR_MSG") + "<br />" + e1.ToString();
        }
        finally { }
    }

    private void sendMessage(string toUsers, TicketItem ticket, string titleLabel, string descriptionLabel, string extra)
    {
        if (string.IsNullOrEmpty(toUsers))
            return;
        sendMessage(Utility.String2List(toUsers, ";"), ticket, titleLabel, descriptionLabel, extra);
    }

    private void sendMessage(List<string> toUsers, TicketItem ticket, string titleLabel, string descriptionLabel, string extra)
    {
        if (toUsers.Count == 0)
            return;

        var msg = new Message();
        msg.Title = fillMessagePlaceholders(ticket, base.GetLabel(titleLabel), extra);
        msg.Description = fillMessagePlaceholders(ticket, base.GetLabel(descriptionLabel), extra);
        msg.ItemId = ticket.Id;
        MessageProvider.SendMessage(toUsers, msg,
            MessageProvider.SendMessageEnum.UserSetting,
            MessageProvider.SendMessageEnum.UserSetting);
    }

    private string fillMessagePlaceholders(TicketItem ticket, string textToParse, string extra)
    {
        textToParse = textToParse.Replace("[[ItemId]]", ticket.ThreadId.ToString())
            .Replace("[[ItemTitle]]", ticket.Title)
            .Replace("[[ItemDescription]]", ticket.Description)
            .Replace("[[ItemUserUpdated]]", ticket.UserUpdated)
            .Replace("[[ItemDateUpdated]]", ticket.DateUpdated.ToLongDateString())
            .Replace("[[Extra]]", extra);
        return textToParse;
    }

    [PigeonCms.UserControlScriptMethod]
    public static void ReloadThread()
    {
        
    }

    #endregion
}
