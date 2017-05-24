<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Default.ascx.cs" Inherits="Controls_Default" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

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


								<div class="form-group col-sm-6 col-lg-3 form-select-wrapper ">
									<asp:DropDownList runat="server" ID="DropTopRowsFilter" AutoPostBack="true" CssClass="form-control" 
									OnSelectedIndexChanged="Filter_Changed"></asp:DropDownList>
								</div>
								<div class="form-group col-sm-6 col-lg-3 form-select-wrapper ">
									<asp:DropDownList runat="server" ID="DropTracerItemTypeFilter" AutoPostBack="true" CssClass="form-control" 
									OnSelectedIndexChanged="Filter_Changed"></asp:DropDownList>
								</div>
								<div class="form-group col-sm-6 col-lg-3 form-select-wrapper ">
									<asp:DropDownList runat="server" ID="DropModuleTypesFilter"  AutoPostBack="true" CssClass="form-control" 
									OnSelectedIndexChanged="Filter_Changed"></asp:DropDownList>
								</div>
								<div class="form-group col-sm-6 col-lg-3 form-select-wrapper ">
									<asp:DropDownList runat="server" ID="DropDatesRangeFilter" AutoPostBack="true" CssClass="form-control" 
									OnSelectedIndexChanged="Filter_Changed"></asp:DropDownList>
								</div>
								<div class="form-group col-sm-6 col-lg-4">
									<asp:TextBox ID="TxtIpFilter" runat="server" placeholder="<IP address>" AutoPostBack="true" CssClass="form-control" 
									ontextchanged="Filter_Changed"></asp:TextBox>
								</div>
								<div class="form-group col-sm-6 col-lg-4">
									<asp:TextBox ID="TxtSessionIdFilter" runat="server" placeholder="<Session ID>" AutoPostBack="true" CssClass="form-control" 
									ontextchanged="Filter_Changed"></asp:TextBox>
								</div>
								<div class="form-group col-sm-12 col-lg-4">
									<asp:TextBox ID="TxtDescriptionFilter" runat="server" placeholder="<description>" AutoPostBack="true" CssClass="form-control" 
									ontextchanged="Filter_Changed"></asp:TextBox>
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

								    <div class="table-modern__col col-sm-1"></div>
								    <div class="table-modern__col col-sm-2">IP / Session</div>
								    <div class="table-modern__col col-sm-4">ID - User / Module / Url</div>
								    <div class="table-modern__col col-sm-5">Data</div>

							    </div>

							    <asp:Repeater runat="server" ID="Rep1" OnItemDataBound="Rep1_ItemDataBound" OnItemCommand="Rep1_ItemCommand">
								    <ItemTemplate>

									    <div class="table-modern--row">

										    <%--#action edit --%>
										    <div class="table-modern__col col-sm-1">

											    <asp:LinkButton runat="server" ID="LnkEdit" CausesValidation="false" CommandName="Select" 
												    CommandArgument='<%#Eval("Id") %>' class="table-modern--media" ClientIDMode="AutoID" data-title-mobile="EDIT">
												    <div class="table-modern--media--wrapper">
													    <div class="table-modern--media--show"></div>
												    </div>
											    </asp:LinkButton>

										    </div>

										    <div class="table-modern__col col-sm-2">
											    <div class="table-modern--description" data-menu="IP/SESSION">
												    <div class="table-modern--description--wrapper">
													    <span class="table-modern--date">
														    <asp:Literal runat="server" ID="LitDateInserted"></asp:Literal>	
													    </span>
													    <asp:Literal ID="LitItemInfo" runat="server" />
												    </div>
											    </div>
										    </div>


										    <div class="table-modern__col col-sm-4">
											    <div class="table-modern--description" data-menu="usr/mod/url">
												    <div class="table-modern--description--wrapper">
													    <span class="table-modern--smallcontent">
														    <asp:Literal runat="server" ID="LitType"></asp:Literal>
														    <strong><asp:Literal ID="LitId" runat="server" Text=""></asp:Literal></strong><br>
														    <asp:Literal ID="LitModule" runat="server" Text=""></asp:Literal><br>
														    <asp:Literal ID="LitUrl" runat="server" Text=""></asp:Literal>
													    </span>
												    </div>
											    </div>
										    </div>


										    <div class="table-modern__col col-sm-5">
											    <div class="table-modern--description" data-menu="data">
												    <div class="table-modern--description--wrapper">
													    <span class="table-modern--smallcontent">
														    <asp:Literal ID="LitDescription" runat="server" Text=""></asp:Literal>
													    </span>
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
							<span><%=base.GetLabel("LblDetails", "Details")%></span>
							<span class="title-modern-insert"><asp:Literal runat="server" ID="LitInsertTitle"></asp:Literal></span>

							<div class="btn-group clearfix">
								<div class="align-center clearfix">
									<asp:Button ID="BtnCancel" runat="server" Text="Back" CssClass="btn btn-primary" CausesValidation="false" OnClick="BtnCancel_Click" />
								</div>
							</div>
						</div>

						<div class="panel-body">

							<div class="form-group col-md-4">
								<%=base.GetLabel("LblRecordId", "ID", LitId) %>
								<asp:TextBox ID="LitId" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
							</div>

							<div class="form-group col-md-4">
								<%=base.GetLabel("LblCreated", "Created", LitCreated) %>
								<asp:TextBox ID="LitCreated" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
							</div>

							<div class="form-group col-md-4">
								<strong><%=base.GetLabel("LblType", "Type", LitType) %></strong>
								<asp:TextBox ID="LitType" enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
							</div>

							<div class="form-group col-sm-6">
								<%=base.GetLabel("LblModuleType", "Module", LitModuleType) %>
								<asp:TextBox ID="LitModuleType" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
							</div>

							<div class="form-group col-sm-6">
								<%=base.GetLabel("LblView", "View", LitView) %>
								<asp:TextBox ID="LitView" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
							</div>

							<div class="form-group col-sm-4">
								<%=base.GetLabel("LblIp", "Ip", LitUserHostAddress) %>
								<asp:TextBox ID="LitUserHostAddress" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
							</div>

							<div class="form-group col-sm-4">
								<%=base.GetLabel("LblSessionId", "Session ID", LitSessionId) %>
								<asp:TextBox ID="LitSessionId" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
							</div>

							<div class="form-group col-sm-4">
								<%=base.GetLabel("LblUser", "User", LitUserInserted) %>
								<asp:TextBox ID="LitUserInserted" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
							</div>
                    
							<div class="form-group col-sm-12">
								<%=base.GetLabel("LblUrl", "Url", LitUrl) %>
								<asp:TextBox ID="LitUrl" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
							</div>

							<div class="form-group col-sm-12">
								<%=base.GetLabel("LblDescription", "Description", LitDescription) %>
								<asp:TextBox ID="LitDescription" CssClass="form-control" TextMode="MultiLine" Rows="6" Enabled="false" runat="server"></asp:TextBox>
							</div>

						</div>

					</div>

				</div>

		</asp:Panel>

	</div>



</ContentTemplate>
</asp:UpdatePanel>