using QuanLyBanHang.CSDL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBanHang.PhanMem
{
    public partial class DialogKhachHang : Form
    {
        ShopEntities db;
        public string SDT, DiaChi, Ten;
        public DialogKhachHang()
        {
            InitializeComponent();
            db = new ShopEntities();
            //AutoCompleteStringCollection allowedTypes = new AutoCompleteStringCollection();
            //var lstSuggestions = db.KhachHangs.Select(x => x.SDT).ToArray();
            //allowedTypes.AddRange(lstSuggestions);
            //txtSDTKH.AutoCompleteCustomSource = allowedTypes;
            //txtSDTKH.AutoCompleteMode = AutoCompleteMode.Suggest;
            //txtSDTKH.AutoCompleteSource = AutoCompleteSource.CustomSource;
            
        }

        private void DialogKhachHang_Load(object sender, EventArgs e)
        {
           
           
     
        }

        private void txtSDTKH_TextChanged(object sender, EventArgs e)
        {
            TextBox box = (TextBox)sender;
            var lstSuggestions = db.KhachHangs.ToList();
            foreach (var item in lstSuggestions)
            {
                if (item.SDT.StartsWith(box.Text))
                {
                    var length = box.Text.Length;
                    txtTenKH.Text = item.FullName;
                    this.Ten = item.FullName;
                    txtDiaChiKH.Text = item.Address;
                    this.DiaChi = item.Address;
                    box.Text = item.SDT;
                    this.SDT = item.SDT;
                    box.SelectionStart = length;
                    box.SelectionLength = box.Text.Length - length;
                    break;
                }
            }
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
