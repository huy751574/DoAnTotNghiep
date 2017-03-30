namespace QuanLyBanHang.PhanMem
{
    partial class Cha
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.lấyDữLiệuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.đồngBộToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lậpHóaĐơnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.danhSáchMặtHàngToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.btnDangXuat = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lấyDữLiệuToolStripMenuItem,
            this.đồngBộToolStripMenuItem,
            this.lậpHóaĐơnToolStripMenuItem,
            this.danhSáchMặtHàngToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(608, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // lấyDữLiệuToolStripMenuItem
            // 
            this.lấyDữLiệuToolStripMenuItem.Name = "lấyDữLiệuToolStripMenuItem";
            this.lấyDữLiệuToolStripMenuItem.Size = new System.Drawing.Size(76, 20);
            this.lấyDữLiệuToolStripMenuItem.Text = "Lấy dữ liệu";
            this.lấyDữLiệuToolStripMenuItem.Click += new System.EventHandler(this.lấyDữLiệuToolStripMenuItem_Click);
            // 
            // đồngBộToolStripMenuItem
            // 
            this.đồngBộToolStripMenuItem.Name = "đồngBộToolStripMenuItem";
            this.đồngBộToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.đồngBộToolStripMenuItem.Text = "Đồng bộ";
            this.đồngBộToolStripMenuItem.Click += new System.EventHandler(this.đồngBộToolStripMenuItem_Click);
            // 
            // lậpHóaĐơnToolStripMenuItem
            // 
            this.lậpHóaĐơnToolStripMenuItem.Name = "lậpHóaĐơnToolStripMenuItem";
            this.lậpHóaĐơnToolStripMenuItem.Size = new System.Drawing.Size(85, 20);
            this.lậpHóaĐơnToolStripMenuItem.Text = "Lập hóa đơn";
            this.lậpHóaĐơnToolStripMenuItem.Click += new System.EventHandler(this.lậpHóaĐơnToolStripMenuItem_Click);
            // 
            // danhSáchMặtHàngToolStripMenuItem
            // 
            this.danhSáchMặtHàngToolStripMenuItem.Name = "danhSáchMặtHàngToolStripMenuItem";
            this.danhSáchMặtHàngToolStripMenuItem.Size = new System.Drawing.Size(128, 20);
            this.danhSáchMặtHàngToolStripMenuItem.Text = "Danh sách mặt hàng";
            this.danhSáchMặtHàngToolStripMenuItem.Click += new System.EventHandler(this.danhSáchMặtHàngToolStripMenuItem_Click);
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Location = new System.Drawing.Point(517, 5);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(10, 13);
            this.lblWelcome.TabIndex = 2;
            this.lblWelcome.Text = ".";
            this.lblWelcome.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnDangXuat
            // 
            this.btnDangXuat.Location = new System.Drawing.Point(533, 0);
            this.btnDangXuat.Name = "btnDangXuat";
            this.btnDangXuat.Size = new System.Drawing.Size(75, 23);
            this.btnDangXuat.TabIndex = 4;
            this.btnDangXuat.Text = "Đăng xuất";
            this.btnDangXuat.UseVisualStyleBackColor = true;
            this.btnDangXuat.Visible = false;
            // 
            // Cha
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(608, 459);
            this.Controls.Add(this.btnDangXuat);
            this.Controls.Add(this.lblWelcome);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Cha";
            this.Text = "Quản lý bán hàng";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem lấyDữLiệuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem đồngBộToolStripMenuItem;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.ToolStripMenuItem lậpHóaĐơnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem danhSáchMặtHàngToolStripMenuItem;
        private System.Windows.Forms.Button btnDangXuat;
    }
}