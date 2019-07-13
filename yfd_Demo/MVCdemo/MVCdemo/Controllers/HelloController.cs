using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCdemo.Controllers
{
    public class HelloController : Controller
    {
        // GET: Hello
        public ActionResult Index()
        {
            //使用其他视图，返回根目录在往下找
            //return View("../../views/text");
            return View();
        }
        //自定义
        public ActionResult dianp()
        {
            return View();
        }
        public ActionResult welcome(string name,int id=1)
        {
            ViewBag.ID = id;
            ViewBag.Name = name;
            return View();
        }
    }
}