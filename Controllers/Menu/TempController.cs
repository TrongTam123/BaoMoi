using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaoMoi.Models;

namespace BaoMoi.Controllers
{
    public class TempController : Controller
    {
        BaoMoiEntities1 _db = new BaoMoiEntities1();
        //menu trên layout
        // GET: Temp
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult getMenu()
        {

            var v = from t in _db.menus
                    where t.hide == true
                    orderby t.order ascending
                    select t;
            return PartialView(v.ToList());
        }
        public ActionResult getmenuIcon()
        {
            var v = from t in _db.icons
                    where t.hide == true
                    orderby t.order ascending
                    select t;
            return PartialView(v.ToList());
        }
        
    }
}