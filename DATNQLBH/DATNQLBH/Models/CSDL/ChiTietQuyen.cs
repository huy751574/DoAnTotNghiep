using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DATNQLBH.Models.CSDL
{
    public class ChiTietQuyen
    {
        [Key, ForeignKey("Role"), Column(Order = 1)]
        public string RoleId { get; set; }
        [Key, ForeignKey("ChucNangNhanVien"), Column(Order = 2)]
        public int ChucNangId { get; set; }
        public bool IsEnable { get; set; }
        public virtual ApplicationRole Role { get; set; }
        public virtual ChucNangNhanVien ChucNangNhanVien { get; set; }
    }
}