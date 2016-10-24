using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCCourse_HomeWork.Models
{
    public class CustomerCotactRepository : EFRepository<客戶聯絡人>, ICustomerCotactRepository
    {
        public override IQueryable<客戶聯絡人> All()
        {
            return base.All().Where(p=>!p.Is刪除);
        }

        public 客戶聯絡人 find(int id)
        {
            return All().FirstOrDefault(p=>p.Id==id);
        }
    }

    public interface ICustomerCotactRepository : IRepository<客戶聯絡人>
    {

    }
}