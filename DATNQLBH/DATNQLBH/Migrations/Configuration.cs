namespace DATNQLBH.Migrations
{
    using Models;
    using Microsoft.AspNet.Identity;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Controllers;
    using Models.CSDL;
    using System.Collections.Generic;
    using Manager;

    internal sealed class Configuration : DbMigrationsConfiguration<DATNQLBH.Models.ShopEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(DATNQLBH.Models.ShopEntities context)
        {
           

            var rolestore = new RoleStore<ApplicationRole>(context);
            var roleManager = new RoleManager<ApplicationRole>(rolestore);

            var userstore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userstore);
            //Tạo chi nhánh
            context.ChiNhanhs.AddOrUpdate(new Models.CSDL.ChiNhanh { MaCN = "ChiNhanh-Hutech", SoTaiKhoan = "0", Name = "HUTECH", Email = "hutech@hutech.edu.vn", DiaChi = "475A Điện Biên Phủ, P.25, Q.Bình Thạnh, TP.HCM", Date = DateTime.Now, SDT = "5445 7777", WebSite = "http://www.hutech.edu.vn/", });
            context.ChiNhanhs.AddOrUpdate(new Models.CSDL.ChiNhanh { MaCN = "GianHang-HetHan", SoTaiKhoan = "0", Name = "Hết hạn", Email = "hutech@hutech.edu.vn", DiaChi = "475A Điện Biên Phủ, P.25, Q.Bình Thạnh, TP.HCM", Date = DateTime.Now, SDT = "5445 7777", WebSite = "http://www.hutech.edu.vn/", });
            context.ChiNhanhs.AddOrUpdate(new Models.CSDL.ChiNhanh { MaCN = "GianHang-ConHan", SoTaiKhoan = "0", Name = "Còn hạn", Email = "hutech@hutech.edu.vn", DiaChi = "475A Điện Biên Phủ, P.25, Q.Bình Thạnh, TP.HCM", Date = DateTime.Now, SDT = "5445 7777", WebSite = "http://www.hutech.edu.vn/", });

            context.SaveChanges();
            //Sologan giao diện
            context.GiaoDiens.AddOrUpdate(new Models.CSDL.GiaoDien { Image = "Tiêu đề.png", Description = "Kế hoạch siêu linh hoạt cho bạn" });
            context.GiaoDiens.AddOrUpdate(new Models.CSDL.GiaoDien { Image = "Tiêu đề 1.png", Description = "Chọn một gói phù hợp với bạn và xây dựng trang web của bạn ngay hôm nay" });
            context.GiaoDiens.AddOrUpdate(new Models.CSDL.GiaoDien { Image = "Tiêu đề 2.png", Description = "Hãy tưởng tượng những điều tuyệt vời tiếp theo. Sau đó xây dựng nó!" });
            context.SaveChanges();
            //Tạo Quyền Super Admin
            if (roleManager.FindByName("SuperAdmin") == null)
            {
                var role = new ApplicationRole { Name = "SuperAdmin" };
                roleManager.Create(role);
            }

            //Tạo quyền Admin
            if (roleManager.FindByName("Admin") == null)
            {
                var role = new ApplicationRole { Name = "Admin" };
                roleManager.Create(role);
            }
            //Tạo quyền Staff
            if (roleManager.FindByName("Staff") == null)
            {
                var role = new ApplicationRole { Name = "Staff" };
                roleManager.Create(role);
               
            }


            //Tạo tài khoản SuperAdmin
            if (userManager.FindByName("SuperAdmin") == null)
            {
                var superadmin = new ApplicationUser { UserName = "SuperAdmin", FullName = "Bùi Ngọc Minh Huy", MaCN = "ChiNhanh-Hutech", Email = "huy751574@gmail.com", Address = "74 Trung Nghĩa, Xuân Trường, Xuân Lộc, Đồng Nai", PhoneNumber = "01676293913", Birthday = new DateTime(1994, 1, 14), CMND = "272490997", Active = true, Experience = 0, NgayDangKy = DateTime.Now };

                userManager.Create(superadmin, "123456");
                userManager.AddToRole(superadmin.Id, "SuperAdmin");

            }
        
            var superadm = new ApplicationUser { UserName = "SuperAdmin", FullName = "Bùi Ngọc Minh Huy", MaCN = "ChiNhanh-Hutech", Email = "huy751574@gmail.com", Address = "74 Trung Nghĩa, Xuân Trường, Xuân Lộc, Đồng Nai", PhoneNumber = "01676293913", Birthday = new DateTime(1994, 1, 14), CMND = "272490997", Active = true, Experience = 0, NgayDangKy = DateTime.Now };

            var db = ShopEntities.CreateEntitiesForSpecificDatabaseName(superadm.MaCN);
            db.Database.CreateIfNotExists();
            var cn = new ChiNhanh { MaCN = "ChiNhanh-Hutech", SoTaiKhoan = "0", Name = "HUTECH", Email = "hutech@hutech.edu.vn", DiaChi = "475A Điện Biên Phủ, P.25, Q.Bình Thạnh, TP.HCM", Date = DateTime.Now, SDT = "5445 7777", WebSite = "http://www.hutech.edu.vn/"  };
         
            //Tạo các chức năng sử dụng của nhân viên tổng hợp
            var rolestoreDb = new RoleStore<ApplicationRole>(db);
            var roleManagerDb = new RoleManager<ApplicationRole>(rolestoreDb);

            var userstoreDb = new UserStore<ApplicationUser>(db);
            var userManagerDb = new UserManager<ApplicationUser>(userstoreDb);
            
            ThemCuaHang(superadm, cn);
           
            //Tạo Quyền Super Admin




            //Tạo tài khoản Admin
            if (userManager.FindByName("Admin") == null)
            {
                var admin = new ApplicationUser { UserName = "Admin", FullName = "Bùi Ngọc Minh Huy", MaCN = "ChiNhanh-Hutech", Email = "huy751574@gmail.com", Address = "74 Trung Nghĩa, Xuân Trường, Xuân Lộc, Đồng Nai", PhoneNumber = "01676293913", Birthday = new DateTime(1994, 1, 14), CMND = "272490997", Active = true, Experience = 0, NgayDangKy = DateTime.Now };
                var admin1 = new ApplicationUser { UserName = "Admin", FullName = "Bùi Ngọc Minh Huy", MaCN = "ChiNhanh-Hutech", Email = "huy751574@gmail.com", Address = "74 Trung Nghĩa, Xuân Trường, Xuân Lộc, Đồng Nai", PhoneNumber = "01676293913", Birthday = new DateTime(1994, 1, 14), CMND = "272490997", Active = true, Experience = 0, NgayDangKy = DateTime.Now };

              
                userManagerDb.Create(admin1);
             
                userManager.Create(admin, "Huy0908529946");
                userManager.AddToRole(admin.Id, "Admin");

            }
            //Tạo tài khoản Staff
            if (userManager.FindByName("Staff") == null)
            {

                var staff = new ApplicationUser { UserName = "Staff", FullName = "Bùi Ngọc Minh Huy", MaCN = "ChiNhanh-Hutech", Email = "huy751574@gmail.com", Address = "74 Trung Nghĩa, Xuân Trường, Xuân Lộc, Đồng Nai", PhoneNumber = "01676293913", Birthday = new DateTime(1994, 1, 14), CMND = "272490997", Active = true, Experience = 0, NgayDangKy = DateTime.Now };
                var staff1 = new ApplicationUser { UserName = "Staff", FullName = "Bùi Ngọc Minh Huy", MaCN = "ChiNhanh-Hutech", Email = "huy751574@gmail.com", Address = "74 Trung Nghĩa, Xuân Trường, Xuân Lộc, Đồng Nai", PhoneNumber = "01676293913", Birthday = new DateTime(1994, 1, 14), CMND = "272490997", Active = true, Experience = 0, NgayDangKy = DateTime.Now };

                userManagerDb.Create(staff1);
                userManager.Create(staff, "Huy0908529946");              
                userManager.AddToRole(staff.Id, "Staff");
               
            }
            //Tạo tài khoản Staff
            if (userManager.FindByName("Staff1") == null)
            {

                var staff = new ApplicationUser { UserName = "Staff1", FullName = "Bùi Ngọc Minh", MaCN = "ChiNhanh-Hutech", Email = "huy751574@gmail.com", Address = "74 Trung Nghĩa, Xuân Trường, Xuân Lộc, Đồng Nai", PhoneNumber = "01676293913", Birthday = new DateTime(1994, 1, 14), CMND = "272490997", Active = true, Experience = 0, NgayDangKy = DateTime.Now };
                var staff1 = new ApplicationUser { UserName = "Staff1", FullName = "Bùi Ngọc Minh", MaCN = "ChiNhanh-Hutech", Email = "huy751574@gmail.com", Address = "74 Trung Nghĩa, Xuân Trường, Xuân Lộc, Đồng Nai", PhoneNumber = "01676293913", Birthday = new DateTime(1994, 1, 14), CMND = "272490997", Active = true, Experience = 0, NgayDangKy = DateTime.Now };
            
                userManagerDb.Create(staff1);
                userManager.Create(staff, "Huy0908529946");
                userManager.AddToRole(staff.Id, "Staff");
               
            }
          
            //Tạo chức năng
            /*1*/
            context.ChucNangs.AddOrUpdate(new Models.CSDL.ChucNang { Name = "Người Dùng" });
            /*2*/
            context.ChucNangs.AddOrUpdate(new Models.CSDL.ChucNang { Name = "Hàng hóa" });
            /*3*/
            context.ChucNangs.AddOrUpdate(new Models.CSDL.ChucNang { Name = "Lập hóa đơn bán hàng" });
            /*4*/
            context.ChucNangs.AddOrUpdate(new Models.CSDL.ChucNang { Name = "Quản lý sản phẩm" });
            /*5*/
            context.ChucNangs.AddOrUpdate(new Models.CSDL.ChucNang { Name = "Quản lý khách hàng" });
            /*6*/
            context.ChucNangs.AddOrUpdate(new Models.CSDL.ChucNang { Name = "Cài đặt lịch làm việc" });
            /*7*/
            context.ChucNangs.AddOrUpdate(new Models.CSDL.ChucNang { Name = "Lập báo cáo" });
            context.SaveChanges();

            //Tạo loại gian hang
            /*1*/
            context.LoaiGianHangs.AddOrUpdate(new Models.CSDL.LoaiGianHang { Name = "Dùng thử",TimeUsing=7 });
            /*2*/
            context.LoaiGianHangs.AddOrUpdate(new Models.CSDL.LoaiGianHang { Name = "Cơ bản", Price = 50000,TimeUsing = 30 });
            /*3*/
            context.LoaiGianHangs.AddOrUpdate(new Models.CSDL.LoaiGianHang { Name = "Nâng cao", Price = 70000, TimeUsing = 30 });
            /*4*/
            context.LoaiGianHangs.AddOrUpdate(new Models.CSDL.LoaiGianHang { Name = "Cao Cấp", Price = 100000, TimeUsing = 30 });
            context.SaveChanges();

            //Tạo chi tiét chức năng
            /*1*/
            context.CT_ChucNangs.AddOrUpdate(new Models.CSDL.CT_ChucNang { ChucNangId = 1, IsActive = true, Quantity = 1, MaLoaiGianHang = 1 });
            context.CT_ChucNangs.AddOrUpdate(new Models.CSDL.CT_ChucNang { ChucNangId = 1, IsActive = true, Quantity = 2, MaLoaiGianHang = 2 });
            context.CT_ChucNangs.AddOrUpdate(new Models.CSDL.CT_ChucNang { ChucNangId = 1, IsActive = true, Quantity = 3, MaLoaiGianHang = 3 });
            context.CT_ChucNangs.AddOrUpdate(new Models.CSDL.CT_ChucNang { ChucNangId = 1, IsActive = true, Quantity = 5, MaLoaiGianHang = 4 });
            context.CT_ChucNangs.AddOrUpdate(new Models.CSDL.CT_ChucNang { ChucNangId = 2, IsActive = true, Quantity = 20, MaLoaiGianHang = 1 });
            context.CT_ChucNangs.AddOrUpdate(new Models.CSDL.CT_ChucNang { ChucNangId = 2, IsActive = true, Quantity = 50, MaLoaiGianHang = 2 });
            context.CT_ChucNangs.AddOrUpdate(new Models.CSDL.CT_ChucNang { ChucNangId = 2, IsActive = true, Quantity = 100, MaLoaiGianHang = 3 });
            context.CT_ChucNangs.AddOrUpdate(new Models.CSDL.CT_ChucNang { ChucNangId = 2, IsActive = true, Quantity = 500, MaLoaiGianHang = 4 });
            context.CT_ChucNangs.AddOrUpdate(new Models.CSDL.CT_ChucNang { ChucNangId = 3, IsActive = true, MaLoaiGianHang = 1 });
            context.CT_ChucNangs.AddOrUpdate(new Models.CSDL.CT_ChucNang { ChucNangId = 3, IsActive = true, MaLoaiGianHang = 2 });
            context.CT_ChucNangs.AddOrUpdate(new Models.CSDL.CT_ChucNang { ChucNangId = 3, IsActive = true, MaLoaiGianHang = 3 });
            context.CT_ChucNangs.AddOrUpdate(new Models.CSDL.CT_ChucNang { ChucNangId = 3, IsActive = true, MaLoaiGianHang = 4 });
            context.CT_ChucNangs.AddOrUpdate(new Models.CSDL.CT_ChucNang { ChucNangId = 4, IsActive = true, MaLoaiGianHang = 1 });
            context.CT_ChucNangs.AddOrUpdate(new Models.CSDL.CT_ChucNang { ChucNangId = 4, IsActive = true, MaLoaiGianHang = 2 });
            context.CT_ChucNangs.AddOrUpdate(new Models.CSDL.CT_ChucNang { ChucNangId = 4, IsActive = true, MaLoaiGianHang = 3 });
            context.CT_ChucNangs.AddOrUpdate(new Models.CSDL.CT_ChucNang { ChucNangId = 4, IsActive = true, MaLoaiGianHang = 4 });
            context.CT_ChucNangs.AddOrUpdate(new Models.CSDL.CT_ChucNang { ChucNangId = 5, IsActive = true, MaLoaiGianHang = 1 });
            context.CT_ChucNangs.AddOrUpdate(new Models.CSDL.CT_ChucNang { ChucNangId = 5, IsActive = false, MaLoaiGianHang = 2 });
            context.CT_ChucNangs.AddOrUpdate(new Models.CSDL.CT_ChucNang { ChucNangId = 5, IsActive = true, MaLoaiGianHang = 3 });
            context.CT_ChucNangs.AddOrUpdate(new Models.CSDL.CT_ChucNang { ChucNangId = 5, IsActive = true, MaLoaiGianHang = 4 });
            context.CT_ChucNangs.AddOrUpdate(new Models.CSDL.CT_ChucNang { ChucNangId = 6, IsActive = true, MaLoaiGianHang = 1 });
            context.CT_ChucNangs.AddOrUpdate(new Models.CSDL.CT_ChucNang { ChucNangId = 6, IsActive = false, MaLoaiGianHang = 2 });
            context.CT_ChucNangs.AddOrUpdate(new Models.CSDL.CT_ChucNang { ChucNangId = 6, IsActive = false, MaLoaiGianHang = 3 });
            context.CT_ChucNangs.AddOrUpdate(new Models.CSDL.CT_ChucNang { ChucNangId = 6, IsActive = true, MaLoaiGianHang = 4 });
            context.CT_ChucNangs.AddOrUpdate(new Models.CSDL.CT_ChucNang { ChucNangId = 7, IsActive = true, MaLoaiGianHang = 1 });
            context.CT_ChucNangs.AddOrUpdate(new Models.CSDL.CT_ChucNang { ChucNangId = 7, IsActive = false, MaLoaiGianHang = 2 });
            context.CT_ChucNangs.AddOrUpdate(new Models.CSDL.CT_ChucNang { ChucNangId = 7, IsActive = false, MaLoaiGianHang = 3 });
            context.CT_ChucNangs.AddOrUpdate(new Models.CSDL.CT_ChucNang { ChucNangId = 7, IsActive = true, MaLoaiGianHang = 4 });
            context.SaveChanges();

            //Tạo hợp đồng hết hạn sử dụng để test
            context.HopDongChiNhanhs.AddOrUpdate(new Models.CSDL.HopDongChiNhanh { BeginDate = new DateTime(2016, 1, 1), EndDate = new DateTime(2016, 1, 1), MaLoaiGianHang = 2, MaCN = "GianHang-HetHan" });
            context.SaveChanges();
            //Tạo Admin sử dụng hợp đồng hết hạn để test
            if (userManager.FindByName("HetHan") == null)
            {
                var admin = new ApplicationUser { UserName = "HetHan", FullName = "Bùi Ngọc Minh Huy", MaCN = "GianHang-HetHan", Email = "huy751574@gmail.com", Address = "74 Trung Nghĩa, Xuân Trường, Xuân Lộc, Đồng Nai", PhoneNumber = "01676293913", Birthday = new DateTime(1994, 1, 14), CMND = "272490997", Active = true, Experience = 0, NgayDangKy = new DateTime(2016, 1, 1) };

                userManager.Create(admin, "Huy0908529946");
                userManager.AddToRole(admin.Id, "Admin");
            }

            //Tạo hợp đồng còn hạn sử dụng 
            context.HopDongChiNhanhs.AddOrUpdate(new Models.CSDL.HopDongChiNhanh { BeginDate = DateTime.Now, EndDate = DateTime.Now.AddDays(30), MaLoaiGianHang = 2, MaCN = "GianHang-ConHan" });
            context.SaveChanges();
            //Tạo Admin sử dụng hợp đồng hết hạn để test
            if (userManager.FindByName("ConHan") == null)
            {
                var admin = new ApplicationUser { UserName = "ConHan", FullName = "Bùi Ngọc Minh Huy", MaCN = "GianHang-ConHan", Email = "huy751574@gmail.com", Address = "74 Trung Nghĩa, Xuân Trường, Xuân Lộc, Đồng Nai", PhoneNumber = "01676293913", Birthday = new DateTime(1994, 1, 14), CMND = "272490997", Active = true, Experience = 0, NgayDangKy = DateTime.Now };

                userManager.Create(admin, "Huy0908529946");
                userManager.AddToRole(admin.Id, "Admin");
            }
            //Tạo tài khoản khách hàng
            db.KhachHangs.AddOrUpdate(new Models.CSDL.KhachHang { FullName = "Bùi Ngọc Minh Huy", Address = "74 Trung Nghĩa, Xuân Trường, Xuân Lộc, Đồng Nai", Email = "huy751574@ymail.com", MaCN = "ChiNhanh-Hutech", SDT = "01676293913", ExpVip = 0, Vip = 0, CMND = "272490997" });
            db.SaveChanges();
            //Tạo quy cách
            db.QuyCachs.AddOrUpdate(new Models.CSDL.QuyCach { Name = "Chiếc", MaCN = "ChiNhanh-Hutech" });
            db.QuyCachs.AddOrUpdate(new Models.CSDL.QuyCach { Name = "Quả", MaCN = "ChiNhanh-Hutech" });
            db.QuyCachs.AddOrUpdate(new Models.CSDL.QuyCach { Name = "Kg", MaCN = "ChiNhanh-Hutech" });
            db.QuyCachs.AddOrUpdate(new Models.CSDL.QuyCach { Name = "Hũ", MaCN = "ChiNhanh-Hutech" });
            db.QuyCachs.AddOrUpdate(new Models.CSDL.QuyCach { Name = "Lọ", MaCN = "ChiNhanh-Hutech" });
            db.QuyCachs.AddOrUpdate(new Models.CSDL.QuyCach { Name = "Chai", MaCN = "ChiNhanh-Hutech" });
            db.QuyCachs.AddOrUpdate(new Models.CSDL.QuyCach { Name = "Con", MaCN = "ChiNhanh-Hutech" });
            db.QuyCachs.AddOrUpdate(new Models.CSDL.QuyCach { Name = "Nồi", MaCN = "ChiNhanh-Hutech" });
            db.QuyCachs.AddOrUpdate(new Models.CSDL.QuyCach { Name = "Lít", MaCN = "ChiNhanh-Hutech" });
            db.QuyCachs.AddOrUpdate(new Models.CSDL.QuyCach { Name = "Lốc", MaCN = "ChiNhanh-Hutech" });
            db.QuyCachs.AddOrUpdate(new Models.CSDL.QuyCach { Name = "Thùng", MaCN = "ChiNhanh-Hutech" });
            db.QuyCachs.AddOrUpdate(new Models.CSDL.QuyCach { Name = "Hộp", MaCN = "ChiNhanh-Hutech" });

            //Tạo loại cấp 1
            db.LoaiCap1s.AddOrUpdate(new Models.CSDL.LoaiCap1 { Name = "Thực Phẩm", MaCN = "ChiNhanh-Hutech" });
            db.LoaiCap1s.AddOrUpdate(new Models.CSDL.LoaiCap1 { Name = "Điện-Điện tử", MaCN = "ChiNhanh-Hutech" });
            db.LoaiCap1s.AddOrUpdate(new Models.CSDL.LoaiCap1 { Name = "Thời trang", MaCN = "ChiNhanh-Hutech" });
            db.LoaiCap1s.AddOrUpdate(new Models.CSDL.LoaiCap1 { Name = "Khác", MaCN = "ChiNhanh-Hutech" });
            db.SaveChanges();
            //Tạo loại cấp 2
            db.LoaiCap2s.AddOrUpdate(new Models.CSDL.LoaiCap2 { Name = "Thời trang nam", LoaiCap1Id = 3, MaCN = "ChiNhanh-Hutech" });
            db.LoaiCap2s.AddOrUpdate(new Models.CSDL.LoaiCap2 { Name = "Thời trang nữ", LoaiCap1Id = 3, MaCN = "ChiNhanh-Hutech" });
            db.LoaiCap2s.AddOrUpdate(new Models.CSDL.LoaiCap2 { Name = "Tươi sống", LoaiCap1Id = 1, MaCN = "ChiNhanh-Hutech" });
            db.LoaiCap2s.AddOrUpdate(new Models.CSDL.LoaiCap2 { Name = "Sơ chế-Đông lạnh", LoaiCap1Id = 1, MaCN = "ChiNhanh-Hutech" });
            db.LoaiCap2s.AddOrUpdate(new Models.CSDL.LoaiCap2 { Name = "Điện gia dụng", LoaiCap1Id = 2, MaCN = "ChiNhanh-Hutech" });
            db.LoaiCap2s.AddOrUpdate(new Models.CSDL.LoaiCap2 { Name = "Điện tử", LoaiCap1Id = 2, MaCN = "ChiNhanh-Hutech" });
            db.LoaiCap2s.AddOrUpdate(new Models.CSDL.LoaiCap2 { Name = "Điện máy", LoaiCap1Id = 2, MaCN = "ChiNhanh-Hutech" });
            db.SaveChanges();


            //Tạo loại cấp 3
            /*1*/
            db.LoaiCap3s.AddOrUpdate(new Models.CSDL.LoaiCap3 { Name = "Quần Nam", LoaiCap2Id = 1, MaCN = "ChiNhanh-Hutech", PropertyNames = "SKU|Model|Chất Liệu|Hoàn cảnh|Kích thước sản phẩm (D x R x C cm)|Trọng lượng (KG)|Sản xuất tại" });
            /*2*/
            db.LoaiCap3s.AddOrUpdate(new Models.CSDL.LoaiCap3 { Name = "Áo Sơ Mi Nam", LoaiCap2Id = 1, MaCN = "ChiNhanh-Hutech", PropertyNames = "SKU|Chất liệu vải|Model|Kích thước sản phẩm (D x R x C cm)|Trọng lượng (KG)|Sản xuất tại" });
            /*3*/
            db.LoaiCap3s.AddOrUpdate(new Models.CSDL.LoaiCap3 { Name = "Đồng Hồ Nam", LoaiCap2Id = 1, MaCN = "ChiNhanh-Hutech", PropertyNames = "Thương hiệu|Kiểu máy|Chất liệu vỏ đồng hồ|Chất liệu mặt trước|Loại dây|Kích thước mặt|Độ dày mặt|Kích thước dây|Số kim|Độ chịu nước" });

            /*4*/
            db.LoaiCap3s.AddOrUpdate(new Models.CSDL.LoaiCap3 { Name = "Quần Nữ", LoaiCap2Id = 2, MaCN = "ChiNhanh-Hutech", PropertyNames = "SKU|Model|Chất liệu|Kiểu dáng|Kích thước sản phẩm (D x R x C cm)|Trọng lượng (KG)|Sản xuất tại" });
            /*5*/
            db.LoaiCap3s.AddOrUpdate(new Models.CSDL.LoaiCap3 { Name = "Áo Thun Nữ", LoaiCap2Id = 2, MaCN = "ChiNhanh-Hutech", PropertyNames = "SKU|Model|Chất liệu|Kiểu Dáng|Kích thước sản phẩm (D x R x C cm)|Trọng lượng (KG)|Sản xuất tại" });
            /*6*/
            db.LoaiCap3s.AddOrUpdate(new Models.CSDL.LoaiCap3 { Name = "Váy", LoaiCap2Id = 2, MaCN = "ChiNhanh-Hutech", PropertyNames = "Chất Liệu|Loại Khóa|Lót Trong|Co giản" });
            /*7*/
            db.LoaiCap3s.AddOrUpdate(new Models.CSDL.LoaiCap3 { Name = "Đầm", LoaiCap2Id = 2, MaCN = "ChiNhanh-Hutech", PropertyNames = "Chất liệu|Loại cổ|Loại Tay|Dạng Khóa|Dáng|Lót Trong" });
            /*8*/
            db.LoaiCap3s.AddOrUpdate(new Models.CSDL.LoaiCap3 { Name = "Đồng Hồ Nữ", LoaiCap2Id = 2, MaCN = "ChiNhanh-Hutech", PropertyNames = "Thương hiệu|Kiểu máy|Chất liệu vỏ đồng hồ|Chất liệu mặt trước|Loại dây|Kích thước mặt|Độ dày mặt|Kích thước dây|Số kim|Độ chịu nước" });

            /*9*/
            db.LoaiCap3s.AddOrUpdate(new Models.CSDL.LoaiCap3 { Name = "Thịt", LoaiCap2Id = 3, MaCN = "ChiNhanh-Hutech", PropertyNames = "Thành Phần" });
            /*10*/
            db.LoaiCap3s.AddOrUpdate(new Models.CSDL.LoaiCap3 { Name = "Trứng", LoaiCap2Id = 3, MaCN = "ChiNhanh-Hutech", PropertyNames = "Thành Phần" });

            /*11*/
            db.LoaiCap3s.AddOrUpdate(new Models.CSDL.LoaiCap3 { Name = "Cá", LoaiCap2Id = 4, MaCN = "ChiNhanh-Hutech", PropertyNames = "Thành Phần" });

            /*12*/
            db.LoaiCap3s.AddOrUpdate(new Models.CSDL.LoaiCap3 { Name = "Bếp Điện", LoaiCap2Id = 5, MaCN = "ChiNhanh-Hutech", PropertyNames = "Loại bếp|Chất liệu mặt bếp|Công suất|Chế độ nấu|Hẹn giờ|Bảng điều khiển|Khóa an toàn|Đặc điểm nổi bật|Loại nồi nấu|Kích thước (Dài x Rộng x Cao)|Khối lượng|Thương hiệu" });
            /*13*/
            db.LoaiCap3s.AddOrUpdate(new Models.CSDL.LoaiCap3 { Name = "Lò Vi Sóng", LoaiCap2Id = 5, MaCN = "ChiNhanh-Hutech", PropertyNames = "Loại lò|Dung tích|Công suất|Chức năng chính|Công nghệ inverter|Bảng điều khiển|Khoang lò|Hẹn giờ|Chuông báo|Màn hình hiển thị|Chức năng khác|Khóa an toàn|Kích thước bên ngoài (RxCxS) cm|Trọng lượng (kg)|Thương hiệu" });

            /*14*/
            db.LoaiCap3s.AddOrUpdate(new Models.CSDL.LoaiCap3 { Name = "Điện Thoại", LoaiCap2Id = 6, MaCN = "ChiNhanh-Hutech", PropertyNames = "Màn hình|Hệ điều hành|CPU|RAM|Camera sau|Dung lượng pin|Camera trước|Chip đồ hoạ|Bộ nhớ trong|Hỗ trợ thẻ tối đa|Số khe SIM|Kết nối" });
            /*15*/
            db.LoaiCap3s.AddOrUpdate(new Models.CSDL.LoaiCap3 { Name = "Ti Vi", LoaiCap2Id = 6, MaCN = "ChiNhanh-Hutech", PropertyNames = "Loại TiVi|Màn Hình|Độ Phân Giải|Tần Số Quét|Kết Nối|Công Suất Loa|Góc Nhìn|Điều khiển bằng cử chỉ|Kết nối Bàn phím, Chuột|Điều khiển bằng giọng nói|Nhận diện khuôn mặt" });
            /*16*/
            db.LoaiCap3s.AddOrUpdate(new Models.CSDL.LoaiCap3 { Name = "TABLET", LoaiCap2Id = 6, MaCN = "ChiNhanh-Hutech", PropertyNames = "Màn hình|Hệ điều hành|CPU|RAM|Bộ nhớ trong|Camera sau|Camera trước|3G|Wifi|Hỗ trợ SIM|Đàm thoại|Dung lượng pin|Trọng lượng" });
            /*17*/
            db.LoaiCap3s.AddOrUpdate(new Models.CSDL.LoaiCap3 { Name = "Laptop", LoaiCap2Id = 6, MaCN = "ChiNhanh-Hutech", PropertyNames = "CPU|RAM|Đĩa cứng|Màn hình rộng|Cảm ứng|Đồ họa|Đĩa quang|Webcam|Chất liệu vỏ|Cổng giao tiếp|Kết nối khác|PIN/Battery|Trọng lượng( kg)" });

            /*18*/
            db.LoaiCap3s.AddOrUpdate(new Models.CSDL.LoaiCap3 { Name = "Tủ Lạnh", LoaiCap2Id = 7, MaCN = "ChiNhanh-Hutech", PropertyNames = "Kiểu tủ|Số cửa|Dung tích tổng|Dung tích sử dụng|Dung tích ngăn lạnh|Dung tích ngăn đá|Máy nén Inverter|Chất liệu khay ngăn|Chất liệu vỏ tủ lạnh|Số người sử dụng|Công suất tiêu thụ|Kích thước (Cao x Rộng x Sâu)|Khối lượng" });
            /*19*/
            db.LoaiCap3s.AddOrUpdate(new Models.CSDL.LoaiCap3 { Name = "Máy Lạnh", LoaiCap2Id = 7, MaCN = "ChiNhanh-Hutech", PropertyNames = "Loại máy|Số ngựa|Công suất làm lạnh (BTU)|Công suất tiêu thụ|Phạm vi làm lạnh hiệu quả|Công nghệ Inverter|Loại Gas sử dụng|Kích thước cục lạnh (Dài x Cao x Dày)|Kích thước cục nóng (Dài x Cao x Dày)|Khối lượng cục lạnh|Khối lượng cục nóng|Chế độ làm lạnh nhanh|Kháng khuẩn khử mùi|Chế độ gió|Chế độ hẹn giờ|Tự khởi động khi có điện lại" });
            db.SaveChanges();

            //Tạo mặt hàng
            /*1*/
            db.MatHangs.AddOrUpdate(new Models.CSDL.MatHang { Name = "Samsung Galaxy S6 32GB", LoaiCap3Id = 14, QuyCachId = 1, AuthorName = "Hàn Quốc", Propertynames = "HD 4|Android 4x|ATX|2|3|3000|5|ATX|15|0|2|3G|", Price = 14490000, Reduction = 0, VAT = 0, Directory = "samsung_S6", Image = "0.png", MaCN = "ChiNhanh-Hutech", IsUse = true });
            /*2*/
            db.MatHangs.AddOrUpdate(new Models.CSDL.MatHang { Name = "Samsung Galaxy S6 16GB", LoaiCap3Id = 14, QuyCachId = 1, AuthorName = "Hàn Quốc", Propertynames = "HD 4|Android 4x|ATX|2|3|3000|5|ATX|15|0|2|3G|", Price = 9000000, Reduction = 0, VAT = 0, Directory = "samsung_S6", Image = "0.png", MaCN = "ChiNhanh-Hutech", IsUse = true });
            /*3*/
            db.MatHangs.AddOrUpdate(new Models.CSDL.MatHang { Name = "Samsung Galaxy S6 128GB", LoaiCap3Id = 14, QuyCachId = 1, AuthorName = "Hàn Quốc", Propertynames = "HD 4|Android 4x|ATX|2|3|3000|5|ATX|15|0|2|3G|", Price = 20000000, Reduction = 0, VAT = 0, Directory = "samsung_S6", Image = "0.png", MaCN = "ChiNhanh-Hutech", IsUse = true });
            /*4*/
            db.MatHangs.AddOrUpdate(new Models.CSDL.MatHang { Name = "Smart Tivi LED Samsung", LoaiCap3Id = 15, QuyCachId = 1, AuthorName = "Việt Nam", Propertynames = "HD 4|Android 4x|ATX|2|3|3000|5|ATX|15|0|2|3G|", Price = 24900000, Reduction = 0, VAT = 0, Directory = "TV_SS_55", Image = "0.png", MaCN = "ChiNhanh-Hutech", IsUse = true });
            /*5*/
            db.MatHangs.AddOrUpdate(new Models.CSDL.MatHang { Name = "Đầm Mini Phối Ren", LoaiCap3Id = 7, QuyCachId = 1, AuthorName = "ZALORA", Propertynames = "HD 4|Android 4x|ATX|2|3|3000|5|ATX|15|0|2|3G|", Price = 699000, Reduction = 0, VAT = 0, Directory = "damminiphoiren", Image = "0.png", MaCN = "ChiNhanh-Hutech", IsUse = true });
            /*6*/
            db.MatHangs.AddOrUpdate(new Models.CSDL.MatHang { Name = "Đầm Thời Trang Festive", LoaiCap3Id = 7, QuyCachId = 1, AuthorName = "ZALORA", Propertynames = "HD 4|Android 4x|ATX|2|3|3000|5|ATX|15|0|2|3G|", Price = 649000, Reduction = 0, VAT = 0, Directory = "damthoitrangfestive", Image = "0.png", MaCN = "ChiNhanh-Hutech", IsUse = true });
            /*7*/
            db.MatHangs.AddOrUpdate(new Models.CSDL.MatHang { Name = "Váy Hoa Lai Xéo", LoaiCap3Id = 6, QuyCachId = 1, AuthorName = "Milaross", Propertynames = "HD 4|Android 4x|ATX|2|3|3000|5|ATX|15|0|2|3G|", Price = 229000, Reduction = 0, VAT = 0, Directory = "vayhoalaixeo", Image = "0.png", MaCN = "ChiNhanh-Hutech", IsUse = true });
            /*8*/
            db.MatHangs.AddOrUpdate(new Models.CSDL.MatHang { Name = "iPad Air 2 Cellular 64GB", LoaiCap3Id = 16, QuyCachId = 1, AuthorName = "Đang cập Nhật...", Propertynames = "HD 4|Android 4x|ATX|2|3|3000|5|ATX|15|0|2|3G|", Price = 17590000, Reduction = 0, VAT = 0, Directory = "iPad Air 2", Image = "0.png", MaCN = "ChiNhanh-Hutech", IsUse = true });
            /*9*/
            db.MatHangs.AddOrUpdate(new Models.CSDL.MatHang { Name = "Asus TP500LB", LoaiCap3Id = 17, QuyCachId = 1, AuthorName = "Đài Loan", Propertynames = "HD 4|Android 4x|ATX|2|3|3000|5|ATX|15|0|2|3G|", Price = 15890000, Reduction = 0, VAT = 0, Directory = "Asus TP500LB", Image = "0.png", MaCN = "ChiNhanh-Hutech", IsUse = true });
            /*10*/
            db.MatHangs.AddOrUpdate(new Models.CSDL.MatHang { Name = "Thịt gà bản", LoaiCap3Id = 9, QuyCachId = 3, AuthorName = "Việt Nam", Propertynames = "HD 4|Android 4x|ATX|2|3|3000|5|ATX|15|0|2|3G|", Price = 235000, Reduction = 0, VAT = 0, Directory = "thitgaban", Image = "0.png", MaCN = "ChiNhanh-Hutech", IsUse = true });
            /*11*/
            db.MatHangs.AddOrUpdate(new Models.CSDL.MatHang { Name = "Trứng Vịt Trang trại Biggreen", LoaiCap3Id = 10, QuyCachId = 1, AuthorName = "Trang trại Biggreen", Propertynames = "HD 4|Android 4x|ATX|2|3|3000|5|ATX|15|0|2|3G|", Price = 4800, Reduction = 0, VAT = 0, Directory = "trungvitbiggreen", Image = "0.png", MaCN = "ChiNhanh-Hutech", IsUse = true });
            /*12*/
            db.MatHangs.AddOrUpdate(new Models.CSDL.MatHang { Name = "Thịt Lợn Bản Sơn La", LoaiCap3Id = 9, QuyCachId = 2, AuthorName = "Sơn La", Propertynames = "HD 4|Android 4x|ATX|2|3|3000|5|ATX|15|0|2|3G|", Price = 265000, Reduction = 0, VAT = 0, Directory = "thitlonbansonla", Image = "0.png", MaCN = "ChiNhanh-Hutech", IsUse = true });
            /*13*/
            db.MatHangs.AddOrUpdate(new Models.CSDL.MatHang { Name = "Cá Lăng kho tộ", LoaiCap3Id = 11, QuyCachId = 3, AuthorName = "Đang cập Nhật...", Propertynames = "HD 4|Android 4x|ATX|2|3|3000|5|ATX|15|0|2|3G|", Price = 250000, Reduction = 0, VAT = 0, Directory = "calangkhoto", Image = "0.png", MaCN = "ChiNhanh-Hutech", IsUse = true });
            /*14*/
            db.MatHangs.AddOrUpdate(new Models.CSDL.MatHang { Name = "Đồng hồ Skmei SK069", LoaiCap3Id = 8, QuyCachId = 8, AuthorName = "Việt Nam", Propertynames = "HD 4|Android 4x|ATX|2|3|3000|5|ATX|15|0|2|3G|", Price = 590000, Reduction = 0, VAT = 0, Directory = "dongho_Skmei SK069", Image = "0.png", MaCN = "ChiNhanh-Hutech", IsUse = true });
            /*15*/
            db.MatHangs.AddOrUpdate(new Models.CSDL.MatHang { Name = "iPhone 6 64GB", LoaiCap3Id = 14, QuyCachId = 1, AuthorName = "Mỹ", Propertynames = "HD 4|Android 4x|ATX|2|3|3000|5|ATX|15|0|2|3G|", Price = 18490000, Reduction = 0, VAT = 0, Directory = "iphone_6_64g", Image = "0.png", MaCN = "ChiNhanh-Hutech", IsUse = true });
            /*16*/
            db.MatHangs.AddOrUpdate(new Models.CSDL.MatHang { Name = "Samsung Galaxy Note 5", LoaiCap3Id = 14, QuyCachId = 1, AuthorName = "Hàn Quốc", Propertynames = "HD 4|Android 4x|ATX|2|3|3000|5|ATX|15|0|2|3G|", Price = 17990000, Reduction = 0, VAT = 0, Directory = "samsung_g_note5", Image = "0.png", MaCN = "ChiNhanh-Hutech", IsUse = true });
            /*17*/
            db.MatHangs.AddOrUpdate(new Models.CSDL.MatHang { Name = "OPPO NEO 7", LoaiCap3Id = 14, QuyCachId = 1, AuthorName = "Trung Quốc", Propertynames = "HD 4|Android 4x|ATX|2|3|3000|5|ATX|15|0|2|3G|", Price = 3990000, Reduction = 0, VAT = 0, Directory = "oppo_neo7", Image = "0.png", MaCN = "ChiNhanh-Hutech", IsUse = true });
            /*18*/
            db.MatHangs.AddOrUpdate(new Models.CSDL.MatHang { Name = "HP Pavilion 14 ab019TU i3", LoaiCap3Id = 17, QuyCachId = 1, AuthorName = "Hoa kỳ", Propertynames = "HD 4|Android 4x|ATX|2|3|3000|5|ATX|15|0|2|3G|", Price = 11590000, Reduction = 0, VAT = 0, Directory = "HP Pavilion 14 ab019TU i3", Image = "0.png", MaCN = "ChiNhanh-Hutech", IsUse = true });
            /*19*/
            db.MatHangs.AddOrUpdate(new Models.CSDL.MatHang { Name = "Laptop HP Pavilion X360", LoaiCap3Id = 17, QuyCachId = 1, AuthorName = "Hoa kỳ", Propertynames = "HD 4|Android 4x|ATX|2|3|3000|5|ATX|15|0|2|3G|", Price = 21550000, Reduction = 0, VAT = 0, Directory = "HP Pavilion X360 k026TU M", Image = "0.png", MaCN = "ChiNhanh-Hutech", IsUse = true });
            /*20*/
            db.MatHangs.AddOrUpdate(new Models.CSDL.MatHang { Name = "Laptop HP 14 ac145TU", LoaiCap3Id = 17, QuyCachId = 1, AuthorName = "Hoa kỳ", Propertynames = "HD 4|Android 4x|ATX|2|3|3000|5|ATX|15|0|2|3G|", Price = 7490000, Reduction = 0, VAT = 0, Directory = "Laptop HP 14 ac145TU", Image = "0.png", MaCN = "ChiNhanh-Hutech", IsUse = true });
            /*21*/
            db.MatHangs.AddOrUpdate(new Models.CSDL.MatHang { Name = "Laptop Dell Inspiron 3451", LoaiCap3Id = 17, QuyCachId = 1, AuthorName = "Hoa kỳ", Propertynames = "HD 4|Android 4x|ATX|2|3|3000|5|ATX|15|0|2|3G|", Price = 7190000, Reduction = 0, VAT = 0, Directory = "Dell Inspiron 3451", Image = "0.png", MaCN = "ChiNhanh-Hutech", IsUse = true });
            /*22*/
            db.MatHangs.AddOrUpdate(new Models.CSDL.MatHang { Name = " Laptop Acer Aspire E5 473 i3", LoaiCap3Id = 17, QuyCachId = 1, AuthorName = "Hoa kỳ", Propertynames = "HD 4|Android 4x|ATX|2|3|3000|5|ATX|15|0|2|3G|", Price = 8890000, Reduction = 0, VAT = 0, Directory = "Acer Aspire E5 473 i3", Image = "0.png", MaCN = "ChiNhanh-Hutech", IsUse = true });
            /*23*/
            db.MatHangs.AddOrUpdate(new Models.CSDL.MatHang { Name = "Laptop Dell Inspiron 5458 i3", LoaiCap3Id = 17, QuyCachId = 1, AuthorName = "Hoa kỳ", Propertynames = "HD 4|Android 4x|ATX|2|3|3000|5|ATX|15|0|2|3G|", Price = 12190000, Reduction = 0, VAT = 0, Directory = "Dell Inspiron 5458 i3", Image = "0.png", MaCN = "ChiNhanh-Hutech", IsUse = true });
            /*24*/
            db.MatHangs.AddOrUpdate(new Models.CSDL.MatHang { Name = "Laptop Asus TP300LA i5", LoaiCap3Id = 17, QuyCachId = 1, AuthorName = "Hoa kỳ", Propertynames = "HD 4|Android 4x|ATX|2|3|3000|5|ATX|15|0|2|3G|", Price = 14490000, Reduction = 0, VAT = 0, Directory = "Asus TP300LA i5", Image = "0.png", MaCN = "ChiNhanh-Hutech", IsUse = true });
            /*25*/
            db.MatHangs.AddOrUpdate(new Models.CSDL.MatHang { Name = "Laptop Asus N551JX i7", LoaiCap3Id = 17, QuyCachId = 1, AuthorName = "Hoa kỳ", Propertynames = "HD 4|Android 4x|ATX|2|3|3000|5|ATX|15|0|2|3G|", Price = 27590000, Reduction = 0, VAT = 0, Directory = "Asus N551JX i7", Image = "0.png", MaCN = "ChiNhanh-Hutech", IsUse = true });
            /*26*/
            db.MatHangs.AddOrUpdate(new Models.CSDL.MatHang { Name = "Laptop Dell Inspiron 5458 i7", LoaiCap3Id = 17, QuyCachId = 1, AuthorName = "Hoa kỳ", Propertynames = "HD 4|Android 4x|ATX|2|3|3000|5|ATX|15|0|2|3G|", Price = 18890000, Reduction = 0, VAT = 0, Directory = "Laptop Dell Inspiron 5458 i7", Image = "0.png", MaCN = "ChiNhanh-Hutech", IsUse = true });
            /*27*/
            db.MatHangs.AddOrUpdate(new Models.CSDL.MatHang { Name = "iPad Air 2 Cellular 16GB", LoaiCap3Id = 16, QuyCachId = 1, AuthorName = "Hoa kỳ", Propertynames = "HD 4|Android 4x|ATX|2|3|3000|5|ATX|15|0|2|3G|", Price = 15590000, Reduction = 0, VAT = 0, Directory = "iPad Air 2 Cellular 16GB", Image = "0.png", MaCN = "ChiNhanh-Hutech", IsUse = true });
            /*28*/
            db.MatHangs.AddOrUpdate(new Models.CSDL.MatHang { Name = "Samsung Galaxy Tab S2 9.7", LoaiCap3Id = 16, QuyCachId = 1, AuthorName = "Hàn Quốc", Propertynames = "HD 4|Android 4x|ATX|2|3|3000|5|ATX|15|0|2|3G|", Price = 13990000, Reduction = 0, VAT = 0, Directory = "Samsung Galaxy Tab S2", Image = "0.png", MaCN = "ChiNhanh-Hutech", IsUse = true });
            /*29*/
            db.MatHangs.AddOrUpdate(new Models.CSDL.MatHang { Name = "iPad Mini 3 Retina Cellular 16GB", LoaiCap3Id = 16, QuyCachId = 1, AuthorName = "Hàn Quốc", Propertynames = "HD 4|Android 4x|ATX|2|3|3000|5|ATX|15|0|2|3G|", Price = 12990000, Reduction = 0, VAT = 0, Directory = "iPad Mini 3 Retina Cellular 16GB", Image = "0.png", MaCN = "ChiNhanh-Hutech", IsUse = true });
            /*30*/
            db.MatHangs.AddOrUpdate(new Models.CSDL.MatHang { Name = "Ipad Air 2 Wifi 16GB", LoaiCap3Id = 16, QuyCachId = 1, AuthorName = "Hoa kỳ", Propertynames = "HD 4|Android 4x|ATX|2|3|3000|5|ATX|15|0|2|3G|", Price = 11990000, Reduction = 0, VAT = 0, Directory = "Ipad Air 2 Wifi 16GB", Image = "0.png", MaCN = "ChiNhanh-Hutech", IsUse = true });
            /*31*/
            db.MatHangs.AddOrUpdate(new Models.CSDL.MatHang { Name = "Samsung Galaxy Tab S2 8", LoaiCap3Id = 16, QuyCachId = 1, AuthorName = "Hàn Quốc", Propertynames = "HD 4|Android 4x|ATX|2|3|3000|5|ATX|15|0|2|3G|", Price = 11990000, Reduction = 0, VAT = 0, Directory = "Samsung Galaxy Tab S2 8", Image = "0.png", MaCN = "ChiNhanh-Hutech", IsUse = true });
            /*32*/
            db.MatHangs.AddOrUpdate(new Models.CSDL.MatHang { Name = "iPad Mini 4 Wifi 64GB", LoaiCap3Id = 16, QuyCachId = 1, AuthorName = "Hoa kỳ", Propertynames = "HD 4|Android 4x|ATX|2|3|3000|5|ATX|15|0|2|3G|", Price = 11990000, Reduction = 0, VAT = 0, Directory = "iPad Mini 4 Wifi 64GB", Image = "0.png", MaCN = "ChiNhanh-Hutech", IsUse = true });
            /*33*/
            db.MatHangs.AddOrUpdate(new Models.CSDL.MatHang { Name = "iPad Mini 4 Wifi 16GB", LoaiCap3Id = 16, QuyCachId = 1, AuthorName = "Hoa kỳ", Propertynames = "HD 4|Android 4x|ATX|2|3|3000|5|ATX|15|0|2|3G|", Price = 9900000, Reduction = 0, VAT = 0, Directory = "iPad Mini 4 Wifi 16GB", Image = "0.png", MaCN = "ChiNhanh-Hutech", IsUse = true });
            /*34*/
            db.MatHangs.AddOrUpdate(new Models.CSDL.MatHang { Name = "iPad Air Wifi 16GB", LoaiCap3Id = 16, QuyCachId = 1, AuthorName = "Hoa kỳ", Propertynames = "HD 4|Android 4x|ATX|2|3|3000|5|ATX|15|0|2|3G|", Price = 9590000, Reduction = 0, VAT = 0, Directory = "iPad Air Wifi 16GB", Image = "0.png", MaCN = "ChiNhanh-Hutech", IsUse = true });
            /*35*/
            db.MatHangs.AddOrUpdate(new Models.CSDL.MatHang { Name = "Samsung Galaxy Tab A Plus 9.7", LoaiCap3Id = 16, QuyCachId = 1, AuthorName = "Hàn Quốc", Propertynames = "HD 4|Android 4x|ATX|2|3|3000|5|ATX|15|0|2|3G|", Price = 8990000, Reduction = 0, VAT = 0, Directory = "Samsung Galaxy Tab A Plus", Image = "0.png", MaCN = "ChiNhanh-Hutech", IsUse = true });
            /*36*/
            db.MatHangs.AddOrUpdate(new Models.CSDL.MatHang { Name = "HTC One A9", LoaiCap3Id = 14, QuyCachId = 1, AuthorName = "Đài Loan", Propertynames = "HD 4|Android 4x|ATX|2|3|3000|5|ATX|15|0|2|3G|", Price = 11990000, Reduction = 0, VAT = 0, Directory = "HTC One A9", Image = "0.png", MaCN = "ChiNhanh-Hutech", IsUse = true });
            /*37*/
            db.MatHangs.AddOrUpdate(new Models.CSDL.MatHang { Name = "HTC Desire 626G", LoaiCap3Id = 14, QuyCachId = 1, AuthorName = "Đài Loan", Propertynames = "HD 4|Android 4x|ATX|2|3|3000|5|ATX|15|0|2|3G|", Price = 4290000, Reduction = 0, VAT = 0, Directory = "HTC Desire 626G", Image = "0.png", MaCN = "ChiNhanh-Hutech", IsUse = true });
            /*38*/
            db.MatHangs.AddOrUpdate(new Models.CSDL.MatHang { Name = "Đồng hồ nam dây da Sinobi SI024", LoaiCap3Id = 3, QuyCachId = 1, AuthorName = "Đài Loan", Propertynames = "HD 4|Android 4x|ATX|2|3|3000|5|ATX|15|0|2|3G|", Price = 319000, Reduction = 0, VAT = 0, Directory = "Sinobi SI024", Image = "0.png", MaCN = "ChiNhanh-Hutech", IsUse = true });
            /*39*/
            db.MatHangs.AddOrUpdate(new Models.CSDL.MatHang { Name = "Đồng hồ Citizen AG8352-59E", LoaiCap3Id = 3, QuyCachId = 1, AuthorName = "Đài Loan", Propertynames = "HD 4|Android 4x|ATX|2|3|3000|5|ATX|15|0|2|3G|", Price = 4260000, Reduction = 0, VAT = 0, Directory = "Citizen AG8352-59E", Image = "0.png", MaCN = "ChiNhanh-Hutech", IsUse = true });
            /*40*/
            db.MatHangs.AddOrUpdate(new Models.CSDL.MatHang { Name = "TISSOT FLAMINGO T094.210.33.111.00", LoaiCap3Id = 8, QuyCachId = 1, AuthorName = "Đài Loan", Propertynames = "HD 4|Android 4x|ATX|2|3|3000|5|ATX|15|0|2|3G|", Price = 12400000, Reduction = 0, VAT = 0, Directory = "TISSOT FLAMINGO T094", Image = "0.png", MaCN = "ChiNhanh-Hutech", IsUse = true });
            /*41*/
            db.MatHangs.AddOrUpdate(new Models.CSDL.MatHang { Name = "Quần dài thể thao Titishop QD17", LoaiCap3Id = 1, QuyCachId = 1, AuthorName = "Nhật Bản", Propertynames = "HD 4|Android 4x|ATX|2|3|3000|5|ATX|15|0|2|3G|", Price = 179000, Reduction = 0, VAT = 0, Directory = "Titishop QD17", Image = "0.png", MaCN = "ChiNhanh-Hutech", IsUse = true });
            /*42*/
            db.MatHangs.AddOrUpdate(new Models.CSDL.MatHang { Name = "Quần kaki nam SAIGONBOY K30", LoaiCap3Id = 1, QuyCachId = 1, AuthorName = "Thụy Sỹ", Propertynames = "HD 4|Android 4x|ATX|2|3|3000|5|ATX|15|0|2|3G|", Price = 199000, Reduction = 0, VAT = 0, Directory = "SAIGONBOY K30", Image = "0.png", MaCN = "ChiNhanh-Hutech", IsUse = true });
            /*43*/
            db.MatHangs.AddOrUpdate(new Models.CSDL.MatHang { Name = "Quần nam H2T Q-1415", LoaiCap3Id = 1, QuyCachId = 1, AuthorName = "Việt nam", Propertynames = "HD 4|Android 4x|ATX|2|3|3000|5|ATX|15|0|2|3G|", Price = 820000, Reduction = 0, VAT = 0, Directory = "H2T Q-1415", Image = "0.png", MaCN = "ChiNhanh-Hutech", IsUse = true });
            /*44*/
            db.MatHangs.AddOrUpdate(new Models.CSDL.MatHang { Name = "Quần âu nam Prazenta I-QAM10", LoaiCap3Id = 1, QuyCachId = 1, AuthorName = "Việt nam", Propertynames = "HD 4|Android 4x|ATX|2|3|3000|5|ATX|15|0|2|3G|", Price = 219000, Reduction = 0, VAT = 0, Directory = "Prazenta I-QAM10", Image = "0.png", MaCN = "ChiNhanh-Hutech", IsUse = true });
            /*45*/
            db.MatHangs.AddOrUpdate(new Models.CSDL.MatHang { Name = "Áo sơ mi nam Mốt Trẻ BTD142", LoaiCap3Id = 2, QuyCachId = 1, AuthorName = "Việt nam", Propertynames = "HD 4|Android 4x|ATX|2|3|3000|5|ATX|15|0|2|3G|", Price = 299000, Reduction = 0, VAT = 0, Directory = "BTD142", Image = "0.png", MaCN = "ChiNhanh-Hutech", IsUse = true });
            /*46*/
            db.MatHangs.AddOrUpdate(new Models.CSDL.MatHang { Name = "Quần kaki nữ Aponi N0310", LoaiCap3Id = 4, QuyCachId = 1, AuthorName = "Việt nam", Propertynames = "HD 4|Android 4x|ATX|2|3|3000|5|ATX|15|0|2|3G|", Price = 279000, Reduction = 0, VAT = 0, Directory = "Aponi N0310", Image = "0.png", MaCN = "ChiNhanh-Hutech", IsUse = true });
            /*47*/
            db.MatHangs.AddOrUpdate(new Models.CSDL.MatHang { Name = "Áo thun sọc", LoaiCap3Id = 5, QuyCachId = 1, AuthorName = "Việt nam", Propertynames = "HD 4|Android 4x|ATX|2|3|3000|5|ATX|15|0|2|3G|", Price = 339000, Reduction = 0, VAT = 0, Directory = "aothunsoc", Image = "0.png", MaCN = "ChiNhanh-Hutech", IsUse = true });
            /*48*/
            db.MatHangs.AddOrUpdate(new Models.CSDL.MatHang { Name = "Sharp AH-A9PEWS 1 Hp", LoaiCap3Id = 19, QuyCachId = 1, AuthorName = "Việt nam", Propertynames = "HD 4|Android 4x|ATX|2|3|3000|5|ATX|15|0|2|3G|", Price = 6190000, Reduction = 0, VAT = 0, Directory = "Sharp AH-A9PEWS", Image = "0.png", MaCN = "ChiNhanh-Hutech", IsUse = true });
            /*49*/
            db.MatHangs.AddOrUpdate(new Models.CSDL.MatHang { Name = "Sanyo SR-U185PN 165 lít", LoaiCap3Id = 18, QuyCachId = 1, AuthorName = "Việt nam", Propertynames = "HD 4|Android 4x|ATX|2|3|3000|5|ATX|15|0|2|3G|", Price = 5090000, Reduction = 0, VAT = 0, Directory = "Sanyo SR-U185PN", Image = "0.png", MaCN = "ChiNhanh-Hutech", IsUse = true });
            db.SaveChanges();



            //Tạo kho hàng
            /*1*/
            db.KhoHangs.AddOrUpdate(new Models.CSDL.KhoHang { Name = "Kho Chính", DiaChi = "300 Điện Biên Phủ, Q.Bình Thạnh, TP.HCM", MaCN = "ChiNhanh-Hutech", SDT = "841234567891" });
            db.SaveChanges();

            //Tạo chi tiết kho hàng
            db.ChiTietKhoHangs.AddOrUpdate(new Models.CSDL.ChiTietKhoHang { ItemId = 1, WarehouseID = 1, Quantities = 50 });
            db.ChiTietKhoHangs.AddOrUpdate(new Models.CSDL.ChiTietKhoHang { ItemId = 2, WarehouseID = 1, Quantities = 50 });
            db.ChiTietKhoHangs.AddOrUpdate(new Models.CSDL.ChiTietKhoHang { ItemId = 3, WarehouseID = 1, Quantities = 50 });
            db.ChiTietKhoHangs.AddOrUpdate(new Models.CSDL.ChiTietKhoHang { ItemId = 4, WarehouseID = 1, Quantities = 50 });
            db.ChiTietKhoHangs.AddOrUpdate(new Models.CSDL.ChiTietKhoHang { ItemId = 5, WarehouseID = 1, Quantities = 50 });
            db.ChiTietKhoHangs.AddOrUpdate(new Models.CSDL.ChiTietKhoHang { ItemId = 6, WarehouseID = 1, Quantities = 50 });
            db.ChiTietKhoHangs.AddOrUpdate(new Models.CSDL.ChiTietKhoHang { ItemId = 7, WarehouseID = 1, Quantities = 50 });
            db.ChiTietKhoHangs.AddOrUpdate(new Models.CSDL.ChiTietKhoHang { ItemId = 8, WarehouseID = 1, Quantities = 50 });
            db.ChiTietKhoHangs.AddOrUpdate(new Models.CSDL.ChiTietKhoHang { ItemId = 9, WarehouseID = 1, Quantities = 50 });
            db.ChiTietKhoHangs.AddOrUpdate(new Models.CSDL.ChiTietKhoHang { ItemId = 10, WarehouseID = 1, Quantities = 50 });
            db.ChiTietKhoHangs.AddOrUpdate(new Models.CSDL.ChiTietKhoHang { ItemId = 11, WarehouseID = 1, Quantities = 50 });
            db.SaveChanges();

            ////Giao diện

        }
        public static void ThemCuaHang(ApplicationUser user, ChiNhanh cn)
        {
            var db = ShopEntities.CreateEntitiesForSpecificDatabaseName(user.MaCN);
            db.Database.CreateIfNotExists();
            db.ChiNhanhs.Add(cn);
            db.SaveChanges();
            ////Tạo các chức năng sử dụng của nhân viên tổng hợp
            //var rolestore = new RoleStore<ApplicationRole>(db);
            //var roleManager = new RoleManager<ApplicationRole>(rolestore);
            var userstore = new UserStore<ApplicationUser>(db);
            var userManager = new UserManager<ApplicationUser>(userstore);

            userManager.Create(user);

            //var role = new ApplicationRole { Name = "Quản lý sản phẩm" };
            //roleManager.Create(role);
            //var role1 = new ApplicationRole { Name = "Quản lý nhà cung cấp" };
            //roleManager.Create(role1);
            //var role2 = new ApplicationRole { Name = "Quản lý kho" };
            //roleManager.Create(role2);
            //var role3 = new ApplicationRole { Name = "Lập hóa đơn" };
            //roleManager.Create(role3);
            //var role4 = new ApplicationRole { Name = "Giao hàng" };
            //roleManager.Create(role4);
            //var role5 = new ApplicationRole { Name = "Quản lý hóa đơn" };
            //roleManager.Create(role5);
            //var role6 = new ApplicationRole { Name = "Lập đơn nhập hàng" };
            //roleManager.Create(role6);
            //var role7 = new ApplicationRole { Name = "Lập đơn trả hàng" };
            //roleManager.Create(role7);
            //var role8 = new ApplicationRole { Name = "Quản lý đơn nhập hàng" };
            //roleManager.Create(role8);
            //var role9 = new ApplicationRole { Name = "Quản lý đơn trả hàng" };
            //roleManager.Create(role9);
            //var role10 = new ApplicationRole { Name = "Quản lý đơn khách hàng" };
            //roleManager.Create(role10);

            //tạo các chức năng cụ thể
            List<ChucNangNhanVien> lst = new List<ChucNangNhanVien>();
            lst.Add(new ChucNangNhanVien { Id = (int)ChucNangNVType.ThemKho, Name = "Thêm kho" });
            lst.Add(new ChucNangNhanVien { Id = (int)ChucNangNVType.UpdateKho, Name = "Thay đổi thông tin kho" });
            lst.Add(new ChucNangNhanVien { Id = (int)ChucNangNVType.ThemMatHang, Name = "Thêm mặt hàng" });
            lst.Add(new ChucNangNhanVien { Id = (int)ChucNangNVType.UpdateMatHang, Name = "Sửa thông tin mặt hàng" });
            lst.Add(new ChucNangNhanVien { Id = (int)ChucNangNVType.XoaMatHang, Name = "Xóa mặt hàng" });
            lst.Add(new ChucNangNhanVien { Id = (int)ChucNangNVType.ChangeStateMatHang, Name = "Thay đổi trạng thái mặt hàng" });
            lst.Add(new ChucNangNhanVien { Id = (int)ChucNangNVType.LapHoaDon, Name = "Lập hóa đơn" });
            lst.Add(new ChucNangNhanVien { Id = (int)ChucNangNVType.ChangeStateDon, Name = "Thay đổi trạng thái đơn" });
          
            lst.Add(new ChucNangNhanVien { Id = (int)ChucNangNVType.ThemNCC, Name = "Thêm nhà cung cấp" });
            lst.Add(new ChucNangNhanVien { Id = (int)ChucNangNVType.UpdateNCC, Name = "Sửa thông tin nhà cung cấp" });
            lst.Add(new ChucNangNhanVien { Id = (int)ChucNangNVType.ThemKhachHang, Name = "Thêm khách hàng" });
            lst.Add(new ChucNangNhanVien { Id = (int)ChucNangNVType.UpdateKhachHang, Name = "Sửa thông tin khách hàng" });
            lst.Add(new ChucNangNhanVien { Id = (int)ChucNangNVType.ThemLoaiCap1, Name = "Thêm loại cấp 1" });
            lst.Add(new ChucNangNhanVien { Id = (int)ChucNangNVType.UpdateLoaiCap1, Name = "Sửa thông tin loại cấp 1" });
            lst.Add(new ChucNangNhanVien { Id = (int)ChucNangNVType.ThemLoaiCap2, Name = "Thêm loại cấp 2" });
            lst.Add(new ChucNangNhanVien { Id = (int)ChucNangNVType.UpdateLoaiCap2, Name = "Sửa thông tin loại cấp 2" });
            lst.Add(new ChucNangNhanVien { Id = (int)ChucNangNVType.ThemLoaiCap3, Name = "Thêm loại cấp 3" });
            lst.Add(new ChucNangNhanVien { Id = (int)ChucNangNVType.UpdateLoaiCap3, Name = "Sửa thông tin loại cấp 3" });
            lst.Add(new ChucNangNhanVien { Id = (int)ChucNangNVType.ThemQuyCach, Name = "Thêm quy cách" });
            lst.Add(new ChucNangNhanVien { Id = (int)ChucNangNVType.UpdateQuyCach, Name = "Sửa thông tin quy cách" });
            lst.Add(new ChucNangNhanVien { Id = (int)ChucNangNVType.ReportDoanhThu, Name = "Lập báo cáo doanh thu" });
            lst.Add(new ChucNangNhanVien { Id = (int)ChucNangNVType.ReportCongNo, Name = "Lập báo cáo công nợ" });
            lst.Add(new ChucNangNhanVien { Id = (int)ChucNangNVType.ReportTonKho, Name = "Lập báo cáo tồn kho" });
            lst.Add(new ChucNangNhanVien { Id = (int)ChucNangNVType.ThemNhapHang, Name = "Thêm đơn nhập hàng" });
            lst.Add(new ChucNangNhanVien { Id = (int)ChucNangNVType.ThemTraHang, Name = "Thêm đơn trả hàng" });
            lst.Add(new ChucNangNhanVien { Id = (int)ChucNangNVType.ChangeStateNhapHang, Name = "Đổi trạng thái đơn nhập hàng" });
            lst.Add(new ChucNangNhanVien { Id = (int)ChucNangNVType.ChangeStateTraHang, Name = "Đổi trạng thái đơn trả hàng" });
 
            db.ChucNangNhanViens.AddRange(lst);
            db.SaveChanges();

          

        }
    }
}
