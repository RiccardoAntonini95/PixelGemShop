using PixelGemShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PixelGemShop.Controllers
{
    public class HomeController : Controller
    {
        private DBContext db = new DBContext();
        public ActionResult Index()
        {
            var products = db.Products.ToList();
            foreach (var product in products)
            {
                var reviews = db.Reviews.Where(r => r.IdProduct == product.IdProduct).ToList();
                if (reviews.Any())
                {
                    product.AverageRating = reviews.Average(r => r.Rate);
                }
                else
                {
                    product.AverageRating = 0; // Imposta la media a 0 se non ci sono recensioni
                }
            }
            var editorsChoiceProduct = products.OrderByDescending(p => p.Stock).ToList(); //ordinati per maggior stock
            var savingsProduct = products.Where(p => p.DiscountPercentage != null).ToList(); //selezionati solo quelli in sconto
            ViewBag.editorsChoice = editorsChoiceProduct;
            ViewBag.savings = savingsProduct;
            return View();
        }
    }
}
