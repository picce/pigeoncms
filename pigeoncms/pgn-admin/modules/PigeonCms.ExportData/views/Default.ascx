<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Default.ascx.cs" Inherits="PigeonCms_ExportData_Default" %>
<%@ Register src="~/Controls/ContentEditorControl.ascx" tagname="ContentEditorControl" tagprefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<script type="text/javascript">
    var deleteQuestion = '<%=PigeonCms.Utility.GetLabel("RECORD_DELETE_QUESTION")%>';
</script>



<asp:ScriptManager runat="server" EnablePageMethods="true"></asp:ScriptManager>
<asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="1" AssociatedUpdatePanelID="Upd1">
    <ProgressTemplate>
        <div class="loading"><%=PigeonCms.Utility.GetLabel("LblLoading", "loading") %></div>
    </ProgressTemplate>
</asp:UpdateProgress>

<asp:UpdatePanel ID="Upd1" runat="server">
<Triggers>
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

        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-body"> 
                    <div class="pull-right">
                        <div class="btn-group adminToolbar">
                            <asp:Button ID="BtnExport" runat="server" Text="Export" CssClass="btn btn-primary clearfix" OnClick="BtnExport_Click" />
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
                            <%=base.GetLabel("Resource", "Resource", DropResourceFilter) %>
                            <asp:DropDownList ID="DropResourceFilter" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="Filter_Changed">
                            </asp:DropDownList>
                        </div>
                                        
                        <div class="form-group col-md-6 col-lg-3">
                            <%=base.GetLabel("LblMissingValues", "Missing values", DropMissingFilter) %>
                            <asp:DropDownList ID="DropMissingFilter" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="Filter_Changed"></asp:DropDownList>
                        </div>
                        <div class="form-group col-md-6 col-lg-3">
                            <%=base.GetLabel("LblValuesStartsWith", "Only values that starts with", TxtValuesStartsWithFilter) %>
                            <asp:TextBox runat="server" ID="TxtValuesStartsWithFilter" AutoPostBack="true" CssClass="form-control" MaxLength="10" OnTextChanged="Filter_Changed"></asp:TextBox>
                        </div>
                        <div class="form-group col-md-6 col-lg-3">
                            <%=base.GetLabel("LblValuesContains", "Only values that contains", TxtValuesContainsFilter) %>
                            <asp:TextBox runat="server" ID="TxtValuesContainsFilter" AutoPostBack="true" CssClass="form-control" MaxLength="10" OnTextChanged="Filter_Changed"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-lg-12">

            <div class="panel panel-default">
                <div class="table-responsive">

                    <asp:GridView ID="Grid1" runat="server" AllowPaging="false" AllowSorting="false" Width="100%" 
                        AutoGenerateColumns="True"
                        OnPageIndexChanging="Grid1_PageIndexChanging"  
                        OnRowCreated="Grid1_RowCreated" OnRowDataBound="Grid1_RowDataBound">
                        <Columns>
                    

                        </Columns>
                    </asp:GridView>

                </div>
            </div>
        </div>


    </div>

</ContentTemplate>
</asp:UpdatePanel>