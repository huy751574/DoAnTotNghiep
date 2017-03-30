using DATNQLBH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Web.Routing;

namespace DATNQLBH.Manager
{
    public class LogActionFilter:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //Log("OnActionExecuting", filterContext.RouteData);
            ShopEntities db;
            KiemTra kiemtra = new KiemTra();
            var user = kiemtra.getUser(HttpContext.Current.User.Identity.Name);
            db = new ShopEntities();
          
            
            if (user.Active == true)
            {
                var gianhang = db.HopDongChiNhanhs.OrderByDescending(x => x.EndDate).FirstOrDefault(a => a.MaCN.Equals(user.MaCN));
                if (gianhang != null)
                {
                    if (gianhang.EndDate.CompareTo(DateTime.Now) < 0)
                    {
                        user.Active = false;
                        db.SaveChanges();                      
                    }
                 
                }
            }
           
           
            if (user.Active == false)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
                                   {
                                       { "action", "Index" },
                                       { "controller", "Home" }
                                   });
                ThongBaoMvc thongbao = new ThongBaoMvc { CssClassName = "warning", Message = "Xin lỗi~, tài khoản của bạn đã hết hạn, vui lòng mua gói mới" };
               
                filterContext.Controller.TempData.Add("ResultAction", thongbao);

            }
            
            var taikhoan = db.TaiKhoans.FirstOrDefault(x => x.UserName.Equals(user.UserName));
            var claim = taikhoan.Claims.FirstOrDefault(x => x.ClaimType.Equals("MyApp:ThemeUrl"));
            if (claim != null && !filterContext.Controller.TempData.ContainsKey("CustomTheme"))
            {
                filterContext.Controller.TempData.Add("CustomTheme", claim.ClaimValue);

            }
        }

        //public override void OnActionExecuted(ActionExecutedContext filterContext)
        //{
        //    Log("OnActionExecuted", filterContext.RouteData);
        //}

        //public override void OnResultExecuting(ResultExecutingContext filterContext)
        //{
        //    Log("OnResultExecuting", filterContext.RouteData);
        //}

        //public override void OnResultExecuted(ResultExecutedContext filterContext)
        //{
          
           
        //}


        //private void Log(string methodName, RouteData routeData)
        //{
        //    var controllerName = routeData.Values["controller"];
        //    var actionName = routeData.Values["action"];
        //    var message = String.Format("{0} controller:{1} action:{2}", methodName, controllerName, actionName);
        //    Debug.WriteLine(message, "Action Filter Log");
        //}

        //private void KiemTraQuanLyKhachHang(string actionName, ActionExecutingContext filterContext)
        //{
            
        //        if (actionName.Equals("KhachHang") || actionName.Equals("ListKhachHang") || actionName.Equals("OrderOfCustomer") || actionName.Equals("ThemKhachHang") || actionName.Equals("EditKhach"))
        //        {
        //            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
        //                           {
        //                               { "action", "Index" },
        //                               { "controller", "Home" }
        //                           });
        //            ThongBaoMvc thongbao = new ThongBaoMvc { CssClassName = "warning", Message = "Xin lỗi~, tài khoản của bạn không thể sử dụng chức năng quản lí khách hàng" };

        //            filterContext.Controller.TempData.Add("ResultAction", thongbao);
        //        }
            
        //}
        //private void KiemTraCaiDatLichLam(string actionName, ActionExecutingContext filterContext)
        //{
           
        //        if (actionName.Equals("TimeSheet"))
        //        {
        //            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
        //                           {
        //                               { "action", "Index" },
        //                               { "controller", "Home" }
        //                           });
        //            ThongBaoMvc thongbao = new ThongBaoMvc { CssClassName = "warning", Message = "Xin lỗi~, tài khoản của bạn không thể sử dụng chức năng cài đặt lịch làm việc" };

        //            filterContext.Controller.TempData.Add("ResultAction", thongbao);
        //        }
            
        //}
        //private void KiemTraLapBaoCao(string actionName,  ActionExecutingContext filterContext)
        //{
        //    if (actionName.Equals("DoanhThu") || actionName.Equals("CongNo") || actionName.Equals("ReportDoanhThu") || actionName.Equals("ReportCongNo"))
        //    {
        //        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
        //                           {
        //                               { "action", "Index" },
        //                               { "controller", "Home" }
        //                           });
        //        ThongBaoMvc thongbao = new ThongBaoMvc { CssClassName = "warning", Message = "Xin lỗi~, tài khoản của bạn không thể sử dụng chức năng lập báo cáo" };

        //        filterContext.Controller.TempData.Add("ResultAction", thongbao);
        //    }
        //}
    }
}