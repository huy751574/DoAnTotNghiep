using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DATNQLBH.Models.CSDL
{
    public class TimeSheet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("TaiKhoan")]
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public bool allDay { get; set; }
        
        public virtual ApplicationUser TaiKhoan { get; set; }
    }
}