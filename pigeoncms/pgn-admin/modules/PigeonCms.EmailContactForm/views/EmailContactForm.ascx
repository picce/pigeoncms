<%@ Register TagPrefix="cc1" Namespace="WebControlCaptcha" Assembly="WebControlCaptcha" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EmailContactForm.ascx.cs" Inherits="Controls_EmailContactFormControl" EnableViewState="false" %>

<%=HeaderText %>
<%=base.GetLabel("LblHeader") %>
<span class="error"><%=LblErroreInfo %></span>
<span class="success"><%=LblSuccessInfo %></span>
<br />
<fieldset id="contacts" class="EmailContactForm">
<legend></legend>
<dl class="EmailContactForm">
    <dt class="EmailContactForm">
        <%=base.GetLabel("LblName", "name", TxtNomeCognome) %>
    </dt>
    <dd class="EmailContactForm">
        <asp:TextBox ID="TxtNomeCognome" runat="server" ValidationGroup="InfoArea" CssClass="EmailContactForm"
        MaxLength="100"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredNomeCognome" runat="server" ControlToValidate="TxtNomeCognome"
        ValidationGroup="InfoArea" ErrorMessage="Inserire il proprio nome e cognome">*</asp:RequiredFieldValidator>
    </dd>
    
    <dt>
        <%=base.GetLabel("LblCompanyName", "company name", TxtCompanyName) %>
    </dt>
    <dd>
        <asp:TextBox ID="TxtCompanyName" runat="server" ValidationGroup="InfoArea"
        MaxLength="100" CssClass="EmailContactForm"></asp:TextBox>
    </dd>
    
    <dt>
        <%=base.GetLabel("LblCity", "city", TxtCity) %>
    </dt>
    <dd>
        <asp:TextBox ID="TxtCity" runat="server" ValidationGroup="InfoArea"
        MaxLength="100" CssClass="EmailContactForm"></asp:TextBox>
    </dd>

    <dt>
        <%=base.GetLabel("LblInfoEmail", "e-mail", TxtEmail) %>
    </dt>
    <dd>
        <asp:TextBox ID="TxtEmail" MaxLength="100" ValidationGroup="InfoArea"
        runat="server" CssClass="EmailContactForm"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredEmail" runat="server" ControlToValidate="TxtEmail"
        ValidationGroup="InfoArea" ErrorMessage="">*</asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularEmail" runat="server" ControlToValidate="TxtEmail"
        ErrorMessage="e-mail non valida" ValidationGroup="InfoArea" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
    </dd>

    <dt>
        <%=base.GetLabel("LblPhone", "phone", TxtPhone) %>
    </dt>
    <dd>
        <asp:TextBox ID="TxtPhone" runat="server" ValidationGroup="InfoArea"
        MaxLength="100" CssClass="EmailContactForm"></asp:TextBox>
    </dd>

    <dt>
        <%=base.GetLabel("LblMessage", "message", TxtMessage) %>
    </dt>
    <dd>
        <asp:TextBox ID="TxtMessage" runat="server"  ValidationGroup="InfoArea"
        TextMode="MultiLine" CssClass="EmailContactForm"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredMessage" runat="server" ControlToValidate="TxtMessage"
        ValidationGroup="InfoArea" ErrorMessage="Inserire il messaggio">*</asp:RequiredFieldValidator>
    </dd>
</dl>
</fieldset>

<asp:Panel ID="PanelPrivacy" runat="server">
    <asp:TextBox ID="TxtShowPrivacy" runat="server" />
    <asp:CheckBox ID="ChkShowPrivacy" runat="server" Checked="false" />
    <%=base.GetLabel("LblPrivacyText", "message", ChkShowPrivacy) %>
    <asp:RequiredFieldValidator ID="RequiredShowPrivacy" runat="server" ControlToValidate="TxtShowPrivacy"
        ValidationGroup="InfoArea" ErrorMessage="" InitialValue="false">*</asp:RequiredFieldValidator>
    <br />
</asp:Panel>

<cc1:captchacontrol id="CaptchaControl1" runat="server" ValidationGroup="InfoArea"></cc1:captchacontrol>

<asp:Button ID="BtnSend" ValidationGroup="InfoArea" CssClass="button" runat="server" 
    Text="<%$ Resources:PublicLabels, CmdSend %>" onclick="BtnSend_Click" />
<br />
<%=FooterText %>