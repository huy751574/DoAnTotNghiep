/*
 *  Document   : base_forms_validation.js
 *  Author     : pixelcave
 *  Description: Custom JS code used in Form Validation Page
 */

var BaseFormValidation = function() {
    // Init Bootstrap Forms Validation, for more examples you can check out https://github.com/jzaefferer/jquery-validation
    var initValidationBootstrap = function(){
        jQuery('.js-validation-bootstrap').validate({
            errorClass: 'help-block animated fadeInDown',
            errorElement: 'div',
            errorPlacement: function(error, e) {
                jQuery(e).parents('.form-group > div').append(error);
            },
            highlight: function(e) {
                jQuery(e).closest('.form-group').removeClass('has-error').addClass('has-error');
                jQuery(e).closest('.help-block').remove();
            },
            success: function(e) {
                jQuery(e).closest('.form-group').removeClass('has-error');
                jQuery(e).closest('.help-block').remove();
            },
            rules: {
                'FullName': {
                    required: true,
                    minlength: 6
                },
                'SDT': {
                    required: true,
                    number: true
                },
                'Address': {
                    required: true,
                    minlength: 6
                }
            },
            messages: {
                'FullName': {
                    required: 'Họ Tên không được để trống',
                    minlength: 'Họ tên không được ít hơn 6 ký tự'
                },
                'SDT': 'Số điện thoại không được để trống',
                'Address': {
                    required: 'Địa chỉ không được để trống',
                    minlength: 'Địa chỉ không được ít hơn 6 ký tự'
                }
            }
        });
    };
    return {
        init: function () {
            // Init Bootstrap Forms Validation
            initValidationBootstrap();
        }
    };
}();

// Initialize when page loads
jQuery(function(){ BaseFormValidation.init(); });