using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaoMoi.Models;

namespace BaoMoi.Controllers
{
    public class NewsTiepController : Controller
    {
        BaoMoiEntities1 _db = new BaoMoiEntities1();
        // GET: NewsTiep
        public ActionResult Index(String meta)
        {
            var v = from t in _db.NewsTieps
                    where t.meta == meta
                    select t;
            return View();
        }
        public ActionResult DetailNewwsTiep(long id)
        {
            var v = from t in _db.NewsTieps
                    where t.id == id
                    select t;
            return View(v.FirstOrDefault());
        }

    }
}