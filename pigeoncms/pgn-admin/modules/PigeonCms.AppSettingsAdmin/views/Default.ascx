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
                                    CssClass="btn btn-primary" OnClick="BtnNew_Click" />
                                <asp:Button ID="BtnApplySettings" runat="server" Text="<%$ Resources:PublicLabels, CmdApply %>" 
                                    CssClass="btn btn-default" OnClick="BtnApply_Click" />
                            </div>
                        </div> 
                    </div>
                </div>
            </div>
          
          
            <div class="col-lg-12">

                <%--INIT accordion--%>
                <div class="panel-group" id="accordion">

                    <asp:Repeater runat="server" ID="RepGroups" OnItemDataBound="RepGroups_ItemDataBound" OnItemCommand="RepGroups_ItemCommand">
                        <ItemTemplate>

                            <div class='panel <%# Eval("PanelClass") %>'>
                                <div class="panel-heading">

                                    <div class="pull-right">
                                        <div class="btn-group">
                                            <asp:Button runat="server" ID="BtnUpdateDbVersion" CssClass="btn btn-default btn-xs" 
                                                CommandName="UpdateVersion"
                                                Visible="false" Text="Upgrade version" />
                                        </div>
                                    </div>

                                    <h4 class="panel-title">
                        
                                        <i class='fa <%# Eval("IconClass") %> fa-fw'></i>
                                        <a data-toggle="collapse" data-parent="#accordion" href='#AccordionGroup<%# Eval("Row") %>'>
                                            <%# Eval("Title") %>
                                        </a><br />
                                        <span class="small text-muted">
                                            <%# Eval("Abstract") %><br />
                                            <asp:Literal runat="server" ID="LitVersionInfo"></asp:Literal>
                                        </span>
                                    </h4>
                                </div>
                                
                                <div id='AccordionGroup<%# Eval("Row") %>' class='panel-collapse <%#Eval("CollapseClass") %>'>
                                    <div class="panel-body">

                                        <%--INIT table--%>
                                        <div class="table-responsive">
                                        <table class="table table-hover">
                                        <tbody>
                                            <asp:Repeater runat="server" ID="RepSettings" 
                                                EnableViewState="true" 
                                                OnItemCommand="RepSettings_ItemCommand"
                                                OnItemDataBound="RepSettings_ItemDataBound">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:LinkButton ID="LnkName" runat="server" CausesValidation="false" CommandName="Select"></asp:LinkButton>
                                                            <br />
                                                            <span class="small text-muted">
                                                            <%#Eval("KeyTitle") %>
                                                            </span>
                                                        </td>

                                                        <td>
                                                            <span class="small text-muted">
                                                                <asp:Literal runat="server" ID="LblKeyValue"></asp:Literal>
                                                            </span>
                                                        </td>

                                                        <td>
                                                            <asp:LinkButton ID="LnkDel" runat="server" CommandName="DeleteRow" 
                                                                OnClientClick="return confirm(deleteQuestion);">
                                                                <i class='fa fa-pgn_delete fa-fw'></i>
                                                            </asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                        </table>
                                        </div>
                                        <%--END table--%>

                                    </div>
                                </div>
                            </div>

                        </ItemTemplate>
                    </asp:Repeater>

                </div>
                <%--EDN accordion--%>

            </div>

        </asp:View>
   
        <asp:View ID="ViewInsert" runat="server">

            <div class="panel panel-default">

                <div class="panel-heading clearfix">
                    <div class="pull-right">
                        <div class="btn-group">
                            <asp:Button ID="BtnSave" runat="server" Text="<%$ Resources:PublicLabels, CmdSave %>" CssClass="btn btn-primary" OnClick="BtnSave_Click" />
                            <asp:Button ID="BtnCancel" runat="server" Text="<%$ Resources:PublicLabels, CmdCancel %>" CssClass="btn btn-default" CausesValidation="false" OnClick="BtnCancel_Click" />
                        </div>
                    </div>
                </div>

                <div class="panel-body">

                    <div class="form-group col-md-6">
                        <%=base.GetLabel("LblKeySet", "KeySet", DropKeySet, true)%>
                        <asp:DropDownList runat="server" ID="DropKeySet" Enabled="false" CssClass="form-control"></asp:DropDownList>
                    </div>

                    <div class="form-group col-md-6">
                        <%=base.GetLabel("LblName", "Name", TxtKeyName, true)%>
                        <asp:TextBox ID="TxtKeyName" MaxLength="50" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                    </div>
                              
                    <div class="form-group col-md-6">
                        <%=base.GetLabel("LblTitle", "Title", TxtKeyTitle, true)%>
                        <asp:TextBox ID="TxtKeyTitle" MaxLength="500" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="form-group col-md-6">
                        <%=base.GetLabel("LblValue", "Value", true)%>
                        <asp:Panel ID="PanelValue" runat="server"></asp:Panel>
                        <%--<asp:TextBox ID="TxtKeyValue" MaxLength="500" runat="server" CssClass="form-control"></asp:TextBox>--%>
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