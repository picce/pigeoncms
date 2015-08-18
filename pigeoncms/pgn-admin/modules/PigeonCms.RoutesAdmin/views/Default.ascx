<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Default.ascx.cs" Inherits="Controls_Default" %>

<script type="text/javascript">

    var deleteQuestion = '<%=PigeonCms.Utility.GetLabel("RECORD_DELETE_QUESTION") %>';

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
                                <asp:Button ID="BtnApplySettings" runat="server" Text="<%$ Resources:PublicLabels, CmdApply %>" CssClass="btn btn-default btn-xs" OnClick="BtnApply_Click" />
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

                            <div class="form-group col-md-6">
                                <asp:DropDownList runat="server" ID="DropPublishedFilter" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="DropPublishedFilter_SelectedIndexChanged">
                                    <asp:ListItem Value="">--Published--</asp:ListItem>
                                    <asp:ListItem Value="1">On-line</asp:ListItem>
                                    <asp:ListItem Value="0">Off-line</asp:ListItem>
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

                                <asp:TemplateField HeaderText="Name" SortExpression="Name">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LnkName" runat="server" CausesValidation="false" 
                                        CommandName="Select" CommandArgument='<%#Eval("Id") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
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

                                <asp:BoundField DataField="Pattern" HeaderText="Pattern" SortExpression="Pattern" />
                                <asp:BoundField DataField="CurrTheme" HeaderText="Theme" SortExpression="CurrTheme" />
                                <asp:BoundField DataField="CurrMasterpage" HeaderText="Masterpage" SortExpression="CurrMasterpage" />
                                
                                <asp:BoundField DataField="Ordering" HeaderText="Order" SortExpression="Ordering" Visible="false"
                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />

                                <asp:TemplateField HeaderText="Use SSL" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="ImgUseSslOk" runat="server" CommandName="ImgUseSslOk" Visible="false" CommandArgument='<%#Eval("Id") %>'>
                                            <i class='fa fa-pgn_checked fa-fw'></i>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="ImgUseSslKo" runat="server" CommandName="ImgUseSslKo" Visible="false" CommandArgument='<%#Eval("Id") %>'>
                                            <i class='fa fa-pgn_unchecked fa-fw'></i>                            
                                        </asp:LinkButton>                                    
                                    </ItemTemplate>
                                </asp:TemplateField>
                        
                                <asp:TemplateField HeaderText="Published" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="ImgEnabledOk" runat="server" CommandName="ImgEnabledOk" Visible="false" CommandArgument='<%#Eval("Id") %>'>
                                            <i class='fa fa-pgn_checked fa-fw'></i>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="ImgEnabledKo" runat="server" CommandName="ImgEnabledKo" Visible="false" CommandArgument='<%#Eval("Id") %>'>
                                            <i class='fa fa-pgn_unchecked fa-fw'></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                    
                                <asp:TemplateField HeaderText="Core" SortExpression="IsCore" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" Enabled="false" Checked='<%#Eval("IsCore") %>' runat="server" />                
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
                    
                                <asp:BoundField DataField="Id" HeaderText="ID" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" />
                            </Columns>
                        </asp:GridView>

                    </div>
                </div>

            </div>
            
            <asp:ObjectDataSource ID="ObjDs1" runat="server" SortParameterName="sort"
                SelectMethod="GetByFilter" TypeName="PigeonCms.MvcRoutesManager" OnSelecting="ObjDs1_Selecting">
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
                    &nbsp;
                    <div class="pull-right">
                        <div class="btn-group">
                            <asp:Button ID="BtnSave" runat="server" Text="<%$ Resources:PublicLabels, CmdSave %>" CssClass="btn btn-primary btn-xs" OnClick="BtnSave_Click" />
                            <asp:Button ID="BtnCancel" runat="server" Text="<%$ Resources:PublicLabels, CmdCancel %>" CssClass="btn btn-default btn-xs" CausesValidation="false" OnClick="BtnCancel_Click" />
                        </div>
                    </div>
                </div>
            
                <div class="panel-body">
                    
                    <div class="form-group col-sm-6">
                        <strong>ID</strong>
                        <asp:Label ID="LitId" runat="server" Text=""></asp:Label>
                    </div>

                    <div class="form-group col-sm-6">
                        Core
                        <asp:CheckBox ID="ChkIsCore" runat="server" Enabled="false" />
                    </div>

                    <div class="form-group col-sm-6">
                        <%=PigeonCms.Utility.GetLabel("LblName", "Name", TxtName) %>
                        <asp:TextBox ID="TxtName" MaxLength="50" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="form-group col-sm-6">
                        <%=PigeonCms.Utility.GetLabel("LblPattern", "Pattern", TxtPattern) %>
                        <asp:TextBox ID="TxtPattern" MaxLength="255" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="form-group col-sm-6">
                        <%=PigeonCms.Utility.GetLabel("LblEnabled", "Enabled", ChkPublished) %>
                        <asp:CheckBox ID="ChkPublished" runat="server" CssClass="form-control" Enabled="true" />
                    </div>

                    <div class="form-group col-sm-6">
                        <%=PigeonCms.Utility.GetLabel("LblUseSsl", "Use SSL", ChkUseSsl) %>
                        <asp:CheckBox ID="ChkUseSsl" runat="server" CssClass="form-control" Enabled="true" />
                    </div>

                    <div class="form-group col-sm-6">
                        <%=PigeonCms.Utility.GetLabel("", "Theme", DropTheme)%>
                        <asp:DropDownList ID="DropTheme" CssClass="form-control" runat="server">
                        </asp:DropDownList>
                    </div>

                    <div class="form-group col-sm-6">
                        <%=PigeonCms.Utility.GetLabel("", "Masterpage", DropMasterPage)%>
                        <asp:DropDownList ID="DropMasterPage" CssClass="form-control" runat="server">
                        </asp:DropDownList>
                    </div>
                </div>

            </div>

        </asp:View>
    </asp:MultiView>
    </div>

</ContentTemplate>
</asp:UpdatePanel>