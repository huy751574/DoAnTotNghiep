using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DATNQLBH.Models.CSDL
{
    public class BlogDoiTac
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BlogID { get; set; }
        [Required]
        [Display(Name="Tiêu đề")]
        public string Title { get; set; }
        [Required]
        [AllowHtml]
       
        public string Description { get; set; }
        [Display(Name ="Ghi Chú")]
        public string Note { get; set; }
        [ForeignKey("ChiNhanh")]
        public string MaCN { get; set; }
        [Display(Name ="Hình ảnh")]
        public string Image { get; set; }
        public virtual ChiNhanh ChiNhanh { get; set; }
    }
}