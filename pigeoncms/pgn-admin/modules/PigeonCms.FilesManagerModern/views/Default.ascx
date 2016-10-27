<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Default.ascx.cs" Inherits="Controls_FilesManagerModern_default" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Import Namespace="PigeonCms" %>

<script>
	function ReloadGrid() {
		var upd1 = '<%=Upd1.ClientID%>';
		if (upd1 != null) {
			__doPostBack(null, 'grid');
		}
	}

	function pageLoad(sender, args) {
		if (args.get_isPartialLoad()) {

			bindUploadFiles();//in UploadFiles.js
		}
	}
</script>

<asp:ScriptManager runat="server" EnablePageMethods="true"></asp:ScriptManager>
<asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="1" AssociatedUpdatePanelID="Upd1" Visible="false">
    <ProgressTemplate>
        <div class="loading"><%=PigeonCms.Utility.GetLabel("LblLoading", "loading") %></div>
    </ProgressTemplate>
</asp:UpdateProgress>

<asp:UpdatePanel ID="Upd1" runat="server" UpdateMode="Conditional">
<Triggers>
</Triggers>
<ContentTemplate>

    <span class="error" style="display:none;"><asp:Literal ID="LblErr" runat="server" Text=""></asp:Literal></span>
    <span class="success" style="display:none;"><asp:Literal ID="LblOk" runat="server" Text=""></asp:Literal></span>

    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
    <asp:View ID="ViewSee" runat="server">
        <div class="pigeoncms-filesmanager-litContentBefore">
            <asp:Literal runat="server" ID="LitContentBefore"></asp:Literal>
        </div>

        <br />
        
        <fieldset class="adminFilters"></fieldset>
        <br />

        <div class="lightbox-fileupload-modern">

            <div class="pgn-fileUpload-litAllowedFiles">
				<%=base.GetLabel("Allowedfiles", "Allowed files")%>: 
				<asp:Literal runat="server" ID="LitRestrictions"></asp:Literal>
            </div>

            <div class="pgn-fileUpload-litFolder">
				<asp:Literal runat="server" ID="LitFolder"></asp:Literal>
            </div>

			<div class="adminToolbar">

				<asp:TextBox ID="TxtNewFolder" runat="server" CssClass="js-txt-newfolder adminSmallText"></asp:TextBox>
				<cc1:TextBoxWatermarkExtender TargetControlID="TxtNewFolder" runat="server" 
					WatermarkText="<folder>" WatermarkCssClass="adminSmallText watermark">
					</cc1:TextBoxWatermarkExtender>
				
				<asp:Button runat="server" ID="BtnNewFolder" OnClick="BtnNewFolder_Click" CssClass="button btn btn-modern" Text="Create folder" />
				
				<asp:Button runat="server" ID="BtnParentFolder" OnClick="BtnParentFolder_Click" CssClass="button btn btn-modern" Text=".." />

			</div>

            <div class="pgn-fileUpload-panelFields">
                <div ID="PanelFields" class="pgn-fileUpload-panelFields-inner clearfix">
                    <div id="items"></div>

                    <asp:Repeater ID="repImages" runat="server" 
						OnItemCommand="repImages_ItemCommand"
						OnItemDataBound="repImages_ItemDataBound">
                        <HeaderTemplate></HeaderTemplate>
                        <ItemTemplate>
                                <asp:Panel ID="BoxContainer" runat="server" CssClass="box-container">
                                    <asp:Panel ID="Box" runat="server" CssClass="box">

                                        <asp:Panel ID="ActionsLayer" runat="server" CssClass="box-actions-layer">

											<%--delete--%>
                                            <asp:Label runat="server" ID="lblDelete"
												CssClass="box-actions__label box-actions__label--delete js-delete">delete</asp:Label>
                                            <asp:Button ID="BtnDel" runat="server" CssClass="" CommandName="delete" style="display:none;"  />

                                        </asp:Panel>

                                        <asp:Label ID="lblFileName" runat="server" CssClass="box__filename"></asp:Label>
										<%--navigate--%>
										<asp:Button ID="BtnNavigate" runat="server" CommandName="navigate" CssClass="js-btn-navigatefolder-trigger" style="display:none;"></asp:Button>

                                    </asp:Panel>

                                    <div class="label-container">
                                        <asp:TextBox runat="server" ID="TxtName" CssClass="image-name js-txtname"></asp:TextBox>
                                        <asp:Label runat="server" ID="lblSize" CssClass="image-size"></asp:Label>
                                    </div>

                                </asp:Panel>
                        </ItemTemplate>
                        <FooterTemplate></FooterTemplate>
                    </asp:Repeater>

                    <asp:Panel ID="BoxContainer" runat="server" CssClass="box-container no-image">
                        <div id="dragandrophandler" class="box box-insert">
                            <asp:Label ID="lblNoimage" runat="server" CssClass="icon icon-image"></asp:Label>
                            <asp:Label ID="lblNoimageHover" runat="server" CssClass="icon icon-image icon-image--hover"></asp:Label>
                            <asp:Label ID="lblTrascina" runat="server" CssClass="drop-label"><%=base.GetLabel("click or drag", "clicca qui o trascina") %></asp:Label>
                            <div class="progress-bar"><div></div></div>
                        </div>
                        <div class="label-container">
                            <asp:Label runat="server" ID="lblName" CssClass="image-name">&nbsp</asp:Label>
                            <asp:Label runat="server" ID="lblSize" CssClass="image-size">&nbsp</asp:Label>
                            <fieldset class="adminForm" style="display:none;">
								<div class="lightbox-fileupload-modern">
									<asp:Panel runat="server" ID="UploadContainer"> 
										<asp:FileUpload runat="server" ID="FileUpload1" ClientIDMode="Static" />
									</asp:Panel>
								</div>
                            </fieldset>
                        </div>
                    </asp:Panel>

                </div>
            </div>

        </div>

        <div class="pigeoncms-filesmanager-litContentAfter">
            <asp:Literal runat="server" ID="LitContentAfter"></asp:Literal>
        </div>
    </asp:View>
    
    </asp:MultiView>

	<asp:HiddenField ID="hidPath" runat="server" ClientIDMode="Static" />
	<asp:HiddenField ID="hidCurrViewPath" runat="server" ClientIDMode="Static" />
	<asp:HiddenField ID="hidMuduleId" runat="server" ClientIDMode="Static" />
	<asp:HiddenField ID="hidFolderName" runat="server" ClientIDMode="Static" />

<%--	<div class="edit-container">
        <div class="adminToolbar">
            <asp:Button ID="BtnSave" runat="server" Text="<%$ Resources:PublicLabels, CmdSave %>" CssClass="button" OnClientClick="MyObject.UpdateEditorFormValue();" OnClick="BtnSave_Click" />
            <asp:Button ID="BtnCancel" runat="server" Text="<%$ Resources:PublicLabels, CmdCancel %>" CssClass="button" OnClick="BtnCancel_Click" />
        </div>  
          
        <div class="col width-100">
            <fieldset class="adminForm">
                <legend></legend>
                <table class="adminTable">
                <tr>
                    <td class="key"><%=base.GetLabel("", "File", TxtFileName)%></td>
                    <td>
                        <asp:TextBox ID="TxtFileName" runat="server" CssClass="adminMediumText"></asp:TextBox>
                    </td>
                </tr>                
                <tr>
                    <td class="key"><%=base.GetLabel("LblTitle", "Title") %></td>
                    <td>
                        <asp:Panel runat="server" ID="PanelTitle"></asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td class="key"><%=base.GetLabel("LblDescription", "Description") %></td>
                    <td>
                        <asp:Panel runat="server" ID="PanelDescription"></asp:Panel>
                    </td>
                </tr>
                </table>
            </fieldset>
        </div>
	</div>--%>

</ContentTemplate>
</asp:UpdatePanel>