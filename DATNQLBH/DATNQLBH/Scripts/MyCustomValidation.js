jQuery.extend(jQuery.validator.messages, {
    required: "Không được để trống.",
    remote: "Please fix this field.",
    email: "Vui lòng điền đúng địa chỉ mail.",
    url: "Vui lòng điền đúng địa chỉ url.",
    date: "Vui lòng điền đúng định dạng ngày.",
    dateISO: "Please enter a valid date (ISO).",
    number: "Vui lòng điền đúng định dạng số.",
    digits: "Vui lòng điền mỗi kí tự số.",
    creditcard: "Vui lòng chỉ điền số tài khoản.",
    equalTo: "Vui lòng điền giống dữ liệu một lần nữa",
    accept: "Please enter a value with a valid extension.",
    maxlength: jQuery.validator.format("Vui lòng nhập không nhiều hơn {0} kí tự."),
    minlength: jQuery.validator.format("Vui lòng nhập ít nhất {0} kí tự."),
    rangelength: jQuery.validator.format("Vui lòng nhập với độ dài từ {0} tới {1} kí tự."),
    range: jQuery.validator.format("Vui lòng nhập trong phạm vi {0} và {1}."),
    max: jQuery.validator.format("Vui lòng nhập giá trị thấp hơn hoặc bằng {0}."),
    min: jQuery.validator.format("Vui lòng nhập giá trị lớn hơn hoặc bằng {0}.")
});