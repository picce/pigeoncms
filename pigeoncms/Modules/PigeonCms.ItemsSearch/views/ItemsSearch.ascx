<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ItemsSearch.ascx.cs" Inherits="Controls_ItemsSearch" %>

<div class='moduleBody modItemsSearch <%=base.BaseModule.CssClass %>'>
    <%=HeaderText %>

    <%=base.GetLabel("LblSearchText", "", TxtSearch) %>
    
    <asp:TextBox ID="TxtSearch" runat="server" ValidationGroup="SearchForm" AutoPostBack="true"
        ontextchanged="TxtSearch_TextChanged"
        CssClass='modItemsSearch_searchText' onkeydown="return (event.keyCode==13?event.keycode=9);"></asp:TextBox>
    <asp:LinkButton ID="BtnSearch" runat="server" onclick="BtnSearch_Click" 
        ValidationGroup="SearchForm" CausesValidation="true" CssClass='modItemsSearch_searchLink'>
        <%=base.GetLabel("LblSearchLink", "search") %>
        </asp:LinkButton>
            
    <%=FooterText %>
</div>
