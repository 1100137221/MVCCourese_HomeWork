using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVCCourse_HomeWork.Models
{   
	public  class 客戶聯絡人Repository : EFRepository<客戶聯絡人>, I客戶聯絡人Repository
	{
        public override IQueryable<客戶聯絡人> All()
        {
            return base.All().Where(p => !p.Is刪除);
        }

        public 客戶聯絡人 find(int id)
        {
            return All().FirstOrDefault(p => p.Id == id);
        }
    }

	public  interface I客戶聯絡人Repository : IRepository<客戶聯絡人>
	{

	}
}