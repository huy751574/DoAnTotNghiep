namespace QuanLyBanHang.PhanMem
{
    partial class LapHoaDon
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.lblSDTKH = new System.Windows.Forms.Label();
            this.lblDiaChiKH = new System.Windows.Forms.Label();
            this.lblTenKH = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblSDTCN = new System.Windows.Forms.Label();
            this.lblDiaChiCN = new System.Windows.Forms.Label();
            this.lblEmailCN = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTenCN = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabThem = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.STT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenSP = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.DonGia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Controls.Add(this.lblSDTKH);
            this.panel1.Controls.Add(this.lblDiaChiKH);
            this.panel1.Controls.Add(this.lblTenKH);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.lblSDTCN);
            this.panel1.Controls.Add(this.lblDiaChiCN);
            this.panel1.Controls.Add(this.lblEmailCN);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lblTenCN);
            this.panel1.Location = new System.Drawing.Point(17, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(725, 462);
            this.panel1.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.STT,
            this.TenSP,
            this.DonGia,
            this.SL,
            this.Tong});
            this.dataGridView1.GridColor = System.Drawing.SystemColors.Window;
            this.dataGridView1.Location = new System.Drawing.Point(34, 278);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(656, 181);
            this.dataGridView1.TabIndex = 10;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView1_EditingControlShowing);
            // 
            // lblSDTKH
            // 
            this.lblSDTKH.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSDTKH.Location = new System.Drawing.Point(379, 215);
            this.lblSDTKH.Name = "lblSDTKH";
            this.lblSDTKH.Size = new System.Drawing.Size(311, 19);
            this.lblSDTKH.TabIndex = 9;
            this.lblSDTKH.Text = ".";
            this.lblSDTKH.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDiaChiKH
            // 
            this.lblDiaChiKH.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDiaChiKH.Location = new System.Drawing.Point(405, 170);
            this.lblDiaChiKH.Name = "lblDiaChiKH";
            this.lblDiaChiKH.Size = new System.Drawing.Size(285, 45);
            this.lblDiaChiKH.TabIndex = 8;
            this.lblDiaChiKH.Text = ".";
            this.lblDiaChiKH.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblTenKH
            // 
            this.lblTenKH.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTenKH.Location = new System.Drawing.Point(401, 146);
            this.lblTenKH.Name = "lblTenKH";
            this.lblTenKH.Size = new System.Drawing.Size(289, 19);
            this.lblTenKH.TabIndex = 7;
            this.lblTenKH.Text = ".";
            this.lblTenKH.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Cursor = System.Windows.Forms.Cursors.No;
            this.label7.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(529, 91);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(161, 31);
            this.label7.TabIndex = 6;
            this.label7.Text = "Khách Hàng";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSDTCN
            // 
            this.lblSDTCN.AutoSize = true;
            this.lblSDTCN.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSDTCN.Location = new System.Drawing.Point(40, 215);
            this.lblSDTCN.Name = "lblSDTCN";
            this.lblSDTCN.Size = new System.Drawing.Size(45, 19);
            this.lblSDTCN.TabIndex = 4;
            this.lblSDTCN.Text = "label5";
            // 
            // lblDiaChiCN
            // 
            this.lblDiaChiCN.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDiaChiCN.Location = new System.Drawing.Point(40, 170);
            this.lblDiaChiCN.Name = "lblDiaChiCN";
            this.lblDiaChiCN.Size = new System.Drawing.Size(290, 45);
            this.lblDiaChiCN.TabIndex = 3;
            this.lblDiaChiCN.Text = "label4";
            // 
            // lblEmailCN
            // 
            this.lblEmailCN.AutoSize = true;
            this.lblEmailCN.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmailCN.Location = new System.Drawing.Point(40, 146);
            this.lblEmailCN.Name = "lblEmailCN";
            this.lblEmailCN.Size = new System.Drawing.Size(45, 19);
            this.lblEmailCN.TabIndex = 2;
            this.lblEmailCN.Text = "label3";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(255, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(171, 40);
            this.label2.TabIndex = 1;
            this.label2.Text = "Đơn Hàng";
            this.label2.UseMnemonic = false;
            // 
            // lblTenCN
            // 
            this.lblTenCN.AutoSize = true;
            this.lblTenCN.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTenCN.Location = new System.Drawing.Point(38, 91);
            this.lblTenCN.Name = "lblTenCN";
            this.lblTenCN.Size = new System.Drawing.Size(85, 31);
            this.lblTenCN.TabIndex = 0;
            this.lblTenCN.Text = "label1";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabThem);
            this.tabControl1.Location = new System.Drawing.Point(22, 46);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(767, 502);
            this.tabControl1.TabIndex = 1;
            this.tabControl1.Click += new System.EventHandler(this.tabControl1_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(759, 476);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "New Tab";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabThem
            // 
            this.tabThem.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.tabThem.Location = new System.Drawing.Point(4, 22);
            this.tabThem.Name = "tabThem";
            this.tabThem.Padding = new System.Windows.Forms.Padding(3);
            this.tabThem.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tabThem.Size = new System.Drawing.Size(759, 476);
            this.tabThem.TabIndex = 1;
            this.tabThem.Text = "Thêm Tab";
            this.tabThem.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Image = global::QuanLyBanHang.Properties.Resources.Profile;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(627, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(141, 50);
            this.button1.TabIndex = 2;
            this.button1.Text = "Thêm khách hàng";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // STT
            // 
            this.STT.HeaderText = "STT";
            this.STT.Name = "STT";
            // 
            // TenSP
            // 
            this.TenSP.HeaderText = "Tên sản phẩm";
            this.TenSP.Name = "TenSP";
            // 
            // DonGia
            // 
            this.DonGia.HeaderText = "Đơn giá";
            this.DonGia.Name = "DonGia";
            // 
            // SL
            // 
            this.SL.HeaderText = "Số lượng";
            this.SL.Name = "SL";
            // 
            // Tong
            // 
            this.Tong.HeaderText = "Tổng";
            this.Tong.Name = "Tong";
            // 
            // LapHoaDon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(801, 560);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tabControl1);
            this.Name = "LapHoaDon";
            this.Text = "LapHoaDon";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblSDTCN;
        private System.Windows.Forms.Label lblDiaChiCN;
        private System.Windows.Forms.Label lblEmailCN;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblTenCN;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblSDTKH;
        private System.Windows.Forms.Label lblDiaChiKH;
        private System.Windows.Forms.Label lblTenKH;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabThem;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn STT;
        private System.Windows.Forms.DataGridViewComboBoxColumn TenSP;
        private System.Windows.Forms.DataGridViewTextBoxColumn DonGia;
        private System.Windows.Forms.DataGridViewTextBoxColumn SL;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tong;
    }
}