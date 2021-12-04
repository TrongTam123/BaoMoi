using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaoMoi.Models;
namespace BaoMoi.Controllers
{
    public class NewsController : Controller
    {
        BaoMoiEntities1 _db = new BaoMoiEntities1();
        // GET: News
        public ActionResult Index(String meta)
        {
            var v = from t in _db.News
                    where t.meta == meta
                    select t;
            return View();
        }
        public ActionResult DetailNewws(long id)
        {
            var v = from t in _db.News
                    where t.id == id
                    select t;
            return View(v.FirstOrDefault());
        }
    }
}