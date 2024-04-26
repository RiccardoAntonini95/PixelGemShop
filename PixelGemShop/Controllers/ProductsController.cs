using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PixelGemShop.Models;

namespace PixelGemShop.Controllers
{
    public class ProductsController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Products
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Categories);
            return View(products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var reviews = db.Reviews.Include(r => r.Users).Where(r => r.IdProduct == id).OrderByDescending(r => r.IdReview).ToList();
            var reviewsCount = reviews.Count();
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            ViewBag.Reviews = reviews;
            ViewBag.ReviewsCount = reviewsCount;
            return View(products);
        }

        // GET: Products/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.IdCategory = new SelectList(db.Categories, "IdCategory", "ProductCategory");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(Products products, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid) //TODO: GUARDA EDIT DI LINKEDIN PER LA MODIFICA FOTO
            {
                if (Image != null && Image.ContentLength > 0)
                {
                    // Verifica che il file sia un'immagine
                    if (Image.ContentType.ToLower().StartsWith("image/"))
                    {
                        // Imposta la proprietà dell'immagine del prodotto con il nome del file
                        products.Image = Path.GetFileName(Image.FileName);

                        // Salva il file sul server
                        var imagePath = Path.Combine(Server.MapPath("~/Uploads"), products.Image);
                        Image.SaveAs(imagePath);
                    }
                    else
                    {
                        ModelState.AddModelError("Image", "Il file deve essere un'immagine.");
                        ViewBag.IdCategory = new SelectList(db.Categories, "IdCategory", "ProductCategory", products.IdCategory);
                        return View(products);
                    }
                }

                db.Products.Add(products);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdCategory = new SelectList(db.Categories, "IdCategory", "ProductCategory", products.IdCategory);
            return View(products);
        }


        // GET: Products/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCategory = new SelectList(db.Categories, "IdCategory", "ProductCategory", products.IdCategory);
            return View(products);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "IdProduct,Name,Description,Image,Stock,IdCategory,Price,DiscountPercentage")] Products products, HttpPostedFileBase Image)
        {

            if (products == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                if (Image != null && Image.ContentLength > 0)
                {
                    // Verifica che il file sia un'immagine
                    if (Image.ContentType.ToLower().StartsWith("image/"))
                    {
                        // Sovrascrivi l'immagine esistente con la nuova
                        products.Image = Path.GetFileName(Image.FileName);

                        // Salva il file sul server
                        var imagePath = Path.Combine(Server.MapPath("~/Uploads"), products.Image);
                        Image.SaveAs(imagePath);
                    }
                    else
                    {
                        ModelState.AddModelError("Image", "Il file deve essere un'immagine.");
                        ViewBag.IdCategory = new SelectList(db.Categories, "IdCategory", "ProductCategory", products.IdCategory);
                        return View(products);
                    }
                }

                db.Entry(products).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdCategory = new SelectList(db.Categories, "IdCategory", "ProductCategory", products.IdCategory);
            return View(products);
        }

        // GET: Products/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Products products = db.Products.Find(id);
            db.Products.Remove(products);
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
