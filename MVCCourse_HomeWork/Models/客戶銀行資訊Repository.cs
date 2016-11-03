using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVCCourse_HomeWork.Models
{   
	public  class 客戶銀行資訊Repository : EFRepository<客戶銀行資訊>, I客戶銀行資訊Repository
	{
        public override IQueryable<客戶銀行資訊> All()
        {
            return base.All().Where(p => !p.Is刪除);
        }

        public 客戶銀行資訊 find(int id)
        {
            return All().FirstOrDefault(p => p.Id == id);
        }
    }

	public  interface I客戶銀行資訊Repository : IRepository<客戶銀行資訊>
	{

	}
}