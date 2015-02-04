<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Default.ascx.cs" Inherits="Controls_UpdatesAdmin" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register src="~/Controls/FileUpload.ascx" tagname="FileUpload" tagprefix="uc1" %>
<%@ Import Namespace="PigeonCms" %>

<script type="text/javascript">
// <!CDATA[

function pageLoad(sender, args) 
{
    $("div.fancy a").fancybox({
        'width': '75%',
        'height': '75%',
        'type': 'iframe',
        'hideOnContentClick': false,
        onClosed: function() { }
    });
}

// ]]>
</script>

<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="1" AssociatedUpdatePanelID="Upd1">
    <ProgressTemplate>
        <div class="loading"><%=PigeonCms.Utility.GetLabel("LblLoading", "loading") %></div>
    </ProgressTemplate>
</asp:UpdateProgress>
    
<asp:UpdatePanel ID="Upd1" runat="server">
<Triggers>
    <asp:PostBackTrigger ControlID="FileUpload1" />
</Triggers>
<ContentTemplate>
    
    <asp:Label ID="LblErr" runat="server" Text="" CssClass="error"></asp:Label>
    <asp:Label ID="LblOk" runat="server" Text="" CssClass="success"></asp:Label>
    
    <div class="adminToolbar">
    <ul>
        <li>
            <asp:LinkButton ID="LnkInstall" runat="server" onclick="LnkInstall_Click"><%=base.GetLabel("LblInstall", "Install")%></asp:LinkButton>
        </li>
        <li>
            <asp:LinkButton ID="LnkModules" runat="server" onclick="LnkModules_Click"><%=base.GetLabel("LblModules", "Modules")%></asp:LinkButton>
        </li>
        <li>
            <asp:LinkButton ID="LnkTemplates" runat="server" onclick="LnkTemplates_Click"><%=base.GetLabel("LblTemplates", "Templates")%></asp:LinkButton>
        </li>
        <li class="last">
            <asp:LinkButton ID="LnkSql" runat="server" onclick="LnkSql_Click"><%=base.GetLabel("LblSql", "Sql")%></asp:LinkButton>
        </li>
    </ul>
    </div>
    
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0" OnActiveViewChanged="MultiView1_ActiveViewChanged">
        <asp:View ID="ViewInstall" runat="server">
            <div class="col width-100">
                <fieldset class="adminForm">
                <legend>Upload package file</legend>
                <table class="adminTable">
                <tr>
                    <td class="key"><%=base.GetLabel("", "package file", FileUpload1) %></td>
                    <td>
                        <uc1:FileUpload ID="FileUpload1" runat="server" ButtonText="Upload & install"
                        ErrorText="Error uploading file" SuccessText="File uploaded successfully" FilePrefix=""
                        FilePath="~/Installation/Uploads/" FileSize="4096" FileExtensions="zip" FileNameType="KeepOriginalName" 
                        UploadFields="1" OnAfterUpload="FileUpload1_AfterUpload" />
                    </td>
                </tr>
                </table>
                </fieldset>
                
                <fieldset class="adminForm">
                <legend>Install from directory</legend>
                <table class="adminTable">
                <tr>
                    <td class="key">local directory</td>
                    <td>
                    </td>
                </tr>
                </table>
                </fieldset>
                
                <fieldset class="adminForm">
                <legend>Install from URL</legend>
                <table class="adminTable">
                <tr>
                    <td class="key">url</td>
                    <td>
                    </td>
                </tr>
                </table>
                </fieldset>
            </div>
        </asp:View>
    
        <asp:View ID="ViewSeeModules" runat="server">
            <fieldset title="Filtri">
                <%=base.GetLabel("LblFilters", "filters")%>&nbsp;
                <asp:DropDownList ID="DropCoreFilter" runat="server" AutoPostBack="true" CssClass="adminMediumText" OnSelectedIndexChanged="DropCoreFilter_SelectedIndexChanged"></asp:DropDownList>
                <asp:TextBox ID="TxtNameFilter" runat="server" AutoPostBack="true" CssClass="adminShortText" 
                    ontextchanged="TxtNameFilter_TextChanged"></asp:TextBox>
                    <cc1:TextBoxWatermarkExtender TargetControlID="TxtNameFilter" runat="server" 
                        WatermarkText="<name>" WatermarkCssClass="adminMediumText watermark">
                    </cc1:TextBoxWatermarkExtender>                
            </fieldset>
            <br />
            <asp:GridView ID="GridModules" runat="server" Width="100%" AllowPaging="True" AllowSorting="false" AutoGenerateColumns="False"
                DataSourceID="ObjDs1" DataKeyNames="FullName"  OnRowCommand="GridModules_RowCommand" OnRowCreated="GridModules_RowCreated" OnRowDataBound="GridModules_RowDataBound">
                <Columns>                   
                    <asp:BoundField DataField="FullName" HeaderText="Name" SortExpression="FullName" />
                                       
                    <asp:TemplateField ItemStyle-Width="10" ItemStyle-HorizontalAlign="Center" HeaderText="Labels">
                        <ItemTemplate>
                            <div class="fancy">
                            <asp:HyperLink runat="server" ID="LnkModuleLabels">
                            <asp:Image ID="ImgAttribute" runat="server" SkinID="ImgAttributes" AlternateText="" Visible="true" />
                            </asp:HyperLink>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:BoundField DataField="FullVersion" HeaderText="Version" SortExpression="FullVersion" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="CreationDate" HeaderText="Date" SortExpression="CreationDate" DataFormatString="{0:dd-MMM-yyyy}" />
                    <asp:BoundField DataField="Author" HeaderText="Author" SortExpression="Author" />
                    <asp:TemplateField HeaderText="Core" SortExpression="IsCore" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" Enabled="false" Checked='<%#Eval("IsCore") %>' runat="server" />                
                        </ItemTemplate>
                    </asp:TemplateField>                    
                    <asp:TemplateField HeaderText="Uninstall" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="25">
                        <ItemTemplate>
                            <asp:ImageButton ID="LnkDel" CommandName="DeleteRow" CommandArgument='<%#Eval("FullName") %>' runat="server" 
                                SkinID="ImgDelFile" OnClientClick="return confirm('Uninstall item?');"  />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            
            <asp:ObjectDataSource ID="ObjDs1" runat="server" SortParameterName="" OnSelecting="ObjDs1_Selecting"
                SelectMethod="GetByFilter" TypeName="PigeonCms.ModuleTypeManager">
                <SelectParameters>
                    <asp:Parameter Name="filter" Type="object" />
                    <asp:Parameter Name="sort" Type="String" DefaultValue="" />
                </SelectParameters>
                <DeleteParameters>
                    <asp:Parameter Name="FullName" Type="String" />
                </DeleteParameters>
            </asp:ObjectDataSource>
        </asp:View>
   
        <asp:View ID="ViewSeeTemplates" runat="server">
            <fieldset title="Filtri">
            </fieldset>
            <br />
            <asp:GridView ID="GridTemplates" runat="server" Width="100%" AllowPaging="True" AllowSorting="false" AutoGenerateColumns="False"
                DataSourceID="ObjDsTemplates" DataKeyNames="Name"  OnRowCommand="GridTemplates_RowCommand" OnRowCreated="GridTemplates_RowCreated" OnRowDataBound="GridTemplates_RowDataBound">
                <Columns>
                    <asp:TemplateField ItemStyle-Width="10">
                        <ItemTemplate>
                        <asp:ImageButton ID="LnkSel" CommandName="Select" CommandArgument='<%#Eval("Name") %>' 
                            runat="server" SkinID="ImgEditFile" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                </Columns>
            </asp:GridView>
            
            <asp:ObjectDataSource ID="ObjDsTemplates" runat="server" SortParameterName="" OnSelecting="ObjDsTemplates_Selecting"
                SelectMethod="GetByFilter" TypeName="PigeonCms.ThemesObjManager">
                <SelectParameters>
                    <asp:Parameter Name="filter" Type="object" />
                </SelectParameters>
                <DeleteParameters>
                    <asp:Parameter Name="Name" Type="String" />
                </DeleteParameters>
            </asp:ObjectDataSource>
        </asp:View>   
   
        <asp:View ID="ViewEditModule" runat="server">

            <div class="adminToolbar">
                <asp:Button ID="BtnSaveModule" runat="server" Text="<%$ Resources:PublicLabels, CmdSave %>" CssClass="button" OnClick="BtnSaveModule_Click" />
                <asp:Button ID="BtnCancelModule" runat="server" Text="<%$ Resources:PublicLabels, CmdCancel %>" CssClass="button" OnClick="BtnCancelModule_Click" />
            </div>
            
            <div class="col width-50">
                <fieldset class="adminForm">
                <legend><%=base.GetLabel("LblDetails", "Details") %></legend>
                <table cellspacing="0" class="adminTable">
                <tr>
                    <td class="key">Tipo menu</td>
                    <td>
                        <asp:DropDownList ID="DropMenuTypes" CssClass="adminMediumText" runat="server">
                        </asp:DropDownList></td>
                </tr>
                    <tr>
                        <td class="key"><%=base.GetLabel("LblContentType", "Content type") %></td>
                        <td>
                            <asp:Literal ID="LitModuleType" runat="server"></asp:Literal></td>
                    </tr>                
                <tr>
                    <td class="key"><%=base.GetLabel("", "Name", TxtName)%></td>
                    <td>
                        <asp:TextBox ID="TxtName" MaxLength="200" runat="server" CssClass="adminMediumText"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="key">Title</td>
                    <td>
                        <asp:Panel runat="server" ID="PanelTitle"></asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td class="key"><%=base.GetLabel("", "Alias", TxtAlias) %></td>
                    <td>
                        <asp:TextBox ID="TxtAlias" Rows="3" runat="server" CssClass="adminMediumText"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="key"><%=base.GetLabel("", "Link", TxtLink)%></td>
                    <td>
                        <asp:TextBox ID="TxtLink" MaxLength="50" runat="server" CssClass="adminMediumText"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="key"><%=base.GetLabel("", "Redirect to", DropReferMenuId)%></td>
                    <td>
                        <asp:DropDownList ID="DropReferMenuId" CssClass="adminMediumText" runat="server">
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td class="key"><%=base.GetLabel("", "Parent item", ListParentId)%></td>
                    <td>
                        <asp:ListBox ID="ListParentId" SelectionMode="Single" Rows="18" runat="server">
                        </asp:ListBox>
                    </td>
                </tr>
                </table>
                </fieldset>
            </div>
            
            <div class="col width-50">
                <fieldset class="adminForm recordInfo">
                    <legend><%=base.GetLabel("LblRecordInfo", "Record info") %></legend>
                    <table cellspacing="0" class="adminTable">
                        <tr>
                            <td class="key"><strong>ID</strong></td>
                            <td><asp:Label ID="LblId" runat="server" Text=""></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="key"><strong>Module Id</strong></td>
                            <td><asp:Label ID="LblModuleId" runat="server" Text=""></asp:Label></td>
                        </tr>
                    </table>
                </fieldset>
            </div>
        </asp:View>
        
        <asp:View ID="ViewSql" runat="server">
            <div class="adminToolbar">
                <asp:Button ID="BtnSqlRun" runat="server" Text="Run" CssClass="button" OnClick="BtnSqlRun_Click" />
                <asp:Button ID="BtnSqlCancel" runat="server" Text="Cancel" CssClass="button" OnClick="BtnSqlCancel_Click" />
            </div>
                    
            <div class="col width-100">
                <fieldset class="adminForm">
                <legend>Sql query analyzer</legend>
                <table class="adminTable">
                <tr>
                    <td class="key"><%=base.GetLabel("LblSqlString", "Sql string", TxtSql)%></td>
                    <td>
                        <asp:TextBox ID="TxtSql" runat="server" TextMode="MultiLine" Width="750px" 
                            Height="300px"></asp:TextBox>                    
                    </td>
                </tr>
                <tr>
                    <td class="key"><%=base.GetLabel("LblResult", "Result") %></td>
                    <td>
                        <div style="width:750px; height:300px; overflow:scroll; ">
                        <asp:Literal ID="LitSqlResult" runat="server"></asp:Literal>
                        </div>
                    </td>
                </tr>
                </table>
                </fieldset>
            </div>
        </asp:View>
    </asp:MultiView>

</ContentTemplate>
</asp:UpdatePanel>