<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" ValidateRequest="false" Title="{PigeonCms} - Installation wizard" Inherits="Installation_Default" %>
<%@ Import Namespace="PigeonCms" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphContent" Runat="Server">

<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="1" AssociatedUpdatePanelID="Upd1">
    <ProgressTemplate>
        <div class="loading"><%=PigeonCms.Utility.GetLabel("LblLoading", "loading") %></div>
    </ProgressTemplate>
</asp:UpdateProgress>
    
<asp:UpdatePanel ID="Upd1" runat="server">
<Triggers>
</Triggers>
<ContentTemplate>
    
    <h1>{PigeonCms} - Installation wizard</h1>

    <div class="row">

        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-body"> 
                    <div class="pull-right">
                        <div class="btn-group adminToolbar">
                            <asp:Button ID="BtnPrevious" runat="server" Text="Previous" CssClass="btn btn-default btn-xs" OnClick="BtnPrevious_Click" />
                            <asp:Button ID="BtnNext" runat="server" Text="Next" CssClass="btn btn-primary btn-xs" OnClick="BtnNext_Click" />
                        </div>
                    </div> 
                </div>
            </div>
        </div>

    </div>

    <div class="row">
        <div class="col-lg-12">
            <asp:Label ID="LblErr" runat="server" Text=""></asp:Label>
            <asp:Label ID="LblOk" runat="server" Text=""></asp:Label>
        </div>
    </div>

    <div class="row">

        <div class="col-sm-5">

            <div class="panel panel-default">
                <div class="panel-heading">
                    <i class="fa fa-clock-o fa-fw"></i> Steps
                </div>
                        
                <div class="panel-body">
                    <ul class="timeline">
                        <%=StepsList %>
                    </ul>
                </div>
            </div>


        </div>

        <div class="col-sm-7">

            <div class="panel panel-default">
                <div class="panel-body"> 

                    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0" OnActiveViewChanged="MultiView1_ActiveViewChanged">
            

                        <asp:View ID="ViewSystem" runat="server">
                            
                            <h2>Step: System check</h2>
                            
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h4 class="panel-title">
                                        Server variables
                                    </h4>
                                </div>

                                <div class="panel-body">
                                <%=ServerVariables %>
                                </div>
                            </div>


                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h4 class="panel-title">
                                        Files permissions
                                    </h4>
                                </div>

                                <div class="panel-body">
                                <%=FilesPermissionsList %>
                                </div>
                            </div>
         
                        </asp:View>
   
                        <asp:View ID="ViewDatabase" runat="server">

                            <h2>Step: Database settings</h2>

                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h4 class="panel-title">
                                    Connection string
                                    </h4>
                                </div>

                                <div class="panel-body">

                                    <div class="form-group col-lg-12">
                                        <%=Utility.GetLabel("", "sql server version", RadioSqlversion)%>
                                        <asp:RadioButtonList ID="RadioSqlversion" runat="server" AutoPostBack="true"
                                            OnSelectedIndexChanged="RadioSqlversion_SelectedIndexChanged" >
                                            <asp:ListItem Value="standard" Selected="True">standard</asp:ListItem>
                                            <asp:ListItem Value="express">express</asp:ListItem>
                                        </asp:RadioButtonList>

                                        <div class="well well-sm">
                                        choose between a standard or express sqlserver version (SQLServer 2005 and 2008 are supported)
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-12">
                                        <%=Utility.GetLabel("", "name", TxtConnectionName)%>
                                        <asp:TextBox ID="TxtConnectionName" Text="default" MaxLength="50" runat="server" CssClass="form-control"></asp:TextBox>
                                        <div class="well well-sm">
                                            the name of connection string. If you are an advanced user you can manage more than one database with the same installation
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-12">
                                        <%=PigeonCms.Utility.GetLabel("", "host name", TxtHostName)%>
                                        <asp:TextBox ID="TxtHostName" Text="(local)" MaxLength="50" runat="server" CssClass="form-control"></asp:TextBox>
                                        <div class="well well-sm">
                                            host name or ip address (".\SQLEXPRESS" for SqlServer Express version)
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-12">
                                        <%=PigeonCms.Utility.GetLabel("", "database name", TxtDbName)%>
                                        <asp:TextBox ID="TxtDbName" Text="pigeondb" MaxLength="50" runat="server" CssClass="form-control"></asp:TextBox>
                                        <div class="well well-sm">
                                            the name of the existing database to use
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-12">
                                        <%=PigeonCms.Utility.GetLabel("", "user id", TxtUserId)%>
                                        <asp:TextBox ID="TxtUserId" Text="" MaxLength="50" runat="server" CssClass="form-control"></asp:TextBox>
                                        <div class="well well-sm">
                                            database user
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-12">
                                        <%=PigeonCms.Utility.GetLabel("", "password", TxtPassword)%>
                                        <asp:TextBox ID="TxtPassword" Text="" MaxLength="50" runat="server" CssClass="form-control"></asp:TextBox>
                                        <div class="well well-sm">
                                            database password
                                        </div>
                                    </div>

                                </div>
                            </div>


                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h4 class="panel-title">
                                    Other settings
                                    </h4>
                                </div>
                                
                                <div class="panel-body">
                                    <div class="form-group col-lg-12">
                                        <%=PigeonCms.Utility.GetLabel("", "tables prefix", TxtTablesPrefix)%>
                                        <asp:TextBox ID="TxtTablesPrefix" Text="pgn_" MaxLength="50" runat="server" CssClass="form-control"></asp:TextBox>
                                        <div class="well well-sm">
                                            change the default value only if you want to use this database to manage more than one PigeonCms instance.
                                        </div>
                                    </div>
                                     
                                    <%--<tr>
                                        <td class="key"><%=PigeonCms.Utility.GetLabel("", "create database", ChkCreateDb) %></td>
                                        <td>
                                            <asp:CheckBox ID="ChkCreateDb" runat="server" Enabled="true" Checked="false" />
                                        </td>
                                        <td>check to create a new database</td>
                                    </tr>--%>
                                    <div class="form-group col-lg-12">
                                        <%=PigeonCms.Utility.GetLabel("", "backup old tables", ChkBackupTables)%>
                                        <asp:CheckBox ID="ChkBackupTables" runat="server" CssClass="form-control" Enabled="false" Checked="false" />
                                        <div class="well well-sm">
                                            check to create backup of the old existing tables
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-12">
                                        <%=PigeonCms.Utility.GetLabel("", "install example data", ChkExampleData)%>
                                        <asp:CheckBox ID="ChkExampleData" runat="server" CssClass="form-control" Enabled="false" Checked="true" />
                                        <div class="well well-sm">
                                            don't uncheck if you are not an advanced user
                                        </div>
                                    </div>                                
                                </div>

                            </div>

                        </asp:View>
        
                        <asp:View ID="ViewSite" runat="server">

                            <h2>Step: Site info</h2>

                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h4 class="panel-title">
                                    Site info
                                    </h4>
                                </div>

                                <div class="panel-body">

                                    <div class="form-group col-lg-12">
                                        <%=Utility.GetLabel("", "site title", TxtSiteTitle)%>
                                        <asp:TextBox ID="TxtSiteTitle" Text="" runat="server" CssClass="form-control"></asp:TextBox>
                                        <div class="well well-sm">
                                            example: Gino's website
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-12">
                                        <%=Utility.GetLabel("", "ecryption key", TxtEncryptKey)%>
                                        <asp:TextBox ID="TxtEncryptKey" Text="" runat="server" CssClass="form-control"></asp:TextBox>
                                        <div class="well well-sm">
                                            Auto-generated key. It is used to encrypt/decrypt cookies and other confidential data. 
                                            At least 8 alphanumeric chars.
                                        </div>
                                    </div>

                                </div>
                            </div>

                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h4 class="panel-title">
                                    Admin info
                                    </h4>
                                </div>

                                <div class="panel-body">

                                    <div class="form-group col-lg-12">
                                        <%=PigeonCms.Utility.GetLabel("", "admin password", TxtAdminPassword)%>
                                        <asp:TextBox ID="TxtAdminPassword" Text="" MaxLength="50" runat="server" CssClass="form-control"></asp:TextBox>
                                        <div class="well well-sm">administrator's password</div>
                                    </div>
                                                 
                                    <div class="form-group col-lg-12">
                                        <%=PigeonCms.Utility.GetLabel("", "e-mail", TxtEmail)%>
                                        <asp:TextBox ID="TxtEmail" Text="" MaxLength="50" runat="server" CssClass="form-control"></asp:TextBox>
                                        <div class="well well-sm">administrator's email</div>
                                    </div>
                                </div>
                            </div>

                        </asp:View>
        
                        <asp:View ID="ViewSummary" runat="server">

                            <h2>Step: Settings summary</h2>

                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h4 class="panel-title">
                                    Settings summary
                                    </h4>
                                </div>

                                <div class="panel-body">

                                    <div class="form-group col-lg-12">
                                        <%=Utility.GetLabel("", "connection string")%>
                                        <asp:TextBox ID="LitConnString"  Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-lg-12">
                                        <%=Utility.GetLabel("", "tables prefix")%>
                                        <asp:TextBox ID="LitTablesPrefix" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-lg-12">
                                        <%=Utility.GetLabel("", "backup old tables")%>
                                        <asp:TextBox ID="LitBackupTables" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-lg-12">
                                        <%=Utility.GetLabel("", "install example data")%>
                                        <asp:TextBox ID="LitInstallExampleData" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-lg-12">
                                        <%=Utility.GetLabel("", "site title")%>
                                        <asp:TextBox ID="LitSiteTitle" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-lg-12">
                                        <%=Utility.GetLabel("", "site url")%>
                                        <asp:TextBox ID="LitSiteUrl" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-lg-12">
                                        <%=Utility.GetLabel("", "encryption key")%>
                                        <asp:TextBox ID="LitEncryptKey" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-lg-12">
                                        <%=Utility.GetLabel("", "admin user")%>
                                        <asp:TextBox ID="LitAdminUser" Text="admin" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-lg-12">
                                        <%=Utility.GetLabel("", "admin password")%>
                                        <asp:TextBox ID="LitAdminPassword" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-lg-12">
                                        <%=Utility.GetLabel("", "email")%>
                                        <asp:TextBox ID="LitEmail" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>

                                </div>
                            </div>

                        </asp:View>
        
                        <asp:View ID="ViewFinish" runat="server">

                            <h2>Installation completed</h2>

                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <p>
                                    You have sucessfully installed PigeonCms.<br />
                                    Please visit <a href="https://github.com/picce/pigeoncms">github</a> for further
                                    information.</p>
                                    <p>
                                    Visit your website <a href='<%=Utility.GetAbsoluteUrl() %>'>homepage</a><br />
                                    </p>
                                </div>
                            </div>

                        </asp:View>        
                    </asp:MultiView>
            
                </div>
            </div>

        </div>

    </div>

</ContentTemplate>
</asp:UpdatePanel>     


    <script>

        $(document).ready(function () {

            <%--ajax loader--%>
            $("div.loading").html("<div style='width: 100vw;height: 100vh;background-color: rgba(250, 255, 255, .7);background: rgba(255, 255, 255, .7);position: fixed;z-index: 1000;left: 0;top: 0;'>"
            + "<img src='/App_Themes/SbAdmin/images/loading.gif' "
            + "style='position:fixed; top:45%; left:47%; width:64px; height:64px;' "
            + "alt='loading' /></div>");
            $("a.fancy").fancybox({
                'width': '80%',
                'height': '80%',
                'type': 'iframe',
                'hideOnContentClick': false,
                onClosed: function () { }
            });

        });

        function initToolTip(){
            $('.tooltip-demo').tooltip({
                selector: "[data-toggle=tooltip]",
                container: "body"
            });
        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function() {
            //initToolTip();
        });

    </script>

</asp:Content>