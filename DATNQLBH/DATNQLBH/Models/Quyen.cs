using DATNQLBH.Models.CSDL;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DATNQLBH.Models
{
    public class Quyen
    {
        [Display(Name ="Chức năng")]
        [Key]
        public string Name { get; set; }
        [Display(Name ="Kích hoạt")]
        public bool IsActive { get; set; }
        public List<ChiTietQuyen> ChiTietQuyens { get; set; }

    }
   
}