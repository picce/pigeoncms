<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ShopProduct.ascx.cs" Inherits="Controls_ShopProduct" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register src="~/Controls/ItemParams.ascx" tagname="ItemParams" tagprefix="uc1" %>
<%@ Register src="~/Controls/PermissionsControl.ascx" tagname="PermissionsControl" tagprefix="uc1" %>
<%@ Register src="~/Controls/ContentEditorControl.ascx" tagname="ContentEditorControl" tagprefix="uc1" %>

<script type="text/javascript">
    // <!CDATA[

    function preloadAlias(sourceControlName, targetControl) {
        var res = document.getElementById(sourceControlName).value;
        if (targetControl.value == "") {
            res = res.toLowerCase();
            res = res.replace(/\ /g, '-');    //replace all occurs
            targetControl.value = res;
        }
    }

    function autosuggest(sourceControl, targetControl) {
        var res = sourceControl.value;
        if (targetControl.value == "") {
            targetControl.value = res;
            $(targetControl).focus().select();
        }
    }

    function pageLoad(sender, args) {
        $(document).ready(function () {
            $("a.fancyRefresh").fancybox({
                'width': '80%',
                'height': '80%',
                'type': 'iframe',
                'hideOnContentClick': false,
                onClosed: function () {
                    RemoveFromCache(onSuccess, onFailure);
                    ReloadUpd1();
                }
            });
        });
    }

var upd1 = '<%=Upd1.ClientID%>';

    function ReloadUpd1() {
        if (upd1 != null) {
            __doPostBack(upd1, 'items');
        }
    }

    function changeTab(tabId) {
        $('.nav-pills a[href="#' + tabId + '"]').tab('show');
    }

    var deleteQuestion = '<%=PigeonCms.Utility.GetLabel("RECORD_DELETE_QUESTION") %>';

//use in popup version
function closePopup() {
    parent.$.fancybox.close();
}

function onSuccess(result) { }
function onFailure(result) { }
// ]]>
</script>

<cc1:ToolkitScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></cc1:ToolkitScriptManager>
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
            <span id="spanResult" style="display: none"></span>
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
                                <asp:DropDownList runat="server" ID="DropNew"  AutoPostBack="true" CssClass="form-control" 
                                    OnSelectedIndexChanged="DropNew_SelectedIndexChanged"></asp:DropDownList>
                                <asp:Button ID="BtnNew" runat="server" Text="<%$ Resources:PublicLabels, CmdNew %>" 
                                    Visible="true" CssClass="btn btn-primary btn-xs" OnClick="BtnNew_Click" />
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
                            <div class="form-group col-lg-4 col-md-6">
                                <asp:DropDownList ID="DropEnabledFilter" runat="server" AutoPostBack="true" CssClass="form-control" 
                                OnSelectedIndexChanged="DropEnabledFilter_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                            <div class="form-group col-lg-4 col-md-6">
                                <asp:DropDownList ID="DropCategoriesFilter" runat="server" AutoPostBack="true" CssClass="form-control" 
                                OnSelectedIndexChanged="DropCategoriesFilter_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                            <div class="form-group col-lg-4 col-md-6">
                                <asp:DropDownList ID="DropProductTypeFilter" runat="server" AutoPostBack="true" CssClass="form-control" 
                                OnSelectedIndexChanged="DropProductTypeFilter_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


            <div class="col-lg-12">

                <div class="panel panel-default">
                    <div class="table-responsive">
                        <asp:GridView ID="Grid1" runat="server" AllowPaging="True" AllowSorting="true" Width="100%" AutoGenerateColumns="False"
                            DataSourceID="ObjDs1" DataKeyNames="Id" OnRowCommand="Grid1_RowCommand" OnRowCreated="Grid1_RowCreated" OnRowDataBound="Grid1_RowDataBound">
                            <Columns>
                                <%--0--%>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LnkShowVariants" runat="server" CausesValidation="false" CommandArgument='<%#Eval("Id") %>'>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%--1--%>
                                <asp:TemplateField HeaderText="<%$ Resources:PublicLabels, LblTitle %>" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LnkTitle" runat="server" CausesValidation="false" CommandName="Select" CommandArgument='<%#Eval("Id") %>'></asp:LinkButton>
                                        <br />
                                        <span class="small text-muted">
                                            <asp:Literal ID="LitItemInfo" runat="server" />
                                        </span>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%--2--%>
                                <asp:BoundField DataField="Alias" HeaderText="Alias" 
                                    ItemStyle-CssClass="small text-muted" SortExpression="Alias" />
                            
                                <%--3--%>
                                <asp:BoundField Visible="false" DataField="CssClass" HeaderText="Css" 
                                    ItemStyle-CssClass="small text-muted" SortExpression="CssClass" />

                                <%--4--%>
                                <asp:TemplateField HeaderText="<%$ Resources:PublicLabels, LblCategory %>" 
                                    ItemStyle-CssClass="small text-muted" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Literal ID="LitCategoryTitle" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%--5--%>
                                <asp:TemplateField HeaderText="Access" SortExpression="AccessType"
                                    ItemStyle-CssClass="small text-muted">
                                    <ItemTemplate>
                                    <asp:Literal ID="LitAccessTypeDesc" runat="server" Text=""></asp:Literal>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                 <%--6--%>
                                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="ImgEnabledOk" runat="server" CommandName="ImgEnabledOk" Visible="false" CommandArgument='<%#Eval("Id") %>'>
                                            <i class='fa fa-pgn_checked fa-fw'></i>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="ImgEnabledKo" runat="server" CommandName="ImgEnabledKo" Visible="false" CommandArgument='<%#Eval("Id") %>'>
                                            <i class='fa fa-pgn_unchecked fa-fw'></i>                            
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                               <%--7--%>
                                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right" SortExpression="Ordering">
                                    <ItemTemplate>
                                        <div>
                                            <asp:LinkButton ID="ImgMoveUp" runat="server" CommandName="MoveUp" CommandArgument='<%#Eval("Id") %>'>
                                                <i class='fa fa-pgn_up fa-fw'></i>                            
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="ImgMoveDown" runat="server" CommandName="MoveDown" CommandArgument='<%#Eval("Id") %>'>
                                                <i class='fa fa-pgn_down fa-fw'></i>                            
                                            </asp:LinkButton>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%-- 8 --%>
                                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="LnkUploadFiles" runat="server">
                                            <i class='fa fa-pgn_attach fa-fw'></i>
                                        </asp:HyperLink>
                                        <br />
                                        <span class="small text-muted"><asp:Literal ID="LitFilesCount" runat="server" Text=""></asp:Literal></span>
                                    </ItemTemplate>
                                </asp:TemplateField>
                    
                                <%-- 9 --%>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="LnkUploadImg" runat="server">
                                            <i class='fa fa-pgn_image fa-fw'></i>
                                        </asp:HyperLink>
                                        <br />
                                        <span class="small text-muted"><asp:Literal ID="LitImgCount" runat="server" Text=""></asp:Literal></span>
                                    </ItemTemplate>
                                </asp:TemplateField>
                    
                                <%-- 10--%>
                                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LnkDel" runat="server" CommandName="DeleteRow" 
                                            CommandArgument='<%#Eval("Id") %>' OnClientClick="return confirm(itemsDeleteQuestion);">
                                            <i class='fa fa-pgn_delete fa-fw'></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                    
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
            
            <asp:ObjectDataSource ID="ObjDs1" runat="server" 
                SortParameterName="sort" SelectMethod="GetByFilter" 
                TypeName="PigeonCms.Shop.ProductItemsManager" 
                OnObjectCreating="ObjDs1_ObjectCreating"
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
   

        <asp:View ID="ViewInsert" runat="server" >

            <div class="panel panel-default">

                <div class="panel-heading">
                    <%=base.GetLabel("LblDetails", "Details") %>
                    <div class="pull-right">
                        <div class="btn-group">
                            <asp:Button ID="BtnSave" runat="server" Text="<%$ Resources:PublicLabels, CmdSave %>" CssClass="btn btn-primary btn-xs" OnClick="BtnSave_Click" validationgroup="SaveProduct" />
                            <asp:Button ID="BtnCancel" runat="server" Text="<%$ Resources:PublicLabels, CmdCancel %>" CssClass="btn btn-default btn-xs" CausesValidation="false" OnClick="BtnCancel_Click" />
                        </div>
                    </div>
                </div>

                <div class="panel-body">

                    <ul class="nav nav-pills">
                        <li class="active"><a href="#tab-main" data-toggle="tab"><%=base.GetLabel("Main", "Main") %></a></li>
                        <li><a href="#tab-security" data-toggle="tab"><%=base.GetLabel("Security", "Security") %></a></li>
                        <li><a href="#tab-parameters" data-toggle="tab"><%=base.GetLabel("Parameters", "Parameters") %></a></li>
                        <li><a href="#tab-related" data-toggle="tab"><%=base.GetLabel("Related", "Related") %></a></li>
                        <asp:PlaceHolder ID="plhConfigurableProductTab" runat="server" Visible="false">
                            <li><a href="#tab-associated" data-toggle="tab"><%=base.GetLabel("Associated", "Associated Products") %></a></li>
                        </asp:PlaceHolder>
                    </ul>

                    <div class="tab-content">
                        
                        <div class="tab-pane fade in active" id="tab-main">

                            <div class="form-group col-md-3">
                                <%=base.GetLabel("LblItemType", "Item type", LitItemType, true)%>
                                <asp:TextBox ID="LitItemType" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                            </div>

                            <div class="form-group col-md-3">
                                <%=base.GetLabel("LblCategory", "Category", DropCategories, true)%>
                                <asp:DropDownList ID="DropCategories" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>

                            <div class="form-group col-md-3">
                                <%=base.GetLabel("LblEnabled", "Enabled", ChkEnabled, true)%>
                                <asp:CheckBox ID="ChkEnabled" runat="server" CssClass="form-control" Enabled="true" />
                            </div>

                            <div class="form-group col-md-3">
                                <%=base.GetLabel("LblProductGroup", "Gruppo Prodotto", DropSets, true)%>
                                <asp:DropDownList ID="DropSets" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="DropSets_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                                              
                            <div class="col-lg-12">
                                <%=base.GetLabel("LblTitle", "Title", null, true)%>
                                <asp:Panel runat="server" ID="PanelTitle"></asp:Panel>
                            </div>

                            <div class="form-group col-md-6">
                                <%=base.GetLabel("LblSKU", "SKU", TxtSKU, true)%>
                                <asp:RequiredFieldValidator ID="ReqSKU" ControlToValidate="TxtSKU" runat="server" Text="*" ForeColor="Red" validationgroup="SaveProduct"></asp:RequiredFieldValidator>
                                <asp:TextBox ID="TxtSKU" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>

                            <div class="col-md-6">
                                <%=base.GetLabel("LblWeight", "Weight", TxtWeight, true)%>
                                <asp:RequiredFieldValidator ID="ReqWeight" ControlToValidate="TxtWeight" runat="server" Text="*" ForeColor="Red" validationgroup="SaveProduct"></asp:RequiredFieldValidator>
                                <div class="form-group input-group">
                                    <asp:TextBox ID="TxtWeight" runat="server" CssClass="form-control"></asp:TextBox>
                                    <span class='input-group-addon'><%=ShopSettings.WeightUnit %></span>
                                </div>
                            </div>

                            <div class="form-group col-lg-12">
                                <%=base.GetLabel("LblAttributes", "Attributes", null, true)%>
                                <asp:Panel runat="server" ID="PanelAttributes"></asp:Panel>
                            </div>

                            <div class="col-md-6">
                                <%=base.GetLabel("LblRegularPrice", "Regular Price", TxtRegularPrice, true)%>
                                <asp:RequiredFieldValidator ID="ReqRegularPrice" ControlToValidate="TxtRegularPrice" runat="server" Text="*" ForeColor="Red" validationgroup="SaveProduct"></asp:RequiredFieldValidator>
                                <div class="form-group input-group">
                                    <asp:TextBox ID="TxtRegularPrice" runat="server" CssClass="form-control"></asp:TextBox>
                                    <span class='input-group-addon'><%=ShopSettings.ShopCurrency %></span>
                                </div>
                            </div>

                            <div class="col-md-6">
                                <%=base.GetLabel("LblSalePrice", "Sale Price", TxtSalePrice, true)%>
                                <div class="form-group input-group">
                                    <asp:TextBox ID="TxtSalePrice" runat="server" CssClass="form-control"></asp:TextBox>
                                    <span class='input-group-addon'><%=ShopSettings.ShopCurrency %></span>
                                </div>
                            </div>

                            <div class="col-lg-12">
                                <div class="form-group col-md-6" style="padding-left:0">
                                    <%=base.GetLabel("LblStock", "Stock Availability", DropStock, true)%>
                                    <asp:DropDownList ID="DropStock" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="1" Text="In Stock"></asp:ListItem>
                                        <asp:ListItem Value="0" Text="Out Of Stock"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="ReqDropStock" ControlToValidate="DropStock" runat="server" Text="*" ForeColor="Red" validationgroup="SaveProduct"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group col-md-6" style="padding-right:0">
                                    <%=base.GetLabel("LblAvailability", "Qty", TxtQty, true)%>
                                    <asp:RequiredFieldValidator ID="ReqTxtQty" ControlToValidate="TxtQty" runat="server" Text="*" ForeColor="Red" validationgroup="SaveProduct"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="TxtQty" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group col-md-6">
                                <%=base.GetLabel("LblAlias", "Alias", TxtAlias, true)%>
                                <asp:RequiredFieldValidator ID="ReqAlias" ControlToValidate="TxtAlias" runat="server" Text="*" ForeColor="Red" ValidationGroup="SaveProduct"></asp:RequiredFieldValidator>
                                <asp:TextBox ID="TxtAlias" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>

                            <div class="form-group col-md-6">
                                <%=base.GetLabel("LblCssClass", "Css class", TxtCssClass, true)%>
                                <asp:TextBox ID="TxtCssClass" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                            </div>

                            <div class="form-group col-sm-4 col-md-4">
                                <%=base.GetLabel("LblItemDate", "Item date", TxtItemDate, true)%>
                                <asp:TextBox ID="TxtItemDate" runat="server" CssClass='form-control'></asp:TextBox>        
                                <cc1:CalendarExtender ID="CalItemDate" runat="server" TargetControlID="TxtItemDate" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                            </div>

                            <div class="form-group col-sm-4 col-md-4">
                                <%=base.GetLabel("LblValidFrom", "Valid from", TxtValidFrom, true)%>
                                <asp:TextBox ID="TxtValidFrom" runat="server" CssClass='form-control'></asp:TextBox>        
                                <cc1:CalendarExtender ID="CalValidFrom" runat="server" TargetControlID="TxtValidFrom" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                            </div>

                            <div class="form-group col-sm-4 col-md-4">
                                <%=base.GetLabel("LblValidTo", "Valid to", TxtValidTo, true)%>
                                <asp:TextBox ID="TxtValidTo" runat="server" CssClass='form-control'></asp:TextBox>        
                                <cc1:CalendarExtender ID="CalValidTo" runat="server" TargetControlID="TxtValidTo" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                            </div>
                    
                            <div class="form-group col-sm-12">
                                <%=base.GetLabel("LblDescription", "Description", null, true)%>
                                <asp:Panel runat="server" ID="PanelDescription"></asp:Panel>
                            </div>

                        </div>

                        <div class="tab-pane fade" id="tab-security">
                            
                            <div class="form-group col-sm-12">
                                <strong><%=base.GetLabel("LblRecordId", "ID") %></strong>
                                <asp:Label ID="LblId" runat="server" Text=""></asp:Label>
                            </div>

                            <div class="form-group col-sm-12">
                                <strong><%=base.GetLabel("LblCreated", "Created") %></strong>
                                <asp:Label ID="LblCreated" runat="server" Text=""></asp:Label>
                            </div>

                            <div class="form-group col-sm-12">
                                <strong><%=base.GetLabel("LblLastUpdate", "Last update") %></strong>
                                <asp:Label ID="LblUpdated" runat="server" Text=""></asp:Label>
                            </div>

                            <div class="form-group col-sm-12">
                                <strong><%=base.GetLabel("LblOrderId", "Order Id") %></strong>
                                <asp:Label ID="LblOrderId" runat="server" Text=""></asp:Label>
                            </div>

                            <uc1:PermissionsControl ID="PermissionsControl1" runat="server" />
                        </div>

                        <div class="tab-pane fade" id="tab-parameters">
                            <uc1:ItemParams ID="ItemFields1" runat="server" />
                            <uc1:ItemParams ID="ItemParams1" runat="server" />
                        </div>
                        
                        <div class="tab-pane fade" id="tab-related">
                            
                            <div class="row col-lg-12">
                                <%=base.GetLabel("LblAssociateRelated", "Associa Correlati", null, true) %>
                                <div class="panel panel-default">
                                    <div class="table-responsive">

                                        <asp:GridView ID="GridRelated" runat="server" AllowPaging="True" AllowSorting="true" Width="100%" AutoGenerateColumns="False"
                                            DataSourceID="ObjDs3" DataKeyNames="Id" OnRowDataBound="GridRelated_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Associate" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkRow" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Title" HeaderText="Title" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" />
                                                <asp:BoundField DataField="SKU" HeaderText="SKU" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" />
                                                <asp:BoundField DataField="RegularPrice" HeaderText="Regular Price" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" />
                                                <asp:BoundField DataField="SalePrice" HeaderText="Sale Price" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" />
                                                <asp:BoundField DataField="Id" HeaderText="ID" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" />
                                            </Columns>
                                        </asp:GridView>

                                        <asp:ObjectDataSource ID="ObjDs3" runat="server" 
                                            SortParameterName="sort" SelectMethod="GetByFilter" 
                                            TypeName="PigeonCms.Shop.ProductItemsManager" 
                                            OnObjectCreating="ObjDs3_ObjectCreating"
                                            OnSelecting="ObjDs3_Selecting">
                                            <SelectParameters>
                                                <asp:Parameter Name="filter" Type="Object" />
                                                <asp:Parameter Name="sort" Type="String" DefaultValue="" />
                                            </SelectParameters>
                                            <DeleteParameters>
                                                <asp:Parameter Name="Id" Type="Int32" />
                                            </DeleteParameters>
                                        </asp:ObjectDataSource>

                                    </div>
                                </div>

                            </div>
                        </div>

                        <asp:PlaceHolder ID="PlhConfigurableProductPane" runat="server" Visible="false">

                            <div class="tab-pane fade" id="tab-associated">

                                <div class="row">

                                    <div class="form-group col-md-6">

                                        <div class="form-group col-lg-12">
                                            <%=base.GetLabel("LblSKU", "SKU", QuickTxtSKU, true)%>
                                            <asp:RequiredFieldValidator ID="ReqQuickSKU" ControlToValidate="QuickTxtSKU" runat="server" Text="*" validationgroup="QuickProduct"></asp:RequiredFieldValidator>
                                            <asp:TextBox ID="QuickTxtSKU" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-12">
                                            <%=base.GetLabel("LblEnabled", "Enabled", DropEnabled, true)%>
                                            <asp:DropDownList ID="DropEnabled" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="" Text="-- Select status --"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="Enabled"></asp:ListItem>
                                                <asp:ListItem Value="0" Text="Disabled"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="col-lg-12">
                                            <%=base.GetLabel("LblWeight", "Weight", QuickTxtWeight, true)%>
                                            <asp:RequiredFieldValidator ID="ReqQuickWeight" ControlToValidate="QuickTxtWeight" runat="server" Text="*" ForeColor="Red" validationgroup="QuickProduct"></asp:RequiredFieldValidator>
                                            <div class="form-group input-group">
                                                <asp:TextBox ID="QuickTxtWeight" runat="server" CssClass="form-control"></asp:TextBox>
                                                <span class='input-group-addon'><%=ShopSettings.WeightUnit %></span>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-12">
                                            <%=base.GetLabel("LblAttributes", "Attributes", null, true)%>
                                            <asp:Panel runat="server" ID="QuickAttributes"></asp:Panel>
                                        </div>

                                        <div class="col-md-6">
                                            <%=base.GetLabel("LblRegularPrice", "Regular Price", QuickTxtRegularPrice, true)%>
                                            <asp:RequiredFieldValidator ID="ReqQuickRegPrice" ControlToValidate="QuickTxtRegularPrice" runat="server" Text="*" ForeColor="Red" validationgroup="QuickProduct"></asp:RequiredFieldValidator>
                                            <div class="form-group input-group">
                                                <asp:TextBox ID="QuickTxtRegularPrice" runat="server" CssClass="form-control"></asp:TextBox>
                                                <span class='input-group-addon'><%=ShopSettings.ShopCurrency %></span>
                                            </div>
                                        </div>

                                        <div class="col-md-6">
                                            <%=base.GetLabel("LblSalePrice", "Sale Price", QuickTxtSalePrice, true)%>
                                            <div class="form-group input-group">
                                                <asp:TextBox ID="QuickTxtSalePrice" runat="server" CssClass="form-control"></asp:TextBox>
                                                <span class='input-group-addon'><%=ShopSettings.ShopCurrency %></span>
                                            </div>
                                        </div>

                                        <div class="form-group col-md-6">
                                            <%=base.GetLabel("LblStock", "Stock Availability", QuickDropStock, true)%>
                                            <asp:DropDownList ID="QuickDropStock" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="1" Text="In Stock"></asp:ListItem>
                                                <asp:ListItem Value="0" Text="Out Of Stock"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="ReqQuickStock" ControlToValidate="QuickDropStock" runat="server" Text="*" ForeColor="Red" validationgroup="QuickProduct"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-md-6">
                                            <%=base.GetLabel("LblAvailability", "Qty", QuickTxtQty, true)%>
                                            <asp:RequiredFieldValidator ID="ReqQuickQty" ControlToValidate="QuickTxtQty" runat="server" Text="*" ForeColor="Red" validationgroup="QuickProduct"></asp:RequiredFieldValidator>
                                            <asp:TextBox ID="QuickTxtQty" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-12">
                                              <asp:Button ID="BtnQuickAdd" runat="server" Text="Quick Add" CssClass="btn btn-primary btn-xs" OnClick="BtnQuickAdd_Click" validationgroup="QuickProduct" />
                                        </div>

                                    </div>

                                    <div class="form-group col-md-6">

                                        <div class="panel panel-default">
                                            <div class="table-responsive">

                                                <asp:GridView ID="GridViewSimple" runat="server" AllowPaging="True" AllowSorting="true" Width="100%" AutoGenerateColumns="False"
                                                    DataSourceID="ObjDs2" DataKeyNames="Id" OnRowDataBound="GridViewSimple_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Associate" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkRow" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Title" HeaderText="Title" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" />
                                                        <asp:BoundField DataField="SKU" HeaderText="SKU" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" />
                                                        <asp:BoundField DataField="RegularPrice" HeaderText="Regular Price" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" />
                                                        <asp:BoundField DataField="SalePrice" HeaderText="Sale Price" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" />
                                                        <asp:BoundField DataField="Id" HeaderText="ID" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" />
                                                    </Columns>
                                                </asp:GridView>

                                                <asp:ObjectDataSource ID="ObjDs2" runat="server" 
                                                    SortParameterName="sort" SelectMethod="GetByFilter" 
                                                    TypeName="PigeonCms.Shop.ProductItemsManager" 
                                                    OnObjectCreating="ObjDs2_ObjectCreating"
                                                    OnSelecting="ObjDs2_Selecting">
                                                    <SelectParameters>
                                                        <asp:Parameter Name="filter" Type="Object" />
                                                        <asp:Parameter Name="sort" Type="String" DefaultValue="" />
                                                    </SelectParameters>
                                                    <DeleteParameters>
                                                        <asp:Parameter Name="Id" Type="Int32" />
                                                    </DeleteParameters>
                                                </asp:ObjectDataSource>

                                            </div>
                                        </div>

                                    </div>

                                </div>

                            </div>

                        </asp:PlaceHolder>

                    </div>

                </div>
            
            </div>
        </asp:View>
    
    </asp:MultiView>
    </div>

</ContentTemplate>
</asp:UpdatePanel>