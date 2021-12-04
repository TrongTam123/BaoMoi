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
    public class TagsController : Controller
    {
        private BaoMoiEntities1 db = new BaoMoiEntities1();

        // GET: admin/Tags
        public ActionResult Index(long? id = null)
        {
            getCategory(id);
            //return View(db.products.ToList());
            return View();
        }

        public void getCategory(long? selectedId = null)
        {
            ViewBag.Category = new SelectList(db.category3.Where(x => x.hide == true)
                .OrderBy(x => x.order), "id", "name", selectedId);
        }
        public ActionResult getTags(long? id)
        {
            if (id == null)
            {
                var v = db.Tags.OrderBy(x => x.order).ToList();
                return PartialView(v);
            }
            var m = db.Tags.Where(x => x.categoryid3 == id).OrderBy(x => x.order).ToList();
            return PartialView(m);
        }

        // GET: admin/Tags/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tag tag = db.Tags.Find(id);
            if (tag == null)
            {
                return HttpNotFound();
            }
            return View(tag);
        }

        // GET: admin/Tags/Create
        public ActionResult Create()
        {
            getCategory();
            return View();
        }

        // POST: admin/Tags/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "id,name,img,link,meta,hide,order,datebegin,categoryid3,title,chitietup,chitietup1,chitietdown")] Tag tag, HttpPostedFileBase img)
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
                        tag.img = filename; //Lưu ý
                    }
                    else
                    {
                        tag.img = "logo.png";
                    }
                    tag.datebegin = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                    tag.meta = Functions.ConvertToUnSign(tag.meta); //convert Tiếng Việt không dấu
                    tag.order = getMaxOrder(tag.categoryid3);
                    db.Tags.Add(tag);
                    db.SaveChanges();
                    //return RedirectToAction("Index");
                    return RedirectToAction("Index", "Tags", new { id = tag.categoryid3 });
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

            return View(tag);
        }

        // GET: admin/Tags/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tag tag = db.Tags.Find(id);
            if (tag == null)
            {
                return HttpNotFound();
            }
            getCategory(tag.categoryid3);
            return View(tag);
        }

        // POST: admin/Tags/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "id,name,img,link,meta,hide,order,datebegin,categoryid3,title,chitietup,chitietup1,chitietdown")] Tag tag, HttpPostedFileBase img)
        {
            try
            {
                var path = "";
                var filename = "";
                Tag temp = db.Tags.Find(tag.id);
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
                    temp.name = tag.name;

                   
                    temp.meta = Functions.ConvertToUnSign(tag.meta); //convert Tiếng Việt không dấu

                    temp.hide = tag.hide;
                    temp.order = tag.order;
                    temp.chitietdown = tag.chitietdown;
                    temp.chitietup = tag.chitietup;
                    temp.chitietup1 = tag.chitietup1;
                    temp.title = tag.title;
                    
                    db.Entry(temp).State = EntityState.Modified;
                    db.SaveChanges();
                    //return RedirectToAction("Index");
                    return RedirectToAction("Index", "Tags", new { id = tag.categoryid3 });
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
            return View(tag);
        }

        // GET: admin/Tags/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tag tag = db.Tags.Find(id);
            if (tag == null)
            {
                return HttpNotFound();
            }
            return View(tag);
        }

        // POST: admin/Tags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tag tag = db.Tags.Find(id);
            db.Tags.Remove(tag);
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
            return db.Tags.Where(x => x.categoryid3 == CategoryId).Count();
        }
    }
}
