using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaoMoi.Models;
using System.Data.Entity;


namespace BaoMoi.Areas.admin.Controllers
{
    public class LoginController : Controller
    {
        private BaoMoiEntities1 db = new BaoMoiEntities1();

        [HttpGet]
        // GET: admin/Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(String username, string password)
        {
           
            return View();
        }

    }
}