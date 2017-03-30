using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using DATNQLBH.Models.CSDL;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DATNQLBH.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Họ tên")]
        public string FullName { get; set; }
      
        public virtual ICollection<DonHang> DonHangs { get; set; }
        public virtual ICollection<NhapHang> NhapHangs { get; set; }
        public virtual ICollection<PhanHoi> PhanHois { get; set; }
        public virtual ICollection<TimeSheet> TimeSheets { get; set; }
        public virtual ICollection<TraHang> TraHangs { get; set; }
        public virtual ICollection<LichSuThaoTac> LichSuThaoTacs { get; set; }

      
        [ForeignKey("ChiNhanh")]
        public string MaCN { get; set; }
        public virtual ChiNhanh ChiNhanh { get; set; }
       [Display(Name ="Ngày sinh")]
      
        public System.DateTime Birthday { get; set; }
        public System.DateTime NgayDangKy { get; set; }
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }
        [Display(Name = "Lương")]
        public long Luong { get; set; }
        [Display(Name = "Số chứng minh thư")]
        public string CMND { get; set; }
        public bool Active { get; set; }
        public long Experience { get; set; }
        public virtual ICollection<ApplicationRole> Roles { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
    public class ApplicationRole : IdentityRole
    {
        public virtual ICollection<ApplicationUser> Users { get; set; }
        public virtual ICollection<ChiTietQuyen> ChiTietQuyens { get; set; }
    }
    //public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    //{

    //    public ApplicationDbContext()
    //        : base("ShopEntities", throwIfV1Schema: false)
    //    {
    //    }
        
    //    public static ApplicationDbContext Create()
    //    {
    //        return new ApplicationDbContext();
    //    }


     
    //}
}