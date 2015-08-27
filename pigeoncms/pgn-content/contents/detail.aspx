<%@ Page Language="C#" AutoEventWireup="true" CodeFile="detail.aspx.cs" Inherits="_detail" MasterPageFile="~/pgn-content/masterpages/puppets.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphHead" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="CphMain" Runat="Server">

    <h1>
        <%=GetLabel("AQ_detail", "Title", "Prodotto Singolo")%>    
    </h1>


    <div class="product-box">
        <h2><%=Product.Title %></h2>
        <p><%=Product.Description %></p>
        <p><%=Product.RegularPrice %></p>
        <p><%=Product.SalePrice %></p>
    </div>

    <asp:Literal ID="LitVariants" runat="server" Visible="false"></asp:Literal>

</asp:Content>
