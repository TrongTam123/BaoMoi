using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BaoMoi.Models;

namespace BaoMoi.Areas.admin.Controllers.categoryyys
{
    public class category6Controller : Controller
    {
        private BaoMoiEntities1 db = new BaoMoiEntities1();

        // GET: admin/category6
        public ActionResult Index()
        {
            return View(db.category6.ToList());
        }

        // GET: admin/category6/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            category6 category6 = db.category6.Find(id);
            if (category6 == null)
            {
                return HttpNotFound();
            }
            return View(category6);
        }

        // GET: admin/category6/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/category6/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,link,meta,hide,order,datebegin")] category6 category6)
        {
            if (ModelState.IsValid)
            {
                db.category6.Add(category6);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category6);
        }

        // GET: admin/category6/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            category6 category6 = db.category6.Find(id);
            if (category6 == null)
            {
                return HttpNotFound();
            }
            return View(category6);
        }

        // POST: admin/category6/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,link,meta,hide,order,datebegin")] category6 category6)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category6).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category6);
        }

        // GET: admin/category6/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            category6 category6 = db.category6.Find(id);
            if (category6 == null)
            {
                return HttpNotFound();
            }
            return View(category6);
        }

        // POST: admin/category6/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            category6 category6 = db.category6.Find(id);
            db.category6.Remove(category6);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
