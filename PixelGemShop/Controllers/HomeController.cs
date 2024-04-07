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
        DBContext db = new DBContext();
        public ActionResult Index()
        {
            var products = db.Products.ToList();
            var editorsChoiceProduct = products.OrderByDescending(p => p.Stock).ToList(); //ordinati per maggior stock
            var savingsProduct = products.Where(p => p.DiscountPercentage != null).ToList(); //selezionati solo quelli in sconto
            ViewBag.editorsChoice = editorsChoiceProduct;
            ViewBag.savings = savingsProduct;
            return View();
        }

        [Authorize(Roles = "User")]
        public ActionResult ProvaUser()
        {
            return View();
        }
    }
}
