<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Default.ascx.cs" Inherits="Controls_MenuAdmin" %>
<%@ Register src="~/Controls/ModuleParams.ascx" tagname="ModuleParams" tagprefix="uc1" %>
<%@ Register src="~/Controls/PermissionsControl.ascx" tagname="PermissionsControl" tagprefix="uc1" %>

<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="1" AssociatedUpdatePanelID="Upd1">
    <ProgressTemplate>
        <div class="loading"><%=PigeonCms.Utility.GetLabel("LblLoading", "loading") %></div>
    </ProgressTemplate>
</asp:UpdateProgress>

<script type="text/javascript">
var deleteQuestion = '<%=PigeonCms.Utility.GetLabel("RECORD_DELETE_QUESTION") %>';

function preloadTitle(sourceControlName, targetControl)
{
    if (targetControl.value == "")
        targetControl.value = document.getElementById(sourceControlName).value;
}

function preloadAlias(sourceControlName, targetControl)
{
    var res = document.getElementById(sourceControlName).value;
    if (targetControl.value == "")
    {
        res = res.toLowerCase();
        res = res.replace(/\ /g,'-');    //replace all occurs
        targetControl.value = res;
    }
}

function changeModueleTypes() {
    var elemname = '#<%=DropModuleTypes.ClientID %>';
    $(elemname).removeAttr('disabled');
    $(elemname).removeClass('locked');
    $('#lnkchange').hide();
}

function changeView() {
    var elemname = '#<%=DropViews.ClientID %>';
    $(elemname).removeAttr('disabled');
    $(elemname).removeClass('locked');
    $('#lnkChangeView').hide();
}

function pageLoad(sender, args) {
    $("div.fancy a").fancybox({
        'width': '80%',
        'height': '80%',
        'type': 'iframe',
        'hideOnContentClick': false,
        onClosed: function() { }
    });
}
</script>

<asp:UpdatePanel ID="Upd1" runat="server">
<ContentTemplate>
    
    <div class="row">
        <div class="col-lg-12">
            <%--<h1 class="page-header"><%=BaseModule.Title %></h1>--%>
            <asp:Label ID="LitErr" runat="server" Text=""></asp:Label>
            <asp:Label ID="LitOk" runat="server" Text=""></asp:Label>
        </div>
        <!-- /.col-lg-12 -->
    </div>

    <div class="row">
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0" OnActiveViewChanged="MultiView1_ActiveViewChanged">
    
        <asp:View ID="ViewSee" runat="server">

            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-body"> 
                        <div class="pull-right">
                            <div class="btn-group adminToolbar">
                                <asp:DropDownList runat="server" ID="DropNewItem"  AutoPostBack="true" CssClass="form-control adminMediumText" OnSelectedIndexChanged="DropNewItem_SelectedIndexChanged">
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
                            <div class="form-group col-sm-6 col-lg-3">
                                <asp:DropDownList ID="DropMenuTypesFilter" runat="server" AutoPostBack="true" CssClass="form-control adminMediumText" OnSelectedIndexChanged="DropMenuTypesFilter_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                            <div class="form-group col-sm-6 col-lg-3">
                                <asp:DropDownList ID="DropPublishedFilter" runat="server" AutoPostBack="true" CssClass="form-control adminMediumText" OnSelectedIndexChanged="DropPublishedFilter_SelectedIndexChanged"></asp:DropDownList>                            
                            </div>
                            <div class="form-group col-sm-6 col-lg-3">
                                <asp:DropDownList ID="DropMasterPageFilter" runat="server" AutoPostBack="true" CssClass="form-control adminMediumText" OnSelectedIndexChanged="DropMasterPageFilter_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                            <div class="form-group col-sm-6 col-lg-3">
                                <asp:DropDownList ID="DropModuleTypesFilter" runat="server" AutoPostBack="true" CssClass="form-control adminMediumText" OnSelectedIndexChanged="DropModuleTypesFilter_SelectedIndexChanged"></asp:DropDownList>
                            </div>  
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-lg-12">

                <div class="panel panel-default">
                    <div class="table-responsive">
            

                    <asp:GridView ID="Grid1" runat="server" Width="100%" AllowPaging="True" AllowSorting="false" AutoGenerateColumns="False"
                        DataSourceID="ObjDs1" DataKeyNames="Id"  OnRowCommand="Grid1_RowCommand" OnRowCreated="Grid1_RowCreated" OnRowDataBound="Grid1_RowDataBound">
                        <Columns>

                            <%--0--%>
                            <asp:TemplateField HeaderText="Name/Alias/Route" SortExpression="Alias">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LnkSel" runat="server" CommandName="Select" CommandArgument='<%#Eval("Id") %>'></asp:LinkButton>
                                    <br />
                                    <span class="small text-muted">
                                        <asp:Literal ID="LitAlias" runat="server" Text=""></asp:Literal>
                                    </span>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%--1--%>
                            <asp:TemplateField HeaderText="Css/Theme/Master">
                                <ItemTemplate>
                                <span class="small text-muted">
                                    <asp:Literal ID="LitStyle" runat="server" Text=""></asp:Literal>
                                </span>
                                </ItemTemplate>
                            </asp:TemplateField>
                    
                            <asp:TemplateField HeaderText="Module name">
                                <ItemTemplate>
                                <span class="small text-muted">
                                    <asp:Literal ID="LitModuleNameDesc" runat="server" Text=""></asp:Literal>
                                </span>
                                </ItemTemplate>
                            </asp:TemplateField>
                    
                            <%--3--%>
                            <asp:TemplateField ItemStyle-Width="10" ItemStyle-HorizontalAlign="Center" HeaderText="Content">
                                <ItemTemplate>
                                    <div class="fancy">
                                    <asp:HyperLink runat="server" ID="LnkModuleContent" Visible="false">
                                        <i class='fa fa-pgn_content fa-fw'></i>                            
                                    </asp:HyperLink>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                    
                            <%--10--%>
                            <asp:TemplateField HeaderText="Vis" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="ImgVisibleOk" runat="server" CommandName="ImgVisibleOk" Visible="false" CommandArgument='<%#Eval("Id") %>'>
                                        <i class='fa fa-pgn_checked fa-fw'></i>
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="ImgVisibleKo" runat="server" CommandName="ImgVisibleKo" Visible="false" CommandArgument='<%#Eval("Id") %>'>
                                        <i class='fa fa-pgn_unchecked fa-fw'></i>                            
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%--10--%>
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
                                <span class="small text-muted">
                                    <asp:Literal ID="LitAccessTypeDesc" runat="server" Text=""></asp:Literal>
                                </span>
                                </ItemTemplate>
                            </asp:TemplateField>
                    
                            <asp:TemplateField Visible="false" HeaderText="Access Level" SortExpression="AccessCode, AccessLevel">
                                <ItemTemplate>
                                <asp:Literal ID="LitAccessLevel" runat="server" Text=""></asp:Literal>
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
                            <asp:BoundField DataField="Ordering" Visible="false" HeaderText="" SortExpression="Ordering" ItemStyle-HorizontalAlign="Right" />

                            <asp:TemplateField HeaderText="" Visible="true" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="25">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LnkCopy" runat="server" CommandName="CopyRow" CommandArgument='<%#Eval("Id") %>'>
                                        <i class='fa fa-pgn_copy fa-fw'></i>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="SSL" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <i class='fa fa-pgn_security fa-fw' runat="server" id="ImgUseSsl" visible="false"></i>
                                    <%--<asp:Image runat="server" ID="ImgUseSsl" SkinID="ImgSecurity" Visible="false" />--%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Core" SortExpression="IsCore" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                <asp:CheckBox Enabled="false" Checked='<%#Eval("IsCore") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>                    
                    
                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="25">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LnkDel" runat="server" CommandName="DeleteRow" 
                                        CommandArgument='<%#Eval("Id") %>' OnClientClick="return confirm(deleteQuestion);">
                                        <i class='fa fa-pgn_delete fa-fw'></i>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                    
                            <asp:BoundField DataField="Id" Visible="false" HeaderText="ID" SortExpression="Id" />
                        </Columns>
                    </asp:GridView>
                
                    </div>
                </div>
            </div>

            <asp:ObjectDataSource ID="ObjDs1" runat="server" SortParameterName="" OnSelecting="ObjDs1_Selecting"
                SelectMethod="GetTree" TypeName="PigeonCms.MenuManager">
                <SelectParameters>
                    <asp:Parameter Name="filter" Type="object" />
                    <asp:Parameter Name="level" Type="Int32" DefaultValue="-1" />
                    <asp:Parameter Name="separatorText" Type="String" DefaultValue="--" />
                </SelectParameters>
                <DeleteParameters>
                    <asp:Parameter Name="Id" Type="Int32" />
                </DeleteParameters>
            </asp:ObjectDataSource>

        </asp:View>
   

        <asp:View ID="ViewInsert" runat="server">

            <div class="panel panel-default">
                
                <div class="panel-heading clearfix">
                    <%=base.GetLabel("LblDetails", "Details") %>
                    <div class="pull-right">
                        <div class="btn-group">
                            <asp:Button ID="BtnSave" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:PublicLabels, CmdSave %>" OnClick="BtnSave_Click" />
                            <asp:Button ID="BtnApply" CssClass="btn btn-default" runat="server" Text="<%$ Resources:PublicLabels, CmdApply %>" OnClick="BtnApply_Click" />
                            <asp:Button ID="BtnCancel" CssClass="btn btn-default" CausesValidation="false" runat="server" Text="<%$ Resources:PublicLabels, CmdCancel %>" OnClick="BtnCancel_Click" />
                        </div>
                    </div>
                </div>

                <div class="panel-body">

                    <ul class="nav nav-pills">
                        <li class="active"><a href="#tab-main" data-toggle="tab"><%=base.GetLabel("Main", "Main") %></a></li>
                        <li><a href="#tab-options" data-toggle="tab"><%=base.GetLabel("Options", "Options") %></a></li>
                        <li><a href="#tab-security" data-toggle="tab"><%=base.GetLabel("Security", "Security") %></a></li>
                        <li><a href="#tab-parameters" data-toggle="tab"><%=base.GetLabel("Parameters", "Parameters") %></a></li>
                    </ul>

                    <div class="tab-content row">

                        <div class="tab-pane fade in active" id="tab-main">
                            <h4></h4>
                            
                            <div class="form-group col-sm-6">
                                <%=base.GetLabel("LblMenuType", "Menu type", LitMenuType, true)%>
                                <asp:TextBox ID="LitMenuType" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>

                            <div class="form-group col-sm-6">
                                <%=base.GetLabel("LblContentType", "Content type", DropModuleTypes, true)%>
                                <a href="javascript:void(0)" onclick="changeModueleTypes();" id="lnkchange" runat="server">
                                    <%=base.GetLabel("LblChange", "change", null, true) %>
                                </a>
                                <asp:DropDownList ID="DropModuleTypes" CssClass="form-control" runat="server"
                                    AutoPostBack="true" OnSelectedIndexChanged="DropModuleTypes_SelectedIndexChanged"></asp:DropDownList>
                            </div>

                            <div class="form-group col-sm-6">
                                <%=base.GetLabel("LblName", "Name", TxtName, true)%>
                                <asp:TextBox ID="TxtName" MaxLength="200" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ReqName" ControlToValidate="TxtName" runat="server" Text="*"></asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group col-sm-6">
                                <%=base.GetLabel("LblAlias", "Alias", TxtAlias, true)%>
                                <asp:TextBox ID="TxtAlias" Rows="3" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ReqAlias" ControlToValidate="TxtAlias" runat="server" Text="*"></asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group col-sm-6">
                                <%=base.GetLabel("LblTitle", "Title", null, true)%>
                                <asp:Panel runat="server" ID="PanelTitle"></asp:Panel>
                            </div>

                            <div class="form-group col-sm-6">
                                <%=base.GetLabel("LblTitleWindow", "Window's title", null, true)%>
                                <asp:Panel runat="server" ID="PanelTitleWindow"></asp:Panel>
                            </div>

                            <div class="form-group col-lg-3 col-sm-6">
                                <%=base.GetLabel("LblRoute", "Route", DropRouteId, true)%>
                                <asp:DropDownList ID="DropRouteId" CssClass="form-control" runat="server">
                                </asp:DropDownList>
                            </div>

                            <div class="form-group col-lg-3 col-sm-6">
                                <%=base.GetLabel("LblUseSsl", "Use SSL", DropUseSsl, true)%>
                                <asp:DropDownList ID="DropUseSsl" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="2" Text="Use route settings"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="No"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                                </asp:DropDownList>                            
                            </div>

                            <div class="form-group col-lg-3 col-sm-6">
                                <%=base.GetLabel("LblLink", "Link", TxtLink, true)%>
                                <asp:TextBox ID="TxtLink" MaxLength="200" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>

                            <div class="form-group col-lg-3 col-sm-6">
                                <%=base.GetLabel("LblRedirectTo", "Redirect to", DropReferMenuId, true)%>
                                <asp:DropDownList ID="DropReferMenuId" CssClass="form-control" runat="server"></asp:DropDownList>
                            </div>

                            <div class="form-group col-sm-12">
                                <%=base.GetLabel("LblParentItem", "Parent item", ListParentId, true)%>
                                <asp:ListBox ID="ListParentId" SelectionMode="Single" Rows="18" runat="server" CssClass="form-control"></asp:ListBox>
                            </div>

                            <div class="form-group col-sm-6 col-lg-3">
                                <%=base.GetLabel("LblCssClass", "Css class", TxtCssClass, true)%>
                                <asp:TextBox ID="TxtCssClass" MaxLength="200" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>

                            <div class="form-group col-sm-6 col-lg-3">
                                <%=base.GetLabel("LblViews", "Views", DropViews, true)%>
                                <a href="javascript:void(0)" onclick="changeView();" id="lnkChangeView" runat="server">
                                    <%=base.GetLabel("LblChange", "change", null, true) %>
                                </a>
                                <asp:DropDownList ID="DropViews" runat="server" CssClass="form-control"
                                    AutoPostBack="true" OnSelectedIndexChanged="DropViews_SelectedIndexChanged"></asp:DropDownList>
                            </div>

                            <div class="form-group col-sm-6 col-lg-3">
                                <%=base.GetLabel("LblTheme", "Theme", DropCurrTheme, true)%>
                                <asp:DropDownList ID="DropCurrTheme" CssClass="form-control" runat="server"></asp:DropDownList>
                            </div>

                            <div class="form-group col-sm-6 col-lg-3">
                                <%=base.GetLabel("LblMasterpage", "Masterpage", DropCurrMasterPage, true)%>
                                <asp:DropDownList ID="DropCurrMasterPage" CssClass="form-control" runat="server"></asp:DropDownList>
                            </div>

                        </div>

                        <div class="tab-pane fade" id="tab-options">
                            <h4></h4>
                        
                            <div class="form-group col-sm-12">
                                <asp:CheckBox ID="ChkVisible" runat="server" Enabled="true" />
                                <%=base.GetLabel("LblVisible", "Visible", ChkVisible, true)%>
                            </div>

                            <div class="form-group col-sm-12">
                                <asp:CheckBox ID="ChkPublished" runat="server" Enabled="true" />
                                <%=base.GetLabel("LblPublished", "Published", ChkPublished, true)%>
                            </div>

                            <div class="form-group col-sm-12">
                                <asp:CheckBox ID="ChkOverridePageTitle" runat="server" Enabled="true" />
                                <%=base.GetLabel("LblOverrideTitle", "Override window title", ChkOverridePageTitle, true)%>
                            </div>

                            <div class="form-group col-sm-12">
                                <asp:CheckBox ID="ChkShowModuleTitle" runat="server" Enabled="true" />
                                <%=base.GetLabel("LblShowModuleTitle", "Show module title", ChkShowModuleTitle, true)%>
                            </div>

                            <div class="form-group col-sm-12">
                                <asp:CheckBox ID="ChkIsCore" runat="server" Enabled="false" />
                                <strong>Core</strong>
                            </div>

                            <div class="form-group col-sm-12">
                                <strong>ID</strong>
                                <asp:Label ID="LblId" runat="server" Text=""></asp:Label>
                            </div>

                            <div class="form-group col-sm-12">
                                <strong>Module Id</strong>
                                <asp:Label ID="LblModuleId" runat="server" Text=""></asp:Label>
                            </div>

                        </div>   
                        
                        <div class="tab-pane fade" id="tab-security">
                            <div class="form-group col-lg-12">
                                <uc1:PermissionsControl ID="PermissionsControl1" runat="server" />
                            </div>
                        </div>

                        <div class="tab-pane fade" id="tab-parameters">
                            <h4></h4>
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
