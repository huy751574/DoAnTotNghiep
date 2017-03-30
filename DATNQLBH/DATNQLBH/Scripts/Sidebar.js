jQuery(document).ready(function () {
    if (jQuery('.Control').length > 0) {
        jQuery('.Control').bind('click', function (e) {

            if (e) e.preventDefault();
            if ($(this).hasClass('CloseBtn')) {
                jQuery('.SideBar').animate({
                    'margin-left': "-260px"                    
                }, 1000, function () {
                    // Animation complete.
                });
                $(".Control").removeClass('CloseBtn');
                $(".Control").addClass('OpenBtn');

                $('#iframeGame').animate({
                    'padding-left': 5//,
              //      'width': $(window).width() - 115
                });
                $('#iframeGame iframe').animate({
                    'padding-left': 5//,
              //      'width': $(window).width() - 115
                });
            } else {

                jQuery('.SideBar').animate({
                    'margin-left': 0,
                    'background-position':0
                });
                $(".Control").removeClass('OpenBtn');
                $(".Control").addClass('CloseBtn');
                $('#iframeGame').animate({
                    'padding-left': 260//,
                 //   'width': $(window).width() - 360
                });
                $('#iframeGame iframe').animate({
                    'padding-left': 260//,
                //    'width': $(window).width() - 360
                });
            }
        });
    }

});