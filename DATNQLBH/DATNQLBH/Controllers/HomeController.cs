using DATNQLBH.Manager;
using DATNQLBH.Models;
using DATNQLBH.Models.CSDL;
using log4net;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace thuctap.Controllers
{
    //  [Authorize(Roles="Admin,SuperAdmin,User")] 
    [Authorize(Roles = "Admin,SuperAdmin,Staff")]
    public class HomeController : Controller
    {
        KiemTra kiemtra = new KiemTra();
        private ShopEntities db;
        private static readonly ILog log = LogManager.GetLogger(typeof(HomeController));
        public HomeController()
        {
          
            kiemtra = new KiemTra();
            var user = kiemtra.getUser(kiemtra.HttpContext.User.Identity.Name);
            db = ShopEntities.CreateEntitiesForSpecificDatabaseName(user.MaCN);
        }
        /// <summary>
        ///    3, Name = "Hoàn Thành",
        ///    0, Name = "Chờ Duyệt",
        ///    1, Name = "Đang giao",
        ///    2, Name = "Đã Hủy"
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            HienThiHome home = new HienThiHome();
            
            //Lấy Mã chi nhánh của tài khoản
            var MaCN = kiemtra.getUser(User.Identity.Name).MaCN;
            var DateOfToday = new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day);
            var NextDay = DateOfToday.AddDays(1);
            //Lấy danh sách đơn hàng mà trạng thái là hoàn thành, thuộc chi nhánh người dùng đang đăng nhập. Trong đó, tìm tất cả các chi tiết đơn hàng bằng thời gian hoàn thành trong ngày hôm nay
            var listDonHang = db.ChiTietDonHangs.Where(s => s.DonHang.State == (int)LapHoaDonKhach.Complete && s.DonHang.Completiontime.CompareTo(DateOfToday) >=0 && s.DonHang.Completiontime.CompareTo(NextDay) < 0).ToList();
         
            long totalMoney = 0;
            int toDay = 0;

            if (listDonHang.Count() > 0)
            {
                //Lấy tổng số lượng sản phẩm trong ngày
                toDay = listDonHang.Sum(x => x.Quantities);
              
                    //TÍnh tổng lượng tiền
                totalMoney = listDonHang.Sum(x=>(x.Price * x.Quantities));
                
            }
            var beginDate = kiemtra.GetFirstDayOfWeek(DateTime.Now);
            var endDate = beginDate.AddDays(6);
            int sevenDay = 0;
            var listsevenDay = db.ChiTietDonHangs.Where(s => s.DonHang.State == (int)LapHoaDonKhach.Complete && s.DonHang.Completiontime.CompareTo(beginDate)>=0 && s.DonHang.Completiontime.CompareTo(endDate)<=0).ToList();
            if (listsevenDay.Count() > 0)
                //Tổng sản phẩm trong 7 ngày
                sevenDay = listsevenDay.Sum(x => x.Quantities);

           
            beginDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            endDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month+1, 1);
            endDate = endDate.AddDays(-1);
            int thisMonth = 0;
            long totalMoneyMonth = 0;
            //Lấy đơn hàng chi tiết từ cuối tháng trước
            var ListthisMonth = db.ChiTietDonHangs.Where(s => s.DonHang.Completiontime.CompareTo(beginDate)>=0 && s.DonHang.Completiontime.CompareTo(endDate)<=0 && s.DonHang.State == (int)LapHoaDonKhach.Complete && s.DonHang.MaCN.Equals(MaCN));
            if (ListthisMonth.Count() > 0)
            {
                //Số lượng sản phẩm
                thisMonth = ListthisMonth.Sum(x => x.Quantities);
                //Tổng tiền
                totalMoneyMonth = ListthisMonth.Sum(x => (x.Quantities * x.Price));
            }
            long AverageMoney = totalMoneyMonth / DateTime.DaysInMonth(DateTime.Now.Year,DateTime.Now.Month);
            var AccountCheck = kiemtra.getUser(User.Identity.Name);

            home = new HienThiHome { Today = toDay, Week = sevenDay, TotalMoney = totalMoney, ThisMonth = thisMonth, AverageMoneyMonth = AverageMoney, TotalMoneyMonth = totalMoneyMonth };
            return View(home);
        }

        public ActionResult Welcome()
        {
            return View();
        }
   
        public ActionResult GetLog(int curPage)
        {
            return Json(LogMgr.GetTopLog(User.Identity.Name, curPage), JsonRequestBehavior.AllowGet);
        }
       
        public ActionResult GetWeeklySale()
        {
            var MaCN = kiemtra.getUser(User.Identity.Name).MaCN;
            var ArraySell = new int[] { 0, 0, 0, 0, 0, 0, 0 };          
            var ArrayWeeklySell = new int[] { 0, 0, 0, 0, 0, 0, 0 };      
            var beginDate = kiemtra.GetFirstDayOfWeek(DateTime.Today); //Hàm GetFirstDayOfWeek,lấy thứ 2 datetime      
            beginDate = new DateTime(beginDate.Year, beginDate.Month, beginDate.Day);
            var NextDay = beginDate.AddDays(1);
            for (int i = 0; i < 7; i++)
            {

                //Lấy theo beginDate
                //hoàn thành
                var value = db.ChiTietDonHangs.Where(s => s.DonHang.State == (int)LapHoaDonKhach.Complete  && s.DonHang.Completiontime.CompareTo(beginDate) >= 0 && s.DonHang.Completiontime.CompareTo(NextDay) < 0).ToList();
                if (value.Count() > 0)
                {
                    ArraySell[i] = value.Sum(x => x.Quantities);
                }

                //từ 7 ngày trước
                //hoàn thành
                var tuantruoc = beginDate.AddDays(-7);
                var value2 = db.ChiTietDonHangs.Where(s => s.DonHang.State == (int)LapHoaDonKhach.Complete && s.DonHang.Completiontime==tuantruoc).ToList();
                if (value2.Count() > 0)
                {
                    ArrayWeeklySell[i] = value2.Sum(x => x.Quantities);
                }
                beginDate = beginDate.AddDays(1);
                NextDay = beginDate.AddDays(1);
            }
            return Json(new { ArraySell, ArrayWeeklySell }, JsonRequestBehavior.AllowGet);
        }

       
        [LogActionFilter]
        public ActionResult TimeSheet()
        {
            return View();
        }
        public JsonResult GetTimeSheet()
        {
            var user = db.TaiKhoans.FirstOrDefault(s => s.UserName == User.Identity.Name);
            if (user != null)
            {
                return Json(new { array = user.TimeSheets.Select(s => new { id = s.Id, title = s.Title, start = s.Start, end = s.End, allDay = s.allDay }).ToList() }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { array = new string[0] }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AddTimeSheet(string Title = "", string Start = "", string End = "", bool allDay = true)
        {

            var acc = db.TaiKhoans.FirstOrDefault(s => s.UserName == User.Identity.Name);
           
          
                Start = (DateTime.Parse(Start)).ToString("yyyy-MM-dd HH:mm");
                if (!string.IsNullOrEmpty(End))
                {
                    End = (DateTime.Parse(End)).ToString("yyyy-MM-dd HH:mm");
                }

                var timeSheet = db.TimeSheets.Add(new TimeSheet { Id = 0, UserId = acc.Id, Title = Title, Start = Start, End = End, allDay = allDay });
            try { 

                db.SaveChanges();
                
                return Json(new { array = 1 }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                log.Error("Lỗi thêm lịch: " + e.Message);
                return Json(new { array = "" }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult UpdateTimeSheet(int id, string title, string start, string end, bool allDay = true)
        {

            var user = db.TaiKhoans.FirstOrDefault(s => s.UserName == User.Identity.Name);

            var timeSheet = db.TimeSheets.FirstOrDefault(s => s.Id == id);
            if (timeSheet != null)
            {
                timeSheet.Title = title;
                if (!String.IsNullOrEmpty(start))
                    timeSheet.Start = start;
                if (!String.IsNullOrEmpty(end))
                    timeSheet.End = end;
                timeSheet.allDay = allDay;
                try
                {
                    db.SaveChanges();
                    return Json(new { array = 1 }, JsonRequestBehavior.AllowGet);
                }
                catch(Exception e)
                {
                    log.Error("Lỗi sửa thông tin lịch: " + e.Message);

                    return Json(new { array="" }, JsonRequestBehavior.AllowGet);
                }

            }
           
            return Json(new { array = "" }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult DeleteTimeSheet(int Id = -1)
        {
            var user = db.TaiKhoans.FirstOrDefault(s => s.UserName == User.Identity.Name);

            if (Id != -1)
            {
                var timeSheet = db.TimeSheets.FirstOrDefault(s => s.Id == Id);
                if (timeSheet != null)
                {
                    db.TimeSheets.Remove(timeSheet);
                    try {
                        db.SaveChanges();
                        return Json(new { array = 1 });
                    }
                    catch (Exception e)
                    {
                        log.Error("Lỗi xóa thông tin lịch: " + e.Message);
                        return Json(new { array = "" });
                    }

                }
            } 
            return Json(new { array = "" }, JsonRequestBehavior.AllowGet);
        }
          
           
        
        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}