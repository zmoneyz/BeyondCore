using Beyond.Core.Admin.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Beyond.Core.Admin.Controllers
{
    public class HomeController : Controller
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();


        public ActionResult Index()
        {
            //Logger.Info("11111nlog test error");
            //Logger.Fatal("1111222222nlog test error");
            //Logger.Error("22134444nlog test error");

            //LogEventInfo lei = new LogEventInfo();
            //lei.Properties["user_id"] = "18";
            //lei.Properties["user_name"] = "AllenLee";
            //lei.Properties["action_type"] = "Created Log";
            //lei.Properties["user_ip"] = "172.16.0.132";
            //lei.Properties["add_time"] = DateTime.Now;
            //lei.Properties["remark"] = "This is a remark";
            //lei.Level = LogLevel.Info;
            //Logger.Log(lei);
            //var strStatus = "";
            //if (string.IsNullOrEmpty(strStatus))
            //{
            //    return View("Login");
            //}

            //string userName = Request.Params["user_name"].ToString();//账号
            //string password = Request.Params["password"].ToString(); //密码
            //if (userName == "admin" && password == "123")
            //{
            //    return View("Index", "login");
            //}

            //return Content("The name or password is not correct");

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
    }
}
