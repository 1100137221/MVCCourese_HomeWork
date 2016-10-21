using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MVCCourse_HomeWork.Models
{
    public interface IRepository<T>
    {
        IQueryable<T> All();
        IQueryable<T> Where(Expression<Func<T,bool>> expression);
        void Add(T entity);
        void Delete(T entity);
        void Save();
    }
}
