using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebOA.Service;
using WebOA.Model;
using WebOA.Common;
using WebOA.IService;

namespace WebOA.UI.Controllers
{
    public class UserInfoController : Controller
    {
      //  private UserInfoService userInfoService = new UserInfoService();
        public IUserInfoService  userInfoService { set; get; }
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="page">PageIndex</param>
        /// <param name="rows">PageSize</param>
        /// <returns></returns>
        public ActionResult GetPageList(int page, int rows)
        {
            //接收查询数据
            int id1 = string.IsNullOrEmpty(Request["sid"]) ? 0 : int.Parse(Request["sid"]);
            string name1 = string.IsNullOrEmpty(Request["sname"]) ? "" : Request["sname"];

            //调用Service来完成查询，获取数据
            int total;
            //var result=UserInfoService.GetPageList<int>(
            //    u => (u.IsDelete == false)
            //        &&
            //        (id1==0?true:u.UserId==id1)
            //        &&
            //        (name1==""?true:u.UserName.Contains(name1)),
            //    u => u.UserId,
            //    rows, page,out total);


            WhereHelper<UserInfo> where = new WhereHelper<UserInfo>();
            where.Equal("IsDelete", false);
            if (id1 != 0)
            {
                where.Equal("UserId", id1);
            }
            if (name1 != "")
            {
                where.Contains("UserName", name1);
            }

            var result = userInfoService.GetPageList<int>(where.GetExpression(), u => u.UserId, page, rows, out total).Select(u => new
            {
                u.UserId,
                u.UserName
            });


            return Json(new { total, rows = result }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 处理用户的添加请求
        /// </summary>
        /// <param name="userInfo">使用自动装配，完成对象的创建与初始化</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(UserInfo userInfo)
        {
            string result = "no";

            //完成属性值的初始化
            userInfo.IsDelete = false;
            userInfo.SubBy = 1;
            userInfo.SubTime = DateTime.Now;
            if (userInfo.Remark == null)
            {
                userInfo.Remark = "";
            }

            //对密码进行md5加密
            userInfo.UserPwd = MD5Helper.EncryptString(userInfo.UserPwd);

            //调用Service完成添加
            if (userInfoService.Add(userInfo))
            {
                result = "ok";
            }
            return Content(result);
        }

        public ActionResult Edit(int id)
        {
            UserInfo u = userInfoService.GetById(id);
            return View(u);//直接将对象作为View方法的参数，本质是ViewData.Model=u;
        }

        [HttpPost]
        public ActionResult Edit(UserInfo userInfo)
        {
            string result = "no";

            //装配其它值
            userInfo.SubBy = 1;
            userInfo.SubTime = DateTime.Now;
            userInfo.IsDelete = false;
            if (userInfo.Remark == null)
            {
                userInfo.Remark = "";
            }
            //判断密码是否修改
            if (userInfo.UserPwd.Trim() != "")
            {
                userInfo.UserPwd = MD5Helper.EncryptString(userInfo.UserPwd);
            }
            else
            {
                userInfo.UserPwd = Request["pwd2"];
            }

            if (userInfoService.Edit(userInfo))
            {
                result = "ok";
            }

            return Content(result);
        }

        [HttpPost]
        public ActionResult Remove(string idList)
        {
            string result = "no";

            //将数据转换成int[]
            List<int> list = new List<int>();
            string[] ids = idList.Split(',');
            foreach (var id in ids)
            {
                list.Add(int.Parse(id));
            }

            if (userInfoService.Remove(list.ToArray()))
            {
                result = "ok";
            }

            return Content(result);
        }


    }
}