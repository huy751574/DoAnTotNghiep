using DATNQLBH.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DATNQLBH.Manager.CheckChucNangNhanVien
{
    public class CheckCNNV:ActionFilterAttribute
    {
        public int Id { get; set; }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //Log("OnActionExecuting", filterContext.RouteData);
            ShopEntities db;
            KiemTra kiemtra = new KiemTra();
            var user = kiemtra.getUser(HttpContext.Current.User.Identity.Name);
            if (!HttpContext.Current.User.IsInRole("SuperAdmin") && !HttpContext.Current.User.IsInRole("Admin"))
            {
                db = ShopEntities.CreateEntitiesForSpecificDatabaseName(user.MaCN);

                //var userstore = new UserStore<ApplicationUser>(db);
                //var usermanager = new UserManager<ApplicationUser>(userstore);
                var account = kiemtra.getUser(HttpContext.Current.User.Identity.Name);
                var ChucNangNV = db.ChucNangNhanViens.FirstOrDefault(x => x.Id.Equals((int)ChucNangNVType.LapHoaDon));
                switch (Id)
                {
                    case (int)ChucNangNVType.ChangeStateMatHang:
                        ChucNangNV= db.ChucNangNhanViens.FirstOrDefault(x => x.Id.Equals((int)ChucNangNVType.ChangeStateMatHang));
                        break;
                    case (int)ChucNangNVType.ChangeStateDon:
                        ChucNangNV = db.ChucNangNhanViens.FirstOrDefault(x => x.Id.Equals((int)ChucNangNVType.ChangeStateDon));
                        break;
                    case (int)ChucNangNVType.ChangeStateNhapHang:
                        ChucNangNV = db.ChucNangNhanViens.FirstOrDefault(x => x.Id.Equals((int)ChucNangNVType.ChangeStateNhapHang));
                        break;
                    case (int)ChucNangNVType.ChangeStateTraHang:
                        ChucNangNV = db.ChucNangNhanViens.FirstOrDefault(x => x.Id.Equals((int)ChucNangNVType.ChangeStateTraHang));
                        break;
                   
                    case (int)ChucNangNVType.LapHoaDon:
                        ChucNangNV = db.ChucNangNhanViens.FirstOrDefault(x => x.Id.Equals((int)ChucNangNVType.LapHoaDon));
                        break;
                    case (int)ChucNangNVType.ReportCongNo:
                        ChucNangNV = db.ChucNangNhanViens.FirstOrDefault(x => x.Id.Equals((int)ChucNangNVType.ReportCongNo));
                        break;
                    case (int)ChucNangNVType.ReportDoanhThu:
                        ChucNangNV = db.ChucNangNhanViens.FirstOrDefault(x => x.Id.Equals((int)ChucNangNVType.ReportDoanhThu));
                        break;
                    case (int)ChucNangNVType.ReportTonKho:
                        ChucNangNV = db.ChucNangNhanViens.FirstOrDefault(x => x.Id.Equals((int)ChucNangNVType.ReportTonKho));
                        break;
                    case (int)ChucNangNVType.ThemKhachHang:
                        ChucNangNV = db.ChucNangNhanViens.FirstOrDefault(x => x.Id.Equals((int)ChucNangNVType.ThemKhachHang));
                        break;
                    case (int)ChucNangNVType.ThemKho:
                        ChucNangNV = db.ChucNangNhanViens.FirstOrDefault(x => x.Id.Equals((int)ChucNangNVType.ThemKho));
                        break;
                    case (int)ChucNangNVType.ThemLoaiCap1:
                        ChucNangNV = db.ChucNangNhanViens.FirstOrDefault(x => x.Id.Equals((int)ChucNangNVType.ThemLoaiCap1));
                        break;
                    case (int)ChucNangNVType.ThemLoaiCap2:
                        ChucNangNV = db.ChucNangNhanViens.FirstOrDefault(x => x.Id.Equals((int)ChucNangNVType.ThemLoaiCap2));
                        break;
                    case (int)ChucNangNVType.ThemLoaiCap3:
                        ChucNangNV = db.ChucNangNhanViens.FirstOrDefault(x => x.Id.Equals((int)ChucNangNVType.ThemLoaiCap3));
                        break;
                    case (int)ChucNangNVType.ThemMatHang:
                        ChucNangNV = db.ChucNangNhanViens.FirstOrDefault(x => x.Id.Equals((int)ChucNangNVType.ThemMatHang));
                        break;
                    case (int)ChucNangNVType.ThemNCC:
                        ChucNangNV = db.ChucNangNhanViens.FirstOrDefault(x => x.Id.Equals((int)ChucNangNVType.ThemNCC));
                        break;
                    case (int)ChucNangNVType.ThemNhapHang:
                        ChucNangNV = db.ChucNangNhanViens.FirstOrDefault(x => x.Id.Equals((int)ChucNangNVType.ThemNhapHang));
                        break;
                    case (int)ChucNangNVType.ThemQuyCach:
                        ChucNangNV = db.ChucNangNhanViens.FirstOrDefault(x => x.Id.Equals((int)ChucNangNVType.ThemQuyCach));
                        break;
                    case (int)ChucNangNVType.ThemTraHang:
                        ChucNangNV = db.ChucNangNhanViens.FirstOrDefault(x => x.Id.Equals((int)ChucNangNVType.ThemTraHang));
                        break;
                    case (int)ChucNangNVType.UpdateKhachHang:
                        ChucNangNV = db.ChucNangNhanViens.FirstOrDefault(x => x.Id.Equals((int)ChucNangNVType.UpdateKhachHang));
                        break;
                    case (int)ChucNangNVType.UpdateKho:
                        ChucNangNV = db.ChucNangNhanViens.FirstOrDefault(x => x.Id.Equals((int)ChucNangNVType.UpdateKho));
                        break;
                    case (int)ChucNangNVType.UpdateLoaiCap1:
                        ChucNangNV = db.ChucNangNhanViens.FirstOrDefault(x => x.Id.Equals((int)ChucNangNVType.UpdateLoaiCap1));
                        break;
                    case (int)ChucNangNVType.UpdateLoaiCap2:
                        ChucNangNV = db.ChucNangNhanViens.FirstOrDefault(x => x.Id.Equals((int)ChucNangNVType.UpdateLoaiCap2));
                        break;
                    case (int)ChucNangNVType.UpdateLoaiCap3:
                        ChucNangNV = db.ChucNangNhanViens.FirstOrDefault(x => x.Id.Equals((int)ChucNangNVType.UpdateLoaiCap3));
                        break;
                    case (int)ChucNangNVType.UpdateMatHang:
                        ChucNangNV = db.ChucNangNhanViens.FirstOrDefault(x => x.Id.Equals((int)ChucNangNVType.UpdateMatHang));
                        break;
                    case (int)ChucNangNVType.UpdateNCC:
                        ChucNangNV = db.ChucNangNhanViens.FirstOrDefault(x => x.Id.Equals((int)ChucNangNVType.UpdateNCC));
                        break;                  
                    case (int)ChucNangNVType.UpdateQuyCach:
                        ChucNangNV = db.ChucNangNhanViens.FirstOrDefault(x => x.Id.Equals((int)ChucNangNVType.UpdateQuyCach));
                        break;
                   
                    case (int)ChucNangNVType.XoaMatHang:
                        ChucNangNV = db.ChucNangNhanViens.FirstOrDefault(x => x.Id.Equals((int)ChucNangNVType.XoaMatHang));
                        break;
                    
                

                }
               
                var CTQuyen = db.ChiTietQuyens.Where(x => x.ChucNangId == ChucNangNV.Id);
                var roles = db.Quyens.Where(x=>x.Users.Contains(account));
                var flag = false;
                foreach (var item in roles)
                {
                    if (flag == true) break;
                    foreach (var ct in CTQuyen)
                    {
                        if (item.ChiTietQuyens.Contains(ct))
                        {
                            flag = true;
                            break;
                        }
                    }
                }
                if (!flag)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
                                   {
                                       { "action", "Index" },
                                       { "controller", "Home" }
                                   });
                    ThongBaoMvc thongbao = new ThongBaoMvc { CssClassName = "warning", Message = "Xin lỗi~, tài khoản của bạn không có quyền sử dụng chức năng này" };

                    filterContext.Controller.TempData.Add("ResultAction", thongbao);
                }
            }
        }

    }
}