using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCCourse_HomeWork.Models
{
    public abstract class BaseController : Controller
    {
        protected int pageSize = 1;
    }
}