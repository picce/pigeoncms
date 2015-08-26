<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Default.ascx.cs" Inherits="Controls_ModulesAdmin" %>
<%@ Register src="~/Controls/ModuleParams.ascx" tagname="ModuleParams" tagprefix="uc1" %>
<%@ Register src="~/Controls/PermissionsControl.ascx" tagname="PermissionsControl" tagprefix="uc1" %>

<asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
<asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="1" AssociatedUpdatePanelID="Upd1">
    <ProgressTemplate>
        <div class="loading"><%=PigeonCms.Utility.GetLabel("LblLoading", "loading") %></div>
    </ProgressTemplate>
</asp:UpdateProgress>

<script type="text/javascript">
var deleteQuestion = '<%=PigeonCms.Utility.GetLabel("RECORD_DELETE_QUESTION") %>';

function disableListMenu()
{
    document.getElementById('<%=ListMenu.ClientID %>').disabled = 'disabled';
}
function enableListMenu()
{
    document.getElementById('<%=ListMenu.ClientID %>').disabled = '';
}

function changeView() {
    var elemname = '#<%=DropViews.ClientID %>';
    $(elemname).removeAttr('disabled');
    $(elemname).removeClass('locked');
    $('#lnkChangeView').hide();
}

function pageLoad(sender, args) {
    $("div.fancy a").fancybox({
        'width': '75%',
        'height': '75%',
        'type': 'iframe',
        'hideOnContentClick': false,
        onClosed: function () { }
    });
}
</script>

<asp:UpdatePanel ID="Upd1" runat="server">
<ContentTemplate>

    <div class="row">
        <div class="col-lg-12">
            <asp:Label ID="LblErr" runat="server" Text=""></asp:Label>
            <asp:Label ID="LblOk" runat="server" Text=""></asp:Label>
        </div>
    </div>
    
    <div class="row">
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0" OnActiveViewChanged="MultiView1_ActiveViewChanged">
    
        <asp:View ID="ViewSee" runat="server">

            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-body"> 
                        <div class="pull-right">
                            <div class="btn-group adminToolbar">
                                <asp:DropDownList runat="server" ID="DropNewModule" AutoPostBack="true" 
                                CssClass="form-control adminMediumText" 
                                OnSelectedIndexChanged="DropNewModule_SelectedIndexChanged">
                                </asp:DropDownList>
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
                            <div class="form-group col-md-4">
                                <asp:DropDownList runat="server" ID="DropPublishedFilter" AutoPostBack="true" 
                                CssClass="form-control adminMediumText" OnSelectedIndexChanged="DropPublishedFilter_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group col-md-4">
                                <asp:DropDownList runat="server" ID="DropTemplateBlockNameFilter" AutoPostBack="true" 
                                CssClass="form-control adminMediumText" OnSelectedIndexChanged="DropTemplateBlockNameFilter_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group col-md-4">
                                <asp:DropDownList runat="server" ID="DropModuleTypesFilter"  AutoPostBack="true" 
                                CssClass="form-control adminMediumText" OnSelectedIndexChanged="DropModuleTypesFilter_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-lg-12">

                <div class="panel panel-default">
                    <div class="table-responsive">

                        <asp:GridView ID="Grid1" runat="server" Width="100%" AllowPaging="True" AllowSorting="true" AutoGenerateColumns="False"
                            DataSourceID="ObjDs1" DataKeyNames="Id" OnRowCommand="Grid1_RowCommand" OnRowCreated="Grid1_RowCreated" OnRowDataBound="Grid1_RowDataBound">
                            <Columns>

                                <%--0--%>
                                <asp:TemplateField HeaderText="Title" SortExpression="Title">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LnkTitle" runat="server" CommandName="Select" CommandArgument='<%#Eval("Id") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                
                                <asp:BoundField DataField="TemplateBlockName" HeaderText="Block" SortExpression="TemplateBlockName" />
                    
                                <asp:TemplateField HeaderText="Menu" SortExpression="MenuSelection">
                                    <ItemTemplate>
                                    <asp:Literal ID="LitMenuEntries" runat="server" Text=""></asp:Literal>
                                    </ItemTemplate>
                                </asp:TemplateField>
                    
                                <asp:TemplateField HeaderText="Module name" SortExpression="ModuleNamespace, ModuleName">
                                    <ItemTemplate>
                                    <asp:Literal ID="LitModuleNameDesc" runat="server" Text=""></asp:Literal>
                                    </ItemTemplate>
                                </asp:TemplateField>
                    
                                <asp:TemplateField ItemStyle-Width="10" ItemStyle-HorizontalAlign="Center" HeaderText="Content">
                                    <ItemTemplate>
                                        <div class="fancy">
                                        <asp:HyperLink runat="server" ID="LnkModuleContent" Visible="false">
                                            <i class='fa fa-pgn_content fa-fw'></i>                            
                                        </asp:HyperLink>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Pub" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="ImgPublishedOk" runat="server" CommandName="ImgPublishedOk" Visible="false" CommandArgument='<%#Eval("Id") %>'>
                                            <i class='fa fa-pgn_checked fa-fw'></i>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="ImgPublishedKo" runat="server" CommandName="ImgPublishedKo" Visible="false" CommandArgument='<%#Eval("Id") %>'>
                                            <i class='fa fa-pgn_unchecked fa-fw'></i>                            
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Access">
                                    <ItemTemplate>
                                    <asp:Literal ID="LitAccessTypeDesc" runat="server" Text=""></asp:Literal>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="ImgMoveUp" runat="server" CommandName="MoveUp" CommandArgument='<%#Eval("Id") %>'>
                                            <i class='fa fa-pgn_up fa-fw'></i>                            
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="ImgMoveDown" runat="server" CommandName="MoveDown" CommandArgument='<%#Eval("Id") %>'>
                                            <i class='fa fa-pgn_down fa-fw'></i>                            
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Ordering" HeaderText="Order" SortExpression="Ordering" ItemStyle-HorizontalAlign="Right" />
                    
                                <asp:TemplateField HeaderText="Copy" Visible="true" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="25">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LnkCopy" runat="server" CommandName="CopyRow" CommandArgument='<%#Eval("Id") %>'>
                                            <i class='fa fa-pgn_copy fa-fw'></i>                            
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Core" SortExpression="IsCore" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                    <asp:CheckBox Enabled="false" Checked='<%#Eval("IsCore") %>' runat="server" />                
                                    </ItemTemplate>
                                </asp:TemplateField>                    

                                <asp:TemplateField HeaderText="Del" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="25">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LnkDel" runat="server" CommandName="DeleteRow" 
                                            CommandArgument='<%#Eval("Id") %>' OnClientClick="return confirm(deleteQuestion);">
                                            <i class='fa fa-pgn_delete fa-fw'></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="Id" HeaderText="ID" SortExpression="Id" />
                            </Columns>
                        </asp:GridView>
            
                    </div>
                </div>
            </div>

            <asp:ObjectDataSource ID="ObjDs1" runat="server" SortParameterName="sort" OnSelecting="ObjDs1_Selecting"
                SelectMethod="GetByFilter" TypeName="PigeonCms.ModulesManager">
                <SelectParameters>
                    <asp:Parameter Name="filter" Type="object" />
                    <asp:Parameter Name="sort" Type="String" DefaultValue="Nome" />
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
                            <asp:Button ID="BtnSave" CssClass="btn btn-primary btn-xs" runat="server" Text="<%$ Resources:PublicLabels, CmdSave %>"  OnClick="BtnSave_Click" />
                            <asp:Button ID="BtnCancel" CssClass="btn btn-default btn-xs" runat="server" Text="<%$ Resources:PublicLabels, CmdCancel %>" OnClick="BtnCancel_Click" />
                        </div>
                    </div>
                </div>

                <div class="panel-body">

                    <ul class="nav nav-pills">
                        <li class="active"><a href="#tab-main" data-toggle="tab"><%=base.GetLabel("Main", "Main") %></a></li>
                        <li><a href="#tab-menu" data-toggle="tab"><%=base.GetLabel("Menu", "Menu") %></a></li>
                        <li><a href="#tab-options" data-toggle="tab"><%=base.GetLabel("Options", "Options") %></a></li>
                        <li><a href="#tab-security" data-toggle="tab"><%=base.GetLabel("Security", "Security") %></a></li>
                        <li><a href="#tab-parameters" data-toggle="tab"><%=base.GetLabel("Parameters", "Parameters") %></a></li>
                    </ul>

                    <div class="tab-content">
                        <div class="tab-pane fade in active" id="tab-main">

                            <div class="form-group col-sm-12">
                                <%=base.GetLabel("LblModuleType", "Module type", null, true)%>
                                <asp:TextBox ID="LitModuleType" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                            </div>

                            <div class="form-group col-sm-12">
                                <%=base.GetLabel("LblTitle", "Title", null, true) %>
                                <asp:Panel runat="server" ID="PanelTitle"></asp:Panel>
                            </div>
                            
                            <div class="form-group col-sm-6">
                                <%=base.GetLabel("LblPosition", "Position", DropTemplateBlockName, true)%>
                                <asp:DropDownList ID="DropTemplateBlockName" CssClass="form-control" runat="server">
                                </asp:DropDownList></td>
                            </div>

                            <div class="form-group col-sm-6">
                                <%=base.GetLabel("LblOrder", "Order", DropOrdering, true)%>
                                <asp:DropDownList ID="DropOrdering" CssClass="form-control" runat="server">
                                </asp:DropDownList>
                            </div>

                            <div class="form-group col-sm-12">
                                <%=base.GetLabel("LblContent", "Content", TxtContent, true)%>
                                <asp:TextBox ID="TxtContent" Rows="3" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>

                        </div>

                        <div class="tab-pane fade" id="tab-menu">

                            <div class="form-group col-lg-6">
                                <%=base.GetLabel("LblMenus", "Menus", null, true) %>
                                <br />

                                <asp:RadioButton GroupName="MenuSelection" ID="RadioMenuAll" CssClass="" runat="server" />
                                <%=base.GetLabel("LblMenuAll", "All", RadioMenuAll, true)%>
                                <br />
                                
                                <asp:RadioButton GroupName="MenuSelection" ID="RadioMenuNone" CssClass="" runat="server" />
                                <%=base.GetLabel("LblMenuNone", "None", RadioMenuNone, true)%>
                                <br />

                                <asp:RadioButton GroupName="MenuSelection" ID="RadioMenuSelection" CssClass="" runat="server" />
                                <%=base.GetLabel("LblMenuSelection", "Select menu items", RadioMenuSelection, true)%>
                                <br />
                            </div>

                            <div class="form-group col-lg-6">
                                <%=base.GetLabel("LblMenuEntries", "Menu entries", ListMenu, true)%>
                                <asp:ListBox ID="ListMenu" SelectionMode="Multiple" CssClass="form-control" disabled="disabled" Rows="18" runat="server">
                                </asp:ListBox>
                            </div>
                         
                            <div class="form-group col-sm-12">
                                <%=base.GetLabel("LblViews", "Views", DropViews, true)%>
                                <a href="javascript:void(0)" onclick="changeView();" id="lnkChangeView" runat="server">
                                    <%=base.GetLabel("LblChange", "change", null, true) %>
                                </a>
                                <asp:DropDownList ID="DropViews" runat="server" CssClass="form-control"
                                    AutoPostBack="true" OnSelectedIndexChanged="DropViews_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                           
                        </div>

                        <div class="tab-pane fade" id="tab-options">

                            <div class="form-group col-sm-12">
                                <strong><%=base.GetLabel("LblRecordId", "ID") %></strong> 
                                <asp:Label ID="LblId" runat="server" Text=""></asp:Label>
                            </div>

                            <div class="form-group col-sm-12">
                                <strong><%=base.GetLabel("LblCreated", "Created")%></strong>
                                <asp:Label ID="LblCreated" runat="server" Text=""></asp:Label>
                            </div>

                            <div class="form-group col-sm-12">
                                <strong><%=base.GetLabel("LblLastUpdate", "Last update")%></strong>
                                <asp:Label ID="LblUpdated" runat="server" Text=""></asp:Label>
                            </div>

                            <div class="form-group col-sm-12">
                                <asp:CheckBox ID="ChkIsCore" runat="server" Enabled="false" />
                                <strong>Core</strong>
                            </div>

                            <div class="form-group col-sm-12">
                                <asp:CheckBox ID="ChkShowTitle" runat="server" Enabled="true" />
                                <%=base.GetLabel("LblShowTitle", "Show title", ChkShowTitle, true)%>
                            </div>

                            <div class="form-group col-sm-12">
                                <asp:CheckBox ID="ChkPublished" runat="server" Enabled="true" />
                                <%=base.GetLabel("LblPublished", "Published", ChkPublished, true)%>
                            </div>

                        </div>

                        <div class="tab-pane fade" id="tab-security">
                            <uc1:PermissionsControl ID="PermissionsControl1" runat="server" />
                        </div>

                        <div class="tab-pane fade" id="tab-parameters">
                            <uc1:ModuleParams ID="ModuleParams1" runat="server" />
                        </div>

                    </div>

                </div>

            </div>

        </asp:View>
    </asp:MultiView>
    </div>

</ContentTemplate>
</asp:UpdatePanel>