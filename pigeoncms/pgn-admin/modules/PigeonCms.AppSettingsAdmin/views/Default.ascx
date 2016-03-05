<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Default.ascx.cs" Inherits="Controls_Default" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="true"></cc1:ToolkitScriptManager>
<asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="1" AssociatedUpdatePanelID="Upd1">
    <ProgressTemplate>
        <div class="loading"><%=PigeonCms.Utility.GetLabel("LblLoading", "loading") %></div>
    </ProgressTemplate>
</asp:UpdateProgress>
    

<asp:UpdatePanel ID="Upd1" runat="server">
<ContentTemplate>


	<%--pigeonmodern--%>
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
										CssClass="btn btn-primary clearfix" OnClick="BtnNew_Click" />
									<asp:Button ID="BtnApplySettings" runat="server" Text="<%$ Resources:PublicLabels, CmdApply %>" 
										CssClass="btn btn-primary" OnClick="BtnApply_Click" />

								</div>
							</div>

						</div>
					</div>
				</div>

				<%--INIT accordion--%>
				<div class="row">

					<div class="panel clearfix"></div>
					<div class="panel-group" id="accordion">

                    <asp:Repeater runat="server" ID="RepGroups" OnItemDataBound="RepGroups_ItemDataBound" OnItemCommand="RepGroups_ItemCommand">
                        <ItemTemplate>

                            <div class='panel <%# Eval("PanelClass") %>'>

                                <div class="panel-heading">

                                    <div class="pull-right">
                                        <div class="btn-group">
                                            <asp:Button runat="server" ID="BtnUpdateDbVersion" CssClass="btn btn-default btn-xs" 
                                                CommandName="UpdateVersion"
                                                Visible="false" Text="Upgrade version" />
                                        </div>
                                    </div>

                                    <h4 class="panel-title">
                        
                                        <i class='<%# Eval("IconClass") %> fa-fw'></i>
                                        <a data-toggle="collapse" data-parent="#accordion" href='#AccordionGroup<%# Eval("Row") %>'>
                                            <%# Eval("Title") %>
                                        </a><br />
                                        <span class="small text-muted">
                                            <%# Eval("Abstract") %><br />
                                            <asp:Literal runat="server" ID="LitVersionInfo"></asp:Literal>
                                        </span>
                                    </h4>
                                </div>
                                
                                <div id='AccordionGroup<%# Eval("Row") %>' class='panel-collapse <%#Eval("CollapseClass") %>'>
                                    

										<%--INIT newtable --%>
										<div class="table-modern">
											<div class="table-modern--wrapper table-modern--mobile table-modern--sortable">

												<div class="table-modern--row table-modern--row-title">

													<div class="table-modern__col table-modern__col--small align-left"></div>
													<div class="table-modern__col align-left"><%=base.GetLabel("Key", "Key")%></div>
													<div class="table-modern__col align-left"><%=base.GetLabel("Title", "Title")%></div>
													<div class="table-modern__col align-left table-modern__col--large"><%=base.GetLabel("Value", "Value")%></div>
													<div class="table-modern__col align-left table-modern__col--small"></div>

												</div>

												<asp:Repeater runat="server" ID="RepSettings" OnItemDataBound="RepSettings_ItemDataBound" OnItemCommand="RepSettings_ItemCommand">
													<ItemTemplate>

														<div class="table-modern--row">

															<%--#action edit --%>
															<div class="table-modern__col table-modern__col--small">
																<asp:LinkButton runat="server" ID="LnkEdit" CausesValidation="false" CommandName="Select" class="table-modern--media" ClientIDMode="AutoID" data-title-mobile="EDIT">
																	<div class="table-modern--media--wrapper">
																		<div class="table-modern--media--modify"></div>
																		<span class="table-modern--media--label"><%=base.GetLabel("edit", "edit") %></span>
																	</div>
																</asp:LinkButton>
															</div>

															<div class="table-modern__col">
																<div class="table-modern--description" data-menu="key">
																	<div class="table-modern--description--wrapper">
																		<strong><%#Eval("KeyName") %></strong>
																	</div>
																</div>
															</div>

															<div class="table-modern__col">
																<div class="table-modern--description" data-menu="title">
																	<div class="table-modern--description--wrapper">
																		<%#Eval("KeyTitle") %>
																	</div>
																</div>
															</div>

															<div class="table-modern__col table-modern__col--large">
																<div class="table-modern--description" data-menu="value">
																	<div class="table-modern--description--wrapper">
																		<asp:Literal ID="LblKeyValue" runat="server" /><br />
																	</div>
																</div>
															</div>
											
															<%--#action delete --%>
															<div class="table-modern__col table-modern__col--small" runat="server" id="ColDelete">
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
																<asp:Button ID="LnkDel" CommandName="DeleteRow" runat="server" style="display:none;"  />
															</div>

														</div>
													</ItemTemplate>
												</asp:Repeater>

											</div>
										</div>
										<%--END newtable--%>

                                </div>
                            </div>

                        </ItemTemplate>
                    </asp:Repeater>

					</div>

				</div>
                <%--EDN accordion--%>
				
		</asp:Panel>

		<asp:Panel runat="server" ID="PanelInsert" Visible="false">

			<div class="panel panel-default panel-modern--insert">

					<div class="panel-modern--scrollable" onscroll="onScrollEditBtns()">

						<div class="panel-heading">
							<span><%=base.GetLabel("LblDetails", "Details")%></span>
							<span class="title-modern-insert"><asp:Literal runat="server" ID="LitInsertTitle"></asp:Literal></span>
							<div class="btn-group clearfix">
								<div class="btn-group-follow clearfix">
									<asp:Button ID="BtnCancel" runat="server" Text="<%$ Resources:PublicLabels, CmdCancel %>" CssClass="btn btn-default btn-modern btn-modern--cancel" CausesValidation="false" OnClick="BtnCancel_Click" />
									<asp:Button ID="BtnSave" runat="server" Text="<%$ Resources:PublicLabels, CmdSave %>" CssClass="btn btn-primary btn-modern" OnClick="BtnSave_Click" />

									<div class="btn-group-alert">
										<asp:Label ID="LblErrInsert" runat="server" Text=""></asp:Label>
										<asp:Label ID="LblOkInsert" runat="server" Text=""></asp:Label>
									</div>
								</div>
							</div>
						</div>

						<div class="panel-body">

							<div class="form-group col-md-4 form-select-wrapper form-select-detail-item">
								<%=base.GetLabel("LblKeySet", "KeySet", DropKeySet, true)%>
								<asp:DropDownList runat="server" ID="DropKeySet" CssClass="form-control"></asp:DropDownList>
							</div>

							<div class="form-group col-md-4">
								<%=base.GetLabel("LblName", "Name", TxtKeyName, true)%>
								<asp:TextBox ID="TxtKeyName" MaxLength="50" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
							</div>

							<div class="form-group col-md-4">
								<%=base.GetLabel("LblTitle", "Title", TxtKeyTitle, true)%>
								<asp:TextBox ID="TxtKeyTitle" MaxLength="500" runat="server" CssClass="form-control"></asp:TextBox>
							</div>

							<div class="form-group col-md-12 spacing-detail">
								<%=base.GetLabel("LblValue", "Value", null, true)%>
								<asp:Panel ID="PanelValue" runat="server"></asp:Panel>
							</div>

							<div class="form-group col-md-12">
								<%=base.GetLabel("LblInfo", "Additional info", TxtKeyInfo, true)%>
								<asp:TextBox ID="TxtKeyInfo" MaxLength="500" runat="server" CssClass="form-control" Rows="3" TextMode="MultiLine"></asp:TextBox>
							</div>

						</div>

					</div>

			</div>

		</asp:Panel>

	</div>


</ContentTemplate>
</asp:UpdatePanel>