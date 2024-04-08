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
            var product = db.Products.Where(p => p.IdCategory == 3).ToList();
            return View(product);
        }
    }
}