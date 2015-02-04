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
    
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0" OnActiveViewChanged="MultiView1_ActiveViewChanged">
    
        <asp:View ID="ViewSee" runat="server">
            <div class="adminToolbar">
                <asp:Button ID="BtnNew" runat="server" Text="Nuovo" CssClass="button" OnClick="BtnNew_Click" />
            </div>
            <br />

            <asp:GridView ID="Grid1" runat="server" Width="100%" AllowPaging="True" AllowSorting="true" AutoGenerateColumns="False"
                DataSourceID="ObjDs1" DataKeyNames="Id" OnRowCommand="Grid1_RowCommand" OnRowCreated="Grid1_RowCreated" OnRowDataBound="Grid1_RowDataBound">
                <Columns>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="15">
                        <ItemTemplate>
                            <asp:ImageButton ID="LnkSel" CommandName="Select" CommandArgument='<%#Eval("Id") %>' 
                                runat="server" SkinID="ImgEditFile" />      
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="MenuType" HeaderText="MenuType" SortExpression="MenuType" />
                    <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                    <asp:BoundField DataField="Id" HeaderText="Id" />
                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:ImageButton ID="LnkDel" CommandName="DeleteRow" CommandArgument='<%#Eval("Id") %>' runat="server" 
                                SkinID="ImgDelFile" OnClientClick="return confirm('Cancellare la riga?');"  />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            
            <asp:ObjectDataSource ID="ObjDs1" runat="server" SortParameterName="sort" OnSelecting="ObjDs1_Selecting"
                SelectMethod="GetByFilter" TypeName="PigeonCms.MenutypesManager">
                <SelectParameters>
                    <asp:Parameter Name="filter" Type="object" />
                    <asp:Parameter Name="sort" Type="String" DefaultValue="Nome" />
                </SelectParameters>
                <DeleteParameters>
                    <asp:Parameter Name="Id" Type="Int32" />
                </DeleteParameters>
            </asp:ObjectDataSource>
        </asp:View>
   

        <asp:View ID="ViewInsert" runat="server">
            <div class="divAdminInsertPanel">

                <div class="adminToolbar">                
                    <asp:Button ID="BtnSave" runat="server" Text="<%$ Resources:PublicLabels, CmdSave %>" CssClass="button" OnClick="BtnSave_Click" />
                    <asp:Button ID="BtnCancel" runat="server" Text="<%$ Resources:PublicLabels, CmdCancel %>" CssClass="button" OnClick="BtnCancel_Click" />
                </div>
                
                <table cellspacing="0" cellpadding="5">
                <tr>
                    <td>Nome</td>
                    <td colspan="3">
                        <asp:TextBox ID="TxtMenuType" MaxLength="50" runat="server" CssClass="adminSmallText"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Titolo</td>
                    <td colspan="3">
                        <asp:TextBox ID="TxtTitle" MaxLength="200" runat="server" CssClass="adminText"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Descrizione</td>
                    <td colspan="3">
                        <asp:TextBox ID="TxtDescription" MaxLength="200" runat="server" CssClass="adminText"></asp:TextBox></td>
                </tr>
                </table>
            </div>            
        </asp:View>
    </asp:MultiView>

</ContentTemplate>
</asp:UpdatePanel>