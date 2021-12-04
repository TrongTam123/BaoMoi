using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaoMoi.Models;
using PagedList;
using PagedList.Mvc;

namespace BaoMoi.Controllers
{
    public class LatestController : Controller
    {
        BaoMoiEntities1 _db = new BaoMoiEntities1();
        // GET: Latest
        public ActionResult Index(string meta)
        {
            var v = from t in _db.category2
                    where t.meta == meta
                    select t;
            return View(v.FirstOrDefault());
        }
        public ActionResult DetailLasts(long id)
        {
            var v = from t in _db.Lasts
                    where t.id == id
                    select t;
            return View(v.FirstOrDefault());
        }
        public ActionResult ViewPage(int? page,int? pageSize)
        {
            if (page == null)
            {
                page = 1;
            }
            if(pageSize==null)
            {
                pageSize = 10;
            }
           
            return View();
        }
    }
}