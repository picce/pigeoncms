<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Default.ascx.cs" Inherits="Controls_Default" %>

<div class='<%=base.BaseModule.CssClass %>'>
    <%=HeaderText %>
    <ul class='<%=base.BaseModule.CssClass %>'>
    <%=ListString %>
    </ul>
    <%=FooterText %>
</div>
