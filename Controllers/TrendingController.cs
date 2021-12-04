using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaoMoi.Models;
namespace BaoMoi.Controllers
{
    public class TrendingController : Controller
    {
        BaoMoiEntities1 _db = new BaoMoiEntities1();
        // GET: Trending
        public ActionResult Index(String meta)
        {
            var v = from t in _db.categories
                    where t.meta == meta
                    select t;
            return View(v.FirstOrDefault());
        }
        public ActionResult Detail(long id)
        {
            var v = from t in _db.Trendings
                    where t.id == id
                    select t;
            return View(v.FirstOrDefault());
        }
    }
}