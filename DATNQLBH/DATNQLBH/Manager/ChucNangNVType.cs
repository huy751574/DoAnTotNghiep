using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DATNQLBH.Manager
{
    public enum ChucNangNVType
    {
        ThemKho=1,
        UpdateKho=2,
            
        ThemMatHang=3,
          UpdateMatHang=4,
           XoaMatHang=5,
            ChangeStateMatHang=6,
           LapHoaDon=7,
         ChangeStateDon=8,
            ThemNCC=11,
           UpdateNCC=12,
            ThemKhachHang=13,
           UpdateKhachHang=14,
            ThemLoaiCap1=15,
            UpdateLoaiCap1=16,
           ThemLoaiCap2=17,
           UpdateLoaiCap2=18,
           ThemLoaiCap3=19,
          UpdateLoaiCap3=20,
           ThemQuyCach=21,
           UpdateQuyCach=22,
            ReportDoanhThu=23,
         ReportCongNo=24,
          ReportTonKho=25,
           ThemNhapHang=26,
           ThemTraHang=27,
        
        ChangeStateNhapHang = 30,
        ChangeStateTraHang = 31
    }
}