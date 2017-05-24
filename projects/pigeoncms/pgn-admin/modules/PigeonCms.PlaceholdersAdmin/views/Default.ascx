<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Default.ascx.cs" Inherits="Controls_Default" %>


<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
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

									<asp:Button ID="BtnNew" runat="server" Text="<%$ Resources:PublicLabels, CmdNew %>" CssClass="btn btn-primary clearfix" OnClick="BtnNew_Click" />

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

                                    <div class="table-modern__col col-md-1"></div><%--edit--%>
									<div class="table-modern__col align-left col-md-3"><%=base.GetLabel("Name", "Name")%></div>
                                    <div class="table-modern__col align-left col-md-6"><%=base.GetLabel("ContentPreview", "Content preview")%></div>
									<div class="table-modern__col col-md-1"><%=base.GetLabel("Enabled", "Enabled")%></div>
									<div class="table-modern__col col-md-1"></div><%--del--%>

								</div>

                                <%-- REP here--%>
                                <asp:Repeater runat="server" ID="Rep1" OnItemDataBound="Rep1_ItemDataBound" OnItemCommand="Rep1_ItemCommand">
									<ItemTemplate>

										<div class="table-modern--row">

											<%--#action edit --%>
									        <div class="table-modern__col col-md-1">
										        <asp:LinkButton runat="server" ID="LnkEdit" CausesValidation="false" CommandName="Select" CommandArgument='<%#Eval("Name") %>' class="table-modern--media" ClientIDMode="AutoID" data-title-mobile="EDIT">
											        <div class="table-modern--media--wrapper">
												        <div class="table-modern--media--modify"></div>
											        </div>
										        </asp:LinkButton>
									        </div>

											<div class="table-modern__col align-left col-md-3">
												<div class="table-modern--description" data-menu="name">
													<div class="table-modern--description--wrapper">
														<%#Eval("Name")%>
													</div>
												</div>
											</div>


											<div class="table-modern__col col-md-6">
												<div class="table-modern--description" data-menu="content">
													<div class="table-modern--description--wrapper">
														<span class="table-modern--smallcontent">
															<asp:Literal ID="LitContent" runat="server" />
														</span>
													</div>
												</div>
											</div>


											<div class="col-sm-1 table-modern__col">
												<div class="table-modern--checkbox" data-menu="enabled">
													<asp:Literal runat="server" ID="LitEnabled"></asp:Literal>
												</div>
											</div>

											
											<%--#action delete --%>
											<div class="table-modern__col col-md-1">
												<a href="#" class="table-modern--media delete-label js-delete" data-title-mobile="DEL"
													data-msg-title='<%=base.GetLabel("delete", "delete")%>' 
													data-msg-subtitle='<%=PigeonCms.Utility.GetLabel("RECORD_DELETE_QUESTION")%>' 
													data-msg-cancel='<%=base.GetLabel("cancel", "cancel")%>' 
													data-msg-confirm='<%=base.GetLabel("confirm", "confirm")%>'>
													<div class="table-modern--media--wrapper">
														<div class="table-modern--media--delete"></div>
													</div>
												</a>
												<asp:Button ID="LnkDel" CommandName="DeleteRow" CommandArgument='<%#Eval("Name")%>' runat="server" style="display:none;"  />
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
						<span><%=base.GetLabel("LblDetails", "Details")%></span>
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

                        <div class="form-group col-md-12">
                            <%=base.GetLabel("LblName", "Name", TxtName, true)%>
                            <asp:TextBox ID="TxtName" MaxLength="50" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                        </div>

                        <div class="form-group col-md-12">
                            <%=base.GetLabel("LblContent", "Content", null, true)%>
                            <asp:TextBox ID="TxtContent" MaxLength="500" runat="server" CssClass="form-control" Rows="10" TextMode="MultiLine"></asp:TextBox>
                        </div>

                        <div class="form-group col-md-12 checkbox-container">
                            <asp:CheckBox ID="ChkVisibile" runat="server" Enabled="true" />
                            <%=base.GetLabel("LblVisible", "Visible", ChkVisibile, true)%>
                        </div>

                    </div>

                </div>

            </div>

        </asp:Panel>

    </div>

</ContentTemplate>
</asp:UpdatePanel>