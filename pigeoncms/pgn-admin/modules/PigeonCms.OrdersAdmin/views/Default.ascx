<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Default.ascx.cs" Inherits="Controls_PigeonCms_Shop_OrdersAdmin" %>
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

function pageLoad(sender, args) {

}

var upd1 = '<%=Upd1.ClientID%>';

function ReloadUpd1() {
    if (upd1 != null) {
        __doPostBack(upd1, 'items');
    }
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
        </div>
    </div>


    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0" OnActiveViewChanged="MultiView1_ActiveViewChanged">

        <asp:View ID="ViewSee" runat="server">
        <div class="row">

            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-body"> 
                        <div class="pull-right">
                            <div class="btn-group adminToolbar">
                                <asp:Button ID="BtnNew" runat="server" Text="<%$ Resources:PublicLabels, CmdNew %>" 
                                    CssClass="btn btn-primary" OnClick="BtnNew_Click" />
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
                            <div class="form-group col-md-6 col-lg-3">
                                <%=GetLabel("OrderDateFilter", "Order date", DropOrderDatesRangeFilter) %>
                                <asp:DropDownList runat="server" ID="DropOrderDatesRangeFilter" AutoPostBack="true" CssClass="form-control" 
                                OnSelectedIndexChanged="Filter_Changed"></asp:DropDownList>
                            </div>

                            <div class="form-group col-md-6 col-lg-3">
                                <%=GetLabel("OwnerUserFilter", "Owner user", TxtOwnerUserFilter) %>
                                <asp:TextBox runat="server" ID="TxtOwnerUserFilter" CssClass="form-control" 
                                    OnTextChanged="Filter_Changed" AutoPostBack="true"></asp:TextBox>
                            </div>
                            <div class="form-group col-md-6 col-lg-3">
                                <%=GetLabel("ConfirmedFilter", "Confirmed", DropConfirmedFilter) %>
                                <asp:DropDownList ID="DropConfirmedFilter" runat="server" AutoPostBack="true" CssClass="form-control" 
                                OnSelectedIndexChanged="Filter_Changed"></asp:DropDownList>
                            </div>
                            <div class="form-group col-md-6 col-lg-3">
                                <%=GetLabel("PaymentFilter", "Payment", DropPaymentFilter) %>
                                <asp:DropDownList ID="DropPaymentFilter" runat="server" AutoPostBack="true" CssClass="form-control" 
                                OnSelectedIndexChanged="Filter_Changed"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-md-6 col-lg-3">
                <div class="panel panel-green">
                    <div class="panel-heading">
                        <div class="row">
                            <div class="col-xs-3">
                                <i class="fa fa-shopping-cart fa-4x"></i>
                            </div>
                            <div class="col-xs-9 text-right">
                                <div class="huge">
                                    <asp:Literal runat="server" ID="LitBoardOrdersCount"></asp:Literal>
                                </div>
                                <div>Orders</div>
                            </div>
                        </div>
                    </div>
                    <%--<a href="#">
                        <div class="panel-footer">
                            <span class="pull-left">View Details</span>
                            <span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>
                            <div class="clearfix"></div>
                        </div>
                    </a>--%>
                </div>
            </div>

            <div class="col-md-6 col-lg-3">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <div class="row">
                            <div class="col-xs-3">
                                <i class="fa fa-rocket fa-4x"></i>
                            </div>
                            <div class="col-xs-9 text-right">
                                <div class="huge">
                                    <asp:Literal runat="server" ID="LitBoardOrdersToShip"></asp:Literal>
                                </div>
                                <div>Orders to ship</div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-md-6 col-lg-3">
                <div class="panel panel-yellow">
                    <div class="panel-heading">
                        <div class="row">
                            <div class="col-xs-12 text-right">
                                <div class="huge">
                                    <asp:Literal runat="server" ID="LitBoardOrdersTotalAmount"></asp:Literal>
                                </div>
                                <div>Total amount</div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-md-6 col-lg-3">
                <div class="panel panel-red">
                    <div class="panel-heading">
                        <div class="row">
                            <div class="col-xs-12 text-right">
                                <div class="huge">
                                    <asp:Literal runat="server" ID="LitBoardOrdersTotalPaid"></asp:Literal>
                                </div>
                                <div>Total paid</div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-lg-12">

                <div class="panel panel-default">
                    <div class="table-responsive">

                    <asp:GridView ID="Grid1" runat="server" AllowPaging="True" AllowSorting="false" Width="100%" AutoGenerateColumns="False"
                        DataKeyNames="Id" OnRowCommand="Grid1_RowCommand" OnPageIndexChanging="Grid1_PageIndexChanging" 
                        OnRowCreated="Grid1_RowCreated" OnRowDataBound="Grid1_RowDataBound">
                        <Columns>

                            <asp:TemplateField HeaderText="Order" SortExpression="OrderRef" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <span class="small">
                                        <asp:LinkButton ID="LnkOrderRef" runat="server" CausesValidation="false" CommandName="Select" CommandArgument='<%#Eval("Id") %>'></asp:LinkButton>
                                    </span>
                                    <br />
                                    <small class="small text-muted">
                                        <asp:Literal ID="LitOwnerUser" runat="server" />
                                    </small>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Date" SortExpression="OrderDate" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <span class="small text-muted">
                                        <asp:Literal ID="LitOrderDate" runat="server" />
                                    </span>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Customer" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <span class="small text-muted">
                                        <i class="fa fa-folder-o"></i>
                                        <asp:Literal ID="LitCustomerName" runat="server" />
                                    </span><br />
                                    <span class="small text-muted">
                                        <asp:Literal ID="LitCustomerAddress" runat="server" />
                                    </span>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="<abbr title='Confirmed'>C</abbr>" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Literal ID="LitConfirmed" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="<abbr title='Paid'>P</abbr>" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Literal ID="LitPaid" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="<abbr title='Processed'>P</abbr>" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Literal ID="LitProcessed" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="<abbr title='Invoiced'>I</abbr>" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Literal ID="LitInvoiced" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Summary" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <span class="small text-muted">
                                        <asp:Literal ID="LitSummary" runat="server" />
                                    </span>
                                </ItemTemplate>
                            </asp:TemplateField>
                    
                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LnkDel" runat="server" CommandName="DeleteRow" 
                                        CommandArgument='<%#Eval("Id") %>' OnClientClick="return confirm(deleteQuestion);">
                                        <i class='fa fa-pgn_delete fa-fw'></i>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                    
                        </Columns>
                    </asp:GridView>
                    </div>
                </div>
            </div>
        
        </div>
        </asp:View>
   

        <asp:View ID="ViewInsert" runat="server">

            <div class="panel panel-default">

                <div class="panel-heading clearfix">
                    <asp:Literal runat="server" ID="LitCurrentOrder"></asp:Literal>
                    <div class="pull-right">
                        <div class="btn-group">
                            <asp:Button ID="BtnSave" runat="server" Text="<%$ Resources:PublicLabels, CmdSave %>" CssClass="btn btn-primary" OnClick="BtnSave_Click" />
                            <asp:Button ID="BtnCancel" runat="server" Text="<%$ Resources:PublicLabels, CmdCancel %>" CssClass="btn btn-default" CausesValidation="false" OnClick="BtnCancel_Click" />
                        </div>
                    </div>
                </div>

                <div class="panel-body">

                    <ul class="nav nav-pills">
                        <li class="active"><a href="#tab-header" data-toggle="tab"><%=base.GetLabel("Header", "Header") %></a></li>
                        <li><a href="#tab-rows" data-toggle="tab"><%=base.GetLabel("Rows", "Rows") %></a></li>
                    </ul>

                    <div class="tab-content row">
                        
                        <div class="tab-pane fade in active" id="tab-header">
                            <h4></h4>
                            <div class="form-group col-md-4">
                                <%=base.GetLabel("OrderRef", "Order ref.", TxtOrderRef, true)%>
                                <asp:TextBox ID="TxtOrderRef" runat="server"  CssClass="form-control"></asp:TextBox>
                            </div>

                            <div class="form-group col-md-4">
                                <%=base.GetLabel("OrderDate", "Date", TxtOrderDate, true)%>
                                <asp:TextBox ID="TxtOrderDate" runat="server" CssClass='form-control'></asp:TextBox>        
                                <cc1:CalendarExtender ID="CalOrderDate" runat="server" TargetControlID="TxtOrderDate" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                            </div>

                            <div class="form-group col-md-4">
                                <%=base.GetLabel("OrderDateRequested", "Date requested", TxtOrderDateRequested, true)%>
                                <asp:TextBox ID="TxtOrderDateRequested" runat="server" CssClass='form-control'></asp:TextBox>        
                                <cc1:CalendarExtender ID="CalOrderDateRequested" runat="server" TargetControlID="TxtOrderDateRequested" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                            </div>

                            <%---sped---%>
                            <div class="form-group col-lg-4">
                                <%=base.GetLabel("PaymentCode", "Payment code", DropPaymentCode, true)%>
                                <asp:DropDownList runat="server" CssClass='form-control' ID="DropPaymentCode"></asp:DropDownList>
                            </div>
                            <div class="form-group col-lg-4">
                                <%=base.GetLabel("Paid", "Paid", ChkPaid, true)%>
                                <asp:CheckBox runat="server" CssClass='form-control' ID="ChkPaid"></asp:CheckBox>
                            </div>
                            <div class="form-group col-lg-4">
                                <%=base.GetLabel("CouponCode", "Coupon", TxtCouponCode, true)%>
                                <asp:TextBox ID="TxtCouponCode" runat="server" MaxLength="50"  CssClass="form-control"></asp:TextBox>
                            </div>

                            <%--ship--%>
                            <div class="form-group col-lg-6">
                                <%=base.GetLabel("ShipCode", "Shipment code", DropShipCode, true)%>
                                <asp:DropDownList runat="server" CssClass='form-control' ID="DropShipCode"></asp:DropDownList>
                            </div>
                            <div class="form-group col-lg-6">
                                <%=base.GetLabel("OrderDateShipped", "Shipping date", TxtOrderDateShipped, true)%>
                                <asp:TextBox ID="TxtOrderDateShipped" runat="server" CssClass='form-control'></asp:TextBox>        
                                <cc1:CalendarExtender ID="CalOrderDateShipped" runat="server" TargetControlID="TxtOrderDateShipped" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                            </div>

                            <%---notes---%>
                            <div class="form-group col-lg-12">
                                <%=base.GetLabel("Notes", "Notes", TxtNotes, true)%>
                                <asp:TextBox ID="TxtNotes" runat="server" TextMode="MultiLine"  CssClass="form-control"></asp:TextBox>
                            </div>

                            <div class="form-group col-lg-12">
                                <%---customer data---%>
                                <div class="panel panel-info">
                                    <div class="panel-heading">
                                        <h4 class="panel-title">
                                            <a data-toggle="collapse" data-parent="#accordionCustomer" href="#collapseCustomer" aria-expanded="false" class="">
                                                <%=base.GetLabel("CustomerData", "Customer data") %>
                                            </a>
                                        </h4>
                                    </div>
                                    <div id="collapseCustomer" class="panel-collapse collapse" aria-expanded="true">
                                        <div class="panel-body">

                                            <div class="form-group col-md-6">
                                                <%=base.GetLabel("OrdName", "Name", TxtOrdName, true)%>
                                                <asp:TextBox ID="TxtOrdName" runat="server" MaxLength="255"  CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-md-6">
                                                <%=base.GetLabel("OrdAddress", "Address", TxtOrdAddress, true)%>
                                                <asp:TextBox ID="TxtOrdAddress" runat="server" MaxLength="255"  CssClass="form-control"></asp:TextBox>
                                            </div>

                                            <div class="form-group col-md-6 lg-3">
                                                <%=base.GetLabel("OrdZipCode", "Zip", TxtOrdZipCode, true)%>
                                                <asp:TextBox ID="TxtOrdZipCode" runat="server" MaxLength="255"  CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-md-6 lg-3">
                                                <%=base.GetLabel("OrdZipCode", "Zip", TxtOrdZipCode, true)%>
                                                <asp:TextBox ID="TextBox1" runat="server" MaxLength="255"  CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-md-6 lg-3">
                                                <%=base.GetLabel("OrdCity", "City", TxtOrdCity, true)%>
                                                <asp:TextBox ID="TxtOrdCity" runat="server" MaxLength="255"  CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-md-6 lg-3">
                                                <%=base.GetLabel("OrdState", "State", TxtOrdState, true)%>
                                                <asp:TextBox ID="TxtOrdState" runat="server" MaxLength="255"  CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-md-6 lg-3">
                                                <%=base.GetLabel("OrdNation", "Nation", TxtOrdNation, true)%>
                                                <asp:TextBox ID="TxtOrdNation" runat="server" MaxLength="255"  CssClass="form-control"></asp:TextBox>
                                            </div>

                                            <div class="form-group col-md-6">
                                                <%=base.GetLabel("OrdPhone", "Phone", TxtOrdPhone, true)%>
                                                <asp:TextBox ID="TxtOrdPhone" runat="server" MaxLength="255"  CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-md-6">
                                                <%=base.GetLabel("OrdEmail", "E-mail", TxtOrdEmail, true)%>
                                                <asp:TextBox ID="TxtOrdEmail" runat="server" MaxLength="255"  CssClass="form-control"></asp:TextBox>
                                            </div>

                                        </div>
                                    </div>
                                </div>

                                <%---order summary---%>
                                <div class="panel panel-success">
                                    <div class="panel-heading">
                                        <h4 class="panel-title">
                                            <a data-toggle="collapse" data-parent="#accordionSummary" href="#collapseSummary" aria-expanded="false" class="">
                                                <%=base.GetLabel("Order summary", "Order summary") %>
                                            </a>
                                        </h4>
                                    </div>
                                    <div id="collapseSummary" class="panel-collapse collapse in" aria-expanded="true">
                                        <div class="panel-body">

                                            <div class="form-group col-md-3">
                                                <%=base.GetLabel("QtyAmount", "Qty amount")%>
                                                <asp:Literal ID="LitQtyAmount" runat="server"></asp:Literal>
                                            </div>
                                            <div class="form-group col-md-3">
                                                <%=base.GetLabel("OrderAmount", "Order amount")%>
                                                <asp:Literal ID="LitOrderAmount" runat="server"></asp:Literal>
                                            </div>
                                            <div class="form-group col-md-3">
                                                <%=base.GetLabel("ShipAmount", "Ship amount")%>
                                                <asp:Literal ID="LitShipAmount" runat="server"></asp:Literal>
                                            </div>
                                            <div class="form-group col-md-3">
                                                <%=base.GetLabel("TotalAmount", "Total amount")%>
                                                <asp:Literal ID="LitTotalAmount" runat="server"></asp:Literal>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                                    
                        </div>

                        <div class="tab-pane fade" id="tab-rows">
                            <h4></h4>
                            <div class="form-group col-lg-12">
                            
                            <%--rows--%>
                            <div class="panel panel-default">
                                <div class="table-responsive">
                                    <asp:GridView ID="GridRows" runat="server" AllowPaging="false" AllowSorting="false" Width="100%" AutoGenerateColumns="False"
                                        DataKeyNames="Id" OnRowCommand="GridRows_RowCommand"  
                                        OnRowCreated="GridRows_RowCreated" OnRowDataBound="GridRows_RowDataBound">
                                        <Columns>

                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Image runat="server" ID="ImgPreview"></asp:Image>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <span class="small">
                                                        <asp:LinkButton ID="LnkProduct" runat="server" CausesValidation="false" CommandName="Select" CommandArgument='<%#Eval("Id") %>'></asp:LinkButton>
                                                    </span>
                                                    <br />
                                                    <small class="small text-muted">
                                                        <asp:Literal ID="LitProductDetail" runat="server" />
                                                    </small>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Qty" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <span class="small text-muted">
                                                        <asp:Literal ID="LitQty" runat="server" />
                                                    </span>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Price net" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <span class="small text-muted">
                                                        <asp:Literal ID="LitPriceNet" runat="server" />
                                                    </span>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Amount" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <span class="small text-muted">
                                                        <asp:Literal ID="LitAmountNet" runat="server" />
                                                    </span>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <span class="small text-muted">
                                                        <asp:Literal ID="LitNotes" runat="server" />
                                                    </span>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                    
                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LnkDel" runat="server" CommandName="DeleteRow" 
                                                        CommandArgument='<%#Eval("Id") %>' OnClientClick="return confirm(deleteQuestion);">
                                                        <i class='fa fa-pgn_delete fa-fw'></i>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                    
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                            </div>
                        </div>

                    </div>

                </div>
            
            </div>
        </asp:View>
    
    </asp:MultiView>
    

</ContentTemplate>
</asp:UpdatePanel>