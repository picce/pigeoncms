<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FileUpload.ascx.cs" Inherits="Controls_FileUpload" %>

<%=HeaderText%>

<div class="pgn-fileUpload-litFolder">
<%=LitFolder %>
</div>

<div class="pgn-fileUpload-litAllowedFiles">
<%=base.GetLabel("Allowedfiles", "Allowed files")%>: <%=LitRestrictions%>
</div>

<div class="pgn-fileUpload-panelFields">
<asp:Panel runat="server" ID="PanelFields"></asp:Panel>
</div>

<div class="pgn-fileUpload-cmdConfirm">
<asp:Button ID="CmdConfirm" runat="server" Text="" CssClass="button" onclick="CmdConfirm_Click" />
</div>

<asp:Label runat="server" ID="LblSuccess" CssClass="success"></asp:Label>
<asp:Label runat="server" ID="LblError" CssClass="error"></asp:Label>
<%=FooterText%>