<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Item.ascx.cs" Inherits="Controls_Default" %>

 <script type="text/javascript"> 
$(document).ready(function() {

    $(".modItem_defaultImage a").fancybox({
	    'hideOnContentClick': true
    });

    $(".modItem_images a").fancybox({
	    'hideOnContentClick': true
    });
});
</script>


<div class='moduleBody modItem <%=BaseModule.CssClass %>'>
    <%=this.HeaderText %>
    
    <div class='modItem_defaultImage <%=BaseModule.CssClass %>'>
    <%=LitDefaultImage %>
    </div>
    
    <div class='modItem_title <%=BaseModule.CssClass %>'>
    <%=CurrItem.Title %>
    </div>
    
    <div class='modItem_path <%=BaseModule.CssClass %>'>
    <%=PathString.ToString() %>
    </div>
    
    <div class='modItem_description <%=BaseModule.CssClass %>'>
    <%=LitDescriptionPage %>
    </div>
    
    <div class='modItem_images <%=BaseModule.CssClass %>'>
    <ul>
    <%=LitImages %>
    </ul>
    </div>
    
    <div class='modItem_files <%=BaseModule.CssClass %>'>
    <ul>
    <%=LitFiles %>
    </ul>
    </div>

    <div class='modItem_pages <%=BaseModule.CssClass %>'>
    <ul>
    <%=LitPages %>
    </ul>
    </div>
   
    <%=this.FooterText %>
</div>
