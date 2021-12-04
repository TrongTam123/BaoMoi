using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaoMoi.Models;
namespace BaoMoi.Controllers.Footer
{
    public class FooterController : Controller
    {
        BaoMoiEntities1 _db = new BaoMoiEntities1();
        // GET: getFooter
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult getFooter()
        {
            var v = from t in _db.footers
                    where t.hide == true
                    orderby t.order ascending
                    select t;
            return PartialView(v.ToList());
        }
        public ActionResult getFooter1()
        {
            var v = from t in _db.footer1
                    where t.hide == true
                    orderby t.order ascending
                    select t;
            return PartialView(v.ToList());
        }
        public ActionResult getFooter2()
        {
            var v = from t in _db.footer2
                    where t.hide == true
                    orderby t.order ascending
                    select t;
            return PartialView(v.ToList());
        }
        public ActionResult getFooter3()
        {
            var v = from t in _db.footer3
                    where t.hide == true
                    orderby t.order ascending
                    select t;
            return PartialView(v.ToList());
        }
        public ActionResult getAbout()
        {
            var v = from t in _db.Abouts
                    where t.hide == true
                    orderby t.order ascending
                    select t;
            return PartialView(v.ToList());
        }
        public ActionResult getIcon()
        {
            var v = from t in _db.icons
                    where t.hide == true
                    orderby t.order ascending
                    select t;
            return PartialView(v.ToList());
        }
        public ActionResult getCate()
        {
            ViewBag.meta = "tags";
            var v = from t in _db.Tags
                    where t.hide == true
                    orderby t.order ascending
                    select t;
            return PartialView(v.ToList());
        }
        public ActionResult getViewed()
        {
            var v = from t in _db.Viewds
                    where t.hide == true
                    orderby t.order ascending
                    select t;
            return PartialView(v.ToList());
        }

        public ActionResult getPosts()
        {
            var v = from t in _db.Posts
                    where t.hide == true
                    orderby t.order ascending
                    select t;
            return PartialView(v.ToList());
        }
        public ActionResult getFooterdown1()
        {
            var v = from t in _db.footerdown1
                    where t.hide == true
                    orderby t.order ascending
                    select t;
            return PartialView(v.ToList());
        }
        public ActionResult getFooterdown2()
        {
            var v = from t in _db.menus
                    where t.hide == true
                    orderby t.order ascending
                    select t;
            return PartialView(v.ToList());
        }
    }
}