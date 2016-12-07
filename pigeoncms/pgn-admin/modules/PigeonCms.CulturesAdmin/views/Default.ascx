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

<asp:ScriptManager runat="server" EnablePageMethods="true"></asp:ScriptManager>
<asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="1" AssociatedUpdatePanelID="Upd1">
	<ProgressTemplate>
		<div class="loading"><%=PigeonCms.Utility.GetLabel("LblLoading", "loading") %></div>
	</ProgressTemplate>
</asp:UpdateProgress>


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

                            <div class="form-group col-md-6">
                                <asp:DropDownList runat="server" ID="DropEnabledFilter" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="DropEnabledFilter_SelectedIndexChanged">
                                    <asp:ListItem Value="">--Enabled--</asp:ListItem>
                                    <asp:ListItem Value="1">True</asp:ListItem>
                                    <asp:ListItem Value="0">False</asp:ListItem>
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
								<div class="col-sm-3 table-modern__col align-left"><%=base.GetLabel("CultureCode", "Culture Code") %></div>
								<div class="col-sm-3 table-modern__col align-left"><%=base.GetLabel("ShortCode", "Short Code") %></div>
								<div class="col-sm-4 table-modern__col align-left"><%=base.GetLabel("DisplayName", "Display Name") %></div>
								<div class="col-sm-2 table-modern__col align-center"><%=base.GetLabel("Enabled", "Enabled") %></div>
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
										<input type="hidden" name="RowId" value='<%# Eval("CultureCode") %>' />

										<div class="col-sm-3 table-modern__col">
											<div class="table-modern--description" data-menu="culture">
												<div class="table-modern--description--wrapper">
													<strong><%# Eval("CultureCode") %></strong>
												</div>
											</div>
										</div>

										<div class="col-sm-3 table-modern__col">
											<div class="table-modern--description" data-menu="short">
												<div class="table-modern--description--wrapper">
													<%# Eval("ShortCode") %>
												</div>
											</div>
										</div>

										<div class="col-sm-4 table-modern__col">
											<div class="table-modern--description" data-menu="name">
												<div class="table-modern--description--wrapper">
													<%# Eval("DisplayName") %>
												</div>
											</div>
										</div>

										<div class="col-sm-2 table-modern__col">
											<div class="table-modern--checkbox" data-menu="enabled">
												<asp:Literal runat="server" ID="LitEnabled"></asp:Literal>
											</div>
										</div>


										<div class="table-modern--hover">

											<a href="javascript:void(0)" class="table-modern-edit--close"><span></span></a>

											<div class="table-modern--hover--wrapper clearfix">

												<div class="table-modern--hover--col">
													<asp:LinkButton runat="server" ID="LnkEdit" CausesValidation="false" CommandName="Select" CommandArgument='<%#Eval("CultureCode") %>' class="table-modern--media" ClientIDMode="AutoID" data-title-mobile="EDIT">
														<div class="table-modern--media--wrapper">
															<div class="table-modern--media--modify"></div>
															<span class="table-modern--media--label"><%=base.GetLabel("edit", "edit") %></span>
														</div>
													</asp:LinkButton>
												</div>

												<div class="table-modern--hover--col">
													<asp:LinkButton runat="server" ID="LnkPublished" CommandArgument='<%#Eval("CultureCode") %>' ClientIDMode="AutoID" data-title-mobile="PUB">
														<div class="table-modern--checkbox--wrapper">
															<div class="table-modern--checkbox--check"></div>
															<span class="table-modern--checkbox--label"><%=base.GetLabel("Enabled", "Enabled") %></span>
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
													<asp:Button ID="LnkDel" CommandName="DeleteRow" CommandArgument='<%#Eval("CultureCode") %>' runat="server" style="display:none;"  />
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
								<asp:Button ID="BtnSave" runat="server" Text="<%$ Resources:PublicLabels, CmdCancel %>" CssClass="btn btn-default btn-xs btn-modern btn-modern--cancel" CausesValidation="false" OnClick="BtnCancel_Click" />
								<asp:Button ID="BtnCancel" runat="server" Text="<%$ Resources:PublicLabels, CmdSave %>" CssClass="btn btn-primary btn-xs btn-modern" OnClick="BtnSave_Click" />
								<div class="btn-group-alert">
									<asp:Label ID="LblErrInsert" runat="server" Text=""></asp:Label>
									<asp:Label ID="LblOkInsert" runat="server" Text=""></asp:Label>
								</div>
							</div>
						</div>
					</div>

					<div class="panel-body">

						<div class="form-group col-md-6">
							<%=base.GetLabel("CultureCode", "Culture code", TxtCultureCode)%>
							<asp:TextBox ID="TxtCultureCode" MaxLength="10" runat="server" CssClass="form-control"></asp:TextBox>
						</div>

						<div class="form-group col-md-6">
							<%=base.GetLabel("ShortCode", "Short code", TxtShortCode)%>
							<asp:TextBox ID="TxtShortCode" MaxLength="10" runat="server" CssClass="form-control"></asp:TextBox>
						</div>

						<div class="form-group col-md-12">
							<%=base.GetLabel("DisaplyName", "Display name", TxtDisplayName)%>
							<asp:TextBox ID="TxtDisplayName" MaxLength="50" runat="server" CssClass="form-control"></asp:TextBox>
						</div>

						<div class="form-group col-md-12 checkbox-container">
							<asp:CheckBox ID="ChkEnabled" runat="server" Enabled="true" />
							<%=base.GetLabel("LblEnabled", "Enabled", ChkEnabled, true)%>
						</div>

					</div>

				</div>

			</div>

		</asp:Panel>

    </div>

</ContentTemplate>
</asp:UpdatePanel>