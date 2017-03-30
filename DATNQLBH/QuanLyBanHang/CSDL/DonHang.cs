using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QuanLyBanHang.CSDL
{
    public class DonHang
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }
       
        [Required]
        [Display(Name ="Họ tên khách hàng")]
        public string FullName { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name ="Số điện thoại khách hàng")]
        public string SDT { get; set; }
        [Required]
        [Display(Name ="Địa chỉ khách hàng")]
        public string Address { get; set; }
        [Display(Name ="Thời gian mua")]
        [DataType(DataType.DateTime)]
        public System.DateTime BuyTime { get; set; }
        [Display(Name ="Trạng thái")]
        public int State { get; set; }
        [ForeignKey("KhachHang")]
        public Nullable<int> CustomerId { get; set; }
        [Display(Name ="Thời gian hoàn thành đơn")]
        [DataType(DataType.DateTime)]
        public System.DateTime Completiontime { get; set; }
      
        
        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }
       
        public virtual KhachHang KhachHang { get; set; }
        
    }
}