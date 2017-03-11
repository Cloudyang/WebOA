using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebOA.Common;
using WebOA.IService;
using WebOA.Model;
using WebOA.UI.Models;

namespace WebOA.UI.Controllers
{
    public class MyBaseController : Controller
    {
        public IUserInfoService UserInfoService { get; set; }
        public IActionInfoService ActionInfoService { get; set; }
        public IUserActionService UserActionService { get; set; }

        public UserInfoViewModel UserLogin { get; set; }

        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            //return;
            //base.OnAuthorization(filterContext);

            //去session，使用分布式缓存完成登录
            if (Session["UserLogin"] == null)
            {
                filterContext.Result = new RedirectResult(Url.Action("Index", "UserLogin"));
                return;
            }
            UserLogin = Session["UserLogin"] as UserInfoViewModel;

            //           #region 登录验证
            //	    //1、获取客户端标识
            //           if (Request.Cookies.Get("loginId") == null)
            //           {
            //               filterContext.Result = new RedirectResult(Url.Action("Index", "UserLogin"));
            //               return;
            //           }
            //           string key=Request.Cookies.Get("loginId").Value;
            //           //2、与分布式缓存进行通信，获取对象
            //           MmHelper helper=new MmHelper();
            //           UserLogin = helper.Get(key) as UserInfoViewModel;
            //           //3、判断是否登录
            //           if (UserLogin == null)
            //           {
            //               filterContext.Result = new RedirectResult(Url.Action("Index", "UserLogin"));
            //               return;
            //           }
            //           //4、设置超时滑动时间
            //           helper.Set(key, UserLogin, DateTime.Now.AddMinutes(20)); 
            //#endregion

            #region 验证是否有访问权限
            //留个后门，给管理方便，生产环境下不存在这句代码
            if (UserLogin.UserName.ToLower().Equals("admin"))
            {
                return;
            }

            //1、准备工作，拿到用户，拿到权限信息
            UserInfo userInfo = UserInfoService.GetById(UserLogin.UserId);
            string controllerName = RouteData.GetRequiredString("controller");
            string actionName = RouteData.GetRequiredString("action");
            ActionInfo actionInfo = ActionInfoService.GetList(a =>
                (a.ControllerName.ToLower().Equals(controllerName.ToLower()))
                &&
                (a.ActionName.ToLower().Equals(actionName.ToLower()))
                &&
                a.IsDelete==false)
                .FirstOrDefault();
            if (actionInfo == null)
            {
                filterContext.Result=new RedirectResult("/Error.html");
            }

            //2、查询否决表，看有没有数据
            UserAction userAction= UserActionService.GetList(ua =>
                (ua.UserId == userInfo.UserId)
                &&
                (ua.ActionId == actionInfo.ActionId)).FirstOrDefault();
            if (userAction != null)
            {
                //2.1否决表中有数据
                if (userAction.IsAllow)
                {
                    //2.1.1允许
                }
                else
                {
                    //2.1.2拒绝
                    filterContext.Result=new RedirectResult("/NoAllow.html");
                }
            }
            else
            {
                //2.2否决表中无数据，则通过用户找角色，通过角色找权限
                var result = from r in userInfo.RoleInfo
                    from a in r.ActionInfo
                    where a.ActionId==actionInfo.ActionId
                    select a;
                if (result.Count() > 0)
                {
                    //2.2.1有权限
                }
                else
                {
                    //2.2.2无权限
                    filterContext.Result=new RedirectResult("/NoAllow.html");
                }
            }
            #endregion

        }
    }
}
