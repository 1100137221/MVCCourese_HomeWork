using MVCCourse_HomeWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCCourse_HomeWork.Controllers
{
    public class HomeController : BaseController
    {
        //UNDONE: QQ
        public ActionResult Index()
        {
            return View();
        }

        //TODO: TEST
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            var str = "";
            str += "                                    " ;
            str += "                                    " ;
            str += "                                    " ;
            str += "                                    " ;
            str += "                                    " ;
            str += "                                    " ;



            return View();
        }
        

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}