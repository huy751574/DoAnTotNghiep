using log4net;
using QuanLyBanHang.CSDL;
using QuanLyBanHang.Repository;
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
    public partial class DanhSachMatHang : Form
    {
        ShopEntities db;
        Repository<MatHang> RepositoryMatHang;
        DataTable TableMatHang;
        private static readonly ILog log = LogManager.GetLogger(typeof(DanhSachMatHang));
        public DanhSachMatHang()
        {
            InitializeComponent();
            db = new ShopEntities();
            RepositoryMatHang = new Repository<MatHang>(db);
            LoadMatHang();
        }

        public void LoadMatHang()
        {
            TableMatHang = new DataTable();
            TableMatHang.Columns.Add(new DataColumn("ID"));
            TableMatHang.Columns.Add(new DataColumn("Tên sản phẩm"));
            TableMatHang.Columns.Add(new DataColumn("Xuất xứ"));
            TableMatHang.Columns.Add(new DataColumn("Đơn giá"));
            TableMatHang.Columns.Add(new DataColumn("% Giảm giá"));
            TableMatHang.Columns.Add(new DataColumn("VAT"));
            TableMatHang.Columns.Add(new DataColumn("Loại sản phẩm"));
            TableMatHang.Columns.Add(new DataColumn("Đơn vị"));
            TableMatHang.Columns.Add(new DataColumn("Mã vạch"));
          
            var lstMatHang = RepositoryMatHang.SelectAll();
            foreach (var item in lstMatHang)
            {
                TableMatHang.Rows.Add(item.ItemId, item.Name, item.AuthorName, item.Price, item.Reduction, item.VAT, item.LoaiCap3.Name, item.QuyCach.Name, item.Serial);              
            }
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = TableMatHang;
        }
        private void DanhSachMatHang_Load(object sender, EventArgs e)
        {

        }
    }
}
