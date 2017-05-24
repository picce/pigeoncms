<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TicketsList.ascx.cs" Inherits="Controls_TicketsList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register src="~/Controls/ContentEditorControl.ascx" tagname="ContentEditorControl" tagprefix="uc1" %>
<%@ Register src="~/Controls/FileUpload.ascx" tagname="FileUpload" tagprefix="uc1" %>


<script type="text/javascript">

    function pageLoad(sender, args) {
        $(document).ready(function () {
            $("a.fancy").fancybox({
                'width': '80%',
                'height': '80%',
                'type': 'iframe',
                'hideOnContentClick': false,
                onClosed: function () {}
            });

            $('td.key').each(function () {
                var hide = true;
                var html = $(this).html();
                if ($.trim(html) != '')
                    hide = false;
                if (hide)
                    $(this).parent('tr').hide();
            });
        });
    }

var deleteQuestion = '<%=PigeonCms.Utility.GetLabel("RECORD_DELETE_QUESTION") %>';
</script>

<asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
<asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="1" AssociatedUpdatePanelID="Upd1">
    <ProgressTemplate>
        <div class="loading"><%=PigeonCms.Utility.GetLabel("LblLoading", "loading") %></div>
    </ProgressTemplate>
</asp:UpdateProgress>

<div class='moduleBody <%=base.BaseModule.CssClass %>'>
<%=HeaderText%>

<asp:UpdatePanel ID="Upd1" runat="server">
<ContentTemplate>
    <asp:Label ID="LblErr" runat="server" Text="" CssClass="error"></asp:Label>
    <asp:Label ID="LblOk" runat="server" Text="" CssClass="success"></asp:Label>

    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0" OnActiveViewChanged="MultiView1_ActiveViewChanged">

    <asp:View ID="ViewList" runat="server">
        <div class="adminToolbar">
            <asp:Button ID="BtnNew" runat="server" Text="New" CssClass="button" CausesValidation="false" OnClick="BtnNew_Click" />
        </div>

        <fieldset class="adminFilters">
            <asp:DropDownList ID="DropStatusFilter" runat="server" AutoPostBack="true" CssClass="" 
                OnSelectedIndexChanged="Filter_Changed"></asp:DropDownList>
            <asp:DropDownList ID="DropPriorityFilter" runat="server" AutoPostBack="true" CssClass="" 
                OnSelectedIndexChanged="Filter_Changed"></asp:DropDownList>
            <asp:DropDownList ID="DropCategoriesFilter" runat="server" AutoPostBack="true" CssClass="" 
                OnSelectedIndexChanged="Filter_Changed"></asp:DropDownList>
            <asp:DropDownList ID="DropCustomersFilter" runat="server" AutoPostBack="true" CssClass="adminMediumText" 
                OnSelectedIndexChanged="Filter_Changed"></asp:DropDownList>
            <asp:DropDownList ID="DropAssignedUserFilter" runat="server" AutoPostBack="true" CssClass="adminMediumText" 
                OnSelectedIndexChanged="Filter_Changed"></asp:DropDownList>
            <asp:DropDownList ID="DropUserInsertedFilter" runat="server" AutoPostBack="true" CssClass="adminMediumText" 
                OnSelectedIndexChanged="Filter_Changed"></asp:DropDownList>

            <asp:DropDownList runat="server" ID="DropDatesRangeFilter" AutoPostBack="true" CssClass="adminShortText" 
                OnSelectedIndexChanged="Filter_Changed"></asp:DropDownList>
            <asp:TextBox ID="TxtTitleFilter" runat="server" AutoPostBack="true" CssClass="adminMediumText" 
                ontextchanged="Filter_Changed"></asp:TextBox>
            <cc1:TextBoxWatermarkExtender ID="WaterTitleFilter" TargetControlID="TxtTitleFilter" runat="server" 
                WatermarkText="<subject>" WatermarkCssClass="adminMediumText watermark"></cc1:TextBoxWatermarkExtender>
            <asp:CheckBox ID="ChkMyTickets" Text="only mine" AutoPostBack="true" OnCheckedChanged="ChkMyTickets_Changed" runat="server" />
        </fieldset>
        <br />

        <asp:GridView ID="Grid1" runat="server" AllowPaging="True" AllowSorting="true" Width="100%" AutoGenerateColumns="False"
            DataKeyNames="Id" OnRowCommand="Grid1_RowCommand" OnRowCreated="Grid1_RowCreated" OnRowDataBound="Grid1_RowDataBound"
            OnPageIndexChanging="Grid1_PageIndexChanging" OnSorting="Grid1_Sorting">
            <Columns>
                <%--0--%>
                <asp:TemplateField HeaderText="Subject" SortExpression="Title" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                    <asp:LinkButton ID="LnkTitle" runat="server" CommandName="Select" CommandArgument='<%#Eval("Id") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <%--1--%>
                <asp:TemplateField HeaderText="<%$ Resources:PublicLabels, LblCategory %>" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Literal ID="LitCategoryTitle" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                
                <%--2--%>
                <asp:TemplateField HeaderText="Customer" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Literal ID="LitCustomer" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>

                <%--3--%>
                <asp:TemplateField HeaderText="Status" SortExpression="Status">
                    <ItemTemplate>
                        <asp:Literal ID="LitStatus" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>

                <%--4--%>
                <asp:TemplateField HeaderText="Priority" SortExpression="Priority">
                    <ItemTemplate>
                        <asp:Literal ID="LitPriority" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>

                <%--5--%>
                <asp:TemplateField HeaderText="Flag" SortExpression="Flagged" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:ImageButton runat="server" ID="ImgFlaggedOk" CommandName="ImgFlaggedOk" CommandArgument='<%#Eval("Id") %>' SkinID="ImgStarOn" Visible="false" />
                        <asp:ImageButton runat="server" ID="ImgFlaggedKo" CommandName="ImgFlaggedKo" CommandArgument='<%#Eval("Id") %>' SkinID="ImgStarOff" Visible="false" />
                    </ItemTemplate>
                </asp:TemplateField>

                <%--6--%>
                <asp:TemplateField HeaderText="Support operator">
                    <ItemTemplate>
                        <asp:Literal ID="LitUserAssigned" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>

                <%--7--%>
                <asp:TemplateField HeaderText="Inserted" SortExpression="DateInserted">
                    <ItemTemplate>
                        <asp:Literal ID="LitInserted" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>

                <%--8--%>
                <asp:TemplateField HeaderText="Last activity" SortExpression="DateUpdated">
                    <ItemTemplate>
                        <asp:Literal ID="LitUpdated" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>

                <%--9--%>
                <asp:TemplateField HeaderText="" Visible="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10">
                    <ItemTemplate>
                    <asp:ImageButton ID="LnkDel" CommandName="DeleteRow" CommandArgument='<%#Eval("Id") %>' 
                    runat="server" SkinID="ImgDelFile" 
                    OnClientClick="return confirm(deleteQuestion);"  />
                    </ItemTemplate>
                </asp:TemplateField>
                    
                <%--10--%>
                <asp:BoundField DataField="Id" HeaderText="ID" SortExpression="Id" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" />
            </Columns>
        </asp:GridView>
    </asp:View>

    <asp:View ID="ViewThread" runat="server">
        <div class="adminToolbar">
            <asp:Button ID="BtnReply" runat="server" Text="Reply" CssClass="button" Visible="false" OnClick="BtnReply_Click" />
            <asp:Button ID="BtnBackToList" runat="server" Text="Back" CssClass="button floatLeft" OnClick="BtnBackToList_Click" />
            <asp:DropDownList ID="DropThreadActions" runat="server" AutoPostBack="true" CssClass="" 
                OnSelectedIndexChanged="DropThreadActions_Changed"></asp:DropDownList>
            <asp:DropDownList ID="DropAssignedUser" runat="server" AutoPostBack="true" Visible="false" CssClass="" 
                OnSelectedIndexChanged="DropAssignedUser_Changed"></asp:DropDownList>
            <asp:DropDownList ID="DropChangeCustomer" runat="server" AutoPostBack="true" Visible="false" CssClass="" 
                OnSelectedIndexChanged="DropChangeCustomer_Changed"></asp:DropDownList>
            <asp:DropDownList ID="DropThreadOrder" runat="server" AutoPostBack="true" CssClass="" 
                OnSelectedIndexChanged="DropThreadOrder_Changed"></asp:DropDownList>
        </div>
        <%=ListString %>
    </asp:View>

    <asp:View ID="ViewInsert" runat="server">
        
        <div class="adminToolbar">
            <asp:Button ID="BtnSaveClose" runat="server" Text="Save and close" CssClass="button" CausesValidation="true" OnClick="BtnSaveClose_Click" />
            <asp:Button ID="BtnSave" runat="server" Text="<%$ Resources:PublicLabels, CmdSave %>" CssClass="button" CausesValidation="true" OnClick="BtnSave_Click" />
            <asp:Button ID="BtnCancel" runat="server" Text="<%$ Resources:PublicLabels, CmdCancel %>" CssClass="button" CausesValidation="false" OnClick="BtnCancel_Click" />
        </div>
            
        <div class="col width-100">
            <fieldset class="adminForm">
                <legend></legend>
                <table class="adminTable">
                <tr>
                    <td class="key"><%=base.GetLabel("LblCategory", "Category", DropCategories, true)%></td>
                    <td>
                        <asp:DropDownList ID="DropCategories" runat="server" CssClass="adminText"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="key"><%=base.GetLabel("Priority", "Priority", DropPriority, true)%></td>
                    <td>
                        <asp:DropDownList ID="DropPriority" runat="server" CssClass="adminText"></asp:DropDownList>                    
                    </td>
                </tr>
                <tr>
                    <td class="key"><%=base.GetLabel("Customer", "Customer", DropCustomers, true)%></td>
                    <td>
                        <asp:DropDownList ID="DropCustomers" runat="server" CssClass="adminText"></asp:DropDownList>                    
                    </td>
                </tr>
                <tr>
                    <td class="key"><%=base.GetLabel("Subject", "Subject", TxtTitle, true)%></td>
                    <td>
                        <asp:TextBox ID="TxtTitle" runat="server" CssClass="adminText"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="ReqTitle" ControlToValidate="TxtTitle" runat="server" Text="*"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="key"><%=base.GetLabel("Template", "Template", DropTemplates, true)%></td>
                    <td>
                        <asp:DropDownList ID="DropTemplates" runat="server" CssClass="adminText"
                        AutoPostBack="true" OnSelectedIndexChanged="DropTemplates_Changed"></asp:DropDownList>                    
                    </td>
                </tr>
                <tr>
                    <td class="key"><%=base.GetLabel("Text", "Text", TxtDescription, true)%></td>
                    <td>
                        <uc1:ContentEditorControl ID="TxtDescription" runat="server"
                        ReadMoreButton="false" PageBreakButton="false" FileButton="false" />
                    </td>
                </tr>
                <tr>
                    <td class="key"><%=base.GetLabel("Attachment", "Attachment", null, true)%></td>
                    <td>
                        <asp:HyperLink runat="server" ID="LnkUploadFiles">
                        <%=base.GetLabel("AttachFiles", "Attach files", null, true)%>
                        </asp:HyperLink>
                    </td>
                </tr>
                <tr>
                    <td class="key"><%=base.GetLabel("SendEmail", "Send email", ChkSendEmailToUserInserted, true)%></td>
                    <td>
                        <asp:CheckBox ID="ChkSendEmailToUserInserted" runat="server" Checked="true" />
                    </td>
                </tr>
                </table>
            </fieldset>
        </div>
            
        <div class="clear"/>
    </asp:View>

    </asp:MultiView>

</ContentTemplate>
</asp:UpdatePanel>

<%=FooterText%>
</div>
