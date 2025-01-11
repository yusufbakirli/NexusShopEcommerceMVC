using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Tez.MvcWebUI.Entity;

namespace Tez.MvcWebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly DataContext db = new DataContext();

        // GET: Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = db.Products
                .Include(p => p.Comments)
                .FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/Home/Details.cshtml", product);
        }

        // Kullanıcının ürünü satın alıp almadığını kontrol eden metot
        private bool HasPurchasedProduct(string userName, int productId)
        {
            return db.OrderLines
                    .Any(ol => ol.Order.Username == userName && ol.ProductId == productId);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddComment(int productId, string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return RedirectToAction("Details", new { id = productId });
            }

            // Kullanıcının ürünü satın alıp almadığını kontrol et
            if (!HasPurchasedProduct(User.Identity.Name, productId))
            {
                TempData["Error"] = "Bu ürüne yorum yapabilmek için satın almış olmanız gerekiyor.";
                return RedirectToAction("Details", new { id = productId });
            }

            var comment = new Comment
            {
                ProductId = productId,
                Content = content,
                UserName = User.Identity.Name
            };

            db.Comments.Add(comment);
            db.SaveChanges();

            return RedirectToAction("Details", new { id = productId });
        }

        [Authorize(Roles = "admin")]
        // GET: Product
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Category);
            return View(products.ToList());
        }

        [Authorize(Roles = "admin")]
        public ActionResult DeleteComment(int commentId, int productId)
        {
            var comment = db.Comments.FirstOrDefault(c => c.Id == commentId);
            if (comment == null)
            {
                return HttpNotFound();
            }

            db.Comments.Remove(comment);
            db.SaveChanges();

            return RedirectToAction("Details", new { id = productId });
        }

        [Authorize(Roles = "admin")]
        // GET: Product/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            return View();
        }

        [Authorize(Roles = "admin")]
        // POST: Product/Create
        // Aşırı gönderim saldırılarından korunmak için, bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Features,Price,Code,Image,IsHome,IsApproved,CategoryId")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        [Authorize(Roles = "admin")]
        // GET: Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        [Authorize(Roles = "admin")]
        // POST: Product/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Features,Price,Code,Image,IsHome,IsApproved,CategoryId")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        [Authorize(Roles = "admin")]
        // GET: Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [Authorize(Roles = "admin")]
        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
