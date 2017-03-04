using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebOA.Service;

namespace WebOA.UI.Controllers
{
    public class UserInfoController : Controller
    {
        private UserInfoService userInfoService = new UserInfoService();
        // GET: UserInfo
        public ActionResult Index()
        {
            ViewData.Model = userInfoService.GetList(u => true);
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public  ActionResult Add(WebOA.Model.UserInfo u)
        {
            string result = "no";
            if (userInfoService.Add(u))
            {
                result = "ok";
            }
            return Content(result);
        }
    }
}