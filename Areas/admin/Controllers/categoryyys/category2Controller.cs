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
    public class category2Controller : Controller
    {
        private BaoMoiEntities1 db = new BaoMoiEntities1();

        // GET: admin/category2
        public ActionResult Index()
        {
            return View(db.category2.ToList());
        }

        // GET: admin/category2/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            category2 category2 = db.category2.Find(id);
            if (category2 == null)
            {
                return HttpNotFound();
            }
            return View(category2);
        }

        // GET: admin/category2/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/category2/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,link,meta,hide,order,datebegin")] category2 category2)
        {
            if (ModelState.IsValid)
            {
                db.category2.Add(category2);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category2);
        }

        // GET: admin/category2/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            category2 category2 = db.category2.Find(id);
            if (category2 == null)
            {
                return HttpNotFound();
            }
            return View(category2);
        }

        // POST: admin/category2/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,link,meta,hide,order,datebegin")] category2 category2)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category2).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category2);
        }

        // GET: admin/category2/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            category2 category2 = db.category2.Find(id);
            if (category2 == null)
            {
                return HttpNotFound();
            }
            return View(category2);
        }

        // POST: admin/category2/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            category2 category2 = db.category2.Find(id);
            db.category2.Remove(category2);
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
