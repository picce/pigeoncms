<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Default.ascx.cs" Inherits="Controls_Default" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:ScriptManager runat="server" EnablePageMethods="true"></asp:ScriptManager>
<asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="1" AssociatedUpdatePanelID="Upd1">
    <ProgressTemplate>
        <div class="loading"><%=PigeonCms.Utility.GetLabel("LblLoading", "loading") %></div>
    </ProgressTemplate>
</asp:UpdateProgress>

<script type="text/javascript">
    
    var deleteQuestion = '<%=PigeonCms.Utility.GetLabel("RECORD_DELETE_QUESTION") %>';

    function moveListItem(list1, list2)
    {
        var i;
        for (i = list1.length-1; i>=0; i--) 
        {
            if (list1.options[i].selected) {
                var opt = document.createElement("option");
                opt.value = list1.options[i].value;
                opt.text = list1.options[i].text;

                list2.add(opt, null);
                list1.remove(i);
            }
        }
    }
    
    function refreshHidden()
    {
        var hidden = document.getElementById('<%=HiddenUsersInRole.ClientID %>');
        var list1 = document.getElementById('<%=ListUsersInRole.ClientID %>');
        var i;
        hidden.value = "";
        for (i = list1.length-1; i>=0; i--) 
        {
            hidden.value += list1.options[i].value;
            if (i > 0) hidden.value += "|";
        }        
    }

    function addUser()
    {
        moveListItem(
            document.getElementById('<%=ListUsersNotInRole.ClientID %>'), 
            document.getElementById('<%=ListUsersInRole.ClientID %>')
            );
        refreshHidden();
    }
    
    function removeUser()
    {
        moveListItem(
            document.getElementById('<%=ListUsersInRole.ClientID %>'), 
            document.getElementById('<%=ListUsersNotInRole.ClientID %>')
            );
        refreshHidden();
    }
</script>
    

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
									<div class="table-modern__col col-md-3 align-left"><%=base.GetLabel("Role", "Role")%></div>
									<div class="table-modern__col col-md-6 align-left"><%=base.GetLabel("Users", "Users")%></div>
									<div class="table-modern__col align-right col-md-1"><%=base.GetLabel("UsersCount", "# Users")%></div>
									<div class="table-modern__col col-md-1"></div><%--del--%>

								</div>

								<asp:Repeater runat="server" ID="Rep1" OnItemDataBound="Rep1_ItemDataBound" OnItemCommand="Rep1_ItemCommand">
									<ItemTemplate>

										<div class="table-modern--row">

											<%--#action edit --%>

											<div class="table-modern__col col-md-1">
												<asp:LinkButton runat="server" ID="LnkEdit" CausesValidation="false" CommandName="Select" CommandArgument='<%#Eval("Role") %>' class="table-modern--media" ClientIDMode="AutoID" data-title-mobile="EDIT">
													<div class="table-modern--media--wrapper">
														<div class="table-modern--media--modify"></div>
													</div>
												</asp:LinkButton>
											</div>

											<div class="table-modern__col align-left col-md-3">

												<div class="table-modern--description" data-menu="role">
													<div class="table-modern--description--wrapper">
														<%#Eval("Role")%>
													</div>
												</div>

											</div>

											<div class="table-modern__col col-md-6">
												<div class="table-modern--description" data-menu="users">
													<div class="table-modern--description--wrapper">
														<span class="table-modern--smallcontent">
															<asp:Literal ID="LitUsersInRole" runat="server" />
														</span>
													</div>
												</div>
											</div>


											<div class="col-sm-1 table-modern__col align-right">
												<div class="table-modern--checkbox" data-menu="count">
													<asp:Literal runat="server" ID="LitNumUsersInRole"></asp:Literal>
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
												<asp:Button ID="LnkDel" CommandName="DeleteRow" CommandArgument='<%#Eval("Role")%>' runat="server" style="display:none;"  />
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

							<div class="form-group col-md-12">

								<%=base.GetLabel("LblRoleName", "Rolename", TxtRolename)%>                        
								<asp:TextBox ID="TxtRolename" MaxLength="255" runat="server" CssClass="form-control"></asp:TextBox>

							</div>


						</div>

					</div>

			</div>

		</asp:Panel>

		<asp:Panel runat="server" ID="PanelUsers" Visible="false">

			<div class="panel panel-default panel-modern--insert">

					<div class="panel-modern--scrollable" onscroll="onScrollEditBtns()">

						<div class="panel-heading">
							<span>
								<%=base.GetLabel("LblUsersInRole", "Users in role") %>
							</span>
							<span class="title-modern-insert"><asp:Literal runat="server" ID="Literal1"></asp:Literal></span>
							<div class="btn-group clearfix">
								<div class="btn-group-follow clearfix">
									<asp:Button ID="Button3" runat="server" Text="<%$ Resources:PublicLabels, CmdCancel %>" CssClass="btn btn-default btn-xs btn-modern btn-modern--cancel" CausesValidation="false" OnClick="BtnCancel_Click" />
									<asp:Button ID="Button4" runat="server" Text="<%$ Resources:PublicLabels, CmdSave %>" CssClass="btn btn-primary btn-xs btn-modern" OnClick="BtnSave_Click" />
									<div class="btn-group-alert">
										<asp:Label ID="LblErrUser" runat="server" Text=""></asp:Label>
										<asp:Label ID="LblOkUser" runat="server" Text=""></asp:Label>
									</div>
								</div>
							</div>
						</div>

						<div class="panel-body">

							<div class="form-group col-md-12">
								<%=base.GetLabel("LblRoleName", "Rolename", TxtRolenameUser)%>                        
								<asp:TextBox ID="TxtRolenameUser" MaxLength="255" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
							</div>

							<div class="form-group col-md-6 align-center">

								<label><%=base.GetLabel("LblUsers", "Users")%></label>

								<asp:ListBox ID="ListUsersNotInRole" SelectionMode="Multiple" Height="250" Rows="18" 
									CssClass="form-control" runat="server">
								</asp:ListBox>
								<br />
								<br />
								<input type="button" id="BtnAddUser" onclick="addUser();" value=">>>" class="btn btn-primary btn-xs btn-modern" />
							</div>

							<div class="form-group col-md-6 align-center">

								<label><%=base.GetLabel("LblUsersInRole", "Users in roles")%></label>

								<asp:ListBox ID="ListUsersInRole" SelectionMode="Multiple" Height="250" Rows="18" 
									CssClass="form-group form-control" runat="server">
								</asp:ListBox>
								<asp:HiddenField ID="HiddenUsersInRole" runat="server" />

								<br />
								<br />
								<input type="button" id="BtnRemoveUser" onclick="removeUser();" value="<<<" class="btn btn-primary btn-xs btn-modern" />
							</div>

						</div>

					</div>

			</div>

		</asp:Panel>

	</div>

</ContentTemplate>
</asp:UpdatePanel>