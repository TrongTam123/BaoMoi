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

namespace BaoMoi.Areas.admin.Controllers
{
    public class ad_LastsController : Controller
    {
        private BaoMoiEntities1 db = new BaoMoiEntities1();

        // GET: admin/ad_Lasts
        public ActionResult Index(long? id = null)
        {
            getCategory(id);
            //return View(db.products.ToList());
            return View();
        }

        public void getCategory(long? selectedId = null)
        {
            ViewBag.Category = new SelectList(db.category2.Where(x => x.hide == true)
                .OrderBy(x => x.order), "id", "name", selectedId);
        }
        public ActionResult get_ad_Lasts(long? id)
        {
            if (id == null)
            {
                var v = db.Lasts.OrderBy(x => x.order).ToList();
                return PartialView(v);
            }
            var m = db.Lasts.Where(x => x.categoryid2 == id).OrderBy(x => x.order).ToList();
            return PartialView(m);
        }


        // GET: admin/ad_Lasts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Last last = db.Lasts.Find(id);
            if (last == null)
            {
                return HttpNotFound();
            }
            return View(last);
        }

        // GET: admin/ad_Lasts/Create
        public ActionResult Create()
        {
            getCategory();
            return View();
        }

        // POST: admin/ad_Lasts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "id,name,img,link,description,detail,title,chitietup,chitietup1,chitietdown,meta,hide,order,datebegin,categoryid2")] Last last, HttpPostedFileBase img)
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
                        last.img = filename; //Lưu ý
                    }
                    else
                    {
                        last.img = "logo.png";
                    }
                    last.datebegin = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                    last.meta = Functions.ConvertToUnSign(last.meta); //convert Tiếng Việt không dấu
                    last.order = getMaxOrder(last.categoryid2);
                    db.Lasts.Add(last);
                    db.SaveChanges();
                    //return RedirectToAction("Index");
                    return RedirectToAction("Index", "ad_Lasts", new { id = last.categoryid2 });
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

            return View(last);
        }

        // GET: admin/ad_Lasts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Last last = db.Lasts.Find(id);
            if (last == null)
            {
                return HttpNotFound();
            }
            getCategory(last.categoryid2);
            return View(last);
        }

        // POST: admin/ad_Lasts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "id,name,img,link,description,detail,title,chitietup,chitietup1,chitietdown,meta,hide,order,datebegin,categoryid2")] Last last, HttpPostedFileBase img)
        {
            try
            {
                var path = "";
                var filename = "";
                Last temp = db.Lasts.Find(last.id);
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
                    temp.name = last.name;

                    temp.description = last.description;
                    temp.meta = Functions.ConvertToUnSign(last.meta); //convert Tiếng Việt không dấu

                    temp.hide = last.hide;
                    temp.order = last.order;
                    temp.chitietdown = last.chitietdown;
                    temp.chitietup = last.chitietup;
                    temp.chitietup1 = last.chitietup1;
                    temp.title = last.title;

                    db.Entry(temp).State = EntityState.Modified;
                    db.SaveChanges();
                    //return RedirectToAction("Index");
                    return RedirectToAction("Index", "ad_Lasts", new { id = last.categoryid2 });
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
            return View(last);
        }

        // GET: admin/ad_Lasts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Last last = db.Lasts.Find(id);
            if (last == null)
            {
                return HttpNotFound();
            }
            return View(last);
        }

        // POST: admin/ad_Lasts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Last last = db.Lasts.Find(id);
            db.Lasts.Remove(last);
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
    }
}
