using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanHang.CSDL
{
    public class DangSuDung
    {
        [Key]
        public string UserName { get; set; }
        public string QuyenSuDung { get; set; }
      
        public string TenCN { get; set; }
        public string DiaChi { get; set; }
        public string SDT { get; set; }
        public string Email { get; set; }
    }
}
