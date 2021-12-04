using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BaoMoi.Models;
using BaoMoi.Help;
using System.Data.Entity.Validation;

namespace BaoMoi.Areas.admin.Controllers.product
{
    public class NewNewsController : Controller
    {
        private BaoMoiEntities1 db = new BaoMoiEntities1();

        // GET: admin/NewNews
        public ActionResult Index(long? id = null)
        {
            getCategory(id);
            //return View(db.products.ToList());
            return View();
        }

        public void getCategory(long? selectedId = null)
        {
            ViewBag.Category = new SelectList(db.category1.Where(x => x.hide == true)
                .OrderBy(x => x.order), "id", "name", selectedId);
        }
        public ActionResult getNewNews(long? id)
        {
            if (id == null)
            {
                var v = db.NewNews.OrderBy(x => x.order).ToList();
                return PartialView(v);
            }
            var m = db.NewNews.Where(x => x.categoryid1 == id).OrderBy(x => x.order).ToList();
            return PartialView(m);
        }

        // GET: admin/NewNews/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewNew newNew = db.NewNews.Find(id);
            if (newNew == null)
            {
                return HttpNotFound();
            }
            return View(newNew);
        }

        // GET: admin/NewNews/Create
        public ActionResult Create()
        {
            getCategory();
            return View();
        }

        // POST: admin/NewNews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "id,name,img,link,description,detail,meta,hide,order,datebegin,categoryid1,title,chitietup,chitietup1,chitietdown")] NewNew newNew, HttpPostedFileBase img)
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
                        newNew.img = filename; //Lưu ý
                    }
                    else
                    {
                        newNew.img = "logo.png";
                    }
                    newNew.datebegin = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                    newNew.meta = Functions.ConvertToUnSign(newNew.meta); //convert Tiếng Việt không dấu
                    newNew.order = getMaxOrder(newNew.categoryid1);
                    db.NewNews.Add(newNew);
                    db.SaveChanges();
                    //return RedirectToAction("Index");
                    return RedirectToAction("Index", "product", new { id = newNew.categoryid1 });
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

            return View(newNew);
        }

        // GET: admin/NewNews/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewNew newNew = db.NewNews.Find(id);
            if (newNew == null)
            {
                return HttpNotFound();
            }
            getCategory(newNew.categoryid1);
            return View(newNew);
        }

        // POST: admin/NewNews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "id,name,img,link,description,detail,meta,hide,order,datebegin,categoryid1,title,chitietup,chitietup1,chitietdown")] NewNew newNew, HttpPostedFileBase img)
        {
            try
            {
                var path = "";
                var filename = "";
                NewNew temp = db.NewNews.Find(newNew.id);
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
                    temp.name = newNew.name;

                    temp.description = newNew.description;
                    temp.meta = Functions.ConvertToUnSign(newNew.meta); //convert Tiếng Việt không dấu

                    temp.hide = newNew.hide;
                    temp.order = newNew.order;
                    temp.chitietdown = newNew.chitietdown;
                    temp.chitietup = newNew.chitietup;
                    temp.chitietup1 = newNew.chitietup1;
                    temp.title = newNew.title;

                    db.Entry(temp).State = EntityState.Modified;
                    db.SaveChanges();
                    //return RedirectToAction("Index");
                    return RedirectToAction("Index", "NewNews", new { id = newNew.categoryid1 });
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
            return View(newNew);
        }

        // GET: admin/NewNews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewNew newNew = db.NewNews.Find(id);
            if (newNew == null)
            {
                return HttpNotFound();
            }
            return View(newNew);
        }

        // POST: admin/NewNews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NewNew newNew = db.NewNews.Find(id);
            db.NewNews.Remove(newNew);
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
            return db.Trendings.Where(x => x.categoryid == CategoryId).Count();
        }
        //ViewBag.getMaxOrder = getMaxOrder(product.categoryid) + 1;
    }
}
