using DATNQLBH.Models.CSDL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DATNQLBH.Manager
{
    public class BuyItem
    {
        public MatHang productOrder { get; set; }
        [Display(Name = "Số lượng")]
        public int Quantity { get; set; }
    }
    public class AddChange_Delivery
    {
        public ChiTietDonHang Order_Detail { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
    }
    public class LapHoaDon : ChiTietHoaDon
    {
        public int MaHoaDon { get; set; }
        public string Name { get; set; }
        public string DiaChi { get; set; }
        public string Fax { get; set; }
        public string SDT { get; set; }
        public string NameUser { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string TotalName { get; set; }
        public string Date { get; set; }
    }
    public class ChiTietHoaDon
    {
        public string NameItem { get; set; }
        public string QuyCach { get; set; }
        public int Quantitys { get; set; }
        public int Price { get; set; }
        public int Vat { get; set; }
        public long Total
        {
            get
            {
                return (long)Quantitys * (long)Price;
            }
        }
    }
}