<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LoginPigeonModernAdmin.ascx.cs" Inherits="Controls_LoginPigeonModernAdmin" %>

<div class="login-modern-container">

    <div class="login-modern login-wrapper <%=base.BaseModule.CssClass %>">

    <div class="login-logo--svg">
        <img alt="logo" src="/pgn-admin/masterpages/PigeonModern/img/logo.svg" />
    </div>

    <div class="login-panel">

        <div class="panel-body">

            <fieldset>

                <div class="form-group">
                    <asp:TextBox ID="TxtUser" ValidationGroup="AdminArea" CssClass="form-control form-control--transparent" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredUser" runat="server" ControlToValidate="TxtUser"
                        ValidationGroup="AdminArea" ErrorMessage="Inserire il nome utente" CssClass="error">*</asp:RequiredFieldValidator>
                </div>


                <div class="form-group">
                    <asp:TextBox ID="TxtPassword" TextMode="Password" ValidationGroup="AdminArea" CssClass="form-control form-control--transparent" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredPassword" runat="server" ControlToValidate="TxtPassword"
                        ValidationGroup="AdminArea" ErrorMessage="Inserire la password" CssClass="error">*</asp:RequiredFieldValidator>
                </div>                

                <asp:Button ID="CmdLogin" ValidationGroup="AdminArea" runat="server"
                    CssClass="btn btn-lg btn-success btn-block btn-modern" OnClick="CmdLogin_Click" Text="Login" />                

                <div class="form-group checkbox-container">
                    <asp:CheckBox runat="server" ID="ChkRememberMe" Text="" ClientIDMode="Static" />
                    <label for="ChkRememberMe" class="ck-bordered">
                        <asp:Literal runat="server" ID="LitRememberMe"></asp:Literal>
                    </label>
                </div>

            </fieldset>

        </div>
    </div>

    <%=LblErrore %>

</div>

</div>