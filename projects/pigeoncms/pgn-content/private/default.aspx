<%@ Page Title="" Language="C#" MasterPageFile="~/pgn-content/masterpages/puppets.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="private_default" %>
<%@ Register Assembly="PigeonCms.Core" Namespace="PigeonCms.Controls" TagPrefix="pgn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphHead" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="CphMain" Runat="Server">

    <div class="o-container">

        <div class="o-row o-row--small u-table u-table--full u-pad-tb--5 u-border--bottom">

			<div class="o-title o-title--small o-col o-col--20 u-table-cell">
				<%=GetLabel("AQ_private", "Title", "Private area")%>   
			</div>

			<div class="o-title o-subtitle--big o-col o-col--80 u-table-cell">
                <%=Description  %>
			</div>

		</div>

    </div>

</asp:Content>