using DATNQLBH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace DATNQLBH.Manager
{
    public class KiemTra
    {
        ShopEntities db = new ShopEntities();
        public int XacNhanHopDong(ApplicationUser user)
        {
            var hientai = user.ChiNhanh.HopDongChiNhanhs.Where(x => x.EndDate.CompareTo(DateTime.Now) > 0);

            if (hientai.Where(x => x.MaLoaiGianHang==4).Count()>0) return 4;
            if (hientai.Where(x => x.MaLoaiGianHang == 3).Count() > 0) return 3;
            if (hientai.Where(x => x.MaLoaiGianHang == 2).Count() > 0) return 2;
            return 1;
        }

        public ApplicationUser getUser(string username)
        {
            db = new ShopEntities();
            return db.TaiKhoans.FirstOrDefault(x => x.UserName.Equals(username));
             
        }
        public new HttpContextBase HttpContext
        {
            get
            {
                HttpContextWrapper context =
                    new HttpContextWrapper(System.Web.HttpContext.Current);
                return (HttpContextBase)context;
            }
        }
        //Lấy thời gian của thứ 2
        public DateTime GetFirstDayOfWeek(DateTime dayInWeek)
        {
            var dayofWeek = (int)dayInWeek.DayOfWeek;
            switch (dayofWeek)
            {
                case 0:
                    return dayInWeek.AddDays(-6);
                case 1:
                    return dayInWeek;
                case 2:
                    return dayInWeek.AddDays(-1);
                case 3:
                    return dayInWeek.AddDays(-2);
                case 4:
                    return dayInWeek.AddDays(-3);
                case 5:
                    return dayInWeek.AddDays(-4);
                case 6:
                    return dayInWeek.AddDays(-5);
            }
            return dayInWeek;
        }

        public DateTime GetQuarter(DateTime date)
        {
            if (date.Month >= 4 && date.Month <= 6)
                return new DateTime(date.Year,4,1);
            else if (date.Month >= 7 && date.Month <= 9)
                return new DateTime(date.Year, 7, 1);
            else if (date.Month >= 10 && date.Month <= 12)
                return new DateTime(date.Year, 10, 1);
            else
                return new DateTime(date.Year, 1, 1);

        }
    }
}