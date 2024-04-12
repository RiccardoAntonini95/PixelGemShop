using PixelGemShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PixelGemShop.Controllers
{
    public class AccessoriesController : Controller
    {
        private DBContext db = new DBContext();
        // GET: Accessories
        public ActionResult Index()
        {
            var products = db.Products.Where(p => p.IdCategory == 3).ToList();
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
            return View(products);
        }
    }
}