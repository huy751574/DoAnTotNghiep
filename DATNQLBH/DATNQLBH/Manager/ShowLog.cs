using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DATNQLBH.Manager
{
    public class ShowLog
    {
        public string GetTimeLine(DateTime time)
        {
            var total = DateTime.Now.Subtract(time);
            if (total.TotalSeconds < 60)
            {
                return (int)total.TotalSeconds + " giây trước";
            }
            else if (total.TotalMinutes < 60)
            {
                return (int)total.TotalMinutes + " phút trước";
            }
            else if (total.TotalHours < 24)
            {
                return (int)total.TotalHours + " giờ trước";
            }
            else
            {
                return (int)total.TotalDays + " ngày trước";
            }
        }
        public string GetNameFunction(int type)
        {
            switch (type)
            {
                case (int)FunctionType.AddInvoice:
                    return "Thêm Hóa Đơn";
                case (int)FunctionType.EditInvoice:
                    return "Sửa Hóa Đơn";
                case (int)FunctionType.CompleteInvoice:
                    return "Hoàn Thành Hóa Đơn";
                case (int)FunctionType.CancelInvoice:
                    return "Hủy Hóa Đơn";
                case (int)FunctionType.AddItem:
                    return "Thêm sản phẩm";
                case (int)FunctionType.AddDetailItem:
                    return "Cập nhập chi tiết sản phẩm";
                case (int)FunctionType.UpdateDescription:
                    return "Cập nhập mô tả sản phẩm"; 
                case (int)FunctionType.AddLoaiCap1:
                    return "Thêm Loại cấp 1";
                case (int)FunctionType.UpdateLoaiCap1:
                    return "Update Loại cấp 1";
                case (int)FunctionType.XoaLoaiCap1:
                    return "Xoá Loại cấp 1";
                case (int)FunctionType.AddLoaiCap2:
                    return "Thêm Loại cấp 2";
                case (int)FunctionType.UpdateLoaiCap2:
                    return "Update Loại cấp 2";
                case (int)FunctionType.XoaLoaiCap2:
                    return "Xoá Loại cấp 2";
                case (int)FunctionType.AddLoaiCap3:
                    return "Thêm Loại cấp 3";
                case (int)FunctionType.UpdateLoaiCap3:
                    return "Update Loại cấp 3";
                case (int)FunctionType.UpdateChitietSanPhamLoaiCap3:
                    return "Update chi tiết sản phẩm loại cấp 3";
                case (int)FunctionType.XoaLoaiCap3:
                    return "Xoá Loại cấp 3";
                case (int)FunctionType.XoaSanPham:
                    return "Xoá Sản phẩm";
                case (int)FunctionType.UpdateTinhTrangSanPham:
                    return "Update Tình Trạng sản phẩm";
                case (int)FunctionType.UpdateSanPham:
                    return "Update Thông tin sản phẩm";
                case (int)FunctionType.AddTaiKhoan:
                    return "Thêm nhân viên";
                case (int)FunctionType.AddNhapHang:
                    return "Thêm đơn nhập hàng";
                case (int)FunctionType.EditNhapHang:
                    return "Update đơn nhập hàng";
                case (int)FunctionType.CompleteNhapHang:
                    return "Hoàn thành đơn nhập hàng";
                case (int)FunctionType.DeleteNhapHang:
                    return "Xóa đơn nhập hàng";
                case (int)FunctionType.AddTraHang:
                    return "Thêm đơn trả hàng";
                case (int)FunctionType.CompleteTraHang:
                    return "Hoàn thành đơn trả hàng";
                case (int)FunctionType.DeleteTraHang:
                    return "Xóa đơn trả hàng";
                case (int)FunctionType.ProgressingInvoice:
                    return "Đang giao sản phẩm";
                case (int)FunctionType.AddKhachHang:
                    return "Thêm khách hàng";
                case (int)FunctionType.EditKhachHang:
                    return "Thay đổi thông tin khách";
                case (int)FunctionType.AddNhanVien:
                    return "Thêm nhân viên";
                case (int)FunctionType.DisableNhanVien:
                    return "Vô hiệu tài khoản nhân viên";
                case (int)FunctionType.EnableNhanVien:
                    return "Kích hoạt tài khoản nhân viên"; 
                default:
                    return "";
            }
        }
        public string UserName { get; set; }
        public string Function { get{ return GetNameFunction(type); } }
        public string Smg { get; set; }
        public string TimeLine { get { return GetTimeLine(time); } }
        private int type;
        public int Type { set { type = value; } }
        private DateTime time;
        public DateTime Time { set{ time = value; }}
    }
}