<%@ Page Title="" Language="C#" MasterPageFile="~/puppets.master" AutoEventWireup="true" CodeFile="list.aspx.cs" Inherits="pages_list" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphMain" Runat="Server">

    <h1>
        <%=GetLabel("AQ_list", "Title", "Items list example")%>    
    </h1>

    <ul>
    <asp:Repeater runat="server" ID="Rep1" OnItemDataBound="Rep1_ItemDataBound">
        <ItemTemplate>
            <li>
                <asp:Literal runat="server" ID="LitImg"></asp:Literal>
                <%# Eval("Title") %>
            </li>    
        </ItemTemplate>
    </asp:Repeater>
    </ul>

</asp:Content>

