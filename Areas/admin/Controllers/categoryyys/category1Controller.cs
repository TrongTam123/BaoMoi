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
    public class category1Controller : Controller
    {
        private BaoMoiEntities1 db = new BaoMoiEntities1();

        // GET: admin/category1
        public ActionResult Index()
        {
            return View(db.category1.ToList());
        }

        // GET: admin/category1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            category1 category1 = db.category1.Find(id);
            if (category1 == null)
            {
                return HttpNotFound();
            }
            return View(category1);
        }

        // GET: admin/category1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/category1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,link,meta,hide,order,datebegin")] category1 category1)
        {
            if (ModelState.IsValid)
            {
                db.category1.Add(category1);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category1);
        }

        // GET: admin/category1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            category1 category1 = db.category1.Find(id);
            if (category1 == null)
            {
                return HttpNotFound();
            }
            return View(category1);
        }

        // POST: admin/category1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,link,meta,hide,order,datebegin")] category1 category1)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category1).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category1);
        }

        // GET: admin/category1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            category1 category1 = db.category1.Find(id);
            if (category1 == null)
            {
                return HttpNotFound();
            }
            return View(category1);
        }

        // POST: admin/category1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            category1 category1 = db.category1.Find(id);
            db.category1.Remove(category1);
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
