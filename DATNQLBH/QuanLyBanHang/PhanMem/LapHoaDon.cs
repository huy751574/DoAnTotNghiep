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
    public partial class LapHoaDon : Form
    {
        ShopEntities db;
        Repository<DangSuDung> RepositoryDangSuDung;
        Repository<KhachHang> RepositoryKhachHang;
       
        DataTable TenSanPham;
        private int SoLuong = 1;
        List<TabPage> lstTab = new List<TabPage>();
       
        public LapHoaDon()
        {

            
            InitializeComponent();
            //IntPtr h = this.tabControl1.Handle;
            db = new ShopEntities();
            RepositoryDangSuDung = new Repository<DangSuDung>(db);
            RepositoryKhachHang = new Repository<KhachHang>(db);

            KhoiTao();
        }

     
        public void KhoiTao()
        {
            var ChiNhanh = db.DangSuDungs.FirstOrDefault();
            TenSanPham = new DataTable("TableTenSanPham");
            TenSanPham.Columns.Add(new DataColumn("ItemId"));
            TenSanPham.Columns.Add(new DataColumn("Name"));

   

           
            if (ChiNhanh != null)
            {
                lblDiaChiCN.Text = ChiNhanh.DiaChi;
                lblEmailCN.Text = ChiNhanh.Email;
                lblSDTCN.Text = ChiNhanh.SDT;
                lblTenCN.Text = ChiNhanh.TenCN;
            }
          
      

            
            DataGridViewComboBoxColumn lstSanPham = (DataGridViewComboBoxColumn)dataGridView1.Columns["TenSP"];

            foreach (var item in db.MatHangs.Select(x => new { x.Name, x.ItemId }).ToList())
            {
                TenSanPham.Rows.Add(item.ItemId,item.Name);
            }

            lstSanPham.ValueMember = "ItemId";
            lstSanPham.DisplayMember = "Name";
            lstSanPham.DataSource = TenSanPham;





        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogKhachHang dialog = new DialogKhachHang();
            if(dialog.ShowDialog()==DialogResult.OK)
            {
                var tab = tabControl1.SelectedTab;
                foreach(var item in tab.Controls)
                {

                }
                lblTenKH.Text = dialog.Ten;
                lblDiaChiKH.Text = dialog.DiaChi;
                lblSDTKH.Text = dialog.SDT;
                dialog.Close();
            }
            
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            var last = tabControl1.TabPages.Count - 1;
            if (tabControl1.SelectedIndex == last)
            {
                Panel pnl = new Panel();
                foreach (Control c in panel1.Controls)
                {
                    var c2=new Control();
                    DataGridView k;
                    if (c.GetType() == typeof(TextBox))
                        c2 = new TextBox();
                    else if (c.GetType() == typeof(Label))
                        c2 = new Label();
                    else if (c.GetType() == typeof(DataGridView))
                    {
                        c2 = new DataGridView();
                        c2 = KhoiTaoGridViewTrongTabMoi((DataGridView)c);
                    }
                    c2.Name = "Tab" + SoLuong + "_" + c.Name;
                    c2.Location = c.Location;
                    c2.Size = c.Size;
                    c2.Text = c.Text;
                    c2.Font = c.Font;
                    c2.BackColor = c.BackColor;
                   
                    pnl.Size = panel1.Size;
                    pnl.BackColor = panel1.BackColor;
                    pnl.Location = panel1.Location;
                    pnl.Controls.Add(c2);
                    
                }
                TabPage tab = new TabPage { Name = "Tab" + SoLuong, Text = "New Tab" };
                tab.BackColor = tabPage1.BackColor;
                tab.Controls.Add(pnl);
                lstTab.Add(tab);
               
                tabControl1.TabPages.Insert(last, tab);
                tabControl1.SelectedIndex = last;
                SoLuong++;
            }

        }

        public DataGridView KhoiTaoGridViewTrongTabMoi(DataGridView k)
        {
            var c= new DataGridView
            {
                BackgroundColor = k.BackgroundColor, 
                MultiSelect = false
            };


         
            DataGridViewComboBoxColumn lstSanPham = new DataGridViewComboBoxColumn();


            lstSanPham.ValueMember = "ItemId";
            lstSanPham.DisplayMember = "Name";
            lstSanPham.DataSource = TenSanPham;
            c.Columns.Add("STT","STT");
            c.Columns.Add(lstSanPham);
            c.Columns.Add("Số lượng", "Số lượng");
            c.Columns.Add("Đơn giá", "Đơn giá");
            c.Columns.Add("Tổng giá", "Tổng giá");
            c.EditingControlShowing += dataGridView1_EditingControlShowing;
            return c;
        }


       

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            var grid = (DataGridView)sender;
            grid.DataError += new DataGridViewDataErrorEventHandler(dgvCombo_DataError);

            if (grid.CurrentCell.ColumnIndex == 1 && e.Control is ComboBox)
            {
                ComboBox comboBox = (ComboBox)e.Control;
                comboBox.ValueMember = "ItemId";
                comboBox.DisplayMember = "Name";
                comboBox.DataSource = TenSanPham;
                comboBox.SelectedIndexChanged += TenSanPhamComboBoxChange;
            }
        }

        private void TenSanPhamComboBoxChange(object sender, EventArgs e)
        {
            var comboBoxMatHang = sender as ComboBox;
            
            var grid = (DataGridView)comboBoxMatHang.Parent.Parent;
            var currentcell = grid.CurrentCellAddress;

            DataGridViewTextBoxCell celGia = (DataGridViewTextBoxCell)grid.Rows[currentcell.Y].Cells[3];
            var tam = comboBoxMatHang.SelectedValue.ToString();
            var id = Convert.ToInt32(tam);
            var gia = db.MatHangs.Find(id);
            celGia.Value = 9;

            DataGridViewTextBoxCell celSl = (DataGridViewTextBoxCell)grid.Rows[currentcell.Y].Cells[2];
            if (celSl.Value == null)
                celSl.Value = 1;
            var tong = (DataGridViewTextBoxCell)grid.Rows[currentcell.Y].Cells[4];
            tong.Value = Convert.ToInt32(celSl.Value) * Convert.ToInt64(celGia.Value);

        }
        void dgvCombo_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // (No need to write anything in here)
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
