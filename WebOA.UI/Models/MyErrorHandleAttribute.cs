using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


//[assembly: log4net.Config.XmlConfigurator(Watch = true)]  //以在Application_Start中注册
namespace WebOA.UI.Models
{
    public class MyHandleErrorAttribute : System.Web.Mvc.HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);

            //异常处理记录
            log4net.ILog log = log4net.LogManager.GetLogger("WebLogger");
            log.Debug(filterContext.Exception.StackTrace);

            //转到错误提示页
            filterContext.Result = new RedirectResult("/Project_Readme.html");
        }
    }
}