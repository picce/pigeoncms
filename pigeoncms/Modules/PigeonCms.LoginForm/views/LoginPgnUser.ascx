<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LoginPgnUser.ascx.cs" Inherits="Controls_LoginPgnUser" %>

<div class='moduleBody <%=base.BaseModule.CssClass %>'>
<ul class='bar <%=base.BaseModule.CssClass %>'>
<li>
    <div style="width:60px; display:inline-table">
        <%=base.GetLabel("user", "user", TxtUser)%>
    </div>
    <asp:TextBox id="TxtUser" ValidationGroup="AdminArea" runat="server" Width="100px"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredUser" runat="server" ControlToValidate="TxtUser"
    ValidationGroup="AdminArea" ErrorMessage="Inserire il nome utente">*</asp:RequiredFieldValidator>
</li>
<li>
    <div style="width:60px; display:inline-table">
        <%=base.GetLabel("password", "password", TxtPassword)%>
    </div>
    <asp:TextBox id="TxtPassword" TextMode="Password" ValidationGroup="AdminArea" runat="server" Width="100px"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredPassword" runat="server" ControlToValidate="TxtPassword"
    ValidationGroup="AdminArea" ErrorMessage="Inserire la password">*</asp:RequiredFieldValidator>
</li>
<li>
    <asp:Button ID="CmdLogin" ValidationGroup="AdminArea" runat="server" CssClass="button" OnClick="CmdLogin_Click" Text="OK" />
    <span class="error"><%=LblErrore %></span>
</li>
</ul>
</div>