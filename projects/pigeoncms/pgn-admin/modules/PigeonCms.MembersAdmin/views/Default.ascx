<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Default.ascx.cs" Inherits="Controls_Default" %>
<%@ Register src="~/Controls/MemberEditorControl.ascx" tagname="MemberEditor" tagprefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<script type="text/javascript">
    // <!CDATA[

    function pageLoad(sender, args) {
        $(document).ready(function () {

            mst_initSort();

            //subscribe global search
            $(document).on('search.pigeon', function (e, data) {
                var upd1 = '<%=Upd1.ClientID%>';
                if (upd1 != null) {
                    __doPostBack(upd1, 'search.pigeon|' + data.value);
                }
            })

        });
    }

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
    

	<%--pigeonmodern--%>
	<div class="row">

		<asp:Panel runat="server" ID="PanelSee">

                <asp:HiddenField runat="server" ID="MasterFilter" />


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

				<%--#filters--%>
				<div class="col-lg-12">
					<div class="panel panel-default panel-filter">
						<div class="panel-heading">
							<h4 class="panel-title">
								<a data-toggle="collapse" data-parent="#accordion" href="#collapseOne">
									<%=PigeonCms.Utility.GetLabel("LblFilters")%>
									<span>
										<font class="close-span"><%=base.GetLabel("LblClose", "Close")%></font>
										<font class="open-span"><%=base.GetLabel("LblOpen", "Open")%></font>
									</span>
								</a>
							</h4>

						</div>
						<div id="collapseOne" class="panel-collapse collapse in">
							<div class="panel-body">

								<div class="form-group col-md-6">
								    <asp:TextBox ID="TxtUserNameFilter" runat="server" AutoPostBack="true" CssClass="form-control" 
										ontextchanged="TxtUserNameFilter_TextChanged" placeholder="<username>"></asp:TextBox>
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

									<div class="table-modern__col align-left col-md-2"><%=base.GetLabel("UsernameEmail", "Username/email")%></div>
									<div class="table-modern__col align-center col-md-2"><%=base.GetLabel("MetaInfo", "Meta info")%></div>
									<div class="table-modern__col align-left col-md-2"><%=base.GetLabel("Roles", "Roles")%></div>
									<div class="table-modern__col col-md-1"><%=base.GetLabel("ChangePwd", "Change password")%></div><%--#action pwd--%>
									<div class="table-modern__col col-md-1"><%=base.GetLabel("ChangeRoles", "Change roles")%></div><%--#action roles--%>
									<div class="table-modern__col col-md-1"><%=base.GetLabel("Enabled", "Enabled")%></div>
									<div class="table-modern__col col-md-1"><%=base.GetLabel("Approved", "Approved")%></div>
									<div class="table-modern__col col-md-1"><%=base.GetLabel("IsCore", "Core")%></div>
									<div class="table-modern__col col-md-1"></div><%--del--%>

								</div>

								<asp:Repeater runat="server" ID="Rep1" OnItemDataBound="Rep1_ItemDataBound" OnItemCommand="Rep1_ItemCommand">
									<ItemTemplate>

										<div class="table-modern--row">

											<%--#action edit --%>
											<div class="table-modern__col align-left col-md-2">

												<div class="table-modern--description" data-menu="user">
													<div class="table-modern--description--wrapper">

														<asp:Literal runat="server" ID="LitEdit"></asp:Literal>
												
														<asp:LinkButton runat="server" ID="LnkEdit" CausesValidation="false" CommandName="Select" CommandArgument='<%#Eval("Username")%>' class="table-modern--media" ClientIDMode="AutoID" data-title-mobile="EDIT">
															<%#Eval("Username")%>
														</asp:LinkButton>
														<asp:Literal ID="LitMoreInfo" runat="server" />
													</div>
												</div>

											</div>


											<div class="table-modern__col col-md-2">
                                                <div class="table-modern--description" data-menu="meta">
	                                                <div class="table-modern--description--wrapper">
														<span class="table-modern--smallcontent">
															<asp:Literal ID="LitMeta" runat="server" />
														</span>
													</div>
												</div>
											</div>

											<div class="table-modern__col col-md-2">
												<div class="table-modern--description" data-menu="permissions">
													<div class="table-modern--description--wrapper">
														<span class="table-modern--smallcontent">
															<asp:Literal ID="LitPermissions" runat="server" />
														</span>
													</div>
												</div>
											</div>

											<div class="table-modern__col col-md-1">
												<div class="table-modern__col--value" data-menu="password">
													<asp:LinkButton ID="LnkPwd" runat="server" CausesValidation="false" CommandName="Password" CommandArgument='<%#Eval("UserName") %>'>
														<i class='fa fa-pgn_key fa-fw'></i>                                    
													</asp:LinkButton>
												</div>
											</div>

											<div class="table-modern__col col-md-1">
												<div class="table-modern__col--value" data-menu="roles">
													<asp:LinkButton ID="LnkRoles" runat="server" CausesValidation="false" CommandName="Roles" CommandArgument='<%#Eval("UserName") %>'>
														<i class='fa fa-pgn_roles fa-fw'></i>                                    
													</asp:LinkButton>  
												</div>
											</div>

											<div class="col-sm-1 table-modern__col">
												<div class="table-modern--checkbox" data-menu="enabled">
													<asp:Literal runat="server" ID="LitEnabled"></asp:Literal>
												</div>
											</div>

											<div class="col-sm-1 table-modern__col">
												<div class="table-modern--checkbox" data-menu="approved">
													<asp:Literal runat="server" ID="LitApproved"></asp:Literal>
												</div>
											</div>

											<div class="col-sm-1 table-modern__col">
												<div class="table-modern--checkbox" data-menu="is core">
													<asp:Literal runat="server" ID="LitIsCore"></asp:Literal>
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
												<asp:Button ID="LnkDel" CommandName="DeleteRow" CommandArgument='<%#Eval("Username")%>' runat="server" style="display:none;"  />
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
									<asp:Button ID="BtnSave" runat="server" Text="<%$ Resources:PublicLabels, CmdSave %>" CssClass="btn btn-primary btn-xs btn-modern" OnClick="BtnSave_Click" OnClientClick="MyObject.UpdateEditorFormValue();" />
									<div class="btn-group-alert">
										<asp:Label ID="LblErrInsert" runat="server" Text=""></asp:Label>
										<asp:Label ID="LblOkInsert" runat="server" Text=""></asp:Label>
									</div>
								</div>
							</div>
						</div>

						<div class="panel-body">

							<uc1:MemberEditor ID="MemberEditor1" runat="server" />

						</div>

					</div>

			</div>

		</asp:Panel>

	</div>

</ContentTemplate>
</asp:UpdatePanel>