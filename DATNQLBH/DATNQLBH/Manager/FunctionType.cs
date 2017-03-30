using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DATNQLBH.Manager
{
    public enum FunctionType
    {
        AddInvoice = 0,
        EditInvoice = 1,
        CompleteInvoice = 2,
        CancelInvoice = 3,
        AddItem=4,
        AddDetailItem=5,
        UpdateDescription=6,
        AddLoaiCap1=7,
        UpdateLoaiCap1=8,
        AddLoaiCap2=9,
        UpdateLoaiCap2=10,
        AddLoaiCap3=11,
        UpdateLoaiCap3=12,
        UpdateChitietSanPhamLoaiCap3=13,
        XoaLoaiCap1=14,
        XoaLoaiCap2=15,
        XoaLoaiCap3=16,
        XoaSanPham=17,
        UpdateTinhTrangSanPham=18,
        UpdateSanPham=19,
        AddTaiKhoan=20,
       AddNhapHang=21,
       CompleteNhapHang=22,
       EditNhapHang=24,
       DeleteNhapHang=25,
        AddTraHang = 26,
        CompleteTraHang =27,
        DeleteTraHang =28,
        ProgressingInvoice=29,
        AddKhachHang=30,
        EditKhachHang=31,
        AddNhanVien=32,
        DisableNhanVien=33,
        EnableNhanVien=34

    }
}