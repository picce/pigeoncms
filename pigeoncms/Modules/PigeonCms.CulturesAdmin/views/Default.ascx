<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Default.ascx.cs" Inherits="Controls_Default" %>

<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="1" AssociatedUpdatePanelID="Upd1">
    <ProgressTemplate>
        <div class="loading"><%=PigeonCms.Utility.GetLabel("LblLoading", "loading") %></div>
    </ProgressTemplate>
</asp:UpdateProgress>

<asp:UpdatePanel ID="Upd1" runat="server">
<ContentTemplate>
    <asp:Label ID="LblErr" runat="server" Text="" CssClass="error"></asp:Label>
    <asp:Label ID="LblOk" runat="server" Text="" CssClass="success"></asp:Label>

    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">

        <asp:View ID="ViewSee" runat="server">
            <div class="adminToolbar">
                <asp:Button ID="BtnNew" runat="server" Text="Nuovo" CssClass="button" OnClick="BtnNew_Click" />
                <asp:Button ID="BtnApplySettings" runat="server" Text="Applica" CssClass="button" OnClick="BtnApply_Click" />
            </div>
            <fieldset title="Filtri">
                Enabled 
                    <asp:DropDownList runat="server" ID="DropEnabledFilter" AutoPostBack="true" CssClass="adminShortText" OnSelectedIndexChanged="DropEnabledFilter_SelectedIndexChanged">
                        <asp:ListItem Value="">Entrambi</asp:ListItem>
                        <asp:ListItem Value="1">On-line</asp:ListItem>
                        <asp:ListItem Value="0">Off-line</asp:ListItem>
                    </asp:DropDownList>&nbsp;&nbsp;&nbsp;
            </fieldset>
            <br />
            <asp:GridView ID="Grid1" runat="server" AllowPaging="True" AllowSorting="false" Width="100%" AutoGenerateColumns="False"
                DataSourceID="ObjDs1" DataKeyNames="CultureCode" OnRowCommand="Grid1_RowCommand" OnRowCreated="Grid1_RowCreated" OnRowDataBound="Grid1_RowDataBound">
                <Columns>
                       
                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10">
                        <ItemTemplate>
                            <asp:ImageButton ID="ImgMoveUp" CommandName="MoveUp" CommandArgument='<%#Eval("CultureCode") %>'
                            SkinID="ImgSortAsc" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10">
                        <ItemTemplate>
                            <asp:ImageButton ID="ImgMoveDown" CommandName="MoveDown" CommandArgument='<%#Eval("CultureCode") %>'
                            SkinID="ImgSortDesc" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField> 
                                  
                    <asp:TemplateField ItemStyle-Width="10">
                        <ItemTemplate>
                        <asp:ImageButton ID="LnkSel" CommandName="Select" CommandArgument='<%#Eval("CultureCode") %>' 
                        runat="server" SkinID="ImgEditFile" />                
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:BoundField DataField="CultureCode" HeaderText="CultureCode" SortExpression="CultureCode" />
                    <asp:BoundField DataField="DisplayName" HeaderText="DisplayName" SortExpression="DisplayName" />
                    <asp:BoundField DataField="Ordering" HeaderText="Ordine" SortExpression="Ordering"
                        ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                    <asp:TemplateField HeaderText="Abilitato" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:ImageButton runat="server" ID="ImgEnabledOk" CommandName="ImgEnabledOk" CommandArgument='<%#Eval("CultureCode") %>' SkinID="ImgOk" Visible="false" />
                            <asp:ImageButton runat="server" ID="ImgEnabledKo" CommandName="ImgEnabledKo" CommandArgument='<%#Eval("CultureCode") %>' SkinID="ImgUnchecked" Visible="false" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Cancella" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10">
                        <ItemTemplate>
                        <asp:ImageButton ID="LnkDel" CommandName="DeleteRow" CommandArgument='<%#Eval("CultureCode") %>' 
                        runat="server" SkinID="ImgDelFile" 
                        OnClientClick="return confirm('Cancellare la riga?');"  />
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                </Columns>
            </asp:GridView>
            
            <asp:ObjectDataSource ID="ObjDs1" runat="server" SortParameterName="sort"
                SelectMethod="GetByFilter" TypeName="PigeonCms.CulturesManager" OnSelecting="ObjDs1_Selecting">
                <SelectParameters>
                    <asp:Parameter Name="filter" Type="Object" />
                    <asp:Parameter Name="sort" Type="String" DefaultValue="Ordering" />
                </SelectParameters>
                <DeleteParameters>
                    <asp:Parameter Name="CultureCode" Type="String" />
                </DeleteParameters>
            </asp:ObjectDataSource>
        </asp:View>
   

        <asp:View ID="ViewInsert" runat="server">
            <div class="adminToolbar">
                <asp:Button ID="BtnSave" runat="server" Text="<%$ Resources:PublicLabels, CmdSave %>" CssClass="button" OnClick="BtnSave_Click" />
                <asp:Button ID="BtnCancel" runat="server" Text="<%$ Resources:PublicLabels, CmdCancel %>" CssClass="button" OnClick="BtnCancel_Click" />
            </div>
            <div class="col width-100">
                <fieldset class="adminForm">
                    <legend></legend>
                    <table class="adminTable">
                    <tr>
                        <td class="key">Culture code</td>
                        <td>
                            <asp:TextBox ID="TxtCultureCode" MaxLength="10" runat="server" CssClass="adminText"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="key">Display name</td>
                        <td>
                            <asp:TextBox ID="TxtDisplayName" MaxLength="50" runat="server" CssClass="adminText"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="key">Enabled</td>
                        <td>
                            <asp:CheckBox ID="ChkEnabled" runat="server" Enabled="true" />
                        </td>
                    </tr>
                    </table>
                </fieldset>
            </div>
        </asp:View>
    </asp:MultiView>

</ContentTemplate>
</asp:UpdatePanel>