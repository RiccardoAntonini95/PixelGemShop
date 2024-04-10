using PixelGemShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace PixelGemShop.Controllers
{
    public class CartController : Controller
    {
        private DBContext db = new DBContext();
        // GET: Cart
        public ActionResult Index()
        {
            int currentUser = int.Parse(User.Identity.Name);
            int currentIdCart = db.Carts.FirstOrDefault(c => c.IdUser == currentUser).IdCart;
            var products = db.CartItems.Include(p => p.Products).Where(p => p.IdCart == currentIdCart);
            return View(products.ToList());
            //renderizza tutti i cart items che hanno come id cart quello attuale
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //idproduct idcart quantity
        public ActionResult AddToCart([Bind(Include = "idProduct, quantity")] int idProduct, int quantity)
        {
            int currentUser = int.Parse(User.Identity.Name);
            CartItems product = new CartItems
            {
                IdCart = db.Carts.FirstOrDefault(c => c.IdUser == currentUser).IdCart,
                IdProduct = idProduct,
                Quantity = quantity
            };
            db.CartItems.Add(product);
            db.SaveChanges();

            TempData["Message"] = "Added to cart";

            return View(); //TODO: Tornare su pagine diverse quando si effettua l'aggiunta al carrello
        }
    }
}