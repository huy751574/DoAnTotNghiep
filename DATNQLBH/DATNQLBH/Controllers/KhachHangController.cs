using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

using log4net;
using System.Data;
using DATNQLBH.Models;
using DATNQLBH.Models.CSDL;
using DATNQLBH.Manager;
using System.Net;
using DATNQLBH.Manager.CheckChucNangNhanVien;

namespace thuctap.Controllers
{
   
    [Authorize]
    [LogActionFilter]
    public class KhachHangController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(KhachHangController));
        private ShopEntities db;
        KiemTra kiemtra = new KiemTra();
      public KhachHangController()
        {
            kiemtra = new KiemTra();
            var user = kiemtra.getUser(kiemtra.HttpContext.User.Identity.Name);
            if(user== null)
            {
                RedirectToAction("Login", "Account");
            }
            db = ShopEntities.CreateEntitiesForSpecificDatabaseName(user.MaCN);
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        public ActionResult ListOrder()
        {          
            return View();
        }
        [Authorize(Roles = "SuperAdmin,Admin")]
        public JsonResult DanhSachDonHangDaDat()
        {
           
            List<object> lst = new List<object>();
            lst.AddRange(db.DonHangs.Select(x=>new  {
                x.Address,
                x.BuyTime,
                x.Completiontime,
                x.CustomerId,
                x.FullName,
                x.MaCN,
                x.OrderId,
                x.SDT,
                x.State,
                MaNhanVien=x.UserId,
                TenNhanVien=x.NhanVien.FullName
            }));
            return Json(new { lst }, JsonRequestBehavior.AllowGet);
        }
    
        /// <summary>
        /// Lấy chi tiết của đơn hàng
        /// </summary>
        /// <param name="Id">mã đơn hàng</param>
        /// <returns>chuỗi html của chi tiết đơn hàng</returns>
        [HttpPost]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public JsonResult GetOrderOrderDetails(int Id)
        {
            var data = db.ChiTietDonHangs.Where(s => s.OrderId == Id).OrderBy(s => s.ItemId);
            var quyCach = db.QuyCachs;
            var smg = "";
            if (data.Count() > 0)
            {
                int i = 0;
                foreach (var orderDetail in data)
                {
                    smg += "<tr><td class=\"hidden-xs\">" + (i = i + 1) + "</td><td>" + orderDetail.ItemId + "</td><td class=\"hidden-xs\">" + orderDetail.NameItem + "</td><td class=\"hidden-xs text-right\">" + orderDetail.Price.ToString("#,##0").Replace(",", ".") + "</td><td>" + orderDetail.Quantities + "</td><td class=\"hidden-xs\">" + quyCach.FirstOrDefault(s => s.QuyCachId == orderDetail.MatHang.QuyCachId).Name + "</td></tr>";
                }
            }
            return Json(new { smg });
        }
        /// <summary>
        /// cập nhật trạng thái đơn hàng
        /// </summary>
        /// <param name="Id">Mã đơn hàng</param>
        /// <param name="State">Trạng thái đơn hàng</param>
        /// <returns>chỗi html cập nhật trạng thái</returns>
        [HttpPost]
        [Authorize(Roles = "SuperAdmin,Admin")]
        [CheckCNNV(Id =(int)ChucNangNVType.ChangeStateDon)]
        public JsonResult UpdateStateOrder(int Id, int State)
        {
           
            var orderInfo = "";
            var order = db.DonHangs.FirstOrDefault(s => s.OrderId == Id);
            if (order != null)
            {
                if (order.State != 2 || order.State != 3)
                {
                    if (State > 0 && State < 4)
                    {
                        
                            order.State = State;
                            order.Completiontime = DateTime.Now;                         
                            if(order.State==(int)LapHoaDonKhach.Complete || order.State==(int)LapHoaDonKhach.Progressing)
                            {
                               
                                if (order.ChiTietDonHangs.Any(s=>s.Quantities>db.ChiTietKhoHangs.Where(x =>x.ItemId == s.ItemId).Sum(x=> x.Quantities)))
                                {
                                    orderInfo = "Số lượng trong kho không đủ để thực hiện đơn hàng";
                                }
                            }
                            if (order.State == (int)LapHoaDonKhach.Complete)
                            {
                                var khach = new KhachHang();
                                if (order.CustomerId.HasValue)
                                {
                                    khach = db.KhachHangs.Find(order.CustomerId.Value);
                                    
                                }
                                var xoa=db.ChiTietKhoHangs.Where(x => order.ChiTietDonHangs.Any(v => v.ItemId == x.ItemId));
                                var chitietkho = db.ChiTietKhoHangs.ToList();
                                foreach (var item in order.ChiTietDonHangs)
                                {
                                    var quantity = item.Quantities;
                                    var tam = chitietkho.Where(x => x.ItemId == item.ItemId);
                                    if(khach!= null)
                                    {
                                        khach.ExpVip += quantity * item.Price;
                                    }
                                    foreach(var k in tam)
                                    {
                                        if (quantity != 0)
                                        {
                                            
                                            if(k.Quantities >= quantity)
                                            {
                                                k.Quantities = k.Quantities - quantity;
                                                quantity = 0;
                                            }
                                            else 
                                            {
                                                quantity = quantity - k.Quantities;
                                                k.Quantities = 0;
                                            }
                                        }
                                    }
                                }
                               
                                  

                            }
                        try
                        {
                            db.SaveChanges();
                            if (order.State == (int)LapHoaDonKhach.Complete)
                            {
                                LogMgr.AddLog(User.Identity.Name, (int)FunctionType.CompleteInvoice, "Hoàn thành hóa đơn " + order.OrderId + " của khách hàng " + order.FullName);
                                orderInfo = "Đã hoàn thành đơn";
                            }
                            else if (order.State == (int)LapHoaDonKhach.Delete)
                            {
                                LogMgr.AddLog(User.Identity.Name, (int)FunctionType.CancelInvoice, "Hủy hóa đơn " + order.OrderId + " của khách hàng " + order.FullName);
                                orderInfo = "Đã hủy đơn";
                            }
                            else if (order.State == (int)LapHoaDonKhach.Progressing)
                            {
                                LogMgr.AddLog(User.Identity.Name, (int)FunctionType.ProgressingInvoice, "Đang thực hiện giao hóa đơn " + order.OrderId + " cho khách hàng " + order.FullName);
                                orderInfo = "Đơn bắt đầu giao";
                            }
                            
                        }
                        catch (Exception ex)
                        {
                            if (log.IsInfoEnabled)
                            {
                                log.Error("Lỗi thay đổi tình trạng sản phẩm: " + ex.Message.ToString());
                            }
                            orderInfo = "Lỗi!";
                        }
                    }
                }
                else
                {
                    orderInfo = "Không thể thay đổi thông tin đơn hàng đã thành công hoặc hủy";
                }
            }
            return Json(new { orderInfo });
        }

        /// <summary>
        /// lấy thông tin các khách hàng phù hợp với số điện thoại
        /// </summary>
        /// <param name="term">Số điện thoại</param>
        /// <returns>danh sách thông tin khách hàng</returns>
        
        public JsonResult GetIdCustomerByPhone(string term)
        {
           
         
            var results = db.KhachHangs.Where(r=>r.SDT.ToString().Contains(term.ToLower())).Select(x=>new { x.SDT,x.Address,x.FullName,x.Id }).Distinct();
            return Json(results, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// lấy thông tin đơn đặn hàng để đổi trả
        /// </summary>
        /// <param name="id">mã đơn hàng</param>
        /// <returns>thông tin đơn hàng</returns>
        //[Authorize(Roles = "SuperAdmin,Admin")]
        //public ActionResult DoiTra(int? id)
        //{
        //    if (id == null)
        //    {
        //        return RedirectToAction("ListOrder", "KhachHang");
        //    }
        //    var order = db.DonHangs.FirstOrDefault(s => s.OrderId == id);
        //    if (order == null)
        //    {
        //        return RedirectToAction("ListOrder", "KhachHang");
        //    }
         
        //    return View(order);
        //}
        ///// <summary>
        ///// Post thông tin đơn hàng đổi trả
        ///// </summary>
        ///// <param name="model">thông tin đơn hàng</param>
        ///// <returns></returns>
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[Authorize(Roles = "SuperAdmin,Admin")]
        //public ActionResult DoiTra(FormCollection form)
        //{
        //    var donhangid= Convert.ToInt32(form["OrderId"]);
        //    var donhang = db.DonHangs.FirstOrDefault(x => x.OrderId == donhangid);
        //    List<ChiTietDonHang> lstToRemove = new List<ChiTietDonHang>();
        //    foreach(var item in donhang.ChiTietDonHangs)
        //    {
        //        var chitietid= form["ChiTiet-"+item.OrderdetailId];
        //        if(chitietid!=null)
        //        {
                   
        //              var soluong = Convert.ToInt32(form["SoLuong-" + item.OrderdetailId]);
        //            item.Quantities = item.Quantities - soluong;
        //            if(item.Quantities==0)
        //            {
        //                lstToRemove.Add(item);
        //            }
        //        }
        //    }
        //    try
        //    {
        //        if(lstToRemove.Count>0)
        //            db.ChiTietDonHangs.RemoveRange(lstToRemove);
        //        db.SaveChanges();
        //    }
        //    catch(Exception e)
        //    {
        //        log.Error("Trả sản phẩm lỗi : "+e.Message);
        //    }
        //    return RedirectToAction("ListOrder");
        //}
        
        public ActionResult TabContentHoaDon(int? id)
        {
            
            ViewBag.Company = db.ChiNhanhs.FirstOrDefault();
            var donhang = new DonHang();
            if (id != null)
            {
                donhang = db.DonHangs.FirstOrDefault(x => x.OrderId == id);
                
            }
            return PartialView(donhang);
        }


        /// <summary>
        /// Thêm mới hoặc chỉnh sửa đơn đặt hàng
        /// </summary>
        /// <param name="id">mã đơn hàng</param>
        /// <returns></returns>
        ///
        /// 
        [CheckCNNV(Id = (int)ChucNangNVType.LapHoaDon)]
        public ActionResult CreateInvoice()
        {
            var user = kiemtra.getUser(User.Identity.Name);
        
            ViewBag.ListItem = db.MatHangs.Where(s => s.IsUse==true && s.ChiTietKhoHangs.Sum(x=>x.Quantities)>0);
            ViewBag.SelectLoai = new SelectList(db.LoaiCap3s, "Name", "Name");
            List<DonHang> DSDonHangLuu = db.DonHangs.Where(x => x.State == (int)LapHoaDonKhach.Waiting && x.UserId.Equals(user.Id)).ToList();
      
            return View(DSDonHangLuu);

        }
        /// <summary>
        /// lưu đơn đặt hàng
        /// </summary>
        /// <param name="model">thông tin đơn hàng</param>
        /// <returns></returns>
        [HttpPost]
        [CheckCNNV(Id = (int)ChucNangNVType.LapHoaDon)]
        public JsonResult CreateInvoice(long[] id, int[] quantity, int? khachhangid, string khachhangten, string khachhangdiachi, string khachhangsdt, int? isHad)
        {

          
            var user = db.TaiKhoans.FirstOrDefault(x=>x.UserName.Equals(User.Identity.Name));

            var dbTransaction = db.Database.BeginTransaction();
            ViewBag.ListItem = db.MatHangs.Where(s =>s.IsUse == true && s.ChiTietKhoHangs.Sum(x => x.Quantities) > 0);
            ViewBag.SelectLoai = new SelectList(db.LoaiCap3s, "Name", "Name");

            // nếu là đơn hàng cũ
            if (isHad.HasValue)
            {
                int orderid = isHad.Value;
                var donhang = db.DonHangs.FirstOrDefault(x => x.OrderId == orderid);
                try
                {
                    long tam;
                    for (int i = 0; i < id.Length; i++)
                    {
                        tam = id[i];
                        var soluong = quantity[i];
                        var sanphamcosan = donhang.ChiTietDonHangs.FirstOrDefault(x => x.ItemId == tam);
                        //Chưa có mặt hàng trong hóa đơn trước đó
                        if (sanphamcosan == null)
                        {

                            var mathang = db.MatHangs.FirstOrDefault(x => x.ItemId == tam);
                            donhang.ChiTietDonHangs.Add(new ChiTietDonHang { ItemId = mathang.ItemId, Price = mathang.Price, OrderId = donhang.OrderId, NameItem = mathang.Name, Quantities = soluong });


                        }
                        else
                        {
                            sanphamcosan.Quantities += soluong - sanphamcosan.Quantities;
                        }


                    }
                    List<long> xoa = new List<long>(); ;
                    foreach (var k in donhang.ChiTietDonHangs)
                    {

                        if (id.Count(x => x == k.ItemId) == 0)
                        {
                            xoa.Add(k.ItemId);
                        };
                    }
                    if (xoa.Count() > 0)
                    {
                        foreach (var k in xoa)
                        {
                            db.ChiTietDonHangs.Remove(db.ChiTietDonHangs.FirstOrDefault(x => x.ItemId == k && x.OrderId == donhang.OrderId));
                        }
                    }
                    donhang.Completiontime = DateTime.Now.AddDays(7);
                    db.SaveChanges();
                    LogMgr.AddLog(User.Identity.Name, (int)FunctionType.EditInvoice, "Thay đổi hóa đơn cho khách hàng " + khachhangten);
                    dbTransaction.Commit();
                    return Json(new { thongbao = "Đã cập nhật" });
                }
                catch (Exception ex)
                {
                   
                    log.Error("Thay đổi đơn hàng lỗi: " + ex.Message);
                    dbTransaction.Rollback();
                    return Json(new { thongbao = "Lỗi!" });
                }
            }
            else
            {
                int idkhach = khachhangid.HasValue ? khachhangid.Value : 0;
                DonHang dh;
                if (idkhach != 0)
                {
                    dh = new DonHang { UserId = user.Id, FullName = khachhangten, SDT = khachhangsdt, Address = khachhangdiachi, BuyTime = DateTime.Now, Completiontime = DateTime.Now.AddDays(7), State = (int)LapHoaDonKhach.Waiting, CustomerId = idkhach, MaCN = user.MaCN };
                }
                else
                {
                    dh = new DonHang { UserId = user.Id, FullName = khachhangten, SDT = khachhangsdt, Address = khachhangdiachi, BuyTime = DateTime.Now, Completiontime = DateTime.Now.AddDays(7), State = (int)LapHoaDonKhach.Waiting, MaCN = user.MaCN };

                }
                user.Experience = user.Experience + 1;
                dh.ChiTietDonHangs = new List<ChiTietDonHang>();
                long tam;
                for (int i = 0; i < id.Length; i++)
                {
                    tam = id[i];
                    var mathang = db.MatHangs.FirstOrDefault(x => x.ItemId == tam);
                    dh.ChiTietDonHangs.Add(new ChiTietDonHang { ItemId = mathang.ItemId, DonHang = dh, NameItem = mathang.Name, Quantities = quantity[i], Price = mathang.Price });
                }
                db.DonHangs.Add(dh);
                LogMgr.AddLog(User.Identity.Name, (int)FunctionType.AddInvoice, "Lập hóa đơn cho khách hàng " + khachhangten);
                try
                {

                    db.SaveChanges();
                    dbTransaction.Commit();
                }
                catch (Exception ex)
                {
             
                    log.Error("Thêm hóa đơn mới lỗi: ",ex);
                    dbTransaction.Rollback();
                    return Json(new { thongbao = "Lỗi" });
                }
                return Json(new { thongbao = "Đã thêm hóa đơn mới" });
            }   
        }
        
      
        [HttpPost]
        
        public JsonResult GetMatHangTheoSerial(string serial)
        {
            
            var mathang=db.MatHangs.FirstOrDefault(x => x.Serial.Equals(serial));
            return Json(new { ItemId = mathang.ItemId, Name = mathang.Name, Gia = mathang.Price.ToString("#,##0").Replace(",",".") });
        }


        [Authorize(Roles = "SuperAdmin,Admin")]
        public ActionResult KhachHang()
        {
            return View();
        }
        [Authorize(Roles = "SuperAdmin,Admin")]
        public JsonResult ListKhachHang()
        {
            List<object> lst = new List<object>();
            var user = kiemtra.getUser(User.Identity.Name);
            lst.AddRange(db.KhachHangs.Select(x=> new{
                x.Address,
                x.CMND,
                x.Email,
                x.FullName,
                x.Id,
                x.SDT,
                x.Vip             
            }));
            return Json(new { lst }, JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "SuperAdmin,Admin")]
        public JsonResult OrderOfCustomer(int id)
        {
            List<object> lstOrder = new List<object>();
            lstOrder.AddRange(db.DonHangs.Where(x => x.CustomerId == id).Select(x => new {
                x.OrderId,
               
                Tong=x.ChiTietDonHangs.Sum(y=>y.Price*y.Quantities),
                x.Completiontime,
                x.BuyTime,
                x.State
               
            }));
            return Json(new { lstOrder }, JsonRequestBehavior.AllowGet);
        }
        [CheckCNNV(Id = (int)ChucNangNVType.UpdateKhachHang)]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public ActionResult EditKhach(int? id)
        {
         
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang customer = db.KhachHangs.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin,Admin")]
        [CheckCNNV(Id = (int)ChucNangNVType.UpdateKhachHang)]
        public ActionResult EditKhach(KhachHang model)
        {
            ThongBaoMvc thongbao;
            db.Entry(model).State = EntityState.Modified;
            try
            {
               
                db.SaveChanges();
                LogMgr.AddLog(User.Identity.Name, (int)FunctionType.EditKhachHang, "Đã thay đổi thông tin khách hàng " + model.FullName + ": địa chỉ--" + model.Address + ", Email--" + model.Email + ", Số điện thoại--" + model.SDT);
                thongbao = new ThongBaoMvc { CssClassName = "success", Message = "Thành công." };
                TempData["ResultAction"] = thongbao;
            }
            catch(Exception e)
            {
                log.Error("Lỗi sửa thông tin khách hàng, Fail: "+e.Message);
                thongbao = new ThongBaoMvc { CssClassName = "danger", Message = "Lỗi." };
                TempData["ResultAction"] = thongbao;

            }
          
            return View(model);
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        [CheckCNNV(Id = (int)ChucNangNVType.ThemKhachHang)]
        // GET: /Create
        public ActionResult ThemKhachHang()
        {

            return View();
        }

        // POST: /Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin,Admin")]
        [CheckCNNV(Id = (int)ChucNangNVType.ThemKhachHang)]
        public ActionResult ThemKhachHang(KhachHang khach)
        {

            if (ModelState.IsValid)
            {
                ThongBaoMvc thongbao;
                khach.MaCN = kiemtra.getUser(User.Identity.Name).MaCN;
                khach.ExpVip = 0;
                khach.Vip = 0;
                db.KhachHangs.Add(khach);
                try {
                    db.SaveChanges();
                    LogMgr.AddLog(User.Identity.Name, (int)FunctionType.EditKhachHang, "Đã thêm khách hàng mới" + khach.FullName + ": địa chỉ--" + khach.Address + ", Email--" + khach.Email + ", Số điện thoại--" + khach.SDT);
                    thongbao = new ThongBaoMvc { CssClassName = "success", Message = "Thêm khách hàng mới thành công." };
                    TempData["ResultAction"] = thongbao;
                    return RedirectToAction("KhachHang");
                }
                catch(Exception e)
                {
                    log.Error("Lỗi thêm khách hàng, Fail: " + e.Message);
                    thongbao = new ThongBaoMvc { CssClassName = "danger", Message = "Lỗi." };
                    TempData["ResultAction"] = thongbao;
                }
               
            }           
            return View(khach);
        }
       

    }
}
   
