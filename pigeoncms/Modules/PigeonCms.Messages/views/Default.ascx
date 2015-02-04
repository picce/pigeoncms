<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Default.ascx.cs" Inherits="Controls_Messages" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register src="~/Controls/MessageControl.ascx" tagname="MessageControl" tagprefix="uc1" %>

<asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
<asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="1" AssociatedUpdatePanelID="Upd1">
    <ProgressTemplate>
        <div class="loading"><%=PigeonCms.Utility.GetLabel("LblLoading", "loading") %></div>
    </ProgressTemplate>
</asp:UpdateProgress>

<asp:UpdatePanel ID="Upd1" runat="server">
<ContentTemplate>

    <asp:Label ID="LblErr" runat="server" Text="" CssClass="error"></asp:Label>
    <asp:Label ID="LblOk" runat="server" Text="" CssClass="success"></asp:Label>

    <div class="adminToolbar">
        <asp:Button ID="BtnRefresh" runat="server" CausesValidation="false" Text="Refresh" CssClass="button" OnClick="BtnRefresh_Click" />
        <asp:Button ID="BtnNew" runat="server" CausesValidation="false" Text="<%$ Resources:PublicLabels, CmdNew %>" CssClass="button" OnClick="BtnNew_Click" />
    </div>
    <fieldset class="adminFilters">
        <%=PigeonCms.Utility.GetLabel("LblFilters", "Filters") %>&nbsp;
        <asp:DropDownList ID="DropFolderFilter" runat="server" AutoPostBack="true" CssClass="" 
            OnSelectedIndexChanged="Filter_Changed"></asp:DropDownList>
        <asp:DropDownList ID="DropIsReadFilter" runat="server" AutoPostBack="true" CssClass="" 
            OnSelectedIndexChanged="Filter_Changed"></asp:DropDownList>
    </fieldset>
    <br />
    <uc1:MessageControl ID="MsgList" 
        AllowDelete="true"
        NumberOfRowsPerPage="10"
        ViewMode="ListMessages"
        onAfterDelete="MsgList_AfterDelete" 
        onItemSelected="MsgList_ItemSelected"
        runat="server" />
    <br />

    <div class="adminToolbar">
        <asp:Button ID="BtnSend" Visible="false" runat="server" Text="Send" CssClass="button" CausesValidation="true" OnClick="BtnSend_Click" />
        <asp:Button ID="BtnCancel" Visible="false" runat="server" Text="Cancel" CssClass="button" CausesValidation="false" OnClick="BtnCancel_Click" />
        <asp:Button ID="BtnForward" runat="server" Text="Forward" CssClass="button" CausesValidation="false" OnClick="BtnForward_Click" />
        <asp:Button ID="BtnReply" runat="server" Text="Reply" CssClass="button" CausesValidation="false" OnClick="BtnReply_Click" />
    </div>
    <uc1:MessageControl ID="MsgShow" 
        AllowDelete="true"
        ViewMode="ShowMessage"
        onMessageSent="MsgShow_MessageSent" 
        runat="server" />

</ContentTemplate>
</asp:UpdatePanel>