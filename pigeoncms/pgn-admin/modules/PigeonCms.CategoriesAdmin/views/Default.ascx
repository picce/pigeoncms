<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Default.ascx.cs" Inherits="Controls_CategoriesAdmin" %>
<%@ Register src="~/Controls/PermissionsControl.ascx" tagname="PermissionsControl" tagprefix="uc1" %>

<script type="text/javascript">
// <!CDATA[

function pageLoad(sender, args) 
{
    $("a.fancyRefresh").fancybox({
        'width': '80%',
        'height': '80%',
        'type': 'iframe',
        'hideOnContentClick': false,
        onClosed: function() { }
    });
}

var deleteQuestion = '<%=PigeonCms.Utility.GetLabel("RECORD_DELETE_QUESTION") %>';

// ]]>
</script>

<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
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
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">

        <asp:View ID="ViewSee" runat="server">

            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-body"> 
                        <div class="pull-right">
                            <div class="btn-group adminToolbar">
                                <asp:Button ID="BtnNew" runat="server" Text="<%$ Resources:PublicLabels, CmdNew %>" CssClass="btn btn-primary btn-xs" OnClick="BtnNew_Click" />
                            </div>
                        </div> 
                    </div>
                </div>
            </div>

            <div class="col-lg-12">
                <div class="panel panel-default adminFilters">
                    <div class="panel-heading">
                        <h4 class="panel-title">
                            <a data-toggle="collapse" data-parent="#accordion" href="#collapseOne">
                            <%=PigeonCms.Utility.GetLabel("LblFilters")%>
                            </a>
                        </h4>

                    </div>
                    <div id="collapseOne" class="panel-collapse collapse in">
                        <div class="panel-body">

                            <div class="form-group col-sm-6">
                                <asp:DropDownList runat="server" ID="DropEnabledFilter" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="DropEnabledFilter_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>

                            <div class="form-group col-sm-6">
                                <asp:DropDownList ID="DropSectionsFilter" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="DropSectionsFilter_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>

                        </div>
                    </div>
                </div>
            </div>

            <div class="col-lg-12">

                <div class="panel panel-default">
                    <div class="table-responsive">

                        <asp:GridView ID="Grid1" runat="server" AllowPaging="True" AllowSorting="false" Width="100%" AutoGenerateColumns="False"
                            DataSourceID="ObjDs1" DataKeyNames="Id" OnRowCommand="Grid1_RowCommand" OnRowCreated="Grid1_RowCreated" OnRowDataBound="Grid1_RowDataBound">
                            <Columns>
                                  
                                <%--0--%>                      
                                <asp:TemplateField HeaderText="<%$ Resources:PublicLabels, LblTitle %>" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LnkTitle" runat="server" CausesValidation="false" 
                                        CommandName="Select" CommandArgument='<%#Eval("Id") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%--1--%>
                                <asp:TemplateField HeaderText="<%$ Resources:PublicLabels, LblSection %>" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Literal ID="LitSectionTitle" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%--2--%>
                                <asp:BoundField DataField="CssClass" HeaderText="Css" SortExpression="CssClass" />
                                
                                <%--3--%>
                                <asp:BoundField DataField="Ordering" SortExpression="Ordering" ItemStyle-HorizontalAlign="Right" />
                                <%--4--%>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="<%$ Resources:PublicLabels, LblOrder %>" SortExpression="Ordering">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="ImgMoveUp" runat="server" CommandName="MoveUp" CommandArgument='<%#Eval("Id") %>'>
                                            <i class='fa fa-pgn_up fa-fw'></i>                            
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="ImgMoveDown" runat="server" CommandName="MoveDown" CommandArgument='<%#Eval("Id") %>'>
                                            <i class='fa fa-pgn_down fa-fw'></i>                            
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                       
                                <%--5--%>
                                <asp:TemplateField HeaderText="<%$ Resources:PublicLabels, LblEnabled %>" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="ImgEnabledOk" runat="server" CommandName="ImgEnabledOk" Visible="false" CommandArgument='<%#Eval("Id") %>'>
                                            <i class='fa fa-pgn_checked fa-fw'></i>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="ImgEnabledKo" runat="server" CommandName="ImgEnabledKo" Visible="false" CommandArgument='<%#Eval("Id") %>'>
                                            <i class='fa fa-pgn_unchecked fa-fw'></i>                            
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%--6--%>
                                <asp:TemplateField HeaderText="Access" SortExpression="AccessType">
                                    <ItemTemplate>
                                    <asp:Literal ID="LitAccessTypeDesc" runat="server" Text=""></asp:Literal>
                                    </ItemTemplate>
                                </asp:TemplateField>
                    
                                <%--7--%>
                                <asp:TemplateField HeaderText="Files">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="LnkUploadFiles" runat="server">
                                            <i class='fa fa-pgn_attach fa-fw'></i>
                                        </asp:HyperLink>
                                        <br />
                                        <span><asp:Literal ID="LitFilesCount" runat="server" Text=""></asp:Literal></span>
                                    </ItemTemplate>
                                </asp:TemplateField>
                    
                                <%--8--%>
                                <asp:TemplateField HeaderText="Img">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="LnkUploadImg" runat="server">
                                            <i class='fa fa-pgn_image fa-fw'></i>
                                        </asp:HyperLink>
                                        <br />
                                        <span><asp:Literal ID="LitImgCount" runat="server" Text=""></asp:Literal></span>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%--9--%>
                                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LnkDel" runat="server" CommandName="DeleteRow" 
                                            CommandArgument='<%#Eval("Id") %>' OnClientClick="return confirm(deleteQuestion);">
                                            <i class='fa fa-pgn_delete fa-fw'></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                    
                                <%--10--%>
                                <asp:BoundField DataField="Id" HeaderText="ID" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" />
                            </Columns>
                        </asp:GridView>

                    </div>
                </div>

            </div>
            
            <asp:ObjectDataSource ID="ObjDs1" runat="server" SortParameterName="sort"
                SelectMethod="GetByFilter" TypeName="PigeonCms.CategoriesManager" 
                OnObjectCreating="ObjDs1_ObjectCreating"                
                OnSelecting="ObjDs1_Selecting">
                <SelectParameters>
                    <asp:Parameter Name="filter" Type="Object" />
                    <asp:Parameter Name="sort" Type="String" DefaultValue="Ordering" />
                </SelectParameters>
                <DeleteParameters>
                    <asp:Parameter Name="Id" Type="Int32" />
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
                            <asp:Button ID="BtnCancel" runat="server" Text="<%$ Resources:PublicLabels, CmdCancel %>" CssClass="btn btn-default btn-xs" CausesValidation="false" OnClick="BtnCancel_Click" />
                        </div>
                    </div>
                </div>

                <div class="panel-body">
                    <div class="form-group col-sm-12 col-md-4">
                        <%=base.GetLabel("LblSection", "Section", DropSections, true)%>
                        <asp:DropDownList ID="DropSections" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>

                    <div class="form-group col-sm-6 col-md-4">
                        <%=base.GetLabel("LblCssClass", "Css class", TxtCssClass, true)%>
                        <asp:TextBox ID="TxtCssClass" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="form-group col-sm-6 col-md-4">
                        <%=base.GetLabel("LblEnabled", "Enabled", ChkEnabled, true)%>
                        <asp:CheckBox ID="ChkEnabled" runat="server" CssClass="form-control" Enabled="true" />
                    </div>

                    <div class="form-group col-lg-12">
                        <%=base.GetLabel("LblTitle", "Title", null, true)%>
                        <asp:Panel runat="server" ID="PanelTitle"></asp:Panel>
                    </div>

                    <div class="form-group col-lg-12">
                        <%=base.GetLabel("LblDescription", "Description", null, true)%>
                        <asp:Panel runat="server" ID="PanelDescription"></asp:Panel>
                    </div>

                    <uc1:PermissionsControl ID="PermissionsControl1" runat="server" />

                </div>

            </div>

        </asp:View>
    </asp:MultiView>
    </div>

</ContentTemplate>
</asp:UpdatePanel>