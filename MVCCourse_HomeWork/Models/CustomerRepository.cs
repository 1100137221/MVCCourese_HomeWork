using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCCourse_HomeWork.Models
{
    public class CustomerRepository : EFRepository<客戶資料>, ICustomerRepository
    {
        public override IQueryable<客戶資料> All()
        {
            return base.All().Where(p=>!p.Is刪除);
        }

        public 客戶資料 find(int id)
        {
            return All().FirstOrDefault(p => p.Id == id);
        }
    }

    public interface ICustomerRepository : IRepository<客戶資料> {

    }
}