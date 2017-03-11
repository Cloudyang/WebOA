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
    public class UserLoginController : Controller
    {
        public IUserInfoService UserInfoService { get; set; }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ValidateCode()
        {
            ValidateCode vCode = new ValidateCode();
            string code = vCode.CreateValidateCode(4);
            Session["ValidateCode"] = code;
            byte[] bytes = vCode.CreateValidateGraphic(code);
            return File(bytes, @"image/jpeg");

        }
        [HttpPost]
        public ActionResult Login(UserInfoViewModel userInfo)
        {
            string result = "no";


            //判断是否是一个正常的请求
            if (Session["ValidateCode"] == null || Session["ValidateCode"].ToString() == "")
            {
                return Content("请正常请求");
            }

            string vcode = Request["vCode"];
            //先匹配验证码
            if (Session["ValidateCode"].ToString().Equals(vcode,StringComparison.InvariantCultureIgnoreCase))
            {
                int id1;
                //根据用户名密码进行查询
                if (UserInfoService.Login(new UserInfo()
                {
                    UserName = userInfo.UserName,
                    UserPwd = userInfo.UserPwd
                },out id1))
                {
                    result = "ok";
                    userInfo.UserId = id1;

                    //去session，改成分布式缓存
                    Session["UserLogin"] = userInfo;

                    // //使用分布式缓存进行登录操作
                    // //1、创建标识
                    // string key = Guid.NewGuid().ToString();
                    // //2、向客户端写标识
                    //Response.Cookies.Add(new HttpCookie("loginId",key));
                    // //3、向分布式缓存服务器写信息
                    // MmHelper helper=new MmHelper();
                    // helper.Set(key, userInfo, DateTime.Now.AddMinutes(20));
                }
                else
                {
                    result = "用户名或密码错误";
                    Session["ValidateCode"] = "";
                }
            }
            else
            {
                //验证码不匹配
                result = "验证码错误";
                Session["ValidateCode"] = "";
            }

            return Content(result);
        }

        public ActionResult Logout()
        {
            //去session
            Session["UserLogin"] = null;

            ////使用分布式缓存的用户注销
            //if (Request.Cookies.Get("loginId") != null)
            //{
            //    string key = Request.Cookies.Get("loginId").Value;
            //    //1、更新客户端cookie
            //    //Response.Cookies["loginId"].Expires = DateTime.Now.AddMinutes(-1);
            //    //2、销毁分布式缓存中的对象
            //    MmHelper helper=new MmHelper();
            //    helper.Delete(key);
            //}

            return RedirectToAction("Index");
        }
    }
}
