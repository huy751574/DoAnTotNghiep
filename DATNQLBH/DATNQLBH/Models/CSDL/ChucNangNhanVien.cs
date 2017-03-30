using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DATNQLBH.Models.CSDL
{
    public class ChucNangNhanVien
    {
        [Key]
       
        public int Id { get; set; }
        [Display(Name="Tên chức năng")]
        public string Name { get; set; }
        public virtual ICollection<ChiTietQuyen> ChiTietQuyens { get; set; }
    }
}