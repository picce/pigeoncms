<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Default.ascx.cs" Inherits="Controls_ShipZonesWeightAdmin" %>

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

<asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" EnableScriptGlobalization="true"></asp:ScriptManager>
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
                                <asp:Button ID="BtnNew" runat="server" Text="<%$ Resources:PublicLabels, CmdNew %>" CssClass="btn btn-primary" OnClick="BtnNew_Click" />
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
                            <div class="form-group col-lg-3 col-md-6">
                                <asp:DropDownList ID="DropZoneFilter" runat="server" AutoPostBack="true" CssClass="form-control" 
                                OnSelectedIndexChanged="DropZoneFilter_SelectedIndexChanged"></asp:DropDownList>
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

                                <%-- 1 --%>
                                <asp:BoundField DataField="WeightFrom" HeaderText="Peso minimo" 
                                    ItemStyle-CssClass="small text-muted" SortExpression="WeightFrom" />

                                <%-- 2 --%>
                                <asp:BoundField DataField="WeightTo" HeaderText="Peso massimo" 
                                    ItemStyle-CssClass="small text-muted" SortExpression="WeightTo" />

                                <%-- 3 --%>
                                <asp:BoundField DataField="ShippingPrice" HeaderText="Tariffa spedizione" 
                                    ItemStyle-CssClass="small text-muted" SortExpression="ShippingPrice" />

                                <%-- 4 --%>
                                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10">
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
            
            <asp:ObjectDataSource ID="ObjDs1" runat="server" SortParameterName="sort"
                SelectMethod="GetByFilter" TypeName="PigeonCms.Shop.ShipZonesWeightManager" 
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

                <div class="panel-heading clearfix">
                    <%=base.GetLabel("LblDetails", "Details") %>
                    <div class="pull-right">
                        <div class="btn-group">
                            <asp:Button ID="BtnSave" runat="server" Text="<%$ Resources:PublicLabels, CmdSave %>" CssClass="btn btn-primary" OnClick="BtnSave_Click" />
                            <asp:Button ID="BtnCancel" runat="server" Text="<%$ Resources:PublicLabels, CmdCancel %>" CssClass="btn btn-default" CausesValidation="false" OnClick="BtnCancel_Click" />
                        </div>
                    </div>
                </div>

                <div class="panel-body">

                    <div class="form-group col-sm-6 col-md-6">
                        <%=base.GetLabel("LblCode", "Seleziona Zona", DDLZones, true)%>
                        <asp:DropDownList ID="DDLZones" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>

                    <div class="form-group col-sm-6 col-md-6">
                        <%=base.GetLabel("LblWeightFrom", "Peso minimo", TxtWeightFrom, true)%>
                        <asp:TextBox ID="TxtWeightFrom" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="form-group col-sm-6 col-md-6">
                        <%=base.GetLabel("LblWeightTo", "Peso massimo", TxtWeightTo, true)%>
                        <asp:TextBox ID="TxtWeightTo" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="form-group col-sm-6 col-md-6">
                        <%=base.GetLabel("LblShippingPrice", "Tariffa spedizione", TxtShippingPrice, true)%>
                        <asp:TextBox ID="TxtShippingPrice" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                </div>

            </div>

        </asp:View>

    </asp:MultiView>
    </div>

</ContentTemplate>
</asp:UpdatePanel>