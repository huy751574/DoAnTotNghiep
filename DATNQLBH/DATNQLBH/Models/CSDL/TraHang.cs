using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DATNQLBH.Models.CSDL
{
    public class TraHang
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaTH { get; set; }
       
        [Required]
        [Display(Name ="Tên phiếu trả hàng")]
        public string Name { get; set; }
        [ForeignKey("TaiKhoan")]
        public string UserId { get; set; }
        [Display(Name ="Ngày lập phiếu")]
        public System.DateTime Date { get; set; }
        [Display(Name ="Trạng thái")]
        public int State { get; set; }
        [Display(Name ="Ngày kết thúc")]
        public System.DateTime DateEnd { get; set; }
        [ForeignKey("ChiNhanh")]
        public string MaCN { get; set; }

 
        public virtual ICollection<CT_TraHang> CT_TraHangs { get; set; }
       
        public virtual ApplicationUser TaiKhoan { get; set; }
        public virtual ChiNhanh ChiNhanh { get; set; }
    }
}