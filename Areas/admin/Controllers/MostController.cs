using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BaoMoi.Models;
using BaoMoi.Help;
using System.IO;
using System.Data.Entity.Validation;

namespace BaoMoi.Areas.admin.Controllers.product
{
    public class MostController : Controller
    {
        private BaoMoiEntities1 db = new BaoMoiEntities1();

        // GET: admin/Most
        public ActionResult Index(long? id = null)
        {
            getCategory(id);
            //return View(db.products.ToList());
            return View();
        }

        public void getCategory(long? selectedId = null)
        {
            ViewBag.Category = new SelectList(db.category4.Where(x => x.hide == true)
                .OrderBy(x => x.order), "id", "name", selectedId);
        }
        public ActionResult getMost(long? id)
        {
            if (id == null)
            {
                var v = db.Most.OrderBy(x => x.order).ToList();
                return PartialView(v);
            }
            var m = db.Most.Where(x => x.categoryid4 == id).OrderBy(x => x.order).ToList();
            return PartialView(m);
        }

        // GET: admin/Most/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Most most = db.Most.Find(id);
            if (most == null)
            {
                return HttpNotFound();
            }
            return View(most);
        }

        // GET: admin/Most/Create
        public ActionResult Create()
        {
            getCategory();
            return View();
        }

        // POST: admin/Most/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "id,name,img,link,description,meta,hide,order,datebegin,categoryid4,title,chitietup,chitietup1,chitietdown")] Most most, HttpPostedFileBase img)
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
                        path = Path.Combine(Server.MapPath("~/Content/upload/img/product"), filename);
                        img.SaveAs(path);
                        most.img = filename; //Lưu ý
                    }
                    else
                    {
                        most.img = "logo.png";
                    }
                    most.datebegin = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                    most.meta = Functions.ConvertToUnSign(most.meta); //convert Tiếng Việt không dấu
                    most.order = getMaxOrder(most.categoryid4);
                    db.Most.Add(most);
                    db.SaveChanges();
                    //return RedirectToAction("Index");
                    return RedirectToAction("Index", "Most", new { id = most.categoryid4 });
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

            return View(most);
        }

        // GET: admin/Most/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Most most = db.Most.Find(id);
            if (most == null)
            {
                return HttpNotFound();
            }
            getCategory(most.categoryid4);
            return View(most);
        }

        // POST: admin/Most/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "id,name,img,link,description,meta,hide,order,datebegin,categoryid4,title,chitietup,chitietup1,chitietdown")] Most most, HttpPostedFileBase img)
        {
            try
            {
                var path = "";
                var filename = "";
                Most temp = db.Most.Find(most.id);
                if (ModelState.IsValid)
                {
                    if (img != null)
                    {
                        //filename = Guid.NewGuid().ToString() + img.FileName;
                        filename = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss-") + img.FileName;
                        path = Path.Combine(Server.MapPath("~/Content/upload/img/product"), filename);
                        img.SaveAs(path);
                        temp.img = filename; //Lưu ý
                    }
                    // temp.datebegin = Convert.ToDateTime(DateTime.Now.ToShortDateString());                   
                    temp.name = most.name;

                    temp.description = most.description;
                    temp.meta = Functions.ConvertToUnSign(most.meta); //convert Tiếng Việt không dấu

                    temp.hide = most.hide;
                    temp.order = most.order;
                    temp.chitietdown = most.chitietdown;
                    temp.chitietup = most.chitietup;
                    temp.chitietup1 = most.chitietup1;
                    temp.title = most.title;

                    db.Entry(temp).State = EntityState.Modified;
                    db.SaveChanges();
                    //return RedirectToAction("Index");
                    return RedirectToAction("Index", "Most", new { id = most.categoryid4 });
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
            return View(most);
        }

        // GET: admin/Most/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Most most = db.Most.Find(id);
            if (most == null)
            {
                return HttpNotFound();
            }
            return View(most);
        }

        // POST: admin/Most/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Most most = db.Most.Find(id);
            db.Most.Remove(most);
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
        public int getMaxOrder(long? CategoryId)
        {
            if (CategoryId == null)
                return 1;
            return db.Most.Where(x => x.categoryid4 == CategoryId).Count();
        }
    }
}
