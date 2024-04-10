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
        { //TODO: Alternativa se carrello vuoto o se nessuno è loggato
            int currentUser = int.Parse(User.Identity.Name);
            int currentIdCart = db.Carts.FirstOrDefault(c => c.IdUser == currentUser).IdCart;
            var products = db.CartItems.Include(p => p.Products).Where(p => p.IdCart == currentIdCart);
            return View(products.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToCart([Bind(Include = "idProduct, quantity")] int idProduct, int quantity, string returnUrl)
        {
            if (!User.Identity.IsAuthenticated)
            {
                TempData["Fail"] = "To add this product to your cart you must log in";
                return RedirectToAction("Index", "Login");

            }
            int currentUser = int.Parse(User.Identity.Name);
            int currentCart = db.Carts.FirstOrDefault(c => c.IdUser == currentUser).IdCart;
            var existingCartItem = db.CartItems.FirstOrDefault(c => c.IdProduct == idProduct && c.IdCart == currentCart);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity += quantity; //se esiste aumenti la quantità del numero in ingresso
            }

            else
            {
                CartItems product = new CartItems
                {
                    IdCart = currentCart,
                    IdProduct = idProduct,
                    Quantity = quantity
                };
                db.CartItems.Add(product);

            }

            db.SaveChanges();

            TempData["Message"] = "Added to cart";

            return Redirect(returnUrl);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveFromCart([Bind(Include = "idProduct")] int idProduct)
        {
            var productToRemove = db.CartItems.Where(p => p.IdProduct == idProduct).FirstOrDefault();
            if (productToRemove != null)
            {
                db.CartItems.Remove(productToRemove);
                db.SaveChanges();
                TempData["Success"] = "Product removed from cart";
                return RedirectToAction("Index", "Cart");
            }
            return RedirectToAction("Index", "Cart");
        }
    }
}