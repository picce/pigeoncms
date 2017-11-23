<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Logmebot.Login.ascx.cs" Inherits="Controls_Logmebot_Login" %>

<div class="login-modern-container">

    <div class="login-modern login-wrapper <%=base.BaseModule.CssClass %>">

    <div class="login-logo--svg">
        <img alt="logo" src="/pgn-admin/masterpages/PigeonModern/img/logo.svg" />
    </div>

    <div class="login-panel">

        <div class="panel-body">

            <fieldset>

                <asp:Button ID="CmdOauthLogmebot" runat="server"
                    Visible="false"
                    CssClass="btn btn-lg btn-success btn-block btn-modern" 
                    OnClick="CmdOauthLogmebot_Click" Text="Login with LogMeBot" />                


                <div class="form-group checkbox-container">
                    <asp:CheckBox runat="server" ID="ChkRememberMe" Text="" ClientIDMode="Static" />
                    <label for="ChkRememberMe" class="ck-bordered">
                        <asp:Literal runat="server" ID="LitRememberMe"></asp:Literal>
                    </label>
                </div>

            </fieldset>

        </div>
    </div>

    <%=LblErr %>

</div>

</div>