<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Query.aspx.cs" Inherits="Query" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PigeonCms Query Analyzer</title>
    <style type="text/css">
    td
    {
        text-align:left;
        vertical-align: top;
    }
    input{ font-size: 8pt; }
    textarea{ font-size: 8pt; }
    select{ font-size: 8pt; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="1" AssociatedUpdatePanelID="Upd1">
        <ProgressTemplate>
            <div class="loading">loading</div>
        </ProgressTemplate>
    </asp:UpdateProgress>    
    
    <asp:UpdatePanel ID="Upd1" runat="server">
    <ContentTemplate>
        <table>
        <tr>
            <td style="width: 150px;">connection string</td>
            <td>
                <asp:TextBox ID="TxtConnString" runat="server" Width="600px" 
                    TextMode="SingleLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 150px;">tab prefix</td>
            <td>
                <asp:TextBox ID="TxtTabPrefix" runat="server" Width="100px" 
                    TextMode="SingleLine">pgn_</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>query</td>
            <td>
            <asp:TextBox ID="TxtSql" runat="server" TextMode="MultiLine" Width="750px" 
                    Height="400px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="right">
                <asp:Button ID="BtnRun" runat="server" Text="Run" onclick="BtnRun_Click" />
                <asp:Button ID="BtnCancel" runat="server" Text="Cancel" 
                    onclick="BtnCancel_Click" />
            </td>
        </tr>
        <tr>
            <td>result</td>
            <td>
                <asp:Literal ID="LitResult" runat="server"></asp:Literal>
            </td>
        </tr>
        </table>
    </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
