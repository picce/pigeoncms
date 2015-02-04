<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Default.ascx.cs" Inherits="Controls_StaticPagesAdmin" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register src="~/Controls/ContentEditorControl.ascx" tagname="ContentEditorControl" tagprefix="uc1" %>
<asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>


<script type="text/javascript">
// <!CDATA[

function ismaxlength(obj){
    var mlength=obj.getAttribute ? parseInt(obj.getAttribute("maxlength")) : "";
    if (obj.getAttribute && obj.value.length > mlength)
        obj.value=obj.value.substring(0, mlength);
}

//use in popup version
function closePopup() { parent.$.fancybox.close(); }

var deleteQuestion = '<%=PigeonCms.Utility.GetLabel("RECORD_DELETE_QUESTION") %>';

// ]]>
</script>

<asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="1" AssociatedUpdatePanelID="Upd1">
    <ProgressTemplate>
        <div class="loading"><%=PigeonCms.Utility.GetLabel("LblLoading", "loading") %></div>
    </ProgressTemplate>
</asp:UpdateProgress>
    
<asp:UpdatePanel ID="Upd1" runat="server">
<ContentTemplate>

    <div class="row">
        <div class="col-lg-12">
            <asp:Label ID="LblErr" runat="server" Text=""></asp:Label>
            <asp:Label ID="LblOk" runat="server" Text=""></asp:Label>
        </div>
    </div>

    <div class="row">
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0" 
        onactiveviewchanged="MultiView1_ActiveViewChanged">
    
        <asp:View ID="ViewSee" runat="server">

            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-body"> 
                        <div class="pull-right">
                            <div class="btn-group adminToolbar">
                                <asp:Button ID="BtnNew" runat="server" Text="<%$ Resources:PublicLabels, CmdNew %>" 
                                    CssClass="btn btn-primary btn-xs" OnClick="BtnNew_Click" />
                            </div>
                        </div> 
                    </div>
                </div>
            </div>

            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="table-responsive">

                        <asp:GridView ID="Grid1" Width="100%" runat="server" AllowPaging="True" AllowSorting="false" AutoGenerateColumns="False"
                            DataSourceID="ObjDs1" DataKeyNames="PageName" 
                            OnRowDataBound="Grid1_RowDataBound" OnRowCommand="Grid1_RowCommand" OnRowCreated="Grid1_RowCreated">
                            <Columns>

                                <asp:TemplateField HeaderText="<%$ Resources:PublicLabels, LblTitle %>" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LnkSel" runat="server" CausesValidation="false" CommandName="Select" CommandArgument='<%#Eval("pageName") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="PageTitle" HeaderText="Titolo" SortExpression="PageTitle" />

                                <asp:TemplateField HeaderText="Visibile" SortExpression="Visible">
                                    <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" Enabled="false" Checked='<%#Eval("Visible") %>' runat="server" />                
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LnkDel" runat="server" CommandName="DeleteRow" 
                                            CommandArgument='<%#Eval("pageName") %>' OnClientClick="return confirm(deleteQuestion);">
                                            <i class='fa fa-pgn_delete fa-fw'></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>

                    </div>
                </div>
            </div>
            
            <asp:ObjectDataSource ID="ObjDs1" runat="server" SortParameterName="sort"
                SelectMethod="GetByFilter" TypeName="PigeonCms.StaticPagesManager" OnSelecting="ObjDs1_Selecting">
                <SelectParameters>
                    <asp:Parameter Name="filter" Type="Object" />
                    <asp:Parameter Name="sort" Type="String" DefaultValue="PageName" />
                </SelectParameters>
                <DeleteParameters>
                    <asp:Parameter Name="pageName" Type="String" />
                </DeleteParameters>
            </asp:ObjectDataSource>
        </asp:View>
   

        <asp:View ID="ViewInsert" runat="server">
        
            <div class="panel panel-default">

                <div class="panel-heading">
                    <%=base.GetLabel("LblDetails", "Details") %>
                    <div class="pull-right">
                        <div class="btn-group">
                            <asp:Button ID="BtnSave" runat="server" Text="<%$ Resources:PublicLabels, CmdSave %>" CssClass="btn btn-primary btn-xs" OnClick="BtnSave_Click" />
                            <asp:Button ID="BtnApply" runat="server" Text="<%$ Resources:PublicLabels, CmdApply %>" CssClass="btn btn-default btn-xs" OnClick="BtnApply_Click" />
                            <asp:Button ID="BtnCancel" runat="server" Text="<%$ Resources:PublicLabels, CmdCancel %>" CssClass="btn btn-default btn-xs" CausesValidation="false" OnClick="BtnCancel_Click" />
                        </div>
                    </div>
                </div>

                <div class="panel-body">

                    <div class="form-group col-md-4">
                        <%=base.GetLabel("LblName", "Name", TxtPageName, true)%>
                        <asp:TextBox ID="TxtId" runat="server" Visible="false"></asp:TextBox>
                        <asp:TextBox ID="TxtPageName" MaxLength="50" runat="server" CssClass="form-control" Enabled="false" Width="255px"></asp:TextBox>
                    </div>

                    <div class="form-group col-md-4">
                        <%=base.GetLabel("LblShowTitle", "Show title", ChkShowPageTitle, true)%>
                        <asp:CheckBox ID="ChkShowPageTitle" runat="server" CssClass="form-control" Enabled="true" />
                    </div>

                    <div class="form-group col-md-4">
                        <%=base.GetLabel("LblVisible", "Visible", ChkVisibile, true)%>
                        <asp:CheckBox ID="ChkVisibile" runat="server" CssClass="form-control" Enabled="true" />
                    </div>

                    <div class="form-group">
                        <%=base.GetLabel("LblTitle", "Title", null, true)%>
                        <asp:Panel runat="server" ID="PanelPageTitle"></asp:Panel>
                    </div>

                    <div class="form-group">
                        <%=base.GetLabel("LblContent", "Content", null, true)%>
                        <asp:Panel runat="server" ID="PanelPageContent"></asp:Panel>
                    </div>

                </div>

            </div>

        </asp:View>
    </asp:MultiView>
    </div>

</ContentTemplate>
</asp:UpdatePanel>