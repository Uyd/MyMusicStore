using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicStoreEntity;
using MusicStoreEntity.UserAndRole;
using MusicStore.ViewModels;
using System.IO;

namespace MusicStore.Controllers
{
    public class MyController : Controller
    {
        private static EntityDbContext _context = new EntityDbContext();


        /// <summary>
        /// 个人信息设置选项页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //1.确认用户是否登录 是否登录过期
            if (Session["LoginUserSessionModel"] == null)
                return RedirectToAction("login", "Account", new { returnUrl = Url.Action("Index", "My") });
            return View();
        }


        /// <summary>
        /// 个人信息修改
        /// </summary>
        /// <returns></returns>
        public ActionResult Myinfo()
        {
            //1.确认用户是否登录 是否登录过期
            if (Session["LoginUserSessionModel"] == null)
                return RedirectToAction("login", "Account", new { returnUrl = Url.Action("Myinfo", "My") });

            //当前用户
            var person = (Session["LoginUserSessionModel"] as LoginUserSessionModel).Person;
            //var person = _context.Persons.Find((Session["LoginUserSessionModel"] as LoginUserSessionModel).Person.ID);

            var myVM = new MyViewModel()
            {
                Name = person.Name,
                HomeAddress = person.Address,
                MobilNumber = person.MobileNumber,
                Birthday = person.Birthday.ToString("yyyy-MM-dd"),
                Sex = person.Sex
            };

            ViewBag.AvardaUrl = person.Avada;

            return View(myVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Myinfo(MyViewModel model)
        {
            if (Session["LoginUserSessionModel"] == null)
                return RedirectToAction("login", "Account", new { returnUrl = Url.Action("Index", "My") });

            var person = _context.Persons.Find((Session["LoginUserSessionModel"] as LoginUserSessionModel).Person.ID);
            //用户原来的头像
            var oldAvarda = person.Avada;
            //设置默认头像
            if (oldAvarda == null)
                if (person.Sex)
                {
                    oldAvarda = "/content/images/boy.jpg";
                }
                else
                {
                    oldAvarda = "/content/images/girl.jpg";
                }
            //if (ModelState.IsValid)
            //{
            //保存头像
            if (model.Avada != null)
            {
                var uploadDir = "~/Upload/Avarda/";
                //取后缀名
                var fileLastName = model.Avada.FileName.Substring(model.Avada.FileName.LastIndexOf(".") + 1,
                    (model.Avada.FileName.Length - model.Avada.FileName.LastIndexOf(".") - 1));
                var imagePath = Path.Combine(Server.MapPath(uploadDir), person.ID + "." + fileLastName);  //将网站虚拟路径转化为真实的物理路径
                model.Avada.SaveAs(imagePath);
                oldAvarda = "/Upload/Avarda/" + person.ID + "." + fileLastName;
            }

            //保存个人信息
            person.MobileNumber = model.MobilNumber;
            person.Address = model.HomeAddress;
            person.Name = model.Name;
            person.FirstName = person.Name.Substring(0, 1);
            person.LastName = person.Name.Substring(1, person.Name.Length - 1);
            person.Avada = oldAvarda;
            person.Birthday = Convert.ToDateTime(model.Birthday);
            person.Sex = model.Sex;
            person.UpdateTime = DateTime.Now;

            _context.SaveChanges();


            //重新登陆，刷新一下当前用户信息
            return RedirectToAction("login", "Account", new { returnUrl = Url.Action("Index", "My") });

            //return RedirectToAction("Myinfo");
            //}

            //ViewBag.AvardaUrl = oldAvarda;
            //return View();
        }


        /// <summary>
        /// 收货地址设置
        /// </summary>
        /// <returns></returns>
        public ActionResult Address()
        {
            //当前登录验证
            if (Session["LoginUserSessionModel"] == null)
                return RedirectToAction("login", "Account", new { returnUrl = Url.Action("Address", "My") });
            //当前用户
            var person = (Session["LoginUserSessionModel"] as LoginUserSessionModel).Person;

            //是否有当前收获地址
            var personOrder = _context.PersonOrders.SingleOrDefault(x => x.Person.ID == person.ID);

            //没有就将当前用户的信息初始化为一个地址，传给视图
            if (personOrder == null)
            {
                personOrder = new PersonOrder();
                personOrder.Person = person;
                personOrder.AddressPerson = person.Name;
                personOrder.OrderAddress = person.Address;
                personOrder.OrderPhone = person.MobileNumber;
            }
            //var p = new MyViewModel(personOrder);
            var p = new MyViewModel()
            {
                AddressPerson = personOrder.AddressPerson,
                Address = personOrder.OrderAddress,
                MobilNumber = personOrder.Person.MobileNumber,
            };
            return View(p);
        }

        /// <summary>
        /// 修改收货信息
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Address(MyViewModel pView)
        {
            //当前登录验证
            if (Session["LoginUserSessionModel"] == null)
                return RedirectToAction("login", "Account", new { returnUrl = Url.Action("Address", "My") });
            //当前用户
            var person = (Session["LoginUserSessionModel"] as LoginUserSessionModel).Person;
            var po = _context.PersonOrders.SingleOrDefault(x => x.Person.ID == person.ID);


            if (po == null)
            {
                var addPersonOrder = new PersonOrder();
                addPersonOrder.OrderAddress = pView.Address;
                addPersonOrder.OrderPhone = pView.MobilNumber;
                addPersonOrder.AddressPerson = pView.AddressPerson;
                addPersonOrder.Person = _context.Persons.Find(person.ID);
                //先修改一次本人地址
                //_context.Persons.SingleOrDefault(x => x.ID == person.ID).Address = pView.Address;
                _context.PersonOrders.Add(addPersonOrder);
            }
            else
            {
                po.OrderAddress = pView.Address;
                po.OrderPhone = pView.MobilNumber;
                po.AddressPerson = pView.AddressPerson;
                po.Person = _context.Persons.Find(person.ID);
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "my");
        }
    }
}