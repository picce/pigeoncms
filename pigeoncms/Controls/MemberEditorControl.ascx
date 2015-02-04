<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MemberEditorControl.ascx.cs" Inherits="Controls_MemberEditorControl" %>

<asp:Label ID="LblErr" runat="server" Text="" CssClass="error"></asp:Label>
<asp:Label ID="LblOk" runat="server" Text="" CssClass="success"></asp:Label>

<asp:Panel runat="server" ID="PanelInsert">
    <table class='<%=BaseModule.CssClass %>'>
    <tr>
        <td class="key"><%=LitUsername %></td>
        <td>
            <asp:TextBox ID="TxtInsUserName" MaxLength="255" runat="server" CssClass="adminMediumText mandatory"></asp:TextBox>
            <span class="member-new-user-suffix"><%=base.NewUserSuffix %></span>
        </td>
    </tr>
    <tr>
        <td class="key"><%=LitPassword %></td>
        <td>
            <asp:TextBox ID="TxtInsPassword" MaxLength="255" runat="server" CssClass="adminMediumText mandatory" TextMode="Password"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="key"><%=LitPasswordControl %></td>
        <td>
            <asp:TextBox ID="TxtInsPasswordControl" MaxLength="255" runat="server" CssClass="adminMediumText mandatory" TextMode="Password"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="key"><%=LitEmail %></td>
        <td>
            <asp:TextBox ID="TxtInsEmail" MaxLength="255" runat="server" CssClass="adminMediumText mandatory"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="key"><%=LitEnabled %></td>
        <td>
            <asp:CheckBox ID="ChkInsEnabled" runat="server" Visible="false" Checked="true" />
        </td>
    </tr>
    <tr>
        <td class="key"><%=LitAllowMessages %></td>
        <td>
            <asp:CheckBox ID="ChkInsAllowMessages" runat="server" Visible="false" Checked="false" />
        </td>
    </tr>
    <tr>
        <td class="key"><%=LitAllowEmails %></td>
        <td>
            <asp:CheckBox ID="ChkInsAllowEmails" runat="server" Visible="false" Checked="false" />
        </td>
    </tr>

    <tr>
        <td class="key"><%=LitCompanyName %></td>
        <td>
            <asp:TextBox ID="TxtInsCompanyName" MaxLength="255" Visible="false" runat="server" CssClass="adminMediumText"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="key"><%=LitFirstName %></td>
        <td>
            <asp:TextBox ID="TxtInsFirstName" MaxLength="255" Visible="false" runat="server" CssClass="adminMediumText"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="key"><%=LitSecondName %></td>
        <td>
            <asp:TextBox ID="TxtInsSecondName" MaxLength="255" Visible="false" runat="server" CssClass="adminMediumText"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="key"><%=LitVat %></td>
        <td>
            <asp:TextBox ID="TxtInsVat" MaxLength="255" Visible="false" runat="server" CssClass="adminMediumText"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="key"><%=LitSsn %></td>
        <td>
            <asp:TextBox ID="TxtInsSsn" MaxLength="255" Visible="false" runat="server" CssClass="adminMediumText"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="key"><%=LitAddress1 %></td>
        <td>
            <asp:TextBox ID="TxtInsAddress1" MaxLength="255" Visible="false" runat="server" CssClass="adminMediumText"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="key"><%=LitAddress2 %></td>
        <td>
            <asp:TextBox ID="TxtInsAddress2" MaxLength="255" Visible="false" runat="server" CssClass="adminMediumText"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="key"><%=LitCity %></td>
        <td>
            <asp:TextBox ID="TxtInsCity" MaxLength="255" Visible="false" runat="server" CssClass="adminMediumText"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="key"><%=LitState %></td>
        <td>
            <asp:TextBox ID="TxtInsState" MaxLength="255" Visible="false" runat="server" CssClass="adminMediumText"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="key"><%=LitZipCode %></td>
        <td>
            <asp:TextBox ID="TxtInsZipCode" MaxLength="255" Visible="false" runat="server" CssClass="adminMediumText"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="key"><%=LitNation %></td>
        <td>
            <asp:DropDownList ID="DropInsNation" runat="server" Visible="false" CssClass="adminMediumText"></asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="key"><%=LitTel1 %></td>
        <td>
            <asp:TextBox ID="TxtInsTel1" MaxLength="255" Visible="false" runat="server" CssClass="adminMediumText"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="key"><%=LitMobile1 %></td>
        <td>
            <asp:TextBox ID="TxtInsMobile1" MaxLength="255" Visible="false" runat="server" CssClass="adminMediumText"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="key"><%=LitWebsite1 %></td>
        <td>
            <asp:TextBox ID="TxtInsWebsite1" MaxLength="255" Visible="false" runat="server" CssClass="adminMediumText"></asp:TextBox>
        </td>
    </tr>
    </table>
</asp:Panel>


<asp:Panel runat="server" ID="PanelUpdate">
    <table class='<%=BaseModule.CssClass %>'>
    <tr>
        <td class="key"><%=LitUsername %></td>
        <td>
            <asp:Literal ID="LitUpdUserName" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td class="key"><%=LitEnabled %></td>
        <td>
            <asp:CheckBox ID="ChkUpdEnabled" runat="server" Visible="false" />
        </td>
    </tr>
    <tr>
        <td class="key"><%=LitAllowMessages %></td>
        <td>
            <asp:CheckBox ID="ChkUpdAllowMessages" runat="server" Visible="false" />
        </td>
    </tr>
    <tr>
        <td class="key"><%=LitAllowEmails %></td>
        <td>
            <asp:CheckBox ID="ChkUpdAllowEmails" runat="server" Visible="false" />
        </td>
    </tr>
    <tr>
        <td class="key"><%=LitEmail %></td>
        <td>
            <asp:TextBox ID="TxtUpdEmail" MaxLength="255" runat="server" CssClass="adminMediumText"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="key"><%=LitComment %></td>
        <td>
            <asp:TextBox ID="TxtUpdComment" MaxLength="255" runat="server" Visible="false" CssClass="adminMediumText"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="key"><%=LitAccessCode %></td>
        <td>
            <asp:TextBox ID="TxtUpdAccessCode" CssClass="adminMediumText" Visible="false" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="key"><%=LitAccessLevel %></td>
        <td>
            <asp:TextBox ID="TxtUpdAccessLevel" CssClass="adminMediumText" Visible="false" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="key"><%=LitCompanyName %></td>
        <td>
            <asp:TextBox ID="TxtUpdCompanyName" MaxLength="255" Visible="false" runat="server" CssClass="adminMediumText"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="key"><%=LitFirstName %></td>
        <td>
            <asp:TextBox ID="TxtUpdFirstName" MaxLength="255" Visible="false" runat="server" CssClass="adminMediumText"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="key"><%=LitSecondName %></td>
        <td>
            <asp:TextBox ID="TxtUpdSecondName" MaxLength="255" Visible="false" runat="server" CssClass="adminMediumText"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="key"><%=LitVat %></td>
        <td>
            <asp:TextBox ID="TxtUpdVat" MaxLength="255" Visible="false" runat="server" CssClass="adminMediumText"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="key"><%=LitSsn %></td>
        <td>
            <asp:TextBox ID="TxtUpdSsn" MaxLength="255" Visible="false" runat="server" CssClass="adminMediumText"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="key"><%=LitAddress1 %></td>
        <td>
            <asp:TextBox ID="TxtUpdAddress1" MaxLength="255" Visible="false" runat="server" CssClass="adminMediumText"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="key"><%=LitAddress2 %></td>
        <td>
            <asp:TextBox ID="TxtUpdAddress2" MaxLength="255" Visible="false" runat="server" CssClass="adminMediumText"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="key"><%=LitCity %></td>
        <td>
            <asp:TextBox ID="TxtUpdCity" MaxLength="255" Visible="false" runat="server" CssClass="adminMediumText"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="key"><%=LitState %></td>
        <td>
            <asp:TextBox ID="TxtUpdState" MaxLength="255" Visible="false" runat="server" CssClass="adminMediumText"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="key"><%=LitZipCode %></td>
        <td>
            <asp:TextBox ID="TxtUpdZipCode" MaxLength="255" Visible="false" runat="server" CssClass="adminMediumText"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="key"><%=LitNation %></td>
        <td>
            <asp:DropDownList ID="DropUpdNation" runat="server" Visible="false" CssClass="adminMediumText"></asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="key"><%=LitTel1 %></td>
        <td>
            <asp:TextBox ID="TxtUpdTel1" MaxLength="255" Visible="false" runat="server" CssClass="adminMediumText"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="key"><%=LitMobile1 %></td>
        <td>
            <asp:TextBox ID="TxtUpdMobile1" MaxLength="255" Visible="false" runat="server" CssClass="adminMediumText"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="key"><%=LitWebsite1 %></td>
        <td>
            <asp:TextBox ID="TxtUpdWebsite1" MaxLength="255" Visible="false" runat="server" CssClass="adminMediumText"></asp:TextBox>
        </td>
    </tr>
                
    </table>
</asp:Panel>


<asp:Panel runat="server" ID="PanelChangePassword">
    <table class='<%=BaseModule.CssClass %>'>
    <tr>
        <td class="key"><%=base.GetLabel("LblUsername", "Username")%></td>
        <td>
            <asp:Literal ID="LitPwdUsername" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td class="key"><%=base.GetLabel("LblOldPassword", "Old password", TxtPwdOldPassword)%></td>
        <td>
            <asp:TextBox ID="TxtPwdOldPassword" MaxLength="255" runat="server" CssClass="adminMediumText" TextMode="Password"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="key"><%=base.GetLabel("LblPassword", "Password", TxtPwdPassword)%></td>
        <td>
            <asp:TextBox ID="TxtPwdPassword" MaxLength="255" runat="server" CssClass="adminMediumText" TextMode="Password"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="key"><%=base.GetLabel("LblPasswordControl", "Repeat password", TxtPwdPasswordControl)%></td>
        <td>
            <asp:TextBox ID="TxtPwdPasswordControl" MaxLength="255" runat="server" CssClass="adminMediumText" TextMode="Password"></asp:TextBox>
        </td>
    </tr>
    </table>
</asp:Panel>


<asp:Panel runat="server" ID="PanelRoles">
    <table class='<%=BaseModule.CssClass %>'>
    <tr>
        <td class="key"><%=base.GetLabel("LblRoles", "Roles")%></td>
        <td>
            <asp:ListBox ID="ListRolesNotInUser" SelectionMode="Multiple" Rows="10" 
                CssClass="adminMediumText" runat="server">
            </asp:ListBox>
        </td>
        <td style="vertical-align:middle;">
            <input type="button" id="BtnAddRole" onclick="addRole();" value=">>>" class="button" />
            <br /><br />
            <input type="button" id="BtnRemoveRole" onclick="removeRole();" value="<<<" class="button" />
        </td>
        <td class="key"><%=base.GetLabel("LblRolesInUser", "User roles")%></td>
        <td>
            <asp:ListBox ID="ListRolesInUser" SelectionMode="Multiple" Rows="10" 
                CssClass="adminMediumText" runat="server">
            </asp:ListBox>
            <asp:HiddenField ID="HiddenRolesInUser" runat="server" />
        </td>
    </tr>
    </table>
</asp:Panel>