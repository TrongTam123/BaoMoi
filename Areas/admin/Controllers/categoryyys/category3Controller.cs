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
    public class category3Controller : Controller
    {
        private BaoMoiEntities1 db = new BaoMoiEntities1();

        // GET: admin/category3
        public ActionResult Index()
        {
            return View(db.category3.ToList());
        }

        // GET: admin/category3/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            category3 category3 = db.category3.Find(id);
            if (category3 == null)
            {
                return HttpNotFound();
            }
            return View(category3);
        }

        // GET: admin/category3/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/category3/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,link,meta,hide,order,datebegin")] category3 category3)
        {
            if (ModelState.IsValid)
            {
                db.category3.Add(category3);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category3);
        }

        // GET: admin/category3/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            category3 category3 = db.category3.Find(id);
            if (category3 == null)
            {
                return HttpNotFound();
            }
            return View(category3);
        }

        // POST: admin/category3/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,link,meta,hide,order,datebegin")] category3 category3)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category3).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category3);
        }

        // GET: admin/category3/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            category3 category3 = db.category3.Find(id);
            if (category3 == null)
            {
                return HttpNotFound();
            }
            return View(category3);
        }

        // POST: admin/category3/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            category3 category3 = db.category3.Find(id);
            db.category3.Remove(category3);
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
