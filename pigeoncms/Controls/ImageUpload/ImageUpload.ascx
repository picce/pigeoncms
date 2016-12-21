<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ImageUpload.ascx.cs" Inherits="AQuest.PigeonCMS.Controls.ImageUpload" %>

<div class="pgn-ajax-image-upload-container" data-uploader-id="<asp:Literal ID="litUploaderID" runat="server"/>">
    <ul>
        <li>
            <div class="asyncFileupload_selectFileContainer">
                <span class="asyncFileupload_button">Select</span>
                <ajaxToolkit:AsyncFileUpload OnClientUploadError="AQuest.PigeonCMS.Control.ImageUpload.OnUploadError" 
                    OnClientUploadComplete="AQuest.PigeonCMS.Control.ImageUpload.OnUploadComplete" runat="server"
                    ID="asyncFileUpload" Width="200px" UploaderStyle="Traditional" CssClass="asyncFileupload_selectFileInputContainer"
                    ThrobberID="lblLoader" EnableViewState="true" />
            </div>
        </li>
        <li>
            <asp:Button ID="btnRemove" runat="server" Text="Remove" CssClass="asyncFileupload_button" />
        </li>
    </ul>
    <div class="pgn-ajax-image-upload-thumb-container">
        <asp:Image ID="imgThumb" CssClass="pgn-ajax-image-upload-thumb" runat="server" />
    </div>


    <asp:Label ID="lblLoader" runat="server" Visible="true"></asp:Label>
</div>
