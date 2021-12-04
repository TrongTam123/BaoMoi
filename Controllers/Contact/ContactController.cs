using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaoMoi.Models;
namespace BaoMoi.Controllers.Contact
{
    public class ContactController : Controller
    {
        BaoMoiEntities1 _db = new BaoMoiEntities1();
        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult getmenuContact()
        {
            
            var v = from t in _db.menuContacts
                    where t.hide == true
                    orderby t.order ascending
                    select t;
            return PartialView(v.ToList());
        }
        public ActionResult getbannerContact()
        {

            var v = from t in _db.bannerContacts
                    where t.hide == true
                    orderby t.order ascending
                    select t;
            return PartialView(v.ToList());
        }
    }
}