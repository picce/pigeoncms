<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FileUploadModern.ascx.cs" Inherits="FileUploadModern" %>
<%@ Import Namespace="PigeonCms" %>

<div class="pgn-fileUploadSingle clearfix">    

    <div class="box-container" <asp:Literal runat="server" ID="LitName" /> <asp:Literal ID="litTranslations" runat="server"/>>

        <div class="dragandrophandler box box-insert" <asp:Literal ID="litDataMaxSize" runat="server"/>>
            <span class="drop-label drop-file"><%=Utility.GetLabel("click or drag", "click or drag here") %></span>
            <div class="progress-bar">
                <div></div>
            </div>
        </div>

        <asp:Panel ID="BoxPreview" runat="server" CssClass="box box-preview">
            <asp:HyperLink ID="lnkPreview" runat="server" CssClass="preview-link box-actions__label" Target="_blank"/>
            <asp:Panel ID="ActionsLayer" runat="server" CssClass="box-actions-layer">
                <asp:Label runat="server" ID="lblDelete" CssClass="box-actions__label box-actions__label--delete js-delete"></asp:Label>
                <asp:Button ID="BtnDel" runat="server" Style="display: none;" />
            </asp:Panel>
        </asp:Panel>

        <asp:HiddenField ID="hidParameters" runat="server" />

        <fieldset class="adminForm" style="display: none;">
            <asp:Panel runat="server" ID="UploadContainer">
                <asp:FileUpload runat="server" ID="fileUpload" CssClass="pgn-fileupload-input" AllowMultiple="false" />
            </asp:Panel>
        </fieldset>
    </div>

    <div class="pgn-uploadRestrictions">
        <asp:Literal runat="server" ID="LitRestrictions"></asp:Literal>
    </div>

</div>
