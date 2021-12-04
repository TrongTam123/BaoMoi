using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaoMoi.Models;

namespace BaoMoi.Controllers
{
    public class TagsController : Controller
    {
        BaoMoiEntities1 _db = new BaoMoiEntities1();
        // GET: Tags
        public ActionResult Index(String meta)
        {
            var v = from t in _db.category3
                    where t.meta == meta
                    select t;
            return View(v.FirstOrDefault());
        }
        public ActionResult DetailTags(long id)
        {
            var v = from t in _db.Tags
                    where t.id == id
                    select t;
            return View(v.FirstOrDefault());
        }

        public ActionResult getCategoryTrending_Tags()
        {
            ViewBag.meta = "san-pham";
            var v = from t in _db.categories
                    where t.hide == true
                    orderby t.order ascending
                    select t;
            return PartialView(v.ToList());
        }
        public ActionResult getTrending_Tags(long id, string metatitle)
        {
            ViewBag.meta = "san-pham";
            var v = from t in _db.Trendings
                    where t.categoryid == id && t.hide == true
                    orderby t.order ascending
                    select t;

            return PartialView(v.ToList());
        }
        public ActionResult getCategoryTags()
        {
            ViewBag.meta = "tags";
            var v = from t in _db.category3
                    where t.hide == true
                    orderby t.order ascending
                    select t;
            return PartialView(v.ToList());
        }
        public ActionResult getTagTag(long id, string metatitle)
        {
            ViewBag.meta = "tags";
            var v = from t in _db.Tags
                    where t.categoryid3 == id && t.hide == true
                    orderby t.order ascending
                    select t;

            return PartialView(v.ToList());
        }
        public ActionResult getCategoryNews()
        {
            ViewBag.meta = "news";
            var v = from t in _db.category1
                    where t.hide == true
                    orderby t.order ascending
                    select t;
            return PartialView(v.ToList());
        }
        public ActionResult getNewNew_Tags(long id, string metatitle)
        {
            ViewBag.meta = "news";
            var v = from t in _db.NewNews
                    where t.categoryid1 == id && t.hide == true
                    orderby t.order ascending
                    select t;

            return PartialView(v.ToList());
        }
        public ActionResult getCategoryMost()
        {
            ViewBag.meta = "most";
            var v = from t in _db.category4
                    where t.hide == true
                    orderby t.order ascending
                    select t;
            return PartialView(v.ToList());
        }
        public ActionResult getMost_Tags(long id, string metatitle)
        {
            ViewBag.meta = "most";
            var v = from t in _db.Most
                    where t.categoryid4 == id && t.hide == true
                    orderby t.order ascending
                    select t;

            return PartialView(v.ToList());
        }
    }

}