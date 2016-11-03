using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVCCourse_HomeWork.Models
{   
	public  class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
	{
        public override IQueryable<客戶資料> All()
        {
            return base.All().Where(p => !p.Is刪除);
        }

        public 客戶資料 find(int id)
        {
            return All().FirstOrDefault(p => p.Id == id);
        }
    }

	public  interface I客戶資料Repository : IRepository<客戶資料>
	{

	}
}