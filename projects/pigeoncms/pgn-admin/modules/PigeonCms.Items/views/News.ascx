<%@ Control Language="C#" AutoEventWireup="true" CodeFile="News.ascx.cs" Inherits="Controls_Default" %>

<script type="text/javascript">

jQuery(document).ready(function(){
    $(".itemMore").fancybox({
        'scrolling': 'yes',
        'width': 550, 
        'height': 420, 
        'autoDimensions': false
    });
});

var win = null;
function printIt(printThis) {
    win = window.open();
    self.focus();
    win.document.open();
    win.document.write('<html><head><style>');
    win.document.write('body, td { font-family: Verdana; font-size: 10pt;}');
    win.document.write('</style></head><body>');
    win.document.write(printThis);
    win.document.write('</body></html>');
    win.document.close();
    win.print();
    win.close();
}

</script>

<div class='moduleBody <%=base.BaseModule.CssClass %>'>
   <%=HeaderText %>
   <ul class='<%=base.BaseModule.CssClass %>'>
   <%=ListString %>
   </ul>
    <%=DetailsString %>
    <%=FooterText %>
</div>
