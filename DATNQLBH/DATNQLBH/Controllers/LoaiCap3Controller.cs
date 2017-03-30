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
    public class LoaiCap3Controller : Controller
    {
        private ShopEntities db;
        KiemTra kiemtra = new KiemTra();
        private static readonly ILog log = LogManager.GetLogger(typeof(LoaiCap3Controller));

        public LoaiCap3Controller()
        {
            kiemtra = new KiemTra();
            var user = kiemtra.getUser(kiemtra.HttpContext.User.Identity.Name);
            db = ShopEntities.CreateEntitiesForSpecificDatabaseName(user.MaCN);
        }
        public ActionResult Index()
        {
            ViewBag.MaLoaiCap2 = new SelectList(db.LoaiCap2s, "LoaiCap2Id", "Name");
            ViewBag.ChiTietSanPham = new SelectList(db.LoaiCap3s, "LoaiCap3Id", "Name");
          
            var loaiCap3 = db.LoaiCap3s.Include(l => l.LoaiCap2);
            return View(loaiCap3.ToList());
        }


        [CheckCNNV(Id = (int)ChucNangNVType.UpdateLoaiCap3)]
        // GET: LoaiCap3/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiCap3 loaiCap3 = db.LoaiCap3s.Find(id);
            if (loaiCap3 == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaCN = new SelectList(db.ChiNhanhs, "MaCN", "Name", loaiCap3.MaCN);
            ViewBag.LoaiCap2Id = new SelectList(db.LoaiCap2s, "LoaiCap2Id", "Name", loaiCap3.LoaiCap2Id);
            return View(loaiCap3);
        }

        // POST: LoaiCap3/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LoaiCap3 loaiCap3)
        {
            ViewBag.MaCN = new SelectList(db.ChiNhanhs, "MaCN", "Name", loaiCap3.MaCN);
            ViewBag.LoaiCap2Id = new SelectList(db.LoaiCap2s, "LoaiCap2Id", "Name", loaiCap3.LoaiCap2Id);
            if (ModelState.IsValid)
            {
                ThongBaoMvc thongbao;
                db.Entry(loaiCap3).State = EntityState.Modified;
                try {
                    db.SaveChanges();
                    LogMgr.AddLog(User.Identity.Name, (int)FunctionType.UpdateLoaiCap3, "Update Loại Cấp 3 " + loaiCap3.Name);
                    thongbao = new ThongBaoMvc { CssClassName = "success", Message = "Thành công thay đổi thông tin loại cấp 3." };
                    TempData["ResultAction"] = thongbao;
                    return RedirectToAction("Index");
                }
                catch(Exception e)
                {
                    thongbao = new ThongBaoMvc { CssClassName = "danger", Message = "Lỗi." };
                    TempData["ResultAction"] = thongbao;
                    log.Error("Lỗi thay đổi thông tin loại cấp 3: " + e.Message);
                }
            }
            
            return View(loaiCap3);
        }

        // GET: LoaiCap3/Delete/5
     
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost]
        [CheckCNNV(Id = (int)ChucNangNVType.ThemLoaiCap3)]
        public ActionResult Capnhap(string NameCap3, int MaLoaiCap2)
        {
            ThongBaoMvc thongbao;
            LoaiCap3 loaicap3 = new LoaiCap3();
            loaicap3.LoaiCap3Id = 0;
            loaicap3.Name = NameCap3;
            loaicap3.LoaiCap2Id = MaLoaiCap2;
            loaicap3.PropertyNames = "";
            loaicap3.MaCN= db.TaiKhoans.FirstOrDefault(s => s.UserName == User.Identity.Name).MaCN;
            db.LoaiCap3s.Add(loaicap3);
            try {
                db.SaveChanges();
                LogMgr.AddLog(User.Identity.Name, (int)FunctionType.AddLoaiCap3, "Thêm Loại cấp 3 " + loaicap3.Name);
                thongbao = new ThongBaoMvc { CssClassName = "success", Message = "Thêm mới loại cấp 3 thành công." };
                TempData["ResultAction"] = thongbao;

            }
            catch(Exception e)
            {
                log.Error("Lỗi thêm loại cấp 3: " + e.Message);
                thongbao = new ThongBaoMvc { CssClassName = "danger", Message = "Lỗi." };
                TempData["ResultAction"] = thongbao;
            }
            return RedirectToAction("Index");

        }
        
       
        [HttpPost]
        public JsonResult ShowLoai(int? id)
        {
            var text = "";
            int dem=0;

            if (id != null)
            {
                var item = db.LoaiCap3s.FirstOrDefault(s => s.LoaiCap3Id == id);

                //Cắt chuỗi
                var chuoi = item.PropertyNames.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                dem = chuoi.Length;

                if (item != null)
                {
                    foreach (var s in chuoi)
                    {
                            text += "<input type=\"text\" class=\"input\" id=\"tb" + dem + "\" value=\"" + s + "\" style=\"width:300px;\"  /><br>";
                            dem--;
                    }
                }


            }
            return Json(new { text});


        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateChuoi(int id, string chuoichitiet)
        {
            ThongBaoMvc thongbao;
            LoaiCap3 loaicap3 = db.LoaiCap3s.FirstOrDefault(s => s.LoaiCap3Id == id);

            loaicap3.PropertyNames = chuoichitiet;
            try {
                db.SaveChanges();
                LogMgr.AddLog(User.Identity.Name, (int)FunctionType.UpdateChitietSanPhamLoaiCap3, "Update mô tả chi tiết sản phẩm tại Loai Cấp 3 " + loaicap3.Name);
                thongbao = new ThongBaoMvc { CssClassName = "success", Message = "Cập nhật mô tả chi tiết sản phẩm của loại cấp 3 thành công." };
                TempData["ResultAction"] = thongbao;
            } catch(Exception e)
            {
                thongbao = new ThongBaoMvc { CssClassName = "danger", Message = "Lỗi." };
                TempData["ResultAction"] = thongbao;
                log.Error("Lỗi cập nhật mô tả chi tiết loại cấp 3: " + e.Message);
            }
                return RedirectToAction("Index");
        }
   
        [HttpPost]
        public JsonResult DelLoai3(int id)
        {
            if (db.MatHangs.FirstOrDefault(s => s.LoaiCap3Id == id) == null)
            {
                LoaiCap3 loaicap3 = db.LoaiCap3s.FirstOrDefault(s => s.LoaiCap3Id == id);
                db.LoaiCap3s.Remove(loaicap3);
                try {
                    db.SaveChanges();
                    LogMgr.AddLog(User.Identity.Name, (int)FunctionType.XoaLoaiCap3, "Xoá Loại Cấp 3 " + loaicap3.Name);
                    return Json(new { smg = "Xoá thành công!" });
                }catch(Exception e)
                {
                    log.Error("Lỗi xóa loại cấp 3: " + e.Message);
                    return Json(new { smg = "Lỗi!" });
                }
            }
            else
            {
                return Json(new { smg = "Không được phép xoá do có sản phẩm loại này!" });
            }


        }
    }
}
