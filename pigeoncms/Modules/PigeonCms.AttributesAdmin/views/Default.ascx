<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Default.ascx.cs" Inherits="Controls_AttributesAdmin" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script type="text/javascript">
// <!CDATA[

    var deleteQuestion = '<%=PigeonCms.Utility.GetLabel("RECORD_DELETE_QUESTION") %>';

// ]]>
</script>

<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="1" AssociatedUpdatePanelID="Upd1">
    <ProgressTemplate>
        <div class="loading"><%=PigeonCms.Utility.GetLabel("LblLoading", "loading") %></div>
    </ProgressTemplate>
</asp:UpdateProgress>

<asp:UpdatePanel ID="Upd1" runat="server">
<ContentTemplate>

    <asp:Label ID="LblErr" runat="server" Text="" CssClass="error"></asp:Label>
    <asp:Label ID="LblOk" runat="server" Text="" CssClass="success"></asp:Label>

    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">

        <asp:View ID="ViewSee" runat="server">
            <div class="adminToolbar">
                <asp:Button ID="BtnNew" runat="server" Text="<%$ Resources:PublicLabels, CmdNew %>" CssClass="button" OnClick="BtnNew_Click" />
            </div>
            <fieldset class="adminFilters">
                    <%=PigeonCms.Utility.GetLabel("LblFilters", "Filters") %>&nbsp;
                    <asp:TextBox ID="TxtNameFilter" runat="server" AutoPostBack="true" CssClass="adminMediumText" 
                        ontextchanged="Filter_TextChanged"></asp:TextBox>
                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" TargetControlID="TxtNameFilter" runat="server" 
                        WatermarkText="<name>" WatermarkCssClass="adminMediumText watermark"></cc1:TextBoxWatermarkExtender>
            </fieldset>
            <br />
            <asp:GridView ID="Grid1" runat="server" AllowPaging="True" AllowSorting="false" Width="100%" AutoGenerateColumns="False"
                DataSourceID="ObjDs1" DataKeyNames="Id" OnRowCommand="Grid1_RowCommand" OnRowCreated="Grid1_RowCreated" OnRowDataBound="Grid1_RowDataBound">
                <Columns>
                                  
                    <%--0--%>          
                    <asp:TemplateField ItemStyle-Width="10" Visible="false">
                        <ItemTemplate>
                        <asp:ImageButton ID="LnkSel" CommandName="Select" CommandArgument='<%#Eval("Id") %>' 
                        runat="server" SkinID="ImgEditFile" />                
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <%--1--%>          
                    <asp:TemplateField HeaderText="Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:LinkButton ID="LnkName" runat="server" CausesValidation="false" 
                            CommandName="Select" CommandArgument='<%#Eval("Id") %>'></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%--2--%>          
                    <asp:TemplateField HeaderText="Type" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Literal ID="LitFieldType" runat="server"></asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%--3--%>          
                    <asp:TemplateField HeaderText="Enabled" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="10">
                        <ItemTemplate>
                            <asp:ImageButton runat="server" ID="ImgEnabledOk" CommandName="ImgEnabledOk" CommandArgument='<%#Eval("Id") %>' SkinID="ImgOk" Visible="false" />
                            <asp:ImageButton runat="server" ID="ImgEnabledKo" CommandName="ImgEnabledKo" CommandArgument='<%#Eval("Id") %>' SkinID="ImgUnchecked" Visible="false" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%--4--%>          
                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10">
                        <ItemTemplate>
                        <asp:ImageButton ID="LnkDel" CommandName="DeleteRow" CommandArgument='<%#Eval("Id") %>' 
                        runat="server" SkinID="ImgDelFile" 
                        OnClientClick="return confirm(deleteQuestion);"  />
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                </Columns>
            </asp:GridView>
            
            <asp:ObjectDataSource ID="ObjDs1" runat="server" SortParameterName="sort"
                SelectMethod="GetByFilter" TypeName="PigeonCms.FormFieldsManager" 
                OnObjectCreating="ObjDs1_ObjectCreating" OnSelecting="ObjDs1_Selecting">
                <SelectParameters>
                    <asp:Parameter Name="filter" Type="Object" />
                    <asp:Parameter Name="sort" Type="String" DefaultValue="Name" />
                </SelectParameters>
                <DeleteParameters>
                    <asp:Parameter Name="Id" Type="Int32" />
                </DeleteParameters>
            </asp:ObjectDataSource>
        </asp:View>
   

        <asp:View ID="ViewInsert" runat="server">
            <div class="adminToolbar">
                <asp:Button ID="BtnSave" runat="server" Text="<%$ Resources:PublicLabels, CmdSave %>" CssClass="button" OnClick="BtnSave_Click" />
                <asp:Button ID="BtnCancel" runat="server" Text="<%$ Resources:PublicLabels, CmdCancel %>" CssClass="button" OnClick="BtnCancel_Click" />
            </div>
            <div class="col width-50">
                <fieldset class="adminForm">
                    <legend></legend>
                    <table class="adminTable">
                    <tr>
                        <td class="key"><%=base.GetLabel("Name", "Name", TxtName)%></td>
                        <td>
                            <asp:TextBox ID="TxtName" runat="server" CssClass="adminMediumText"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="key"><%=base.GetLabel("FieldType", "Field type", DropFieldType)%></td>
                        <td>
                            <asp:DropDownList runat="server" ID="DropFieldType"  AutoPostBack="true" 
                            CssClass="adminMediumText" OnSelectedIndexChanged="DropFieldType_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="key"><%=base.GetLabel("MinValue", "Min value", TxtMinValue)%></td>
                        <td>
                            <asp:TextBox ID="TxtMinValue" runat="server" CssClass="adminShortText"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="key"><%=base.GetLabel("MaxValue", "Max value", TxtMaxValue)%></td>
                        <td>
                            <asp:TextBox ID="TxtMaxValue" runat="server" CssClass="adminShortText"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="key"><%=base.GetLabel("Enabled", "Enabled", ChkEnabled, true)%></td>
                        <td>
                            <asp:CheckBox ID="ChkEnabled" runat="server" Enabled="true" />
                        </td>
                    </tr>
                    </table>
                </fieldset>
            </div>

            <div class="col width-50">
                <fieldset class="adminForm">
                    <legend><%=base.GetLabel("AttributeValues", "Attribute values") %></legend>

                    <table class="adminTable">
                    <tr>
                        <td class="key"><%=base.GetLabel("Value", "Value", TxtValue)%></td>
                        <td>
                            <asp:TextBox ID="TxtValue" runat="server" CssClass="adminMediumText"></asp:TextBox>
                            <asp:Button ID="BtnAddValue" runat="server" Text="Add" CssClass="button" OnClick="BtnAddValue_Click" />                            
                        </td>
                    </tr>
                    </table>

                    <asp:GridView ID="GridValues" runat="server" AllowPaging="false" AllowSorting="false" Width="100%" AutoGenerateColumns="False"
                        DataSourceID="ObjDsValues" DataKeyNames="Id" 
                        OnRowCommand="GridValues_RowCommand" OnRowCreated="GridValues_RowCreated" OnRowDataBound="GridValues_RowDataBound">
                        <Columns>
                                  
                            <%--0--%>   
                            <asp:BoundField HeaderText="Value" DataField="Value" />

                            <%--1--%>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-BorderStyle="None" ItemStyle-Width="25" SortExpression="Ordering">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImgMoveUp" CommandName="MoveUp" CommandArgument='<%#Eval("Id") %>'
                                    SkinID="ImgSortAsc" runat="server" />
                                    <asp:ImageButton ID="ImgMoveDown" CommandName="MoveDown" CommandArgument='<%#Eval("Id") %>'
                                    SkinID="ImgSortDesc" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Ordering" HeaderText="<%$ Resources:PublicLabels, LblOrder %>" 
                            ItemStyle-Width="10" SortExpression="Ordering" ItemStyle-HorizontalAlign="Right" />

                            <%--2--%>          
                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10">
                                <ItemTemplate>
                                <asp:ImageButton ID="LnkDel" CommandName="DeleteRow" CommandArgument='<%#Eval("Id") %>' 
                                runat="server" SkinID="ImgDelFile" 
                                OnClientClick="return confirm(deleteQuestion);"  />
                                </ItemTemplate>
                            </asp:TemplateField>
                    
                        </Columns>
                    </asp:GridView>
            
                    <asp:ObjectDataSource ID="ObjDsValues" runat="server" SortParameterName="sort"
                        SelectMethod="GetByFilter" TypeName="PigeonCms.FormFieldOptionsManager" 
                        OnSelecting="ObjDsValues_Selecting">
                        <SelectParameters>
                            <asp:Parameter Name="filter" Type="Object" />
                            <asp:Parameter Name="sort" Type="String" DefaultValue="Ordering" />
                        </SelectParameters>
                        <DeleteParameters>
                            <asp:Parameter Name="Id" Type="Int32" />
                        </DeleteParameters>
                    </asp:ObjectDataSource>
                
                </fieldset>
                <fieldset class="adminForm recordInfo">
                    <legend><%=base.GetLabel("LblRecordInfo", "Record info") %></legend>
                    <table class="adminTable" cellspacing="0">
                    <tr>
                        <td class="key"><%=base.GetLabel("LblRecordId", "ID") %></td>
                        <td><asp:Label ID="LblId" runat="server" Text=""></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="key"><%=base.GetLabel("LblCreated", "Created") %></td>
                        <td><asp:Label ID="LblCreated" runat="server" Text=""></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="key"><%=base.GetLabel("LblLastUpdate", "Last update") %></td>
                        <td><asp:Label ID="LblUpdated" runat="server" Text=""></asp:Label></td>
                    </tr>
                    </table>
                </fieldset>

        </asp:View>
    </asp:MultiView>

</ContentTemplate>
</asp:UpdatePanel>