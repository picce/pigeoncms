<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Default.ascx.cs" Inherits="Controls_Default" %>

<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="1" AssociatedUpdatePanelID="Upd1">
    <ProgressTemplate>
        <div class="loading"><%=PigeonCms.Utility.GetLabel("LblLoading", "loading") %></div>
    </ProgressTemplate>
</asp:UpdateProgress>

<script type="text/javascript">
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
    
    <asp:Label ID="LblErr" runat="server" Text="" CssClass="error"></asp:Label>
    <asp:Label ID="LblOk" runat="server" Text="" CssClass="success"></asp:Label>

    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0" OnActiveViewChanged="MultiView1_ActiveViewChanged">
    
        <asp:View ID="ViewSee" runat="server">
            <div class="adminToolbar">
                <asp:Button ID="BtnNew" runat="server" Text="<%$ Resources:PublicLabels, CmdNew %>" CssClass="button" OnClick="BtnNew_Click" />
            </div>
            <br />
            <asp:GridView ID="Grid1" runat="server" Width="100%" AllowPaging="True" AllowSorting="false" AutoGenerateColumns="False"
                DataKeyNames="Role" OnRowCommand="Grid1_RowCommand" OnRowCreated="Grid1_RowCreated" 
                OnPageIndexChanging="Grid1_PageIndexChanging" OnRowDataBound="Grid1_RowDataBound">
                <Columns>
                    <asp:TemplateField ItemStyle-Width="10">
                        <ItemTemplate>
                        <asp:ImageButton ID="LnkUsers" CommandName="Select" CommandArgument='<%#Eval("Role") %>' 
                        runat="server" SkinID="ImgUsers" ToolTip="Members" />                
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:BoundField DataField="Role" HeaderText="Ruolo" />
                    
                    <asp:TemplateField HeaderText="Utenti">
                        <ItemTemplate>
                            <asp:Literal ID="LitUsersInRole" runat="server"></asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField ItemStyle-Width="10" ItemStyle-HorizontalAlign="Right" HeaderText="#Utenti">
                        <ItemTemplate>
                            <asp:Literal ID="LitNumUsersInRole" runat="server"></asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="10">
                        <ItemTemplate>
                        <asp:ImageButton ID="LnkDel" CommandName="DeleteRow" CommandArgument='<%#Eval("Role") %>' runat="server" 
                        SkinID="ImgDelFile" OnClientClick="return confirm('Cancellare la riga?');"  />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </asp:View>
   

        <asp:View ID="ViewInsert" runat="server">
        
            <div class="adminToolbar">
                <asp:Button ID="BtnInsSave" runat="server" Text="<%$ Resources:PublicLabels, CmdSave %>" CssClass="button" OnClick="BtnSave_Click" />
                <asp:Button ID="BtnInsCancel" runat="server" Text="<%$ Resources:PublicLabels, CmdCancel %>" CssClass="button" OnClick="BtnCancel_Click" />
            </div>
        
            <div class="col width-50">
                <fieldset class="adminForm">
                <legend>Nuovo ruolo</legend>
                
                <table class="adminTable">
                <tr>
                    <td class="key"><%=PigeonCms.Utility.GetLabel("LblRolename", "Rolename", TxtRolename)%></td>
                    <td>
                        <asp:TextBox ID="TxtRolename" MaxLength="255" runat="server" CssClass="adminMediumText"></asp:TextBox>
                    </td>
                </tr>
                </table>
                
                </fieldset>
            </div>
        </asp:View>
        
        <asp:View ID="ViewUsers" runat="server">
        
            <div class="adminToolbar">
                <asp:Button ID="Button1" runat="server" Text="<%$ Resources:PublicLabels, CmdSave %>" CssClass="button" OnClick="BtnSave_Click" />
                <asp:Button ID="Button2" runat="server" Text="<%$ Resources:PublicLabels, CmdCancel %>" CssClass="button" OnClick="BtnCancel_Click" />
            </div>
        
            <div class="col width-100">
                <fieldset class="adminForm">
                <legend>Utenti nel ruolo</legend>
                
                <table class="adminTable">
                <tr>
                    <td class="key"><%=PigeonCms.Utility.GetLabel("LblRolename", "Ruolo")%></td>
                    <td>
                        <asp:Literal ID="LitRolename" runat="server"></asp:Literal>
                    </td>
                    <td colspan="3">
                    </td>                  
                </tr>
                <tr>
                    <td class="key"><%=PigeonCms.Utility.GetLabel("LblUsers", "Utenti")%></td>
                    <td>
                        <asp:ListBox ID="ListUsersNotInRole" SelectionMode="Multiple" Rows="18" 
                            CssClass="adminMediumText" runat="server">
                        </asp:ListBox>
                    </td>
                    <td style="vertical-align:middle;">
                        <input type="button" id="BtnAddUser" onclick="addUser();" value=">>>" class="button" />
                        <br /><br />
                        <input type="button" id="BtnRemoveUser" onclick="removeUser();" value="<<<" class="button" />
                    </td>
                    <td class="key"><%=PigeonCms.Utility.GetLabel("LblUsersInRole", "Utenti nel ruolo")%></td>
                    <td>
                        <asp:ListBox ID="ListUsersInRole" SelectionMode="Multiple" Rows="18" 
                            CssClass="adminMediumText" runat="server">
                        </asp:ListBox>
                        <asp:HiddenField ID="HiddenUsersInRole" runat="server" />
                    </td>                    
                </tr>
                </table>
                
                </fieldset>
            </div>
        </asp:View>
        
    </asp:MultiView>

</ContentTemplate>
</asp:UpdatePanel>