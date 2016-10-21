using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCCourse_HomeWork.Models
{
    public class CustomerCotactRepository : EFRepository<客戶聯絡人>, ICustomerCotactRepository
    {
    }

    public interface ICustomerCotactRepository : IRepository<客戶聯絡人>
    {

    }
}