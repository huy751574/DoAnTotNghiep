using DATNQLBH.Manager;
using DATNQLBH.Manager.CheckChucNangNhanVien;
using DATNQLBH.Models;
using DATNQLBH.Models.CSDL;
using ImageResizer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;




namespace thuctap.Controllers
{
    [LogActionFilter]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class ItemController : Controller
    {
        private readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(ItemController));
        private ShopEntities db;
        KiemTra kiemtra = new KiemTra();
         
        // GET: /Item/ 
       public ItemController()
        {
            kiemtra = new KiemTra();
            var user = kiemtra.getUser(kiemtra.HttpContext.User.Identity.Name);
            db = ShopEntities.CreateEntitiesForSpecificDatabaseName(user.MaCN);
        }
      
        public ActionResult Index()
        {
           
            ViewBag.LoaiCap1 = new SelectList(db.LoaiCap1s.ToList());
            return View();
            
        }
        public JsonResult DanhSachMatHang()
        {
           

            List<object> list = new List<object>();
            list.AddRange(db.MatHangs
                .Select(item=>new
                {
                    item.AuthorName,
            
                    item.Directory,
                    item.Description,
                    item.ItemId,
                    item.Name,
                    item.Price,
                    item.Propertynames,
                    item.Reduction,
                    item.VAT,
                    item.QuyCachId,
                    item.LoaiCap3Id,
                    item.LoaiCap3.LoaiCap2Id,
                    item.LoaiCap3.LoaiCap2.LoaiCap1Id,
                    item.IsUse,
                    item.Image,
                   
                })
                .ToList());
           
           
            return Json(new { list }, JsonRequestBehavior.AllowGet);
        }

        [CheckCNNV(Id = (int)ChucNangNVType.ThemMatHang)]
        // GET: /Item/Create
        public ActionResult Create()
        {
            ViewBag.LoaiCap1 = new SelectList(db.LoaiCap1s, "LoaiCap1Id", "Name");
         
            ViewBag.QuyCachId = new SelectList(db.QuyCachs, "QuyCachId", "Name");
            //       ViewBag.MaCN = new SelectList(db.ChiNhanhs, "MaCN", "Name");

            return View();
        }

        // POST: /Item/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CheckCNNV(Id = (int)ChucNangNVType.ThemMatHang)]
        public ActionResult Create(MatHang item, HttpPostedFileBase[] files)
        {
            ThongBaoMvc thongbao;

            item.Image = "";

            var user = kiemtra.getUser(User.Identity.Name);
            if (files[0] != null && files.Length > 0)
            {
                item.Directory = user.MaCN + "/" + BoDau.ConvertToUnsign3(item.Name);

                var pathfile = Server.MapPath("~/Content/Image/") + item.Directory + "/" + Path.GetFileNameWithoutExtension(files[0].FileName);

                var job = new ImageJob(files[0], pathfile, new Instructions("model=max;format=png;width=100;height=300;"));

                job.CreateParentDirectory = true;// tạo folder nếu chưa có
                job.AddFileExtension = true;
                job.Build();

                item.Image = Path.GetFileNameWithoutExtension(files[0].FileName) + ".png";


            }

            ViewBag.LoaiCap1 = new SelectList(db.LoaiCap1s, "LoaiCap1Id", "Name");
            ViewBag.QuyCachId = new SelectList(db.QuyCachs, "QuyCachId", "Name");
            try
            {

                item.MaCN = db.TaiKhoans.FirstOrDefault(s => s.UserName.Equals(User.Identity.Name)).MaCN;
                item.IsUse = true;

                db.MatHangs.Add(item);
                db.SaveChanges();
                //Ghi log
                LogMgr.AddLog(User.Identity.Name, (int)FunctionType.AddItem, "Thêm sản phẩm " + item.Name + "-" + item.ItemId);
                thongbao = new ThongBaoMvc { CssClassName = "success", Message = "Thêm sản phẩm thành công" };
                TempData["ResultAction"] = thongbao;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                thongbao = new ThongBaoMvc { CssClassName = "danger", Message = "Lỗi" };
                TempData["ResultAction"] = thongbao;
                log.Error("Lỗi khi thêm sản phẩm", ex);
                return View(item);
            }



        }



        //         return View(item);
        [CheckCNNV(Id = (int)ChucNangNVType.UpdateMatHang)]
        public ActionResult CreateLan2(long id)
        {
            var item = db.MatHangs.FirstOrDefault(s => s.ItemId == id);
            bientam.ItemId = item.ItemId;
            if (item != null)
            {
                //cắt chuỗi trong PropertyNames để lấy chuỗi chi tiết sản phẩm
                var tt = item.LoaiCap3.PropertyNames.ToString().Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                ViewBag.Info = tt;
                var array = item.Propertynames.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                if (array.Length == tt.Length)
                {
                    ViewBag.ArrayValue = array;
                    item.Propertynames = array[0];
                }
                else
                {
                    ViewBag.ArrayValue = null;
                }
                return View(item);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        [CheckCNNV(Id = (int)ChucNangNVType.UpdateMatHang)]
        public ActionResult CreateLan2(MatHang mathang)
        {
            ThongBaoMvc thongbao;
            var item = db.MatHangs.SingleOrDefault(s => s.ItemId == bientam.ItemId);
            if (!string.IsNullOrEmpty(mathang.Propertynames))
            {
                item.Propertynames = mathang.Propertynames;
                try
                {
                    db.SaveChanges();
                    thongbao = new ThongBaoMvc { CssClassName = "success", Message = "Cập nhật chi tiết sản phẩm thành công" };
                    TempData["ResultAction"] = thongbao;
                    LogMgr.AddLog(User.Identity.Name, 5, "Cập nhập chi tiết sản phẩm " + item.Name + "-" + item.ItemId);
                    return RedirectToAction("Index", "Item");
                }
                catch (Exception ex)
                {
                    log.Error("Lỗi ở CreateLan2"+ ex.Message);
                    thongbao = new ThongBaoMvc { CssClassName = "danger", Message = "Lỗi" };
                    TempData["ResultAction"] = thongbao;
                }
            }

            var tt = item.LoaiCap3.PropertyNames.ToString().Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
            ViewBag.Info = tt;
            return View();

        }

        public JsonResult lstLoaiCap1()
        {
          
            List<LoaiCap1> list = db.LoaiCap1s.ToList();
            
           
            var text = "<div><label> Loại cấp 1:</label><select id=\"LoaiCap1\" class=\"form-control\" onchange=\"changeLoai2()\"><option value=\"\">--Chọn loại cấp 1--</option>";
            foreach (var item in list)
            {
  
                text += "<option value=\"" + item.LoaiCap1Id+"\" > " + item.Name + "</option >";
            }
            text += "</select></div>";
         
            return Json(new { text },JsonRequestBehavior.AllowGet);
        }

        public JsonResult updateLoaiCap2(int id)
        {
          
            var list = db.LoaiCap2s.Where(x => x.LoaiCap1Id == id).ToList();
            var text = "<div id=\"LoaiCap2\"><label> Loại cấp 2:</label><select id=\"LoaiCap2\" class=\"form-control\" onchange=\"changeLoai3()\"><option value=\"\">--Chọn loại cấp 2--</option>";


            foreach (var item in list)
            {
                text += "<option value=\"" + item.LoaiCap2Id + "\">" + item.Name + "</option>";
            }
            text += "</select></div>";
            return Json(new { text }, JsonRequestBehavior.AllowGet);
        }
   
    public JsonResult updateLoaiCap3(int id)
    {
          
            var list = db.LoaiCap3s.Where(x => x.LoaiCap2Id==id).ToList();
            var text = "<div id=\"LoaiCap3\"><label> Loại cấp 3:</label><select id=\"LoaiCap3\" class=\"form-control\" name=\"LoaiCap3Id\" onchange=\"timloai()\"><option value=\"\">--Chọn loại cấp 3--</option>";

             
        foreach (var item in list)
        {
            text += "<option value=\"" + item.LoaiCap3Id + "\">" + item.Name + "</option>";
        }
            text += "</select></div>";
            return Json(new { text }, JsonRequestBehavior.AllowGet);
    }
        //[HttpPost]
        //public JsonResult updateChinhanh(string id)
        //{
        //    var text = "<select class=\"form-control valid\" id=\"MaCN\" name=\"MaCN\">";
        //    var list = from s in db.ChiNhanhs
        //               where s.MaCN == id
        //               select s;
        //    foreach (var item in list)
        //    {
        //        text += "<option value=\"" + item.MaCN + "\">" + item.Name + "</option>";
        //    }
        //    text += "</select>";
        //    return Json(new { text });
        //}


        [CheckCNNV(Id = (int)ChucNangNVType.UpdateMatHang)]
        // GET: /Item/Edit/5
        public ActionResult Edit(long? id)
    {
        if (id == null)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        MatHang item = db.MatHangs.Find(id);
        if (item == null)
        {
            return HttpNotFound();
        }
        ViewBag.CategorysId = new SelectList(db.LoaiCap3s, "CategorysId", "Name", item.LoaiCap3Id);
        ViewBag.QuyCachId = new SelectList(db.QuyCachs, "QuyCachId", "Name", item.QuyCachId);
        return View(item);
    }

        // POST: /Item/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CheckCNNV(Id = (int)ChucNangNVType.UpdateMatHang)]
        public ActionResult Edit(MatHang item, HttpPostedFileBase[] files)
        {
            ThongBaoMvc thongbao;
            ViewBag.CategorysId = new SelectList(db.LoaiCap3s, "CategorysId", "Name", item.LoaiCap3Id);
            ViewBag.QuyCachId = new SelectList(db.QuyCachs, "QuyCachId", "Name", item.QuyCachId);

            var MatHangCanSua = db.MatHangs.Find(item.ItemId);
            MatHangCanSua.Name = item.Name;
            MatHangCanSua.Price = item.Price;
            MatHangCanSua.AuthorName = item.AuthorName;
            MatHangCanSua.Reduction = item.Reduction;
            MatHangCanSua.VAT = item.VAT;
            MatHangCanSua.QuyCachId = item.QuyCachId;
            MatHangCanSua.Description = item.Description;
            MatHangCanSua.Serial = item.Serial;
            if (files != null && files.Length > 0 && files[0] != null)
            {
                try
                {

                    var pathfile = Server.MapPath("~/Content/Image/") + item.Directory + "/" + Path.GetFileNameWithoutExtension(files[0].FileName);
                    var job = new ImageJob(files[0], pathfile, new Instructions("model=max;format=png;width=100;hight=300;"));
                    job.CreateParentDirectory = true;
                    job.AddFileExtension = true;
                    job.Build();
                    MatHangCanSua.Image = Path.GetFileNameWithoutExtension(files[0].FileName) + ".png";

                }
                catch (Exception ex)
                {
                    log.Error("Lỗi khi up file", ex);
                    thongbao = new ThongBaoMvc { CssClassName = "danger", Message = "Lỗi" };
                    TempData["ResultAction"] = thongbao;


                }
            }
            if (ModelState.IsValid)
            {
                try
                {

                    // item.Propertynames = db.MatHangs.FirstOrDefault(s => s.ItemId == item.ItemId).Propertynames;
                    //     db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();
                    LogMgr.AddLog(User.Identity.Name, (int)FunctionType.UpdateSanPham, "Cập nhập thông tin sản phẩm " + item.Name + "-" + item.ItemId);
                    thongbao = new ThongBaoMvc { CssClassName = "success", Message = "Cập nhật thông tin sản phẩm thành công" };
                    TempData["ResultAction"] = thongbao;
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    log.Error("Error when upload image edit", ex);
                    thongbao = new ThongBaoMvc { CssClassName = "danger", Message = "Lỗi" };
                    TempData["ResultAction"] = thongbao;


                }
            }
         
            return View(item);
        }

        public ActionResult QuyCach()
        {

            return View(db.QuyCachs.ToList());
        }
        [CheckCNNV(Id = (int)ChucNangNVType.ThemQuyCach)]
        public ActionResult CreateQuyCach()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CheckCNNV(Id = (int)ChucNangNVType.ThemQuyCach)]
        public ActionResult CreateQuyCach(QuyCach model)
        {
            ThongBaoMvc thongbao;
            model.MaCN = kiemtra.getUser(User.Identity.Name).MaCN;
            db.QuyCachs.Add(model);
            try {
                db.SaveChanges();
                thongbao = new ThongBaoMvc { CssClassName = "success", Message = "Thêm đơn vị đo mới thành công" };
                TempData["ResultAction"] = thongbao;
            }
            catch(Exception e)
            {
                thongbao = new ThongBaoMvc { CssClassName = "danger", Message = "Lỗi" };
                TempData["ResultAction"] = thongbao;
                log.Error("Lỗi khi thêm quy cách mới: " + e.Message);
            }
            return RedirectToAction("QuyCach");
        }
        [CheckCNNV(Id = (int)ChucNangNVType.UpdateQuyCach)]
        public ActionResult EditQuyCach(int id)
        {
          
            return View(db.QuyCachs.Find(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CheckCNNV(Id = (int)ChucNangNVType.UpdateQuyCach)]
        public ActionResult EditQuyCach(QuyCach model)
        {
            ThongBaoMvc thongbao;
            db.Entry(model).State = EntityState.Modified;
            try {
                db.SaveChanges();
                thongbao = new ThongBaoMvc { CssClassName = "success", Message = "Thay đổi thông tin thành công" };
                TempData["ResultAction"] = thongbao;
            }
            catch(Exception e)
            {
                log.Error("Lỗi thay đổi quy cách: " + e.Message);
                thongbao = new ThongBaoMvc { CssClassName = "danger", Message = "Lỗi" };
                TempData["ResultAction"] = thongbao;
            }
            return View(model);
        }
        protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            db.Dispose();
        }
        base.Dispose(disposing);
    }
        public ActionResult KiemKho()
        {
            return View();
        }
        public JsonResult ThongKe()
        {

            //0: tổng cộng
            //1: trả 3: doi
            //2: mua
            KiemTra kiemtra = new KiemTra();
          
            List<MatHang> itemkho = db.MatHangs.ToList();
            var ListKho = new List<object>();
            foreach (var s in itemkho)
            {
                var demo = new
                {
                    ItemId = s.ItemId,
                    Name = s.Name,
                    Link = s.Directory,
                    Image = s.Image,
                    QuyCach = s.QuyCach.Name,
                    Total = db.ChiTietKhoHangs.Where(p => p.ItemId == s.ItemId).Sum(p => (int?)p.Quantities),
                    TotalBuy = (db.CT_NhapHangs.Where(p => p.ItemId == s.ItemId && p.NhapHang.State == (int)NhapHangType.ComleteNhapHang).Sum(p => (int?)p.Quantity)),
                    TotalSell = (db.ChiTietDonHangs.Where(p => p.ItemId == s.ItemId && p.DonHang.State == (int)LapHoaDonKhach.Complete).Sum(p => (int?)p.Quantities))

                };
                ListKho.Add(demo);
            }


            return Json(new { ListKho }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        [CheckCNNV(Id = (int)ChucNangNVType.ChangeStateMatHang)]
        public JsonResult Tinhtrang(long id)
        {
          
            var item = db.MatHangs.FirstOrDefault(s => s.ItemId == id);
            if (item.IsUse == true)
            {
                item.IsUse = false;
                //"Sản phẩm không còn được sử dụng";
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                LogMgr.AddLog(User.Identity.Name, (int)FunctionType.UpdateTinhTrangSanPham, "Đã vô hiệu hóa mặt hàng " + item.Name );
                return Json(new { smg = "Sản phẩm không còn được sử dụng" });
            }
            else
            {
                item.IsUse = true;
                //"Sản phẩm bắt đầu sử dụng";
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                LogMgr.AddLog(User.Identity.Name, (int)FunctionType.UpdateTinhTrangSanPham, "Đã hữu hiệu hóa mặt hàng " + item.Name);
                return Json(new { smg = "Sản phẩm bắt đầu  sử dụng" });
            }
        }
        [HttpPost]
        [CheckCNNV(Id = (int)ChucNangNVType.XoaMatHang)]
        public JsonResult DelItem(long id)
        {
          
            var itemCTNhap = db.CT_NhapHangs.FirstOrDefault(s => s.ItemId == id);         
            var itemCTXuat = db.CT_TraHangs.FirstOrDefault(s => s.ItemId == id);
            var item = db.MatHangs.FirstOrDefault(s => s.ItemId == id);
            if (itemCTNhap!=null&&itemCTXuat!=null)
            {
                return Json(new { smg = "Sản phẩm không được xoá do có hoá đơn liên quan đến sản phẩm này!" });
            }
            else
            {
                db.MatHangs.Remove(item);
                try {
                    db.SaveChanges();
                    LogMgr.AddLog(User.Identity.Name, (int)FunctionType.XoaSanPham, "Xoá sản phẩm " + item.Name + "-" + item.ItemId);
                    return Json(new { smg = "Sản phẩm đã xoá" });
                }
                catch (Exception e)
                {
                    log.Error("Lỗi xóa sản phẩm: " + e.Message);
                    return Json(new { smg = "Lỗi khi thực hiện xóa sản phẩm" });
                }
                
               
            }


            //return Json(new { smg });

        }



    }
}
