using DATNQLBH.Manager;
using DATNQLBH.Models;
using DATNQLBH.Models.CSDL;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;

namespace DATNQLBH.Controllers
{
    public class WinformConnectController : Controller
    {
        KiemTra kiemtra = new KiemTra();
        ShopEntities db = new ShopEntities(); private static readonly ILog log = LogManager.GetLogger(typeof(WinformConnectController));
        // GET: WinformConnect
        [HttpPost]
 
        public JsonResult GetDuLieu(string UserName,bool ChapNhan)
        {
            var user = kiemtra.getUser(UserName);
            if (user!=null && ChapNhan== true)
            {
                db = ShopEntities.CreateEntitiesForSpecificDatabaseName(user.MaCN);
                var lstMatHang = db.MatHangs.Select(x => new { x.AuthorName, x.ItemId, x.LoaiCap3Id, x.Name, x.Price, x.Propertynames, x.QuyCachId, x.Reduction, x.Serial, x.VAT });
                var lstLoaiCap1 = db.LoaiCap1s.Select(x => new { x.LoaiCap1Id, x.Name });
                var lstLoaiCap2 = db.LoaiCap2s.Select(x => new { x.LoaiCap2Id, x.Name, x.LoaiCap1Id });
                var lstLoaiCap3 = db.LoaiCap3s.Select(x => new { x.LoaiCap3Id, x.Name, x.PropertyNames, x.LoaiCap2Id });
                var lstKhachHang = db.KhachHangs.Select(x => new { x.Address, x.CMND, x.Email, x.ExpVip, x.FullName, x.Id, x.SDT, x.Vip });
                var lstKhoHang = db.KhoHangs.Select(x => new { x.Name, x.SDT, x.WarehouseID, x.DiaChi,CTKhoHang=x.ChiTietKhoHangs.Select(y=> new {y.ItemId,y.WarehouseID,y.Quantities }) });
                var lstNhaCC = db.NhaCCs.Select(x => new { x.TaiKhoan, x.SDT, x.Name, x.MaNCC, x.Fax, x.Email, x.Address });
                var lstQuyCach = db.QuyCachs.Select(x => new { x.QuyCachId, x.Name });
             

                return Json(new { lstMatHang, lstLoaiCap1, lstLoaiCap2, lstLoaiCap3, lstKhachHang, lstKhoHang, lstNhaCC, lstQuyCach },JsonRequestBehavior.AllowGet);
            }
            return Json(new { });
        }

        [HttpPost]
     
        public JsonResult DongBo(List<DonHang> don,string UserName,bool ChapNhan)
        {
            var user = kiemtra.getUser(UserName);
            if (user != null && ChapNhan == true)
            {
                db = ShopEntities.CreateEntitiesForSpecificDatabaseName(user.MaCN);
                foreach(var item in don)
                {
                    item.UserId = user.Id;
                    db.DonHangs.Add(item);
                }
                try {
                    db.SaveChanges();
                }
                catch(Exception e)
                {
                    log.Error("Lỗi post dữ liệu đồng bộ", e);
                }
                return Json(new { data=1 }, JsonRequestBehavior.AllowGet);
            }
            return Json(new{ data=0}, JsonRequestBehavior.AllowGet);
        }
    }
}