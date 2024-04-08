using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using PixelGemShop.Models;

namespace PixelGemShop.Controllers
{
    public class VideogamesController : Controller
    {
        private DBContext db = new DBContext();
        // GET: Videogames
        public ActionResult Index()
        {
            var product = db.Products.Where(p => p.IdCategory == 2).ToList();
            return View(product);
        }
    }
}