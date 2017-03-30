using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DATNQLBH.Models
{
    public class ThemGianHang
    {
        [Key]
        [Display(Name ="Tên tài khoản")]
        public string UserName{get;set;}
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }
        [Display(Name = "Họ tên")]
        public string FullName { get; set; }
        [Display(Name = "Địa chỉ")]
        public string DiaChi { get; set; }
        [Display(Name = "Số điện thoại")]
        public string SDT { get; set; }

        public string Email { get; set; }
      
        [Display(Name = "Số chứng minh thứ")]
        public string CMND { get; set; }
    }
}