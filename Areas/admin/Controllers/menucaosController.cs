using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BaoMoi.Models;

namespace BaoMoi.Areas.admin.Controllers
{
    public class menucaosController : Controller
    {
        private BaoMoiEntities1 db = new BaoMoiEntities1();

        // GET: admin/menucaos
        public ActionResult Index()
        {
            return View(db.menucaos.ToList());
        }

        // GET: admin/menucaos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            menucao menucao = db.menucaos.Find(id);
            if (menucao == null)
            {
                return HttpNotFound();
            }
            return View(menucao);
        }

        // GET: admin/menucaos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/menucaos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,link,description,detail,meta,hide,order,datebegin")] menucao menucao)
        {
            if (ModelState.IsValid)
            {
                db.menucaos.Add(menucao);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(menucao);
        }

        // GET: admin/menucaos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            menucao menucao = db.menucaos.Find(id);
            if (menucao == null)
            {
                return HttpNotFound();
            }
            return View(menucao);
        }

        // POST: admin/menucaos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,link,description,detail,meta,hide,order,datebegin")] menucao menucao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(menucao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(menucao);
        }

        // GET: admin/menucaos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            menucao menucao = db.menucaos.Find(id);
            if (menucao == null)
            {
                return HttpNotFound();
            }
            return View(menucao);
        }

        // POST: admin/menucaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            menucao menucao = db.menucaos.Find(id);
            db.menucaos.Remove(menucao);
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
