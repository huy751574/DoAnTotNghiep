using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DATNQLBH.Models.CSDL
{
    public class LoaiCap2
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LoaiCap2Id { get; set; }
        [Required]
        [Display(Name ="Tên loại cấp 2")]
        public string Name { get; set; }
        [ForeignKey("LoaiCap1")]
        public int LoaiCap1Id { get; set; }
        [ForeignKey("ChiNhanh")]
        public string MaCN { get; set; }

        public virtual LoaiCap1 LoaiCap1 { get; set; }
        
        public virtual ICollection<LoaiCap3> LoaiCap3 { get; set; }
        public virtual ChiNhanh ChiNhanh { get; set; }
    }
}