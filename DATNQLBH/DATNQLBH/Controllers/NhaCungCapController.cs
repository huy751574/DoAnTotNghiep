using DATNQLBH.Manager;
using DATNQLBH.Manager.CheckChucNangNhanVien;
using DATNQLBH.Models;
using DATNQLBH.Models.CSDL;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace thuctap.Controllers
{
    [Authorize(Roles = "SuperAdmin,Admin")]
    [LogActionFilter]
    public class NhaCungCapController : Controller
    {
     
        public static readonly ILog log = LogManager.GetLogger(typeof(NhaCungCapController));
        private ShopEntities db;
        KiemTra kiemtra = new KiemTra();
      
        public NhaCungCapController()
        {
            kiemtra = new KiemTra();
            var user = kiemtra.getUser(kiemtra.HttpContext.User.Identity.Name);
            db = ShopEntities.CreateEntitiesForSpecificDatabaseName(user.MaCN);
        }

        // GET: /NhaCungCap/
        public ActionResult Index()
        {

            return View();


        }

        public JsonResult DanhSachNCC()
        {
            List<object> lstNCC = new List<object>();
            var NCCs = db.NhaCCs.ToList();
            foreach (var x in NCCs)
            {
                var ThanhToan = x.NhapHangs.Where(h=>h.State==(int)NhapHangType.ComleteNhapHang).Sum(s => s.CT_NhapHangs.Sum(v => (double)v.Price * (double)v.Quantity ));

                lstNCC.Add(new
                {
                    x.Address,
                    x.Email,
                    x.MaCN,
                    x.MaNCC,
                    x.Name,
                    
                    x.TaiKhoan,
                    x.SDT,
                    x.Fax,
                    ThanhToan
                });

            }


            return Json(new { lstNCC }, JsonRequestBehavior.AllowGet);
        }
        [CheckCNNV(Id = (int)ChucNangNVType.ThemNCC)]
        public ActionResult Create()
        {
            return View();
        }
        /// <summary>
        /// Thêm nhà cung cấp mới
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CheckCNNV(Id = (int)ChucNangNVType.ThemNCC)]
        public ActionResult Create(NhaCC model)
        {
            ThongBaoMvc thongbao;
            var account = kiemtra.getUser(User.Identity.Name);
            var MNCC = db.NhaCCs.SingleOrDefault(s => s.MaNCC.Equals(model.MaNCC));
            model.MaCN = account.MaCN;
            try
            {

                db.NhaCCs.Add(model);
                db.SaveChanges();
                thongbao = new ThongBaoMvc { CssClassName = "success", Message = "Thêm nhà cung cấp thành công." };
                TempData["ResultAction"] = thongbao;
                return RedirectToAction("Index", "NhaCungCap");


            }
            catch (Exception ex)
            {
                thongbao = new ThongBaoMvc { CssClassName = "danger", Message = "Lỗi." };
                TempData["ResultAction"] = thongbao;
                log.Error("Lỗi thêm nhà cung cấp mới: "+ex.Message);
                return View(model);
            }

        }
        // Get
        [CheckCNNV(Id = (int)ChucNangNVType.UpdateNCC)]
        public ActionResult Update(int? id)
        {

            if (id == null)
            {
                return RedirectToAction("Index", "NhaCungCap");
            }
            var showncc = db.NhaCCs.Find(id);
            if (showncc == null)
            {
                return RedirectToAction("Index", "NhaCungCap");
            }
            return View(showncc);
        }


        /// <summary>
        /// chỉnh sửa thông tin nhà cung cấp
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CheckCNNV(Id = (int)ChucNangNVType.UpdateNCC)]
        public ActionResult Update(NhaCC model)
        {
            ThongBaoMvc thongbao;

            try
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                thongbao = new ThongBaoMvc { CssClassName = "success", Message = "Thay đổi thông tin nhà cung cấp thành công." };
                TempData["ResultAction"] = thongbao;
                return RedirectToAction("Index", "NhaCungCap");
            }
            catch (Exception ex)
            {
                thongbao = new ThongBaoMvc { CssClassName = "danger", Message = "Lỗi." };
                TempData["ResultAction"] = thongbao;
                log.Error("Lỗi cập nhật thông tin nhà cung cấp: "+ex.Message);
                return View(model);
            }

        }
        

        /// <summary>
        /// đổi trạng thái của đơn nhập hàng
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpPost]
        [CheckCNNV(Id = (int)ChucNangNVType.ChangeStateNhapHang)]
        public JsonResult DoiTrangThai(int id, int type)
        {
            var smg = "Đơn nhập hàng không tồn tại";
           

            var DonNhapHang = db.NhapHangs.SingleOrDefault(s => s.MaNH==id);
            
            if (DonNhapHang != null)
            {
                if (DonNhapHang.State == (int)NhapHangType.ComleteNhapHang)
                {
                    smg = "Không thể thay đổi đơn nhập hàng đã hoàn thành";
                    return Json(new { smg });
                }
               
                
                if(type== (int)NhapHangType.ComleteNhapHang && DonNhapHang.State != (int)NhapHangType.DeleteNhapHang)
                {
                    List<ChiTietKhoHang> lstCTKhoHang = db.KhoHangs.FirstOrDefault(x=>x.WarehouseID== DonNhapHang.WarehouseID).ChiTietKhoHangs.ToList();
                    var lstCTNhapHang = DonNhapHang.CT_NhapHangs.Select(x => new ChiTietKhoHang { ItemId = x.ItemId, Quantities = x.Quantity, WarehouseID = DonNhapHang.WarehouseID }).ToList();
                    foreach (var item in lstCTNhapHang)
                    {
                        var mathang=lstCTKhoHang.Find(x=>x.ItemId==item.ItemId);
                        if(mathang!= null)
                        {
                            mathang.Quantities = mathang.Quantities + item.Quantities;
                        }
                        else
                        {
                            lstCTKhoHang.Add(new ChiTietKhoHang { ItemId = item.ItemId, WarehouseID = item.WarehouseID, Quantities = item.Quantities });
                        }
                    }
                  
                    DonNhapHang.DateEnd = DateTime.Now;
                  
                }
               else if (type == (int)NhapHangType.DeleteNhapHang && DonNhapHang.State!= (int)NhapHangType.ComleteNhapHang)
                {
                    

                    db.CT_NhapHangs.RemoveRange(DonNhapHang.CT_NhapHangs);
                    DonNhapHang.DateEnd = DateTime.Now;
                    db.SaveChanges();

                }
                DonNhapHang.State = type;
                try
                {
                    db.SaveChanges();
                    smg = "Cập nhập thông tin thành công!";
                    if (DonNhapHang.State == (int)NhapHangType.ComleteNhapHang)
                    {
                        LogMgr.AddLog(User.Identity.Name, (int)FunctionType.CompleteNhapHang, "Hoàn thành đơn nhập hàng " + DonNhapHang.MaNH);
                    }
                   else if (DonNhapHang.State == (int)NhapHangType.Progressing)
                    {
                        LogMgr.AddLog(User.Identity.Name, (int)FunctionType.EditNhapHang, "Đang xử lý đơn nhập hàng " + DonNhapHang.MaNH);
                    }
                   else if (DonNhapHang.State == (int)NhapHangType.DeleteNhapHang)
                    {
                        LogMgr.AddLog(User.Identity.Name, (int)FunctionType.DeleteNhapHang, "Đã hủy đơn nhập hàng " + DonNhapHang.MaNH);
                    }

                }
                catch (Exception e)
                {
                    log.Error("Update trạng thái nhập hàng fail. Error: " + e.Message);
                    smg = "Cập nhập thông tin thất bại!";
                }
               


            }
            return Json(new { smg });
        }
        /// <summary>
        /// danh sách cac mặt hàng của đơn nhập hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult ChiTietHoaDon(int id)
        {
            var data = db.CT_NhapHangs.Where(s => s.MaNH == id);
            var smg = "";
            if (data.Count() > 0)
            {
                foreach (var item in data)
                {
                    smg += "<tr><td class=\"text-left\">" + item.ItemId + "</td><td class=\"text-center\">" + item.MatHang.Name + "</td><td class=\"text-right\">" + item.VAT + "</td><td class=\"text-right\">" + item.Quantity + "</td><td class=\"text-right\">" + (item.Price.ToString("#,##0").Replace(",", ".")) + "</td><td class=\"text-right\">" + (item.Price * item.Quantity + item.Price * item.Quantity * (item.VAT / 100)).ToString("#,##0").Replace(",", ".") + " đ" + "</td></tr>";

                }

            }
            return Json(new { smg });
        }
        public JsonResult ChiTietHoaDonTraHang(int id)
        {
            var data = db.CT_TraHangs.Where(s => s.MaTH == id);
            var smg = "";
            if (data.Count() > 0)
            {
                foreach (var item in data)
                {
                    smg += "<tr><td class=\"text-left\">" + item.ItemId + "</td><td class=\"text-center\">" + item.MatHang.Name + "</td><td class=\"text-right\">" + item.Quantity + "</td><td class=\"text-right\">" + item.NhapHang.NhaCC.Name + "</td></tr>";

                }

            }
            return Json(new { smg });
        }

       
        public ActionResult NhapHang()
        {
            
            ViewBag.MaNCC = new SelectList(db.NhaCCs.ToList(), "MaNCC", "Name");
            ViewBag.Kho = new SelectList(db.KhoHangs.ToList(), "WarehouseID", "Name");
            
            

            return View();

        }

 
       
        /// <summary>
        /// danh sách các đơn nhập hàng
        /// </summary>
        /// <param name="Begindate"></param>
        /// <param name="Enddate"></param>
        /// <returns></returns>
        public ActionResult DanhSachNhapHang()
        {
         
            return View();
            //var listdanhsach = db.NhapHangs.ToList();
            //return View(listdanhsach);
        }
        public JsonResult DSNhapHang()
        {
         
            List<object> listOrder = new List<object>();
            var list = db.NhapHangs.ToList();
            foreach (var item in list)
            {
                var tongtien = item.CT_NhapHangs.Sum(s => (double)s.Price * (double)s.Quantity + (double)(s.Price * s.Quantity * s.VAT) / 100);
                listOrder.Add(new
                {
                    item.MaNH,
                    item.Name,
                    item.Date,
                    item.DateEnd,
                    item.NhaCC.MaNCC,
                    TenNCC = item.NhaCC.Name,
                    NguoiLap = item.TaiKhoan.FullName,
                    item.UserId,
                    item.State,
                    tongtien
                });
            }
            return Json(new { listOrder }, JsonRequestBehavior.AllowGet);
        }
        [CheckCNNV(Id = (int)ChucNangNVType.ThemNhapHang)]
        public JsonResult ThemPhieuNhapHang(string ten, int kho, int mancc, long[] arraysp, int[] arraysl, int[] arraydg, int[] arrayvat )
        {
            var msg = 1;
            NhapHang nh = new NhapHang();
            var UserLogin = kiemtra.getUser(User.Identity.Name);
          
            
            nh.Name = ten;
            nh.UserId = UserLogin.Id;
            nh.State = (int)NhapHangType.Waiting;
            nh.MaNCC = mancc;
            nh.Date = DateTime.Now;
            nh.DateEnd = nh.Date.AddDays(7);
            nh.MaCN = UserLogin.MaCN;
            nh.WarehouseID = kho;
            nh.CT_NhapHangs = new List<CT_NhapHang>();
            for (int i = 0; i < arraysp.Length; i++)
            {
                nh.CT_NhapHangs.Add(new CT_NhapHang() { ItemId = arraysp[i], Price = arraydg[i], Quantity = arraysl[i], VAT = arrayvat[i] });
            }

            db.NhapHangs.Add(nh);
            try {
                db.SaveChanges();
            }
            catch(Exception e)
            {
                log.Error("Lỗi thêm phiếu nhập hàng: " + e.Message);
            }
            return Json(new { msg });
        }

   
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult DanhSachTraHang()
        {

            return View();
        }
      
        public JsonResult DanhSachDaTra(int? id)
        {

          
            List<object> Danhsach = new List<object>();
            Danhsach.AddRange(db.TraHangs.Select(x => new
            {
                x.MaTH,
                x.Name,
                x.Date,
                x.DateEnd,
                x.State,
                NguoiLap = x.TaiKhoan.FullName


            }));
            return Json(new { Danhsach }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        [CheckCNNV(Id = (int)ChucNangNVType.ChangeStateTraHang)]
        public JsonResult DoiTrangThaiTraHang(int id, int type)
        {
            var smg = "Đơn trả hàng không tồn tại";


            var DonTraHang = db.TraHangs.SingleOrDefault(s => s.MaTH == id);

            if (DonTraHang != null)
            {
                if (DonTraHang.State == 2)
                {
                    smg = "Không thể thay đổi đơn trả hàng đã hoàn thành";
                    return Json(new { smg });
                }
                if (DonTraHang.State == 3)
                {
                    smg = "Không thể thay đổi đơn trả hàng đã hủy";
                    return Json(new { smg });
                }
                DonTraHang.State = type;

                if (type == (int)NhapHangType.DeleteNhapHang)
                {
                    foreach(var item in DonTraHang.CT_TraHangs)
                    {
                        var chitietnhap=db.CT_NhapHangs.FirstOrDefault(x => x.MaNH == item.MaNH && x.ItemId == item.ItemId);
                        chitietnhap.Quantity = chitietnhap.Quantity + item.Quantity;
                    }
                    db.CT_TraHangs.RemoveRange(DonTraHang.CT_TraHangs);
                    DonTraHang.DateEnd = DateTime.Now;
                    db.SaveChanges();

                }
                try
                {
                    db.SaveChanges();
                    smg = "Cập nhập thông tin thành công!";
                    if (DonTraHang.State == (int)TraHangType.ComleteTraHang)
                    {
                        LogMgr.AddLog(User.Identity.Name, (int)FunctionType.CompleteTraHang, "Hoàn thành đơn trả hàng " + DonTraHang.Name);
                    }
                   
                    else if (DonTraHang.State == (int)TraHangType.DeleteTraHang)
                    {
                        LogMgr.AddLog(User.Identity.Name, (int)FunctionType.DeleteTraHang, "Đã hủy đơn trả hàng " + DonTraHang.Name);
                    }

                }
                catch (Exception e)
                {
                    log.Error("Lỗi cập nhật trạng thái trả hảng: " + e.Message);
                    smg = "Cập nhập thông tin thất bại!";
                }



            }
            return Json(new { smg });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult TraHang()
        {
           
            return View();
        }

        public JsonResult DanhSachCoTheTra()
        {
         
            List<object> Danhsach = new List<object>();
            Danhsach.AddRange(db.CT_NhapHangs
                .Where(x => x.NhapHang.State == (int)NhapHangType.Waiting || x.NhapHang.State == (int)NhapHangType.Progressing)
                .Select(x => new
                {
                    x.MaNH,
                    x.ItemId,
                    x.NhapHang.Name,
                    SanPham=x.MatHang.Name,
                    TenNguoiLap=x.NhapHang.TaiKhoan.FullName,
                    TenNhaCC= x.NhapHang.NhaCC.Name,
                    MaNhaCC=x.NhapHang.MaNCC,
                    x.Price,
                    x.VAT,
                    x.Quantity,
                    NgayKetThuc=x.NhapHang.DateEnd,
                    NgayLap=x.NhapHang.Date
                   
                })
                );
            return Json(new { Danhsach }, JsonRequestBehavior.AllowGet);
        }
        
     
        [HttpPost]
        public JsonResult ChiTietDonTraHang(int id)
        {
            var data = db.CT_TraHangs.Where(s => s.MaTH == id);
            var smg = "";
            if (data.Count() > 0)
            {
                foreach (var item in data)
                {
                    smg += "<tr><td class=\"text-left\">" + item.ItemId + "</td><td class=\"text-center\">" + item.MatHang.Name + "</td><td class=\"text-right\">" + item.Quantity + "</td><td class=\"text-left\">" + item.NhapHang.NhaCC.Name + "</td></tr>";

                }

            }
            return Json(new { smg });
        }
        /// <summary>
        /// lưu thông tin đơn trả hàng
        /// </summary>
        /// <param name="nhaphang"></param>
        /// <returns></returns>
        [HttpPost]
        [CheckCNNV(Id = (int)ChucNangNVType.ThemTraHang)]
        public JsonResult ThemPhieuTraHang(string ten, long[] arrayitemid, int[] arraysl, int[] arraymanh)
        {
            
                var user = kiemtra.getUser(User.Identity.Name);


            try
            {
                TraHang a = new TraHang();

                a.Name = ten;
                a.Date = DateTime.Now;
                a.DateEnd = DateTime.Now.AddDays(7);
                a.State = (int)TraHangType.Waiting;
                a.UserId = user.Id;
                a.MaCN = user.MaCN;
                a.CT_TraHangs = new List<CT_TraHang>();
                for (int i = 0; i < arrayitemid.Length; i++)
                {
                    var NH = db.NhapHangs.Find(arraymanh[i]);
                    var sanphamnhap = NH.CT_NhapHangs.FirstOrDefault(x => x.ItemId == arrayitemid[i]);
                    sanphamnhap.Quantity = sanphamnhap.Quantity - arraysl[i];
                    a.CT_TraHangs.Add(new CT_TraHang { ItemId = arrayitemid[i], Quantity = arraysl[i], MaNH = arraymanh[i] });

                }

                db.TraHangs.Add(a);
                db.SaveChanges();
                LogMgr.AddLog(User.Identity.Name, (int)FunctionType.AddTraHang, "Lập đơn trả hàng " + a.MaTH);
                return Json(new { msg = "Đã thêm đơn trả hàng" });

            }

            catch (Exception ex)
            {

                log.Error("Lỗi thêm phiếu trả hàng: " + ex.Message);
                return Json(new { msg = "Lỗi, không thêm được đơn trả hàng" });
            }
            
            
        }
    }
}