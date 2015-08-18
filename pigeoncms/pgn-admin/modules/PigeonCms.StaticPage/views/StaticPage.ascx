<%@ Control EnableViewState="false" Language="C#" AutoEventWireup="true" CodeFile="StaticPage.ascx.cs" Inherits="Controls_StaticPage" %>
<div class="moduleTitle">
   <h1><asp:Literal ID="LitPageTitle" runat="server"></asp:Literal></h1>
</div>
<div class="moduleBody">
   <asp:Literal ID="LitPageContent" runat="server"></asp:Literal>
</div>