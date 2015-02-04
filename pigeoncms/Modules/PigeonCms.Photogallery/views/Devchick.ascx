<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Devchick.ascx.cs" Inherits="Controls_Default" %>

<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="1" AssociatedUpdatePanelID="Upd1">
    <ProgressTemplate>
        <div class="loading"><%=PigeonCms.Utility.GetLabel("LblLoading", "loading") %></div>
    </ProgressTemplate>
</asp:UpdateProgress>

<asp:UpdatePanel ID="Upd1" runat="server">
<ContentTemplate>

    <div class='moduleBody modPhotogallery <%=BaseModule.CssClass %>'>
        <%=HeaderText %>
        <div class='gallery_box'>
            <ul class='gallery_demo'>
            <%=ImagesListString %>
            </ul>
            <p class='gallery_nav'><a href="#" onclick="$.galleria.prev(); return false;">previous</a> | <a href="#" onclick="$.galleria.next(); return false;">next</a></p>
        </div>
        <div class='modPhotogallery_pages <%=BaseModule.CssClass %>'>
            <asp:Panel ID="PanelChildsList" runat="server"></asp:Panel>
        </div>
        <!--<div style="clear:both;">-->
        <div id="main_image"></div>
        <%=FooterText %><br />
        <%=ErrorText %>
    </div>
    
    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

        function EndRequestHandler(sender, args) {
            if (args.get_error() == undefined) {
                gallery();
            }
        }
    </script>
    
</ContentTemplate>
</asp:UpdatePanel>

<script type="text/javascript">
    gallery();

    function gallery() {
        $(document).ready(function() {

            $('.gallery_demo_unstyled').addClass('gallery_demo'); // adds new class name to maintain degradability
            $('.gallery_nav').css('display', 'none'); // hides the nav initially

            $('ul.gallery_demo').galleria({
                history: true, // activates the history object for bookmarking, back-button etc.
                clickNext: true, // helper for making the image clickable
                insert: '#main_image', // the containing selector for our main image
                onImage: function(image, caption, thumb) { // let's add some image effects for demonstration purposes

                    // fade in the image & caption
                    image.css('display', 'none').fadeIn(1000);
                    caption.css('display', 'none').fadeIn(1000);

                    // fetch the thumbnail container
                    var _li = thumb.parents('li');

                    // fade out inactive thumbnail
                    _li.siblings().children('img.selected').fadeTo(500, 0.3);

                    // fade in active thumbnail
                    thumb.fadeTo('fast', 1).addClass('selected');

                    // add a title for the clickable image
                    image.attr('title', 'Next image >>');
                },
                onThumb: function(thumb) { // thumbnail effects goes here

                    // fetch the thumbnail container
                    var _li = thumb.parents('li');

                    // if thumbnail is active, fade all the way.
                    var _fadeTo = _li.is('.active') ? '1' : '0.3';

                    // fade in the thumbnail when finnished loading
                    thumb.css({ display: 'none', opacity: _fadeTo }).fadeIn(1500);

                    // hover effects
                    thumb.hover(
				function() { thumb.fadeTo('fast', 1); },
				function() { _li.not('.active').children('img').fadeTo('fast', 0.3); } // don't fade out if the parent is active
			)
                }
            });

        });
    }
</script>