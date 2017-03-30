using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

using System.Web.Mvc;


using System.Globalization;
using System.IO;

using DATNQLBH.Manager;

using DATNQLBH.Models.CSDL;
using DATNQLBH.Models;
using log4net;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.Web.Security;
using System.Security.Claims;

namespace thuctap.Controllers
{
    [Authorize(Roles = "SuperAdmin,Admin")]
    [LogActionFilter]
    public class UserController : Controller
    {
        private ShopEntities db;
        private KiemTra kiemtra=new KiemTra();
        private static readonly ILog log = LogManager.GetLogger(typeof(UserController));
       
        public UserController()
        {

            kiemtra = new KiemTra();
            
            db =new ShopEntities();
        }
        public ActionResult Index()
        {
            return RedirectToAction("NhanVien");
        }

      
        public ActionResult NhanVien()
        {
            return View();
        }
       
        public JsonResult DanhSachNhanVien()
        {
            
           
            var user = kiemtra.getUser(User.Identity.Name);
            List<object> lst = new List<object>();
           
            if (User.IsInRole("Admin"))
            {
                db = ShopEntities.CreateEntitiesForSpecificDatabaseName(user.MaCN);
                lst.AddRange(db.TaiKhoans.Select(h => new
                {
                    h.Id,
                    h.UserName,
                    h.FullName,
                    h.Address,
                    h.Birthday,
                    h.Email,
                    h.Luong,
                    h.NgayDangKy,
                    h.PhoneNumber,
                    h.MaCN,
                    h.ChiNhanh.Name,
                    h.CMND,
                    Role = "Nhân viên",
                    h.Active,
                    h.Experience
                }));
            }
            else if (User.IsInRole("SuperAdmin"))
            {
                var userstore = new UserStore<ApplicationUser>(db);
                var userManager = new UserManager<ApplicationUser>(userstore);
                db = new ShopEntities();
                string macn = user.MaCN.Split('-')[0];
                var taikhoans = db.TaiKhoans.Where(x => x.MaCN.Contains(macn)).ToList();
                List<ApplicationUser> TaiKhoanNV = new List<ApplicationUser>();
                List<ApplicationUser> TaiKhoanAdmin = new List<ApplicationUser>();
                foreach (var item in taikhoans)
                {
                    if(userManager.IsInRole(item.Id,"Staff"))
                    {
                        TaiKhoanNV.Add(item);
                    }
                    else if (userManager.IsInRole(item.Id, "Admin"))
                    {
                        TaiKhoanAdmin.Add(item);
                    }
                }
                lst.AddRange(TaiKhoanNV.Select(h => new
                {
                    h.Id,
                    h.UserName,
                    h.FullName,
                    h.Address,
                    h.Birthday,
                    h.Email,
                    h.Luong,
                    h.NgayDangKy,
                    h.PhoneNumber,
                    h.MaCN,
                    h.ChiNhanh.Name,
                    h.CMND,
                    Role = "Nhân viên",
                    h.Active,
                    h.Experience
                }));
                lst.AddRange(TaiKhoanAdmin.Select(h => new {
                    h.Id,
                    h.UserName,
                    h.FullName,
                    h.Address,
                    h.Birthday,
                    h.Email,
                    h.Luong,
                    h.NgayDangKy,
                    h.PhoneNumber,
                    h.MaCN,
                    h.ChiNhanh.Name,
                    h.CMND,
                    Role="Admin",
                    h.Active,
                    h.Experience
                }));

            }
            
            return Json(new { lst }, JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "SuperAdmin")]
        public JsonResult ListAdmin()
        {
            List<object> lst = new List<object>();
            var rolestore = new RoleStore<IdentityRole>(db);
            var roleManager = new RoleManager<IdentityRole>(rolestore);
            var roleAdmin = roleManager.Roles.FirstOrDefault(x => x.Name.Equals("Admin"));
            string macn = "ChiNhanh";
            lst.AddRange(db.TaiKhoans.Where(x => x.Roles.Any(y => y.Id.Equals(roleAdmin.Id)) && x.MaCN.Contains(macn)).Select(h => new {
                h.Id,
                h.UserName,
                h.FullName,
                h.Address,
                h.Birthday,
                h.Email,
                h.Luong,
                h.NgayDangKy,
                h.PhoneNumber,
                h.MaCN,
                h.ChiNhanh.Name,
                h.CMND,
                Role = "Admin",
                h.Active,
                h.Experience
            }));
            return Json(new { lst }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ThemNhanVien()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> ThemNhanVien(ApplicationUser nhanvien)
        {
            ThongBaoMvc thongbao;
           
            var userstore = new UserStore<ApplicationUser>(db);
            var userManager = new UserManager<ApplicationUser>(userstore);
            nhanvien.MaCN = kiemtra.getUser(User.Identity.Name).MaCN;
            nhanvien.NgayDangKy = DateTime.Now;
            nhanvien.Experience = 0;
            nhanvien.Active = true;
            try
            {
                var result = await userManager.CreateAsync(nhanvien, nhanvien.PasswordHash);
                userManager.AddToRole(nhanvien.Id, "Staff");
                if (result.Succeeded)
                {
                    var user = kiemtra.getUser(User.Identity.Name);
                    db = ShopEntities.CreateEntitiesForSpecificDatabaseName(user.MaCN);
                    userstore = new UserStore<ApplicationUser>(db);
                    userManager = new UserManager<ApplicationUser>(userstore);
                    result = await userManager.CreateAsync(nhanvien, nhanvien.PasswordHash);
                    if (result.Succeeded)
                    {
                        thongbao = new ThongBaoMvc { CssClassName = "success", Message = "Thêm nhân viên mới thành công." };
                        TempData["ResultAction"] = thongbao;
                        LogMgr.AddLog(User.Identity.Name, (int)FunctionType.AddNhanVien, "Đã thêm nhân viên mới " + nhanvien.FullName);
                    }
                    else
                    {
                        thongbao = new ThongBaoMvc { CssClassName = "warning", Message = "Có vấn đề trong quá trình thêm nhân viên mới." };
                        TempData["ResultAction"] = thongbao;
                        log.Error("Lỗi thêm nhân viên quá trình: " + result.Errors);
                    }
                }
                else
                {
                    thongbao = new ThongBaoMvc { CssClassName = "warning", Message = "Có vấn đề trong quá trình thêm nhân viên mới." };
                    TempData["ResultAction"] = thongbao;
                    log.Error("Lỗi thêm nhân viên quá trình: " + result.Errors);
                }
            }
            catch (Exception e)
            {
                log.Error("Lỗi thêm nhân viên: " + e.Message);
                thongbao = new ThongBaoMvc { CssClassName = "danger", Message = "Lỗi." };
                TempData["ResultAction"] = thongbao;
            }
            return RedirectToAction("NhanVien");


        }
        public ActionResult EditNhanVien(string UserId)
        {
            var user = kiemtra.getUser(User.Identity.Name);
            db = ShopEntities.CreateEntitiesForSpecificDatabaseName(user.MaCN);
            var h = db.TaiKhoans.FirstOrDefault(x => x.Id.Equals(UserId));
            return View(h);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditNhanVien(ApplicationUser model)
        {
            ThongBaoMvc thongbao;
            var user = kiemtra.getUser(User.Identity.Name);
            db = ShopEntities.CreateEntitiesForSpecificDatabaseName(user.MaCN);
            var nhanvien = db.TaiKhoans.FirstOrDefault(x => x.Id.Equals(model.Id));
            nhanvien.Luong = model.Luong;
            try
            {
                db.SaveChanges();
                thongbao = new ThongBaoMvc { CssClassName = "success", Message = "Thay đổi thông tin thành công." };
                TempData["ResultAction"] = thongbao;
                return RedirectToAction("NhanVien");
            }
            catch(Exception e)
            {
                log.Error("Lỗi thay đổi thông tin nhân viên: " + e.Message);
                thongbao = new ThongBaoMvc { CssClassName = "danger", Message = "Lỗi." };
                TempData["ResultAction"] = thongbao;
            }           
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CapQuyen(FormCollection form)
        {
            ThongBaoMvc thongbao;
            var userstore = new UserStore<ApplicationUser>(db);
            var userManager = new UserManager<ApplicationUser>(userstore);
            var rolestore = new RoleStore<IdentityRole>(db);
            var roleManager = new RoleManager<IdentityRole>(rolestore);
            var roleStaff = roleManager.FindByName("Staff");
            var roleAdmin = roleManager.FindByName("Admin");

            var username = form["ten_taikhoan"];
            var user = db.TaiKhoans.FirstOrDefault(x => x.UserName.Equals(username));
            var quyencap = Convert.ToInt32(form["quyen_taikhoan"]);
            if(quyencap==0)
            {
                user.Active = false;
                thongbao = new ThongBaoMvc { CssClassName = "warning", Message = "Vô hiệu thành công nhân viên "+user.FullName };
                TempData["ResultAction"] = thongbao;
                
            }
            else if (quyencap == 3)
            {
                user.Active = true;
              
                if (!user.Roles.Any(x=>x.Id.Equals(roleStaff.Id)) && User.IsInRole("SuperAdmin"))
                {
                    if(user.Roles.Any(x => x.Id.Equals(roleAdmin.Id)))
                    {
                        userManager.RemoveFromRole(user.Id, "Admin");
                    }
                    userManager.AddToRole(user.Id, "Staff");
                }
                thongbao = new ThongBaoMvc { CssClassName = "success", Message = user.FullName+" đã trở thành nhân viên" };
                TempData["ResultAction"] = thongbao;
            }
            else if(quyencap ==2 )
            {
                if(User.IsInRole("SuperAdmin"))
                {
                    user.Active = true;
                    if (!user.Roles.Any(x => x.Id.Equals(roleAdmin.Id)))
                    {
                        if (user.Roles.Any(x => x.Id.Equals(roleStaff.Id)))
                        {
                            userManager.RemoveFromRole(user.Id, "Staff");
                        }
                        userManager.AddToRole(user.Id, "Admin");
                    }
                    thongbao = new ThongBaoMvc { CssClassName = "success", Message = user.FullName + " đã trở thành Admin" };
                    TempData["ResultAction"] = thongbao;
                }
            }
            try {
                db.SaveChanges();
            }
            catch(Exception e)
            {
                thongbao = new ThongBaoMvc { CssClassName = "danger", Message = "Lỗi"};
                TempData["ResultAction"] = thongbao;
                log.Error("Cấp quyền lỗi :" + e.Message);
                return View();
            }

            return RedirectToAction("NhanVien");
        }
        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public JsonResult DoiChiNhanh(string UserName, string MaCN)
        {
            int result = 0;
            var admin = kiemtra.getUser(UserName);
            admin.MaCN = MaCN;
            try
            {
                db.SaveChanges();
                result = 1;
            }
            catch (Exception e)
            {
                log.Error("Đổi chi nhánh cho admin công ty lỗi: " + e.Message);
                return Json(new { result });
            }

            return Json(new { result });
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        public JsonResult AddChiNhanh(string UserName)
        {
            int result = 0;
            var admin = kiemtra.getUser(UserName);
            var chinhanh = new ChiNhanh { MaCN = "ChiNhanh-" + admin.UserName, SoTaiKhoan = "0", Name = "HUTECH", Email = "h", DiaChi = "Đang cập nhật", SDT = "Đang cập nhật", Date = DateTime.Now };
            db.ChiNhanhs.Add(chinhanh);
            try
            {
                db.SaveChanges();
                admin.MaCN = "ChiNhanh-" + admin.UserName;
                db = ShopEntities.CreateEntitiesForSpecificDatabaseName(chinhanh.MaCN);
                db.Database.CreateIfNotExists();
                db.ChiNhanhs.Add(chinhanh);
                db.SaveChanges();
                result = 1;
            }
            catch (Exception e)
            {
                log.Error("Đổi chi nhánh cho admin công ty lỗi: " + e.Message);
                return Json(new { result });
            }

            return Json(new { result });
        }

        public ActionResult HoSo()
        {
            var user = kiemtra.getUser(User.Identity.Name);
            return View(user);
        }

        [HttpPost]
        public ActionResult HoSo(ApplicationUser user)
        {
            ThongBaoMvc thongbao;
            var taikhoan = kiemtra.getUser(User.Identity.Name);
            taikhoan.Address = user.Address;
            if (user.Birthday != null)
                taikhoan.Birthday = user.Birthday;          
            taikhoan.CMND = user.CMND;
            taikhoan.Email = user.Email;
            taikhoan.FullName = user.FullName;
            taikhoan.PhoneNumber = user.PhoneNumber;
            try
            {
                db.SaveChanges();
                db = ShopEntities.CreateEntitiesForSpecificDatabaseName(taikhoan.MaCN);
                var nv = db.TaiKhoans.FirstOrDefault(x => x.UserName.Equals(taikhoan.UserName));
                nv.Address = user.Address;
                if (user.Birthday != null)
                    nv.Birthday = user.Birthday;
                nv.CMND = user.CMND;
                nv.Email = user.Email;
                nv.FullName = user.FullName;
                nv.PhoneNumber = user.PhoneNumber;
                db.SaveChanges();
                thongbao = new ThongBaoMvc { CssClassName = "success", Message = "Đã thay đổi thông tin hồ sơ cá nhân thành công" };
                TempData["ResultAction"] = thongbao;
               
            }
            catch(Exception e)
            {
                thongbao = new ThongBaoMvc { CssClassName = "danger", Message = "Lỗi" };
                TempData["ResultAction"] = thongbao;
                log.Error("Thay đổi thông tin hồ sơ cá nhân lỗi :" + e.Message);
                return View(taikhoan);
            }
            return View(taikhoan);

        }

        public ActionResult FeedBack()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FeedBack(PhanHoi feedback)
        {
            ThongBaoMvc thongbao;
            var user = kiemtra.getUser(User.Identity.Name);
            db.PhanHois.Add(new PhanHoi { Comments = feedback.Comments, Datecomments = DateTime.Now, UserID = user.Id });
            try {
                db.SaveChanges();
                thongbao = new ThongBaoMvc { CssClassName = "success", Message = "Chúng tôi đã nhận phản hồi từ bạn! Cảm ơn đã sử dụng phần mềm của chúng tôi" };
                TempData["ResultAction"] = thongbao;
            }
            catch (Exception e)
            {
                thongbao = new ThongBaoMvc { CssClassName = "danger", Message = "Lỗi" };
                TempData["ResultAction"] = thongbao;
                log.Error("Có lỗi trong quá trình gửi phản hồi! Mong bạn thử lại :" + e.Message);
                return View(feedback);
            }
            return View(feedback);
        }
        [Authorize(Roles = "SuperAdmin")]
        [ChildActionOnly]
        public ActionResult ListFeedBack()
        {
            var list = db.PhanHois.ToList();
            return View(list);
        }
        [Authorize(Roles = "SuperAdmin")]
        public ActionResult FeedBackComment(int id)
        {
            var comment = db.PhanHois.Find(id);
            return View(comment);
        }
        public ActionResult CaiDat()
        {
            var lst = db.LoaiGianHangs.ToList();
            return View(lst);
        }

        public ActionResult GiaoDien()
        {
            var lst = db.GiaoDiens.ToList();
            return PartialView(lst);
        }
        [HttpPost]
        public JsonResult GiaoDien(int id, string descript)
        {
            var gd = db.GiaoDiens.Find(id);
            gd.Description = descript;
            try {
                db.SaveChanges();
                return Json(new { data = 1 });
            }catch(Exception e)
            {
                log.Error("Lỗi trong quá trình thay đổi sologan giao diện:" + e.Message);
                return Json(new { data = 0 });
            }
        }
       
  


        [ChildActionOnly]
        public ActionResult Goi(int MaLoai)
        {
            var goi = db.LoaiGianHangs.Find(MaLoai);
            return View(goi);
        }

        [HttpPost]
        public JsonResult SuaGoi(int MaLoai,string Name, int Price,int TimeUsing, bool[] arrayIsActive, int?[] arrayQuantity)
        {
            var Loai = db.LoaiGianHangs.Find(MaLoai);
            if(Loai!=null)
            {
                int i = 0;
                Loai.Name = Name;
                Loai.Price = Price;
                Loai.TimeUsing = TimeUsing;
                foreach(var item in Loai.CT_ChucNang)
                {
                    item.IsActive = arrayIsActive[i];
                    item.Quantity = arrayQuantity[i];
                    i++;
                }
                try
                {
                    db.SaveChanges();
                    return Json(new { data=1});
                }catch(Exception e)
                {
                    log.Error("Lỗi trong quá trình sửa gói chức năng"+e.Message);
                    return Json(new { data = 0 });
                }
            }
            return Json(new { data = 0 });
        }

        
       
       public JsonResult chuyentheme(string theme)
        {
            var user = kiemtra.getUser(User.Identity.Name);
     
            var userstore = new UserStore<ApplicationUser>(db);
            var userManager = new UserManager<ApplicationUser>(userstore);
            var userid = db.TaiKhoans.FirstOrDefault(x=>x.UserName.Equals(user.UserName)).Id;
            var claim=userManager.GetClaims(userid).FirstOrDefault(x => x.Type.Equals("MyApp:ThemeUrl"));
            if(claim!=null)
                userManager.RemoveClaim(userid, claim);
            userManager.AddClaim(userid,new Claim("MyApp:ThemeUrl", theme));
            return Json(new object{  });
        }
        
        public ActionResult PhanQuyen(string username)
        {
            var user = kiemtra.getUser(User.Identity.Name);
            db = ShopEntities.CreateEntitiesForSpecificDatabaseName(user.MaCN);
            var rolestore = new RoleStore<IdentityRole>(db);
            var roleManager = new RoleManager<IdentityRole>(rolestore);
        
            var userstore = new UserStore<ApplicationUser>(db);
            var userManager = new UserManager<ApplicationUser>(userstore);
            var account =userManager.FindByName(username);
            var ChucNang = db.Quyens.ToList();

            List<Quyen> quyens = new List<Quyen>();
            foreach(var item in ChucNang)
            {
                Quyen s = new Quyen();
                s.ChiTietQuyens = new List<ChiTietQuyen>();
                s.Name = item.Name;
                s.IsActive = userManager.IsInRole(account.Id, item.Name);
                if(item.ChiTietQuyens.Count>0)
                {
                    foreach (var chitietq in item.ChiTietQuyens)
                    {
                        s.ChiTietQuyens.Add(chitietq);
                    }
                }
              
                quyens.Add(s);
            }
            ViewBag.TenTaiKhoan = username;
            return View(quyens);
        }
        [HttpPost]
        public JsonResult ThayDoiChucNang(bool[] IsActive,string[] TenChucNang, string UserName)
        {
            var user = kiemtra.getUser(User.Identity.Name);
            db = ShopEntities.CreateEntitiesForSpecificDatabaseName(user.MaCN);
            var userstore = new UserStore<ApplicationUser>(db);
            var userManager = new UserManager<ApplicationUser>(userstore);
            var account = userManager.FindByName(UserName);

            for(int i=0;i<IsActive.Length;i++)
            {
                if (!IsActive[i])                  
                {
                    if (userManager.IsInRole(account.Id, TenChucNang[i]))
                    {
                        userManager.RemoveFromRole(account.Id, TenChucNang[i]);
                    }
                }
                else
                {
                    if (!userManager.IsInRole(account.Id, TenChucNang[i]))
                    {
                        var tam = TenChucNang[i];
                        account.Roles.Add(db.Quyens.FirstOrDefault(x => x.Name.Equals(tam)));
                      
                    }
                }
            }
            try { db.SaveChanges(); }
            catch(Exception e)
            {
                log.Error("Lỗi gán quyền cho nhân viên",e);
                return Json(new { data = 0 });
            }
            return Json(new { data = 1 });
        }
        public ActionResult ThemChucNang()
        {
            var user = kiemtra.getUser(User.Identity.Name);
            db = ShopEntities.CreateEntitiesForSpecificDatabaseName(user.MaCN);
            var lst = db.ChucNangNhanViens.ToList();
            return View(lst);
        }
        [HttpPost]
        public JsonResult ThemQuyenNhanVien(bool[] IsEnable, int[] ChucNangId,string TenQuyen)
        {
            var user = kiemtra.getUser(User.Identity.Name);
            db = ShopEntities.CreateEntitiesForSpecificDatabaseName(user.MaCN);
            var rolestore = new RoleStore<IdentityRole>(db);
            var roleManager = new RoleManager<IdentityRole>(rolestore);
            var role = new ApplicationRole {  Name=TenQuyen};
            try {
                roleManager.Create(role);
                role.ChiTietQuyens = new List<ChiTietQuyen>();
                for (int i = 0; i < IsEnable.Length; i++)
                {
                    if (IsEnable[i] == true)
                    {
                        var tam = ChucNangId[i];
                        role.ChiTietQuyens.Add(new ChiTietQuyen { IsEnable = true, ChucNangId = tam, RoleId = role.Id });
                    }
                }
                db.SaveChanges();
            }catch(Exception e)
            {
                log.Error("Lỗi thêm quyền nhân viên", e);
                return Json(new { data = 0 });
            }
            return Json(new { data=1});
        }

        public ActionResult ChiTietChucNang(string id)
        {
            var user = kiemtra.getUser(User.Identity.Name);
            db = ShopEntities.CreateEntitiesForSpecificDatabaseName(user.MaCN);
            var role = db.Quyens.FirstOrDefault(x => x.Name.Equals(id));
            var lst = new List<ChiTietQuyen>();
            var chucnangs = db.ChucNangNhanViens.ToList();
            foreach (var item in chucnangs)
            {
                
               
            }
           
            return View(lst);
        }
        [HttpPost]
        public JsonResult ChiTietChucNang(bool[] IsEnable,  string role)
        {
            var user = kiemtra.getUser(User.Identity.Name);
            db = ShopEntities.CreateEntitiesForSpecificDatabaseName(user.MaCN);
            var Quyen = db.Quyens.FirstOrDefault(x => x.Name.Equals(role));
            for (int i = 0; i < IsEnable.Length; i++)
            {

              
                    Quyen.ChiTietQuyens.ElementAt(i).IsEnable = IsEnable[i];
                

            }
            try {
                db.SaveChanges();
                return Json(new { data=1 });
            }
            catch(Exception e)
            {
                log.Error("Lỗi Thay đổi quyền nhân viên chi tiết: ", e);
            }
            return Json(new { data = 0 });
        }
        [HttpPost]
        public JsonResult XoaQuyenDangNhap(string role)
        {
            var user = kiemtra.getUser(User.Identity.Name);
            db = ShopEntities.CreateEntitiesForSpecificDatabaseName(user.MaCN);
            var Quyen = db.Quyens.FirstOrDefault(x => x.Name.Equals(role));
            db.Quyens.Remove(db.Quyens.FirstOrDefault(x => x.Name.Equals(role)));
            try {
                db.SaveChanges();
            }catch(Exception e)
            {
                log.Error("Lỗi xóa quyền đăng nhập nhân viên: ", e);
            }
            return Json(new { data = 1 });
        }
        [HttpPost]
        public ActionResult DeleteChucNang(string id)
        {
            var user = kiemtra.getUser(User.Identity.Name);
            db = ShopEntities.CreateEntitiesForSpecificDatabaseName(user.MaCN);
            var rolestore = new RoleStore<IdentityRole>(db);
            var roleManager = new RoleManager<IdentityRole>(rolestore);
            roleManager.Delete(roleManager.FindByName(id));
            return View();
        }
        public ActionResult ChinhSuaHopDong(int id)
        {
            var hopdong = db.HopDongChiNhanhs.Find(id);
            if (hopdong != null)
            {
                ViewBag.MaCN = new SelectList(db.ChiNhanhs.ToList(), "MaCN", "MaCN", hopdong.MaCN);
                ViewBag.MaLoaiGianHang = new SelectList(db.LoaiGianHangs.ToList(), "MaLoai", "Name", hopdong.MaLoaiGianHang);
            }
            return View(hopdong);
        }
        [HttpPost]
        public ActionResult ChinhSuaHopDong(HopDongChiNhanh hopdong)
        {
            ThongBaoMvc thongbao;
            db.Entry(hopdong).State = EntityState.Modified;
            try {
                db.SaveChanges();
                thongbao = new ThongBaoMvc { CssClassName = "success", Message = "Thay đổi thông tin thành công" };
                TempData["ResultAction"] = thongbao;
            }
            catch(Exception e)
            {
                thongbao = new ThongBaoMvc { CssClassName = "danger", Message = "Lỗi" };
                TempData["ResultAction"] = thongbao;
                log.Error("Lỗi thay đổi thông tin hợp đồng chi nhánh: ", e);
            }
            
            return View(hopdong);
            
          
        }
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
