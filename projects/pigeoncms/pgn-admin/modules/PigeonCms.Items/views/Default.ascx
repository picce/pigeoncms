<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Default.ascx.cs" Inherits="Controls_Default" %>

<div class='moduleBody <%=base.BaseModule.CssClass %>'>
    <%=HeaderText %>
    <h1><%=PathString %></h1>
    <table class='<%=base.BaseModule.CssClass %>' cellspacing="0">
    <%=ListString %>
    </table>
    <%=FooterText %>
</div>
