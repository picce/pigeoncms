<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Default.ascx.cs" Inherits="Controls_Default" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

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
			__doPostBack(upd1, 'items');
		}
	}


	function onSuccess(result) { }
	function onFailure(result) { }

	// ]]>
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
									<asp:Button ID="BtnNew" runat="server" Text="<%$ Resources:PublicLabels, CmdNew %>"
										CssClass="btn btn-primary" OnClick="BtnNew_Click" />
									<asp:Button ID="BtnApplySettings" runat="server" Text="<%$ Resources:PublicLabels, CmdApply %>" 
										CssClass="btn btn-primary" OnClick="BtnApply_Click" />
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
                                <asp:DropDownList runat="server" ID="DropPublishedFilter" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="DropPublishedFilter_SelectedIndexChanged">
                                    <asp:ListItem Value="">--Published--</asp:ListItem>
                                    <asp:ListItem Value="1">On-line</asp:ListItem>
                                    <asp:ListItem Value="0">Off-line</asp:ListItem>
                                </asp:DropDownList>

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
									<div class="col-sm-4 table-modern__col align-left"><%=base.GetLabel("Name-Patter", "Name / Pattern") %></div>
									<div class="col-sm-2 table-modern__col align-left"><%=base.GetLabel("Theme-MasterPage", "Theme/Masterpage") %></div>
									<div class="col-sm-2 table-modern__col align-left"><%=base.GetLabel("PageHandler", "Page Handler") %></div>
									<div class="col-sm-1 table-modern__col align-center"><%=base.GetLabel("Published", "Published") %></div>
									<div class="col-sm-1 table-modern__col align-center"><%=base.GetLabel("UseSsl", "UseSsl") %></div>
									<div class="col-sm-1 table-modern__col align-center"><%=base.GetLabel("Core", "Core") %></div>
									<div class="col-sm-1 table-modern__col align-left"><%=base.GetLabel("ID", "ID") %></div>
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

											<div class="col-sm-4 table-modern__col">
												<div class="table-modern--description" data-menu="name">
													<div class="table-modern--description--wrapper">
														<strong><%# Eval("Name") %></strong><br />
														<%# Eval("Pattern") %>
													</div>
												</div>
											</div>

											<div class="col-sm-2 table-modern__col">
												<div class="table-modern--description" data-menu="theme">
													<div class="table-modern--description--wrapper">
                                                        <span class="table-modern--smallcontent">
                                                            <asp:Literal runat="server" ID="LitTheme"></asp:Literal>
                                                        </span>
													</div>
												</div>
											</div>

											<div class="col-sm-2 table-modern__col">
												<div class="table-modern--description" data-menu="master">
													<div class="table-modern--description--wrapper">
                                                        <span class="table-modern--smallcontent">
                                                            <asp:Literal runat="server" ID="LitHandler"></asp:Literal>
                                                        </span>
													</div>
												</div>
											</div>

											<div class="col-sm-1 table-modern__col">
												<div class="table-modern--checkbox" data-menu="pub">
													<asp:Literal runat="server" ID="LitPublished"></asp:Literal>
												</div>
											</div>

											<div class="col-sm-1 table-modern__col">
												<div class="table-modern--checkbox" data-menu="ssl">
													<asp:Literal runat="server" ID="LitUseSsl"></asp:Literal>
												</div>
											</div>

											<div class="col-sm-1 table-modern__col">
												<div class="table-modern--checkbox" data-menu="core">
													<asp:Literal runat="server" ID="LitCore"></asp:Literal>
												</div>
											</div>

											<div class="col-sm-1 table-modern__col">
												<div class="table-modern--description" data-menu="id">
													<div class="table-modern--description--wrapper">
														<%# Eval("Id") %>
													</div>
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
																<span class="table-modern--checkbox--label"><%=base.GetLabel("Published", "Published") %></span>
															</div>
														</asp:LinkButton>
													</div>

													<div class="table-modern--hover--col">
														<asp:LinkButton runat="server" ID="LnkUseSsl" CommandArgument='<%#Eval("Id") %>' ClientIDMode="AutoID" data-title-mobile="SSL">
															<div class="table-modern--checkbox--wrapper">
																<div class="table-modern--checkbox--check"></div>
																<span class="table-modern--checkbox--label"><%=base.GetLabel("UseSsl", "UseSsl") %></span>
															</div>
														</asp:LinkButton>
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
							<span class="title-modern-insert"><asp:Literal runat="server" ID="LitTitle"></asp:Literal></span>
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

							<div class="form-group col-md-6">
								<%=base.GetLabel("LblName", "Name", TxtName, true)%>
								<asp:TextBox ID="TxtName" MaxLength="50" runat="server" CssClass="form-control"></asp:TextBox>
							</div>

							<div class="form-group col-md-6">
								<%=base.GetLabel("LblPattern", "Pattern", TxtPattern, true)%>
								<asp:TextBox ID="TxtPattern" MaxLength="255" runat="server" CssClass="form-control"></asp:TextBox>
							</div>

							<div class="form-group col-md-6 form-select-wrapper form-select-detail-item">
								<%=base.GetLabel("LblTheme", "Theme", DropTheme, true)%>
								<asp:DropDownList ID="DropTheme" CssClass="form-control" runat="server"></asp:DropDownList>
							</div>

							<div class="form-group col-md-6 form-select-wrapper form-select-detail-item">
								<%=base.GetLabel("LblMasterpage", "Masterpage", DropMasterPage, true)%>
								<asp:DropDownList ID="DropMasterPage" CssClass="form-control" runat="server"></asp:DropDownList>
							</div>

							<div class="form-group col-md-6">
								<%=base.GetLabel("LblAssemblyPath", "Assembly path", TxtAssemblyPath, true)%>
								<asp:TextBox ID="TxtAssemblyPath" MaxLength="255" runat="server" CssClass="form-control"></asp:TextBox>
							</div>

							<div class="form-group col-md-6">
								<%=base.GetLabel("LblHandlerName", "Handler name", TxtHandlerName, true)%>
								<asp:TextBox ID="TxtHandlerName" MaxLength="255" runat="server" CssClass="form-control"></asp:TextBox>
							</div>

							<div class="form-group col-md-12 checkbox-container">
								<asp:CheckBox ID="ChkPublished" runat="server" Enabled="true" />
								<%=base.GetLabel("LblEnabled", "Enabled", ChkPublished, true)%>
							</div>

							<div class="form-group col-md-12 checkbox-container">
								<asp:CheckBox ID="ChkUseSsl" runat="server" Enabled="true" />
								<%=base.GetLabel("LblUseSsl", "Use SSL", ChkUseSsl, true)%>
							</div>

							<div class="form-group col-md-12 checkbox-container">
								<asp:CheckBox ID="ChkIsCore" runat="server" enabled="false" />
								<%=base.GetLabel("LblIsCore", "Core", ChkIsCore, true)%>
							</div>



						</div>

					</div>

				</div>

			</asp:Panel>

		</div>

</ContentTemplate>
</asp:UpdatePanel>