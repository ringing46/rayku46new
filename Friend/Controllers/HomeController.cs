using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Friend.Models;

namespace Friend.Areas.Friend.Controllers
{
    public class HomeController : Controller
    {
        private 交友配對加強版Entities db = new 交友配對加強版Entities();

        // GET: Friend/Friend
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Friendview()
        {
            int LoginUser = int.Parse(Request.Cookies["id"].Value);
            var friends = from p in db.MemberTables.AsEnumerable()
                          join d in db.MemberShipTables
                          on p.MemberID equals d.MemberID
                          where d.FriendID == LoginUser && d.StatusID == 1
                          select p;

            return View(friends.ToList());
        }
        public ActionResult PartFriendview()
        {
            int LoginUser = int.Parse(Request.Cookies["id"].Value);
            var friends = from p in db.MemberTables.AsEnumerable()
                          join d in db.MemberShipTables
                          on p.MemberID equals d.MemberID
                          where d.FriendID == LoginUser && d.StatusID == 4
                          select p;
            var test = from p in db.MemberShipTables.AsEnumerable()
                       where p.FriendID == LoginUser && p.StatusID == 4
                       select p;
            if (test.Any())
            {
                ViewBag.any1 = 1;
            }
            else
            {
                ViewBag.any1 = 0;
            }
            return PartialView(friends.ToList());
        }
        public ActionResult FriendviewNow()
        {

            if (Request.Cookies["id"] != null)
            {

                int LoginUser = int.Parse(Request.Cookies["id"].Value);
                Session["name"] = "Friend";
                var friends = from p in db.MemberTables.AsEnumerable()
                              join d in db.MemberShipTables
                              on p.MemberID equals d.MemberID
                              where d.FriendID == LoginUser && d.StatusID == 2
                              select p;
                var test = from p in db.MemberShipTables.AsEnumerable()
                           where p.FriendID == LoginUser && p.StatusID == 2
                           select p;
                if (test.Any())
                {
                    ViewBag.any1 = 1;
                }
                else
                {
                    ViewBag.any1 = 0;
                }
                //ViewBag.Friends = new SelectList(db.MemberTable, "MemberID", "Name");
                return View(friends.ToList());
            }

            else
            {
                return RedirectToAction("Index", "Home", new { area = "Member" });

            }
        }
        public ActionResult PartFollow()
        {
            int LoginUser = int.Parse(Request.Cookies["id"].Value);
            var friends = from p in db.MemberTables.AsEnumerable()
                          join d in db.MemberShipTables
                          on p.MemberID equals d.MemberID
                          where d.FriendID == LoginUser && d.StatusID == 18
                          select p;
            var test = from p in db.MemberShipTables.AsEnumerable()
                       where p.FriendID == LoginUser && p.StatusID == 18
                       select p;
            if (test.Any())
            {
                ViewBag.any1 = 1;
            }
            else
            {
                ViewBag.any1 = 0;
            }
            return PartialView(friends.ToList());
        }
        public ActionResult PartFriendviewNow()
        {

            if (Request.Cookies["id"] != null)
            {

                int LoginUser = int.Parse(Request.Cookies["id"].Value);
                Session["name"] = "Friend";
                var friends = from p in db.MemberTables.AsEnumerable()
                              join d in db.MemberShipTables
                              on p.MemberID equals d.MemberID
                              where d.FriendID == LoginUser && d.StatusID == 2
                              select p;
                var test = from p in db.MemberShipTables.AsEnumerable()
                           where p.FriendID == LoginUser && p.StatusID == 2
                           select p;
                if (test.Any())
                {
                    ViewBag.any1 = 1;
                }
                else
                {
                    ViewBag.any1 = 0;
                }
                return PartialView(friends.ToList());
            }

            else
            {
                return RedirectToAction("Index", "Home", new { area = "Member" });

            }



        }
        public ActionResult Black()
        {
            int LoginUser = int.Parse(Request.Cookies["id"].Value);
            var friends = from p in db.MemberTables.AsEnumerable()
                          join d in db.MemberShipTables
                          on p.MemberID equals d.MemberID
                          where d.FriendID == LoginUser && d.StatusID == 3
                          select p;
            //ViewBag.Friends = new SelectList(db.MemberTable, "MemberID", "Name");
            return View(friends.ToList());
        }
        public ActionResult PartBlack()
        {
            int LoginUser = int.Parse(Request.Cookies["id"].Value);
            var friends = from p in db.MemberTables.AsEnumerable()
                          join d in db.MemberShipTables
                          on p.MemberID equals d.MemberID
                          where d.FriendID == LoginUser && d.StatusID == 3
                          select p;
            //ViewBag.Friends = new SelectList(db.MemberTable, "MemberID", "Name");
            var test = from p in db.MemberShipTables.AsEnumerable()
                       where p.MemberID == LoginUser && p.StatusID == 3
                       select p;
            if (test.Any())
            {
                ViewBag.any1 = 1;
            }
            else
            {
                ViewBag.any1 = 0;
            }
            return PartialView(friends.ToList());
        }

        //歷史搜尋
        public ActionResult PartSearchDetail()
        {
            ViewBag.Count = db.SearchTables.AsEnumerable().Where(p => p.MemberID == int.Parse(Request.Cookies["id"].Value)).Count();
            return PartialView(db.SearchTables.AsEnumerable().Where(p => p.MemberID == int.Parse(Request.Cookies["id"].Value)).OrderByDescending(p => p.SearchDate).ToList());
        }

        //AJAX歷史搜尋紀錄
        public ActionResult AjaxDetail(int id = 1)
        {
            return Json(db.SearchDetailTables.Where(p => p.SearchID == id).Select(p => new { p.InterestTable.Interest }), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Deletetest(int id = 0)
        {
            int LoginUser = int.Parse(Request.Cookies["id"].Value);
            var q = (from p in db.MemberTables.AsEnumerable()
                     join d in db.MemberShipTables
                     on p.MemberID equals d.MemberID
                     where d.MemberID == LoginUser && d.FriendID == id
                     select p.MemberID).First();
            int q1 = int.Parse(q.ToString());
            db.MemberShipTables.Remove(db.MemberShipTables.Where(p => p.MemberID == q1 && p.FriendID == id && p.StatusID == 1).First());
            db.MemberShipTables.Remove(db.MemberShipTables.Where(p => p.FriendID == q1 && p.MemberID == id && p.StatusID == 1).First());

            //通知
            var q2 = (from p in db.MemberTables.AsEnumerable()
                      where p.MemberID == int.Parse(Request.Cookies["id"].Value)
                      select p).First();
            NoticeTable MST8 = new NoticeTable();
            MST8.NoticeTitle = "好友邀請被拒絕";
            MST8.Date = DateTime.Now;
            MST8.Detail = "給" + q2.NickName + "的好友邀請被拒絕了";
            MST8.ReceiverID = id;
            MST8.SenderID = int.Parse(Request.Cookies["id"].Value);
            MST8.StatusID = 19;
            db.NoticeTables.Add(MST8);
            db.SaveChanges();
            return RedirectToAction("FriendviewNow");
        }

        public ActionResult Deletetest1(int id = 0)
        {
            int LoginUser = int.Parse(Request.Cookies["id"].Value);
            var q = (from p in db.MemberTables.AsEnumerable()
                     join d in db.MemberShipTables
                     on p.MemberID equals d.MemberID
                     where d.MemberID == LoginUser && d.FriendID == id
                     select p.MemberID).First();
            int q1 = int.Parse(q.ToString());
            db.MemberShipTables.Remove(db.MemberShipTables.Where(p => p.MemberID == q1 && p.FriendID == id && p.StatusID == 2).First());
            db.MemberShipTables.Remove(db.MemberShipTables.Where(p => p.FriendID == q1 && p.MemberID == id && p.StatusID == 2).First());
            db.SaveChanges();
            return RedirectToAction("FriendviewNow");
        }
        public ActionResult Deletetest2(int id = 0)
        {
            int LoginUser = int.Parse(Request.Cookies["id"].Value);
            var q = (from p in db.MemberTables.AsEnumerable()
                     join d in db.MemberShipTables
                     on p.MemberID equals d.MemberID
                     where d.MemberID == LoginUser && d.FriendID == id
                     select p.MemberID).First();
            int q1 = int.Parse(q.ToString());
            db.MemberShipTables.Remove(db.MemberShipTables.Where(p => p.MemberID == q1 && p.FriendID == id && p.StatusID == 3).First());
            db.MemberShipTables.Remove(db.MemberShipTables.Where(p => p.FriendID == q1 && p.MemberID == id && p.StatusID == 3).First());
            db.SaveChanges();
            return RedirectToAction("FriendviewNow");
        }
        public ActionResult ConfirmFriend(int id = 0)
        {
            int LoginUser = int.Parse(Request.Cookies["id"].Value);
            var q1 = (from p in db.MemberTables.AsEnumerable()
                      join d in db.MemberShipTables
                      on p.MemberID equals d.MemberID
                      where d.MemberID == LoginUser && d.FriendID == id && d.StatusID==1
                      select d).First();
            q1.StatusID = 2;

            var q2 = (from p in db.MemberTables.AsEnumerable()
                      join d in db.MemberShipTables
                      on p.MemberID equals d.MemberID
                      where d.MemberID == id && d.FriendID == LoginUser && d.StatusID == 4
                      select d).First();
            q2.StatusID = 2;

            //配對律 
            MemberShipTable MST1 = new MemberShipTable();
            MST1.MemberID = int.Parse(Request.Cookies["id"].Value);
            MST1.FriendID = id;
            MST1.StatusID = 21;
            MST1.Date = DateTime.Now;
            db.MemberShipTables.Add(MST1);
            //通知
            var q = (from p in db.MemberTables.AsEnumerable()
                     where p.MemberID == int.Parse(Request.Cookies["id"].Value)
                     select p).First();
            NoticeTable MST8 = new NoticeTable();
            MST8.NoticeTitle = "好友邀請已確認";
            MST8.Date = DateTime.Now;
            MST8.Detail = q.NickName + "已成為您的好友";
            MST8.ReceiverID = id;
            MST8.SenderID = int.Parse(Request.Cookies["id"].Value);
            MST8.StatusID = 19;
            db.NoticeTables.Add(MST8);

            db.SaveChanges();
            return RedirectToAction("FriendviewNow");
        }
        public ActionResult Reject(int id = 0)
        {
            int LoginUser = int.Parse(Request.Cookies["id"].Value);
            var q = (from p in db.MemberTables.AsEnumerable()
                     join d in db.MemberShipTables
                     on p.MemberID equals d.MemberID
                     where d.MemberID == LoginUser && d.FriendID == id
                     select d).First();
            q.StatusID = 22;
            var q1 = (from p in db.MemberTables.AsEnumerable()
                      join d in db.MemberShipTables
                      on p.MemberID equals d.MemberID
                      where d.MemberID == id && d.FriendID == LoginUser
                      select d).First();
            q1.StatusID = 22;
            //int q1 = int.Parse(q.ToString());
            //db.MemberShipTables.Remove(db.MemberShipTables.Where(p => p.MemberID == q1 && p.FriendID == id && p.StatusID == 1).First());
            //db.MemberShipTables.Remove(db.MemberShipTables.Where(p => p.FriendID == q1 && p.MemberID == id && p.StatusID == 1).First());

            //通知
            var q2 = (from p in db.MemberTables.AsEnumerable()
                      where p.MemberID == int.Parse(Request.Cookies["id"].Value)
                      select p).First();
            NoticeTable MST8 = new NoticeTable();
            MST8.NoticeTitle = "好友邀請被拒絕";
            MST8.Date = DateTime.Now;
            MST8.Detail = "給" + q2.NickName + "的好友邀請被拒絕了";
            MST8.ReceiverID = id;
            MST8.SenderID = int.Parse(Request.Cookies["id"].Value);
            MST8.StatusID = 19;
            db.NoticeTables.Add(MST8);
            db.SaveChanges();
            return RedirectToAction("FriendviewNow");
        }
        public ActionResult ConfirmFriend1(int? friendID, string Reason, int? id = 1)
        {
            int LoginUser = int.Parse(Request.Cookies["id"].Value);
            var q1 = (from p in db.MemberTables.AsEnumerable()
                      join d in db.MemberShipTables
                      on p.MemberID equals d.MemberID
                      where d.MemberID == LoginUser && d.FriendID == friendID &&d.StatusID==2
                      select d).First();
            q1.StatusID = 3;
            q1.Reason = Reason;
            var q2 = (from p in db.MemberTables.AsEnumerable()
                      join d in db.MemberShipTables
                      on p.MemberID equals d.MemberID
                      where d.MemberID == friendID && d.FriendID == LoginUser && d.StatusID == 2
                      select d).First();
            q2.StatusID = 3;


            db.SaveChanges();
            return RedirectToAction("FriendviewNow");
        }
        public ActionResult GetImageByte(int id = 1)
        {
            var member = db.MemberTables.Find(id);

            byte[] img = member.Photo;
            return File(img, "image/jpeg");
        }
    }
}