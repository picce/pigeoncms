<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Default.ascx.cs" Inherits="Controls_CouponAdmin" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script type="text/javascript">
    // <!CDATA[

    function pageLoad(sender, args) {
        $("a.fancyRefresh").fancybox({
            'width': '80%',
            'height': '80%',
            'type': 'iframe',
            'hideOnContentClick': false,
            onClosed: function () { }
        });
    }

    var deleteQuestion = '<%=PigeonCms.Utility.GetLabel("RECORD_DELETE_QUESTION") %>';

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

                <div class="panel panel-default">
                    <div class="table-responsive">

                        <asp:GridView ID="Grid1" runat="server" AllowPaging="True" AllowSorting="false" Width="100%" AutoGenerateColumns="False"
                            DataSourceID="ObjDs1" DataKeyNames="Id" OnRowCommand="Grid1_RowCommand" OnRowCreated="Grid1_RowCreated" OnRowDataBound="Grid1_RowDataBound">
                            <Columns>
                                                   
                                <asp:TemplateField HeaderText="<%$ Resources:PublicLabels, LblTitle %>" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LnkTitle" runat="server" CausesValidation="false" 
                                        CommandName="Select" CommandArgument='<%#Eval("Id") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="Amount" HeaderText="Importo" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" />

                                <asp:TemplateField HeaderText="Valido dal " ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Literal ID="LitValidFrom" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Valido al " ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Literal ID="LitValidTo" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="<%$ Resources:PublicLabels, LblEnabled %>" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="ImgEnabledOk" runat="server" CommandName="ImgEnabledOk" Visible="false" CommandArgument='<%#Eval("Id") %>'>
                                            <i class='fa fa-pgn_checked fa-fw'></i>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="ImgEnabledKo" runat="server" CommandName="ImgEnabledKo" Visible="false" CommandArgument='<%#Eval("Id") %>'>
                                            <i class='fa fa-pgn_unchecked fa-fw'></i>                            
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LnkDel" runat="server" CommandName="DeleteRow" 
                                            CommandArgument='<%#Eval("Id") %>' OnClientClick="return confirm(deleteQuestion);">
                                            <i class='fa fa-pgn_delete fa-fw'></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                    
                                <asp:BoundField DataField="Id" HeaderText="ID" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Right" />

                            </Columns>
                        </asp:GridView>

                    </div>
                </div>

            </div>
            
            <asp:ObjectDataSource ID="ObjDs1" runat="server" SortParameterName="sort"
                SelectMethod="GetByFilter" TypeName="PigeonCms.Shop.CouponsManager" 
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


                    <div class="form-group col-sm-6 col-md-4">
                        <%=base.GetLabel("LblTxtCode", "Codice Coupon", TxtCode, true)%>
                        <asp:TextBox ID="TxtCode" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="form-group col-md-4">
                        <%=base.GetLabel("LblEnabled", "Enabled", ChkEnabled, true)%>
                        <asp:CheckBox ID="ChkEnabled" runat="server" CssClass="form-control" Enabled="true" />
                    </div>

                    <div class="form-group col-sm-6 col-md-4">
                        <%=base.GetLabel("LblAmount", "Importo", TxtAmount, true)%>
                        <asp:TextBox ID="TxtAmount" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="form-group col-md-4">
                        <%=base.GetLabel("LblIsPercentage", "Valore in Percentuale", ChkPercentage, true)%>
                        <asp:CheckBox ID="ChkPercentage" runat="server" CssClass="form-control" Enabled="true" />
                    </div>

                    <div class="form-group col-sm-6 col-md-4">
                        <%=base.GetLabel("LblMinOrderAmount", "Importo Minimo", TxtMinAmount, true)%>
                        <asp:TextBox ID="TxtMinAmount" runat="server" CssClass="form-control"></asp:TextBox>
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

                    <div class="form-group col-sm-6 col-md-4">
                        <%=base.GetLabel("LblMaxUses", "Numero d'usi", TxtMaxUses, true)%>
                        <asp:TextBox ID="TxtMaxUses" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <asp:ListBox ID="ListCategories" SelectionMode="Multiple" Rows="10" 
                        CssClass="form-group form-control" runat="server"></asp:ListBox>

                </div>

            </div>



        </asp:View>
    </asp:MultiView>
    </div>

</ContentTemplate>
</asp:UpdatePanel>