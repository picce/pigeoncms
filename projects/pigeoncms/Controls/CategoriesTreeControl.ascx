<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CategoriesTreeControl.ascx.cs" Inherits="Controls_CategoriesTreeControl" %>
<%@ Register Assembly="PigeonCms.Core" Namespace="PigeonCms.Controls" TagPrefix="pgn" %>

<script>

    var Pgn_CategoriesAdmin = {

        config: {

            CLICK_EVENT_HANDLERS: 'click touchend' /*touchend*/

        },

        init: function (upd1ClientId) {

            Pgn_CategoriesAdmin.bind(upd1ClientId);

        },

        bind: function (upd1) {

            $('.action-cat-select, .action-cat-edit, .action-cat-enabled, .action-cat-moveup, .action-cat-movedown').on(
            Pgn_CategoriesAdmin.config.CLICK_EVENT_HANDLERS, function (e) {

                e.preventDefault();

                var command = $(this).data("command");
                if (upd1 != null) {
                    __doPostBack(upd1, command);
                }

            });

            $('.action-cat-delete').on(
            Pgn_CategoriesAdmin.config.CLICK_EVENT_HANDLERS, function (e) {

                if (confirm(deleteQuestion)) {

                    var command = $(this).data("command");
                    if (upd1 != null) {
                        __doPostBack(upd1, command);
                    }

                }

            });

        }

    }

</script>

<pgn:TreeViewLinks ID="Tree1" runat="server" NodeIndent="15"
    CssClass="treeview__links-control"
    OnSelectedNodeChanged="Tree1_SelectedNodeChanged" OnTreeNodePopulate="Tree1_TreeNodePopulate" ExpandDepth="0">
    <HoverNodeStyle Font-Underline="false" BackColor="#f5f5f5" />
    <NodeStyle HorizontalPadding="5px" VerticalPadding="5px"></NodeStyle>
    <ParentNodeStyle Font-Bold="False" />
    <SelectedNodeStyle BackColor="#f5f5f5" />
</pgn:TreeViewLinks>