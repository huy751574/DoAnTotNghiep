using DATNQLBH.Manager;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DATNQLBH.Models.CSDL
{
    public class LoaiGianHang
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaLoai { get; set; }
        [Required]
        [Display(Name="Tên Gói")]
        public string Name { get; set; }
        [Required]
        [Display(Name="Giá")]
        [MinValue(0)]
        public int Price { get; set; }
        [Required]
        [Display(Name = "Thời gian sử dụng(ngày)")]
        [MinValue(0)]
        public int TimeUsing { get; set; }
        public virtual ICollection<CT_ChucNang> CT_ChucNang { get; set; }
        
        public virtual ICollection<HopDongChiNhanh> HopDongChiNhanhs { get; set; }
    }
}