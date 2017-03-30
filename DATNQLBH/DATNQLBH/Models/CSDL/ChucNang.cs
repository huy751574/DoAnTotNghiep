using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DATNQLBH.Models.CSDL
{
    public class ChucNang
    {
      
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Display(Name ="Tên chức năng")]
        public string Name { get; set; }
        [Display(Name ="Kích hoạt")]
        public bool IsActive { get; set; }

        public virtual ICollection<CT_ChucNang> CT_ChucNangs { get; set; }
    }
}