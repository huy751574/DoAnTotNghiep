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
    public class LoaiCap2Controller : Controller
    {
        private ShopEntities db;
        KiemTra kiemtra = new KiemTra();

        private static readonly ILog log = LogManager.GetLogger(typeof(LoaiCap2Controller));
        public LoaiCap2Controller()
        {
            kiemtra = new KiemTra();
            var user = kiemtra.getUser(kiemtra.HttpContext.User.Identity.Name);
            db = ShopEntities.CreateEntitiesForSpecificDatabaseName(user.MaCN);
        }

        public ActionResult Index()
        {
            ViewBag.MaLoaiCap1 = new SelectList(db.LoaiCap1s, "LoaiCap1Id", "Name");
     
            var loaiCap2 = db.LoaiCap2s.Include(l => l.LoaiCap1);
            return View(loaiCap2.ToList());
        }





        // POST: LoaiCap2/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    
        [CheckCNNV(Id = (int)ChucNangNVType.UpdateLoaiCap2)]
        // GET: LoaiCap2/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiCap2 loaiCap2 = db.LoaiCap2s.Find(id);
            if (loaiCap2 == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaCN = new SelectList(db.ChiNhanhs, "MaCN", "Name", loaiCap2.MaCN);
            ViewBag.LoaiCap1Id = new SelectList(db.LoaiCap1s, "LoaiCap1Id", "Name", loaiCap2.LoaiCap1Id);
            return View(loaiCap2);
        }

        // POST: LoaiCap2/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CheckCNNV(Id = (int)ChucNangNVType.UpdateLoaiCap2)]
        public ActionResult Edit(LoaiCap2 loaiCap2)
        {
            ThongBaoMvc thongbao;
            ViewBag.MaCN = new SelectList(db.ChiNhanhs, "MaCN", "Name", loaiCap2.MaCN);
            ViewBag.LoaiCap1Id = new SelectList(db.LoaiCap1s, "LoaiCap1Id", "Name", loaiCap2.LoaiCap1Id);
            if (ModelState.IsValid)
            {
                db.Entry(loaiCap2).State = EntityState.Modified;
                try {
                    db.SaveChanges();
                    LogMgr.AddLog(User.Identity.Name, (int)FunctionType.UpdateLoaiCap2, "Update thông tin Loại cấp 2 " + loaiCap2.Name);
                    thongbao = new ThongBaoMvc { CssClassName = "success", Message = "Thành công thay đổi thông tin loại cấp 2." };
                    TempData["ResultAction"] = thongbao;
                    return RedirectToAction("Index");
                }
                catch(Exception e)
                {
                    thongbao = new ThongBaoMvc { CssClassName = "danger", Message = "Lỗi." };
                    TempData["ResultAction"] = thongbao;
                    log.Error("Lỗi thay đổi thông tin loại cấp 2: " + e.Message);
                    return View(loaiCap2);
                }
             
            }
            return View(loaiCap2);

        }

        

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
        [CheckCNNV(Id = (int)ChucNangNVType.ThemLoaiCap2)]
        public ActionResult Them(string NameCap2, int MaLoaiCap1)
        {
            ThongBaoMvc thongbao;
            LoaiCap2 loaiCap2 = new LoaiCap2();
        
            loaiCap2.Name = NameCap2;
            loaiCap2.LoaiCap1Id = MaLoaiCap1;
            loaiCap2.MaCN = db.TaiKhoans.FirstOrDefault(s => s.UserName == User.Identity.Name).MaCN;
            db.LoaiCap2s.Add(loaiCap2);
            try {
                db.SaveChanges();
                LogMgr.AddLog(User.Identity.Name, (int)FunctionType.AddLoaiCap2, "Thêm Loại cấp 2 " + loaiCap2.Name);
                thongbao = new ThongBaoMvc { CssClassName = "success", Message = "Thành công thêm mới loại cấp 2." };
                TempData["ResultAction"] = thongbao;
            }
            catch(Exception e)
            {
                thongbao = new ThongBaoMvc { CssClassName = "danger", Message = "Lỗi" };
                TempData["ResultAction"] = thongbao;
                log.Error("Lỗi thêm mới loại cấp 2: " + e.Message);
            }
            return RedirectToAction("Index");
        }
        //[HttpPost]
        //public JsonResult DelLoai2(int id)
        //{
          
        //    if (db.LoaiCap3s.FirstOrDefault(s => s.LoaiCap2Id == id) == null)
        //    {
        //        LoaiCap2 loaicap2 = db.LoaiCap2s.FirstOrDefault(s => s.LoaiCap2Id == id);
        //        db.LoaiCap2s.Remove(loaicap2);
        //        try {
        //            db.SaveChanges();
        //            LogMgr.AddLog(User.Identity.Name, (int)FunctionType.XoaLoaiCap2, "Xoá Loại cấp 2 " + loaicap2.Name);
        //            return Json(new { smg = "Xoá thành công!" });
        //        }catch(Exception e)
        //        {
        //            log.Error("Lỗi xóa loại cấp 2: " + e.Message);
        //            return Json(new { smg = "Lỗi!" });
        //        }
        //    }
        //    else
        //    {
        //        return Json(new { smg = "Không được phép xoá do có sản phẩm loại này!" });
        //    }


        //}
    }

}
