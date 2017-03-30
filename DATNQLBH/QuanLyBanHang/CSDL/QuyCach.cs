using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QuanLyBanHang.CSDL
{
    public class QuyCach
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuyCachId { get; set; }
        [Required]
        [Display(Name = "Tên quy cách")]
        public string Name { get; set; }


        public virtual ICollection<DangSuDung> MatHangs { get; set; }
        public delegate double DoNothing(double a, double b, double c);
        public int lam(int a, int b)
        {
            return 1;
        }
        public double la(double a, double b, double k)
        {
            return 1;
        }
        public void Dosomething()
        {
            DoNothing d = delegate(double x, double y, double v) { return x+y; };
            d.Invoke(5, 6, 7);
            d += new DoNothing(la);
            
        }
        
    }
    public interface a
    {
         Delegate lam();
    }
    public class b : a
    {
        public Delegate lam()
        {
            throw new NotImplementedException();
        }
    }
 
}