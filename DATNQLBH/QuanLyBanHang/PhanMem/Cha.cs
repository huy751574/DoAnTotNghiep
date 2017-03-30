using log4net;
using QuanLyBanHang.CSDL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace QuanLyBanHang.PhanMem
{
    public partial class Cha : Form
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Cha));
        public static string Url= "http://localhost:45253/";
        public Cha()
        {
            InitializeComponent();
           
            new Thread(() =>
            {
                System.Timers.Timer timer = new System.Timers.Timer(1000);
                timer.Elapsed += new ElapsedEventHandler(CheckLogin);
                timer.Enabled = true;
                timer.Start();
            });
            
        }
        private void CheckLogin(object source, ElapsedEventArgs e)
        {
            if (IsLogin())
            {
                btnDangXuat.Visible = true;
            }
            else
            {
                btnDangXuat.Visible = false;
            }
        }
        private void lấyDữLiệuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(!IsLogin())
            {
               
                    DangNhap dangnhap = new DangNhap(1);
                    dangnhap.MdiParent = this;
                    dangnhap.Show();
               
               
            }
            else
            {
                (new Thread(() => {
                    GetDuLieu(Properties.Settings.Default.UserName);
                })).Start();
              
            }
        }

        public bool IsLogin()
        {
          
            if (!string.IsNullOrEmpty(Properties.Settings.Default.UserName))
            {
                lblWelcome.Text = "Xin chào " + Properties.Settings.Default.UserName;
                return true;
            }
            else
            {
                lblWelcome.Text = "";
                return false;
            }

        }

        public static void GetDuLieu(string UserName)
        {
            ShopEntities db = new ShopEntities();
            string postData = "UserName=" + UserName + "&ChapNhan=true";
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            Uri target = new Uri(Url+"WinformConnect/GetDuLieu");
            WebRequest request = WebRequest.Create(target);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;

            using (var dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }



            WebResponse response = request.GetResponse();
            string result = new StreamReader(response.GetResponseStream()).ReadToEnd();
            var serializer = new JavaScriptSerializer();
            dynamic jsonObject = serializer.DeserializeObject(result);

            var trans = db.Database.BeginTransaction();
            Thread thread2 = new Thread(() =>
            {
                foreach (var khachhang in jsonObject["lstKhachHang"])
                {
                    db.KhachHangs.AddOrUpdate(new KhachHang { Address = khachhang["Address"], CMND = khachhang["CMND"], Email = khachhang["Email"], ExpVip = khachhang["ExpVip"], FullName = khachhang["FullName"], Id = khachhang["Id"], SDT = khachhang["SDT"], Vip = khachhang["Vip"] });
                }

            });
            thread2.Start();
            Thread thread3 = new Thread(() =>
            {
                foreach (var nhacc in jsonObject["lstNhaCC"])
                {
                    db.NhaCCs.AddOrUpdate(new NhaCC { TaiKhoan = nhacc["TaiKhoan"], SDT = nhacc["SDT"], Name = nhacc["Name"], MaNCC = nhacc["MaNCC"], Fax = nhacc["Fax"], Email = nhacc["Email"], Address = nhacc["Address"] });
                }

            });
            thread3.Start();

            Thread thread1 = new Thread(() =>
            {
                var threads = new List<Thread>();
                Thread threadcon1 = new Thread(() =>
                {
                    foreach (var quycach in jsonObject["lstQuyCach"])
                    {
                        db.QuyCachs.AddOrUpdate(new QuyCach { Name = quycach["Name"], QuyCachId = quycach["QuyCachId"] });
                    }

                });
                threadcon1.Start();
                threads.Add(threadcon1);
                Thread threadcon2 = new Thread(() =>
                {

                    foreach (var loaicap1 in jsonObject["lstLoaiCap1"])
                    {
                        db.LoaiCap1s.AddOrUpdate(new LoaiCap1 { LoaiCap1Id = loaicap1["LoaiCap1Id"], Name = loaicap1["Name"] });
                    }

                    foreach (var loaicap2 in jsonObject["lstLoaiCap2"])
                    {
                        db.LoaiCap2s.AddOrUpdate(new LoaiCap2 { LoaiCap2Id = loaicap2["LoaiCap2Id"], Name = loaicap2["Name"], LoaiCap1Id = loaicap2["LoaiCap1Id"] });
                    }

                    foreach (var loaicap3 in jsonObject["lstLoaiCap3"])
                    {
                        db.LoaiCap3s.AddOrUpdate(new LoaiCap3 { LoaiCap3Id = loaicap3["LoaiCap3Id"], Name = loaicap3["Name"], PropertyNames = loaicap3["PropertyNames"], LoaiCap2Id = loaicap3["LoaiCap2Id"] });
                    }

                });
                threadcon2.Start();
                threads.Add(threadcon2);
                foreach (var thread in threads)
                {
                    thread.Join();
                }

                foreach (var mathang in jsonObject["lstMatHang"])
                {
                    db.MatHangs.AddOrUpdate(new MatHang { AuthorName = mathang["AuthorName"], ItemId = mathang["ItemId"], LoaiCap3Id = mathang["LoaiCap3Id"], Name = mathang["Name"], Price = mathang["Price"], Propertynames = mathang["Propertynames"], QuyCachId = mathang["QuyCachId"], Reduction = mathang["Reduction"], Serial = mathang["Serial"], VAT = mathang["VAT"] });
                }

            });
            thread1.Start();



            thread1.Join();
            thread2.Join();
            thread3.Join();

            Thread thread4 = new Thread(() =>
            {
                foreach (var khohang in jsonObject["lstKhoHang"])
                {
                    var kho = new KhoHang { Name = khohang["Name"], SDT = khohang["SDT"], WarehouseID = khohang["WarehouseID"], DiaChi = khohang["DiaChi"] };
                    kho.ChiTietKhoHangs = new List<ChiTietKhoHang>();
                    foreach (var chitiet in khohang["CTKhoHang"])
                    {
                        var id = khohang["WarehouseID"] + "";
                        var k = chitiet["ItemId"];
                        var h = chitiet["WarehouseID"];
                        var KhoTonTai = db.KhoHangs.FirstOrDefault(x => x.WarehouseID == kho.WarehouseID);
                        if (KhoTonTai != null)
                        {
                            if (KhoTonTai.ChiTietKhoHangs.Any(x => x.ItemId == k && x.WarehouseID == h))
                            {
                                var b = kho.ChiTietKhoHangs.First(x => x.ItemId == chitiet["ItemId"] && x.WarehouseID == chitiet["WarehouseID"]);
                                b.Quantities += chitiet["Quantities"];
                            }
                            else
                            {
                                ChiTietKhoHang a = new ChiTietKhoHang { ItemId = chitiet["ItemId"], Quantities = chitiet["Quantities"] };
                                kho.ChiTietKhoHangs.Add(a);
                            }
                        }

                    }
                    db.KhoHangs.AddOrUpdate(kho);
                }
               


            });
            thread4.Start();
            thread4.Join();
            try {
                db.SaveChanges();
                trans.Commit();
               
                MessageBox.Show("Đã cập nhật dữ liệu");
            }catch(Exception e)
            {
                log.Error("Lỗi cập nhật dữ liệu", e);
                MessageBox.Show("Cập nhật dữ liệu thất bại");
            }




        }

        private void lậpHóaĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
                LapHoaDon laphoadon = new LapHoaDon();
                laphoadon.MdiParent = this;
                laphoadon.Show();
           
        }

        private void danhSáchMặtHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
                DanhSachMatHang danhsach = new DanhSachMatHang();
                danhsach.MdiParent = this;
                danhsach.Show();
           
        }

        private void đồngBộToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!IsLogin())
            {

                DangNhap dangnhap = new DangNhap(2);
                dangnhap.MdiParent = this;
                dangnhap.Show();


            }
            else
            {
                (new Thread(() => {
                    DongBo(Properties.Settings.Default.UserName);
                })).Start();

            }
        }

        public static void DongBo(string userName)
        {
            ShopEntities db = new ShopEntities();
            var postData = new List<KeyValuePair<string, object>>();
            postData.Add(new KeyValuePair<string, object>("UserName", userName));
            postData.Add(new KeyValuePair<string, object>("ChapNhan ", "true"));
            Uri target = new Uri(Url + "WinformConnect/DongBo");
            var ListDH = db.DonHangs.Select(x => new DonHang{  Address=x.Address, BuyTime=x.BuyTime, Completiontime=x.Completiontime, CustomerId= x.CustomerId, FullName=x.FullName, SDT=x.SDT, State=x.State }).ToList();
            postData.Add(new KeyValuePair<string, object>("don", ListDH));
          

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(Url + "WinformConnect/DongBo");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(postData);

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();

                // If you need to read response
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    string result = streamReader.ReadToEnd();
                    var serializer = new JavaScriptSerializer();
                    dynamic jsonObject = serializer.DeserializeObject(result);

                }
            }
        }
    }
}
