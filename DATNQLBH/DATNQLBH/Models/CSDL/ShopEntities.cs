using DATNQLBH.Models.CSDL;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DATNQLBH.Models
{
    public class ShopEntities:IdentityDbContext
    {
       
        public ShopEntities(string connectionString="ShopEntities") : base(connectionString)
        {
            
        }
        public DbSet<ChiNhanh> ChiNhanhs { get; set; }
        public DbSet<QuyCach> QuyCachs { get; set; }
        public DbSet<LoaiCap1> LoaiCap1s { get; set; }
        public DbSet<LoaiCap2> LoaiCap2s { get; set; }
        public DbSet<LoaiCap3> LoaiCap3s { get; set; }
        public DbSet<BlogDoiTac> BlogDoiTacs { get; set; }
        public DbSet<ChucNang> ChucNangs { get; set; }
        public DbSet<DonHang> DonHangs { get; set; }
  
        public DbSet<HopDongChiNhanh> HopDongChiNhanhs { get; set; }
        public DbSet<KhoHang> KhoHangs { get; set; }
        public DbSet<LichSuThaoTac> LichSuThaoTacs { get; set; }
        public DbSet<LoaiGianHang> LoaiGianHangs { get; set; }
        public DbSet<MatHang> MatHangs { get; set; }
        public DbSet<NhaCC> NhaCCs { get; set; }
        public DbSet<NhapHang> NhapHangs { get; set; }
        public DbSet<PhanHoi> PhanHois { get; set; }
        public DbSet<TimeSheet> TimeSheets { get; set; }
        public DbSet<TraHang> TraHangs { get; set; }
       public DbSet<ApplicationUser> TaiKhoans { get; set; }
        public DbSet<ApplicationRole> Quyens { get; set;  }
        public DbSet<KhachHang> KhachHangs { get; set; }
       
        public DbSet<CT_ChucNang> CT_ChucNangs { get; set; }
        public DbSet<CT_NhapHang> CT_NhapHangs { get; set; }
        public DbSet<CT_TraHang> CT_TraHangs { get; set; }
        public DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }
        public DbSet<ChiTietKhoHang> ChiTietKhoHangs { get; set; }
        public DbSet<GiaoDien> GiaoDiens { get; set; }
        public DbSet<ChucNangNhanVien> ChucNangNhanViens { get; set; }
        public DbSet<ChiTietQuyen> ChiTietQuyens { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<IdentityUserClaim>().HasKey<int>(l => l.Id);
            //modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            //modelBuilder.Entity<IdentityRole>().HasKey<string>(l => l.Id);
            //modelBuilder.Entity<IdentityUserRole>().HasKey(r => new
            //{
            //    r.RoleId,
            //    r.UserId
            //});
            //modelBuilder.Entity<ApplicationUser>().ToTable("Users", "dbo");
            //modelBuilder.Entity<ApplicationRole>().ToTable("Roles", "dbo");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles", "dbo");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims", "dbo");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins", "dbo");
        }
        public static ShopEntities Create()
        {
          
            return new ShopEntities();
        }

        public static ShopEntities CreateEntitiesForSpecificDatabaseName(string databaseName, bool contextOwnsConnection = true)
        {
            //Initialize the SqlConnectionStringBuilder
            SqlConnectionStringBuilder sqlConnectionBuilder = new SqlConnectionStringBuilder();
            //sqlConnectionBuilder.DataSource = @"hutechdatnqlbh.database.windows.net";
            sqlConnectionBuilder.DataSource = @".";
            //sqlConnectionBuilder.UserID = "huy751574";
            //sqlConnectionBuilder.Password = "Huy0908529946";
            sqlConnectionBuilder.InitialCatalog = databaseName;
            sqlConnectionBuilder.IntegratedSecurity = true;
            sqlConnectionBuilder.MultipleActiveResultSets = true;
            string sqlConnectionString = sqlConnectionBuilder.ConnectionString;

            //Initialize the EntityConnectionStringBuilder
            //EntityConnectionStringBuilder entityBuilder = new EntityConnectionStringBuilder();
            //entityBuilder.Provider = "System.Data.SqlClient";
            //entityBuilder.ProviderConnectionString = sqlConnectionString;

           
            ////Create entity connection
            //EntityConnection connection = new EntityConnection(entityBuilder.ConnectionString);

            return new ShopEntities(sqlConnectionString);
        }


    }
    public class MyContextFactory : IDbContextFactory<ShopEntities>
    {
        public ShopEntities Create()
        {
            return new ShopEntities("ShopEntities");
        }
    }
}