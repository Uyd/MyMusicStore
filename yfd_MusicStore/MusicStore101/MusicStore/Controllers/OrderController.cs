using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicStoreEntity;
using MusicStore.ViewModels;

namespace MusicStore.Controllers
{
    public class OrderController : Controller
    {
        private static EntityDbContext _context = new EntityDbContext();
        /// <summary>
        /// 下单页
        /// </summary>
        /// <returns></returns>
        public ActionResult Buy()
        {
            _context = new EntityDbContext();
            //1.确认用户是否登录 是否登录过期
            if (Session["LoginUserSessionModel"] == null)
                return RedirectToAction("login", "Account", new { returnUrl = Url.Action("buy", "Order") });
            //2.查询出当前用户、购物项
            var person = (Session["LoginUserSessionModel"] as LoginUserSessionModel).Person;
            var personOrder = _context.PersonOrders.SingleOrDefault(x => x.Person.ID == person.ID);
            var cartItem = _context.Carts.Where(x => x.Person.ID == person.ID).ToList();

            //算出购物车的总价
            decimal? totalPrice = (from item in cartItem select item.Count * item.Album.Price).Sum(); //linq表达式一句完成

            //3.创建新order对象 
            var order = new Order()
            {
                AddressPerson = personOrder.AddressPerson,
                MobilNumber = personOrder.OrderPhone,
                Address = personOrder.OrderAddress,
                Person = _context.Persons.Find(person.ID),
                TotalPrice = totalPrice ?? 0.00M,
            };
            //4.把购物项导入订单明细
            order.OrderDetails = new List<OrderDetail>();
            foreach (var item in cartItem)
            {
                var detail = new OrderDetail()
                {
                    Album = _context.Albums.Find(item.Album.ID),
                    AlbumID = item.AlbumID,
                    Count = item.Count,
                    Price = item.Album.Price
                };
                order.OrderDetails.Add(detail);
            }
            //将订单和明细在视图呈现 验证用户的收件人 地址 电话，供用户选择确认要购买专辑

            //当前订单未持久化,用会话保存方便用户进行编辑
            Session["Order"] = order;
            return View(order);
        }

        /// <summary>
        /// 删除处理
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RemoveDetail(Guid id)
        {
            //如果回话中为空，则重新刷新页面
            if (Session["Order"] == null)
                return RedirectToAction("buy");

            //读取回话中的order对象
            var order = Session["Order"] as Order;
            var deleteDetail = order.OrderDetails.SingleOrDefault(x => x.ID == id);
            //从订单明细移除该id记录
            order.OrderDetails.Remove(deleteDetail);

            //重新生成html脚本，返回json数据，局部刷新视图
            var totalPrice = (from item in order.OrderDetails select item.Count * item.Album.Price).Sum();
            var htmlInsert = "";
            foreach (var item in order.OrderDetails)
            {
                htmlInsert += "<tr>";
                htmlInsert += " <td><a href='../store/detail/" + item.ID + "'>" + item.Album.Title + "</a></td>";
                htmlInsert += "<td>" + item.Album.Price.ToString("C") + "</td>";
                htmlInsert += "<td>" + item.Count + "</td>";
                htmlInsert += "<td><a href=\"#\" onclick=\"RemoveDetail('" + item.ID + "');\"><i class=\"glyphicon glyphicon-remove\">我不喜欢这个</i></a></td></tr>";
            }

            htmlInsert += "<tr><td ></td><td></td><td>总价</td><td>" + totalPrice.ToString("C") + "</td ></tr>";

            return Json(htmlInsert);
        }
        /// <summary>
        /// 处理用户提交订单
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Buy(Order order)
        {
            //防止session过期
            if (Session["LoginUserSessionModel"] == null)
                return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("buy", "Order") });

            //当前用户信息
            var person = (Session["LoginUserSessionModel"] as LoginUserSessionModel).Person;
            order.Person = _context.Persons.Find(person.ID);

            //订单明细
            order.OrderDetails = new List<OrderDetail>();
            order.TotalPrice = 0.00M;

            var details = (Session["Order"] as Order).OrderDetails;

            //确定是否有订单
            if (details.Count == 0)
                return Content("<script>alert('订单有误！');location.href='" + Url.Action("buy", "Order") + "'</script>");

            foreach (var item in details)
            {
                item.Album = _context.Albums.Find(item.Album.ID);
                order.OrderDetails.Add(item);
            }
            order.TotalPrice = (from item in order.OrderDetails select item.Count * item.Album.Price).Sum();

            //验证是否已购买
            if (ModelState.IsValid)
            {
                //通过
                LockedHelp.ThreadLocked(order.ID);
                try
                {
                    _context.Orders.Add(order);
                    _context.SaveChanges();

                    //清空购物车
                    //删除在添加的项
                    foreach (var item in details)
                    {
                        var id = _context.Carts.SingleOrDefault(x => x.Album.ID == item.Album.ID);
                        _context.Carts.Remove(id);
                    }
                    _context.SaveChanges();

                }
                catch { }
                finally { LockedHelp.ThreadUnLocked(order.ID); }

                //跳到支付页
                return RedirectToAction("AliPay", "Pay", new { id = order.ID });
            }
            //未通过返回视图
            return RedirectToAction("Buy", "Order");

        }
  
        /// <summary>
        /// 浏览用户订单
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            _context = new EntityDbContext();
            //1.确认用户是否登录 是否登录过期
            if (Session["LoginUserSessionModel"] == null)
                return RedirectToAction("login", "Account", new { returnUrl = Url.Action("Index", "Order") });
            //2.查询出当前用户、购物项
            var person = (Session["LoginUserSessionModel"] as LoginUserSessionModel).Person;
            //订单明细
            var orderItem = _context.Orders.Where(x => x.Person.ID == person.ID).OrderByDescending(x=>x.OrderDateTime).ToList();

            var orderList = new List<Order>();
            foreach (var item in orderItem)
            {
                //创建新order对象 
                var order = new Order()
                {
                    ID = item.ID,
                    AddressPerson = item.AddressPerson,
                    Address = item.Address,
                    MobilNumber = item.MobilNumber,
                    Person = item.Person,
                    TradeNo = item.TradeNo,
                    EnumOrderStatus = item.EnumOrderStatus,
                    TotalPrice = item.TotalPrice,
                    OrderDateTime = item.OrderDateTime
                };
                //把购物项导入订单明细
                order.OrderDetails = item.OrderDetails;

                orderList.Add(order);
            }
            return View(orderList);
        }
    }
}