using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DATNQLBH.Models.CSDL
{
    public class HopDongChiNhanh
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("ChiNhanh")]
        public string MaCN { get; set; }
        [ForeignKey("LoaiGianHang")]
        public int MaLoaiGianHang { get; set; }
        [Display(Name ="Ngày bắt đầu")]
        [DataType(DataType.DateTime)]
        public System.DateTime BeginDate { get; set; }
        [Display(Name ="Ngày kết thúc")]
        [DataType(DataType.DateTime)]   
        public System.DateTime EndDate { get; set; }
        public bool ChapNhanSuDung { get; set; }
        public string KiemTra { get; set; }
        public virtual LoaiGianHang LoaiGianHang { get; set; }
        public virtual ChiNhanh ChiNhanh { get; set; }
    }
}