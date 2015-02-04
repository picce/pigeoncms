<%@ Control Language="C#" AutoEventWireup="true" CodeFile="VerticalList.ascx.cs" Inherits="Controls_Default" %>

<script type="text/javascript">
$(document).ready(function() {

    $("div.verticalListGallery a").fancybox({
		'hideOnContentClick': true
	});

});
</script>

<%=HeaderText %>
<div class="moduleBody verticalListGallery">
    <table cellpadding="4" cellspacing="0" class="verticalListGallery">
    <%=ImagesListString %>
    </table>
</div>
<%=FooterText %><br />
<%=ErrorText %>