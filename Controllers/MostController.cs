using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaoMoi.Models;

namespace BaoMoi.Controllers
{
    public class MostController : Controller
    {
        BaoMoiEntities1 _db = new BaoMoiEntities1();
        // GET: Most
        public ActionResult Index(String meta)
        {
            var v = from t in _db.category4
                    where t.meta == meta
                    select t;
            return View(v.FirstOrDefault());
        }

        public ActionResult DetailMost(long id)
        {
            var v = from t in _db.Most
                    where t.id == id
                    select t;
            return View(v.FirstOrDefault());
        }
    }
}