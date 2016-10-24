using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCCourse_HomeWork.Models
{
    public class CustomerBankRepository : EFRepository<客戶銀行資訊>,ICustomerBankRepository
    {
        public override IQueryable<客戶銀行資訊> All()
        {
            return base.All().Where(p=>!p.Is刪除);
        }

        public 客戶銀行資訊 find(int id)
        {
            return All().FirstOrDefault(p => p.Id == id);
        }

    }

    public interface ICustomerBankRepository : IRepository<客戶銀行資訊>
    {

    }
}