<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LoginSbAdmin.ascx.cs" Inherits="Controls_LoginSbAdmin" %>


<div class="Controls_LoginSbAdmin <%=base.BaseModule.CssClass %> col-md-4 col-md-offset-4">
    <div class="login-logo"></div>
    <div class="login-panel panel panel-default">
        <div class="panel-heading">
            <h3 class="panel-title"><%=this.SiteTitle %> <%=this.BaseModule.Title %></h3>
        </div>
        <div class="panel-body">
            <fieldset>
                <div class="form-group">
                    <asp:TextBox id="TxtUser" ValidationGroup="AdminArea" CssClass="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredUser" runat="server" ControlToValidate="TxtUser"
                        ValidationGroup="AdminArea" ErrorMessage="">*</asp:RequiredFieldValidator>
                </div>


                <div class="form-group">
                    <asp:TextBox id="TxtPassword" TextMode="Password" ValidationGroup="AdminArea" CssClass="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredPassword" runat="server" ControlToValidate="TxtPassword"
                        ValidationGroup="AdminArea" ErrorMessage="">*</asp:RequiredFieldValidator>
                </div>

                <%--<div class="checkbox">
                    <label>
                        <input name="remember" type="checkbox" value="Remember Me">Remember Me
                    </label>
                </div>--%>
                
                <asp:Button ID="CmdLogin" ValidationGroup="AdminArea" runat="server" 
                    CssClass="btn btn-lg btn-success btn-block" OnClick="CmdLogin_Click" Text="Login" />

                <div class="checkbox">
                    <label>
                        <asp:CheckBox runat="server" ID="ChkRememberMe" Text="" />
                        <asp:Literal runat="server" ID="LitRememberMe"></asp:Literal>
                    </label>
                </div>


            </fieldset>
        </div>
    </div>

    <%=LblErrore %>

</div>
