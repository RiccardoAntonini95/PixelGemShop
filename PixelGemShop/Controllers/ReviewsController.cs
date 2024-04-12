using PixelGemShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PixelGemShop.Controllers
{
    public class ReviewsController : Controller
    {
        private DBContext db = new DBContext();
        // GET: Reviews/1
        public ActionResult Index(int? id)
        {
            var reviewsList = db.Reviews.Where(r => r.IdProduct == id).ToList();
            return View(reviewsList);
        }

        public ActionResult AddReview(int? id)
        {
            ViewBag.ProductId = id;
            return View(); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddReview([Bind(Include = "IdUser, Rate, Description, IdProduct")] Reviews Review)
        {
            Reviews newReview = new Reviews
            {
                IdUser = Review.IdUser,
                Rate = Review.Rate,
                Description = Review.Description,
                IdProduct = Review.IdProduct
            };
            db.Reviews.Add(newReview);
            db.SaveChanges();
            return RedirectToAction("OrderHistory", "User");
        }
    }
}