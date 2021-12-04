using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaoMoi.Models;
namespace BaoMoi.Controllers.Blogs
{
    public class BlogsController : Controller
    {
        BaoMoiEntities1 _db = new BaoMoiEntities1();
        // GET: Blogs
        public ActionResult Index()
        {
            return View();
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
    }
}