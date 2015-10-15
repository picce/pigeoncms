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

            $("a.fancyRefresh").fancybox({
                'width': '80%',
                'height': '80%',
                'type': 'iframe',
                'hideOnContentClick': false,
                onClosed: function () {

                    var upd1 = '<%=Upd1.ClientID%>';
                    if (upd1 != null) {
                        __doPostBack(upd1, 'grid');
                    }

                }
            });

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



<cc1:ToolkitScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></cc1:ToolkitScriptManager>
<asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="1" AssociatedUpdatePanelID="Upd1">
    <ProgressTemplate>
        <div class="loading"><%=PigeonCms.Utility.GetLabel("LblLoading", "loading") %></div>
    </ProgressTemplate>
</asp:UpdateProgress>

<asp:UpdatePanel ID="Upd1" UpdateMode="Conditional" runat="server">

<Triggers>
    <asp:PostBackTrigger ControlID="DropTextMode" />
    <asp:PostBackTrigger ControlID="BtnExport" />
</Triggers>

<ContentTemplate>

    <div class="row">
        <div class="col-lg-12">
            <asp:Label ID="LblErr" runat="server" Text=""></asp:Label>
            <asp:Label ID="LblOk" runat="server" Text=""></asp:Label>
        </div>
    </div>

    <div class="row">
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0" OnActiveViewChanged="MultiView1_ActiveViewChanged">

        <asp:View ID="ViewSee" runat="server">

            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-body"> 
                        <div class="pull-right">
                            <div class="btn-group adminToolbar">
                               <asp:Button ID="BtnNew" runat="server" Text="<%$ Resources:PublicLabels, CmdNew %>" CssClass="btn btn-primary clearfix" OnClick="BtnNew_Click" />

                                <button type="button" class="<%=BtnActionClass %> btn btn-default dropdown-toggle" data-toggle="dropdown" aria-expanded="false">
                                    <i class="fa fa-file-excel-o"></i>
                                    Bulk actions
                                    <span class="caret"></span>
                                </button>
                                <ul class="dropdown-menu pull-right <%=BtnActionClass %>" role="menu">
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
                                </ul>
                            </div>

                        </div> 
                    </div>
                </div>
            </div>

            <div class="col-lg-12">
                <div class="panel panel-default adminFilters">
                    <div class="panel-heading">
                        <h4 class="panel-title">
                            <a data-toggle="collapse" data-parent="#accordion" href="#collapseOne">
                            <%=PigeonCms.Utility.GetLabel("LblFilters")%>
                            </a>
                        </h4>

                    </div>
                    <div id="collapseOne" class="panel-collapse collapse in">
                        <div class="panel-body">
                                              
                            <div class="form-group col-md-6 col-lg-3">
                                <%=base.GetLabel("LblResource", "Resource", DropModuleTypesFilter) %>
                                <asp:DropDownList ID="DropModuleTypesFilter" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="Filter_Changed"></asp:DropDownList>
                            </div>
                            <div class="form-group col-md-6 col-lg-3">
                                <%=base.GetLabel("LblMissingValues", "Missing values", DropMissingFilter) %>
                                <asp:DropDownList ID="DropMissingFilter" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="Filter_Changed"></asp:DropDownList>
                            </div>
                            <div class="form-group col-md-6 col-lg-3">
                                <%=base.GetLabel("LblValuesStartsWith", "Only values that starts with", TxtValuesStartsWithFilter) %>
                                <asp:TextBox runat="server" ID="TxtValuesStartsWithFilter" AutoPostBack="true" CssClass="form-control" MaxLength="20" OnTextChanged="Filter_Changed"></asp:TextBox>
                            </div>
                            <div class="form-group col-md-6 col-lg-3">
                                <%=base.GetLabel("LblValuesContains", "Only values that contains", TxtValuesContainsFilter) %>
                                <asp:TextBox runat="server" ID="TxtValuesContainsFilter" AutoPostBack="true" CssClass="form-control" MaxLength="20" OnTextChanged="Filter_Changed"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-lg-12">

                <div class="panel panel-default">
                    <div class="table-responsive">

                    <asp:GridView ID="Grid1" runat="server" AllowPaging="True" AllowSorting="false" Width="100%" AutoGenerateColumns="False"
                        DataKeyNames="ResourceId" OnRowCommand="Grid1_RowCommand" OnPageIndexChanging="Grid1_PageIndexChanging"  
                        OnRowCreated="Grid1_RowCreated" OnRowDataBound="Grid1_RowDataBound">
                        <Columns>

                            <%--0--%>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Literal ID="LitSel" runat="server"></asp:Literal>
                                </ItemTemplate>
                            </asp:TemplateField>
                    
                            <%--1--%>
                            <asp:TemplateField HeaderText="Resource">
                                <ItemTemplate>
                                    <span class="small">
                                        <asp:Literal ID="LitResourceSet" runat="server"  />
                                    </span>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%--2--%>
                            <asp:TemplateField HeaderText="Id">
                                <ItemTemplate>
                                    <span class="small">
                                        <asp:Literal ID="LitResourceId" runat="server"  />
                                    </span>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%--3--%>
                            <asp:TemplateField HeaderText="Text mode">
                                <ItemTemplate>
                                    <span class="small text-muted">
                                        <asp:Literal ID="LitTextMode" runat="server"  />
                                    </span>
                                </ItemTemplate>
                            </asp:TemplateField>
                    
                            <%--4--%>
                            <asp:TemplateField HeaderText="Values">
                                <ItemTemplate>
                                    <asp:Image ID="ImgPreview" runat="server" SkinID="ImgPreviewStyle" />
                                    <span class="small text-muted">
                                        <asp:Literal ID="LitValue" runat="server"  />                
                                    <//span>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%--5--%>
                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10">
                                <ItemTemplate>

                                    <asp:LinkButton ID="BtnDelete" runat="server" CommandName="DeleteRow" 
                                        CommandArgument='<%#Eval("ResourceId") %>' OnClientClick="return confirm(deleteQuestion);">
                                        <i class='fa fa-pgn_delete fa-fw'></i>
                                    </asp:LinkButton>

                                </ItemTemplate>
                            </asp:TemplateField>
                    
                            <%--values columns used in export--%>
                            
                            <%--6--%>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Literal ID="LitValue1" runat="server"  />                
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--7--%>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Literal ID="LitValue2" runat="server"  />                
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--8--%>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Literal ID="LitValue3" runat="server"  />                
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--9--%>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Literal ID="LitValue4" runat="server"  />                
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--10--%>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Literal ID="LitValue5" runat="server"  />                
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>

                    </div>
                </div>
            </div>

        </asp:View>
   

        <asp:View ID="ViewInsert" runat="server">
        
            <div class="panel panel-default">

                <div class="panel-heading clearfix">
                    <div class="pull-right">
                        <div class="btn-group">
                            <asp:Button ID="BtnSave" runat="server" Text="<%$ Resources:PublicLabels, CmdSave %>" CssClass="btn btn-primary" OnClientClick="MyObject.UpdateEditorFormValue();" OnClick="BtnSave_Click" />
                            <asp:Button ID="BtnCancel" CausesValidation="false" runat="server" Text="<%$ Resources:PublicLabels, CmdCancel %>" CssClass="btn btn-default" OnClick="BtnCancel_Click" />
                        </div>
                    </div>
                </div>

            </div>
            
            <div class="panel-body">
                
                    <div class="form-group col-md-6">
                        <%=base.GetLabel("LblResourceSet", "Resource set", LitResourceSet)%>
                        <asp:TextBox ID="LitResourceSet" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                   
                    </div>

                    <div class="form-group col-md-6">
                        <%=base.GetLabel("LblResourceId", "Resource id", TxtResourceId) %>                        
                        <asp:TextBox ID="TxtResourceId" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="ReqResourceId" ControlToValidate="TxtResourceId" runat="server" Text="*"></asp:RequiredFieldValidator>
                    </div>

                    <div class="form-group col-lg-12">
                        <%=base.GetLabel("LblTextMode", "Text mode", DropTextMode) %>
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

                    <div class="form-group col-lg-12">
                        <%=base.GetLabel("LblValue", "Value", null, true) %>
                        <asp:Panel runat="server" ID="PanelValue"  Visible="true"></asp:Panel>
                    </div>
                    
                    <asp:PlaceHolder ID="plhOnlyInImg" runat="server" Visible="false">
                        <img id="myPreview" src="http://placehold.it/300x300/your+img+here!" width="300" height="300" style="display: none" />
                        <input id="langBox" name="langBox" type="hidden" />
                    </asp:PlaceHolder>

                    <div class="form-group col-lg-12">
                        <%=base.GetLabel("LblComment", "Comment", TxtComment, true) %>
                        <asp:TextBox ID="TxtComment" CssClass="form-control" Enabled="true" runat="server"></asp:TextBox>
                    </div>

                    <div class="form-group col-lg-12">
                        <%=base.GetLabel("LblResourceParams", "Resource params", null, true)%>
                        <asp:TextBox ID="TxtResourceParams" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                    </div>
                
            </div>
            
        </asp:View>
    
    
        <asp:View ID="ViewImport" runat="server">

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
                                        <asp:LinkButton ID="ImgEnabledOk" runat="server" CommandName="ImgEnabledOk" Visible="false" CommandArgument='<%#Eval("Id") %>'>
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

        </asp:View>
   
    </asp:MultiView>
    </div>

</ContentTemplate>
</asp:UpdatePanel>