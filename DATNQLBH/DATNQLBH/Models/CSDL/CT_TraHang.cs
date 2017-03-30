using DATNQLBH.Manager;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DATNQLBH.Models.CSDL
{
    public class CT_TraHang
    {
      
        [Key,ForeignKey("TraHang"), Column(Order = 1)]
        public int MaTH { get; set; }
        [Key, ForeignKey("MatHang"), Column(Order = 2)]
        public long ItemId { get; set; }
        [Key, ForeignKey("NhapHang"), Column(Order = 3)]
        public int MaNH { get; set; }
        [Display(Name = "Số lượng")]
        [MinValue(0)]
        public int Quantity { get; set; }

        public virtual NhapHang NhapHang { get; set; }
        public virtual MatHang MatHang { get; set; }
        public virtual TraHang TraHang { get; set; }
    }
}