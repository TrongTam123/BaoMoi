using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using BaoMoi.Models;
using PagedList;
using PagedList.Mvc;

namespace BaoMoi.Controllers
{
    public class DefaultController : Controller
    {
        BaoMoiEntities1 _db = new BaoMoiEntities1();
        // GET: Default

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }
        
        public ActionResult getNews()
        {
            ViewBag.meta = "newws";
            var v = from t in _db.News
                    where t.hide == true
                    orderby t.order ascending
                    select t;
            return PartialView(v.ToList());
        }
        public ActionResult getNewsTiep()
        {
            ViewBag.meta = "newwstiep";
            var v = from t in _db.NewsTieps
                    where t.hide == true
                    orderby t.order ascending
                    select t;
            return PartialView(v.ToList());
        }

        public ActionResult getCategory()
        {
            ViewBag.meta = "san-pham";
            var v = from t in _db.categories
                    where t.hide == true 
                    orderby t.order ascending
                    select t;
            return PartialView(v.ToList());
        }
        public ActionResult getTrending(long id, string metatitle)
        {
            ViewBag.meta = "san-pham";
            var v = from t in _db.Trendings
                    where t.categoryid == id && t.hide == true
                    orderby t.order ascending
                    select t;

            return PartialView(v.ToList());
        }

        public ActionResult getCategory1()
        {
            ViewBag.meta = "news";
            var v = from t in _db.category1
                    where t.hide == true
                    orderby t.order ascending
                    select t;
            return PartialView(v.ToList());
        }
        public ActionResult getNewNew(long id, string metatitle)
        {
            ViewBag.meta = "news";
            var v = from t in _db.NewNews
                    where t.categoryid1 == id && t.hide == true
                    orderby t.order ascending
                    select t;

            return PartialView(v.ToList());
        }

        public ActionResult getCategory6()
        {
            ViewBag.meta = "video";
            var v = from t in _db.category6
                    where t.hide == true
                    orderby t.order ascending
                    select t;
            return PartialView(v.ToList());
        }
        public ActionResult getVideo(long id, string metatitle)
        {
            ViewBag.meta = "video";
            var v = from t in _db.videos
                    where t.categoryid6 == id && t.hide == true
                    orderby t.order ascending
                    select t;

            return PartialView(v.ToList());
        }

        public ActionResult getCategory2()
        {
            ViewBag.meta = "latest";
            var v = from t in _db.category2
                    where t.hide == true
                    orderby t.order ascending
                    select t;
            return PartialView(v.ToList());
        }
        public ActionResult getLasts(long id, string metatitle)
        {
            ViewBag.meta = "latest";
            var v = from t in _db.Lasts
                    where t.categoryid2 == id && t.hide == true
                    orderby t.order ascending
                    select t;

            return PartialView(v.ToList());
        }

        public ActionResult getCategory3()
        {
            ViewBag.meta = "tags";
            var v = from t in _db.category3
                    where t.hide == true
                    orderby t.order ascending
                    select t;
            return PartialView(v.ToList());
        }
        public ActionResult getTags(long id, string metatitle)
        {
            ViewBag.meta = "tags";
            var v = from t in _db.Tags
                    where t.categoryid3 == id && t.hide == true
                    orderby t.order ascending
                    select t;

            return PartialView(v.ToList());
        }
        public ActionResult getCategory4()
        {
            ViewBag.meta = "most";
            var v = from t in _db.category4
                    where t.hide == true
                    orderby t.order ascending
                    select t;
            return PartialView(v.ToList());
        }
        public ActionResult getMost(long id, string metatitle)
        {
            ViewBag.meta = "most";
            var v = from t in _db.Most
                    where t.categoryid4 == id && t.hide == true
                    orderby t.order ascending
                    select t;

            return PartialView(v.ToList());
        }

        public ActionResult getCategor()
        {
            ViewBag.meta = "blog";
            var v = from t in _db.category4
                    where t.hide == true
                    orderby t.order ascending
                    select t;
            return PartialView(v.ToList());
        }
        public ActionResult getMos(long id, string metatitle)
        {
            ViewBag.meta = "blog";
            var v = from t in _db.Most
                    where t.categoryid4 == id && t.hide == true
                    orderby t.order ascending
                    select t;

            return PartialView(v.ToList());
        }


        [HttpGet]
        public ActionResult Register()
        {
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User _user)
        {
            if (ModelState.IsValid)
            {
                var check = _db.Users.FirstOrDefault(s => s.Email == _user.Email);
                if (check == null)
                {
                    _user.Password = GetMD5(_user.Password);
                    _db.Configuration.ValidateOnSaveEnabled = false;
                    _db.Users.Add(_user);
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Email already exists";
                    return View();
                }


            }
            return View();


        }

        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            if (ModelState.IsValid)
            {


                var f_password = GetMD5(password);
                var data = _db.Users.Where(s => s.Email.Equals(email) && s.Password.Equals(f_password)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    Session["FullName"] = data.FirstOrDefault().FirstName + " " + data.FirstOrDefault().LastName;
                    Session["Email"] = data.FirstOrDefault().Email;
                    Session["isAdmin"] = data.FirstOrDefault().isAdmin;
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("Login");
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Index");
        }

    }
}