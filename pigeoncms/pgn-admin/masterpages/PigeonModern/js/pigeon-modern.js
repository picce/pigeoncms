$(function() {

    var mobileThreshold = 768,
        sideMenu = $('#side-menu'),
        $document = $(document),
        $window = $(window),
        $body = $('body'),
        menuMobile = $('.navbar-header > .navbar-toggle'),
        searchIco = $('.search-modern'),
        textboxSearch = $('.textbox--search'),
        sidebarModernVoice = $('.sidebar-modern .menulist:first-child > li'),
        sidebarModernVoiceLink = sidebarModernVoice.find('> a'),
        btnEditFollow = $('.btn-group-follow'),
        tableModern =  $('.table-modern--wrapper'),
        tableModernHover = $('.table-modern--hover');


    //initialize menu bar
    if (sideMenu.length > 0 && typeof sideMenu.metisMenu !== 'undefined') {
        
        sideMenu.metisMenu();

    }


    //click on search icon
    if (searchIco.length > 0) {

        searchIco.on('click', function() {

            searchIco.toggleClass('is-active');
            textboxSearch.toggleClass('is-active');

            textboxSearch.focus();

        });

    }


    //rotation icon menu admin on click
    if (sidebarModernVoice.length > 0) {

        sidebarModernVoiceLink.on('click', function(e) {

            e.preventDefault();

            var $this = $(this),
                hasToAddClass = true;

            if ($this.hasClass('rotate-ico')) {
                hasToAddClass = false;
            }

            sidebarModernVoiceLink.removeClass('rotate-ico');

            if (hasToAddClass) {
                $this.addClass('rotate-ico');
            } else {
                $this.removeClass('rotate-ico');
            }

        });

    }


    //animation on menu mobile click
    if (menuMobile.length > 0) {

        menuMobile.on('click', function() {

            var $this = $(this);
            $this.toggleClass('active');

        });

    }


    //hover on row's table, the content follow the scroll
    if (tableModern.length > 0) {

        tableModern.on('scroll', function() {

            if (tableModernHover.length > 0) {

                tableModernHover.css({
                    left: tableModern.scrollLeft()
                });

            }
        });

    }


    //open edit menu item
    if ($('.table-modern-edit').length > 0) {

        $document.on('click', '.table-modern-edit', function() {

            var $this = $(this);

            //lock row scroll (.table-modern--wrapper)
            $this.parent().parent().addClass("locked");

            //hide all others
            $this.parent().parent().find('.table-modern--hover').fadeOut();

            //show this
            $this.parent().find('.table-modern--hover').fadeIn();

        });
    }


    //close edit menu item
    if ($('.table-modern-edit--close').length > 0) {

        $document.on('click', '.table-modern-edit--close', function() {

            var $this = $(this);

            //unlock row scroll (.table-modern--wrapper)
            $this.parent().parent().parent().removeClass("locked");

            //hide this
            $this.parent().fadeOut();

        });
    }


    //Loads the correct sidebar on window load,
    //collapses the sidebar on window resize.
    // Sets the min-height of #page-wrapper to window size
    $window.bind('load resize', function() {

        var topOffset = 50,
            windowWidth = (this.window.innerWidth > 0) ? this.window.innerWidth : this.screen.width,
            windowHeight = (this.window.innerHeight > 0) ? this.window.innerHeight : this.screen.height,
            navbarCollapse = $('div.navbar-collapse'),
            pageWrapper = $('#page-wrapper');

        if (windowWidth < mobileThreshold) {
            navbarCollapse.addClass('collapse');
            topOffset = 100; // 2-row-menu
        } else {
            navbarCollapse.removeClass('collapse');
        }

        windowHeight = windowHeight - topOffset;

        if (windowHeight < 1) windowHeight = 1;
        if (windowHeight > topOffset) {
            pageWrapper.css('min-height', (windowHeight) + 'px');
        }

        //do rotation of icon in sidebar menu
        if (sidebarModernVoice.length > 0) {

            sidebarModernVoice.each(function(index) {

                var $this = $(this);
                if ($this.hasClass('active')) {
                    $this.find('a').addClass('rotate-ico');
                }

            });

        }

    });


    $('.fancybox').click(function () {

        $.fancybox([
            { href : '#fancybox-popup-form' }
        ]);

    });


    $document.on('click', '.js-open-fancy', function(){

        var $this = $(this);
        if ($this.attr('href') ) 
            link = $this.attr('href');
        else
            link = $this.attr('data-href');

        $.fancybox({

            'width': '100%',
            'height': '100%',
            'type': 'iframe',
            'href': link,
            'hideOnContentClick': false,
            onStart: function () { 
                $body.addClass('blocked'); 
            },
            onClosed: function () {

                $body.removeClass('blocked');

                if (typeof mod_ReloadUpd1 == 'function') { 
                    mod_ReloadUpd1();
                }

            }
        });

        return false;

    });    


    //close lightbox upload files
    $document.on('click', '.js-close-lightbox', function () {

        closeLightBox();

    });


    //close confirm lightbox
    $document.on('click', '.icon-close-confirm', function () {

        koConfirm();

    });


    //close message lightbox
    $document.on('click', '.icon-close-message', function () {

       closeMessage();

    });



    /****************
        SORTING
    ****************/

    //click move element
    $(document).on('mouseenter', '.js-move', function () {

        var $this = $(this);
        //enable sorting
        $(".table-modern--sortable").sortable("option", "disabled", false);

    });


    $(document).on('mouseleave', '.js-move', function () {

        var $this = $(this);
        //disable sorting
        $(".table-modern--sortable").sortable("option", "disabled", true);

    });


    //click delete element
    $(document).on('click', '.js-delete', function () {

        var $this = $(this);
        $this.next().addClass('js-confirm-operation');

        openConfirm(
            $this.data("msg-title"), 
            $this.data("msg-subtitle"), 
            $this.data("msg-cancel"), 
            $this.data("msg-confirm")
        );

    });

});


//group of edit buttons follow you on scroll
var initialPosEditBtns = 0,
    posCalculatedEditBtns = true;

function onScrollEditBtns() {

    if ($('.btn-group-follow').length > 0) {

        if (posCalculatedEditBtns) {

            initialPosEditBtns = $('.btn-group-follow').offset().top;
            posCalculatedEditBtns = false;

        } else {

            var offsetContentScrolled = $('.panel-modern--scrollable').scrollTop();

            if (offsetContentScrolled > (initialPosEditBtns)) {

                $('.btn-group-follow').addClass('to-follow');

            } else {

                $('.btn-group-follow').removeClass('to-follow');

            }

        }

    }

}


// function open confirm lightbox
function openConfirm(title, subtitle, cancelText, confirmText) {
    // var $confirmContainer = window.parent.$('.container-confirm-operation');
    var $confirmContainer = $('.container-confirm-operation');
    
    if (title !== undefined && title.length > 0)
        $confirmContainer.find(".js-title").text(title);

    if (subtitle !== undefined && subtitle.length > 0)
        $confirmContainer.find(".js-subtitle").text(subtitle);

    if (cancelText !== undefined && cancelText.length > 0)
        $confirmContainer.find(".js-cancel").text(cancelText);    

    if (confirmText !== undefined && confirmText.length > 0)
        $confirmContainer.find(".js-confirm").text(confirmText);    

    $confirmContainer.fadeIn();

}


// function click confirm lightbox
function okConfirm() {
    $('.js-confirm-operation').trigger('click');
    koConfirm();
}


// function annulla confirm lightbox
function koConfirm() {

    // var $confirmContainer = window.parent.$('.container-confirm-operation');
    var $confirmContainer = $('.container-confirm-operation');
    if ($confirmContainer != undefined){
        $('.js-confirm-operation').removeClass('js-confirm-operation');
        $confirmContainer.fadeOut();
    }

}


// function open message lightbox
function openMessage(message) {
    
    // var $container = window.parent.$('.container-message-lightbox');
    var $container = $('.container-message-lightbox');
    
    $container.find(".js-message").text(message);
    $container.fadeIn();

    //hide fncybox close
    parent.$("#fancybox-close").css("display", "none !important");
}


// close message lightbox
function closeMessage() {

    // window.parent.$('.container-message-lightbox').fadeOut();
    $('.container-message-lightbox').fadeOut();

    //re-show fncybox close
    parent.$("#fancybox-close").css("display", "block");
}


function closeLightBox(){

    var lightbox = $('.lightbox-container');

    lightbox.fadeOut(300, function() {

        $('body').removeClass('blocked');
        //avoid viewstate corruption on postback caused to multiple form
        $('.body-modern-masterblank--inner').html('');

    });

}


function closeFancy(){

    parent.$.fancybox.close();

}

