using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaoMoi.Models;

namespace BaoMoi.Controllers
{
    public class TempCaoController : Controller
    {
        // GET: TempCao
        BaoMoiEntities1 _db = new BaoMoiEntities1();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult getmenuCao()
        {
            var v = from t in _db.menucaos
                    where t.hide == true
                    orderby t.order ascending
                    select t;
            return PartialView(v.ToList());
        }
    }
}