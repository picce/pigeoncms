<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CreateNew.ascx.cs" Inherits="Controls_CreateNew" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register src="~/Controls/MemberEditorControl.ascx" tagname="MemberEditor" tagprefix="uc1" %>
<%@ Register Namespace="WebControlCaptcha" Assembly="WebControlCaptcha" TagPrefix="uc1" %>

<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="1" AssociatedUpdatePanelID="Upd1">
    <ProgressTemplate>
        <div class="loading"><%=PigeonCms.Utility.GetLabel("LblLoading", "loading") %></div>
    </ProgressTemplate>
</asp:UpdateProgress>

<script type="text/javascript">

    function pageLoad(sender, args) {
        $(document).ready(function () {

            $('td.key').each(function () {
                var hide = true;
                var html = $(this).html();
                if ($.trim(html) != '')
                    hide = false;
                if (hide)
                    $(this).parent('tr').hide();
            });
        });
    }
 
</script>

<asp:UpdatePanel ID="Upd1" runat="server">
<ContentTemplate>

    <div class='itemTitle'>
        <asp:Literal runat="server" ID="LitTitle"></asp:Literal>
    </div>
    <uc1:MemberEditor ID="MemberEditor1" runat="server" />
    <uc1:captchacontrol id="CaptchaControl1" runat="server" 
    Visible="false" Enabled="false"></uc1:captchacontrol>
    
    <br />
    <asp:Button ID="BtnInsSave" runat="server" Text="<%$ Resources:PublicLabels, CmdConfirm %>" CssClass="button" OnClick="BtnSave_Click" />
    <asp:Button ID="BtnInsCancel" runat="server" Text="<%$ Resources:PublicLabels, CmdCancel %>" CssClass="button" OnClick="BtnCancel_Click" />
    
    <br />
    <br />
    <asp:Label ID="LblErr" runat="server" Text="" CssClass="error"></asp:Label>
    <asp:Label ID="LblOk" runat="server" Text="" CssClass="success"></asp:Label>

</ContentTemplate>
</asp:UpdatePanel>