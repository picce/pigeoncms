<%@ Page Title="" Language="C#" MasterPageFile="~/puppets.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="private_default" %>
<%@ Register Assembly="PigeonCms.Core" Namespace="PigeonCms.Controls" TagPrefix="pgn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphHead" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="CphMain" Runat="Server">

    <h1>
        <%=GetLabel("AQ_private", "Title", "Private area")%>    
    </h1>

    <asp:Literal runat="server" ID="Lit1"></asp:Literal>

</asp:Content>

