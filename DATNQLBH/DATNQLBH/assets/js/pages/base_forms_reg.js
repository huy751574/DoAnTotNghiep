/*
 *  Document   : base_forms_wizard.js
 *  Author     : pixelcave
 *  Description: Custom JS code used in Form Wizard Page
 */

var BaseFormWizard = function () {
    // Init simple wizard, for more examples you can check out http://vadimg.com/twitter-bootstrap-wizard-example/

    // Init wizards with validation, for more examples you can check out http://vadimg.com/twitter-bootstrap-wizard-example/
    var initWizardValidation = function () {
        // Get forms
        var $form2 = jQuery('.js-form2');



        // Init form validation on the other wizard form
        var $validator2 = $form2.validate({
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
                    minlength: 6
                },
                'Password': {
                    required: true,
                    minlength: 6
                },
                'ConfirmPassword': {
                    required: true,
                    equalTo: '#Password'
                },
                'FullName': {
                    required: true,
                    minlength: 6
                },
                'Email': {
                    required: true,
                    email: true
                },
                'SDT': {
                    required: true,
                    minlength: 9,
                    maxlength: 12,
                    number: true
                },
                'birthday': {
                    required: true
                },
                'address': {
                    required: true,
                    minlength: 6
                }
            },
            messages: {
                'UserName': {
                    required: 'Tài khoàn không được để trống',
                    minlength: 'Tài khoản phải ít nhất 6 ký tự'
                },
                'Password': {
                    required: 'Mật khẩu không được để trống',
                    minlength: 'Mật khẩu phải ít nhất 6 ký tự'
                },
                'ConfirmPassword': {
                    required: 'Nhập lại mật khẩu',
                    equalTo: 'Mật khẩu mới và xác nhận không khớp'
                },
                'FullName': {
                    required: 'Họ tên không được để trống',
                    minlength: 'Họ tên phải ít nhất 6 ký tự'
                },
                'Email': 'Nhập địa chỉ Email',
                'SDT': {
                    required: 'Số điện thoại không được để trống',
                    minlength: 'Số điện thoại phải ít nhất 9 số',
                    maxlength: 'Số điện thoại chỉ được tối đa 12 số'
                },
                'birthday': {
                    required: 'Ngày sinh không được để trống'
                },
                'address': {
                    required: 'Địa chỉ không được để trống',
                    minlength: 'Địa chỉ phải ít nhất 6 ký tự'
                }
            }
        });

        // Init classic wizard with validation
        jQuery('.js-wizard-classic-validation').bootstrapWizard({
            'tabClass': '',
            'previousSelector': '.wizard-prev',
            'nextSelector': '.wizard-next',
            'onTabShow': function ($tab, $nav, $index) {
                var $total = $nav.find('li').length;
                var $current = $index + 1;

                // Get vital wizard elements
                var $wizard = $nav.parents('.block');
                var $btnNext = $wizard.find('.wizard-next');
                var $btnFinish = $wizard.find('.wizard-finish');

                // If it's the last tab then hide the last button and show the finish instead
                if ($current >= $total) {
                    $btnNext.hide();
                    $btnFinish.show();
                } else {
                    $btnNext.show();
                    $btnFinish.hide();
                }
            },
            'onNext': function ($tab, $navigation, $index) {
                var $valid = $form1.valid();

                if (!$valid) {
                    $validator1.focusInvalid();

                    return false;
                }
            },
            onTabClick: function ($tab, $navigation, $index) {
                return false;
            }
        });

        // Init wizard with validation
        jQuery('.js-wizard-validation').bootstrapWizard({
            'tabClass': '',
            'previousSelector': '.wizard-prev',
            'nextSelector': '.wizard-next',
            'onTabShow': function ($tab, $nav, $index) {
                var $total = $nav.find('li').length;
                var $current = $index + 1;

                // Get vital wizard elements
                var $wizard = $nav.parents('.block');
                var $btnNext = $wizard.find('.wizard-next');
                var $btnFinish = $wizard.find('.wizard-finish');

                // If it's the last tab then hide the last button and show the finish instead
                if ($current >= $total) {
                    $btnNext.hide();
                    $btnFinish.show();
                } else {
                    $btnNext.show();
                    $btnFinish.hide();
                }
            },
            'onNext': function ($tab, $navigation, $index) {
                var $valid = $form2.valid();

                if (!$valid) {
                    $validator2.focusInvalid();

                    return false;
                }
            },
            onTabClick: function ($tab, $navigation, $index) {
                return false;
            }
        });
    };

    return {
        init: function () {

            // Init wizards with validation
            initWizardValidation();
        }
    };
}();

// Initialize when page loads
jQuery(function () { BaseFormWizard.init(); });