<%@ Page Title="" Language="C#" MasterPageFile="~/pgn-content/masterpages/puppets.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="_default" %>

<%@ Register Assembly="PigeonCms.Core" Namespace="PigeonCms.Controls" TagPrefix="pgn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphHead" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="CphMain" runat="Server">

	<div class="o-fullscreen o-media-fullscreen" style="background-image: url('/assets/images/background_home.jpg')">

		<div class="u-table u-table--full u-table--center">

			<div class="u-table-cell">

				<img src="/assets/images/logo.svg" alt="Pigeon CMS" class="o-image o-image--center" />

				<h2 class="o-title o-title--hp"><%=base.GetLabel("SITE_default", "TopReasons", "Top ten reasons to choose the new CMS") %></h2>

				<h3 class="o-title o-title--small o-title--small--hp"><%=base.GetLabel("SITE_default", "KeyFeatures", "... just with few rude key features") %></h3>

				<a href="/contents/elements" class="o-link-button">
                    <span><%=base.GetLabel("SITE_default", "DiscoverPillars", "Discover the pillars") %></span>
                </a>

			</div>

		</div>

	</div>

</asp:Content>