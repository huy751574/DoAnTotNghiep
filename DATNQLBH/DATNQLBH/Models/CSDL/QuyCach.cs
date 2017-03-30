using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DATNQLBH.Models.CSDL
{
    public class QuyCach
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuyCachId { get; set; }
        [Required]
        [Display(Name ="Tên quy cách")]
        public string Name { get; set; }
        [ForeignKey("ChiNhanh")]
        public string MaCN { get; set; }
        public virtual ChiNhanh ChiNhanh { get; set; }
    
        public virtual ICollection<MatHang> MatHangs { get; set; }
    }
}