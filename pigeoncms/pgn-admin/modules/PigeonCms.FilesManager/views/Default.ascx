<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Default.ascx.cs" Inherits="Controls_FilesManager" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Import Namespace="PigeonCms" %>
<%@ Register src="~/Controls/FileUpload.ascx" tagname="FileUpload" tagprefix="uc1" %>

<script type="text/javascript">

/*    function pageLoad(sender, args) {
            var filter = $('fieldset.adminFilters');
            var filterhtml = $(this).html();
            if ($.trim(filterhtml) == '')
                filter.hide();
    }*/

    var deleteQuestion = '<%=PigeonCms.Utility.GetLabel("RECORD_DELETE_QUESTION") %>';
</script>

<asp:ScriptManager runat="server" EnablePageMethods="false"></asp:ScriptManager>
<asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="1" AssociatedUpdatePanelID="Upd1">
    <ProgressTemplate>
        <div class="loading"><%=PigeonCms.Utility.GetLabel("LblLoading", "loading") %></div>
    </ProgressTemplate>
</asp:UpdateProgress>

<asp:UpdatePanel ID="Upd1" runat="server">
<Triggers>
    <asp:PostBackTrigger ControlID="FileUpload1" />
</Triggers>
<ContentTemplate>

    <span class="error"><asp:Literal ID="LblErr" runat="server" Text=""></asp:Literal></span>
    <span class="success"><asp:Literal ID="LblOk" runat="server" Text=""></asp:Literal></span>

    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
    <asp:View ID="ViewSee" runat="server">
        <div class="pigeoncms-filesmanager-litContentBefore">
            <asp:Literal runat="server" ID="LitContentBefore"></asp:Literal>
        </div>
        <fieldset class="adminForm">
            <%--<legend><%=base.GetLabel("LblUploadFiles", "Files upload") %></legend>--%>
            <uc1:FileUpload ID="FileUpload1" runat="server" OnAfterUpload="FileUpload1_AfterUpload" />
        </fieldset>
        <br />
        
        <div class="adminToolbar">
            <asp:TextBox ID="TxtNewFolder" runat="server" CssClass="adminSmallText"></asp:TextBox>
            <cc1:TextBoxWatermarkExtender TargetControlID="TxtNewFolder" runat="server" 
                WatermarkText="<folder>" WatermarkCssClass="adminSmallText watermark">
                </cc1:TextBoxWatermarkExtender>
            <asp:Button ID="BtnNewFolder" runat="server" Text="Create folder" 
                CssClass="button" onclick="BtnNewFolder_Click" />
            <asp:Button ID="BtnParentFolder" runat="server" Text="Parent folder" 
                CssClass="button" onclick="BtnParentFolder_Click" />        
        </div>
        <fieldset class="adminFilters"></fieldset>
        <br />

        <asp:GridView ID="Grid1" runat="server" AutoGenerateColumns="False" Width="100%"
            DataKeyNames="FileName,IsFolder" OnRowCommand="Grid1_RowCommand" OnRowCreated="Grid1_RowCreated" OnRowDataBound="Grid1_RowDataBound">
            <Columns>
                <%--0--%>
                <asp:TemplateField HeaderText=""  ItemStyle-Width="100">
                    <ItemTemplate>
                    <div class="fileItem">
                        <asp:Image ID="ImgPreview" runat="server" SkinID="ImgPreviewStyle" />
                        <asp:HyperLink ID="LnkFileName" runat="server"></asp:HyperLink>
                        <asp:LinkButton ID="BtnNavigate" runat="server" 
                            CommandName="NavigateFolder" CommandArgument='<%#Eval("FileName") %>'>
                        </asp:LinkButton>
                    </div>
                    </ItemTemplate>
                </asp:TemplateField>

                <%--1--%>
                <asp:BoundField DataField="HumanLength" HeaderText="Size" ItemStyle-Width="80"  />

                <%--2--%>
                <asp:TemplateField HeaderText="Meta data">
                    <ItemTemplate>
                    <asp:ImageButton ID="LnkSel" CommandName="Select" CommandArgument='<%#Eval("FileUrl") %>' 
                    runat="server" SkinID="ImgEditFile" />
                    <em><%#Eval("Title") %></em><br />
                    <%#Eval("Description") %>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <%--3--%>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10">
                    <ItemTemplate>
                    <asp:ImageButton ID="LnkDel" CommandName="DeleteRow" CommandArgument='<%#Eval("FileName")+ "," +Eval("IsFolder") %>' 
                    runat="server" SkinID="ImgDelFile" 
                    OnClientClick="return confirm(deleteQuestion);"  />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    
        <div class="pigeoncms-filesmanager-litContentAfter">
            <asp:Literal runat="server" ID="LitContentAfter"></asp:Literal>
        </div>
    </asp:View>
    
    <asp:View ID="ViewInsert" runat="server">
		
        <div class="adminToolbar">
            <asp:Button ID="BtnSave" runat="server" Text="<%$ Resources:PublicLabels, CmdSave %>" CssClass="button" OnClientClick="MyObject.UpdateEditorFormValue();" OnClick="BtnSave_Click" />
            <asp:Button ID="BtnCancel" runat="server" Text="<%$ Resources:PublicLabels, CmdCancel %>" CssClass="button" OnClick="BtnCancel_Click" />
        </div>  
          
        <div class="col width-100">
            <fieldset class="adminForm">
                <legend></legend>
                <table class="adminTable">
                <tr>
                    <td class="key"><%=base.GetLabel("", "File", TxtFileName)%></td>
                    <td>
                        <asp:TextBox ID="TxtFileName" runat="server" CssClass="adminMediumText"></asp:TextBox>
                    </td>
                </tr>                
                <tr>
                    <td class="key"><%=base.GetLabel("LblTitle", "Title") %></td>
                    <td>
                        <asp:Panel runat="server" ID="PanelTitle"></asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td class="key"><%=base.GetLabel("LblDescription", "Description") %></td>
                    <td>
                        <asp:Panel runat="server" ID="PanelDescription"></asp:Panel>
                    </td>
                </tr>
                </table>
            </fieldset>
        </div>
    </asp:View>
    </asp:MultiView>

</ContentTemplate>
</asp:UpdatePanel>
