<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TextAreaFieldContainer.ascx.cs" Inherits="AQuest.PigeonCMS.Controls.TextAreaFieldContainer" %>

<div class='item-field-container <asp:Literal runat="server" ID="litColumnSizes"/>'>
    <div class='form-group <asp:Literal runat="server" ID="litClass"/>'>
        <div class="form-text-wrapper">
            <label><asp:Literal runat="server" ID="litLabel"/></label>
            <asp:PlaceHolder ID="plhInnerControl" runat="server"/>        
        </div>
    </div>
</div>