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
            </div>
            <br />
            <asp:GridView ID="Grid1" runat="server" AllowPaging="True" Width="100%" AutoGenerateColumns="False"
                DataSourceID="ObjDs1" DataKeyNames="Key" OnRowCommand="Grid1_RowCommand" OnRowCreated="Grid1_RowCreated" OnRowDataBound="Grid1_RowDataBound">
                <Columns>                
                    <asp:TemplateField ItemStyle-Width="10">
                        <ItemTemplate>
                        <asp:ImageButton ID="LnkSel" CommandName="Select" CommandArgument='<%#Eval("Key") %>' 
                        runat="server" SkinID="ImgEditFile" />                
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:BoundField DataField="Key" HeaderText="Key" SortExpression="Key" />
                    <asp:BoundField DataField="Value" HeaderText="Value" SortExpression="Value" />
                    
                    <asp:TemplateField HeaderText="Remove" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10">
                        <ItemTemplate>
                        <asp:ImageButton ID="LnkDel" CommandName="DeleteRow" CommandArgument='<%#Eval("Key") %>' 
                        runat="server" SkinID="ImgDelFile" 
                        OnClientClick="return confirm('Cancellare la riga?');"  />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            
            <asp:ObjectDataSource ID="ObjDs1" runat="server" TypeName="PigeonCms.WebConfigManager" 
            SelectMethod="GetByFilter" OnSelecting="ObjDs1_Selecting">
                <SelectParameters>
                    <asp:Parameter Name="filter" Type="Object" />
                </SelectParameters>
                <DeleteParameters>
                    <asp:Parameter Name="Key" Type="Int32" />
                </DeleteParameters>
            </asp:ObjectDataSource>
        </asp:View>
   

        <asp:View ID="ViewInsert" runat="server">
            <div class="adminToolbar">
                <asp:Button ID="BtnSave" runat="server" Text="<%$ Resources:PublicLabels, CmdSave %>" CssClass="button" OnClientClick="MyObject.UpdateEditorFormValue();" OnClick="BtnSave_Click" />
                <asp:Button ID="BtnCancel" runat="server" Text="<%$ Resources:PublicLabels, CmdCancel %>" CssClass="button" OnClick="BtnCancel_Click" />
            </div>
            
            <div class="col width-100">
                <fieldset class="adminForm">
                    <legend></legend>
                    <table class="adminTable">
                    <tr>
                        <td class="key">Key</td>
                        <td>
                            <asp:TextBox ID="TxtKey" MaxLength="100" runat="server" Enabled="true" CssClass="adminText"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="key">Value</td>
                        <td>
                            <asp:TextBox ID="TxtValue" MaxLength="100" runat="server" Enabled="true" CssClass="adminText"></asp:TextBox>
                        </td>
                    </tr>
                    </table>
                </fieldset>
            </div>
        </asp:View>
    </asp:MultiView>

</ContentTemplate>
</asp:UpdatePanel>