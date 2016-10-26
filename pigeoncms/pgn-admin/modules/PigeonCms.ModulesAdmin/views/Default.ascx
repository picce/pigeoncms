<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Default.ascx.cs" Inherits="Controls_ModulesAdmin" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register src="~/Controls/ModuleParams.ascx" tagname="ModuleParams" tagprefix="uc1" %>
<%@ Register src="~/Controls/PermissionsControl.ascx" tagname="PermissionsControl" tagprefix="uc1" %>



<script type="text/javascript">

    function pageLoad(sender, args) {
        $(document).ready(function () {

        });
    }

    function mod_ReloadUpd1() {
        var upd1 = '<%=Upd1.ClientID%>';
        if (upd1 != null) {
            __doPostBack(upd1, 'items');
        }
    }

    function disableListMenu()
    {
        document.getElementById('<%=ListMenu.ClientID %>').disabled = 'disabled';
    }

    function enableListMenu()
    {
        document.getElementById('<%=ListMenu.ClientID %>').disabled = '';
    }

    function changeView() {
        var elemname = '#<%=DropViews.ClientID %>';
        $(elemname).removeAttr('disabled');
        $(elemname).removeClass('locked');
        $('#lnkChangeView').hide();
    }

</script>


<cc1:ToolkitScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></cc1:ToolkitScriptManager>
<asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="1" AssociatedUpdatePanelID="Upd1">
	<ProgressTemplate>
		<div class="loading"><%=PigeonCms.Utility.GetLabel("LblLoading", "loading") %></div>
	</ProgressTemplate>
</asp:UpdateProgress>

<script type="text/javascript">

    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
    function EndRequestHandler(sender, args) {
        if (args.get_error() != undefined) {
            args.set_errorHandled(true);
        }
    }

</script>


<asp:UpdatePanel ID="Upd1" runat="server">
<ContentTemplate>

    <div class="row">

        <asp:Panel runat="server" ID="PanelSee">

				<%--#toolbar--%>
				<div class="col-lg-12">
					<div class="panel panel-default panel-filter panel-filer--new clearfix">
						<div class="panel-body">
							<div>
								<asp:Label ID="LblErrSee" runat="server" Text=""></asp:Label>
								<asp:Label ID="LblOkSee" runat="server" Text=""></asp:Label>
							</div>
							<div class="pull-right">
								<div class="btn-group adminToolbar">
									<div class="form-select-wrapper select-right" runat="server" id="DivDropNewContainer">
                                        <asp:DropDownList runat="server" ID="DropNewModule" AutoPostBack="true" 
                                            CssClass="form-control" 
                                            OnSelectedIndexChanged="DropNewModule_SelectedIndexChanged">
                                        </asp:DropDownList>
									</div>
								</div>
							</div>
						</div>
					</div>
				</div>


				<%--#filters--%>
				<div class="col-lg-12">
					<div class="panel panel-default panel-filter">
						<div class="panel-heading">
							<h4 class="panel-title">
								<a data-toggle="collapse" data-parent="#accordion" href="#collapseOne">
									<%=PigeonCms.Utility.GetLabel("LblFilters")%>
									<span>
										<font class="close-span"><%=base.GetLabel("LblClose", "Close") %></font>
										<font class="open-span"><%=base.GetLabel("LblOpen", "Open") %></font>
									</span>
								</a>
							</h4>

						</div>
						<div id="collapseOne" class="panel-collapse collapse in">
							<div class="panel-body">
                                <div class="form-group col-md-4 form-select-wrapper">
                                    <asp:DropDownList runat="server" ID="DropPublishedFilter" AutoPostBack="true" 
                                    CssClass="form-control" OnSelectedIndexChanged="Filter_Changed">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-md-4 form-select-wrapper">
                                    <asp:DropDownList runat="server" ID="DropTemplateBlockNameFilter" AutoPostBack="true" 
                                    CssClass="form-control" OnSelectedIndexChanged="Filter_Changed">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-md-4 form-select-wrapper">
                                    <asp:DropDownList runat="server" ID="DropModuleTypesFilter"  AutoPostBack="true" 
                                    CssClass="form-control" OnSelectedIndexChanged="Filter_Changed">
                                    </asp:DropDownList>
                                </div>
							</div>
						</div>

                    </div>
                </div>


				<%--#list--%>
				<div class="col-lg-12">

						<div class="table-modern">
							<div class="table-modern--wrapper table-modern--mobile table-modern--sortable">

								<div class="table-modern--row table-modern--row-title">

									<a href="javascript:void(0)" class="table-modern-edit"><span></span></a>
									<div class="col-sm-3 table-modern__col align-left"><%=base.GetLabel("Title", "Title") %></div>
									<div class="col-sm-2 table-modern__col align-left"><%=base.GetLabel("BlockMenu", "Block/Menu") %></div>
									<div class="col-sm-3 table-modern__col align-left"><%=base.GetLabel("ModuleCol", "Module") %></div>
									<div class="col-sm-2 table-modern__col align-center"><%=base.GetLabel("Published", "Published") %></div>
									<div class="col-sm-2 table-modern__col align-center"><%=base.GetLabel("Permissions", "Permissions") %></div>
									<div class="table-modern--hover">
										<a href="javascript:void(0)" class="table-modern-edit--close">
											<span></span>
										</a>
									</div>

								</div>

								<asp:Repeater runat="server" ID="Rep1" OnItemDataBound="Rep1_ItemDataBound" OnItemCommand="Rep1_ItemCommand">
									<ItemTemplate>

                                        <div class="table-modern--row">

											<a href="javascript:void(0)" class="table-modern-edit"><span></span></a>
											<input type="hidden" name="RowId" value='<%# Eval("Id") %>' />

											<div class="col-sm-3 table-modern__col">
												<div class="table-modern--description" data-menu="tit">
													<div class="table-modern--description--wrapper">
                                                        <strong>
                                                            <asp:Literal ID="LitTitle" runat="server" />
                                                        </strong>
                                                        <br />
														<asp:Literal ID="LitInfo" runat="server" />
													</div>
												</div>
											</div>

											<div class="col-sm-2 table-modern__col">
												<div class="table-modern--description" data-menu="cat">
													<div class="table-modern--description--wrapper">
                                                        <span class="table-modern--smallcontent">
                                                            <strong>
														        <asp:Literal ID="LitBlock" runat="server" />
                                                            </strong>
                                                            <br />
														    <asp:Literal ID="LitMenuEntries" runat="server" />
                                                        </span>
													</div>
												</div>
											</div>

											<div class="col-sm-3 table-modern__col">
												<div class="table-modern--description" data-menu="info">
													<div class="table-modern--description--wrapper">
                                                        <span class="table-modern--smallcontent">
														    <asp:Literal ID="LitModuleNameDesc" runat="server" /><br />
                                                            <asp:HyperLink runat="server" ID="LnkModuleContent" Visible="false">
                                                                <%=GetLabel("content", "content") %>
                                                            </asp:HyperLink>
                                                        </span>
													</div>
												</div>
											</div>

											<div class="col-sm-2 table-modern__col">
												<div class="table-modern--checkbox" data-menu="vis">
													<asp:Literal runat="server" ID="LitPublished"></asp:Literal>
												</div>
											</div>

											<div class="col-sm-2 table-modern__col">
												<div class="table-modern__col--value" data-menu="sec">
													<asp:Literal ID="LitAccessTypeDesc" runat="server" Text=""></asp:Literal>
												</div>
											</div>

											<div class="table-modern--hover">

												<a href="javascript:void(0)" class="table-modern-edit--close"><span></span></a>

												<div class="table-modern--hover--wrapper clearfix">

													<div class="table-modern--hover--col">
														<asp:LinkButton runat="server" ID="LnkEdit" CausesValidation="false" CommandName="Select" CommandArgument='<%#Eval("Id") %>' class="table-modern--media" ClientIDMode="AutoID" data-title-mobile="EDIT">
															<div class="table-modern--media--wrapper">
																<div class="table-modern--media--modify"></div>
																<span class="table-modern--media--label"><%=base.GetLabel("edit", "edit") %></span>
															</div>
														</asp:LinkButton>
													</div>

													<div class="table-modern--hover--col">
														<asp:LinkButton runat="server" ID="LnkPublished" CommandArgument='<%#Eval("Id") %>' ClientIDMode="AutoID" data-title-mobile="PUB">
															<div class="table-modern--checkbox--wrapper">
																<div class="table-modern--checkbox--check"></div>
																<span class="table-modern--checkbox--label"><%=base.GetLabel("published", "published") %></span>
															</div>
														</asp:LinkButton>
													</div>

													<div class="table-modern--hover--col">
														<asp:LinkButton runat="server" ID="LnkCopy" CausesValidation="false" CommandName="CopyRow" CommandArgument='<%#Eval("Id") %>' class="table-modern--media" ClientIDMode="AutoID" data-title-mobile="COPY">
															<div class="table-modern--media--wrapper">
																<div class="table-modern--media--modify"></div>
																<span class="table-modern--media--label"><%=base.GetLabel("copy", "copy") %></span>
															</div>
														</asp:LinkButton>
													</div>
													<div class="table-modern--hover--col">
														<a href="#" class="table-modern--media delete-label js-delete" data-title-mobile="DEL"
															data-msg-title='<%=base.GetLabel("delete", "delete") %>' 
															data-msg-subtitle='<%=PigeonCms.Utility.GetLabel("RECORD_DELETE_QUESTION") %>' 
															data-msg-cancel='<%=base.GetLabel("cancel", "cancel") %>' 
															data-msg-confirm='<%=base.GetLabel("confirm", "confirm") %>'>
															<div class="table-modern--media--wrapper">
																<div class="table-modern--media--delete"></div>
																<span class="table-modern--media--label"><%=base.GetLabel("delete", "delete") %></span>
															</div>
														</a>
														<asp:Button ID="LnkDel" CommandName="DeleteRow" CommandArgument='<%#Eval("Id") %>' runat="server" style="display:none;"  />
													</div>

												</div>

											</div>
                                            
                                        </div>

									</ItemTemplate>
								</asp:Repeater>

								<div class="table-modern--rowpaging ">
									<asp:Repeater ID="RepPaging" runat="server" OnItemCommand="RepPaging_ItemCommand" OnItemDataBound="RepPaging_ItemDataBound">
										<ItemTemplate>
											<div class="table-modern__col col--paging table-modern__col--paging">
													<asp:LinkButton ID="BtnPage" runat="server" ClientIDMode="AutoID"
													CommandName="Page" CommandArgument="<%# Container.DataItem %>"><%# Container.DataItem %>
													</asp:LinkButton>
											</div>
										</ItemTemplate>
									</asp:Repeater>
								</div>
                
							</div>
						</div>
                </div>

        </asp:Panel>

        <asp:Panel runat="server" ID="PanelInsert" Visible="false">
            
            <div class="panel panel-default panel-modern--insert">

                <div class="panel-modern--scrollable" onscroll="onScrollEditBtns()">

						<div class="panel-heading">
							<span><%=base.GetLabel("LblDetails", "Details") %></span>
							<div class="btn-group clearfix">
								<div class="btn-group-follow clearfix">
									<asp:Button ID="BtnCancel" runat="server" Text="<%$ Resources:PublicLabels, CmdCancel %>" CssClass="btn btn-default btn-xs btn-modern btn-modern--cancel" CausesValidation="false" OnClick="BtnCancel_Click" />
									<asp:Button ID="BtnSave" runat="server" Text="<%$ Resources:PublicLabels, CmdSave %>" CssClass="btn btn-primary btn-xs btn-modern" OnClick="BtnSave_Click" />
									<div class="btn-group-alert">
										<asp:Label ID="LblErrInsert" runat="server" Text=""></asp:Label>
										<asp:Label ID="LblOkInsert" runat="server" Text=""></asp:Label>
									</div>
								</div>
							</div>
						</div>

                        <div class="panel-body">

                            <%--tabs--%>
                            <ul class="nav nav-pills">
                                <li class="active"><a href="#tab-main" data-toggle="tab"><%=base.GetLabel("Main", "Main") %></a></li>
                                <li><a href="#tab-menu" data-toggle="tab"><%=base.GetLabel("Menu", "Menu") %></a></li>
                                <li><a href="#tab-options" data-toggle="tab"><%=base.GetLabel("Options", "Options") %></a></li>
                                <li><a href="#tab-security" data-toggle="tab"><%=base.GetLabel("Security", "Security") %></a></li>
                                <li><a href="#tab-parameters" data-toggle="tab"><%=base.GetLabel("Parameters", "Parameters") %></a></li>
                            </ul>

                            <div class="tab-content">

                                <%--tab-main--%>
                                <div class="tab-pane fade in active" id="tab-main">

                                    <div class="form-group col-sm-12">
                                        <%=base.GetLabel("LblModuleType", "Module type", null, true)%>
                                        <asp:TextBox ID="LitModuleType" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-sm-12">
                                        <%=base.GetLabel("LblName", "Module Name", null, true)%>
                                        <asp:TextBox ID="TxtName" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-sm-12">
                                        <%=base.GetLabel("LblTitle", "Title", null, true) %>
                                        <asp:Panel runat="server" ID="PanelTitle"></asp:Panel>
                                    </div>
                            
                                    <div class="form-group col-sm-6">
                                        <%=base.GetLabel("LblPosition", "Position", DropTemplateBlockName, true)%>
                                        <asp:DropDownList ID="DropTemplateBlockName" CssClass="form-control" runat="server">
                                        </asp:DropDownList></td>
                                    </div>

                                    <div class="form-group col-sm-6">
                                        <%=base.GetLabel("LblOrder", "Order", DropOrdering, true)%>
                                        <asp:DropDownList ID="DropOrdering" CssClass="form-control" runat="server">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-sm-12">
                                        <%=base.GetLabel("LblContent", "Content", TxtContent, true)%>
                                        <asp:TextBox ID="TxtContent" Rows="3" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>

                                </div>

                                <%--tab-menu--%>
                                <div class="tab-pane fade" id="tab-menu">

                                    <div class="form-group col-lg-6">
                                        <%=base.GetLabel("LblMenus", "Menus", null, true) %>
                                        <br />

                                        <asp:RadioButton GroupName="MenuSelection" ID="RadioMenuAll" CssClass="" runat="server" />
                                        <%=base.GetLabel("LblMenuAll", "All", RadioMenuAll, true)%>
                                        <br />
                                
                                        <asp:RadioButton GroupName="MenuSelection" ID="RadioMenuNone" CssClass="" runat="server" />
                                        <%=base.GetLabel("LblMenuNone", "None", RadioMenuNone, true)%>
                                        <br />

                                        <asp:RadioButton GroupName="MenuSelection" ID="RadioMenuSelection" CssClass="" runat="server" />
                                        <%=base.GetLabel("LblMenuSelection", "Select menu items", RadioMenuSelection, true)%>
                                        <br />
                                    </div>

                                    <div class="form-group col-lg-6">
                                        <%=base.GetLabel("LblMenuEntries", "Menu entries", ListMenu, true)%>
                                        <asp:ListBox ID="ListMenu" style="height:300px;" SelectionMode="Multiple" CssClass="form-control" disabled="disabled" Rows="18" runat="server">
                                        </asp:ListBox>
                                    </div>
                         
                                    <div class="form-group col-sm-12">
                                        <%=base.GetLabel("LblViews", "Views", DropViews, true)%>
                                        <a href="javascript:void(0)" onclick="changeView();" id="lnkChangeView" runat="server">
                                            <%=base.GetLabel("LblChange", "change") %>
                                        </a>
                                        <asp:DropDownList ID="DropViews" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                           
                                </div>

                                <%--tab-options--%>
                                <div class="tab-pane tab-detail-item fade" id="tab-options">

                                    <div class="form-group col-sm-12">
                                        <strong><%=base.GetLabel("LblRecordId", "ID") %></strong> 
                                        <asp:Label ID="LblId" runat="server" Text=""></asp:Label>
                                    </div>

                                    <div class="form-group col-sm-12">
                                        <strong><%=base.GetLabel("LblCreated", "Created")%></strong>
                                        <asp:Label ID="LblCreated" runat="server" Text=""></asp:Label>
                                    </div>

                                    <div class="form-group col-sm-12">
                                        <strong><%=base.GetLabel("LblLastUpdate", "Last update")%></strong>
                                        <asp:Label ID="LblUpdated" runat="server" Text=""></asp:Label>
                                    </div>

                                    <div class="form-group col-sm-12">
                                        <asp:CheckBox ID="ChkShowTitle" runat="server" Enabled="true" />
                                        <%=base.GetLabel("LblShowTitle", "Show title", ChkShowTitle, true)%>
                                    </div>

                                    <div class="form-group col-sm-12">
                                        <asp:CheckBox ID="ChkPublished" runat="server" Enabled="true" />
                                        <%=base.GetLabel("LblPublished", "Published", ChkPublished, true)%>
                                    </div>

                                </div>

                                <%--tab-security--%>
                                <div class="tab-pane fade tab-detail-item" id="tab-security">
                                    <uc1:PermissionsControl ID="PermissionsControl1" runat="server" />
                                </div>

                                <%--tab-parameters--%>
                                <div class="tab-pane fade tab-detail-item" id="tab-parameters">
                                    <uc1:ModuleParams ID="ModuleParams1" runat="server" />
                                </div>    

                            </div>

                        </div>
                </div>

            </div>

        </asp:Panel>

    </div>
    
</ContentTemplate>
</asp:UpdatePanel>