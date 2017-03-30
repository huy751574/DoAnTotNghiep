using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DATNQLBH.Models.CSDL
{
    public class LoaiCap1
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LoaiCap1Id { get; set; }
        [Required]
        [Display(Name ="Tên loại cấp 1")]
        public string Name { get; set; }
        [ForeignKey("ChiNhanh")]
        public string MaCN { get; set; }

        
        public virtual ICollection<LoaiCap2> LoaiCap2 { get; set; }
        public virtual ChiNhanh ChiNhanh { get; set; }
    }
}