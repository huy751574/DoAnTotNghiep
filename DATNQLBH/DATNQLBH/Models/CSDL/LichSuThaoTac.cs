using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DATNQLBH.Models.CSDL
{
    public class LichSuThaoTac
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("TaiKhoan")]
        public string UserId { get; set; }
        public int Function { get; set; }
        public string Smg { get; set; }
        [ForeignKey("ChiNhanh")]
        public string MaCN { get; set; }
        public System.DateTime LogTime { get; set; }

        public virtual ApplicationUser TaiKhoan { get; set; }
        public virtual ChiNhanh ChiNhanh { get; set; }
    }
}