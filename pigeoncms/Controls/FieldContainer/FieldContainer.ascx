<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FieldContainer.ascx.cs" Inherits="AQuest.PigeonCMS.Controls.FieldContainer" %>

<div class='item-field-container <asp:Literal runat="server" ID="litColumnSizes"/>'>
    <div class='form-group <asp:Literal runat="server" ID="litClass"/>'>
        <label><asp:Literal runat="server" ID="litLabel" /></label>
        <asp:PlaceHolder ID="plhInnerControl" runat="server" />
    </div>
</div>

