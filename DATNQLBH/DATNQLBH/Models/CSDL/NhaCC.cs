using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DATNQLBH.Models.CSDL
{
    public class NhaCC
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaNCC { get; set; }
        [Required]
        [Display(Name ="Tên nhà cung cấp")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name ="Số điện thoại")]
        public string SDT { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string Fax { get; set; }
        [ForeignKey("ChiNhanh")]
        public string MaCN { get; set; }
        
        [Display(Name ="Tài khoản ngân hàng")]
        public string TaiKhoan { get; set; }
      

       
        public virtual ICollection<NhapHang> NhapHangs { get; set; }
      
       
        public virtual ChiNhanh ChiNhanh { get; set; }
    }
}