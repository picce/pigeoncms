<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Default.ascx.cs" Inherits="Controls_MenuAdmin" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register src="~/Controls/ModuleParams.ascx" tagname="ModuleParams" tagprefix="uc1" %>
<%@ Register src="~/Controls/PermissionsControl.ascx" tagname="PermissionsControl" tagprefix="uc1" %>
<%@ Register Src="~/Controls/SeoControl.ascx" TagName="SeoControl" TagPrefix="uc1" %>



<script type="text/javascript">
    // <!CDATA[

    function preloadTitle(sourceControlName, targetControl)
    {
        if (targetControl.value == "")
            targetControl.value = document.getElementById(sourceControlName).value;
    }


    function preloadAlias(sourceControlName, targetControl) {
        var res = document.getElementById(sourceControlName).value;
        if (targetControl.value == "") {
            res = res.toLowerCase();
            res = res.replace(/\ /g, '-');    //replace all occurs
            targetControl.value = res;
        }
    }

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

    function changeModueleTypes() {
        var elemname = '#<%=DropModuleTypes.ClientID %>';
        $(elemname).removeAttr('disabled');
        $(elemname).removeClass('locked');
        $('#lnkchange').hide();
    }
    
    function changeView() {
        var elemname = '#<%=DropViews.ClientID %>';
        $(elemname).removeAttr('disabled');
        $(elemname).removeClass('locked');
        $('#lnkChangeView').hide();
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
									<div class="form-select-wrapper select-right" runat="server" id="DivDropNewContainer">
										<asp:DropDownList runat="server" ID="DropNewItem" AutoPostBack="true" CssClass="form-control"
											OnSelectedIndexChanged="DropNewItem_SelectedIndexChanged">
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

                                <div class="form-group col-sm-6 col-lg-3 form-select-wrapper">
                                    <asp:DropDownList ID="DropMenuTypesFilter" runat="server" AutoPostBack="true" CssClass="form-control" 
                                        OnSelectedIndexChanged="DropMenuTypesFilter_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-sm-6 col-lg-3 form-select-wrapper">
                                    <asp:DropDownList ID="DropPublishedFilter" runat="server" AutoPostBack="true" CssClass="form-control" 
                                        OnSelectedIndexChanged="DropPublishedFilter_SelectedIndexChanged">
                                    </asp:DropDownList>                            
                                </div>
                                <div class="form-group col-sm-6 col-lg-3 form-select-wrapper">
                                    <asp:DropDownList ID="DropMasterPageFilter" runat="server" AutoPostBack="true" CssClass="form-control" 
                                        OnSelectedIndexChanged="DropMasterPageFilter_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-sm-6 col-lg-3 form-select-wrapper">
                                    <asp:DropDownList ID="DropModuleTypesFilter" runat="server" AutoPostBack="true" CssClass="form-control" 
                                        OnSelectedIndexChanged="DropModuleTypesFilter_SelectedIndexChanged">
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
									<div class="col-sm-3 table-modern__col align-left"><%=base.GetLabel("NameCol", "Name/Alias/Route") %></div>
									<div class="col-sm-2 table-modern__col align-left"><%=base.GetLabel("CssCol", "Css/Theme/Master") %></div>
									<div class="col-sm-3 table-modern__col align-left"><%=base.GetLabel("ModuleCol", "Module") %></div>
									<div class="col-sm-2 table-modern__col align-center"><%=base.GetLabel("Vis-Pub-Ssl", "Vis/Pub/Ssl") %></div>
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
                                                        <strong><%# Eval("Name") %></strong><br />
														<asp:Literal ID="LitAlias" runat="server" />
													</div>
												</div>
											</div>

											<div class="col-sm-2 table-modern__col">
												<div class="table-modern--description" data-menu="cat">
													<div class="table-modern--description--wrapper">
                                                        <span class="table-modern--smallcontent">
														    <asp:Literal ID="LitStyle" runat="server" />
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
													<asp:Literal runat="server" ID="LitVisible"></asp:Literal>
													<asp:Literal runat="server" ID="LitPublished"></asp:Literal>
													<asp:Literal runat="server" ID="LitSsl"></asp:Literal>
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
														<asp:LinkButton runat="server" ID="LnkVisible" CommandArgument='<%#Eval("Id") %>' ClientIDMode="AutoID" data-title-mobile="VIS">
															<div class="table-modern--checkbox--wrapper">
																<div class="table-modern--checkbox--check"></div>
																<span class="table-modern--checkbox--label"><%=base.GetLabel("visible", "visible") %></span>
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
                                <li><a href="#tab-options" data-toggle="tab"><%=base.GetLabel("Options", "Options") %></a></li>
                                <li><a href="#tab-seo" data-toggle="tab"><%=base.GetLabel("Seo", "Seo") %></a></li>
                                <li><a href="#tab-security" data-toggle="tab"><%=base.GetLabel("Security", "Security") %></a></li>
                                <li><a href="#tab-parameters" data-toggle="tab"><%=base.GetLabel("Parameters", "Parameters") %></a></li>
                            </ul>

                            <div class="tab-content">

                                <%--tab-main--%>
                                <div class="tab-pane fade in active" id="tab-main">
                            
                                    <div class="form-group col-sm-6">
                                        <%=base.GetLabel("LblMenuType", "Menu type", LitMenuType, true)%>
                                        <asp:TextBox ID="LitMenuType" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-sm-6 form-select-wrapper form-select-detail-item">
                                        <%=base.GetLabel("LblContentType", "Content type", DropModuleTypes, true)%>
                                        <a href="javascript:void(0)" onclick="changeModueleTypes();" id="lnkchange" runat="server">
                                            <span><%=base.GetLabel("LblChange", "change") %></span>
                                        </a>
                                        <asp:DropDownList ID="DropModuleTypes" CssClass="form-control" runat="server"
                                            AutoPostBack="true" OnSelectedIndexChanged="DropModuleTypes_SelectedIndexChanged"></asp:DropDownList>
                                    </div>

                                    <div class="form-group col-sm-6">
                                        <%=base.GetLabel("LblName", "Name", TxtName, true)%>
                                        <asp:TextBox ID="TxtName" MaxLength="200" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="ReqName" ControlToValidate="TxtName" runat="server" Text="*"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-sm-6">
                                        <%=base.GetLabel("LblAlias", "Alias", TxtAlias, true)%>
                                        <asp:TextBox ID="TxtAlias" Rows="3" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="ReqAlias" ControlToValidate="TxtAlias" runat="server" Text="*"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-sm-6 spacing-detail">
                                        <%=base.GetLabel("LblTitle", "Title", null, true)%>
                                        <asp:Panel runat="server" ID="PanelTitle"></asp:Panel>
                                    </div>

                                    <div class="form-group col-sm-6 spacing-detail">
                                        <%=base.GetLabel("LblTitleWindow", "Window's title", null, true)%>
                                        <asp:Panel runat="server" ID="PanelTitleWindow"></asp:Panel>
                                    </div>

                                    <div class="form-group col-md-6 col-sm-6 form-select-wrapper form-select-detail-item">
                                        <%=base.GetLabel("LblRoute", "Route", DropRouteId, true)%>
                                        <asp:DropDownList ID="DropRouteId" CssClass="form-control" runat="server">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-md-6 col-sm-6 form-select-wrapper form-select-detail-item">
                                        <%=base.GetLabel("LblUseSsl", "Use SSL", DropUseSsl, true)%>
                                        <asp:DropDownList ID="DropUseSsl" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="2" Text="Use route settings"></asp:ListItem>
                                            <asp:ListItem Value="0" Text="No"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                                        </asp:DropDownList>                            
                                    </div>

                                    <div class="form-group col-md-6 col-sm-6">
                                        <%=base.GetLabel("LblLink", "Link", TxtLink, true)%>
                                        <asp:TextBox ID="TxtLink" MaxLength="200" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-md-6 col-sm-6 form-select-wrapper form-select-detail-item">
                                        <%=base.GetLabel("LblRedirectTo", "Redirect to", DropReferMenuId, true)%>
                                        <asp:DropDownList ID="DropReferMenuId" CssClass="form-control" runat="server"></asp:DropDownList>
                                    </div>

                                    <div class="form-group col-sm-12">
                                        <%=base.GetLabel("LblParentItem", "Parent item", ListParentId, true)%>
                                        <asp:ListBox ID="ListParentId" SelectionMode="Single" Height="250" Rows="15" runat="server" CssClass="form-control"></asp:ListBox>
                                    </div>

                                    <div class="form-group col-md-6">
                                        <%=base.GetLabel("LblCssClass", "Css class", TxtCssClass, true)%>
                                        <asp:TextBox ID="TxtCssClass" MaxLength="200" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-md-6 form-select-wrapper form-select-detail-item">
                                        <%=base.GetLabel("LblViews", "Views", DropViews, true)%>
                                        <a href="javascript:void(0)" onclick="changeView();" id="lnkChangeView" runat="server">
                                            <span><%=base.GetLabel("LblChange", "change") %></span>
                                        </a>
                                        <asp:DropDownList ID="DropViews" runat="server" CssClass="form-control"
                                            AutoPostBack="true" OnSelectedIndexChanged="DropViews_SelectedIndexChanged"></asp:DropDownList>
                                    </div>

                                    <div class="form-group col-md-6 form-select-wrapper form-select-detail-item">
                                        <%=base.GetLabel("LblTheme", "Theme", DropCurrTheme, true)%>
                                        <asp:DropDownList ID="DropCurrTheme" CssClass="form-control" runat="server"></asp:DropDownList>
                                    </div>

                                    <div class="form-group col-md-6 form-select-wrapper form-select-detail-item">
                                        <%=base.GetLabel("LblMasterpage", "Masterpage", DropCurrMasterPage, true)%>
                                        <asp:DropDownList ID="DropCurrMasterPage" CssClass="form-control" runat="server"></asp:DropDownList>
                                    </div>

                                </div>

                                <%--tab-option--%>
                                <div class="tab-pane fade" id="tab-options">
                        
                                    <div class="form-group col-sm-12 checkbox-container">
										<span><%=base.GetLabel("LblVisible", "Visible")%></span>
                                        <asp:CheckBox ID="ChkVisible" runat="server" Enabled="true" />
                                        <%=base.GetLabel("LblVisible", "Visible", ChkVisible, true)%>
                                    </div>

                                    <div class="form-group col-sm-12 checkbox-container">
										<span><%=base.GetLabel("LblPublished", "Published")%></span>
                                        <asp:CheckBox ID="ChkPublished" runat="server" Enabled="true" />
                                        <%=base.GetLabel("LblPublished", "Published", ChkPublished, true)%>
                                    </div>

                                    <div class="form-group col-sm-12 checkbox-container">
                                        <asp:CheckBox ID="ChkOverridePageTitle" runat="server" Enabled="true" />
                                        <%=base.GetLabel("LblOverrideTitle", "Override window title", ChkOverridePageTitle, true)%>
                                    </div>

                                    <div class="form-group col-sm-12 checkbox-container">
                                        <asp:CheckBox ID="ChkShowModuleTitle" runat="server" Enabled="true" />
                                        <%=base.GetLabel("LblShowModuleTitle", "Show module title", ChkShowModuleTitle, true)%>
                                    </div>

                                </div>   
                        
                                <%--tab seo --%>
                                <div class="tab-pane fade " id="tab-seo">

									<uc1:SeoControl ID="SeoControl1" runat="server" />

								</div>

                                <%--tab-security--%>
                                <div class="tab-pane fade tab-detail-item" id="tab-security">
                                    
                                    <div class="form-group col-sm-12 no-padding">
                                        <asp:CheckBox ID="ChkIsCore" runat="server" Enabled="false" />
                                        <strong>Core</strong>
                                    </div>

                                    <div class="form-group col-sm-12 no-padding">
                                        <strong>ID</strong>
                                        <asp:Label ID="LblId" runat="server" Text=""></asp:Label>
                                    </div>

                                    <div class="form-group col-sm-12 no-padding">
                                        <strong>Module Id</strong>
                                        <asp:Label ID="LblModuleId" runat="server" Text=""></asp:Label>
                                    </div>

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
