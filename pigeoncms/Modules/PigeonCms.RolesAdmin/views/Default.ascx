<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Default.ascx.cs" Inherits="Controls_Default" %>


<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="1" AssociatedUpdatePanelID="Upd1">
    <ProgressTemplate>
        <div class="loading"><%=PigeonCms.Utility.GetLabel("LblLoading", "loading") %></div>
    </ProgressTemplate>
</asp:UpdateProgress>

<script type="text/javascript">
    
    var deleteQuestion = '<%=PigeonCms.Utility.GetLabel("RECORD_DELETE_QUESTION") %>';

    function moveListItem(list1, list2)
    {
        var i;
        for (i = list1.length-1; i>=0; i--) 
        {
            if (list1.options[i].selected) {
                var opt = document.createElement("option");
                opt.value = list1.options[i].value;
                opt.text = list1.options[i].text;

                list2.add(opt, null);
                list1.remove(i);
            }
        }
    }
    
    function refreshHidden()
    {
        var hidden = document.getElementById('<%=HiddenUsersInRole.ClientID %>');
        var list1 = document.getElementById('<%=ListUsersInRole.ClientID %>');
        var i;
        hidden.value = "";
        for (i = list1.length-1; i>=0; i--) 
        {
            hidden.value += list1.options[i].value;
            if (i > 0) hidden.value += "|";
        }        
    }

    function addUser()
    {
        moveListItem(
            document.getElementById('<%=ListUsersNotInRole.ClientID %>'), 
            document.getElementById('<%=ListUsersInRole.ClientID %>')
            );
        refreshHidden();
    }
    
    function removeUser()
    {
        moveListItem(
            document.getElementById('<%=ListUsersInRole.ClientID %>'), 
            document.getElementById('<%=ListUsersNotInRole.ClientID %>')
            );
        refreshHidden();
    }
</script>
    
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

                <div class="panel panel-default">
                    <div class="table-responsive">

                        <asp:GridView ID="Grid1" runat="server" Width="100%" AllowPaging="True" AllowSorting="false" AutoGenerateColumns="False"
                            DataKeyNames="Role" OnRowCommand="Grid1_RowCommand" OnRowCreated="Grid1_RowCreated" 
                            OnPageIndexChanging="Grid1_PageIndexChanging" OnRowDataBound="Grid1_RowDataBound">
                            <Columns>

                                <asp:TemplateField HeaderText="Role" SortExpression="UserName">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LnkRole" runat="server" CausesValidation="false" CommandName="Select" CommandArgument='<%#Eval("Role") %>'>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                    
                                <asp:TemplateField HeaderText="Users">
                                    <ItemTemplate>
                                        <asp:Literal ID="LitUsersInRole" runat="server"></asp:Literal>
                                    </ItemTemplate>
                                </asp:TemplateField>
                    
                                <asp:TemplateField ItemStyle-Width="10" ItemStyle-HorizontalAlign="Right" HeaderText="#Users">
                                    <ItemTemplate>
                                        <asp:Literal ID="LitNumUsersInRole" runat="server"></asp:Literal>
                                    </ItemTemplate>
                                </asp:TemplateField>
                    
                                <asp:TemplateField ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="10">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LnkDel" runat="server" CommandName="DeleteRow" 
                                            CommandArgument='<%#Eval("Role") %>' OnClientClick="return confirm(deleteQuestion);">
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
                    <%=base.GetLabel("LblNewRole", "New role") %>
                    <div class="pull-right">
                        <div class="btn-group">
                            <asp:Button ID="BtnInsSave" runat="server" Text="<%$ Resources:PublicLabels, CmdSave %>" CssClass="btn btn-primary btn-xs" OnClick="BtnSave_Click" />
                            <asp:Button ID="BtnInsCancel" runat="server" Text="<%$ Resources:PublicLabels, CmdCancel %>" CssClass="btn btn-default btn-xs" CausesValidation="false" OnClick="BtnCancel_Click" />
                        </div>
                    </div>
                </div>
        
                <div class="panel-body">
                    <div class="form-group col-sm-12">
                        <%=PigeonCms.Utility.GetLabel("LblRolename", "Rolename", TxtRolename)%>
                        <asp:TextBox ID="TxtRolename" MaxLength="255" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

            </div>

        </asp:View>
        
        <asp:View ID="ViewUsers" runat="server">
        
            <div class="panel panel-default">

                <div class="panel-heading">
                    <%=base.GetLabel("LblUsersInRole", "Users in role") %>
                    <div class="pull-right">
                        <div class="btn-group">
                            <asp:Button ID="Button1" runat="server" Text="<%$ Resources:PublicLabels, CmdSave %>" CssClass="btn btn-primary btn-xs" OnClick="BtnSave_Click" />
                            <asp:Button ID="Button2" runat="server" Text="<%$ Resources:PublicLabels, CmdCancel %>" CssClass="btn btn-default btn-xs" CausesValidation="false" OnClick="BtnCancel_Click" />
                        </div>
                    </div>
                </div>
        
                <div class="panel-body">

                    <table class="table table-striped">
                    <tr>
                        <td colspan="3">
                            <%=PigeonCms.Utility.GetLabel("LblRolename", "Ruolo", null)%>
                            <asp:Literal ID="LitRolename" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%=PigeonCms.Utility.GetLabel("LblUsers", "Users", null)%><br />
                            <asp:ListBox ID="ListUsersNotInRole" SelectionMode="Multiple" Rows="18" 
                                CssClass="form-group form-control" runat="server">
                            </asp:ListBox>
                        </td>
                        <td style="vertical-align:middle;" align="center">
                            <input type="button" id="BtnAddUser" onclick="addUser();" value=">>>" class="btn btn-default btn-md" />
                            <br /><br />
                            <input type="button" id="BtnRemoveUser" onclick="removeUser();" value="<<<" class="btn btn-default btn-md" />
                        </td>
                        <td>
                            <%=PigeonCms.Utility.GetLabel("LblUsersInRole", "Users in role", null)%><br />
                            <asp:ListBox ID="ListUsersInRole" SelectionMode="Multiple" Rows="18" 
                                CssClass="form-group form-control" runat="server">
                            </asp:ListBox>
                            <asp:HiddenField ID="HiddenUsersInRole" runat="server" />
                        </td>                    
                    </tr>
                    </table>
                
                    </fieldset>
                </div>

            </div>

        </asp:View>
        
    </asp:MultiView>
    </div>

</ContentTemplate>
</asp:UpdatePanel>