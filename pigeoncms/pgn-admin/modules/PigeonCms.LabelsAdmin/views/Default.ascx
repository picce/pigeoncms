<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Default.ascx.cs" Inherits="Controls_Default" %>
<%@ Register src="~/Controls/ContentEditorControl.ascx" tagname="ContentEditorControl" tagprefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<script type="text/javascript">
    var deleteQuestion = '<%=PigeonCms.Utility.GetLabel("RECORD_DELETE_QUESTION")%>';


    function editRow(resId) {
        var upd1 = '<%=Upd1.ClientID%>';
        var drop = '<%=DropTextMode.ClientID%>'

        if (upd1 != null) {
            __doPostBack(drop, 'edit__' + resId);
        }
    }

    function pageLoad(sender, args) {

        if (args.get_isPartialLoad()) {

        }
    }

    function uploadError(sender, args) {
        console.log(args.get_errorMessage());
    }

    function uploadComplete(sender, args) {
        var contentType = args.get_contentType();
        var text = args.get_length() + " bytes";
        if (contentType.length > 0) {
            text += ", '" + contentType + "'";
        }
        var filename = args.get_fileName();
        var currentPath = document.getElementById('<%=TxtCurrentPath.ClientID%>').value;
        console.log("uploadComplete>filename: " + filename);
        console.log("uploadComplete>currentPath: " + currentPath);
        $('#myPreview').attr('src', currentPath + filename + '?width=300&height=300').css('display', 'block');
        $('#' + $('#langBox').val()).val(currentPath + filename);
    }

    function uploadPreviewComplete(sender, e) {
        var fileName = e.get_fileName();
        var fileExtension = fileName.substring(fileName.lastIndexOf('.') + 1);
        console.log("uploadPreviewComplete " + fileName);
        var upd1 = '<%=Upd1.ClientID%>';
        if (upd1 != null) {
            __doPostBack(upd1, 'gridPreview');
        }
    }

    function uploadPreviewStarted(sender, args) {
        var fileName = args.get_fileName();
        var fileExtension = fileName.substring(fileName.lastIndexOf('.') + 1);
        var permittedExtensions = [
            'xls',
            'xlsx'
        ];

        if ($.inArray(fileExtension, permittedExtensions) == -1) {
            var err = new Error();
            err.name = 'File type not allowed';
            err.message = 'Only .xls, .xlsx files';
            throw (err);

            return false;
        }

        console.log("uploadPreviewStarted " + fileName);
    }

    function onSuccess(result) { }
    function onFailure(result) { }

</script>



<asp:ScriptManager runat="server" EnablePageMethods="true"></asp:ScriptManager>
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

<asp:UpdatePanel ID="Upd1" UpdateMode="Conditional" runat="server">

<Triggers>
    <asp:PostBackTrigger ControlID="DropTextMode" />
    <%--<asp:PostBackTrigger ControlID="BtnExport" />--%>
</Triggers>

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

									<asp:Button ID="BtnNew" runat="server" Text="<%$ Resources:PublicLabels, CmdNew %>" CssClass="btn btn-primary clearfix" OnClick="BtnNew_Click" />

									<%--
									<button type="button" class="<%=BtnActionClass%> btn btn-primary btn-default dropdown-toggle" data-toggle="dropdown" aria-expanded="false">
										<i class="fa fa-file-excel-o"></i>
										Bulk actions
										<span class="caret"></span>
									</button>
									<ul class="dropdown-menu pull-right <%=BtnActionClass%>" role="menu">
										<li>
											<asp:LinkButton runat="server" ID="BtnImport" OnClick="BtnImport_Click">
												Import form file
											</asp:LinkButton>
										</li>
										<li>
											<asp:LinkButton runat="server" ID="BtnExport" OnClick="BtnExport_Click">
												Export current selection
											</asp:LinkButton>

										</li>
									</ul>--%>

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

								<div class="form-group col-lg-3 col-md-6 form-select-wrapper has-label">
									<%=base.GetLabel("LblResource", "Resource", DropModuleTypesFilter)%>
									<asp:DropDownList ID="DropModuleTypesFilter" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="Filter_Changed">
									</asp:DropDownList>
								</div>

								<div class="form-group col-lg-3 col-md-6 form-select-wrapper has-label">
									<%=base.GetLabel("LblMissingValues", "Missing values", DropMissingFilter)%>
									<asp:DropDownList ID="DropMissingFilter" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="Filter_Changed"></asp:DropDownList>
								</div>

								<div class="form-group col-lg-3 col-md-6">
									<%=base.GetLabel("LblValuesStartsWith", "Only values that starts with", TxtValuesStartsWithFilter)%>
									<asp:TextBox runat="server" ID="TxtValuesStartsWithFilter" AutoPostBack="true" CssClass="form-control" MaxLength="20" OnTextChanged="Filter_Changed"></asp:TextBox>
								</div>

								<div class="form-group col-lg-3 col-md-6">
									<%=base.GetLabel("LblValuesContains", "Only values that contains", TxtValuesContainsFilter)%>
									<asp:TextBox runat="server" ID="TxtValuesContainsFilter" AutoPostBack="true" CssClass="form-control" MaxLength="20" OnTextChanged="Filter_Changed"></asp:TextBox>
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

									<div class="table-modern__col col-md-1"></div>
									<div class="table-modern__col align-left col-md-3"><%=base.GetLabel("ResourceSetId", "Resource Set / Id")%></div>
									<div class="table-modern__col align-center col-md-1"><%=base.GetLabel("TextMode", "Text mode")%></div>
									<div class="table-modern__col align-left col-md-6"><%=base.GetLabel("Values", "Values")%></div>
									<div class="table-modern__col col-md-1"></div>

									<%-- values columns used in export--%>
<%--									<div class="table-modern__col align-left"></div>
									<div class="table-modern__col align-left"></div>
									<div class="table-modern__col align-left"></div>
									<div class="table-modern__col align-left"></div>
									<div class="table-modern__col align-left"></div>--%>

								</div>

								<asp:Repeater runat="server" ID="Rep1" OnItemDataBound="Rep1_ItemDataBound" OnItemCommand="Rep1_ItemCommand">
									<ItemTemplate>

										<div class="table-modern--row">

											<%--#action edit --%>
											<div class="table-modern__col col-md-1">
												<asp:Literal runat="server" ID="LitEdit"></asp:Literal>
											</div>

											<div class="table-modern__col col-md-3">
												<div class="table-modern--description" data-menu="set">
													<div class="table-modern--description--wrapper">
														<asp:Literal ID="LitResSetId" runat="server" />
													</div>
												</div>
											</div>

											<div class="table-modern__col col-md-1">
												<div class="table-modern__col--value" data-menu="mode">
													<asp:Literal ID="LitTextMode" runat="server" />
												</div>
											</div>

											<div class="table-modern__col col-md-6">
												<div class="table-modern--description" data-menu="value">
													<div class="table-modern--description--wrapper">
														<asp:Image ID="ImgPreview" runat="server" SkinID="ImgPreviewStyle" />
														<asp:Literal ID="LitValue" runat="server" /><br />
													</div>
												</div>
											</div>

											<div class="table-modern__col" style="display:none;">
												<div class="table-modern--description" data-menu="value">
													<div class="table-modern--description--wrapper">
														<asp:Literal ID="LitValue1" runat="server" /><br />
														<asp:Literal ID="LitValue2" runat="server" /><br />
														<asp:Literal ID="LitValue3" runat="server" /><br />
														<asp:Literal ID="LitValue4" runat="server" /><br />
														<asp:Literal ID="LitValue5" runat="server" /><br />
													</div>
												</div>
											</div>
											
											<%--#action delete --%>
											<div class="table-modern__col col-md-1" runat="server" id="ColDelete">
												<a href="#" class="table-modern--media delete-label js-delete" data-title-mobile="DEL"
													data-msg-title='<%=base.GetLabel("delete", "delete")%>' 
													data-msg-subtitle='<%=PigeonCms.Utility.GetLabel("RECORD_DELETE_QUESTION")%>' 
													data-msg-cancel='<%=base.GetLabel("cancel", "cancel")%>' 
													data-msg-confirm='<%=base.GetLabel("confirm", "confirm")%>'>
													<div class="table-modern--media--wrapper">
														<div class="table-modern--media--delete"></div>
													</div>
												</a>
												<asp:Button ID="LnkDel" CommandName="DeleteRow" CommandArgument='<%#Eval("ResourceId")%>' runat="server" style="display:none;"  />
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

							<div class="form-group col-md-4">
								<%=base.GetLabel("LblResourceSet", "Resource set", LitResourceSet)%>
								<asp:TextBox ID="LitResourceSet" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
							</div>

							<div class="form-group col-md-4">
								<%=base.GetLabel("LblResourceId", "Resource id", TxtResourceId)%>                        
								<asp:TextBox ID="TxtResourceId" runat="server" CssClass="form-control"></asp:TextBox>
								<asp:RequiredFieldValidator ID="ReqResourceId" ControlToValidate="TxtResourceId" runat="server" Text="*"></asp:RequiredFieldValidator>
							</div>

							<div class="form-group col-md-4 form-select-wrapper form-select-detail-item">
								<%=base.GetLabel("LblTextMode", "Text mode", DropTextMode)%>
								<asp:DropDownList ID="DropTextMode" runat="server" AutoPostBack="true" 
									CssClass="form-control" ontextchanged="DropTextMode_TextChanged">
								</asp:DropDownList>
							</div>

							<div class="form-group col-lg-12" style="display:none;">
								<asp:HiddenField runat="server" ID="TxtCurrentPath" />
								<cc1:AsyncFileUpload
									CssClass="action-upload"
									OnClientUploadError="uploadError" OnClientUploadComplete="uploadComplete" 
									runat="server" ID="File1" UploaderStyle="Modern" ClientIDMode="AutoID"
									UploadingBackColor="#CCFFFF" 
									onuploadedcomplete="File1_UploadedComplete" 
									 />
							</div>

							<div class="form-group col-lg-12 spacing-detail">
								<div class="form-text-wrapper">
									<%=base.GetLabel("LblValue", "Value", null, true)%>
									<asp:Panel runat="server" ID="PanelValue"  Visible="true"></asp:Panel>
								</div>
							</div>

							<asp:PlaceHolder ID="plhOnlyInImg" runat="server" Visible="false">
								<img id="myPreview" src="http://placehold.it/300x300/your+img+here!" width="300" height="300" style="display: none" />
								<input id="langBox" name="langBox" type="hidden" />
							</asp:PlaceHolder>

							<div class="form-group col-lg-12">
								<%=base.GetLabel("LblComment", "Comment", TxtComment, true)%>
								<asp:TextBox ID="TxtComment" CssClass="form-control" Enabled="true" runat="server"></asp:TextBox>
							</div>

							<div class="form-group col-lg-12">
								<%=base.GetLabel("LblResourceParams", "Resource params", null, true)%>
								<asp:TextBox ID="TxtResourceParams" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
							</div>

						</div>

					</div>

				</div>

		</asp:Panel>


		<asp:Panel runat="server" ID="PanelImport" Visible="false">


            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-body"> 

                        <div class="col-lg-6 col-sm-12">

                            <cc1:AsyncFileUpload
                                ClientIDMode="AutoID"
                                OnClientUploadError="uploadError"
                                OnClientUploadStarted="uploadPreviewStarted"
                                OnClientUploadComplete="uploadPreviewComplete"
                                OnUploadedFileError="UploadPreview_UploadedFileError"
                                OnUploadedComplete="UploadPreview_OnUploadedComplete"
                                CssClass="form-control  form-control__fileupload"
                                runat="server"
                                ID="UploadPreview" CompleteBackColor="Yellow"
                                UploadingBackColor="#CCFFFF" ErrorBackColor="Red"
                                ThrobberID="myThrobber" />
                        </div>

                        <div class="col-lg-6 col-sm-12">
                            <div class="pull-right btn-group">
                                <asp:Button ID="BtnApplyImport" runat="server" Text="Import" CssClass="btn btn-primary clearfix" OnClick="BtnApplyImport_Click" />
                                <asp:Button ID="BtnCancelImport" CausesValidation="false" runat="server" Text="<%$ Resources:PublicLabels, CmdCancel %>" CssClass="btn btn-default" OnClick="BtnCancel_Click" />
                            </div>
                        </div>
                         
                    </div>
                </div>
            </div>

            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading clearfix">

                        <div class="pull-right">
                            <div class="btn-group adminToolbar">

                                <button type="button" class="btn btn-default btn-xs dropdown-toggle" data-toggle="dropdown" aria-expanded="false">
                                    <i class="fa fa-checked"></i>
                                    Select
                                    <span class="caret"></span>
                                </button>
                                <ul class="dropdown-menu pull-right" role="menu">
                                    <li>
                                        <asp:LinkButton runat="server" ID="BtnImportSelectAll" OnClick="BtnImportSelectAll_Click">
                                            Select all
                                        </asp:LinkButton>
                                    </li>
                                    <li>
                                        <asp:LinkButton runat="server" ID="BtnImportDeselectAll" OnClick="BtnImportDeselectAll_Click">
                                            Deselect all
                                        </asp:LinkButton>
                                    </li>
                                </ul>
                            </div>

                        </div> 

                        <h4 class="panel-title">
                            <a data-toggle="collapse" data-parent="#accordion" href="#collapseOne">
                            Import wizard
                            </a>
                        </h4>

                    </div>
                    <div id="collapseOne" class="panel-collapse collapse in">
                        <div class="panel-body">

                            <div class="form-group col-sm-6 col-lg-3">
                                <%=base.GetLabel("ColResourceSet", "ResourceSet column", DropColResourceSet)%>
                                <asp:DropDownList ID="DropColResourceSet" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>

                            <div class="form-group col-sm-6 col-lg-3">
                                <%=base.GetLabel("ColResourceId", "ResourceId column", DropColResourceId)%>
                                <asp:DropDownList ID="DropColResourceId" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>

                            <div class="form-group col-sm-6 col-lg-3" id="PanelPreviewValue0" runat="server">
                                <label>
                                    <asp:Literal runat="server" ID="LitColValue0"></asp:Literal>
                                </label>
                                <asp:DropDownList ID="DropColValue0" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>


                            <div class="form-group col-sm-6 col-lg-3" id="PanelPreviewValue1" runat="server">
                                <label>
                                    <asp:Literal runat="server" ID="LitColValue1"></asp:Literal>
                                </label>
                                <asp:DropDownList ID="DropColValue1" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>

                            <div class="form-group col-sm-6 col-lg-3" id="PanelPreviewValue2" runat="server">
                                <label>                                
                                    <asp:Literal runat="server" ID="LitColValue2"></asp:Literal>
                                </label>
                                <asp:DropDownList ID="DropColValue2" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>

                            <div class="form-group col-sm-6 col-lg-3" id="PanelPreviewValue3" runat="server">
                                <label>
                                    <asp:Literal runat="server" ID="LitColValue3"></asp:Literal>
                                </label>
                                <asp:DropDownList ID="DropColValue3" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>

                            <div class="form-group col-sm-6 col-lg-3" id="PanelPreviewValue4" runat="server">
                                <label>                                
                                    <asp:Literal runat="server" ID="LitColValue4"></asp:Literal>
                                </label>
                                <asp:DropDownList ID="DropColValue4" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>

                        </div>
                    </div>
                </div>
            </div>

            <div class="col-lg-12">

                <div class="panel panel-default">
                    <div class="table-responsive">

                        <asp:GridView ID="GridImportPreview" runat="server" AutoGenerateColumns="False" Width="100%" AllowPaging="true"
                            OnRowCommand="GridImportPreview_RowCommand" OnRowCreated="GridImportPreview_RowCreated" 
                            OnPageIndexChanging="GridImportPreview_PageIndexChanging"
                            Visible="true"
                            OnRowDataBound="GridImportPreview_RowDataBound">
                            <Columns>

                                <asp:TemplateField HeaderText="Import" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="ImgEnabledOk" runat="server" CommandName="ImgEnabledOk" Visible="false" CommandArgument='<%#Eval("Id")%>'>
                                            <i class='fa fa-pgn_checked fa-fw'></i>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="ImgEnabledKo" runat="server" CommandName="ImgEnabledKo" Visible="false" CommandArgument='<%#Eval("Id") %>'>
                                            <i class='fa fa-pgn_unchecked fa-fw'></i>                            
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="A" ItemStyle-CssClass="small text-muted">
                                    <ItemTemplate><asp:Literal ID="LitCol01" runat="server"></asp:Literal></ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="B" ItemStyle-CssClass="small text-muted">
                                    <ItemTemplate><asp:Literal ID="LitCol02" runat="server"></asp:Literal></ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="C" ItemStyle-CssClass="small text-muted">
                                    <ItemTemplate><asp:Literal ID="LitCol03" runat="server"></asp:Literal></ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="D" ItemStyle-CssClass="small text-muted">
                                    <ItemTemplate><asp:Literal ID="LitCol04" runat="server"></asp:Literal></ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="E" ItemStyle-CssClass="small text-muted">
                                    <ItemTemplate><asp:Literal ID="LitCol05" runat="server"></asp:Literal></ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="F" ItemStyle-CssClass="small text-muted">
                                    <ItemTemplate><asp:Literal ID="LitCol06" runat="server"></asp:Literal></ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>

                    </div>
                </div>
            </div>


		</asp:Panel>

	</div>



</ContentTemplate>
</asp:UpdatePanel>