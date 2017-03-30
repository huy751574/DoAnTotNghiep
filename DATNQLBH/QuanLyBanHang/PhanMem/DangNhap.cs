using System;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Web.Script.Serialization;
using System.Threading;
using QuanLyBanHang.CSDL;
using System.Data.Entity.Migrations;

namespace QuanLyBanHang.PhanMem
{
    public partial class DangNhap : Form
    {
        int Flag = 0;
        ShopEntities db = new ShopEntities();
        public DangNhap()
        {
            InitializeComponent();
        }
        public DangNhap(int flag)
        {
            InitializeComponent();
            this.Flag = flag;
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
                LoginOnline(txtUserName.Text, txtPassword.Text);
           
        }

        public void LoginOnline(string UserName, string Password)
        {
            string postData = "UserName="+ UserName+ "&Password="+ Password;
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            // Post the data to the right place.
            Uri target = new Uri(Cha.Url+"Account/KiemtraDangNhapWinform");
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
            DangSuDung d = new DangSuDung();
            if(jsonObject["result"]==1)
            {
                d.UserName = jsonObject["UserName"];
                d.QuyenSuDung = jsonObject["QuyenSuDung"];
                d.TenCN = jsonObject["TenCN"];
                d.DiaChi = jsonObject["DiaChi"];
                d.SDT = jsonObject["SDT"];
                d.Email = jsonObject["Email"];
                db.DangSuDungs.AddOrUpdate(d);
                db.SaveChanges();
                if (checkRemember.Checked)
                {
                    Properties.Settings.Default.UserName = txtUserName.Text;
                    Properties.Settings.Default.Password = txtPassword.Text;
                    Properties.Settings.Default.Save();
                    
                }
                this.Flag = 1;
            }
            Thread h = new Thread(() => {
                Cha.GetDuLieu(UserName);
            });
            Thread h2=new Thread
                (() => {
                    Cha.DongBo(UserName);
                });
            if (this.Flag== 1)
            {              
                h.Start();
                h.Join();
            } else
            if (this.Flag == 2)
            {
                h2.Start();
                h.Join();
            }
           
            this.Close();
         
        }

    }
}
