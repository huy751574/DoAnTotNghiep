using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DATNQLBH.Models;
using DATNQLBH.Models.CSDL;
using DATNQLBH.Manager;
using log4net;
using ImageResizer;
using System.IO;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using DATNQLBH.Migrations;

namespace DATNQLBH.Controllers
{
    [Authorize(Roles ="Admin,SuperAdmin")]
    [LogActionFilter]
    public class ChiNhanhController : Controller
    {
        private ShopEntities db = new ShopEntities();
        KiemTra kiemtra = new KiemTra();
        private static readonly ILog log = LogManager.GetLogger(typeof(ChiNhanhController));
        // GET: ChiNhanh
       
       
        public ActionResult ChiNhanh()
        {
            if(User.IsInRole("Admin"))
            {
                var user = kiemtra.getUser(User.Identity.Name);
                var cn = db.ChiNhanhs.FirstOrDefault(x => x.MaCN.Equals(user.MaCN));
                return View(cn);
            }
            return View();
        }
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChiNhanh(ChiNhanh cn, HttpPostedFileBase pic)
        {
            ThongBaoMvc thongbao;
            var chinhanh = db.ChiNhanhs.FirstOrDefault(x=>x.MaCN.Equals(cn.MaCN));

            if (pic != null )
            {


                var pathfile = Server.MapPath("~/Content/Images/") + cn.MaCN + "/" + Path.GetFileNameWithoutExtension(pic.FileName);

                var job = new ImageJob(pic, pathfile, new Instructions("model=max;format=png;width=100;height=300;"));

                job.CreateParentDirectory = true;// tạo folder nếu chưa có
                job.AddFileExtension = true;
                job.Build();

                chinhanh.Logo = Path.GetFileNameWithoutExtension(pic.FileName) + ".png";


            }
            chinhanh.Name = cn.Name;
            chinhanh.DiaChi = cn.DiaChi;
            chinhanh.Email = cn.Email;
            chinhanh.FaceBook = cn.FaceBook;
            chinhanh.Fax = cn.Fax;
            chinhanh.SDT = cn.SDT;
            chinhanh.SoTaiKhoan = cn.SoTaiKhoan;
            chinhanh.WebSite = cn.WebSite;


            try
            {

                db.SaveChanges();
                thongbao = new ThongBaoMvc { CssClassName = "success", Message = "Thay đổi thông tin chi nhánh thành công." };
                TempData["ResultAction"] = thongbao;
                return View(chinhanh);
            }
            catch (Exception ex)
            {
                thongbao = new ThongBaoMvc { CssClassName = "danger", Message = "Lỗi." };
                TempData["ResultAction"] = thongbao;
                log.Error("Lỗi khi sửa thông tin chi nhánh: " + ex.Message);
            }
            return View(cn);
                
            
           
        }
        public JsonResult DSChiNhanh()
        {
            List<object> lst = new List<object>();
            lst.AddRange(db.ChiNhanhs.Where(x => x.MaCN.Contains("ChiNhanh")).Select(x => new
            {
                x.Date,
                x.DiaChi,
                x.Email,
                x.FaceBook,
                x.Fax,
                x.Logo,
                x.MaCN,
                x.Name,
                x.SDT,
                x.SoTaiKhoan,
                x.WebSite
            }));
            return Json(new { lst }, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "SuperAdmin")]
        public ActionResult GianHang()
        {
            return View();
        }

        [Authorize(Roles = "SuperAdmin")]
        public JsonResult DSGianHang(string MaCN)
        {
            List<object> lst = new List<object>();
            if (String.IsNullOrEmpty(MaCN))
            {
                lst.AddRange(db.ChiNhanhs.Where(x => x.MaCN.Contains("GianHang"))
                    .Select(x => new
                    {
                        x.Date,
                        x.DiaChi,
                        x.Email,
                        x.FaceBook,
                        x.Fax,
                        x.Logo,
                        x.MaCN,
                        x.Name,
                        x.SDT,
                        x.SoTaiKhoan,
                        x.WebSite,
                        x.TaiKhoans.FirstOrDefault().FullName

                    }));
            }
            else
            {
                lst.AddRange(db.HopDongChiNhanhs.Where(x => x.MaCN.Equals(MaCN)).Select(x => new
                {
                    x.BeginDate,
                    x.EndDate,
                    x.LoaiGianHang.Name,
                    x.LoaiGianHang.Price,
                    x.LoaiGianHang.MaLoai,
                    x.MaCN,
                    x.Id
                }));
            }
            return Json(new { lst }, JsonRequestBehavior.AllowGet);
        }







        /// <summary>
        /// Thêm các trang giới thiệu gian hàng
        /// </summary>
        /// <param name="MaCN">là mã gian hàng</param>
        /// <returns></returns>
        [Authorize(Roles = "SuperAdmin")]
        public ActionResult ThemBlogDoiTac(string MaCN)
        {
            
            BlogDoiTac BlogDoiTac = new BlogDoiTac();
            if (string.IsNullOrEmpty(MaCN))
            {
                return View(BlogDoiTac);
            }
            else
            {
                BlogDoiTac = db.BlogDoiTacs.FirstOrDefault(x=>x.MaCN.Equals(MaCN));
                if (BlogDoiTac == null) return View(new BlogDoiTac());
            }
            return View(BlogDoiTac);
        }
        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemBlogDoiTac(BlogDoiTac BlogDoiTac, HttpPostedFileBase file)
        {
            BlogDoiTac blog = new BlogDoiTac();
            blog = db.BlogDoiTacs.FirstOrDefault(x => x.MaCN.Equals(BlogDoiTac.MaCN));
            ThongBaoMvc thongbao;
            string image = "";
                if (file != null)
                {
                   

                    var pathfile = Server.MapPath("~/Content/ChiNhanh/")  + Path.GetFileNameWithoutExtension(file.FileName);

                    var job = new ImageJob(file, pathfile, new Instructions("model=max;format=png;width=100;height=300;"));

                    job.CreateParentDirectory = true;// tạo folder nếu chưa có
                    job.AddFileExtension = true;
                    job.Build();

                image = Path.GetFileNameWithoutExtension(file.FileName) + ".png";


                }
            
            if (blog != null)
            {
                //Có trang giới thiệu khách hàng sử dụng công ty sẵn, thay đổi nội dung
                blog.Title = BlogDoiTac.Title;
                blog.Description = BlogDoiTac.Description;
                if(!String.IsNullOrEmpty(image))
                {
                    blog.Image = "/Content/ChiNhanh/" + image;
                }
              

            }
            else
            {
                blog = new BlogDoiTac();
                //Thêm trang giới thiệu
                blog.Title = BlogDoiTac.Title;
                blog.MaCN = BlogDoiTac.MaCN;
                blog.Description = BlogDoiTac.Description;
                if (!String.IsNullOrEmpty(image))
                {
                    blog.Image = "/Content/ChiNhanh/" + image;
                }
                db.BlogDoiTacs.Add(blog);
                
            }
            try
            {
                db.SaveChanges();
                thongbao = new ThongBaoMvc { CssClassName = "warning", Message = "Có vấn đề trong quá trình thêm nhân viên mới." };
                TempData["ResultAction"] = thongbao;
            
            }catch(Exception e)
            {
                log.Error("Lỗi thay đổi thông tin blog đối tác" + e.Message);
                thongbao = new ThongBaoMvc { CssClassName = "warning", Message = "Có vấn đề trong quá trình thêm nhân viên mới." };
                TempData["ResultAction"] = thongbao;
              
            }
            return View(BlogDoiTac);
        }



        public ActionResult ThemGianHang()
        {
            return View();
        }
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> ThemGianHang(ThemGianHang gianhang)
        {
            var userstore = new UserStore<ApplicationUser>(db);
            var userManager = new UserManager<ApplicationUser>(userstore);
            var trans= db.Database.BeginTransaction();

            //Chi nhánh
            var chinhanh = new ChiNhanh { MaCN = "GianHang-" + gianhang.UserName, Name = "GianHang-" + gianhang.UserName, DiaChi = gianhang.DiaChi, Email = gianhang.Email, SDT = gianhang.SDT, Date = DateTime.Now };
            db.ChiNhanhs.Add(chinhanh);
            db.SaveChanges();

            var user = new ApplicationUser { UserName = gianhang.UserName, Email = gianhang.Email, Address = gianhang.DiaChi, Birthday = DateTime.Now, FullName = gianhang.FullName, PhoneNumber = gianhang.SDT, MaCN = "GianHang-" + gianhang.UserName, Active = true, Experience = 0, NgayDangKy = DateTime.Now };


            var result = await userManager.CreateAsync(user, gianhang.Password);
            if(result.Succeeded)
            {
                var cn= new ChiNhanh { MaCN = "GianHang-" + gianhang.UserName, Name = "GianHang-" + gianhang.UserName, DiaChi = gianhang.DiaChi, Email = gianhang.Email, SDT = gianhang.SDT, Date = DateTime.Now };
                Configuration.ThemCuaHang(user, chinhanh);
                trans.Commit();
            }
            else
            {
                trans.Rollback();
            }
            return View(gianhang);
        }


        public ActionResult ThemHopDong(string MaCN)
        {
            var cn = db.ChiNhanhs.FirstOrDefault(x => x.MaCN.Equals(MaCN));
            ViewBag.MaLoaiGianHang = new SelectList(db.LoaiGianHangs.ToList(), "MaLoai", "Name", db.LoaiGianHangs.FirstOrDefault().MaLoai);
            return View(cn);
        }
        [HttpPost]
        public ActionResult ThemHopDong(HopDongChiNhanh hopdong)
        {
            ThongBaoMvc thongbao;
            var HopDong = new HopDongChiNhanh();
            HopDong.MaCN = hopdong.MaCN;
            HopDong.MaLoaiGianHang = hopdong.MaLoaiGianHang;
            HopDong.BeginDate = DateTime.Now;
            HopDong.EndDate = DateTime.Now.AddDays(db.LoaiGianHangs.Find(hopdong.MaLoaiGianHang).TimeUsing);
            db.HopDongChiNhanhs.Add(hopdong);
            try
            {
                thongbao = new ThongBaoMvc { CssClassName = "success", Message = "Thêm thành công" };
                TempData["ResultAction"] = thongbao;
                db.SaveChanges();
            }
            catch(Exception e)
            {
                thongbao = new ThongBaoMvc { CssClassName = "danger", Message = "Lỗi" };
                TempData["ResultAction"] = thongbao;
                log.Error("Lỗi thêm hợp đồng: ", e);
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
