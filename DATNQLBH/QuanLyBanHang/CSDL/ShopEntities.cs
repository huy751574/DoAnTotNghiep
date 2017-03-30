
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace QuanLyBanHang.CSDL
{
    public class ShopEntities:DbContext
    {
        public ShopEntities(string connectionString = "ShopEntities") : base(connectionString)
        {

        }

  
        public DbSet<QuyCach> QuyCachs { get; set; }
        public DbSet<LoaiCap1> LoaiCap1s { get; set; }
        public DbSet<LoaiCap2> LoaiCap2s { get; set; }
        public DbSet<LoaiCap3> LoaiCap3s { get; set; }
      
        public DbSet<DonHang> DonHangs { get; set; }
  
      
        public DbSet<KhoHang> KhoHangs { get; set; }
      
        public DbSet<MatHang> MatHangs { get; set; }
        public DbSet<NhaCC> NhaCCs { get; set; }
       
     
        public DbSet<KhachHang> KhachHangs { get; set; }
       
     
        public DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }
        public DbSet<ChiTietKhoHang> ChiTietKhoHangs { get; set; }
        public DbSet<DangSuDung> DangSuDungs { get; set; }
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