using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebOA.IService;
using WebOA.Model;
using WebOA.UI.Models;

namespace WebOA.UI.Controllers
{
    public class HomeController :MyBaseController// Controller
    {


        public ActionResult Index()
        {
            //ViewData.Model = ActionInfoService
            //   .GetList(a => (a.IsDelete == false) && (a.IsMenu == true))
            //   .Select(a => new MenuViewModel()
            //   {
            //       ActionTitle = a.ActionTitle,
            //       ActionName = a.ActionName,
            //       ControllerName = a.ControllerName,
            //       MenuIcon = a.MenuIcon
            //   }).ToList();

            #region 主菜单过滤
            //1准备目标集合
            List<MenuViewModel> listMenu= new List<MenuViewModel>();
            //1.1获取所有的桌面菜单
            List<ActionInfo> list = ActionInfoService.GetList(a => a.IsDelete == false && a.IsMenu == true).ToList();
            //1.2获取当前登录的用户的对象
            UserInfo userInfo = UserInfoService.GetById(UserLogin.UserId);
            //1.3遍历所有桌面菜单，逐个判断是否有权限
            foreach (var actionInfo in list)
            {

                //根据当前数据，构造一个菜单对象
                MenuViewModel menu = new MenuViewModel()
                {
                    ActionTitle = actionInfo.ActionTitle,
                    ControllerName = actionInfo.ControllerName,
                    ActionName = actionInfo.ActionName,
                    MenuIcon = actionInfo.MenuIcon
                };

                //如果当前用户是admin，则不需要判断直接进入
                if (UserLogin.UserName.Equals("admin"))
                {
                    listMenu.Add(menu);
                    continue;
                }


                //2、查找否决中是否允许，如果允许，直接加入目标集合
                if (UserActionService.GetList(ua =>
                    (ua.ActionId == actionInfo.ActionId) &&
                    (ua.UserId == UserLogin.UserId) &&
                    (ua.IsAllow == true)).Count() > 0)
                {
                    listMenu.Add(menu);
                    continue;
                }

                //3、如果特权没有允许，则查找角色-权限过程
                var result1 = from r in userInfo.RoleInfo//from a in list<a>
                    from a in r.ActionInfo
                    where a.ActionId == actionInfo.ActionId
                    select a;
                if (result1.Count() > 0)
                {
                    listMenu.Add(menu);
                }

                //4、排除拒绝的特殊权限
                var result2 = from ua in userInfo.UserAction
                    where ua.ActionId == actionInfo.ActionId 
                    && 
                    ua.IsAllow == false
                    select ua;
                if (result2.Count() > 0)
                {
                    listMenu.Remove(menu);
                }
            }
            #endregion

            return View(listMenu);
        }

    }
}
