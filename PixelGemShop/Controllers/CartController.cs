using PixelGemShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Security.Cryptography;

namespace PixelGemShop.Controllers
{
    public class CartController : Controller
    {
        private DBContext db = new DBContext();
        // GET: Cart
        public ActionResult Index()
        { 
            if (!User.Identity.IsAuthenticated)
            {
                TempData["Message"] = "You must log in first";
                return RedirectToAction("Index", "Login");

            }
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
                TempData["Message"] = "To add this product to your cart you must log in";
                return RedirectToAction("Index", "Login");

            }
            int currentUser = int.Parse(User.Identity.Name);
            int currentCart = db.Carts.FirstOrDefault(c => c.IdUser == currentUser).IdCart;
            var existingCartItem = db.CartItems.FirstOrDefault(c => c.IdProduct == idProduct && c.IdCart == currentCart);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity += quantity; //se esiste aumenti la quantità usando il numero in ingresso
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
        public ActionResult UpdateQuantity([Bind(Include = "idProduct")] int idProductMod, string increment, string decrement)
        {
            var productToModify = db.CartItems.Where(p => p.IdProduct == idProductMod).FirstOrDefault();
            if (productToModify != null)
            {
                if(increment != null)
                {
                    productToModify.Quantity++;
                    db.Entry(productToModify).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["Message"] = "Product added to cart";
                }
                else if(decrement != null)
                {
                    productToModify.Quantity--;
                    db.Entry(productToModify).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["Message"] = "Product removed from cart";
                }
                if (productToModify.Quantity == 0)
                {
                    db.CartItems.Remove(productToModify);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index", "Cart");
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
                TempData["Message"] = "Product removed from cart";
                return RedirectToAction("Index", "Cart");
            }
            return RedirectToAction("Index", "Cart");
        }
    }
}