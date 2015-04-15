<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Default.ascx.cs" Inherits="Controls_Default" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register src="~/Controls/MemberEditorControl.ascx" tagname="MemberEditor" tagprefix="uc1" %>

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
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0" OnActiveViewChanged="MultiView1_ActiveViewChanged">
    
        <asp:View ID="ViewSee" runat="server">

            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-body"> 
                        <div class="pull-right">
                            <div class="btn-group adminToolbar">
                                <asp:Button ID="BtnNew" runat="server" Text="<%$ Resources:PublicLabels, CmdNew %>" 
                                    CssClass="btn btn-primary btn-xs" OnClick="BtnNew_Click" />
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
                                <asp:TextBox ID="TxtUserNameFilter" runat="server" AutoPostBack="true" CssClass="form-control" 
                                    ontextchanged="TxtUserNameFilter_TextChanged" placeholder="<username>"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-lg-12">

                <div class="panel panel-default">
                    <div class="table-responsive">
            
                    <asp:GridView ID="Grid1" runat="server" Width="100%" AllowPaging="True" 
                        AllowSorting="false" AutoGenerateColumns="False"
                        DataKeyNames="UserName" OnRowCommand="Grid1_RowCommand" 
                        OnRowCreated="Grid1_RowCreated" OnRowDataBound="Grid1_RowDataBound" 
                        onpageindexchanging="Grid1_PageIndexChanging" onsorting="Grid1_Sorting">
                        <Columns>
                    
                            <asp:TemplateField HeaderText="User" SortExpression="UserName">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LnkUserName" runat="server" CausesValidation="false" CommandName="Select" CommandArgument='<%#Eval("UserName") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                    
                            <asp:TemplateField HeaderText="Name">
                                <ItemTemplate>
                                    <asp:Literal ID="LitName" runat="server"></asp:Literal>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Email">
                                <ItemTemplate>
                                    <asp:Literal ID="LitEmail" runat="server"></asp:Literal>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Roles">
                                <ItemTemplate>
                                    <asp:Literal ID="LitRolesForUser" runat="server"></asp:Literal>
                                </ItemTemplate>
                            </asp:TemplateField>
                    
                            <asp:TemplateField HeaderText="Access Level" SortExpression="AccessCode, AccessLevel">
                                <ItemTemplate>
                                <asp:Literal ID="LitAccessLevel" runat="server" Text=""></asp:Literal>
                                </ItemTemplate>
                            </asp:TemplateField>
                    
                            <asp:TemplateField ItemStyle-Width="10">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LnkRoles" runat="server" CausesValidation="false" CommandName="Roles" CommandArgument='<%#Eval("UserName") %>'>
                                        <i class='fa fa-pgn_roles fa-fw'></i>                                    
                                    </asp:LinkButton>            
                                </ItemTemplate>
                            </asp:TemplateField>                    
                    
                            <asp:TemplateField ItemStyle-Width="10">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LnkPwd" runat="server" CausesValidation="false" CommandName="Password" CommandArgument='<%#Eval("UserName") %>'>
                                        <i class='fa fa-pgn_key fa-fw'></i>                                    
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                    
                            <asp:TemplateField HeaderText="Enabled">
                                <ItemTemplate>
                                <asp:CheckBox ID="ChkEnabled" Enabled="false" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Approved">
                                <ItemTemplate>
                                <asp:CheckBox ID="ChkApproved" Enabled="false" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Core" SortExpression="IsCore" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                <asp:CheckBox ID="ChkIsCore" Enabled="false" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                    
                            <asp:TemplateField ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="10">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LnkDel" runat="server" CommandName="DeleteRow" 
                                        CommandArgument='<%#Eval("Username") %>' OnClientClick="return confirm(deleteQuestion);">
                                        <i class='fa fa-pgn_delete fa-fw'></i>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>

                    </div>
                </div>
        </asp:View>
   

        <asp:View ID="ViewInsert" runat="server">
        
            <div class="panel panel-default">

                <div class="panel-heading">
                    &nbsp;
                    <div class="pull-right">
                        <div class="btn-group">
                            <asp:Button ID="BtnInsSave" runat="server" Text="<%$ Resources:PublicLabels, CmdSave %>" CssClass="btn btn-primary btn-xs" OnClick="BtnSave_Click" />
                            <asp:Button ID="BtnInsCancel" runat="server" Text="<%$ Resources:PublicLabels, CmdCancel %>" CssClass="btn btn-default btn-xs" CausesValidation="false" OnClick="BtnCancel_Click" />
                        </div>
                    </div>
                </div>
        
                <div class="panel-body">
                    <legend><asp:Literal runat="server" ID="LitTitle"></asp:Literal></legend>
                    <uc1:MemberEditor ID="MemberEditor1" runat="server" />                
                </div>

            </div>

        </asp:View>
        
    </asp:MultiView>
    </div>

</ContentTemplate>
</asp:UpdatePanel>