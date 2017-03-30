using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DATNQLBH.Models.CSDL
{
    public class NhapHang
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaNH { get; set; }
        [ForeignKey("NhaCC")]
        public int MaNCC { get; set; }
        [Required]
        [Display(Name ="Tên phiếu nhập hàng")]
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
        [ForeignKey("KhoHang")]
        public int WarehouseID { get; set; }

        
        public virtual ICollection<CT_NhapHang> CT_NhapHangs { get; set; }
        public virtual ICollection<CT_TraHang> CT_TraHangs { get; set; }

        public virtual KhoHang KhoHang { get; set; }
        public virtual NhaCC NhaCC { get; set; }
        public virtual ApplicationUser TaiKhoan { get; set; }
        public virtual ChiNhanh ChiNhanh { get; set; }
    }
}