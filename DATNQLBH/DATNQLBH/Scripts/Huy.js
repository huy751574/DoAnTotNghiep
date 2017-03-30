//CHuyển sang định dạng só x.xxx.xxx vd: 1999=>1.999
function formatCurrency(num) {
    var h = 0;

    if (num.length % 3 == 1) {
        h = 1;
    }
    if (num.length % 3 == 2) {
        h = 2;
    }
    var ketiep = num;
    var sotrave = "";
    if (num.length <= 3) { return ketiep;}
    if (h == 1 || h == 2) {
        if (num.length / 3 > 1) {
            sotrave = num.substring(0, h) + ".";
            ketiep = ketiep.substring(h);
        }
        h = 0;
    }
    else {
        if (num.length / 3 > 1) {
            sotrave = num.substring(0, 3) + ".";
            ketiep = ketiep.substring(3);
        }
    }
    var k = (num.length / 3) - 1;
    for (var i = 0; i < k; i++) {
       
        if (i >= (num.length / 3) - 2) {
            sotrave += ketiep;
            break;
           
        }
        sotrave += ketiep.substring(0, 3) + ".";
        ketiep = ketiep.substring(3);

      
    }
 
    return sotrave;
}

//Lấy số nếu đang ở định dạng x.xxx.xxx vd:1.999.999=>1999999
function layso(tien) {
    return tien.split(" ")[0].replace(/[.]/g, "");
}
function changeLoai2() {
    var bien = document.getElementById("LoaiCap1").value;
    if (bien == null || bien == "") {
        bien = 0;
    }
    $("#LoaiCap2").remove();
    $("#LoaiCap3").remove();
    $.ajax({
        type: 'GET',
        url: '/Item/updateLoaiCap2',
        data: { id: bien },
        success: function (data) {
            $("#loc").append(data.text);
        }
    });


};

function chuyenQuaTienGon(tien)
{
    var k;
    if (tien >= 1000000000)
        k = (tien / 1000000000).toFixed(2).replace("/[.]/g",",") + ' tỉ';
    else if (tien >= 1000000)
        k = (tien / 1000000).toFixed(2).replace("/[.]/g", ",") + ' triệu';
    else if (tien >= 1000)
        k = (tien / 1000).toFixed(0).replace("/[.]/g", ",") + ' nghìn';
    else k = tien;
  return k;
}
function changeLoai3() {
    var bien = $('select#LoaiCap2').val();
    if (bien == null || bien == "") {
        bien = 0;
    }
    $("#LoaiCap3").remove();
    $.ajax({
        type: 'GET',
        url: '/Item/updateLoaiCap3',
        data: { id: bien },
        success: function (data) {
            $("#loc").append(data.text);
        }
    });
};

function locdau(str) {


    //str = str.toLowerCase();
    str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, "a");
    str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, "e");
    str = str.replace(/ì|í|ị|ỉ|ĩ/g, "i");
    str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, "o");
    str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, "u");
    str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, "y");
    str = str.replace(/đ/g, "d");
    // str = str.replace(/!|\$|%|\^|\*|\(|\)|\+|\=|\<|\>|\?|\/|,|\.|\:|\'| |\"|\&|\#|\[|\]|~/g, "-");
    // str = str.replace(/-+-/g, "-"); //thay thế 2- thành 1-
    // str = str.replace(/^\-+|\-+$/g, "");//cắt bỏ ký tự - ở đầu và cuối chuỗi
    return str;
};
function chuyenngay(date) {
    var day = new Date(parseInt(date.substr(6)));
    var dd = day.getDate();
    var mm = day.getMonth() + 1; //January is 0!
   
    var yyyy = day.getFullYear();
    if (dd < 10) {
        dd = '0' + dd
    }
    if (mm < 10) {
        mm = '0' + mm
    }
    var day = yyyy + '-' + mm + '-' + dd;
    return day;
}
//Thêm vào sau referenceNode
function insertAfter(newNode, referenceNode) {
    $(referenceNode.parentNode).append(newNode);
}