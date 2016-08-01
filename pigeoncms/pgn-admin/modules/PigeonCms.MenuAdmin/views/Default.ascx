<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Default.ascx.cs" Inherits="Controls_MenuAdmin" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register src="~/Controls/ModuleParams.ascx" tagname="ModuleParams" tagprefix="uc1" %>
<%@ Register src="~/Controls/PermissionsControl.ascx" tagname="PermissionsControl" tagprefix="uc1" %>


<script type="text/javascript">
    // <!CDATA[

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
									<div class="col-sm-2 table-modern__col align-left"><%=base.GetLabel("NameCol", "Name/Alias/Route") %></div>
									<div class="col-sm-2 table-modern__col align-left"><%=base.GetLabel("CssCol", "Css/Theme/Master") %></div>
									<div class="col-sm-2 table-modern__col align-left"><%=base.GetLabel("ModuleCol", "Module") %></div>
									<div class="col-sm-2 table-modern__col align-center"><%=base.GetLabel("Images", "Images") %></div>
									<div class="col-sm-2 table-modern__col align-center"><%=base.GetLabel("Files", "Files") %></div>
									<div class="col-sm-1 table-modern__col align-center"><%=base.GetLabel("Enabled", "Enabled") %></div>
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

											<div class="col-sm-1 table-modern__col">
												<div class="table-modern--checkbox" data-menu="enab">
													<asp:Literal runat="server" ID="LitEnabled"></asp:Literal>
												</div>
											</div>

											<div class="col-sm-2 table-modern__col">
												<div class="table-modern--description" data-menu="tit">
													<div class="table-modern--description--wrapper">
														<strong><%# Eval("Alias") %></strong><br />
														<asp:Literal ID="LitTitle" runat="server" /><br />
													</div>
												</div>
											</div>

											<div class="col-sm-2 table-modern__col">
												<div class="table-modern--description" data-menu="cat">
													<div class="table-modern--description--wrapper">
														<asp:Literal ID="LitCategoryTitle" runat="server" /><br />
													</div>
												</div>
											</div>

											<div class="col-sm-2 table-modern__col">
												<div class="table-modern--description" data-menu="info">
													<div class="table-modern--description--wrapper">
														<span class="table-modern--date">
															<asp:Literal ID="LitItemDate" runat="server" />
														</span>
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


    </div>

    <div class="row">
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0" OnActiveViewChanged="MultiView1_ActiveViewChanged">
    
        <asp:View ID="ViewSee" runat="server">

            <div class="col-lg-12">

                <div class="panel panel-default">
                    <div class="table-responsive">
            

                    <asp:GridView ID="Grid1" runat="server" Width="100%" AllowPaging="True" AllowSorting="false" AutoGenerateColumns="False"
                        DataSourceID="ObjDs1" DataKeyNames="Id"  OnRowCommand="Grid1_RowCommand" OnRowCreated="Grid1_RowCreated" OnRowDataBound="Grid1_RowDataBound">
                        <Columns>

                            <%--0--%>
                            <asp:TemplateField HeaderText="Name/Alias/Route" SortExpression="Alias">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LnkSel" runat="server" CommandName="Select" CommandArgument='<%#Eval("Id") %>'></asp:LinkButton>
                                    <br />
                                    <span class="small text-muted">
                                        <asp:Literal ID="LitAlias" runat="server" Text=""></asp:Literal>
                                    </span>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%--1--%>
                            <asp:TemplateField HeaderText="Css/Theme/Master">
                                <ItemTemplate>
                                <span class="small text-muted">
                                    <asp:Literal ID="LitStyle" runat="server" Text=""></asp:Literal>
                                </span>
                                </ItemTemplate>
                            </asp:TemplateField>
                    
                            <asp:TemplateField HeaderText="Module name">
                                <ItemTemplate>
                                <span class="small text-muted">
                                    <asp:Literal ID="LitModuleNameDesc" runat="server" Text=""></asp:Literal>
                                </span>
                                </ItemTemplate>
                            </asp:TemplateField>
                    
                            <%--3--%>
                            <asp:TemplateField ItemStyle-Width="10" ItemStyle-HorizontalAlign="Center" HeaderText="Content">
                                <ItemTemplate>
                                    <div class="fancy">
                                    <asp:HyperLink runat="server" ID="LnkModuleContent" Visible="false">
                                        <i class='fa fa-pgn_content fa-fw'></i>                            
                                    </asp:HyperLink>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                    
                            <%--10--%>
                            <asp:TemplateField HeaderText="Vis" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="ImgVisibleOk" runat="server" CommandName="ImgVisibleOk" Visible="false" CommandArgument='<%#Eval("Id") %>'>
                                        <i class='fa fa-pgn_checked fa-fw'></i>
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="ImgVisibleKo" runat="server" CommandName="ImgVisibleKo" Visible="false" CommandArgument='<%#Eval("Id") %>'>
                                        <i class='fa fa-pgn_unchecked fa-fw'></i>                            
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%--10--%>
                            <asp:TemplateField HeaderText="Pub" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="ImgPublishedOk" runat="server" CommandName="ImgPublishedOk" Visible="false" CommandArgument='<%#Eval("Id") %>'>
                                        <i class='fa fa-pgn_checked fa-fw'></i>
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="ImgPublishedKo" runat="server" CommandName="ImgPublishedKo" Visible="false" CommandArgument='<%#Eval("Id") %>'>
                                        <i class='fa fa-pgn_unchecked fa-fw'></i>                            
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Access">
                                <ItemTemplate>
                                <span class="small text-muted">
                                    <asp:Literal ID="LitAccessTypeDesc" runat="server" Text=""></asp:Literal>
                                </span>
                                </ItemTemplate>
                            </asp:TemplateField>
                    
                            <asp:TemplateField Visible="false" HeaderText="Access Level" SortExpression="AccessCode, AccessLevel">
                                <ItemTemplate>
                                <asp:Literal ID="LitAccessLevel" runat="server" Text=""></asp:Literal>
                                </ItemTemplate>
                            </asp:TemplateField>
                    


                            <asp:TemplateField HeaderText="" Visible="true" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="25">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LnkCopy" runat="server" CommandName="CopyRow" CommandArgument='<%#Eval("Id") %>'>
                                        <i class='fa fa-pgn_copy fa-fw'></i>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="SSL" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <i class='fa fa-pgn_security fa-fw' runat="server" id="ImgUseSsl" visible="false"></i>
                                    <%--<asp:Image runat="server" ID="ImgUseSsl" SkinID="ImgSecurity" Visible="false" />--%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Core" SortExpression="IsCore" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                <asp:CheckBox Enabled="false" Checked='<%#Eval("IsCore") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>                    
                    
                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="25">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LnkDel" runat="server" CommandName="DeleteRow" 
                                        CommandArgument='<%#Eval("Id") %>' OnClientClick="return confirm(deleteQuestion);">
                                        <i class='fa fa-pgn_delete fa-fw'></i>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                    
                            <asp:BoundField DataField="Id" Visible="false" HeaderText="ID" SortExpression="Id" />
                        </Columns>
                    </asp:GridView>
                
                    </div>
                </div>
            </div>

            <asp:ObjectDataSource ID="ObjDs1" runat="server" SortParameterName="" OnSelecting="ObjDs1_Selecting"
                SelectMethod="GetTree" TypeName="PigeonCms.MenuManager">
                <SelectParameters>
                    <asp:Parameter Name="filter" Type="object" />
                    <asp:Parameter Name="level" Type="Int32" DefaultValue="-1" />
                    <asp:Parameter Name="separatorText" Type="String" DefaultValue="--" />
                </SelectParameters>
                <DeleteParameters>
                    <asp:Parameter Name="Id" Type="Int32" />
                </DeleteParameters>
            </asp:ObjectDataSource>

        </asp:View>
   

        <asp:View ID="ViewInsert" runat="server">

            <div class="panel panel-default">
                
                <div class="panel-heading clearfix">
                    <%=base.GetLabel("LblDetails", "Details") %>
                    <div class="pull-right">
                        <div class="btn-group">
                            <asp:Button ID="BtnSave" CssClass="btn btn-primary" runat="server" Text="<%$ Resources:PublicLabels, CmdSave %>" OnClick="BtnSave_Click" />
                            <asp:Button ID="BtnApply" CssClass="btn btn-default" runat="server" Text="<%$ Resources:PublicLabels, CmdApply %>" OnClick="BtnApply_Click" />
                            <asp:Button ID="BtnCancel" CssClass="btn btn-default" CausesValidation="false" runat="server" Text="<%$ Resources:PublicLabels, CmdCancel %>" OnClick="BtnCancel_Click" />
                        </div>
                    </div>
                </div>

                <div class="panel-body">

                    <ul class="nav nav-pills">
                        <li class="active"><a href="#tab-main" data-toggle="tab"><%=base.GetLabel("Main", "Main") %></a></li>
                        <li><a href="#tab-options" data-toggle="tab"><%=base.GetLabel("Options", "Options") %></a></li>
                        <li><a href="#tab-security" data-toggle="tab"><%=base.GetLabel("Security", "Security") %></a></li>
                        <li><a href="#tab-parameters" data-toggle="tab"><%=base.GetLabel("Parameters", "Parameters") %></a></li>
                    </ul>

                    <div class="tab-content row">

                        <div class="tab-pane fade in active" id="tab-main">
                            <h4></h4>
                            
                            <div class="form-group col-sm-6">
                                <%=base.GetLabel("LblMenuType", "Menu type", LitMenuType, true)%>
                                <asp:TextBox ID="LitMenuType" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>

                            <div class="form-group col-sm-6">
                                <%=base.GetLabel("LblContentType", "Content type", DropModuleTypes, true)%>
                                <a href="javascript:void(0)" onclick="changeModueleTypes();" id="lnkchange" runat="server">
                                    <%=base.GetLabel("LblChange", "change", null, true) %>
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

                            <div class="form-group col-sm-6">
                                <%=base.GetLabel("LblTitle", "Title", null, true)%>
                                <asp:Panel runat="server" ID="PanelTitle"></asp:Panel>
                            </div>

                            <div class="form-group col-sm-6">
                                <%=base.GetLabel("LblTitleWindow", "Window's title", null, true)%>
                                <asp:Panel runat="server" ID="PanelTitleWindow"></asp:Panel>
                            </div>

                            <div class="form-group col-lg-3 col-sm-6">
                                <%=base.GetLabel("LblRoute", "Route", DropRouteId, true)%>
                                <asp:DropDownList ID="DropRouteId" CssClass="form-control" runat="server">
                                </asp:DropDownList>
                            </div>

                            <div class="form-group col-lg-3 col-sm-6">
                                <%=base.GetLabel("LblUseSsl", "Use SSL", DropUseSsl, true)%>
                                <asp:DropDownList ID="DropUseSsl" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="2" Text="Use route settings"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="No"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                                </asp:DropDownList>                            
                            </div>

                            <div class="form-group col-lg-3 col-sm-6">
                                <%=base.GetLabel("LblLink", "Link", TxtLink, true)%>
                                <asp:TextBox ID="TxtLink" MaxLength="200" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>

                            <div class="form-group col-lg-3 col-sm-6">
                                <%=base.GetLabel("LblRedirectTo", "Redirect to", DropReferMenuId, true)%>
                                <asp:DropDownList ID="DropReferMenuId" CssClass="form-control" runat="server"></asp:DropDownList>
                            </div>

                            <div class="form-group col-sm-12">
                                <%=base.GetLabel("LblParentItem", "Parent item", ListParentId, true)%>
                                <asp:ListBox ID="ListParentId" SelectionMode="Single" Rows="18" runat="server" CssClass="form-control"></asp:ListBox>
                            </div>

                            <div class="form-group col-sm-6 col-lg-3">
                                <%=base.GetLabel("LblCssClass", "Css class", TxtCssClass, true)%>
                                <asp:TextBox ID="TxtCssClass" MaxLength="200" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>

                            <div class="form-group col-sm-6 col-lg-3">
                                <%=base.GetLabel("LblViews", "Views", DropViews, true)%>
                                <a href="javascript:void(0)" onclick="changeView();" id="lnkChangeView" runat="server">
                                    <%=base.GetLabel("LblChange", "change", null, true) %>
                                </a>
                                <asp:DropDownList ID="DropViews" runat="server" CssClass="form-control"
                                    AutoPostBack="true" OnSelectedIndexChanged="DropViews_SelectedIndexChanged"></asp:DropDownList>
                            </div>

                            <div class="form-group col-sm-6 col-lg-3">
                                <%=base.GetLabel("LblTheme", "Theme", DropCurrTheme, true)%>
                                <asp:DropDownList ID="DropCurrTheme" CssClass="form-control" runat="server"></asp:DropDownList>
                            </div>

                            <div class="form-group col-sm-6 col-lg-3">
                                <%=base.GetLabel("LblMasterpage", "Masterpage", DropCurrMasterPage, true)%>
                                <asp:DropDownList ID="DropCurrMasterPage" CssClass="form-control" runat="server"></asp:DropDownList>
                            </div>

                        </div>

                        <div class="tab-pane fade" id="tab-options">
                            <h4></h4>
                        
                            <div class="form-group col-sm-12">
                                <asp:CheckBox ID="ChkVisible" runat="server" Enabled="true" />
                                <%=base.GetLabel("LblVisible", "Visible", ChkVisible, true)%>
                            </div>

                            <div class="form-group col-sm-12">
                                <asp:CheckBox ID="ChkPublished" runat="server" Enabled="true" />
                                <%=base.GetLabel("LblPublished", "Published", ChkPublished, true)%>
                            </div>

                            <div class="form-group col-sm-12">
                                <asp:CheckBox ID="ChkOverridePageTitle" runat="server" Enabled="true" />
                                <%=base.GetLabel("LblOverrideTitle", "Override window title", ChkOverridePageTitle, true)%>
                            </div>

                            <div class="form-group col-sm-12">
                                <asp:CheckBox ID="ChkShowModuleTitle" runat="server" Enabled="true" />
                                <%=base.GetLabel("LblShowModuleTitle", "Show module title", ChkShowModuleTitle, true)%>
                            </div>

                            <div class="form-group col-sm-12">
                                <asp:CheckBox ID="ChkIsCore" runat="server" Enabled="false" />
                                <strong>Core</strong>
                            </div>

                            <div class="form-group col-sm-12">
                                <strong>ID</strong>
                                <asp:Label ID="LblId" runat="server" Text=""></asp:Label>
                            </div>

                            <div class="form-group col-sm-12">
                                <strong>Module Id</strong>
                                <asp:Label ID="LblModuleId" runat="server" Text=""></asp:Label>
                            </div>

                        </div>   
                        
                        <div class="tab-pane fade" id="tab-security">
                            <div class="form-group col-lg-12">
                                <uc1:PermissionsControl ID="PermissionsControl1" runat="server" />
                            </div>
                        </div>

                        <div class="tab-pane fade" id="tab-parameters">
                            <h4></h4>
                            <uc1:ModuleParams ID="ModuleParams1" runat="server" />
                        </div>                                         
                    
                    </div>


                </div>
            </div>

        </asp:View>
    </asp:MultiView>
    </div>

</ContentTemplate>
</asp:UpdatePanel>     
