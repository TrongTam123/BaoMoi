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
    public class category4Controller : Controller
    {
        private BaoMoiEntities1 db = new BaoMoiEntities1();

        // GET: admin/category4
        public ActionResult Index()
        {
            return View(db.category4.ToList());
        }

        // GET: admin/category4/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            category4 category4 = db.category4.Find(id);
            if (category4 == null)
            {
                return HttpNotFound();
            }
            return View(category4);
        }

        // GET: admin/category4/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/category4/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,link,meta,hide,order,datebegin")] category4 category4)
        {
            if (ModelState.IsValid)
            {
                db.category4.Add(category4);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category4);
        }

        // GET: admin/category4/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            category4 category4 = db.category4.Find(id);
            if (category4 == null)
            {
                return HttpNotFound();
            }
            return View(category4);
        }

        // POST: admin/category4/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,link,meta,hide,order,datebegin")] category4 category4)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category4).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category4);
        }

        // GET: admin/category4/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            category4 category4 = db.category4.Find(id);
            if (category4 == null)
            {
                return HttpNotFound();
            }
            return View(category4);
        }

        // POST: admin/category4/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            category4 category4 = db.category4.Find(id);
            db.category4.Remove(category4);
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
