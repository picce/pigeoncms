<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Default.ascx.cs" Inherits="Controls_CategoriesAdmin" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register src="~/Controls/PermissionsControl.ascx" tagname="PermissionsControl" tagprefix="uc1" %>
<%--<%@ Register src="~/Controls/CategoriesTreeControl.ascx" tagname="CategoriesTreeControl" tagprefix="uc1" %>--%>


<script type="text/javascript">
// <!CDATA[

	function pageLoad(sender, args) {
		$(document).ready(function () {

			mst_initSort();

		});
	}

	function mod_UpdateSortedTable() {
		var upd1 = '<%=Upd1.ClientID%>';
		if (upd1 != null) {
			__doPostBack(upd1, 'sortcomplete');
		}
	}

	function mod_ReloadUpd1() {
		var upd1 = '<%=Upd1.ClientID%>';
		if (upd1 != null) {
			__doPostBack(upd1, 'categories');
		}
	}


	function onSuccess(result) { }
	function onFailure(result) { }

// ]]>
</script>

<asp:ScriptManager runat="server" EnablePageMethods="true"></asp:ScriptManager>
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
									<asp:Button ID="BtnNew" runat="server" Text="<%$ Resources:PublicLabels, CmdNew %>"
										Visible="false" CssClass="btn btn-primary" OnClick="BtnNew_Click" />
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
								<div class="form-group col-lg-12form-select-wrapper">
									<asp:DropDownList ID="DropSectionsFilter" runat="server" AutoPostBack="true" CssClass="form-control"
										OnSelectedIndexChanged="DropSectionsFilter_SelectedIndexChanged">
									</asp:DropDownList>
								</div>
							</div>
						</div>
					</div>
				</div>



				<%--#list--%>
				<div class="col-lg-12">

						<%--MOBILE--%>
						<div class="table-modern">
							<div class="table-modern--wrapper table-modern--mobile table-modern--sortable">

								<div class="table-modern--row table-modern--row-title">

									<a href="javascript:void(0)" class="table-modern-edit"><span></span></a>
									<div class="col-sm-2 table-modern__col align-left"><%=base.GetLabel("Title", "Title") %></div>
									<div class="col-sm-2 table-modern__col align-left"><%=base.GetLabel("Info", "Info") %></div>
									<div class="col-sm-1 table-modern__col align-center"><%=base.GetLabel("Enabled", "Enabled") %></div>
									<div class="col-sm-2 table-modern__col align-center"><%=base.GetLabel("Images", "Images") %></div>
									<div class="col-sm-2 table-modern__col align-center"><%=base.GetLabel("Files", "Files") %></div>
									<div class="col-sm-1 table-modern__col align-center"><%=base.GetLabel("Permissions", "Permissions") %></div>
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

											<div class="col-sm-2 table-modern__col">
												<div class="table-modern--description" data-menu="tit">
													<div class="table-modern--description--wrapper">
														<strong><%# Eval("Name") %></strong><br />
														<asp:Literal ID="LitTitle" runat="server" /><br />
													</div>
												</div>
											</div>

											<div class="col-sm-2 table-modern__col">
												<div class="table-modern--description" data-menu="info">
													<div class="table-modern--description--wrapper">
														<asp:Literal ID="LitItemInfo" runat="server" />
													</div>
												</div>
											</div>

											<div class="col-sm-1 table-modern__col">
												<div class="table-modern--checkbox" data-menu="enab">
													<asp:Literal runat="server" ID="LitEnabled"></asp:Literal>
												</div>
											</div>

											<div class="col-sm-2 table-modern__col align-center">
												<div class="table-modern--media" data-menu="img">
													<div class="table-modern--media--wrapper">
														<div class="table-modern--media--images"></div>
														<span class="table-modern--media--label">
															<asp:Literal ID="LitImgCount" runat="server" Text=""></asp:Literal></span>
														</span>
													</div>
												</div>
											</div>

											<div class="col-sm-2 table-modern__col align-center">
												<div class="table-modern--media" data-menu="file">
													<div class="table-modern--media--wrapper">
														<div class="table-modern--media--files"></div>
														<span class="table-modern--media--label">
															<asp:Literal ID="LitFilesCount" runat="server" Text=""></asp:Literal></span>
														</span>
													</div>
												</div>
											</div>
											
											<div class="col-sm-1 table-modern__col">
												<div class="table-modern__col--value" data-menu="sec">
													<asp:Literal ID="LitAccessTypeDesc" runat="server" Text=""></asp:Literal>

												</div>
											</div>

											<div class="table-modern--hover">

												<a href="javascript:void(0)" class="table-modern-edit--close"><span></span></a>

												<div class="table-modern--hover--wrapper clearfix">

													<div class="table-modern--hover--col">
														<asp:LinkButton runat="server" ID="LnkEnabled" CommandArgument='<%#Eval("Id") %>' ClientIDMode="AutoID" data-title-mobile="PUB">
															<div class="table-modern--checkbox--wrapper">
																<div class="table-modern--checkbox--check"></div>
																<span class="table-modern--checkbox--label"><%=base.GetLabel("enabled", "enabled") %></span>
															</div>
														</asp:LinkButton>
													</div>
													<div class="table-modern--hover--col">
														<asp:LinkButton runat="server" ID="LnkEdit" CausesValidation="false" CommandName="Select" CommandArgument='<%#Eval("Id") %>' class="table-modern--media" ClientIDMode="AutoID" data-title-mobile="EDIT">
															<div class="table-modern--media--wrapper">
																<div class="table-modern--media--modify"></div>
																<span class="table-modern--media--label"><%=base.GetLabel("edit", "edit") %></span>
															</div>
														</asp:LinkButton>
													</div>
													<div class="table-modern--hover--col">
														<asp:HyperLink runat="server" ID="LnkUploadImg" class="table-modern--media js-open-fancy" ClientIDMode="AutoID" data-title-mobile="IMG">
															<div class="table-modern--media--wrapper">
																<div class="table-modern--media--images"></div>
																<span class="table-modern--media--label"><%=base.GetLabel("images", "images") %></span>
															</div>
														</a>
														</asp:HyperLink>
													</div>
													<div class="table-modern--hover--col">
														<asp:HyperLink runat="server" ID="LnkUploadFiles" class="table-modern--media js-open-fancy" ClientIDMode="AutoID" data-title-mobile="DOCS">
															<div class="table-modern--media--wrapper">
																<div class="table-modern--media--files"></div>
																<span class="table-modern--media--label"><%=base.GetLabel("documents", "documents") %></span>
															</div>
														</asp:HyperLink>
													</div>
													<div class="table-modern--hover--col">
														<a href="javascript:void(0);" class="table-modern--media js-move" data-title-mobile="MOVE">
															<div class="table-modern--media--wrapper">
																<div class="table-modern--media--move"></div>
																<span class="table-modern--media--label"><%=base.GetLabel("move", "move") %></span>
															</div>
														</a>
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
							<span class="title-modern-insert"><%=TitleItem %></span>
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

							<ul class="nav nav-pills">
								<li class="active"><a href="#tab-main" data-toggle="tab"><%=base.GetLabel("Main", "Main") %></a></li>
								<li><a href="#tab-security" data-toggle="tab"><%=base.GetLabel("Security", "Security") %></a></li>
							</ul>

							<div class="tab-content">

								<div class="tab-pane fade in active" id="tab-main">

									<div class="form-group col-md-6 col-lg-3">
										<%=base.GetLabel("LblSection", "Section", LitSection, true)%>
										<asp:TextBox ID="LitSection" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
									</div>

									<div class="form-group col-md-6 col-lg-3">
										<%=base.GetLabel("LblExtId", "External Id", TxtExtId, true)%>
										<asp:TextBox ID="TxtExtId" runat="server" Enabled="true" CssClass="form-control"></asp:TextBox>
									</div>

                                    <div class="form-group col-md-6 col-lg-3">
                                        <%=base.GetLabel("LblCssClass", "Css class", TxtCssClass, true)%>
                                        <asp:TextBox ID="TxtCssClass" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                    </div>

									<div class="form-group col-md-6 col-lg-3 checkbox-container">
										<span><%=base.GetLabel("LblEnabled", "Enabled")%></span>
										<asp:CheckBox ID="ChkEnabled" runat="server" Enabled="true" Text="" />
										<%=base.GetLabel("LblEnabledLabel", "Abilita elemento", ChkEnabled, true)%>
									</div>

                                    <div class="form-group col-sm-12">
                                        <%=base.GetLabel("LblParentItem", "Parent category", ListParentId, true)%>
                                        <asp:ListBox ID="ListParentId" SelectionMode="Single" Rows="10" runat="server" CssClass="form-control"></asp:ListBox>
                                    </div>

									<div class="form-group col-md-12 col-lg-12 spacing-detail">
										<%=base.GetLabel("LblTitle", "Title", null, true)%>
										<asp:Panel runat="server" ID="PanelTitle"></asp:Panel>
									</div>

									<div class="form-group col-sm-12 spacing-detail">
										<div class="form-text-wrapper">
											<%=base.GetLabel("LblDescription", "Description", null, true)%>
											<asp:Panel runat="server" ID="PanelDescription"></asp:Panel>
										</div>
									</div>

                                </div>

								<div class="tab-pane fade tab-detail-item" id="tab-security">

									<div class="form-group col-sm-12 no-padding">
										<strong><%=base.GetLabel("LblRecordId", "ID") %>:</strong>&nbsp;
                                            <asp:Label ID="LblId" runat="server" Text=""></asp:Label>
									</div>

									<div class="form-group col-sm-12 no-padding">
										<strong><%=base.GetLabel("LblCreated", "Created") %>:</strong>&nbsp;
                                            <asp:Label ID="LblCreated" runat="server" Text=""></asp:Label>
									</div>

									<div class="form-group col-sm-12 no-padding">
										<strong><%=base.GetLabel("LblLastUpdate", "Last update") %>:</strong>&nbsp;
                                            <asp:Label ID="LblUpdated" runat="server" Text=""></asp:Label>
									</div>

									<div class="form-group col-sm-12 no-padding">
										<strong><%=base.GetLabel("LblOrderId", "Order Id") %>:</strong>&nbsp;
                                            <asp:Label ID="LblOrderId" runat="server" Text=""></asp:Label>
									</div>

									<uc1:PermissionsControl ID="PermissionsControl1" runat="server" />

								</div>

                            </div>

                        </div>

                    </div>

                </div>

            </asp:Panel>

        </div>

    </ContentTemplate>
</asp:UpdatePanel>