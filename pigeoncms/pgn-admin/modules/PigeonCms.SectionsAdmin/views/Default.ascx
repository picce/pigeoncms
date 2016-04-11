<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Default.ascx.cs" Inherits="Controls_SectionsAdmin" %>
<%@ Register src="~/Controls/PermissionsControl.ascx" tagname="PermissionsControl" tagprefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<script type="text/javascript">
// <!CDATA[

    function changeItemType() {
        var elemname = '#<%=DropItemType.ClientID %>';
        $(elemname).removeAttr('disabled');
        $('#lnkchange').hide();
    }

    var deleteQuestion = '<%=PigeonCms.Utility.GetLabel("RECORD_DELETE_QUESTION") %>';

// ]]>
</script>

<cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="true"></cc1:ToolkitScriptManager>
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

							<div class="form-group col-lg-6 col-md-12 form-select-wrapper">
								<asp:DropDownList runat="server" ID="DropEnabledFilter" AutoPostBack="true" CssClass="form-control" 
									OnSelectedIndexChanged="DropEnabledFilter_SelectedIndexChanged">
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
							<div class="col-sm-1 table-modern__col align-center"><%=base.GetLabel("Enabled", "Enabled") %></div>
							<div class="col-sm-3 table-modern__col align-left"><%=base.GetLabel("Title", "Title") %></div>
							<div class="col-sm-2 table-modern__col align-center"><%=base.GetLabel("Images", "Images") %></div>
							<div class="col-sm-2 table-modern__col align-center"><%=base.GetLabel("Files", "Files") %></div>
							<div class="col-sm-2 table-modern__col align-center"><%=base.GetLabel("Permissions", "Permissions") %></div>
							<div class="col-sm-2 table-modern__col align-center"><%=base.GetLabel("Quota", "Quota") %></div>
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

									<div class="col-sm-1 table-modern__col">
										<div class="table-modern--checkbox" data-menu="enab">
											<asp:Literal runat="server" ID="LitEnabled"></asp:Literal>
										</div>
									</div>

									<div class="col-sm-3 table-modern__col">
										<div class="table-modern--description" data-menu="tit">
											<div class="table-modern--description--wrapper">
												<strong><asp:Literal ID="LitTitle" runat="server" /></strong>
                                                <br>
												<asp:Literal ID="LitItemInfo" runat="server" />
											</div>
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
											
									<div class="col-sm-2 table-modern__col">
										<div class="table-modern__col--value" data-menu="sec">
											<asp:Literal ID="LitAccessTypeDesc" runat="server" Text=""></asp:Literal>

										</div>
									</div>

									<div class="col-sm-2 table-modern__col">
										<div class="table-modern--description" data-menu="disk">
											<div class="table-modern--description--wrapper">
												<asp:Literal ID="LitDiskSpace" runat="server" /><br />
												<asp:Literal ID="LitItems" runat="server" /><br />
											</div>
										</div>
									</div>

									<div class="table-modern--hover">

										<a href="javascript:void(0)" class="table-modern-edit--close"><span></span></a>

										<div class="table-modern--hover--wrapper clearfix">

											<div class="table-modern--hover--col">
												<asp:LinkButton runat="server" ID="LnkEnabled" CommandArgument='<%#Eval("Id")%>' ClientIDMode="AutoID" data-title-mobile="PUB">
													<div class="table-modern--checkbox--wrapper">
														<div class="table-modern--checkbox--check"></div>
														<span class="table-modern--checkbox--label"><%=base.GetLabel("enabled", "enabled")%></span>
													</div>
												</asp:LinkButton>
											</div>
											<div class="table-modern--hover--col">
												<asp:LinkButton runat="server" ID="LnkEdit" CausesValidation="false" CommandName="Select" CommandArgument='<%#Eval("Id")%>' class="table-modern--media" ClientIDMode="AutoID" data-title-mobile="EDIT">
													<div class="table-modern--media--wrapper">
														<div class="table-modern--media--modify"></div>
														<span class="table-modern--media--label"><%=base.GetLabel("edit", "edit")%></span>
													</div>
												</asp:LinkButton>
											</div>
											<div class="table-modern--hover--col">
												<asp:HyperLink runat="server" ID="LnkUploadImg" class="table-modern--media js-open-fancy" ClientIDMode="AutoID" data-title-mobile="IMG">
													<div class="table-modern--media--wrapper">
														<div class="table-modern--media--images"></div>
														<span class="table-modern--media--label"><%=base.GetLabel("images", "images")%></span>
													</div>
												</a>
												</asp:HyperLink>
											</div>
											<div class="table-modern--hover--col">
												<asp:HyperLink runat="server" ID="LnkUploadFiles" class="table-modern--media js-open-fancy" ClientIDMode="AutoID" data-title-mobile="DOCS">
													<div class="table-modern--media--wrapper">
														<div class="table-modern--media--files"></div>
														<span class="table-modern--media--label"><%=base.GetLabel("documents", "documents")%></span>
													</div>
												</asp:HyperLink>
											</div>
											<div class="table-modern--hover--col">
												<a href="#" class="table-modern--media delete-label js-delete" data-title-mobile="DEL"
													data-msg-title='<%=base.GetLabel("delete", "delete")%>' 
													data-msg-subtitle='<%=PigeonCms.Utility.GetLabel("RECORD_DELETE_QUESTION")%>' 
													data-msg-cancel='<%=base.GetLabel("cancel", "cancel")%>' 
													data-msg-confirm='<%=base.GetLabel("confirm", "confirm")%>'>
													<div class="table-modern--media--wrapper">
														<div class="table-modern--media--delete"></div>
														<span class="table-modern--media--label"><%=base.GetLabel("delete", "delete")%></span>
													</div>
												</a>
												<asp:Button ID="LnkDel" CommandName="DeleteRow" CommandArgument='<%#Eval("Id")%>' runat="server" style="display:none;"  />
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
											CommandName="Page" CommandArgument="<%# Container.DataItem %>"><%# Container.DataItem%>
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

								<div class="form-group col-lg-3 col-md-6 checkbox-container">
									<span><%=base.GetLabel("LblEnabled", "Enabled")%></span>
                                    <asp:CheckBox ID="ChkEnabled" runat="server" Enabled="true" />
									<%=base.GetLabel("LblEnabledLabel", "Enable section", ChkEnabled, true)%>
								</div>

                                <div class="form-group col-lg-3 col-md-6">
                                    <%=base.GetLabel("LblExtId", "External Id", TxtExtId, true)%>
                                    <asp:TextBox ID="TxtExtId" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 form-select-wrapper form-select-detail-item">
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

                                <div class="form-group col-md-6">
                                    <%=base.GetLabel("LblMaxItems", "Max items", TxtMaxItems, true)%>
                                    <asp:TextBox ID="TxtMaxItems" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>

                                <div class="form-group col-md-6">
                                    <%=base.GetLabel("LblMaxAttachSizeKB", "Max size for attachments (KB)", TxtMaxAttachSizeKB, true)%>
                                    <asp:TextBox ID="TxtMaxAttachSizeKB" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>

                                <div class="form-group col-md-12 spacing-detail">
                                    <%=base.GetLabel("LblTitle", "Title", null, true)%>
                                    <asp:Panel runat="server" ID="PanelTitle"></asp:Panel>
                                </div>

                                <div class="form-group col-md-12">
                                    <%=base.GetLabel("LblDescription", "Description", null, true)%>
                                    <asp:Panel runat="server" ID="PanelDescription"></asp:Panel>
                                </div>


							</div>

							<div class="tab-pane fade tab-detail-item" id="tab-security">

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
