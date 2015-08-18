<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Default.ascx.cs" Inherits="Controls_Default" %>

<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="1" AssociatedUpdatePanelID="Upd1">
    <ProgressTemplate>
        <div class="loading"><%=PigeonCms.Utility.GetLabel("LblLoading", "loading") %></div>
    </ProgressTemplate>
</asp:UpdateProgress>

<script type="text/javascript">
function offlineWarning()
{
    var span = document.getElementById('SpanOfflineWarning');
    if (document.getElementById('<%=ChkOffline.ClientID %>').checked)    
        span.style.visibility = "visible";
    else
        span.style.visibility = "hidden";
    
}
</script>
    
<asp:UpdatePanel ID="Upd1" runat="server">
<ContentTemplate>

   <asp:Label ID="LblErr" runat="server" Text="" CssClass="error"></asp:Label>
   <asp:Label ID="LblOk" runat="server" Text="" CssClass="success"></asp:Label>
   <br />

   <div class="adminToolbar">
       <asp:Button ID="BtnSave" runat="server" Text="<%$ Resources:PublicLabels, CmdSave %>" CssClass="button" OnClick="BtnSave_Click" />
   </div>        
   
   <div class="col width-100">
   <fieldset class="adminForm">
      <legend></legend>
      <table class="adminTable">
      <tr>
         <td class="key"><%=base.GetLabel("LblPageTemplate", "Page template", DropTemplates)%></td>
         <td>
            <asp:DropDownList ID="DropTemplates" CssClass="adminMediumText" runat="server">
            </asp:DropDownList>
         </td>
      </tr>      
      <tr>
         <td class="key"><%=base.GetLabel("LblTitle", "Message title", TxtTitle) %></td>
         <td>
            <asp:TextBox ID="TxtTitle" runat="server" CssClass="adminText"></asp:TextBox>
         </td>
      </tr>
      <tr>
         <td class="key"><%=base.GetLabel("LblMessage", "Message", TxtMessage)%></td>
         <td>
            <asp:TextBox ID="TxtMessage" runat="server" CssClass="adminText" 
            TextMode="MultiLine" Rows="3"></asp:TextBox>
         </td>
      </tr>          
      <tr>
          <td class="key"><%=base.GetLabel("LblOffline", "Offline", ChkOffline)%></td>
          <td>
              <asp:CheckBox ID="ChkOffline" runat="server" />
              <span id="SpanOfflineWarning" class="warningText" style="visibility:hidden;">
              <%=base.GetLabel("LitOfflineWarning", "WARNING")%>
              </span>
          </td>
      </tr>
      </table>
   </fieldset>

</ContentTemplate>
</asp:UpdatePanel>