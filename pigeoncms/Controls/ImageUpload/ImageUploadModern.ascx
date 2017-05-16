<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ImageUploadModern.ascx.cs" Inherits="ImageUploadModern" %>
<%@ Import Namespace="PigeonCms" %>

<div class="pgn-imageUploadSingle clearfix">    

    <div class="box-container" <asp:Literal ID="litTranslations" runat="server"/>>

        <div class="dragandrophandler box box-insert" <asp:Literal ID="litDataMaxSize" runat="server"/>>
            <span class="icon icon-image"></span>
            <span class="icon icon-image icon-image--hover"></span>
            <span class="drop-label"><%=Utility.GetLabel("click or drag", "click or drag here") %></span>
            <div class="progress-bar">
                <div></div>
            </div>
        </div>

        <asp:Panel ID="BoxPreview" runat="server" CssClass="box box-preview">

            <asp:Panel ID="ActionsLayer" runat="server" CssClass="box-actions-layer">
                <asp:Label runat="server" ID="lblDelete" CssClass="box-actions__label box-actions__label--delete js-delete">delete</asp:Label>
                <asp:Button ID="BtnDel" runat="server" Style="display: none;" />
                <br />
                <span class="box-actions__label box-actions__label--preview" data-preview-url='<asp:Literal runat="server" ID="litPreview"/>'>
                    preview
                </span>
            </asp:Panel>
        </asp:Panel>

        <asp:HiddenField ID="hidParameters" runat="server" />

        <fieldset class="adminForm" style="display: none;">
            <asp:Panel runat="server" ID="UploadContainer">
                <asp:FileUpload runat="server" ID="fileUpload" CssClass="pgn-imageupload-input" AllowMultiple="false" />
            </asp:Panel>
        </fieldset>
    </div>

    <div class="pgn-uploadRestrictions">
        <asp:Literal runat="server" ID="LitRestrictions"></asp:Literal>
    </div>

</div>
