<%@ Page Title="" Language="C#" MasterPageFile="~/puppets.master" AutoEventWireup="true" CodeFile="cache.aspx.cs" Inherits="_cache" %>
<%@ Register Assembly="PigeonCms.Core" Namespace="PigeonCms.Controls" TagPrefix="pgn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphHead" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="CphMain" Runat="Server">

    <h1>
        <%=GetLabel("AQ_cache", "Title", "Cache example")%>    
    </h1>

    <div>
        <asp:Button ID="BtnClearCache" runat="server" Text="Clear cache" 
            Visible="true" CssClass="btn btn-primary btn-xs" OnClick="BtnClearCache_Click" />

        <asp:Literal runat="server" ID="LitRes"></asp:Literal>
        <br />

        <%=LitRows %>
    </div>
</asp:Content>

