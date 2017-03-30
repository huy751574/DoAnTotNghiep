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
    [LogActionFilter]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class LoaiCap1Controller : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(LoaiCap1Controller));
        private ShopEntities db;
        KiemTra kiemtra = new KiemTra();
        public LoaiCap1Controller()
        {
            kiemtra = new KiemTra();
            var user = kiemtra.getUser(kiemtra.HttpContext.User.Identity.Name);
            db = ShopEntities.CreateEntitiesForSpecificDatabaseName(user.MaCN);
        }
        public ActionResult Index()
        {
            
            var loaiCap1 = db.LoaiCap1s.ToList();
            return View(loaiCap1);
        }

        [CheckCNNV(Id = (int)ChucNangNVType.UpdateLoaiCap1)]
        // GET: LoaiCap1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiCap1 loaiCap1 = db.LoaiCap1s.Find(id);
            if (loaiCap1 == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaCN = new SelectList(db.ChiNhanhs, "MaCN", "Name", loaiCap1.MaCN);
            return View(loaiCap1);
        }

        // POST: LoaiCap1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CheckCNNV(Id = (int)ChucNangNVType.UpdateLoaiCap1)]
        public ActionResult Edit(LoaiCap1 loaiCap1)
        {
            ThongBaoMvc thongbao;
            ViewBag.MaCN = new SelectList(db.ChiNhanhs, "MaCN", "Name", loaiCap1.MaCN);
            if (ModelState.IsValid)
            {
                db.Entry(loaiCap1).State = EntityState.Modified;
                try {
                    db.SaveChanges();
                    LogMgr.AddLog(User.Identity.Name, (int)FunctionType.UpdateLoaiCap1, "Update thông tin Loại cấp 1" + loaiCap1.Name + "-" + loaiCap1.LoaiCap1Id);
                    thongbao = new ThongBaoMvc { CssClassName = "success", Message = "Thành công thay đổi thông tin loại cấp 1." };
                    TempData["ResultAction"] = thongbao;
                    return RedirectToAction("Index");
                }
                catch(Exception e)
                {
                    log.Error("Lỗi thay đổi loại cấp 1" +e.Message);
                    thongbao = new ThongBaoMvc { CssClassName = "danger", Message = "Lỗi." };
                    TempData["ResultAction"] = thongbao;
                }
            }
           
            return View(loaiCap1);
        }

        // GET: LoaiCap1/Delete/5
      
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CheckCNNV(Id = (int)ChucNangNVType.ThemLoaiCap1)]
        public ActionResult Index(string name)
        {
            ThongBaoMvc thongbao;
            LoaiCap1 loaicap1 = new LoaiCap1();
         
            loaicap1.Name = name;
            loaicap1.MaCN = db.TaiKhoans.FirstOrDefault(s => s.UserName.Equals(User.Identity.Name)).MaCN;
            db.LoaiCap1s.Add(loaicap1);
            try {
                db.SaveChanges();
                LogMgr.AddLog(User.Identity.Name, (int)FunctionType.AddLoaiCap1, "Thêm Loại cấp 1 " + loaicap1.Name);
                thongbao = new ThongBaoMvc { CssClassName = "success", Message = "Thêm loại cấp 1 mới thành công." };
                TempData["ResultAction"] = thongbao;
            }
            catch(Exception e)
            {
                log.Error("Lỗi thêm loại cấp 1: " + e.Message);
                thongbao = new ThongBaoMvc { CssClassName = "danger", Message = "Lỗi." };
                TempData["ResultAction"] = thongbao;
            }
            var loaiCap1 = db.LoaiCap1s.Include(l => l.ChiNhanh).ToList();
            return View(loaiCap1);



        }
        //[HttpPost]
     
        //public JsonResult DelLoai1(int id)
        //{
        //    if (db.LoaiCap2s.FirstOrDefault(s=>s.LoaiCap1Id==id)==null)
        //    {
        //        LoaiCap1 loaicap1 = db.LoaiCap1s.FirstOrDefault(s => s.LoaiCap1Id == id);
        //        db.LoaiCap1s.Remove(loaicap1);
        //        try {
        //            db.SaveChanges();
        //            LogMgr.AddLog(User.Identity.Name, (int)FunctionType.XoaLoaiCap1, "Xoá Loại Cấp 1 " + loaicap1.Name + "-" + loaicap1.LoaiCap1Id);
        //            return Json(new { smg = "Xoá thành công!" });
        //        }catch(Exception e)
        //        {
        //            log.Error("Lỗi xóa loại cấp 1: " + e.Message);
        //            return Json(new { smg = "Lỗi!" });
        //        }
        //    }
        //    else 
        //    {
        //        return Json(new { smg= "Không được phép xoá do có sản phẩm loại này!"});
        //    }
          

        //}
    }
}
