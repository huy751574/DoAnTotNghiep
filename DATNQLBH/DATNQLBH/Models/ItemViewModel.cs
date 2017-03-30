using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DATNQLBH.Manager
{
    public class ItemViewModel
    {
        [Display(Name = "Mã Hàng")]
        public long ItemId { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        [StringLength(250, ErrorMessage = "độ dài không được vượt quá 250 ký tự")]
        [Display(Name = "Tên Hàng")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        [StringLength(250, ErrorMessage = "độ dài không được vượt quá 250 ký tự")]
        [Display(Name = "Xuất Xứ")]
        public string AuthorName { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        [StringLength(250, ErrorMessage = "độ dài không được vượt quá 250 ký tự")]
        [Display(Name = "Thông tin chi tiết")]
        public string Propertynames { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        [Display(Name = "Giá bán")]
        public int Price { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        [Display(Name = "% Giảm Giá")]
        public int Reduction { get; set; }
        [Display(Name = "Số lượng mua")]
        public int BuyQuantity { get; set; }
        //  [Required(ErrorMessage = "Không được để trống")]
        [Display(Name = "Ảnh")]

        public string CoverImage { get; set; }
        [Display(Name = "Loại")]
        public int LoaiCap3Id { get; set; }
        [Display(Name = "Quy Cách")]
        public int QuyCachId { get; set; }
        public string MaCN { get; set; }
        public string Description { get; set; }
        public int VAT { get; set; }
       
    }
    public class ThongkeKho
    {
        public long ItemId { get; set; }
        public string Name { get; set; }
        public string Images { get; set; }
        public string QuyCach { get; set; }
        public int Total { get; set; }
        public int totalBuy { get; set; }
        public int TotalChange { get; set; }
        public int Totalend
        {
            get
            {
                return (Total - totalBuy);
            }
        }
       
    }
    public static class bientam
    {
        public static long ItemId { get; set; }
    }
}