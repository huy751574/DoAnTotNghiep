using DATNQLBH.Manager;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DATNQLBH.Models.CSDL
{
    public class ChiTietDonHang
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderdetailId { get; set; }
        [ForeignKey("MatHang")]
        public long ItemId { get; set; }
        public string NameItem { get; set; }
        [ForeignKey("DonHang")]
        public int OrderId { get; set; }
        [Display(Name = "Số lượng")]
        [MinValue(0)]
        public int Quantities { get; set; }
        [MinValue(0)]
        public int Price { get; set; }
     
      
        public virtual MatHang MatHang { get; set; }
        public virtual DonHang DonHang { get; set; }
    }
}