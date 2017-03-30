using DATNQLBH.Manager;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DATNQLBH.Models.CSDL
{
    public class MatHang
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ItemId { get; set; }
        [Required]
        [Display(Name="Tên sản phẩm")]
        public string Name { get; set; }
        [Required]
        [Display(Name="Xuất xứ")]
        public string AuthorName { get; set; }
        public string Propertynames { get; set; }
        [Required]
        [Display(Name ="Giá")]
        [MinValue(0)]
        public int Price { get; set; }
        [Display(Name = "% Giảm giá")]
        [MinValue(0)]
        public float Reduction { get; set; }
        [Display(Name = "% VAT")]
        [MinValue(0)]
        public float VAT { get; set; }
        public string Directory { get; set; }
        [ForeignKey("LoaiCap3")]
        public int LoaiCap3Id { get; set; }
        [ForeignKey("QuyCach")]
        public int QuyCachId { get; set; }
        [Display(Name ="Ảnh đại diện")]
        public string Image { get; set; }
        [ForeignKey("ChiNhanh")]
        public string MaCN { get; set; }
        [AllowHtml]
        [Display(Name ="Mô tả")]
        public string Description { get; set; }
        public bool IsUse { get; set; }
        [Display(Name ="Số series")]
        public string Serial { get; set; }

   
        public virtual ICollection<CT_NhapHang> CT_NhapHangs { get; set; }
       
        public virtual ICollection<CT_TraHang> CT_TraHangs { get; set; }
        
        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }
        
        public virtual ICollection<ChiTietKhoHang> ChiTietKhoHangs { get; set; }
        
        public virtual ChiNhanh ChiNhanh { get; set; }
        public virtual LoaiCap3 LoaiCap3 { get; set; }
        public virtual QuyCach QuyCach { get; set; }
    }
}