using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVCCourse_HomeWork.Models
{   
	public  class vw_CustomerListRepository : EFRepository<vw_CustomerList>, Ivw_CustomerListRepository
	{

	}

	public  interface Ivw_CustomerListRepository : IRepository<vw_CustomerList>
	{

	}
}