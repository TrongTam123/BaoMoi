using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BaoMoi.Models;
using BaoMoi.Help;

namespace BaoMoi.Areas.admin.Controllers
{
    public class NewsTiepsController : Controller
    {
        private BaoMoiEntities1 db = new BaoMoiEntities1();

        // GET: admin/NewsTieps
        public ActionResult Index()
        {
            return View(db.NewsTieps.ToList());
        }

        // GET: admin/NewsTieps/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsTiep newsTiep = db.NewsTieps.Find(id);
            if (newsTiep == null)
            {
                return HttpNotFound();
            }
            return View(newsTiep);
        }

        // GET: admin/NewsTieps/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/NewsTieps/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "id,name,img,link,description,detail,meta,hide,order,datebegin,title,chitietup,chitietup1,chitietdown")] NewsTiep newsTiep, HttpPostedFileBase img)
        {
            try
            {
                var path = "";
                var filename = "";
                if (ModelState.IsValid)
                {
                    if (img != null)
                    {
                        //filename = Guid.NewGuid().ToString() + img.FileName;
                        filename = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss-") + img.FileName;
                        path = Path.Combine(Server.MapPath("~/Content/upload/img/news"), filename);
                        img.SaveAs(path);
                        newsTiep.img = filename; //Lưu ý
                    }
                    else
                    {
                        newsTiep.img = "logo.png";
                    }
                    newsTiep.datebegin = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                    newsTiep.meta = Functions.ConvertToUnSign(newsTiep.meta); //convert Tiếng Việt không dấu
                    db.NewsTieps.Add(newsTiep);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DbEntityValidationException e)
            {
                throw e;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View(newsTiep);
        }

        // GET: admin/NewsTieps/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsTiep newsTiep = db.NewsTieps.Find(id);
            if (newsTiep == null)
            {
                return HttpNotFound();
            }
            return View(newsTiep);
        }

        // POST: admin/NewsTieps/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "id,name,img,link,description,detail,meta,hide,order,datebegin,title,chitietup,chitietup1,chitietdown")] NewsTiep newsTiep, HttpPostedFileBase img)
        {
            try
            {
                var path = "";
                var filename = "";
                NewsTiep temp = getById(newsTiep.id);
                if (ModelState.IsValid)
                {
                    if (img != null)
                    {
                        //filename = Guid.NewGuid().ToString() + img.FileName;
                        filename = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss-") + img.FileName;
                        path = Path.Combine(Server.MapPath("~/Content/upload/img/news"), filename);
                        img.SaveAs(path);
                        temp.img = filename; //Lưu ý
                    }
                    // temp.datebegin = Convert.ToDateTime(DateTime.Now.ToShortDateString());                   
                    temp.name = newsTiep.name;
                    temp.description = newsTiep.description;
                    temp.detail = newsTiep.detail;
                    temp.meta = Functions.ConvertToUnSign(newsTiep.meta); //convert Tiếng Việt không dấu
                    temp.hide = newsTiep.hide;
                    temp.order = newsTiep.order;
                    db.Entry(temp).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DbEntityValidationException e)
            {
                throw e;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(newsTiep);
        }

        public NewsTiep getById(long id)
        {
            return db.NewsTieps.Where(x => x.id == id).FirstOrDefault();

        }

        // GET: admin/NewsTieps/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsTiep newsTiep = db.NewsTieps.Find(id);
            if (newsTiep == null)
            {
                return HttpNotFound();
            }
            return View(newsTiep);
        }

        // POST: admin/NewsTieps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NewsTiep newsTiep = db.NewsTieps.Find(id);
            db.NewsTieps.Remove(newsTiep);
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
