using CrystalDecisions.Shared;
using DATNQLBH.Manager;
using DATNQLBH.Manager.CheckChucNangNhanVien;
using DATNQLBH.Models;
using DATNQLBH.Models.CSDL;
using DATNQLBH.Report;
using log4net;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DATNQLBH.Controllers
{
    [LogActionFilter]
    [Authorize(Roles ="SuperAdmin,Admin")]
    public class ReportController : Controller
    {
        public static readonly ILog log = LogManager.GetLogger(typeof(ReportController));
        ShopEntities db;
        KiemTra kiemtra = new KiemTra();

        // GET: Report
        [CheckCNNV(Id = (int)ChucNangNVType.ReportDoanhThu)]
        public ActionResult DoanhThu()
        {
            return View();
        }
        [CheckCNNV(Id = (int)ChucNangNVType.ReportDoanhThu)]
        public ActionResult ReportDoanhThu(int Chon, DateTime? NgayBatDau, DateTime? NgayKetThuc)
        {
            DataSetDoanhThu ds = new DataSetDoanhThu();
            DataTable dt = ds.DataTableDoanhThu;
            var user = kiemtra.getUser(User.Identity.Name);
            db = ShopEntities.CreateEntitiesForSpecificDatabaseName(user.MaCN);

            var Dons = db.DonHangs.Where(x => x.State == (int)LapHoaDonKhach.Complete);
            var NgayHomNay = DateTime.Today;
            var NgayKeTiep = NgayHomNay.AddDays(1);
            //Ngày bắt đầu có giá trị --> Lọc lại đơn
            if(NgayBatDau.HasValue)
            {
                Dons=Dons.Where(x => x.Completiontime.CompareTo(NgayBatDau) >= 0);
            }
            //Ngày kết thúc có gia trị --> Lọc lại đơn
            if(NgayKetThuc.HasValue)
            {
                DateTime NgaySoSanh = NgayKeTiep.AddDays(1);
                Dons = Dons.Where(x => x.Completiontime.CompareTo(NgaySoSanh) < 0);
            }
            //Tính số ngày và ngày bắt đầu
            var NgayDau = new DateTime(); ;
            var NgayKe = new DateTime();
            double n = 0;
            if (NgayBatDau.HasValue && NgayKetThuc.HasValue) { n = (NgayKetThuc.Value - NgayBatDau.Value).TotalDays; NgayDau = NgayBatDau.Value; }
            else if (NgayBatDau.HasValue) { n = (DateTime.Now - NgayBatDau.Value).TotalDays; NgayDau = NgayBatDau.Value; }
            else if (NgayKetThuc.HasValue) { var k = Dons.OrderBy(x => x.Completiontime).First().Completiontime; n = (NgayKetThuc.Value - k).TotalDays; NgayDau = k; }

            //Theo Ngày
            if (Chon == 1)
            {
                var DonsHomNay = new List<DonHang>();
                if (!NgayBatDau.HasValue && !NgayKetThuc.HasValue)                   
                {
                     DonsHomNay = Dons.Where(x => x.Completiontime.CompareTo(NgayHomNay) >= 0 && x.Completiontime.CompareTo(NgayKeTiep) < 0).ToList();             
                    for (int i = 0; i < 7; i++)
                    {
                        var DonsMoi3Tieng = DonsHomNay.Where(x => x.Completiontime.Hour >= i * 3 && x.Completiontime.Hour < (i + 1) * 3);
                        if (DonsMoi3Tieng.Count() > 0)
                        {
                            dt.Rows.Add(i + "-" + ((i + 1) * 3) + " h", DonsMoi3Tieng.Sum(x => x.ChiTietDonHangs.Sum(y => y.Quantities)), DonsMoi3Tieng.Sum(x => x.ChiTietDonHangs.Sum(y => y.Price * y.Quantities)));
                        }
                        else
                        {
                            dt.Rows.Add(i + "-" + ((i + 1) * 3) + " h", 0, 0);
                        }
                    }
                }
                else
                {
                     for(int i=0;i< n;i++)
                    {
                        NgayKe = NgayDau.AddDays(1);
                        var DonsMoiNgay = DonsHomNay.Where(x => x.Completiontime >= NgayDau && x.Completiontime < NgayKe);
                        if (DonsMoiNgay.Count() > 0)
                        {
                            dt.Rows.Add(NgayDau.Day +"/"+NgayDau.Month, DonsMoiNgay.Sum(x => x.ChiTietDonHangs.Sum(y => y.Quantities)), DonsMoiNgay.Sum(x => x.ChiTietDonHangs.Sum(y => y.Price * y.Quantities)));
                        }
                        else
                        {
                            dt.Rows.Add(NgayDau.Day + "/" + NgayDau.Month, 0, 0);

                        }
                        NgayDau = NgayKe;
                    }
                }
            }
            //Theo Tuần
            else if (Chon == 2)
            {
                if (!NgayBatDau.HasValue && !NgayKetThuc.HasValue)
                {
                    var Thu2 = kiemtra.GetFirstDayOfWeek(NgayHomNay);
                    var TuanSau = Thu2.AddDays(7);
                    var DonsTuanNay = Dons.Where(x => x.Completiontime.CompareTo(Thu2) >= 0 && x.Completiontime.CompareTo(TuanSau) < 0);
                    for (int i = 0; i < 7; i++)
                    {
                        string Thu = "";
                        switch (i)
                        {
                            case 0: Thu = "Thứ Hai"; break;
                            case 1: Thu = "Thứ Ba"; break;
                            case 2: Thu = "Thứ Tư"; break;
                            case 3: Thu = "Thứ Năm"; break;
                            case 4: Thu = "Thứ Sáu"; break;
                            case 5: Thu = "Thứ Bảy"; break;
                            case 6: Thu = "Chủ Nhật"; break;
                        }

                        NgayKeTiep = Thu2.AddDays(1);
                        var DonNgay = DonsTuanNay.Where(x => x.Completiontime.CompareTo(Thu2) >= 0 && x.Completiontime.CompareTo(NgayKeTiep) < 0);
                        if (DonNgay.Count() > 0)
                        {
                            dt.Rows.Add(Thu, DonNgay.Sum(x => x.ChiTietDonHangs.Sum(y => y.Quantities)), DonNgay.Sum(x => x.ChiTietDonHangs.Sum(y => y.Price * y.Quantities)));
                        }
                        else
                        {
                            dt.Rows.Add(Thu, 0, 0);
                        }
                        Thu2 = Thu2.AddDays(1);
                    }
                }
                else
                {
                    var ChuNhat = kiemtra.GetFirstDayOfWeek(NgayDau).AddDays(6);
                    var TuanSau = ChuNhat.AddDays(1);
                    var flag = false;
                    string Tuan = "";
                    n = n / 7;
                    for (int i=0;i< n;i++)
                    {
                        Tuan = "Tuần " + (i + 1);
                        var DonMoiTuan = Dons.Where(x => x.Completiontime.CompareTo(NgayDau) >= 0 && x.Completiontime.CompareTo(TuanSau) < 0);
                        if (DonMoiTuan.Count() > 0)
                        {
                            if (!flag)
                            {
                                if (NgayDau.DayOfWeek == DayOfWeek.Monday)
                                {
                                    dt.Rows.Add(NgayDau.Day+"/"+NgayDau.Month+"-"+ChuNhat.Day+"/"+ChuNhat.Month, DonMoiTuan.Sum(x => x.ChiTietDonHangs.Sum(y => y.Quantities)), DonMoiTuan.Sum(x => x.ChiTietDonHangs.Sum(y => y.Price * y.Quantities)));
                                }
                                else
                                {
                                    dt.Rows.Add(Tuan, DonMoiTuan.Sum(x => x.ChiTietDonHangs.Sum(y => y.Quantities)), DonMoiTuan.Sum(x => x.ChiTietDonHangs.Sum(y => y.Price * y.Quantities)));
                                }
                                flag = true;
                            }
                            else
                            dt.Rows.Add(Tuan, DonMoiTuan.Sum(x => x.ChiTietDonHangs.Sum(y => y.Quantities)), DonMoiTuan.Sum(x => x.ChiTietDonHangs.Sum(y => y.Price * y.Quantities)));
                        }
                        else
                        {
                            dt.Rows.Add(Tuan, 0, 0);
                        }
                        NgayDau = TuanSau;
                        TuanSau = NgayDau.AddDays(7);
                    }
                    
                }
            }
            //Theo Tháng
            else if (Chon == 3)
            {
                if (!NgayBatDau.HasValue && !NgayKetThuc.HasValue)
                {
                    var ThangNay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    var ThangSau = ThangNay.AddMonths(1);
                    var CuoiThang = ThangSau.AddDays(-1);
                    var Dot = ThangNay.AddDays(5);
                    var DonsThangNay = Dons.Where(x => x.Completiontime.CompareTo(ThangNay) >= 0 && x.Completiontime.CompareTo(ThangSau) < 0);
                    for (int i = 0; i < 6; i++)
                    {
                        string Ngay = (i * 5 + 1) + "-" + ((i + 1) * 5);
                        if (i == 5) Ngay = (i * 5 + 1) + "-" + CuoiThang.Day;
                        var DonTrongThang = DonsThangNay.Where(x => x.Completiontime.CompareTo(ThangNay) >= 0 && x.Completiontime.CompareTo(Dot) < 0);
                        if (DonTrongThang.Count() > 0)
                        {
                            dt.Rows.Add(Ngay, DonTrongThang.Sum(x => x.ChiTietDonHangs.Sum(y => y.Quantities)), DonTrongThang.Sum(x => x.ChiTietDonHangs.Sum(y => y.Price * y.Quantities)));
                        }
                        else
                        {
                            dt.Rows.Add(Ngay, 0, 0);
                        }
                        ThangNay = ThangNay.AddDays(5);
                        Dot = Dot.AddDays(5);
                    }
                }
                else
                {
                    var DauThang = new DateTime(NgayDau.Year, NgayDau.Month, 1);
                    var ThangSau = DauThang.AddMonths(1);
                    var Thang = "T" + ThangSau.Month;
                    var ThangKetThuc = NgayDau.AddDays(n);
                    var flag = false;
                    var QuaHan = false;
                    var DonMoiThang = new List<DonHang>();
                    while (!QuaHan)
                    {
                        if(ThangKetThuc<=ThangSau)
                        {
                            QuaHan = true;
                            ThangKetThuc = ThangKetThuc.AddDays(1);
                            DonMoiThang = Dons.Where(x => x.Completiontime.CompareTo(NgayDau) >= 0 && x.Completiontime.CompareTo(ThangKetThuc) < 0).ToList();
                            ThangKetThuc = ThangKetThuc.AddDays(-1);
                            Thang = "1/" + ThangKetThuc.Month + "-" + ThangKetThuc.Day + "/" + ThangKetThuc.Month;
                        }
                        else
                        {
                            DonMoiThang = Dons.Where(x => x.Completiontime.CompareTo(NgayDau) >= 0 && x.Completiontime.CompareTo(ThangSau) < 0).ToList();
                            Thang = "T" + NgayDau.Month;
                        }
                        if (DonMoiThang.Count() > 0)
                        {
                            if (!flag)
                            {
                                if (NgayDau.Day != 1)
                                {
                                    var CuoiThang = ThangSau.AddDays(-1);
                                    dt.Rows.Add(NgayDau.Day + "/" + NgayDau.Month + "-" + CuoiThang.Day + "/" + CuoiThang.Month, DonMoiThang.Sum(x => x.ChiTietDonHangs.Sum(y => y.Quantities)), DonMoiThang.Sum(x => x.ChiTietDonHangs.Sum(y => y.Price * y.Quantities)));
                                }
                                else
                                {
                                    dt.Rows.Add(Thang, DonMoiThang.Sum(x => x.ChiTietDonHangs.Sum(y => y.Quantities)), DonMoiThang.Sum(x => x.ChiTietDonHangs.Sum(y => y.Price * y.Quantities)));
                                }
                                flag = true;
                            }
                            else
                                dt.Rows.Add(Thang, DonMoiThang.Sum(x => x.ChiTietDonHangs.Sum(y => y.Quantities)), DonMoiThang.Sum(x => x.ChiTietDonHangs.Sum(y => y.Price * y.Quantities)));
                        }
                        else
                        {
                            dt.Rows.Add(Thang, 0, 0);
                        }
                        NgayDau = ThangSau;
                        ThangSau = NgayDau.AddMonths(1);
                    }
                   
                }
            }
            //Theo quý
            else if (Chon == 4)
            {
                if (!NgayBatDau.HasValue && !NgayKetThuc.HasValue)
                {
                    var QuyDau = kiemtra.GetQuarter(DateTime.Now);
                    var QuySau = QuyDau.AddMonths(3);
                    var NgayCuoi = QuySau;
                    var DonsQuyNay = Dons.Where(x => x.Completiontime.CompareTo(QuyDau) >= 0 && x.Completiontime.CompareTo(QuySau) < 0);
                    for (int i = 0; i < 6; i++)
                    {
                        QuySau = QuyDau.AddDays(15);
                        string Ngay = QuyDau.Day + "/" + QuyDau.Month + "-" + QuySau.Day + "/" + QuySau.Month;
                        var DonTrongQuy = new List<DonHang>();
                        if (i == 5)
                        {
                            DonTrongQuy = DonsQuyNay.Where(x => x.Completiontime.CompareTo(QuyDau) >= 0 && x.Completiontime.CompareTo(NgayCuoi) < 0).ToList();
                        }
                        else
                        {
                            DonTrongQuy = DonsQuyNay.Where(x => x.Completiontime.CompareTo(QuyDau) >= 0 && x.Completiontime.CompareTo(QuySau) < 0).ToList();
                        }
                        if (DonTrongQuy.Count() > 0)
                        {
                            dt.Rows.Add(Ngay, DonTrongQuy.Sum(x => x.ChiTietDonHangs.Sum(y => y.Quantities)), DonTrongQuy.Sum(x => x.ChiTietDonHangs.Sum(y => y.Price * y.Quantities)));
                        }
                        else
                        {
                            dt.Rows.Add(Ngay, 0, 0);
                        }
                        QuyDau = QuyDau.AddDays(15);
                    }
                }
                else
                {
                    var k = NgayDau.Month % 3;
                    var QuySau = new DateTime();
                    if (k == 1) { QuySau = NgayDau.AddMonths(3); }
                    if (k == 2) { QuySau = NgayDau.AddMonths(2); }
                    if (k == 0) { QuySau = NgayDau.AddMonths(1); }
                    QuySau = new DateTime(QuySau.Year, QuySau.Month, 1);              
                    var Quy = "";
                    var QuaHan = false;
                    var flag = false;
                    var DonMoiQuy = new List<DonHang>();
                    var NgayCuoi = NgayDau.AddDays(n);
                    while (!QuaHan)
                    {
                        if (NgayCuoi <= QuySau)
                        {
                            QuaHan = true;
                            NgayCuoi = NgayCuoi.AddDays(1);
                            DonMoiQuy = Dons.Where(x => x.Completiontime.CompareTo(NgayDau) >= 0 && x.Completiontime.CompareTo(NgayCuoi) < 0).ToList();
                            NgayCuoi = NgayCuoi.AddDays(-1);
                            if (NgayCuoi.Month % 3 == 1) { Quy = "1/" + NgayCuoi.Month + "-" + NgayCuoi.Day + "/" + NgayCuoi.Month; }
                            else if(NgayCuoi.Month % 3 == 2) { Quy= "1/" + (NgayCuoi.Month-1) + "-" + NgayCuoi.Day + "/" + NgayCuoi.Month; }
                            else  { Quy = "1/" + (NgayCuoi.Month - 2) + "-" + NgayCuoi.Day + "/" + NgayCuoi.Month; }
                        }
                        else
                        {
                            DonMoiQuy = Dons.Where(x => x.Completiontime.CompareTo(NgayDau) >= 0 && x.Completiontime.CompareTo(QuySau) < 0).ToList();
                            Quy = "Quý" + ((NgayDau.Month/3)+1);
                        }

                        if (DonMoiQuy.Count() > 0)
                        {
                            if (!flag)
                            {
                                if (NgayDau.Day != 1 && (NgayDau.Month%3)!=1)
                                {
                                    var CuoiQuy = QuySau.AddDays(-1);
                                    dt.Rows.Add(NgayDau.Day + "/" + NgayDau.Month + "-" + CuoiQuy.Day + "/" + CuoiQuy.Month, DonMoiQuy.Sum(x => x.ChiTietDonHangs.Sum(y => y.Quantities)), DonMoiQuy.Sum(x => x.ChiTietDonHangs.Sum(y => y.Price * y.Quantities)));
                                }
                                else
                                {
                                    dt.Rows.Add(Quy, DonMoiQuy.Sum(x => x.ChiTietDonHangs.Sum(y => y.Quantities)), DonMoiQuy.Sum(x => x.ChiTietDonHangs.Sum(y => y.Price * y.Quantities)));
                                }
                                flag = true;
                            }
                            else
                                dt.Rows.Add(Quy, DonMoiQuy.Sum(x => x.ChiTietDonHangs.Sum(y => y.Quantities)), DonMoiQuy.Sum(x => x.ChiTietDonHangs.Sum(y => y.Price * y.Quantities)));
                        }
                        else
                        {
                            dt.Rows.Add(Quy, 0, 0);
                        }
                        NgayDau = QuySau;
                        QuySau = NgayDau.AddMonths(3);
                    }
                    if (k==1 && NgayDau.Day == 1)
                    {
                        
                    } 
                }
            }
            //Theo năm
            else if(Chon==5)
            {
                if (!NgayBatDau.HasValue && !NgayKetThuc.HasValue)
                {
                    var DauNam = new DateTime(DateTime.Now.Year, 1, 1);
                    var ThangSau = new DateTime();
                    var NamSau = DauNam.AddYears(1);
                    var DonsNamNay = Dons.Where(x => x.Completiontime.CompareTo(DauNam) >= 0 && x.Completiontime.CompareTo(NamSau) < 0);
                    for (int i = 0; i < 12; i++)
                    {
                        ThangSau = DauNam.AddMonths(1);
                        var DonTrongThang = DonsNamNay.Where(x => x.Completiontime.CompareTo(DauNam) >= 0 && x.Completiontime.CompareTo(ThangSau) < 0).ToList();
                        string Thang = "Tháng " + (i + 1);
                        if (DonTrongThang.Count() > 0)
                        {
                            dt.Rows.Add(Thang, DonTrongThang.Sum(x => x.ChiTietDonHangs.Sum(y => y.Quantities)), DonTrongThang.Sum(x => x.ChiTietDonHangs.Sum(y => y.Price * y.Quantities)));
                        }
                        else
                        {
                            dt.Rows.Add(Thang, 0, 0);
                        }
                        DauNam = DauNam.AddMonths(1);
                    }
                }
                else
                {
                    var NamSau = new DateTime(NgayDau.Year + 1, 1, 1);
                    var CuoiNam = NamSau.AddDays(-1);
                    var NgayCuoi = NgayDau.AddDays(n);
                    var Nam = "";
                    var QuaHan = false;
                    var flag = false;
                    var DonMoiNam = new List<DonHang>();                 
                    while (!QuaHan)
                    {
                        if (NgayCuoi <= NamSau)
                        {
                            QuaHan = true;
                            NgayCuoi = NgayCuoi.AddDays(1);
                            DonMoiNam = Dons.Where(x => x.Completiontime.CompareTo(NgayDau) >= 0 && x.Completiontime.CompareTo(NgayCuoi) < 0).ToList();
                            NgayCuoi = NgayCuoi.AddDays(-1);
                            Nam = "->" + NgayCuoi.Day + "/" + NgayCuoi.Month + "/" + NgayCuoi.Year;
                        }
                        else
                        {
                            DonMoiNam = Dons.Where(x => x.Completiontime.CompareTo(NgayDau) >= 0 && x.Completiontime.CompareTo(NamSau) < 0).ToList();
                            Nam = "Năm" + NgayDau.Year;
                        }

                        if (DonMoiNam.Count() > 0)
                        {
                            if (!flag)
                            {
                                if (NgayDau.Day != 1 && NgayDau.Month!= 1)
                                {                                 
                                    dt.Rows.Add(NgayDau.Day + "/" + NgayDau.Month + "/" + NgayDau.Day + "->", DonMoiNam.Sum(x => x.ChiTietDonHangs.Sum(y => y.Quantities)), DonMoiNam.Sum(x => x.ChiTietDonHangs.Sum(y => y.Price * y.Quantities)));
                                }
                                else
                                {
                                    dt.Rows.Add(Nam, DonMoiNam.Sum(x => x.ChiTietDonHangs.Sum(y => y.Quantities)), DonMoiNam.Sum(x => x.ChiTietDonHangs.Sum(y => y.Price * y.Quantities)));
                                }
                                flag = true;
                            }
                            else
                                dt.Rows.Add(Nam, DonMoiNam.Sum(x => x.ChiTietDonHangs.Sum(y => y.Quantities)), DonMoiNam.Sum(x => x.ChiTietDonHangs.Sum(y => y.Price * y.Quantities)));
                        }
                        else
                        {
                            dt.Rows.Add(Nam, 0, 0);
                        }
                        NgayDau = NamSau;
                        NamSau = NgayDau.AddYears(1);
                    }
                 
                }
            }
            CrystalReportDoanhThu rpt = new CrystalReportDoanhThu();
            rpt.Load();
            rpt.SetDataSource(ds);

            if (Chon == 1)
                rpt.SetParameterValue("NoiDung", "Ngày");
            if (Chon == 2)
                rpt.SetParameterValue("NoiDung", "Tuần");
            if (Chon == 3)
                rpt.SetParameterValue("NoiDung", "Tháng");
            if (Chon == 4)
                rpt.SetParameterValue("NoiDung", "Quý");
            if (Chon == 5)
                rpt.SetParameterValue("NoiDung", "Năm");
           
            Stream s = rpt.ExportToStream(ExportFormatType.PortableDocFormat);
            return File(s, "application/pdf");
        }

        [CheckCNNV(Id = (int)ChucNangNVType.ReportCongNo)]
        public ActionResult CongNo()
        {
            var user = kiemtra.getUser(User.Identity.Name);
            db = ShopEntities.CreateEntitiesForSpecificDatabaseName(user.MaCN);
            ViewBag.Khach = new SelectList(db.KhachHangs.Select(x => new { x.Id, x.FullName}), "Id", "FullName");
            return View();
        }
        [CheckCNNV(Id = (int)ChucNangNVType.ReportCongNo)]
        public ActionResult ReportCongNo(int? id, DateTime? NgayBatDau, DateTime? NgayKetThuc)
        {
           
                var user = kiemtra.getUser(User.Identity.Name);
                DataSetCongNo ds = new DataSetCongNo();
                DataTable dt = ds.DataTableCongNo;
                db = ShopEntities.CreateEntitiesForSpecificDatabaseName(user.MaCN);
                var Don = db.DonHangs.Where(x =>x.State == (int)LapHoaDonKhach.Waiting || x.State == (int)LapHoaDonKhach.Progressing);
                if (id != null)
                {
                    Don = Don.Where(x => x.CustomerId == id);
                }
                if (NgayBatDau.HasValue)
                {
                    Don = Don.Where(x => x.BuyTime.CompareTo(NgayBatDau) >= 0);
                }
                if (NgayKetThuc.HasValue)
                {
                    Don = Don.Where(x => x.BuyTime.CompareTo(NgayKetThuc) <= 0);
                }
                foreach (var item in Don)
                {
                    dt.Rows.Add(0,item.Address, item.ChiTietDonHangs.Sum(x => x.Price * x.Quantities), item.BuyTime, item.FullName, item.SDT);
                }
                CrystalReportCongNo rpt = new CrystalReportCongNo();
                rpt.Load();
                rpt.SetDataSource(ds);
                Stream s = rpt.ExportToStream(ExportFormatType.PortableDocFormat);
                return File(s, "application/pdf");
           
        }
        [CheckCNNV(Id = (int)ChucNangNVType.ReportTonKho)]
        public ActionResult TonKho()
        {
            var user = kiemtra.getUser(User.Identity.Name);
            db = ShopEntities.CreateEntitiesForSpecificDatabaseName(user.MaCN);
            ViewBag.LoaiHang = new SelectList(db.LoaiCap3s.Select(x => new { x.LoaiCap3Id, x.Name }), "LoaiCap3Id", "Name");
            ViewBag.MatHang = new SelectList(db.MatHangs.Select(x => new { x.ItemId, x.Name }), "ItemId", "Name");
            ViewBag.Kho = new SelectList(db.KhoHangs.Select(x => new { x.WarehouseID, x.Name }), "WarehouseID", "Name");
            return View();
        }
        [CheckCNNV(Id = (int)ChucNangNVType.ReportTonKho)]
        public ActionResult ReportTonKho(int? MaHang,int? KhoId, int? LoaiCap3Id)
        {
            var user = kiemtra.getUser(User.Identity.Name);
            db = ShopEntities.CreateEntitiesForSpecificDatabaseName(user.MaCN);
            DataSetTonKho ds = new DataSetTonKho();
            DataTable dtKho = ds.DataTableKho;
            DataTable dtTonKho = ds.DataTableTonKho;
            ds.EnforceConstraints = false;
            var Khos = db.KhoHangs.ToList();
            var TonKhos = db.ChiTietKhoHangs.ToList();
            if(MaHang.HasValue)
            {
                TonKhos = TonKhos.Where(x => x.MatHang.ItemId == MaHang).ToList();
            }
            var Kho = new KhoHang();
            if (KhoId.HasValue)
            {
                Kho = Khos.FirstOrDefault(x => x.WarehouseID == KhoId);
                TonKhos = TonKhos.Where(x => x.KhoHang.WarehouseID == KhoId).ToList();
            }
            if(LoaiCap3Id.HasValue)
            {
                TonKhos = TonKhos.Where(x => x.MatHang.LoaiCap3Id == LoaiCap3Id.Value).ToList();
            }

            if(Kho.WarehouseID!=0)
            {
                dtKho.Rows.Add(Kho.WarehouseID, Kho.Name, Kho.DiaChi, Kho.SDT);
            }
            else
            {
                foreach(var item in Khos)
                {
                    dtKho.Rows.Add(item.WarehouseID, item.Name, item.DiaChi, item.SDT);
                }
                
            }            
            foreach(var item in TonKhos)
            {
                dtTonKho.Rows.Add(item.MatHang.Name, item.MatHang.QuyCach.Name, item.MatHang.LoaiCap3.Name, item.Quantities, item.WarehouseID);
            }
            CrystalReportTonKho rpt = new CrystalReportTonKho();
            rpt.Load();
            rpt.SetDataSource(ds);
            Stream s = rpt.ExportToStream(ExportFormatType.PortableDocFormat);
            return File(s, "application/pdf");
        }

        [Authorize(Roles = "SuperAdmin")]
        public ActionResult ThongKeGianHang()
        {
           
            return View();
        }
        [Authorize(Roles = "SuperAdmin")]
        public ActionResult ReportThongKeGianHang()
        {
            db = new ShopEntities();
            var rolestore = new RoleStore<IdentityRole>(db);
            var roleManager = new RoleManager<IdentityRole>(rolestore);
            var userstore = new UserStore<ApplicationUser>(db);
            var userManager = new UserManager<ApplicationUser>(userstore);

            var user = kiemtra.getUser(User.Identity.Name);
            DataSetTongQuanGianHang dsTongQuan = new DataSetTongQuanGianHang();
            DataTable dtGianHangTongQuan = dsTongQuan.DataTableGianHangTongQuan;


            var TongQuan = db.HopDongChiNhanhs.GroupBy(x => x.MaLoaiGianHang).ToList();
            foreach (var item in TongQuan)
            {
                dtGianHangTongQuan.Rows.Add(item.Count(), item.First().LoaiGianHang.Name, item.First().LoaiGianHang.Price);
            }

         


            DataSetThongKeGianHang ds = new DataSetThongKeGianHang();
            DataTable dtThongKeGianHang = ds.DataTableThongKeGianHang;
          
            DataTable dtGianHang = ds.DataTableGianHang;

            var DsGianHang = db.ChiNhanhs.Where(x => x.HopDongChiNhanhs.Count>0);
            foreach( var gianhang in DsGianHang)
            {
                var taikhoans = gianhang.TaiKhoans.ToList();
                foreach (var taikhoan in taikhoans)
                {
                    if (userManager.IsInRole(taikhoan.Id,"Admin"))
                    {
                        dtGianHang.Rows.Add(gianhang.Name,taikhoan.FullName,taikhoan.UserName,taikhoan.Address,taikhoan.PhoneNumber);
                    }
                }
                foreach(var hopdong in gianhang.HopDongChiNhanhs)
                {
                    dtThongKeGianHang.Rows.Add(hopdong.LoaiGianHang.Name, hopdong.BeginDate, hopdong.EndDate,gianhang.Name);
                }
            }
           
            CrystalReportThongKeGianHang rpt = new CrystalReportThongKeGianHang();
            rpt.Load();
            rpt.SetDataSource(ds);
            rpt.Subreports[0].SetDataSource(dsTongQuan);
            Stream s = rpt.ExportToStream(ExportFormatType.PortableDocFormat);
            return File(s, "application/pdf");
        }
    }
    
    
   
}