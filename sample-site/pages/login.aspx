<%@ Page Title="" Language="C#" MasterPageFile="~/puppets.master" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="_login" %>
<%@ Register Assembly="PigeonCms.Core" Namespace="PigeonCms.Controls" TagPrefix="pgn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphHead" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="CphMain" Runat="Server">

    <h1>
        <%=GetLabel("AQ_login", "Title", "Login")%>    
    </h1>

    <p>
        <asp:TextBox id="TxtUser" placeholder="username" runat="server"></asp:TextBox>
    </p>

    <p>
        <asp:TextBox id="TxtPassword" TextMode="Password" placeholder="password" runat="server"></asp:TextBox>
    </p>

    <p>
        <asp:Button ID="CmdLogin" ValidationGroup="AdminArea" runat="server" CssClass="button" OnClick="CmdLogin_Click" Text="OK" />
    </p>

    <p>
        <asp:Literal runat="server" ID="LitRes"></asp:Literal>
    </p>
</asp:Content>

