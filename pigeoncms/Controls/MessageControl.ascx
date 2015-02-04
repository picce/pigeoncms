<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MessageControl.ascx.cs" Inherits="Controls_MessageControl" %>
<%@ Register src="~/Controls/ContentEditorControl.ascx" tagname="ContentEditorControl" tagprefix="uc1" %>

<script type="text/javascript">
    // <!CDATA[

    var deleteQuestion = '<%=PigeonCms.Utility.GetLabel("RECORD_DELETE_QUESTION") %>';

    // ]]>
</script>

<asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0" OnActiveViewChanged="MultiView1_ActiveViewChanged">

<asp:View ID="ViewList" runat="server">

<asp:GridView ID="Grid1" runat="server" AllowPaging="True" AllowSorting="true" Width="100%" AutoGenerateColumns="False"
    DataSourceID="ObjDs1" DataKeyNames="Id" OnRowCommand="Grid1_RowCommand" OnRowCreated="Grid1_RowCreated" 
    OnRowDataBound="Grid1_RowDataBound" OnPreRender="Grid1_PreRender">
    <Columns>

        <asp:TemplateField HeaderText="Starred" SortExpression="IsStarred" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
            <ItemTemplate>
                <asp:ImageButton runat="server" ID="ImgStarredOk" CommandName="ImgStarredOk" CommandArgument='<%#Eval("Id") %>' SkinID="ImgStarOn" Visible="false" />
                <asp:ImageButton runat="server" ID="ImgStarredKo" CommandName="ImgStarredKo" CommandArgument='<%#Eval("Id") %>' SkinID="ImgStarOff" Visible="false" />
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Message" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
            <ItemTemplate>
                <asp:LinkButton ID="LnkSubject" runat="server" CausesValidation="false" CommandName="Select" CommandArgument='<%#Eval("Id") %>'></asp:LinkButton><br />
                <asp:Literal ID="LitBody" runat="server" />
            </ItemTemplate>
        </asp:TemplateField>

        <asp:BoundField DataField="FromUser" HeaderText="From" SortExpression="FromUser" />
        <asp:BoundField DataField="ToUser" HeaderText="To" SortExpression="ToUser" />

        <asp:TemplateField HeaderText="Date" SortExpression="DateInserted">
            <ItemTemplate>
                <asp:Literal ID="LitDateInserted" runat="server" />
            </ItemTemplate>
        </asp:TemplateField>
                    
        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10">
            <ItemTemplate>
            <asp:ImageButton ID="LnkDel" CommandName="DeleteRow" CommandArgument='<%#Eval("Id") %>' 
            runat="server" SkinID="ImgDelFile" 
            OnClientClick="return confirm(deleteQuestion);"  />
            </ItemTemplate>
        </asp:TemplateField>
                    
        <asp:BoundField DataField="Id" HeaderText="ID" SortExpression="Id" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" />
    </Columns>
</asp:GridView>

<asp:ObjectDataSource ID="ObjDs1" runat="server" 
    SortParameterName="sort" SelectMethod="GetByFilter" 
    TypeName="PigeonCms.MessagesManager" 
    OnSelecting="ObjDs1_Selecting">
    <SelectParameters>
        <asp:Parameter Name="filter" Type="Object" />
        <asp:Parameter Name="sort" Type="String" DefaultValue="" />
    </SelectParameters>
    <DeleteParameters>
        <asp:Parameter Name="Id" Type="Int32" />
    </DeleteParameters>
</asp:ObjectDataSource>
</asp:View>

<asp:View ID="ViewShowMessage" runat="server">
    <div class="col width-100">
        <div class="itemTitle">
            <asp:Literal ID="LitSubject" runat="server"></asp:Literal>                
        </div>
        <div class="itemDate">
            <asp:Literal ID="LitCreated" runat="server"></asp:Literal>                
        </div>
        <div class="itemUser">
            <asp:Literal ID="LitUser" runat="server"></asp:Literal>                
        </div>
        <div class="itemDescription">
            <asp:Literal ID="LitDescription" runat="server"></asp:Literal>                
        </div>
        <div class="itemImages">
            <asp:Literal ID="LitImages" runat="server"></asp:Literal>                
        </div>
        <div class="itemFiles">
            <asp:Literal ID="LitFiles" runat="server"></asp:Literal>                
        </div>
    </div>
</asp:View>

<asp:View ID="ViewInsertMessage" runat="server">
    <div class="col width-100">
        <fieldset class="adminForm">
            <legend></legend>
            <table class="adminTable">
            <tr>
                <td class="key"><%=base.GetLabel("To", "To", TxtTo, true)%></td>
                <td>
                    <asp:TextBox ID="TxtTo" runat="server" CssClass="adminText"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ReqTo" ControlToValidate="TxtTo" runat="server" Text="*"></asp:RequiredFieldValidator>
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
                <td class="key"><%=base.GetLabel("Body", "Body", TxtDescription, true)%></td>
                <td>
                    <uc1:ContentEditorControl ID="TxtDescription" runat="server"
                    ReadMoreButton="false" PageBreakButton="false" FileButton="false" />
                </td>
            </tr>
            </table>
        </fieldset>
    </div>
            
    <div class="clear"/>
</asp:View>

</asp:MultiView>