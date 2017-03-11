using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.Mvc;
using WebOA.IService;
using WebOA.Model;
using Spring.Util;

namespace WebOA.UI.Controllers
{
    public class RoleInfoController : MyBaseController//Controller
    {
        public IRoleInfoService RoleInfoService { get; set; }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetPageList(int page,int rows)
        {
            int total;

            var result = RoleInfoService.GetPageList<int>(
                r => r.IsDelete == false,
                r => r.RoleId,
                rows, page, out total
                ).Select(r=>new
                {
                    r.RoleId,
                    r.RoleName
                });

            return Json(new {total, rows = result});
        }

        public ActionResult Add(RoleInfo roleInfo)
        {
            string result = "no";

            //完善数据
            roleInfo.IsDelete = false;
            if (roleInfo.Remark == null)
            {
                roleInfo.Remark = "";
            }
            roleInfo.SubBy = 1;
            roleInfo.SubTime = DateTime.Now;

            //调用Service方法完成添加
            if (RoleInfoService.Add(roleInfo))
            {
                result = "ok";
            }

            return Content(result);
        }

        public ActionResult Edit(int id1)
        {
            ViewData.Model = RoleInfoService.GetById(id1);
            return View();
        }
        [HttpPost]
        public ActionResult Edit(RoleInfo roleInfo)
        {
            string result = "no";

            roleInfo.IsDelete = false;
            roleInfo.SubBy = 1;
            roleInfo.SubTime = DateTime.Now;
            if (roleInfo.Remark == null)
            {
                roleInfo.Remark = "";
            }

            if (RoleInfoService.Edit(roleInfo))
            {
                result = "ok";
            }

            return Content(result);
        }

        public ActionResult Remove(string idList)
        {
            string result = "no";

            List<int> ids=new List<int>();
            string[] ids2 = idList.Split(',');
            foreach (var id in ids2)
            {
                ids.Add(int.Parse(id));
            }
            if (RoleInfoService.Remove(ids.ToArray()))
            {
                result = "ok";
            }

            return Content(result);
        }

        public ActionResult SetAction(int id1)
        {
            ViewBag.RoleInfo = RoleInfoService.GetById(id1);
            ViewData.Model = ActionInfoService.GetList(a => a.IsDelete == false).ToList();

            return View();
        }
        [HttpPost]
        public ActionResult SetAction(int rid, string aids)
        {
            string result = "no";

            if (RoleInfoService.SetAction(rid, aids))
            {
                result = "ok";
            }

            return Content(result);
        }

    }
}
