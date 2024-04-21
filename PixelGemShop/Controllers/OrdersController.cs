using PixelGemShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Data.Entity;

namespace PixelGemShop.Controllers
{
    public class OrdersController : Controller
    {
        private DBContext db = new DBContext();
        // GET: Orders
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OrderNow()
        {
            int currentUser = int.Parse(User.Identity.Name);
            int currentIdCart = db.Carts.FirstOrDefault(c => c.IdUser == currentUser).IdCart;
            var products = db.CartItems.Include(p => p.Products).Where(p => p.IdCart == currentIdCart).ToList();
            //1 - Generare l'ordine nella tabella orders
            var order = new Orders 
            {
                DateOrder = DateTime.Now,
                IdUser = Convert.ToInt32(User.Identity.Name)
            };
            db.Orders.Add(order);
            db.SaveChanges();

            //2 - Generare i dettagli dell'ordine per ogni oggetto nella tabella orderDetails per poter recuperare poi lo storico
            int IdOrder = order.IdOrder; // prendo l'id ordine che ha generato in automatico perchè incrementale
            foreach(var product in products)
            {
                OrderDetails productDetail = new OrderDetails
                {
                    IdProduct = product.IdProduct,
                    IdOrder = IdOrder,
                    Quantity = product.Quantity,
                    ProductName = product.Products.Name,
                    ProductPrice = product.Products.Price, //TODO: e se è in sconto?
                    ProductImage = product.Products.Image
                };
                db.OrderDetails.Add(productDetail);
                db.SaveChanges();
                //3 - Ridurre la quantità del prodotto nella tabella prodotti allo stock
                var productSelled = db.Products.Find(product.IdProduct);
                if(productSelled != null)
                {
                    productSelled.Stock -= product.Quantity;
                    db.Entry(productSelled).State = EntityState.Modified;
                    db.SaveChanges();
                }
                //4 - Svuotare questo carrello
                if (product != null)
                {
                    db.CartItems.Remove(product);
                    db.SaveChanges();
                }
            }
            TempData["Message"] = "Thanks for purchasing!";
            return RedirectToAction("Index", "Cart");
        }
    }
}