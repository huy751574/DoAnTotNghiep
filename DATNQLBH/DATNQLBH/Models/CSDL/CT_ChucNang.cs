using DATNQLBH.Manager;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DATNQLBH.Models.CSDL
{
    public class CT_ChucNang
    {
        [Key, ForeignKey("ChucNang"), Column(Order =1)]
        public int ChucNangId { get; set; }
        [Key, ForeignKey("LoaiGianHang"), Column(Order = 2)]
        public int MaLoaiGianHang { get; set; }
        [Display(Name ="Kích hoạt")]
        public bool IsActive { get; set; }
        [Display(Name ="Số lượng")]
        [MinValue(0)]
        public Nullable<int> Quantity { get; set; }

        public virtual ChucNang ChucNang { get; set; }
        public virtual LoaiGianHang LoaiGianHang { get; set; }
    }
}