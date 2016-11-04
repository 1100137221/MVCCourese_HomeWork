using System;
using System.Web.Mvc;

namespace MVCCourse_HomeWork.Models
{
    internal class 計算action執行時間Attribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Controller.ViewBag.dtStart = DateTime.Now;
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            filterContext.Controller.ViewBag.dtEnd = DateTime.Now;
            var dtTimeSpan =(DateTime)filterContext.Controller.ViewBag.dtEnd -
                 (DateTime)filterContext.Controller.ViewBag.dtStart;
            filterContext.Controller.ViewBag.dtTimespan = dtTimeSpan;
            base.OnActionExecuted(filterContext);   
        }
    }
}