<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Default.ascx.cs" Inherits="Controls_Default" %>
<%@ Register src="~/Controls/ContentEditorControl.ascx" tagname="ContentEditorControl" tagprefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<script type="text/javascript">
    var deleteQuestion = '<%=PigeonCms.Utility.GetLabel("RECORD_DELETE_QUESTION") %>';

    //Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(startRequest);
    //Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequest);

    function editRow(resId) {
        var upd1 = '<%=Upd1.ClientID%>';
        var drop = '<%=DropTextMode.ClientID %>'

        if (upd1 != null) {
            __doPostBack(drop, 'edit__' + resId);
        }
    }

//    $(document).ready(function () {

//        $(document).off("click", "a.fancyRefresh");
//        $(document).on("click", "a.fancyRefresh", function () {
//            $(this).fancybox({

//                'width': '80%',
//                'height': '80%',
//                'type': 'iframe',
//                'hideOnContentClick': false,
//                onClosed: function () {
//                    var upd1 = '<%=Upd1.ClientID%>';
//                    if (upd1 != null) {
//                        __doPostBack(upd1, 'grid');
//                    }
//                }

//            }).trigger("click");
//            return false;
//        });

//    });



    function pageLoad(sender, args) {
        //rebind();

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
        //addToClientTable(args.get_fileName(), "<span style='color:red;'>" + args.get_errorMessage() + "</span>");
        console.log(args.get_errorMessage());
    }

    function uploadComplete(sender, args) {
        var contentType = args.get_contentType();
        var text = args.get_length() + " bytes";
        if (contentType.length > 0) {
            text += ", '" + contentType + "'";
        }
        var filename = args.get_fileName();
        var currentPath = document.getElementById('<%=TxtCurrentPath.ClientID %>').value;
        console.log("uploadComplete>filename: " + filename);
        console.log("uploadComplete>currentPath: " + currentPath);
        $('#myPreview').attr('src', currentPath + filename + '?width=300&height=300').css('display', 'block');
        $('#' + $('#langBox').val()).val(currentPath + filename);

    }

    function onSuccess(result) { }
    function onFailure(result) { }

</script>



<cc1:ToolkitScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></cc1:ToolkitScriptManager>
<%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
<asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="1" AssociatedUpdatePanelID="Upd1">
    <ProgressTemplate>
        <div class="loading"><%=PigeonCms.Utility.GetLabel("LblLoading", "loading") %></div>
    </ProgressTemplate>
</asp:UpdateProgress>

<asp:UpdatePanel ID="Upd1" UpdateMode="Conditional" runat="server">

<Triggers>
    <asp:PostBackTrigger ControlID="DropTextMode" />
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
                               <asp:Button ID="BtnNew" runat="server" Text="<%$ Resources:PublicLabels, CmdNew %>" CssClass="btn btn-primary btn-xs" OnClick="BtnNew_Click" />
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
                            <div class="form-group">
                                <asp:DropDownList ID="DropModuleTypesFilter" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="DropModuleTypesFilter_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-lg-12">

                <div class="panel panel-default">
                    <div class="table-responsive">

                    <asp:GridView ID="Grid1" runat="server" AllowPaging="True" AllowSorting="true" Width="100%" AutoGenerateColumns="False"
                        DataSourceID="ObjDs1" DataKeyNames="ResourceId" OnRowCommand="Grid1_RowCommand" OnRowCreated="Grid1_RowCreated" OnRowDataBound="Grid1_RowDataBound">
                        <Columns>

                            <%--0--%>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Literal ID="LitSel" runat="server"></asp:Literal>
                                    <%--<asp:LinkButton ID="LnkSel" runat="server" CommandName="Select" CommandArgument='<%#Eval("ResourceId") %>'></asp:LinkButton>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                    
                            <%--1--%>
                            <asp:BoundField DataField="ResourceSet" HeaderText="Resource" SortExpression="" />

                            <%--2--%>
                            <asp:BoundField DataField="ResourceId" HeaderText="Id" SortExpression="" />

                            <%--3--%>
                            <asp:TemplateField HeaderText="Text mode">
                                <ItemTemplate>
                                <asp:Literal ID="LitTextMode" runat="server"  />                
                                </ItemTemplate>
                            </asp:TemplateField>
                    
                            <%--4--%>
                            <asp:TemplateField HeaderText="Values">
                                <ItemTemplate>
                                    <asp:Image ID="ImgPreview" runat="server" SkinID="ImgPreviewStyle" />
                                    <asp:Literal ID="LitValue" runat="server"  />                
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
                    
                        </Columns>
                    </asp:GridView>

                    </div>
                </div>
            </div>
            
            <asp:ObjectDataSource ID="ObjDs1" runat="server" SortParameterName="sort" SelectMethod="GetLabelTransByFilter" 
                TypeName="PigeonCms.LabelsManager" OnSelecting="ObjDs1_Selecting">
                <SelectParameters>
                    <asp:Parameter Name="filter" Type="Object" />
                    <asp:Parameter Name="sort" Type="String" DefaultValue="" />
                </SelectParameters>
                <DeleteParameters>
                </DeleteParameters>
            </asp:ObjectDataSource>
        </asp:View>
   

        <asp:View ID="ViewInsert" runat="server">
        
            <div class="panel panel-default">

                <div class="panel-heading">
                    <%=base.GetLabel("LblDetails", "Details") %>
                    <div class="pull-right">
                        <div class="btn-group">
                            <asp:Button ID="BtnSave" runat="server" Text="<%$ Resources:PublicLabels, CmdSave %>" CssClass="btn btn-primary btn-xs" OnClientClick="MyObject.UpdateEditorFormValue();" OnClick="BtnSave_Click" />
                            <asp:Button ID="BtnCancel" CausesValidation="false" runat="server" Text="<%$ Resources:PublicLabels, CmdCancel %>" CssClass="btn btn-default btn-xs" OnClick="BtnCancel_Click" />
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
    </asp:MultiView>
    </div>

</ContentTemplate>
</asp:UpdatePanel>