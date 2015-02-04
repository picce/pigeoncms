<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Default.ascx.cs" Inherits="Controls_Default" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
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
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0" OnActiveViewChanged="MultiView1_ActiveViewChanged">
    
        <asp:View ID="ViewSee" runat="server">

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
                            <div class="form-group col-sm-6 col-md-3">
                                <asp:DropDownList runat="server" ID="DropTopRowsFilter" AutoPostBack="true" CssClass="form-control" 
                                OnSelectedIndexChanged="Filter_Changed"></asp:DropDownList>
                            </div>
                            <div class="form-group col-sm-6 col-md-3">
                                <asp:DropDownList runat="server" ID="DropTracerItemTypeFilter" AutoPostBack="true" CssClass="form-control" 
                                OnSelectedIndexChanged="Filter_Changed"></asp:DropDownList>
                            </div>
                            <div class="form-group col-sm-6 col-md-3">
                                <asp:DropDownList runat="server" ID="DropModuleTypesFilter"  AutoPostBack="true" CssClass="form-control" 
                                OnSelectedIndexChanged="Filter_Changed"></asp:DropDownList>
                            </div>
                            <div class="form-group col-sm-6 col-md-3">
                                <asp:DropDownList runat="server" ID="DropDatesRangeFilter" AutoPostBack="true" CssClass="form-control" 
                                OnSelectedIndexChanged="Filter_Changed"></asp:DropDownList>
                            </div>
                            <div class="form-group col-sm-4">
                                <asp:TextBox ID="TxtIpFilter" runat="server" placeholder="<IP address>" AutoPostBack="true" CssClass="form-control" 
                                ontextchanged="Filter_Changed"></asp:TextBox>
                            </div>
                            <div class="form-group col-sm-4">
                                <asp:TextBox ID="TxtSessionIdFilter" runat="server" placeholder="<Session ID>" AutoPostBack="true" CssClass="form-control" 
                                ontextchanged="Filter_Changed"></asp:TextBox>
                            </div>
                            <div class="form-group col-sm-4">
                                <asp:TextBox ID="TxtDescriptionFilter" runat="server" placeholder="<description>" AutoPostBack="true" CssClass="form-control" 
                                ontextchanged="Filter_Changed"></asp:TextBox>
                            </div>
                        </div>
                    
                    </div>
                </div>
            </div>

            <div class="col-lg-12">

                <div class="panel panel-default">
                    <div class="table-responsive">

                    <asp:GridView ID="Grid1" runat="server" Width="100%" AllowPaging="true" 
                        AllowSorting="true" AutoGenerateColumns="false" 
                        DataSourceID="ObjDs1" DataKeyNames="Id" OnRowCommand="Grid1_RowCommand" 
                        OnRowCreated="Grid1_RowCreated" OnRowDataBound="Grid1_RowDataBound" 
                        ondatabinding="Grid1_DataBinding">
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="10">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LnkSel" runat="server" CausesValidation="false" CommandName="Select" 
                                    CommandArgument='<%#Eval("Id") %>'>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                    
                            <asp:BoundField DataField="DateInserted" HeaderText="Date" SortExpression="DateInserted" />

                            <asp:TemplateField HeaderText="IP/Session">
                                <ItemTemplate>
                                <asp:Literal ID="LitIp" runat="server" Text=""></asp:Literal><br />
                                <asp:Literal ID="LitSessionId" runat="server" Text=""></asp:Literal>
                                </ItemTemplate>
                            </asp:TemplateField>                    

                            <asp:TemplateField HeaderText="Module">
                                <ItemTemplate>
                                <asp:Literal ID="LitModule" runat="server" Text=""></asp:Literal>
                                </ItemTemplate>
                            </asp:TemplateField>
                    
                            <asp:BoundField DataField="UserInserted" HeaderText="User" SortExpression="UserInserted" />
                    
                            <asp:TemplateField HeaderText="Url" SortExpression="Url">
                                <ItemTemplate>
                                <asp:Literal ID="LitUrl" runat="server" Text=""></asp:Literal>
                                </ItemTemplate>
                            </asp:TemplateField>
                    
                            <asp:TemplateField HeaderText="Description">
                                <ItemTemplate>
                                <asp:Literal ID="LitDescription" runat="server" Text=""></asp:Literal>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%--<asp:TemplateField HeaderText="Del" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="25">
                                <ItemTemplate>
                                    <asp:ImageButton ID="LnkDel" CommandName="DeleteRow" CommandArgument='<%#Eval("Id") %>' runat="server" 
                                        SkinID="ImgDelFile" OnClientClick="return confirm('Cancellare la riga?');"  />
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                    
                            <asp:BoundField DataField="Id" HeaderText="ID" SortExpression="Id" />
                        </Columns>
                    </asp:GridView>

                    </div>
                </div>

            </div>
            
            <asp:ObjectDataSource ID="ObjDs1" runat="server"  SortParameterName="sort" OnSelecting="ObjDs1_Selecting"
                SelectMethod="GetByFilter" TypeName="PigeonCms.LogItemsManager">
                <SelectParameters>
                    <asp:Parameter Name="filter" Type="object" />
                    <asp:Parameter Name="sort" Type="String" />
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
                            <asp:Button ID="BtnCancel" runat="server" Text="<%$ Resources:PublicLabels, CmdCancel %>" CssClass="btn btn-default btn-xs" OnClick="BtnCancel_Click" />
                        </div>
                    </div>
                </div>

            </div>

            <div class="panel-body">

                <div class="form-group col-sm-12">
                    <strong><%=base.GetLabel("LblRecordId", "ID") %></strong>
                    <asp:Literal ID="LitId" runat="server" Text=""></asp:Literal>
                </div>

                <div class="form-group col-sm-12">
                    <strong><%=base.GetLabel("LblCreated", "Created") %></strong>
                    <asp:Literal ID="LitCreated" runat="server" Text=""></asp:Literal>
                </div>

                <div class="form-group col-sm-12">
                    <strong><%=base.GetLabel("LblType", "Type") %></strong>
                    <asp:Literal ID="LitType" runat="server"></asp:Literal>
                </div>

                <div class="form-group col-sm-6">
                    <%=base.GetLabel("LblModuleType", "Module") %>
                    <asp:TextBox ID="LitModuleType" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                </div>

                <div class="form-group col-sm-6">
                    <%=base.GetLabel("LblView", "View") %>
                    <asp:TextBox ID="LitView" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                </div>

                <div class="form-group col-sm-4">
                    <%=base.GetLabel("LblIp", "Ip") %>
                    <asp:TextBox ID="LitUserHostAddress" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                </div>

                <div class="form-group col-sm-4">
                    <%=base.GetLabel("LblSessionId", "Session ID") %>
                    <asp:TextBox ID="LitSessionId" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                </div>

                <div class="form-group col-sm-4">
                    <%=base.GetLabel("LblUser", "User") %>
                    <asp:TextBox ID="LitUserInserted" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                </div>
                    
                <div class="form-group col-sm-12">
                    <%=base.GetLabel("LblUrl", "Url") %>
                    <asp:TextBox ID="LitUrl" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                </div>

                <div class="form-group col-sm-12">
                    <%=base.GetLabel("LblDescription", "Description") %>
                    <asp:TextBox ID="LitDescription" CssClass="form-control" TextMode="MultiLine" Rows="6" Enabled="false" runat="server"></asp:TextBox>
                </div>

            </div>
            
        </asp:View>
    </asp:MultiView>
    </div>

</ContentTemplate>
</asp:UpdatePanel>