<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CheckboxFieldContainer.ascx.cs" Inherits="AQuest.PigeonCMS.Controls.CheckboxFieldContainer" %>

<div class='item-field-container <asp:Literal runat="server" ID="litColumnSizes"/>'>
    <div class='form-group <asp:Literal runat="server" ID="litControlSizes"/> checkbox-container <asp:Literal runat="server" ID="litClass"/>'>
        <asp:PlaceHolder ID="plhInnerControl" runat="server"/>
        <asp:Literal runat="server" ID="litLabel"/>
    </div>
</div>