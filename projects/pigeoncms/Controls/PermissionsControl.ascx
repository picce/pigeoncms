<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PermissionsControl.ascx.cs" Inherits="Controls_PermissionsControl" %>

<legend><%=base.GetLabel("LblSecurity", "Security", null, true) %></legend>
<table cellspacing="0" class="table table-striped table-bordered">
    <tr>
        <td></td>
        <td class="key"><%=base.GetLabel("LblRead", "Read", null, true) %></td>
        <td class="key"><%=base.GetLabel("LblWrite", "Write", null, true) %></td>
    </tr>
    <tr>
        <td class="key"><%=base.GetLabel("LblPermissionId", "ID", null, true) %></td>
        <td>
            <asp:Literal ID="LitId" runat="server"></asp:Literal>
        </td>
        <td>
            <asp:Literal ID="LitWriteId" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td class="key"><%=base.GetLabel("LblAccessType", "AccessType", DropAccessType, true)%></td>
        <td>
            <div class="form-group form-select-wrapper select-right form-select-detail-item-colored">
                <asp:DropDownList ID="DropAccessType" runat="server" CssClass="form-control">
                </asp:DropDownList>
            </div>
        </td>
        <td>
            <div class="form-group form-select-wrapper select-right form-select-detail-item-colored">
                <asp:DropDownList ID="DropWriteAccessType" runat="server" CssClass="form-control">
                </asp:DropDownList>
            </div>
        </td>
    </tr>
    <tr>
        <td class="key"><%=base.GetLabel("LblRolesAllowed", "Allowed roles", ListRoles, true)%></td>
        <td>
            <div class="form-group">
                <asp:ListBox ID="ListRoles" SelectionMode="Multiple" Rows="10" 
                    CssClass="form-control double-height" runat="server"></asp:ListBox>
            </div>
        </td>
        <td>
            <div class="form-group">
                <asp:ListBox ID="ListWriteRoles" SelectionMode="Multiple" Rows="10" 
                    CssClass="form-control double-height" runat="server"></asp:ListBox>
            </div>
        </td>
    </tr>
    <tr>
        <td class="key"><%=base.GetLabel("LblAccessCode", "Access code", TxtAccessCode, true)%></td>
        <td>
            <div class="form-group">
                <asp:TextBox ID="TxtAccessCode" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
        </td>
        <td>
            <div class="form-group">
                <asp:TextBox ID="TxtWriteAccessCode" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
        </td>
    </tr>
    <tr>
        <td class="key"><%=base.GetLabel("LblAccessLevel", "Access level", TxtAccessLevel, true)%></td>
        <td>
            <div class="form-group">
                <asp:TextBox ID="TxtAccessLevel" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
        </td>
        <td>
            <div class="form-group">
                <asp:TextBox ID="TxtWriteAccessLevel" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
        </td>
    </tr>
</table>