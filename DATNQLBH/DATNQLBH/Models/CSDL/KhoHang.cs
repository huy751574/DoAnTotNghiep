using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DATNQLBH.Models.CSDL
{
    public class KhoHang
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WarehouseID { get; set; }
        [Required]
        [Display(Name ="Tên kho hàng")]
        public string Name { get; set; }
        [Required]
        [Display(Name ="Địa chỉ")]
        public string DiaChi { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name ="Số điện thoại")]
        public string SDT { get; set; }
        [ForeignKey("ChiNhanh")]
        public string MaCN { get; set; }

 
        public virtual ICollection<ChiTietKhoHang> ChiTietKhoHangs { get; set; }
   
        public virtual ICollection<NhapHang> NhapHangs { get; set; }
        public virtual ChiNhanh ChiNhanh { get; set; }
    }
}