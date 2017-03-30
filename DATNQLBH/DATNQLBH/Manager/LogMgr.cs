using DATNQLBH.Models;
using DATNQLBH.Models.CSDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DATNQLBH.Manager
{
    public sealed class LogMgr
    {
        private static KiemTra kiemtra = new KiemTra();
        
        private static ShopEntities db =  ShopEntities.CreateEntitiesForSpecificDatabaseName(kiemtra.getUser(HttpContext.Current.User.Identity.Name).MaCN);      
        public static void AddLog(string userName, int type, string smg)
        {
            LichSuThaoTac log = new LichSuThaoTac();
            
            log.UserId = db.TaiKhoans.FirstOrDefault(s => s.UserName == userName).Id;
            log.Function = type;
            log.Smg = smg;
            log.LogTime = DateTime.Now;
            log.MaCN = db.TaiKhoans.FirstOrDefault(s => s.UserName == userName).MaCN;
            db.LichSuThaoTacs.Add(log);
            db.SaveChanges();
        }
      
        public static List<ShowLog> GetTopLog(string UserName,int curPage)
        {
            int sumItem = 0;
            int sumPage = 0;
            int skipRow = 0;
            int takeRow = 0;
            //Lấy lịch sử thao tác theo Mã chi nhánh của người đang đăng nhập
            var ListLog = db.LichSuThaoTacs.Where(s=>s.MaCN == db.TaiKhoans.FirstOrDefault(x=>x.UserName.Equals(UserName)).MaCN).OrderByDescending(s=>s.Id).Select(s => new ShowLog { UserName = s.TaiKhoan.UserName, Type = s.Function, Smg = s.Smg ,Time = s.LogTime}).ToList();
            sumItem = ListLog.Count;
            if (sumItem > 0 && (curPage - 1) * 10 < sumItem)
            {
                //Tính số trang hiển thị
                sumPage = sumItem / 10;
                if (sumItem % 10 > 0)
                    sumPage += 1;
                if (curPage < 1)
                    curPage = 1;
                else if (curPage > sumPage) //Không được lớn hơn tổng trang
                    curPage = sumPage;
                skipRow = (curPage - 1) * 10;
                takeRow = 10;
                //Lấy takeRow dòng dữ liệu sau skipRow dòng dữ liệu
                ListLog = ListLog.Skip(skipRow).Take(takeRow).ToList();
                return ListLog;
            }
            else
                return new List<ShowLog>();
           
        }
    }
}