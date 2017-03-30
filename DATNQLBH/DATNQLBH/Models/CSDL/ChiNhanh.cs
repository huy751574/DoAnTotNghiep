using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DATNQLBH.Models.CSDL
{
    public class ChiNhanh
    {
        [Key]
    
        public string MaCN { get; set; }
        [Required]
        [Display(Name = "Tên")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Địa chỉ")]
        public string DiaChi { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Số điện thoại")]
        public string SDT { get; set; }
        [Display(Name = "Số Fax")]
        public string Fax { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
      
        public string Logo { get; set; }
        [Display(Name = "Ngày thành lập")]
        public System.DateTime Date { get; set; }
        [Display(Name = "Tài khoản ngân hàng")]
        public string SoTaiKhoan { get; set; }
        [DataType(DataType.Url)]
        [AllowHtml]
        public string WebSite { get; set; }
        [AllowHtml]
        [DataType(DataType.Url)]
        public string FaceBook { get; set; }

    
        public virtual ICollection<BlogDoiTac> BlogDoiTacs { get; set; }
    
        public virtual ICollection<ApplicationUser> TaiKhoans { get; set; }
       
        public virtual ICollection<LoaiCap2> LoaiCap2s { get; set; }
        
        public virtual ICollection<LoaiCap3> LoaiCap3s { get; set; }
        
        public virtual ICollection<LoaiCap1> LoaiCap1s { get; set; }
      
        public virtual ICollection<HopDongChiNhanh> HopDongChiNhanhs { get; set; }
        
        public virtual ICollection<LichSuThaoTac> LichSuThaoTacs { get; set; }
   
        public virtual ICollection<NhaCC> NhaCCs { get; set; }
       
        public virtual ICollection<NhapHang> NhapHangs { get; set; }
     
        public virtual ICollection<DonHang> DonHangs { get; set; }
    
        public virtual ICollection<TraHang> TraHangs { get; set; }

        public virtual ICollection<KhoHang> KhoHangs { get; set; }
        public virtual ICollection<KhachHang> KhachHangs { get; set; }
        public virtual ICollection<QuyCach> QuyCachs { get; set; }
    }
}