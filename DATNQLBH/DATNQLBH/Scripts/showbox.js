jQuery(document).ready(function ($) {
    $(".fancybox").fancybox({
        fitToView: false,
        autoSize: false,
        autoDimensions: false,
        width: 420,
        height: 510,
        title: 'Đăng Nhập',
        helpers: {
            title: {
                type: 'float'
            },
        },
        'transitionIn': 'elastic',
        'transitionOut': 'elastic',
        'afterClose': function () {
            window.location.reload();
        },
    });
});
jQuery(document).ready(function ($) {
    $(".fancybox1").fancybox({
        fitToView: false,
        autoSize: false,
        autoDimensions: false,
        width: 620,
        height: 510,
        title: 'Thêm Hàng',
        helpers: {
            title: {
                type: 'float'
            },
        },
        'transitionIn': 'elastic',
        'transitionOut': 'elastic',
        'afterClose': function () {
            window.location.reload();
        },
    });
});