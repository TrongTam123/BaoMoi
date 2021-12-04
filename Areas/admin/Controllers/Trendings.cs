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
    public class TrendingsController : Controller
    {
        private BaoMoiEntities1 db = new BaoMoiEntities1();

        // GET: admin/Trendings
        public ActionResult Index(long? id = null)
        {
            getCategory(id);
            //return View(db.products.ToList());
            return View();
        }

        public void getCategory(long? selectedId = null)
        {
            ViewBag.Category = new SelectList(db.categories.Where(x => x.hide == true)
                .OrderBy(x => x.order), "id", "name", selectedId);
        }
        public ActionResult getTrendings(long? id)
        {
            if (id == null)
            {
                var v = db.Trendings.OrderBy(x => x.order).ToList();
                return PartialView(v);
            }
            var m = db.Trendings.Where(x => x.categoryid == id).OrderBy(x => x.order).ToList();
            return PartialView(m);
        }

        // GET: admin/Trendings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trending trending = db.Trendings.Find(id);
            if (trending == null)
            {
                return HttpNotFound();
            }
            return View(trending);
        }

        // GET: admin/Trendings/Create
        public ActionResult Create()
        {
            getCategory();
            return View();
        }

        // POST: admin/Trendings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "id,name,img,link,description,meta,hide,order,datebegin,categoryid,title,chitietup,chitietdown,chitietup1")] Trending trending, HttpPostedFileBase img)
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
                        trending.img = filename; //Lưu ý
                    }
                    else
                    {
                        trending.img = "logo.png";
                    }
                    trending.datebegin = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                    trending.meta = Functions.ConvertToUnSign(trending.meta); //convert Tiếng Việt không dấu
                    trending.order = getMaxOrder(trending.categoryid);
                    db.Trendings.Add(trending);
                    db.SaveChanges();
                    //return RedirectToAction("Index");
                    return RedirectToAction("Index", "Trendings", new { id = trending.categoryid });
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

            return View(trending);
        }

        // GET: admin/Trendings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trending trending = db.Trendings.Find(id);
            if (trending == null)
            {
                return HttpNotFound();
            }
            getCategory(trending.categoryid);
            return View(trending);
        }

        // POST: admin/Trendings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "id,name,img,link,description,meta,hide,order,datebegin,categoryid,title,chitietup,chitietdown,chitietup1")] Trending trending, HttpPostedFileBase img)
        {
            try
            {
                var path = "";
                var filename = "";
                Trending temp = db.Trendings.Find(trending.id);
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
                    temp.name = trending.name;
                    
                    temp.description = trending.description;
                    temp.meta = Functions.ConvertToUnSign(trending.meta); //convert Tiếng Việt không dấu
                    
                    temp.hide = trending.hide;
                    temp.order = trending.order;
                    temp.chitietdown = trending.chitietdown;
                    temp.chitietup = trending.chitietup;
                    temp.chitietup1 = trending.chitietup1;
                    temp.title = trending.title;
                    
                    db.Entry(temp).State = EntityState.Modified;
                    db.SaveChanges();
                    //return RedirectToAction("Index");
                    return RedirectToAction("Index", "Trendings", new { id = trending.categoryid });
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
            return View(trending);
        }

        // GET: admin/Trendings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trending trending = db.Trendings.Find(id);
            if (trending == null)
            {
                return HttpNotFound();
            }
            return View(trending);
        }

        // POST: admin/Trendings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Trending trending = db.Trendings.Find(id);
            db.Trendings.Remove(trending);
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
