<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ListGallery.ascx.cs" Inherits="Controls_ListGallery" %>

<script type="text/javascript">
$(document).ready(function() {

    $("div.ListGallery a").fancybox({
		'hideOnContentClick': true
	});

});
</script>

<%=HeaderText %>
<div class="moduleBody ListGallery">
    <ul class="ListGallery">
    <%=ImagesListString %>
    </ul>
</div>
<%=FooterText %><br />
<%=ErrorText %>