using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebOA.IService;
using WebOA.Model;

namespace WebOA.UI.Controllers
{
    public class ActionInfoController :MyBaseController// Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetPageList(int page,int rows)
        {
            int total;
            var result = ActionInfoService.GetPageList<int>(
                a => a.IsDelete == false,
                a => a.ActionId,
                rows, page, out total
                ).Select(a=>new
                {
                    a.ActionId,
                    a.ActionTitle,
                    a.ControllerName,
                    a.ActionName,
                    a.IsMenu
                });
            return Json(new {total, rows = result}, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Add(ActionInfo actionInfo)
        {
            string result = "no";

            //完善初始化
            actionInfo.IsDelete = false;
            actionInfo.SubBy = 1;
            actionInfo.SubTime = DateTime.Now;
            if (actionInfo.Remark == null)
            {
                actionInfo.Remark = "";
            }
            if (!actionInfo.IsMenu)
            {
                actionInfo.MenuIcon = "";
            }

            //调用Service完成保存
            if (ActionInfoService.Add(actionInfo))
            {
                result = "ok";
            }

            return Content(result);
        }

        public ActionResult UploadMenu()
        {
            var requestFile = Request.Files["MenuPic"];

            string fname = "/uploads/" + Guid.NewGuid().ToString() + Path.GetExtension(requestFile.FileName);

            requestFile.SaveAs(Request.MapPath(fname));
            return Content(fname);
        }


        public ActionResult Edit(int id1)
        {
            ViewData.Model = ActionInfoService.GetById(id1);
            return View();
        }
        [HttpPost]
        public ActionResult Edit(ActionInfo actionInfo)
        {
            string result = "no";

            //完成初始值
            actionInfo.IsDelete = false;
            actionInfo.SubBy = 1;
            actionInfo.SubTime = DateTime.Now;
            if (actionInfo.Remark == null)
            {
                actionInfo.Remark = "";
            }
            if (!actionInfo.IsMenu)
            {
                actionInfo.MenuIcon = "";
            }

            if (ActionInfoService.Edit(actionInfo))
            {
                result = "ok";
            }

            return Content(result);
        }

        public ActionResult Remove(string idList)
        {
            string result = "no";

            List<int> list1=new List<int>();
            string[] list2 = idList.Split(',');
            foreach (var item in list2)
            {
                list1.Add(int.Parse(item));

            }

            if (ActionInfoService.Remove(list1.ToArray()))
            {
                result = "ok";
            }

            return Content(result);
        }
    }
}
