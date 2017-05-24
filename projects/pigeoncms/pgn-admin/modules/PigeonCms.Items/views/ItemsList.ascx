<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ItemsList.ascx.cs" Inherits="Controls_ItemsList" %>

<script type="text/javascript">

jQuery(document).ready(function(){
    $(".itemMore").fancybox({
        'scrolling': 'yes',
        'width': 500, 
        'height': 380, 
        'autoDimensions': false
    });
});

</script>

<div class='moduleBody <%=base.BaseModule.CssClass %>'>
    <%=HeaderText %>
   
    <ul class='<%=base.BaseModule.CssClass %>'>
    <%=ListString %>
    </ul>
    <%=FooterText %>
</div>
