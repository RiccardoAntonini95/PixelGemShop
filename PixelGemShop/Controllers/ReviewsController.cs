using PixelGemShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace PixelGemShop.Controllers
{
    public class ReviewsController : Controller
    {
        private DBContext db = new DBContext();
        // GET: Reviews/1
        public ActionResult Index(int? id) //ARRIVA ID USER non può essere facoltativo
        {
            var reviewsList = db.Reviews
                    .Include(r => r.Users)
                    .Include(r => r.Products)
                    .Where(r => r.IdUser == id)
                    .ToList();
            return View(reviewsList);
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
            TempData["Message"] = "Thank you for your feedback!";
            return RedirectToAction("OrderHistory", "User");
        }

        public ActionResult EditReview(int id)
        {
            var reviewToEdit = db.Reviews.Find(id);
            return View(reviewToEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditReview([Bind(Include = "IdReview, IdUser, Rate, Description, IdProduct")] Reviews reviewToEdit)
        {
            db.Entry(reviewToEdit).State = EntityState.Modified;
            db.SaveChanges();
            TempData["Message"] = "Feedback modified!";
            return RedirectToAction("Index", new { id = reviewToEdit.IdUser});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteReview([Bind(Include = "IdReview, IdUser")]int IdReview, int IdUser)
        {
            var reviewToDelete = db.Reviews.Find(IdReview);
            db.Reviews.Remove(reviewToDelete);
            db.SaveChanges();
            TempData["Message"] = "Feedback deleted!";
            return RedirectToAction("Index", new { id = IdUser });         
        }
    }
}