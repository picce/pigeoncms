<%@ Page Language="C#" AutoEventWireup="true" CodeFile="products.aspx.cs" Inherits="_products" MasterPageFile="~/pgn-content/masterpages/puppets.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphHead" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="CphMain" Runat="Server">

    <h1>
        <%=GetLabel("AQ_products", "Title", "Prodotti")%>    
    </h1>

    <asp:Repeater ID="repProducts" runat="server" OnItemDataBound="repProducts_ItemDataBound">
        <ItemTemplate>
            <div class="product-box">
                <h2><asp:HyperLink ID="HypTitle" runat="server"></asp:HyperLink></h2>
                <p><asp:Literal ID="LitDescription" runat="server"></asp:Literal></p>
                <p><asp:Literal ID="LitPrice" runat="server"></asp:Literal></p>
                <p><asp:Literal ID="LitSalePrice" runat="server"></asp:Literal></p>
            </div>
        </ItemTemplate>
    </asp:Repeater>

</asp:Content>
