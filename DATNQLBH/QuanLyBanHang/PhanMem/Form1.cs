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
    public partial class Form1 : Form
    {
        ShopEntities db = new ShopEntities(); DataTable TenSanPham;
        public Form1()
        {
            InitializeComponent();
            KhoiTao();
        }
        public void KhoiTao()
        {
            var ChiNhanh = db.DangSuDungs.FirstOrDefault();
            TenSanPham = new DataTable("TableTenSanPham");
            TenSanPham.Columns.Add(new DataColumn("ItemId"));
            TenSanPham.Columns.Add(new DataColumn("Name"));

            DataColumn STT = new DataColumn("STT");
            STT.AutoIncrement = true;
            STT.AutoIncrementSeed = 1;
            STT.AutoIncrementStep = 1;




           


            DataGridViewComboBoxColumn lstSanPham = new DataGridViewComboBoxColumn();


            foreach (var item in db.MatHangs.Select(x => new { x.Name, x.ItemId }).ToList())
            {
                TenSanPham.Rows.Add(item.ItemId, item.Name);
            }

            CaiDatComboboxTrongCellTenSanPham(lstSanPham, TenSanPham);
            dataGridView1.Columns.Add("STT", "STT");
            dataGridView1.Columns.Add(lstSanPham);
            dataGridView1.Columns.Add("SoLuong", "Số lượng");
            dataGridView1.Columns.Add("Đơn giá", "Đơn giá");
            dataGridView1.Columns.Add("Tổng giá", "Tổng giá");



        }
        private void CaiDatComboboxTrongCellTenSanPham(DataGridViewComboBoxColumn comboboxColumn, DataTable lst)
        {

            comboboxColumn.DataSource = lst;
            comboboxColumn.DataPropertyName = lst.TableName;
            comboboxColumn.ValueMember = "ItemId";
            comboboxColumn.DisplayMember = "Name";
            comboboxColumn.ValueType = typeof(int);


        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            var grid = (DataGridView)sender;
           // grid.DataError += new DataGridViewDataErrorEventHandler(dgvCombo_DataError);

            if (grid.CurrentCell.ColumnIndex == 1 && e.Control is ComboBox)
            {
                ComboBox comboBox = (ComboBox)e.Control;
                comboBox.SelectedIndexChanged += TenSanPhamComboBoxChange;
            }
        }
        private void TenSanPhamComboBoxChange(object sender, EventArgs e)
        {
            var comboBoxMatHang = sender as DataGridViewComboBoxEditingControl;
            var grid = (DataGridView)comboBoxMatHang.Parent.Parent;
            var currentcell = grid.CurrentCellAddress;

            //DataGridViewTextBoxCell celGia = (DataGridViewTextBoxCell)grid.Rows[currentcell.Y].Cells[3];
            var tam = comboBoxMatHang.SelectedIndex;
            var h= TenSanPham.Rows[tam - 1]["ItemId"];
            var id = Convert.ToInt32(h);
            var gia = db.MatHangs.Find(id);
            //celGia.Value = gia.Price;

            //DataGridViewTextBoxCell celSl = (DataGridViewTextBoxCell)grid.Rows[currentcell.Y].Cells[2];
            //if (celSl.Value == null)
            //    celSl.Value = 1;
            //var tong = (DataGridViewTextBoxCell)grid.Rows[currentcell.Y].Cells[4];
            //tong.Value = Convert.ToInt32(celSl.Value) * Convert.ToInt64(celGia.Value);

        }
        void dgvCombo_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // (No need to write anything in here)
        }
    }
}
