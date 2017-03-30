using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DATNQLBH.Models.CSDL
{
    public class LoaiCap3
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LoaiCap3Id { get; set; }
        [Required]
        [Display(Name ="Tên loại cấp 3")]
        public string Name { get; set; }
        [ForeignKey("LoaiCap2")]
        public int LoaiCap2Id { get; set; }
        [Display(Name="Thuộc tính cấu hình của loại")]
        public string PropertyNames { get; set; }
     
        [ForeignKey("ChiNhanh")]
        public string MaCN { get; set; }

        public virtual LoaiCap2 LoaiCap2 { get; set; }
   
        public virtual ICollection<MatHang> MatHangs { get; set; }
        public virtual ChiNhanh ChiNhanh { get; set; }
    }
}