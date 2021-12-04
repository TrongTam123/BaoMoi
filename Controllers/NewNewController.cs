using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaoMoi.Models;
namespace BaoMoi.Controllers
{
    public class NewNewController : Controller
    {
        BaoMoiEntities1 _db = new BaoMoiEntities1();
        // GET: NewNew
        public ActionResult Index(string meta)
        {
            var v = from t in _db.category1
                    where t.meta == meta
                    select t;
            return View(v.FirstOrDefault());
        }
        public ActionResult DetailNews(long id)
        {
            var v = from t in _db.NewNews
                    where t.id == id
                    select t;
            return View(v.FirstOrDefault());
        }
    }
}