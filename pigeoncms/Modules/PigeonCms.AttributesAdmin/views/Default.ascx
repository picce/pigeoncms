<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Default.ascx.cs" Inherits="Controls_AttributesAdmin" %>

<script type="text/javascript">
// <!CDATA[

function pageLoad(sender, args) 
{
    $("a.fancyRefresh").fancybox({
        'width': '80%',
        'height': '80%',
        'type': 'iframe',
        'hideOnContentClick': false,
        onClosed: function() { }
    });
}

var deleteQuestion = '<%=PigeonCms.Utility.GetLabel("RECORD_DELETE_QUESTION") %>';

// ]]>
</script>

<asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" EnableScriptGlobalization="true"></asp:ScriptManager>
<asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="1" AssociatedUpdatePanelID="Upd1">
    <ProgressTemplate>
        <div class="loading"><%=PigeonCms.Utility.GetLabel("LblLoading", "loading") %></div>
    </ProgressTemplate>
</asp:UpdateProgress>

<asp:UpdatePanel ID="Upd1" runat="server">
<ContentTemplate>

    <div class="row">
        <div class="col-lg-12">
            <asp:Label ID="LblErr" runat="server" Text=""></asp:Label>
            <asp:Label ID="LblOk" runat="server" Text=""></asp:Label>
        </div>
    </div>

    <div class="row">
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">

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

                <div class="panel panel-default">
                    <div class="table-responsive">

                        <asp:GridView ID="Grid1" runat="server" AllowPaging="True" AllowSorting="false" Width="100%" AutoGenerateColumns="False"
                            DataSourceID="ObjDs1" DataKeyNames="Id" OnRowCommand="Grid1_RowCommand" OnRowCreated="Grid1_RowCreated" OnRowDataBound="Grid1_RowDataBound">
                            <Columns>
                                  
                                <%--0--%>                      
                                <asp:TemplateField HeaderText="<%$ Resources:PublicLabels, LblTitle %>" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LnkTitle" runat="server" CausesValidation="false" 
                                        CommandName="Select" CommandArgument='<%#Eval("Id") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%--1--%>
                                <asp:TemplateField HeaderText="Edit Values" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LnkEditValues" runat="server" CausesValidation="false" 
                                        CommandName="EditValues" CommandArgument='<%#Eval("Id") %>'><i class='fa fa-pgn_edit fa-fw'></i></asp:LinkButton>
                                        <asp:Literal ID="ValuesPreview" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%--1--%>
                                <asp:TemplateField HeaderText="ItemType" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Literal ID="LnkItemType" runat="server"></asp:Literal>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%--2--%>
                                <asp:TemplateField HeaderText="Custom Value" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Literal ID="LnkCustomValue" runat="server"></asp:Literal>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%--3--%>
                                <asp:TemplateField HeaderText="Unit of Measure" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Literal ID="LnkMeasureUnit" runat="server"></asp:Literal>
                                    </ItemTemplate>
                                </asp:TemplateField>
                    
                                <%--4--%>
                                <asp:BoundField DataField="Id" HeaderText="ID" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" />

                                <%--5--%>
                                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LnkDel" runat="server" CommandName="DeleteRow" 
                                            CommandArgument='<%#Eval("Id") %>' OnClientClick="return confirm(deleteQuestion);">
                                            <i class='fa fa-pgn_delete fa-fw'></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>

                    </div>
                </div>

            </div>
            
            <asp:ObjectDataSource ID="ObjDs1" runat="server" SortParameterName="sort"
                SelectMethod="GetByFilter" TypeName="PigeonCms.AttributesManager" 
                OnObjectCreating="ObjDs1_ObjectCreating"                
                OnSelecting="ObjDs1_Selecting">
                <SelectParameters>
                    <asp:Parameter Name="filter" Type="Object" />
                    <asp:Parameter Name="sort" Type="String" DefaultValue="Ordering" />
                </SelectParameters>
                <DeleteParameters>
                    <asp:Parameter Name="Id" Type="Int32" />
                </DeleteParameters>
            </asp:ObjectDataSource>

        </asp:View>
   

        <asp:View ID="ViewInsert" runat="server">

            <div class="panel panel-default">

                <div class="panel-heading">
                    <%=base.GetLabel("LblDetails", "Details") %>
                    <div class="pull-right">
                        <div class="btn-group">
                            <asp:Button ID="BtnSave" runat="server" Text="<%$ Resources:PublicLabels, CmdSave %>" CssClass="btn btn-primary btn-xs" OnClick="BtnSave_Click" />
                            <asp:Button ID="BtnCancel" runat="server" Text="<%$ Resources:PublicLabels, CmdCancel %>" CssClass="btn btn-default btn-xs" CausesValidation="false" OnClick="BtnCancel_Click" />
                        </div>
                    </div>
                </div>

                <div class="panel-body">

                    <div class="form-group col-sm-6 col-md-3">
                        <%=base.GetLabel("LblAllowCustomValue", "Use Custom Values", ChkCustomValue, true)%>
                        <asp:CheckBox ID="ChkCustomValue" runat="server" CssClass="form-control" Enabled="true" />
                    </div>

                    <div class="form-group col-sm-6 col-md-3">
                        <%=base.GetLabel("LblItemType", "Select ItemType", null, true)%>
                        <asp:DropDownList runat="server" ID="DropItemType"  AutoPostBack="true" CssClass="form-control" 
                                    ></asp:DropDownList>
                    </div>

                    <div class="form-group col-sm-6 col-md-4">
                        <%=base.GetLabel("LblName", "Name", null, true)%>
                         <asp:TextBox ID="TxtName" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="form-group col-sm-6 col-md-2">
                        <%=base.GetLabel("LblMeasureUnit", "Unit of Measure", null, true)%>
                         <asp:TextBox ID="TxtMeasureUnit" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                    </div>

                </div>

            </div>

        </asp:View>

        <asp:View ID="ViewAttributesValues" runat="server">

            <div class="panel panel-default">

                <div class="panel-heading">
                    <asp:Literal ID="editValueName" runat="server"></asp:Literal>
                    <div class="pull-right">
                        <div class="btn-group">
                            <asp:Button ID="BtnNewValue" runat="server" Text="New" CssClass="btn btn-warning btn-xs" OnClick="BtnNewValue_Click" />
                            <asp:Button ID="BtnSaveValues" runat="server" Text="<%$ Resources:PublicLabels, CmdSave %>" CssClass="btn btn-primary btn-xs" OnClick="BtnSaveValues_Click" />
                            <asp:Button ID="BtnCancelValues" runat="server" Text="<%$ Resources:PublicLabels, CmdCancel %>" CssClass="btn btn-default btn-xs" CausesValidation="false" OnClick="BtnCancel_Click" />
                        </div>
                    </div>
                </div>

                <div class="panel-body">

                    <div class="form-group col-lg-12">
                        <%=base.GetLabel("LblInLang", "Valori in lingua", ChkInLang, true)%>
                        <asp:CheckBox ID="ChkInLang" runat="server" CssClass="form-control" Checked="true" AutoPostBack="true" OnCheckedChanged="ChkInLang_CheckedChanged" />
                    </div>

                    <div class="form-group col-sm-12 col-md-6">
                        <%=base.GetLabel("LblAttributeValue", "Valore Attributo", null, true)%>
                        <asp:Panel runat="server" ID="PanelTitle"></asp:Panel>
                    </div>

                    <div class="col-sm-12 col-md-6">

                        <div class="panel panel-default">
                            <div class="table-responsive">

                                <asp:GridView ID="GridValues" runat="server" AllowPaging="True" AllowSorting="false" Width="100%" AutoGenerateColumns="False"
                                    DataSourceID="ObjValueSource" DataKeyNames="Id" OnRowCommand="GridValues_RowCommand" OnRowCreated="GridValues_RowCreated" OnRowDataBound="GridValues_RowDataBound">
                                    <Columns>
                                  
                                        <%--0--%>                      
                                        <asp:TemplateField HeaderText="<%$ Resources:PublicLabels, LblTitle %>" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LnkTitle" runat="server" CausesValidation="false" 
                                                CommandName="Select" CommandArgument='<%#Eval("Id") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                    
                                        <%--1--%>
                                        <asp:BoundField DataField="Id" HeaderText="ID" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" />

                                        <%--2--%>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LnkDel" runat="server" CommandName="DeleteRow" 
                                                    CommandArgument='<%#Eval("Id") %>' OnClientClick="return confirm(deleteQuestion);">
                                                    <i class='fa fa-pgn_delete fa-fw'></i>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>

                            </div>
                        </div>

                    </div>
            
                    <asp:ObjectDataSource ID="ObjValueSource" runat="server" SortParameterName="sort"
                        SelectMethod="GetByFilter" TypeName="PigeonCms.AttributeValuesManager" 
                        OnObjectCreating="ObjValueSource_ObjectCreating"                
                        OnSelecting="ObjValueSource_Selecting">
                        <SelectParameters>
                            <asp:Parameter Name="filter" Type="Object" />
                            <asp:Parameter Name="sort" Type="String" DefaultValue="Ordering" />
                        </SelectParameters>
                        <DeleteParameters>
                            <asp:Parameter Name="Id" Type="Int32" />
                        </DeleteParameters>
                    </asp:ObjectDataSource>

                </div>

            </div>

        </asp:View>


    </asp:MultiView>
    </div>

</ContentTemplate>
</asp:UpdatePanel>