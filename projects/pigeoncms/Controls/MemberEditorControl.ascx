<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MemberEditorControl.ascx.cs" Inherits="Controls_MemberEditorControl" %>

<asp:Label ID="LblErr" runat="server" Text="" CssClass="error"></asp:Label>
<asp:Label ID="LblOk" runat="server" Text="" CssClass="success"></asp:Label>

<asp:Panel runat="server" ID="PanelInsert">

    <div class="form-group col-md-6">
        <label><%=LitUsername %></label>
        <asp:TextBox ID="TxtInsUserName" MaxLength="255" runat="server" CssClass="form-control"></asp:TextBox>
        <span class="member-new-user-suffix"><%=base.NewUserSuffix %></span>
    </div>

    <div class="form-group col-md-6">
        <label><%=LitEmail %></label>
        <asp:TextBox ID="TxtInsEmail" MaxLength="255" runat="server" CssClass="form-control"></asp:TextBox>
    </div>

    <div class="form-group col-md-6">
        <label><%=LitPassword %></label>
        <asp:TextBox ID="TxtInsPassword" MaxLength="255" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
    </div>

    <div class="form-group col-md-6">
        <label><%=LitPasswordControl %></label>
        <asp:TextBox ID="TxtInsPasswordControl" MaxLength="255" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
    </div>

	<div class="form-group col-lg-3 col-md-6 checkbox-container">
        <asp:CheckBox ID="ChkInsEnabled" runat="server" Visible="false" Checked="true" />
		<asp:Label runat="server" AssociatedControlID="ChkInsEnabled"><%=LitEnabled %></asp:Label>
	</div>

	<div class="form-group col-lg-3 col-md-6 checkbox-container">
        <asp:CheckBox ID="ChkInsAllowMessages" runat="server" Visible="false" Checked="false" />
		<asp:Label runat="server" AssociatedControlID="ChkInsAllowMessages"><%=LitAllowMessages %></asp:Label>
	</div>

	<div class="form-group col-lg-3 col-md-6 checkbox-container">
        <asp:CheckBox ID="ChkInsAllowEmails" runat="server" Visible="false" Checked="false" />
		<asp:Label runat="server" AssociatedControlID="ChkInsAllowEmails"><%=LitAllowEmails %></asp:Label>
	</div>

    <div class="form-group col-md-6">
        <label><%=LitCompanyName %></label>
        <asp:TextBox ID="TxtInsCompanyName" Visible="false" MaxLength="255" runat="server" CssClass="form-control"></asp:TextBox>
    </div>

    <div class="form-group col-md-6">
        <label><%=LitFirstName %></label>
        <asp:TextBox ID="TxtInsFirstName" Visible="false" MaxLength="255" runat="server" CssClass="form-control"></asp:TextBox>
    </div>

    <div class="form-group col-md-6">
        <label><%=LitSecondName %></label>
        <asp:TextBox ID="TxtInsSecondName" Visible="false" MaxLength="255" runat="server" CssClass="form-control"></asp:TextBox>
    </div>

    <div class="form-group col-md-6">
        <label><%=LitVat %></label>
        <asp:TextBox ID="TxtInsVat" Visible="false" MaxLength="255" runat="server" CssClass="form-control"></asp:TextBox>
    </div>

	<div class="form-group col-md-6">
        <label><%=LitSsn %></label>
        <asp:TextBox ID="TxtInsSsn" MaxLength="255" Visible="false" runat="server" CssClass="form-control"></asp:TextBox>
	</div>

	<div class="form-group col-md-6">
        <label><%=LitAddress1 %></label>
        <asp:TextBox ID="TxtInsAddress1" MaxLength="255" Visible="false" runat="server" CssClass="form-control"></asp:TextBox>
    </div>

    <div class="form-group col-md-6">
        <label><%=LitAddress2 %></label>
        <asp:TextBox ID="TxtInsAddress2" MaxLength="255" Visible="false" runat="server" CssClass="form-control"></asp:TextBox>
    </div>

    <div class="form-group col-md-6">
        <label><%=LitCity %></label>
        <asp:TextBox ID="TxtInsCity" MaxLength="255" Visible="false" runat="server" CssClass="form-control"></asp:TextBox>
    </div>

    <div class="form-group col-md-6">
        <label><%=LitState %></label>
        <asp:TextBox ID="TxtInsState" MaxLength="255" Visible="false" runat="server" CssClass="form-control"></asp:TextBox>
    </div>

    <div class="form-group col-md-6">
        <label><%=LitZipCode %></label>
        <asp:TextBox ID="TxtInsZipCode" MaxLength="255" Visible="false" runat="server" CssClass="form-control"></asp:TextBox>
    </div>

    <div class="form-group col-md-6">
        <label><%=LitNation %></label>
        <asp:DropDownList ID="DropInsNation" runat="server" Visible="false" CssClass="form-control"></asp:DropDownList>
    </div>

    <div class="form-group col-md-6">
        <label><%=LitTel1 %></label>
        <asp:TextBox ID="TxtInsTel1" MaxLength="255" Visible="false" runat="server" CssClass="form-control"></asp:TextBox>
    </div>

    <div class="form-group col-md-6">
        <label><%=LitMobile1 %></label>
        <asp:TextBox ID="TxtInsMobile1" MaxLength="255" Visible="false" runat="server" CssClass="form-control"></asp:TextBox>
    </div>

    <div class="form-group col-md-6">
        <label><%=LitWebsite1 %></label>
        <asp:TextBox ID="TxtInsWebsite1" MaxLength="255" Visible="false" runat="server" CssClass="form-control"></asp:TextBox>
    </div>

</asp:Panel>


<asp:Panel runat="server" ID="PanelUpdate">
    <div class="form-group col-md-12">
        <label><%=LitUsername %></label>
        <asp:TextBox ID="TextUpdUserName" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
    </div>

	<div class="form-group col-md-6 checkbox-container">
        <asp:CheckBox ID="ChkUpdEnabled" runat="server" Visible="false" />
		<asp:Label runat="server" AssociatedControlID="ChkUpdEnabled"><%=LitEnabled %></asp:Label>
	</div>

	<div class="form-group col-md-6 checkbox-container">
        <asp:CheckBox ID="ChkUpdApproved" runat="server" Visible="false" />
		<asp:Label runat="server" AssociatedControlID="ChkUpdEnabled"><%=LitApproved %></asp:Label>
	</div>

	<div class="form-group col-md-6 checkbox-container">
        <asp:CheckBox ID="ChkUpdAllowMessages" runat="server" Visible="false" />
		<asp:Label runat="server" AssociatedControlID="ChkUpdAllowMessages"><%=LitAllowMessages %></asp:Label>
	</div>

	<div class="form-group col-md-6 checkbox-container">
        <asp:CheckBox ID="ChkUpdAllowEmails" runat="server" Visible="false" />
		<asp:Label runat="server" AssociatedControlID="ChkUpdAllowEmails"><%=LitAllowEmails %></asp:Label>
	</div>

    <div class="form-group col-md-6">
        <label><%=LitEmail %></label>
        <asp:TextBox ID="TxtUpdEmail" MaxLength="255" runat="server" CssClass="form-control"></asp:TextBox>
    </div>

    <div class="form-group col-md-6">
        <label><%=LitComment %></label>
        <asp:TextBox ID="TxtUpdComment" MaxLength="255" runat="server" Visible="false" CssClass="form-control"></asp:TextBox>
    </div>

    <div class="form-group col-md-6">
        <label><%=LitAccessCode %></label>
        <asp:TextBox ID="TxtUpdAccessCode" CssClass="form-control" Visible="false" runat="server"></asp:TextBox>
    </div>

    <div class="form-group col-md-6">
        <label><%=LitAccessLevel %></label>
        <asp:TextBox ID="TxtUpdAccessLevel" CssClass="form-control" Visible="false" runat="server"></asp:TextBox>
    </div>

    <div class="form-group col-md-6">
        <label><%=LitCompanyName %></label>
        <asp:TextBox ID="TxtUpdCompanyName" MaxLength="255" Visible="false" runat="server" CssClass="form-control"></asp:TextBox>
    </div>

    <div class="form-group col-md-6">
        <label><%=LitFirstName %></label>
        <asp:TextBox ID="TxtUpdFirstName" MaxLength="255" Visible="false" runat="server" CssClass="form-control"></asp:TextBox>
    </div>

    <div class="form-group col-md-6">
        <label><%=LitSecondName %></label>
        <asp:TextBox ID="TxtUpdSecondName" MaxLength="255" Visible="false" runat="server" CssClass="form-control"></asp:TextBox>
    </div>

    <div class="form-group col-md-6">
        <label><%=LitVat %></label>
        <asp:TextBox ID="TxtUpdVat" MaxLength="255" Visible="false" runat="server" CssClass="form-control"></asp:TextBox>
    </div>

    <div class="form-group col-md-6">
        <label><%=LitSsn %></label>
        <asp:TextBox ID="TxtUpdSsn" MaxLength="255" Visible="false" runat="server" CssClass="form-control"></asp:TextBox>
    </div>

    <div class="form-group col-md-6">
        <label><%=LitAddress1 %></label>
        <asp:TextBox ID="TxtUpdAddress1" MaxLength="255" Visible="false" runat="server" CssClass="form-control"></asp:TextBox>
    </div>

    <div class="form-group col-md-6">
        <label><%=LitAddress2 %></label>
        <asp:TextBox ID="TxtUpdAddress2" MaxLength="255" Visible="false" runat="server" CssClass="form-control"></asp:TextBox>
    </div>

    <div class="form-group col-md-6">
        <label><%=LitCity %></label>
        <asp:TextBox ID="TxtUpdCity" MaxLength="255" Visible="false" runat="server" CssClass="form-control"></asp:TextBox>
    </div>

    <div class="form-group col-md-6">
        <label><%=LitState %></label>
        <asp:TextBox ID="TxtUpdState" MaxLength="255" Visible="false" runat="server" CssClass="form-control"></asp:TextBox>
    </div>

    <div class="form-group col-md-6">
        <label><%=LitZipCode %></label>
        <asp:TextBox ID="TxtUpdZipCode" MaxLength="255" Visible="false" runat="server" CssClass="form-control"></asp:TextBox>
    </div>

    <div class="form-group col-md-6">
        <label><%=LitNation %></label>
        <asp:DropDownList ID="DropUpdNation" runat="server" Visible="false" CssClass="form-control"></asp:DropDownList>
    </div>

    <div class="form-group col-md-6">
        <label><%=LitTel1 %></label>
        <asp:TextBox ID="TxtUpdTel1" MaxLength="255" Visible="false" runat="server" CssClass="form-control"></asp:TextBox>
    </div>

    <div class="form-group col-md-6">
        <label><%=LitMobile1 %></label>
        <asp:TextBox ID="TxtUpdMobile1" MaxLength="255" Visible="false" runat="server" CssClass="form-control"></asp:TextBox>
    </div>

    <div class="form-group col-md-6">
        <label><%=LitWebsite1 %></label>
         <asp:TextBox ID="TxtUpdWebsite1" MaxLength="255" Visible="false" runat="server" CssClass="form-control"></asp:TextBox>
    </div>
                
</asp:Panel>


<asp:Panel runat="server" ID="PanelChangePassword">

    <div class="form-group col-md-6">
        <%=base.GetLabel("LblUsername", "Username", TxtPwdUsername)%>
        <asp:TextBox ID="TxtPwdUsername" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
    </div>

    <div class="form-group col-md-6">
        <%=base.GetLabel("LblOldPassword", "Old password", TxtPwdOldPassword)%>
        <asp:TextBox ID="TxtPwdOldPassword" MaxLength="255" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
    </div>

    <div class="form-group col-md-6">
        <%=base.GetLabel("LblPassword", "Password", TxtPwdPassword)%>
        <asp:TextBox ID="TxtPwdPassword" MaxLength="255" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
    </div>

    <div class="form-group col-md-6">
        <%=base.GetLabel("LblPasswordControl", "Repeat password", TxtPwdPasswordControl)%>
        <asp:TextBox ID="TxtPwdPasswordControl" MaxLength="255" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
    </div>
    
</asp:Panel>


<asp:Panel runat="server" ID="PanelRoles">

    <div class="form-group col-md-12">
        <%=base.GetLabel("LblUsername", "Username", TxtRolesUsername)%>
        <asp:TextBox ID="TxtRolesUsername" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
    </div>

	<div class="form-group col-md-6 align-center">

		<label><%=base.GetLabel("LblRoles", "Roles")%></label>

        <asp:ListBox ID="ListRolesNotInUser" SelectionMode="Multiple" Height="250" Rows="10" 
            CssClass="form-control" runat="server">
        </asp:ListBox>

		<br />
		<br />
		<input type="button" id="BtnAddRole" onclick="addRole();" value=">>>" class="btn btn-primary btn-xs btn-modern" />
	</div>

	<div class="form-group col-md-6 align-center">

		<label><%=base.GetLabel("LblRolesInUser", "User roles")%></label>

        <asp:ListBox ID="ListRolesInUser" SelectionMode="Multiple" Height="250" Rows="10" 
            CssClass="form-control adminMediumText" runat="server">
        </asp:ListBox>
        <asp:HiddenField ID="HiddenRolesInUser" runat="server" />

		<br />
		<br />
        <input type="button" id="BtnRemoveRole" onclick="removeRole();" value="<<<" class="btn btn-primary btn-xs btn-modern" />
	</div>


</asp:Panel>