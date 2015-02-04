<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Default.ascx.cs" Inherits="Controls_Default" %>

<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="1" AssociatedUpdatePanelID="Upd1">
    <ProgressTemplate>
        <div class="loading">caricamento..</div>
    </ProgressTemplate>
</asp:UpdateProgress>
    
<asp:UpdatePanel ID="Upd1" runat="server">
<ContentTemplate>
    <asp:Label ID="LblErr" runat="server" Text="" CssClass="errore"></asp:Label>
    <asp:Label ID="LblOk" runat="server" Text="" CssClass="success"></asp:Label>

    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
    
        <asp:View ID="ViewSee" runat="server">
            <fieldset title="Filtri">
                Abilitato
                    <asp:DropDownList runat="server" ID="DropEnabledFilter" AutoPostBack="true" CssClass="adminShortText" OnSelectedIndexChanged="DropEnabledFilter_SelectedIndexChanged">
                        <asp:ListItem Value="">Entrambi</asp:ListItem>
                        <asp:ListItem Value="1">Si</asp:ListItem>
                        <asp:ListItem Value="0">No</asp:ListItem>
                    </asp:DropDownList>&nbsp;&nbsp;&nbsp;
            </fieldset>
            <br />
            <asp:GridView ID="Grid1" Width="100%" runat="server" AllowPaging="True" AllowSorting="true" AutoGenerateColumns="False"
                DataSourceID="ObjDs1" DataKeyNames="Name" OnRowCommand="Grid1_RowCommand" OnRowCreated="Grid1_RowCreated">
                <Columns>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10">
                        <ItemTemplate>
                            <asp:ImageButton ID="ImgMoveUp" CommandName="MoveUp" CommandArgument='<%#Eval("Name") %>'
                            SkinID="ImgSortAsc" runat="server" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10">
                        <ItemTemplate>
                            <asp:ImageButton ID="ImgMoveDown" CommandName="MoveDown" CommandArgument='<%#Eval("Name") %>'
                            SkinID="ImgSortDesc" runat="server" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                        <asp:ImageButton ID="LnkSel" CommandName="Select" CommandArgument='<%#Eval("Name") %>' 
                        runat="server" SkinID="ImgEditFile" />                
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Name" HeaderText="Nome" SortExpression="Name" />
                    <asp:BoundField DataField="Title" HeaderText="Titolo" SortExpression="Title" />
                    <asp:BoundField DataField="OrderId" HeaderText="Ordine" SortExpression="OrderId" />
                    <asp:TemplateField HeaderText="Abilitato" SortExpression="Enabled">
                        <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" Enabled="false" Checked='<%#Eval("Enabled") %>' runat="server" />                
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="20">
                        <ItemTemplate>
                        <asp:ImageButton ID="LnkDel" CommandName="DeleteRow" CommandArgument='<%#Eval("Name") %>' runat="server" SkinID="ImgDelFile" 
                        OnClientClick="return confirm('Cancellare la riga?');"  />
                        </ItemTemplate>
                        <HeaderStyle Width="20px" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                </Columns>
                <PagerStyle HorizontalAlign="Left" Wrap="True" />
            </asp:GridView>
            <br />
            <asp:Button ID="BtnNew" runat="server" Text="Nuovo" CssClass="button" OnClick="BtnNew_Click" />
            
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
            <asp:TextBox ID="TxtId" runat="server" Enabled="false" visible="false"></asp:TextBox>
            <table cellspacing="0" cellpadding="5">
            <tr>
                <td>Nome</td>
                <td style="width: 100%">
                    <asp:TextBox ID="TxtName" MaxLength="50" runat="server" Enabled="false" Width="255px"></asp:TextBox></td>
            </tr> 
            <tr>
                <td>Titolo</td>
                <td>
                    <asp:TextBox ID="TxtTitle" MaxLength="500" runat="server" Width="100%" Height="50px" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="listItemAlternate">Abilitato</td>
                <td class="listItemAlternate">
                    <asp:CheckBox ID="ChkEnabled" runat="server" Enabled="true" />
                </td>
            </tr>
            <tr>
                <td colspan="2" class="listItemAlternate">
                    <asp:Button ID="BtnSave" runat="server" Text="Salva" CssClass="button" OnClick="BtnSave_Click" />
                    <asp:Button ID="BtnCancel" runat="server" Text="Annulla" CssClass="button" OnClick="BtnCancel_Click" />
                </td>
            </tr>
            </table>
        </asp:View>
    </asp:MultiView>

</ContentTemplate>
</asp:UpdatePanel>