<%@ Control Language="C#" AutoEventWireup="true" CodeFile="JqueryCycle.ascx.cs" Inherits="Controls_Default" %>

<%=HeaderText %>
<div class="moduleBody jqueryCycleSlide">
    <%=ImagesListString %>
</div>
<%=FooterText %><br />
<%=ErrorText %>

<script type="text/javascript">
//<![CDATA[
$().ready( function(){ $('.jqueryCycleSlide').cycle('fade'); } );
var Page_ValidationActive = false;
if (typeof(ValidatorOnLoad) == "function") {
    ValidatorOnLoad();
}
//]]>
</script>