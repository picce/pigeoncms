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
                                <asp:Button ID="BtnNew" runat="server" Text="Nuovo" CssClass="btn btn-primary btn-xs" OnClick="BtnNew_Click" />
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
                                <asp:DropDownList runat="server" ID="DropEnabledFilter" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="DropEnabledFilter_SelectedIndexChanged">
                                    <asp:ListItem Value="">--Enabled--</asp:ListItem>
                                    <asp:ListItem Value="1">True</asp:ListItem>
                                    <asp:ListItem Value="0">False</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                        </div>
                    </div>
                </div>
            </div>

            <div class="col-lg-12">

                <div class="panel panel-default">
                    <div class="table-responsive">

                        <asp:GridView ID="Grid1" Width="100%" runat="server" AllowPaging="True" AllowSorting="false" AutoGenerateColumns="False"
                            DataSourceID="ObjDs1" DataKeyNames="Name" OnRowCommand="Grid1_RowCommand" OnRowDataBound="Grid1_RowDataBound" OnRowCreated="Grid1_RowCreated">
                            <Columns>

                                <asp:TemplateField HeaderText="Name" SortExpression="Name">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LnkName" runat="server" CausesValidation="false"
                                        CommandName="Select" CommandArgument='<%#Eval("Name") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />

                                <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="<%$ Resources:PublicLabels, LblOrder %>" Visible="false" SortExpression="OrderId">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="ImgMoveUp" runat="server" CommandName="MoveUp" CommandArgument='<%#Eval("Name") %>'>
                                            <i class='fa fa-pgn_up fa-fw'></i>                            
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="ImgMoveDown" runat="server" CommandName="MoveDown" CommandArgument='<%#Eval("Name") %>'>
                                            <i class='fa fa-pgn_down fa-fw'></i>                            
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="OrderId" HeaderText="" Visible="false" SortExpression="OrderId" />

                                <asp:TemplateField HeaderText="Enabled" SortExpression="Enabled">
                                    <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" Enabled="false" Checked='<%#Eval("Enabled") %>' runat="server" />                
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LnkDel" runat="server" CommandName="DeleteRow" 
                                            CommandArgument='<%#Eval("Name") %>' OnClientClick="return confirm(deleteQuestion);">
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
                SelectMethod="GetByFilter" TypeName="PigeonCms.TemplateBlocksManager" OnSelecting="ObjDs1_Selecting">
                <SelectParameters>
                    <asp:Parameter Name="filter" Type="Object" />
                    <asp:Parameter Name="sort" Type="String" />
                </SelectParameters>
                <DeleteParameters>
                    <asp:Parameter Name="Name" Type="String" />
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

                    <asp:TextBox ID="TxtId" runat="server" Enabled="false" visible="false"></asp:TextBox>

                    <div class="form-group col-sm-12">
                        <%=base.GetLabel("Name", "Name", TxtName) %>
                        <asp:TextBox ID="TxtName" MaxLength="50" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>

                    <div class="form-group col-sm-12">
                        <%=base.GetLabel("Title", "Title", TxtTitle) %>
                        <asp:TextBox ID="TxtTitle" MaxLength="500" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="form-group col-sm-12">
                        <%=base.GetLabel("Enabled", "Enabled", ChkEnabled) %>
                        <asp:CheckBox ID="ChkEnabled" runat="server" Enabled="true" CssClass="form-control" />
                    </div>

                </div>

            </div>

        </asp:View>
    </asp:MultiView>
    </div>

</ContentTemplate>
</asp:UpdatePanel>