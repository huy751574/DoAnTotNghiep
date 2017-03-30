using QuanLyBanHang.CSDL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanHang.Repository
{
    public class Repository<T> where T :class
    {
        ShopEntities db { get; set; }
        private DbSet<T> table = null;
        public Repository()
        {
            db = new ShopEntities();
            table = db.Set<T>();
        }

        public Repository(ShopEntities db)
        {
            this.db = db;
            table = db.Set<T>();
        }

        public void Delete(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
        }

        public void Insert(T obj)
        {
            table.Add(obj);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public IEnumerable<T> SelectAll()
        {
            return table.ToList();
        }
        public IQueryable<T> Where(Expression<Func<T, bool>> filter)
        {
            return db.Set<T>().Where(filter);
        }


        public T SelectById(object id)
        {
            try
            {
                return table.Find(id);
            }
            catch
            {
                return null;
            }

        }

        public void Update(T obj)
        {
            table.Attach(obj);
            db.Entry(obj).State = EntityState.Modified;
        }

        public void AddOrUpdate(T obj)
        {
            db.Set<T>().AddOrUpdate(obj);
        }

        public void ReloadEntity(T obj)
        {
            table.Attach(obj);
            db.Entry(obj).Reload();
        }
    }
}
