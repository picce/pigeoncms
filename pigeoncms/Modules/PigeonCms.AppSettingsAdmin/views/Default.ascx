<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Default.ascx.cs" Inherits="Controls_Default" %>

<script type="text/javascript">
    // <!CDATA[
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
                                <asp:Button ID="BtnNew" runat="server" Text="<%$ Resources:PublicLabels, CmdNew %>" 
                                    CssClass="btn btn-primary btn-xs" OnClick="BtnNew_Click" />
                                <asp:Button ID="BtnApplySettings" runat="server" Text="<%$ Resources:PublicLabels, CmdApply %>" 
                                    CssClass="btn btn-default btn-xs" OnClick="BtnApply_Click" />
                            </div>
                        </div> 
                    </div>
                </div>
            </div>

            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="table-responsive">

                        <asp:GridView ID="Grid1" runat="server" AllowPaging="True" Width="100%" AutoGenerateColumns="False"
                            DataSourceID="ObjDs1" DataKeyNames="KeyName" OnRowCommand="Grid1_RowCommand" OnRowCreated="Grid1_RowCreated" OnRowDataBound="Grid1_RowDataBound">
                            <Columns>

                                <asp:TemplateField HeaderText="Name" ItemStyle-HorizontalAlign="Left" SortExpression="KeyName" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LnkName" runat="server" CausesValidation="false" CommandName="Select" CommandArgument='<%#Eval("KeyName") %>'></asp:LinkButton>
                                        <br />
                                        <%#Eval("KeyTitle") %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField ItemStyle-VerticalAlign="Top" HeaderText="Value" SortExpression="KeyValue">
                                    <ItemTemplate>
                                    <asp:Label runat="server" ID="LblKeyValue"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LnkDel" runat="server" CommandName="DeleteRow" 
                                            CommandArgument='<%#Eval("KeyName") %>' OnClientClick="return confirm(deleteQuestion);">
                                            <i class='fa fa-pgn_delete fa-fw'></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>

                    </div>
                </div>
            </div>
            
            <asp:ObjectDataSource ID="ObjDs1" runat="server" SelectMethod="GetSettings" TypeName="PigeonCms.AppSettingsManager">
                <SelectParameters></SelectParameters>
                <DeleteParameters>
                    <asp:Parameter Name="KeyName" Type="String" />
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

                    <asp:HiddenField ID="HiddenNewRecord" runat="server" />

                    <div class="form-group col-md-6">
                        <%=base.GetLabel("LblName", "Name", TxtKeyName, true)%>
                        <asp:TextBox ID="TxtKeyName" MaxLength="50" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                    </div>
                              
                    <div class="form-group col-md-6">
                        <%=base.GetLabel("LblTitle", "Title", TxtKeyTitle, true)%>
                        <asp:TextBox ID="TxtKeyTitle" MaxLength="500" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="form-group col-md-12">
                        <%=base.GetLabel("LblValue", "Value", TxtKeyValue, true)%>
                        <asp:TextBox ID="TxtKeyValue" MaxLength="500" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    
                    <div class="form-group col-md-12">
                        <%=base.GetLabel("LblInfo", "Additional info", TxtKeyInfo, true)%>
                        <asp:TextBox ID="TxtKeyInfo" MaxLength="500" runat="server" CssClass="form-control" Rows="3" TextMode="MultiLine"></asp:TextBox>
                    </div>

                </div>

            </div>

        </asp:View>
    </asp:MultiView>
    </div>

</ContentTemplate>
</asp:UpdatePanel>