using DATNQLBH.Manager;
using DATNQLBH.Manager.CheckChucNangNhanVien;
using DATNQLBH.Models;
using DATNQLBH.Models.CSDL;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace thuctap.Controllers
{
    [Authorize(Roles = "SuperAdmin,Admin")]
    [LogActionFilter]
    public class WarehouseController : Controller
    {
        private ShopEntities db;
        KiemTra kiemtra = new KiemTra();
        private static readonly ILog log = LogManager.GetLogger(typeof(WarehouseController));

        public WarehouseController()
        {
            kiemtra = new KiemTra();
            var user = kiemtra.getUser(kiemtra.HttpContext.User.Identity.Name);
            db = ShopEntities.CreateEntitiesForSpecificDatabaseName(user.MaCN);
        }
        public ActionResult Index()
        {
            var warehouses = db.KhoHangs.ToList();
            return View(warehouses);
        }


        [CheckCNNV(Id = (int)ChucNangNVType.ThemKho)]
        // GET: /Warehouse/Create
        public ActionResult Create()
        {
            
            return View();
        }

        // POST: /Warehouse/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CheckCNNV(Id =(int)ChucNangNVType.ThemKho)]
        public ActionResult Create(KhoHang warehouse)
        {
            if (ModelState.IsValid)
            {
                ThongBaoMvc thongbao;
                warehouse.MaCN = kiemtra.getUser(User.Identity.Name).MaCN;
                db.KhoHangs.Add(warehouse);
                try {
                    db.SaveChanges();
                    thongbao = new ThongBaoMvc { CssClassName = "success", Message = "Thành công thêm kho hàng mới." };
                    TempData["ResultAction"] = thongbao;
                    return RedirectToAction("Index");
                }catch(Exception e)
                {
                    log.Error("Lỗi thêm kho hàng mới: " + e.Message);
                    thongbao = new ThongBaoMvc { CssClassName = "danger", Message = "Lỗi." };
                    TempData["ResultAction"] = thongbao;
                }
            }
            return View(warehouse);
        }
        [CheckCNNV(Id = (int)ChucNangNVType.UpdateKho)]
        // GET: /Warehouse/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhoHang warehouse = db.KhoHangs.Find(id);
            if (warehouse == null)
            {
                return HttpNotFound();
            }
          
            return View(warehouse);
        }

        // POST: /Warehouse/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CheckCNNV(Id = (int)ChucNangNVType.UpdateKho)]
        public ActionResult Edit(KhoHang warehouse)
        {
            if (ModelState.IsValid)
            {
                ThongBaoMvc thongbao;
                db.Entry(warehouse).State = EntityState.Modified;
                try {
                    db.SaveChanges();
                    thongbao = new ThongBaoMvc { CssClassName = "success", Message = "Thành công thay đổi thông tin kho hàng." };
                    TempData["ResultAction"] = thongbao;
                    return RedirectToAction("Index");
                }catch(Exception e)
                {
                    thongbao = new ThongBaoMvc { CssClassName = "danger", Message = "Lỗi." };
                    TempData["ResultAction"] = thongbao;
                    log.Error("Lỗi thay đổi thông tin kho: " + e.Message);
                }
            }
          
            return View(warehouse);
        }

        //// GET: /Warehouse/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    var warehouse = db.KhoHangs.Find(id);
        //    if (warehouse == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(warehouse);
        //}

        //// POST: /Warehouse/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(KhoHang kho)
        //{
        //    ThongBaoMvc thongbao;
        //    db.KhoHangs.Remove(db.KhoHangs.Find(kho.WarehouseID));
        //    try {
        //        db.SaveChanges();
        //        thongbao = new ThongBaoMvc { CssClassName = "success", Message = "Xóa kho hàng thành công." };
        //        TempData["ResultAction"] = thongbao;
              
        //    }catch(Exception e)
        //    {
        //        log.Error("Lỗi xóa kho hàng: ");
        //        thongbao = new ThongBaoMvc { CssClassName = "danger", Message = "Lỗi." };
        //        TempData["ResultAction"] = thongbao;
        //    }
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
