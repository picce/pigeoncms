<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Default.ascx.cs" Inherits="Controls_CategoriesAdmin" %>
<%@ Register src="~/Controls/PermissionsControl.ascx" tagname="PermissionsControl" tagprefix="uc1" %>
<%@ Register src="~/Controls/CategoriesTreeControl.ascx" tagname="CategoriesTreeControl" tagprefix="uc1" %>


<script type="text/javascript">
// <!CDATA[

    function pageLoad(sender, args) 
    {
        $("a.fancyRefresh").fancybox({
            'width': '80%',
            'height': '80%',
            'type': 'iframe',
            'hideOnContentClick': false,
            onClosed: function() { }
        });

        Pgn_CategoriesAdmin.init('<%=Upd1.ClientID%>');

    }

    var deleteQuestion = '<%=PigeonCms.Utility.GetLabel("RECORD_DELETE_QUESTION") %>';

    $(document).ready(function () {
    })

// ]]>
</script>

<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="1" AssociatedUpdatePanelID="Upd1">
    <ProgressTemplate>
        <div class="loading"><%=PigeonCms.Utility.GetLabel("LblLoading", "loading") %></div>
    </ProgressTemplate>
</asp:UpdateProgress>

<asp:UpdatePanel ID="Upd1" runat="server">
<ContentTemplate>

    <div class="row">
        <div class="col-lg-12">
            <asp:Label ID="LblErr" runat="server" Text=""></asp:Label>
            <asp:Label ID="LblOk" runat="server" Text=""></asp:Label>
        </div>
    </div>

    <div class="row">
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">

        <asp:View ID="ViewSee" runat="server">

            <div class="col-lg-12">
                <div class="panel panel-default clearfix">
                    <div class="panel-body"> 
                        <div class="pull-right">
                            <div class="btn-group adminToolbar">
                                <asp:Button ID="BtnNew" runat="server" Text="<%$ Resources:PublicLabels, CmdNew %>" CssClass="btn btn-primary" OnClick="BtnNew_Click" />
                            </div>
                        </div> 
                    </div>
                </div>
            </div>

            <div class="col-lg-12">
                <div class="panel panel-default adminFilters">
                    <div class="panel-heading">
                        <h4 class="panel-title">
                            <a data-toggle="collapse" data-parent="#accordion" href="#collapseOne">
                            <%=PigeonCms.Utility.GetLabel("LblFilters")%>
                            </a>
                        </h4>

                    </div>
                    <div id="collapseOne" class="panel-collapse collapse in">
                        <div class="panel-body">

                            <div class="form-group col-sm-12">
                                <%=GetLabel("Section", "Section", DropSectionsFilter, true) %>
                                <asp:DropDownList ID="DropSectionsFilter" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="DropSectionsFilter_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>

                        </div>
                    </div>
                </div>
            </div>

            <div class="col-lg-12">

                <div class="panel panel-default">
                    <div class="table-responsive">

                        <uc1:CategoriesTreeControl runat="server" id="Tree1" />
                    
                    </div>
                </div>

            </div>

        </asp:View>
   

        <asp:View ID="ViewInsert" runat="server">

            <div class="panel panel-default">

                <div class="panel-heading clearfix">
                    <%=base.GetLabel("LblDetails", "Details") %>
                    <div class="pull-right">
                        <div class="btn-group">
                            <asp:Button ID="BtnSave" runat="server" Text="<%$ Resources:PublicLabels, CmdSave %>" CssClass="btn btn-primary" OnClick="BtnSave_Click" />
                            <asp:Button ID="BtnCancel" runat="server" Text="<%$ Resources:PublicLabels, CmdCancel %>" CssClass="btn btn-default" CausesValidation="false" OnClick="BtnCancel_Click" />
                        </div>
                    </div>
                </div>

                <div class="panel-body">
                    <div class="form-group col-sm-6 col-lg-3">
                        <%=base.GetLabel("LblSection", "Section", LitSection, true)%>
                        <asp:TextBox ID="LitSection" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                    </div>


                    <div class="form-group col-sm-6 col-lg-3">
                        <%=base.GetLabel("LblExtId", "External Id", TxtExtId, true)%>
                        <asp:TextBox ID="TxtExtId" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="form-group col-lg-3">
                        <%=base.GetLabel("LblCssClass", "Css class", TxtCssClass, true)%>
                        <asp:TextBox ID="TxtCssClass" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="form-group col-lg-3">
                        <%=base.GetLabel("LblEnabled", "Enabled", ChkEnabled, true)%>
                        <asp:CheckBox ID="ChkEnabled" runat="server" CssClass="form-control" Enabled="true" />
                    </div>

                    <div class="form-group col-sm-12">
                        <%=base.GetLabel("LblParentItem", "Parent category", ListParentId, true)%>
                        <asp:ListBox ID="ListParentId" SelectionMode="Single" Rows="10" runat="server" CssClass="form-control"></asp:ListBox>
                    </div>

                    <div class="form-group col-lg-12">
                        <%=base.GetLabel("LblTitle", "Title", null, true)%>
                        <asp:Panel runat="server" ID="PanelTitle"></asp:Panel>
                    </div>

                    <div class="form-group col-lg-12">
                        <%=base.GetLabel("LblDescription", "Description", null, true)%>
                        <asp:Panel runat="server" ID="PanelDescription"></asp:Panel>
                    </div>

                    <uc1:PermissionsControl ID="PermissionsControl1" runat="server" />

                </div>

            </div>

        </asp:View>
    </asp:MultiView>
    </div>

</ContentTemplate>
</asp:UpdatePanel>