using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicStoreEntity;
using MusicStore.ViewModels;

namespace MusicStore.Controllers
{
    public class StoreController : Controller
    {
        private static readonly EntityDbContext _context = new EntityDbContext();
        /// <summary>
        /// 显示专辑的明细
        /// </summary>
        /// <returns></returns>
        public ActionResult Detail(Guid id)
        {
            //当前明细
            var detail = _context.Albums.Find(id);
            //评论
            var cmt = _context.Replies.Where(x => x.Album.ID == id && x.ParentReply == null)
                .OrderByDescending(x => x.CreateDateTime).ToList();
            ViewBag.Cmt = _GetHtml(cmt);//生成html脚本提取给视图

            return View(detail);
        }

        /// <summary>
        /// 生成回复的显示html文本
        /// </summary>
        /// <param name="cmt"></param>
        /// <returns></returns>
        private string _GetHtml(List<Reply> cmt)
        {
            var htmlString = "";
            htmlString = "<ul class='media-list'>";
            foreach (var item in cmt)
            {
                htmlString += " <li class=\"media\">";
                htmlString += "<div class=\"media-left\">";
                htmlString += "<img class=\"media-object\" id='ReplyImg' src='" + item.Person.Avada + "' alt=\"头像\">";
                htmlString += "</div>";

                htmlString += " <div class='media-body' id='Content-" + item.ID + "'>";
                htmlString += " <h5 class='media-heading'>" + item.Person.Name +
                              "  发表于" + item.CreateDateTime.ToString("yyyy年MM月dd日 hh时mm分ss秒") + "</h5>";
                htmlString += item.Content;
                htmlString += "  </div>";
                //查询当前回复的下一级回复数量
                var sonCmt = _context.Replies.Where(x => x.ParentReply.ID == item.ID).ToList();
                htmlString += "<h6><a href='#divEdt' onclick=\"javascript:GetQuote('" + item.ID + "','"+ item.ID + "');\">" +
                              "回复</a>(<a href='#' onclick=\"javascript:ShowCmt('" + item.ID + "')\">" + sonCmt.Count + "</a>)条" + "<a href='#' style=\"margin-left:20px;\"><i class='glyphicon glyphicon-thumbs-up' onclick=\"javascript:Like('" + item.ID + "')\"></i></a>(" +
                              item.Like + ")  <a href='#' style=\"margin-left:5px;\"><i class='glyphicon glyphicon-thumbs-down' onclick=\"javascript:Hate('" + item.ID + "')\"></i></a>(" + item.Hate + ")</h6>";


                htmlString += "</li>";
            }
            htmlString += "</ul>";
            return htmlString;
        }
        /// <summary>
        /// 生成子回复html文本 模态框
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public string _GetHtmlModal(Reply pcmt, List<Reply> cmts)
        {
            var htmlString = "";

            htmlString += "<div class=\"modal-header\">";
            htmlString += "<button type=\"button\" class=\"close\" data-dismiss=\"modal\" aria-hidden=\"true\">×</button>";
            htmlString += "<h4 class=\"modal-title\" id=\"myModalLabel\">";
            htmlString += "<em>楼主&nbsp&nbsp</em>" + pcmt.Person.Name + "  发表于" + pcmt.CreateDateTime.ToString("yyyy年MM月dd日 hh点mm分ss秒") + ":<br/>" + pcmt.Content;
            htmlString += " </h4> </div>";

            htmlString += "<div class=\"modal-body\">";
            //子回复
            htmlString += "<ul class='media-list' style='margin-left:20px;'>";
            //foreach (var item in cmts)
            //{
            //    htmlString += "<h5 class='media-heading'>" + item.Person.Name +
            //                  "  回复  "+item.ParentReply.Person.Name +": "+item.ParentReply.Content +"</h5 >";
            //    htmlString += item.Content;
            //    htmlString += "<h6 style =\"margin-bottom:5px; border-bottom:1px solid #c6e6e8\">" + item.CreateDateTime.ToString("yyyy年MM月dd日 hh时mm分ss秒") +
            //        "<a href = '#div-editor' style =\"margin-left:20px;\" class='reply' onclick=\"javascript:GetQuote('" + item.ParentReply.ID + "','" + item.ID + "');\">回复</a>" +
            //        "<a href = '#' style =\"margin-left:20px;\"><i class='glyphicon glyphicon-thumbs-up' onclick=\"javascript:Like('" + item.ID + "')\"></i></a>(" +
            //        item.Like + ")  <a href='#' style=\"margin-left:5px;\"><i class='glyphicon glyphicon-thumbs-down' onclick=\"javascript:Hate('" + item.ID + "')\"></i></a>(" + item.Hate + ")";
            //}
            //htmlString += "</div><div class=\"modal-footer\"></div>";
            foreach (var item in cmts)
            {
                htmlString += "<li class='media'>";
                htmlString += "<div class='media-left'>";
                htmlString += "<img class='media-object' id='ReplyImg' src='" + item.Person.Avada + "' alt=\"头像\">";
                htmlString += "</div>";
                htmlString += "<div class='media-body' id='Content-" + item.ID + "'>";
                htmlString += "<h5 class='media-heading'><em>" + item.Person.Name + "</em>&nbsp;&nbsp;回复  " + item.ParentReply.Person.Name + ": "+item.ParentReply.Content +" </h5> ";
                htmlString += item.Content;
                htmlString += "</div>";
                htmlString += "<h6 style =\"margin-bottom:5px; border-bottom:1px solid #c6e6e8\">" + item.CreateDateTime.ToString("yyyy年MM月dd日 hh时mm分ss秒") +
                              "<a href='#div-editor' style='margin-left:20px;' class='reply' onclick=\"javascript:GetQuote('" + item.ParentReply.ID + "','" + item.ID + "');\">回复</a>" +
                              "<a href='#' class='reply' style='margin-left:20px;'   onclick=\"javascript:Like('" + item.ID + "');\"><i class='glyphicon glyphicon-thumbs-up'></i>(" + item.Like + ")</a>" +
                              "<a href='#' class='reply' style='margin-left:20px;'   onclick=\"javascript:Hate('" + item.ID + "');\"><i class='glyphicon glyphicon-thumbs-down'></i>(" + item.Hate + ")</a></h6>";
                htmlString += "</li>";
            }
            htmlString += "</ul>";
            htmlString += "</div><div class=\"modal-footer\"></div>";

            return htmlString;
        }
        //老师：点评
        [HttpPost]
        [ValidateInput(false)]//关闭验证
        public ActionResult AddCmt(string id, string cmt, string reply)
        {
            if (Session["LoginUserSessionModel"] == null)
                return Json("nologin");

            var person = _context.Persons.Find((Session["LoginUserSessionModel"] as LoginUserSessionModel).Person.ID);
            var album = _context.Albums.Find(Guid.Parse(id));

            //创建回复对象
            var r = new Reply()
            {
                Title = "",
                Person = person,
                Content = cmt,
                Album = album
            };
            //父级回复
            if (string.IsNullOrEmpty(reply))
            {
                //顶级回复
                r.ParentReply = null;
            }
            else
            {
                //将ueditor提示的内容删除
                //内容
                var txt = _context.Replies.Find(Guid.Parse(reply)).Content;
                r.Content = r.Content.Replace(txt, "");
                //匹配用户、时间
                var i = r.Content.IndexOf("<h5");
                var j = r.Content.IndexOf("</h5>");
                txt = r.Content.Substring(i, j - i + 5);
                r.Content = r.Content.Replace(txt, "");
                r.ParentReply = _context.Replies.Find(Guid.Parse(reply));
            }

            _context.Replies.Add(r);
            _context.SaveChanges();

            //显示评论、排序
            var albumSay = _context.Replies.Where(x => x.Album.ID == album.ID && x.ParentReply == null)
                .OrderByDescending(x => x.CreateDateTime).ToList();
            //局部刷新当前评论内容
            var htmlString = _GetHtml(albumSay);

            return Json(htmlString);
        }

        //老师：子回复
        [HttpPost]
        public ActionResult showCmts(string pid)
        {

            var htmlString = "";
            Guid id = Guid.Parse(pid);
            //子回复，原内容
            var cmts = _context.Replies.Where(x => x.ParentReply.ID == id).OrderByDescending(x => x.CreateDateTime).ToList();
            var pcmt = _context.Replies.Find(id);

            //调用子回复html方法，生成模态框
            htmlString = _GetHtmlModal(pcmt, cmts);

            return Json(htmlString);
        }

        /// <summary>
        /// 点赞
        /// </summary>
        /// <param name="pid">回复id</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Like(Guid pid)
        {
            //初始化存储对象
            var status = new LikeStatus()
            {
                Status = "",
                Html = "",
            };
            //1判断用户是否登录
            if (Session["LoginUserSessionModel"] == null)
            {
                status = new LikeStatus()
                {
                    Status = "noLogin",
                    Html = "",
                };
                return Json(status);
            }
            //2.判断用户是否对这条回复点过赞或踩
            var person = _context.Persons.Find((Session["LoginUserSessionModel"] as LoginUserSessionModel).Person.ID);
            var reply = _context.Replies.Find(pid);//当前评论
            //是否有踩赞记录
            var IsLike = _context.LikeReplies.SingleOrDefault(x => x.Reply.ID == reply.ID && x.Person.ID == person.ID);
            

            //3.保存  reply实体中like+1或hate+1  LikeReply添加一条记录
            if (IsLike == null || IsLike.Person.ID != person.ID)
            {

                reply.Like += 1;//添加赞数
                var ok = new LikeReply()
                {
                    Reply = reply,
                    IsNotLike = true,
                    Person = person
                };
                _context.LikeReplies.Add(ok);
                _context.SaveChanges();
            }
            //点赞失败，已经点过了
            else
            {
                //赞或踩过了 判断
                if (IsLike.IsNotLike == false)
                {
                    status.Status = "false";
                    status.Html = "";
                    return Json(status);
                }
                reply.Like -= 1;
                _context.LikeReplies.Remove(IsLike);
                _context.SaveChanges();
            }

            //根据是否有上级回复 刷新detail 或 modal
            if (reply.ParentReply == null)
            {
                //显示评论、排序
                var albumSay = _context.Replies.Where(x => x.Album.ID == reply.Album.ID && x.ParentReply == null)
                    .OrderByDescending(x => x.CreateDateTime).ToList();

                status.Status = "Parent";
                status.Html = _GetHtml(albumSay);
            }
            else
            {
                //查询子回复
                var pcmt = _context.Replies.Find(pid).ParentReply;//主
                var cmts = _context.Replies.Where(x => x.ParentReply.ID == pcmt.ID).OrderByDescending(x => x.CreateDateTime).ToList();
                
                
                status.Status = "Son";
                status.Html = _GetHtmlModal(pcmt, cmts);
            }
            //生成html 注入视图
            return Json(status);
        }

        /// <summary>
        /// 踩
        /// </summary>
        /// <param name="pid">回复id</param>
        /// <returns></returns>
        public ActionResult Hate(Guid pid)
        {
            //初始化存储对象
            var status = new LikeStatus()
            {
                Status = "",
                Html = "",
            };
            //1判断用户是否登录
            if (Session["LoginUserSessionModel"] == null)
            {
                status = new LikeStatus()
                {
                    Status = "noLogin",
                    Html = "",
                };
                return Json(status);
            }
            //2.判断用户是否对这条回复点过赞或踩
            var person = _context.Persons.Find((Session["LoginUserSessionModel"] as LoginUserSessionModel).Person.ID);
            var reply = _context.Replies.Find(pid);//当前评论
            //是否有踩赞记录
            var IsLike = _context.LikeReplies.SingleOrDefault(x => x.Reply.ID == reply.ID && x.Person.ID == person.ID);


            //3.保存  reply实体中like+1或hate+1  LikeReply添加一条记录
            if (IsLike == null || IsLike.Person.ID != person.ID)
            {

                reply.Hate += 1;//添加赞数
                var ok = new LikeReply()
                {
                    Reply = reply,
                    IsNotLike = false,
                    Person = person
                };
                _context.LikeReplies.Add(ok);
                _context.SaveChanges();
            }
            //点赞失败，已经点过了
            else
            {
                //赞或踩过了 判断
                if (IsLike.IsNotLike == true)
                {
                    status.Status = "false";
                    status.Html = "";
                    return Json(status);
                }
                reply.Hate -= 1;
                _context.LikeReplies.Remove(IsLike);
                _context.SaveChanges();
            }

            //刷新detail 或 modal
            if (reply.ParentReply == null)
            {
                //显示评论、排序
                var albumSay = _context.Replies.Where(x => x.Album.ID == reply.Album.ID && x.ParentReply == null)
                    .OrderByDescending(x => x.CreateDateTime).ToList();

                status.Status = "Parent";
                status.Html = _GetHtml(albumSay);
            }
            else
            {
                //查询子回复
                var pcmt = _context.Replies.Find(pid).ParentReply;//主
                var cmts = _context.Replies.Where(x => x.ParentReply.ID == pcmt.ID).OrderByDescending(x => x.CreateDateTime == x.CreateDateTime).ToList();


                status.Status = "Son";
                status.Html = _GetHtmlModal(pcmt, cmts);
            }
            //生成html 注入视图
            return Json(status);
        }


        /// <summary>
        /// 评论处理
        /// </summary>
        /// <param name="str"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        //public ActionResult Comment(string str, Guid id)
        //{
        //    //是否登陆
        //    if (Session["LoginUserSessionModel"] == null)
        //        return Json("nologin");
        //    //不能输入空的内容
        //    if (str == "")
        //        return Json("");
        //    //2.查询出当前用户,专辑
        //    var person = _context.Persons.Find((Session["LoginUserSessionModel"] as LoginUserSessionModel).Person.ID);
        //    var album = _context.Albums.Find(id);
        //    //给当前专辑添加评论
        //    var txt = new AlbumComment()
        //    {
        //        Title = str,
        //        Person = _context.Persons.Find(person.ID),
        //        Conmment = str
        //    };
        //    album.AlbumComments.Add(txt);

        //    _context.SaveChanges();

        //    //显示评论、排序
        //    var albumSay = _context.Albums.Find(id).AlbumComments.OrderByDescending(x => x.CreateDateTime).ToList();
        //    //局部刷新当前评论内容
        //    var htmlString = "";

        //    foreach (var item in albumSay)
        //    {
        //        htmlString += "<p>" + item.Person.Name + ":" + item.Conmment + "<br/>";
        //        htmlString += "<em>" + item.CreateDateTime + "</em></p>";
        //    }

        //    return Json(htmlString);
        //}

        ///<summary>
        /// 回复处理
        /// </summary>
        /// <returns></returns>
        //public ActionResult ParentReply(string str, Guid id, Guid aid)
        //{
        //    //是否登陆
        //    if (Session["LoginUserSessionModel"] == null)
        //        return Json("nologin");
        //    //不能输入空的内容
        //    if (str == "")
        //        return Json("");
        //    //2.查询出当前用户,专辑
        //    var person = _context.Persons.Find((Session["LoginUserSessionModel"] as LoginUserSessionModel).Person.ID);
        //    var parent = _context.AlbumComments.Find(id);
        //    var album = _context.Albums.Find(aid);
        //    //给当前评论添加回复
        //    var txt = new AlbumComment()
        //    {
        //        Title = parent.Title,
        //        Person = _context.Persons.Find(person.ID),
        //        Conmment = str,
        //        ParentReply = parent
        //    };
        //    album.AlbumComments.Add(txt);

        //    _context.SaveChanges();

        //    // 显示评论、排序
        //    var albumSay = _context.AlbumComments.OrderByDescending(x => x.CreateDateTime).ToList();
        //    //局部刷新当前评论内容
        //    var htmlString = "";

        //    foreach (var item in albumSay)
        //    {
        //        htmlString += "<p>" + item.Person.Name + ":" + item.Conmment + "<br/>";
        //        htmlString += "<em>" + item.CreateDateTime + "</em></p>";
        //    }

        //    return Json(htmlString);
        //}

        /// <summary>
        /// 按分类显示专辑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Browser(Guid id)
        {
            var list = _context.Albums.Where(x => x.Genre.ID == id).ToList();
            return View(list);
        }
        /// <summary>
        /// 所有专辑
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var genres = _context.Genres.OrderBy(x => x.Name).ToList();
            return View(genres);
        }

        /// <summary>
        /// 用于保存需要刷新的html脚本
        /// </summary>
        public class LikeStatus
        {
            public string Status { get; set; }
            public string Html { get; set; }
        }
    }
}