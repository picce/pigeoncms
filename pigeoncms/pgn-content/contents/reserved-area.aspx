<%@ Page Title="" Language="C#" MasterPageFile="~/pgn-content/masterpages/puppets.master" AutoEventWireup="true" CodeFile="reserved-area.aspx.cs" Inherits="contents_reserved_area" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphHead" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="CphMain" runat="Server">

    <div class="o-container">

        <div class="o-row o-row--small u-table u-table--full u-pad-tb--5">

            <div class="o-title o-title--small o-col o-col--35 u-table-cell">
                reserved area
            </div>

            <div class="o-title o-subtitle--big o-col o-col--50 u-table-cell">
				You can use the default membership provider <code class="o-code">PgnUserProvider</code> or create a new one.<br />
				All resources (sections, categories, items, modules, menu) are subject to security context controls.<br />
            </div>

        </div>


        <div class="o-row o-row--small u-table u-table--full u-pad-tb--5">

            <div class="o-title o-subtitle--big o-subtitle--center o-col o-col--100 u-table-cell">
                Login
            </div>

        </div>

        <div class="c-login o-row o-row--small u-pad-b--5">

            <div class="o-login--wrapper">

                <form runat="server" id="formLogin">

                    <div class="o-login--wrapper-input">
                        <asp:TextBox ID="TxtUser" placeholder="username" runat="server"></asp:TextBox>
                    </div>
                    <div class="o-login--wrapper-input">
                        <asp:TextBox ID="TxtPassword" TextMode="Password" placeholder="password" runat="server"></asp:TextBox>
                    </div>
                    <div class="o-login--wrapper-input">
                        <asp:Button ID="CmdLogin" ValidationGroup="AdminArea" runat="server" OnClick="CmdLogin_Click" Text="Login" />
                    </div>

                </form>

            </div>

        </div>

    </div>

</asp:Content>
