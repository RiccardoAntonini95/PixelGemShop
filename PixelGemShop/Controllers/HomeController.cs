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
            var products = db.Products.Include(p => p.Categories).FirstOrDefault(); //seleziona 3 banner per le categorie
            return View(products);
        }

        [Authorize(Roles = "User")]
        public ActionResult ProvaUser()
        {
            return View();
        }
    }
}
