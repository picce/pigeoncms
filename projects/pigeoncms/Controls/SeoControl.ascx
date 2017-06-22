<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SeoControl.ascx.cs" Inherits="Controls_SeoControl" %>
<%@ Register Src="~/Controls/ContentEditorControl.ascx" TagName="ContentEditorControl" TagPrefix="uc1" %>

<div class="form-group col-lg-6 checkbox-container">
	<span><%=GetLabel("MetaNoIndex", "META NoIndex") %></span>
	<asp:CheckBox ID="ChkNoIndex" runat="server" Enabled="true" Text="" />
	<%=base.GetLabel("LblNoIndex", "NoIndex", ChkNoIndex, true)%>
</div>

<div class="form-group col-lg-6 checkbox-container">
	<span><%=GetLabel("MetaNoFollow", "META NoFollow") %></span>
	<asp:CheckBox ID="ChkNoFollow" runat="server" Enabled="true" Text="" />
	<%=base.GetLabel("LblNoFollow", "NoFollow", ChkNoFollow, true)%>
</div>

<div class="form-group col-md-6 spacing-detail">
	<%=base.GetLabel("LblTitle", "Title", null, true)%>
	<asp:Panel runat="server" ID="PanelTitle"></asp:Panel>
</div>

<div class="form-group col-md-6 spacing-detail">
	<%=base.GetLabel("LblSlug", "Slug", null, true)%>
	<asp:Panel runat="server" ID="PanelSlug"></asp:Panel>
</div>

<div class="form-group col-sm-12 spacing-detail">
	<%=base.GetLabel("LblDescription", "Description", null, true)%>
	<asp:Panel runat="server" ID="PanelDescription"></asp:Panel>
</div>