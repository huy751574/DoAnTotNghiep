using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DATNQLBH.Models.CSDL
{
    public class PhanHoi
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("TaiKhoan")]
        public string UserID { get; set; }
        [Display(Name ="Nội dung phản hồi")]
        public string Comments { get; set; }
        [Display(Name ="Ngày phản hồi")]
        public System.DateTime Datecomments { get; set; }
    
        [ForeignKey("ChiNhanh")]
 
        public virtual ApplicationUser TaiKhoan { get; set; }
     
    }
}