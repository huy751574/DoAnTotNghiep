
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QuanLyBanHang.CSDL
{
    public class KhachHang
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [Display(Name ="Họ tên khách hàng")]
        public string FullName { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name ="Số điện thoại khách hàng")]
        public string SDT { get; set; }
        [Required]
        [Display(Name ="Địa chỉ")]
        public string Address { get; set; }
        [Required]
        [Display(Name ="Số chứng minh thư")]
        public string CMND { get; set; }
        [Display(Name ="Đã mua")]
        
        public long ExpVip { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Display(Name ="Cấp độ thành viên")]
        public int Vip { get; set;  }
      
     
    }
}