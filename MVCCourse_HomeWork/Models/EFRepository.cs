using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVCCourse_HomeWork.Models
{
    public abstract class EFRepository<T> : IRepository<T> where T:class
    {
        客戶資料Entities db = new 客戶資料Entities();

        private IDbSet<T> _objSet;
        public IDbSet<T> ObjSet {
            get {
                _objSet = db.Set<T>();
                return _objSet;
            }
        }
        
        public virtual IQueryable<T> All()
        {
            return ObjSet.AsQueryable();
        }

        public IQueryable<T> Where(System.Linq.Expressions.Expression<Func<T, bool>> expression)
        {
            return All().Where(expression);
        }

        public void Add(T entity)
        {
            ObjSet.Add(entity);
        }
        
        public void Delete(T entity)
        {
            ObjSet.Remove(entity);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        
    }
}