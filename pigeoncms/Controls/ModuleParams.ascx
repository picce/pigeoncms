<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ModuleParams.ascx.cs" Inherits="Controls_ModuleParams" %>

<div class="form-group col-sm-4">
    <%=base.GetLabel("LblUseCache", "Use cache", DropUseCache, true)%>
    <asp:DropDownList ID="DropUseCache" runat="server" CssClass="form-control">
        <asp:ListItem Value="2" Text="Use global"></asp:ListItem>
        <asp:ListItem Value="0" Text="No"></asp:ListItem>
        <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
    </asp:DropDownList>
</div>

<div class="form-group col-sm-4">
    <%=base.GetLabel("LblUseLog", "Use log", DropUseLog, true)%>
    <asp:DropDownList ID="DropUseLog" runat="server" CssClass="form-control">
        <asp:ListItem Value="2" Text="Use global"></asp:ListItem>
        <asp:ListItem Value="0" Text="No"></asp:ListItem>
        <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
    </asp:DropDownList>
</div>

<div class="form-group col-sm-4">
    <%=base.GetLabel("LblSystemMessagesTo", "System messages to", TxtSystemMessagesTo, "comma separated members")%>
    <asp:TextBox ID="TxtSystemMessagesTo" CssClass="form-control" MaxLength="255" runat="server"></asp:TextBox>
</div>

<div class="form-group col-sm-6">
    <%=base.GetLabel("LblCssFile", "Css file", TxtCssFile, true)%>
    <asp:TextBox ID="TxtCssFile" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
</div>

<div class="form-group col-sm-6">
    <%=base.GetLabel("LblCssClass", "Css class", TxtCssClass, true)%>
    <asp:TextBox ID="TxtCssClass" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
</div>

<div class="form-group col-sm-12">
    <asp:CheckBox ID="ChkDirectEditMode" runat="server" CssClass="" />
    <%=base.GetLabel("LblDirectEditMode", "Direct edit mode", ChkDirectEditMode, true)%>
</div>

<div class="form-group col-sm-12">
    <asp:Panel ID="PanelParams" runat="server"></asp:Panel>
</div>