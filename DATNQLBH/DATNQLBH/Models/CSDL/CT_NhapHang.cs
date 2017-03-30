using DATNQLBH.Manager;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DATNQLBH.Models.CSDL
{
    public class CT_NhapHang
    {
        [Key, ForeignKey("NhapHang"), Column(Order = 1)]
        public int MaNH { get; set; }
        [Display(Name ="Số lượng")]
        public int Quantity { get; set; }
        [Display(Name ="Giá")]
        public int Price { get; set; }
        [Key, ForeignKey("MatHang"), Column(Order = 2)]
        public long ItemId { get; set; }
        [MinValue(0)]
        public float VAT { get; set; }

        public virtual MatHang MatHang { get; set; }
        public virtual NhapHang NhapHang { get; set; }
    }
}