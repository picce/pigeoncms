<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Default.ascx.cs" Inherits="Controls_Default" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register src="~/Controls/MemberEditorControl.ascx" tagname="MemberEditor" tagprefix="uc1" %>

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

    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0" OnActiveViewChanged="MultiView1_ActiveViewChanged">
    
        <asp:View ID="ViewSee" runat="server">
            <div class="adminToolbar">
                <asp:Button ID="BtnNew" runat="server" Text="<%$ Resources:PublicLabels, CmdNew %>" CssClass="button" OnClick="BtnNew_Click" />
            </div>
            <fieldset class="adminFilters">
                <%=base.GetLabel("LblFilters", "Filters")%>&nbsp;
                <asp:TextBox ID="TxtUserNameFilter" runat="server" AutoPostBack="true" CssClass="adminMediumText" 
                    ontextchanged="TxtUserNameFilter_TextChanged"></asp:TextBox>
                <cc1:TextBoxWatermarkExtender TargetControlID="TxtUserNameFilter" runat="server" 
                    WatermarkText="<username>" WatermarkCssClass="adminMediumText watermark">
                </cc1:TextBoxWatermarkExtender>
            </fieldset>
            <br />       
            <asp:GridView ID="Grid1" runat="server" Width="100%" AllowPaging="True" 
                AllowSorting="false" AutoGenerateColumns="False"
                DataKeyNames="UserName" OnRowCommand="Grid1_RowCommand" 
                OnRowCreated="Grid1_RowCreated" OnRowDataBound="Grid1_RowDataBound" 
                onpageindexchanging="Grid1_PageIndexChanging" onsorting="Grid1_Sorting">
                <Columns>
                    <asp:TemplateField ItemStyle-Width="10">
                        <ItemTemplate>
                        <asp:ImageButton ID="LnkSel" CommandName="Select" CommandArgument='<%#Eval("UserName") %>' 
                        runat="server" SkinID="ImgEditFile" />                
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="User" SortExpression="UserName">
                        <ItemTemplate>
                            <asp:Literal ID="LitUserName" runat="server"></asp:Literal>
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
                        <asp:ImageButton ID="LnkRoles" CommandName="Roles" CommandArgument='<%#Eval("UserName") %>' 
                        runat="server" SkinID="ImgAttributes" ToolTip="roles" />                
                        </ItemTemplate>
                    </asp:TemplateField>                    
                    
                    <asp:TemplateField ItemStyle-Width="10">
                        <ItemTemplate>
                        <asp:ImageButton ID="LnkPwd" CommandName="Password" CommandArgument='<%#Eval("UserName") %>' 
                        runat="server" SkinID="ImgKey" ToolTip="password" />                
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Abilitato">
                        <ItemTemplate>
                        <asp:CheckBox ID="ChkEnabled" Enabled="false" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Core" SortExpression="IsCore" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                        <asp:CheckBox ID="ChkIsCore" Enabled="false" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="10">
                        <ItemTemplate>
                        <asp:ImageButton ID="LnkDel" CommandName="DeleteRow" CommandArgument='<%#Eval("UserName") %>' runat="server" 
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
        
            <div class="col width-100">
                <fieldset class="adminForm">
                    <legend><asp:Literal runat="server" ID="LitTitle"></asp:Literal></legend>
                    <uc1:MemberEditor ID="MemberEditor1" runat="server" />                
                </fieldset>
            </div>
        </asp:View>
        
    </asp:MultiView>

</ContentTemplate>
</asp:UpdatePanel>