/*
 *  Document   : base_pages_reminder.js
 *  Author     : pixelcave
 *  Description: Custom JS code used in Reminder Page
 */

var BasePagesReminder = function () {
    // Init Reminder Form Validation, for more examples you can check out https://github.com/jzaefferer/jquery-validation
    var initValidationReminder = function () {
        jQuery('.js-validation-reminder').validate({
            errorClass: 'help-block text-right animated fadeInDown',
            errorElement: 'div',
            errorPlacement: function (error, e) {
                jQuery(e).parents('.form-group .form-material').append(error);
            },
            highlight: function (e) {
                jQuery(e).closest('.form-group').removeClass('has-error').addClass('has-error');
                jQuery(e).closest('.help-block').remove();
            },
            success: function (e) {
                jQuery(e).closest('.form-group').removeClass('has-error');
                jQuery(e).closest('.help-block').remove();
            },
            rules: {
                'UserName': {
                    required: true,
                    minlength: 3
                },
                'Email': {
                    required: true,
                    email: true
                },
                'Verification': {
                    required: true,
                    minlength: 6
                },
                'NewPassword': {
                    required: true,
                    minlength: 6
                },
                'ConfirmNewPassword': {
                    required: true,
                    equalTo: '#NewPassword'
                }
            },
            messages: {

                'UserName': {
                    required: 'Nhập tài khoản',
                    minlength: 'Tên đăng nhập phải nhiều hơn 3 ký tự'
                },
                'Email': 'Nhập địa chỉ Email',
                'Verification': {
                    required: 'Nhập mã xác nhận',
                    minlength: 'Mã xác nhận phải đủ 6 ký tự'
                },
                'NewPassword': {
                    required: 'Nhập mật khẩu mới',
                    minlength: 'Mật khẩu phải nhiều hơn 5 ký tự'
                },
                'ConfirmNewPassword': {
                    required: 'Nhập mật khẩu mới',
                    minlength: 'Mật khẩu phải nhiều hơn 5 ký tự',
                    equalTo: 'Mật Khẩu mới và Xác nhận phải giống nhau'
                }
            }
        });
    };

    return {
        init: function () {
            // Init Reminder Form Validation
            initValidationReminder();
        }
    };
}();

// Initialize when page loads
jQuery(function () { BasePagesReminder.init(); });