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

    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">

        <asp:View ID="ViewSee" runat="server">
            <div class="row">

                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-body"> 
                            <div class="pull-right">
                                <div class="btn-group adminToolbar">
                                    <asp:Button ID="BtnNew" runat="server" Text="<%$ Resources:PublicLabels, CmdNew %>" CssClass="btn btn-primary" OnClick="BtnNew_Click" />
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
                                    <asp:TemplateField HeaderText="Values Preview" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" ItemStyle-CssClass="small text-muted">
                                        <ItemTemplate>
                                            <asp:Literal ID="ValuesPreview" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <%--2--%>
                                    <asp:BoundField DataField="Ordering"  SortExpression="Ordering" ItemStyle-HorizontalAlign="Right" Visible="false"/>
                                    <asp:TemplateField HeaderText="<%$ Resources:PublicLabels, LblOrder %>" ItemStyle-HorizontalAlign="Left" SortExpression="Ordering">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="ImgMoveUp" runat="server" CommandName="MoveUp" CommandArgument='<%#Eval("Id") %>'>
                                                <i class='fa fa-pgn_up fa-fw'></i>                            
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="ImgMoveDown" runat="server" CommandName="MoveDown" CommandArgument='<%#Eval("Id") %>'>
                                                <i class='fa fa-pgn_down fa-fw'></i>                            
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <%--3--%>
                                    <asp:TemplateField HeaderText="Custom" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" >
                                        <ItemTemplate>
                                            <asp:LinkButton ID="ImgEnabledOk" runat="server" CommandName="CustomEnabled" Visible="false" CommandArgument='<%#Eval("Id") %>'>
                                                <i class='fa fa-pgn_checked fa-fw'></i>
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="ImgEnabledKo" runat="server" CommandName="CustomDisabled" Visible="false" CommandArgument='<%#Eval("Id") %>'>
                                                <i class='fa fa-pgn_unchecked fa-fw'></i>                            
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                    
                                    <%--4--%>
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

            </div>
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
                    <div class="row">
                        <div class="form-group col-md-6 col-sm-12">
                            <%=base.GetLabel("LblName", "Name *", null, true)%>
                            <asp:RequiredFieldValidator ID="ReqTxtName" ControlToValidate="TxtName" runat="server" Text="*" ForeColor="Red"></asp:RequiredFieldValidator>
                             <asp:TextBox ID="TxtName" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group col-md-6 col-sm-12">
                            <%=base.GetLabel("LblAllowCustomValue", "Use Custom Values", ChkCustomValue, true)%>
                            <asp:CheckBox ID="ChkCustomValue" runat="server" CssClass="form-control" Enabled="true" />
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-sm-12 col-md-6">
                            <div class="form-group">
                                <%=base.GetLabel("LblInLang", "Valori in lingua", ChkInLang, true)%>
                                <asp:CheckBox ID="ChkInLang" runat="server" CssClass="form-control" Checked="true" AutoPostBack="true" OnCheckedChanged="ChkInLang_CheckedChanged" />
                            </div>

                            <div class="form-group">
                                <%=base.GetLabel("LblAttributeValue", "Valore Attributo", null, true)%>
                                <asp:Panel runat="server" ID="PanelTitle"></asp:Panel>
                            </div>

                            <div class="form-group">
                                <asp:Button ID="BtnAddValue" runat="server" Text="Add Value" CssClass="btn btn-default" OnClick="BtnAddValue_Click" />
                                <asp:Button ID="BtnNewValue" runat="server" Text="Clear Value" CssClass="btn btn-default" OnClick="BtnNewValue_Click" />
                            </div>
                        </div>
                        <div class="col-sm-12 col-md-6">
                            <%=base.GetLabel("LblInsertedRecords", "Valori Inseriti", GridValues, true)%>
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
                                            <asp:BoundField DataField="Ordering"  SortExpression="Ordering" ItemStyle-HorizontalAlign="Right" Visible="false" />
                                            <asp:TemplateField HeaderText="<%$ Resources:PublicLabels, LblOrder %>" ItemStyle-HorizontalAlign="Left" SortExpression="Ordering">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="ImgMoveUp" runat="server" CommandName="MoveUp" CommandArgument='<%#Eval("Id") %>'>
                                                        <i class='fa fa-pgn_up fa-fw'></i>                            
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="ImgMoveDown" runat="server" CommandName="MoveDown" CommandArgument='<%#Eval("Id") %>'>
                                                        <i class='fa fa-pgn_down fa-fw'></i>                            
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

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
                                <asp:Parameter Name="sort" Type="String" DefaultValue="ordering" />
                            </SelectParameters>
                            <DeleteParameters>
                                <asp:Parameter Name="Id" Type="Int32" />
                            </DeleteParameters>
                        </asp:ObjectDataSource>
                    </div>

                </div>

            </div>

        </asp:View>

    </asp:MultiView>

</ContentTemplate>
</asp:UpdatePanel>