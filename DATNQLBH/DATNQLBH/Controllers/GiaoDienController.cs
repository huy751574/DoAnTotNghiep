using DATNQLBH.Manager;
using DATNQLBH.Models;
using DATNQLBH.Models.CSDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace thuctap.Controllers
{
    [AllowAnonymous]
    public class GiaoDienController : Controller
    {
        ShopEntities db=new ShopEntities();
        //
        // GET: /GiaoDien/

       
        public ActionResult Index()
        {
            var LoaiGianHang = db.LoaiGianHangs.ToList();
            List<string> sologan = new List<string>();
            sologan.AddRange(db.GiaoDiens.Select(x =>  x.Description ).ToList());
            ViewBag.Sologan = sologan;
            return View(LoaiGianHang);
        }
        public ActionResult PhiDichVu()
        {
            return View();
        }
    
        //public ActionResult GetListItem(string search, int page = 1)
        //{
        //    var listItem = db.MatHangs.Where(x => x.Propertynames != "").Select(s => new { s.ItemId, s.Name, s.Price,image = "/Content/Image/" + s.CoverImage+"/"+s.Image,s.Propertynames,chitiet = s.LoaiCap3.PropertyNames}).ToList();
        //    int sumItem = 0;
        //    int sumPage = 0;
        //    int skipRow = 0;
        //    int takeRow = 0;
        //    sumItem = listItem.Count;
        //    if (sumItem > 0 && (page - 1) * 8 < sumItem)
        //    {
        //        sumPage = sumItem / 8;
        //        if (sumItem % 8 > 0)
        //            sumPage += 1;
        //        if (page < 1)
        //            page = 1;
        //        else if (page > sumPage)
        //            page = sumPage;
        //        skipRow = (page - 1) * 8;
        //        takeRow = 8;
        //        listItem = listItem.Skip(skipRow).Take(takeRow).ToList();
        //    }
        //    var Listitem = new List<object>();
        //    for(int i=0;i< listItem.Count; i++)
        //    {
        //        Listitem.Add(new { listItem[i].ItemId, listItem[i].Name, listItem[i].image, listItem[i].Price, DS = listItem[i].chitiet.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries), CT = listItem[i].Propertynames.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries) });
        //    }
        //    return Json(new { listItem = Listitem },JsonRequestBehavior.AllowGet);
        //}

        //public ActionResult GetGanHang(int Id)
        //{
        //    var GianHang = db.LoaiGianHangs.FirstOrDefault(s=>s.MaLoai == Id);
        //    return Json(new { GianHang.Name, Price = GianHang.Price.ToString("#,##0"), ChucNang = GianHang.CT_ChucNang.Select(x=>new {x.ChucNang.Name,x.IsActive }).ToList() },JsonRequestBehavior.AllowGet);
        //}

        
        public ActionResult DanhChoKhachHang()
        {
            var GianHang = db.BlogDoiTacs.ToList();
            return View(GianHang);
        }

        public ActionResult BlogDoiTac(string MaCN)
        {

            BlogDoiTac blog = new BlogDoiTac();

            if (string.IsNullOrEmpty(MaCN))
            {
                return View();

            }
            else
            {
                blog = db.BlogDoiTacs.FirstOrDefault(x => x.MaCN.Equals(MaCN));
              
            }
            return View(blog);
        }

    }
}