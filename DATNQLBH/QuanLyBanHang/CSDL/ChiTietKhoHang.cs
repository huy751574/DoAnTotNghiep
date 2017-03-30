
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QuanLyBanHang.CSDL
{
    public class ChiTietKhoHang
    {
      
        [Key,ForeignKey("KhoHang"), Column(Order = 1)]
        public int WarehouseID { get; set; }
        [Key,ForeignKey("MatHang"), Column(Order = 2)]
        public long ItemId { get; set; }
       
        [Display(Name ="Số lượng")]
        public int Quantities { get; set; }

        public virtual MatHang MatHang { get; set; }
        public virtual KhoHang KhoHang { get; set; }
    }
}