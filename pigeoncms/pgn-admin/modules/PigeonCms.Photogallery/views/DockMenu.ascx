<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DockMenu.ascx.cs" Inherits="Controls_Default" %>

<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="1" AssociatedUpdatePanelID="Upd1">
    <ProgressTemplate>
        <div class="loading"><%=PigeonCms.Utility.GetLabel("LblLoading", "loading") %></div>
    </ProgressTemplate>
</asp:UpdateProgress>

<asp:UpdatePanel ID="Upd1" runat="server">
<ContentTemplate>
    <%=HeaderText %>
    <!--[if lt IE 7]>
     <style type="text/css">
     .dock img { behavior: url(DockMenu_iepngfix.htc) }
     </style>
    <![endif]-->

    <!--top dock -->
    <div class="dock" id="dock">
        <div class="dock-container">
        <%=ImagesListString %>
        </div>
    </div>

    <div id="main_image">
        <a id="fancylink" href="<%=FirstImageSrc %>" title="<%=FirstImageTitle %>">
            <img id="placeholder" src="<%=FirstImageSrc %>" alt="" />
        </a>
    </div>
    <p id="descImage"><%=FirstImageTitle %></p>
    <asp:Panel ID="PanelChildsList" runat="server">
    </asp:Panel>
    <%=FooterText %><br />
    <%=ErrorText %>
    
    <script type="text/javascript">
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
    
    function EndRequestHandler(sender, args)
	{
	    if (args.get_error() == undefined){
	        fishEye();
	        imgClick();
	        fancy();
	    }
	}
    </script>
</ContentTemplate>
</asp:UpdatePanel>

<!--dock menu JS options -->
<script type="text/javascript">
    fishEye();
	imgClick();
	fancy();
    
    function fishEye()
    {
        $(document).ready(function(){
	            $('#dock').Fisheye(
		            {
			            maxWidth: 50,
			            items: 'a',
			            itemsText: 'span',
			            container: '.dock-container',
			            itemWidth: 50,
			            proximity: 50,
			            halign : 'center'
		            }
	            );
	    })
    }
    
    function imgClick()
    {
        $(document).ready(function(){
            $('#dock .dock-item').click(
                function (event)
                {
                    event.preventDefault();
                    
                    var image =  this.href;
                    var description = this.text;
                    
                    $('a#fancylink').attr('href', image);
                    $('a#fancylink').attr('title', description);
                    $('img#placeholder').attr('src', image);
                    $('img#placeholder').css('display', 'none').fadeIn(500);
                    $('#descImage').empty();
                    $('#descImage').append(description);
                }
            );
        }) 
    }
    
    function fancy()
    {
        $(document).ready(function(){
	        $("div#main_image a").fancybox({
		        'hideOnContentClick': true
	        });
	    })
    }
</script>