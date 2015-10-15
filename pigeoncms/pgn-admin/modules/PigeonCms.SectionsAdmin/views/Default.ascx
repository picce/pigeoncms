<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Default.ascx.cs" Inherits="Controls_SectionsAdmin" %>
<%@ Register src="~/Controls/PermissionsControl.ascx" tagname="PermissionsControl" tagprefix="uc1" %>

<script type="text/javascript">
// <!CDATA[

    function pageLoad(sender, args) 
    {
        $(document).ready(function () {
            $("a.fancyRefresh").fancybox({
                'width': '80%',
                'height': '80%',
                'type': 'iframe',
                'hideOnContentClick': false,
                onClosed: function () {
                    //RemoveFromCache(onSuccess, onFailure);
                    //ReloadUpd1();
                }
            });

        });
    }

    function changeItemType() {
        var elemname = '#<%=DropItemType.ClientID %>';
        $(elemname).removeAttr('disabled');
        $('#lnkchange').hide();
    }

    var deleteQuestion = '<%=PigeonCms.Utility.GetLabel("RECORD_DELETE_QUESTION") %>';

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
                            <div class="form-group col-sm-6 col-md-4">
                                <asp:DropDownList runat="server" ID="DropEnabledFilter" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="DropEnabledFilter_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-lg-12">

                <div class="panel panel-default">
                    <div class="table-responsive">

                        <asp:GridView ID="Grid1" runat="server" AllowPaging="True" AllowSorting="true" Width="100%" AutoGenerateColumns="False"
                            DataSourceID="ObjDs1" DataKeyNames="Id" OnRowCommand="Grid1_RowCommand" OnRowCreated="Grid1_RowCreated" OnRowDataBound="Grid1_RowDataBound">
                            <Columns>

                                <asp:TemplateField HeaderText="<%$ Resources:PublicLabels, LblTitle %>" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LnkTitle" runat="server" CausesValidation="false" CommandName="Select" CommandArgument='<%#Eval("Id") %>'></asp:LinkButton>
                                        <br />
                                        <span class="small text-muted">
                                            <asp:Literal ID="LitItemInfo" runat="server" Text=""></asp:Literal>
                                        </span>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="CssClass" HeaderText="Css" ItemStyle-CssClass="small text-muted" SortExpression="CssClass" />
                    
                                <asp:TemplateField HeaderText="<%$ Resources:PublicLabels, LblEnabled %>" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="ImgEnabledOk" runat="server" CommandName="ImgEnabledOk" Visible="false" CommandArgument='<%#Eval("Id") %>'>
                                            <i class='fa fa-pgn_checked fa-fw'></i>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="ImgEnabledKo" runat="server" CommandName="ImgEnabledKo" Visible="false" CommandArgument='<%#Eval("Id") %>'>
                                            <i class='fa fa-pgn_unchecked fa-fw'></i>                            
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Access" SortExpression="AccessType" ItemStyle-CssClass="small text-muted">
                                    <ItemTemplate>
                                    <asp:Literal ID="LitAccessTypeDesc" runat="server" Text=""></asp:Literal>
                                    </ItemTemplate>
                                </asp:TemplateField>
                    
                    
                                <asp:TemplateField HeaderText="<%$ Resources:PublicLabels, LblFiles %>">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="LnkUploadFiles" runat="server">
                                            <i class='fa fa-pgn_attach fa-fw'></i>
                                        </asp:HyperLink>
                                        <br />
                                        <span><asp:Literal ID="LitFilesCount" runat="server" Text=""></asp:Literal></span>
                                    </ItemTemplate>
                                </asp:TemplateField>
                    
                                <asp:TemplateField HeaderText="Img">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="LnkUploadImg" runat="server">
                                            <i class='fa fa-pgn_image fa-fw'></i>
                                        </asp:HyperLink>
                                        <br />
                                        <span class="small text-muted"><asp:Literal ID="LitImgCount" runat="server" Text=""></asp:Literal></span>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Items" SortExpression="">
                                    <ItemTemplate>
                                        <span class="small text-muted">
                                            <asp:Literal ID="LitItems" runat="server" Text=""></asp:Literal>
                                        </span>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Disk space" SortExpression="">
                                    <ItemTemplate>
                                        <span class="small text-muted">
                                            <asp:Literal ID="LitDiskSpace" runat="server" Text=""></asp:Literal>
                                        </span>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LnkDel" runat="server" CommandName="DeleteRow" 
                                            CommandArgument='<%#Eval("Id") %>' OnClientClick="return confirm(deleteQuestion);">
                                            <i class='fa fa-pgn_delete fa-fw'></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                    
                                <asp:BoundField DataField="Id" HeaderText="ID" ItemStyle-CssClass="small text-muted"
                                     SortExpression="Id" ItemStyle-Width="10" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" />
                            </Columns>
                        </asp:GridView>

                    </div>
                </div>
            </div>
            
            <asp:ObjectDataSource ID="ObjDs1" runat="server" SortParameterName="sort"
                SelectMethod="GetByFilter" TypeName="PigeonCms.SectionsManager" 
                OnObjectCreating="ObjDs1_ObjectCreating"
                OnSelecting="ObjDs1_Selecting">
                <SelectParameters>
                    <asp:Parameter Name="filter" Type="Object" />
                    <asp:Parameter Name="sort" Type="String" DefaultValue="" />
                </SelectParameters>
                <DeleteParameters>
                    <asp:Parameter Name="Id" Type="Int32" />
                </DeleteParameters>
            </asp:ObjectDataSource>
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

                    <ul class="nav nav-pills">
                        <li class="active"><a href="#tab-main" data-toggle="tab"><%=base.GetLabel("Main", "Main") %></a></li>
                        <li><a href="#tab-security" data-toggle="tab"><%=base.GetLabel("Security", "Security") %></a></li>
                    </ul>

                    <div class="tab-content">
                        
                        <div class="tab-pane fade in active" id="tab-main">

                            <div class="form-group col-lg-12">
                                <%=base.GetLabel("LblTitle", "Title", null, true)%>
                                <asp:Panel runat="server" ID="PanelTitle"></asp:Panel>
                            </div>

                            <div class="form-group col-lg-12">
                                <%=base.GetLabel("LblDescription", "Description", null, true)%>
                                <asp:Panel runat="server" ID="PanelDescription"></asp:Panel>
                            </div>

                            <div class="form-group col-lg-3 col-md-6">
                                <%=base.GetLabel("LblExtId", "External Id", TxtExtId, true)%>
                                <asp:TextBox ID="TxtExtId" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                            </div>

                            <div class="form-group col-lg-3 col-md-6">
                                <%=base.GetLabel("LblItemType", "ItemType", DropItemType, true)%>
                                <a href="javascript:void(0)" onclick="changeItemType();" id="lnkchange" runat="server">
                                    <%=base.GetLabel("LblChange", "change", null, true) %>
                                </a>
                                <asp:DropDownList ID="DropItemType" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>

                            <div class="form-group col-lg-3 col-md-6">
                                <%=base.GetLabel("LblCssClass", "Css class", TxtCssClass, true)%>
                                <asp:TextBox ID="TxtCssClass" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                            </div>

                            <div class="form-group col-lg-3 col-md-6">
                                <%=base.GetLabel("LblEnabled", "Enabled", ChkEnabled, true)%>
                                <asp:CheckBox ID="ChkEnabled" runat="server" CssClass="form-control" Enabled="true" />
                            </div>

                            <div class="form-group col-sm-6">
                                <%=base.GetLabel("LblMaxItems", "Max items", TxtMaxItems, true)%>
                                <asp:TextBox ID="TxtMaxItems" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>

                            <div class="form-group col-sm-6">
                                <%=base.GetLabel("LblMaxAttachSizeKB", "Max size for attachments (KB)", TxtMaxAttachSizeKB, true)%>
                                <asp:TextBox ID="TxtMaxAttachSizeKB" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>


                        </div>

                        <div class="tab-pane fade" id="tab-security">
                            <uc1:PermissionsControl ID="PermissionsControl1" runat="server" />
                        </div>

                    </div>

                </div>
        
            </div>
        </asp:View>
    </asp:MultiView>
    </div>

</ContentTemplate>
</asp:UpdatePanel>
