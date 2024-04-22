using Microsoft.Ajax.Utilities;
using PixelGemShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PixelGemShop.Controllers
{
    public class UserController : Controller
    {
        private DBContext db = new DBContext();
        // GET: User
        public ActionResult Index()
        {
            var currentUser = int.Parse(User.Identity.Name);
            var user = db.Users.Find(currentUser);
            return View(user);
        }

        public ActionResult Edit()
        {
            var currentUser = int.Parse(User.Identity.Name);
            var user = db.Users.Find(currentUser);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdUser, Username, Password, Email, Role, FirstName, LastName, Phone, Address")] Users user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(user);
            }
        }

        public ActionResult Delete()
        {
            var currentUser = int.Parse(User.Identity.Name);
            var user = db.Users.Find(currentUser);
            var userCart = db.Carts.Where(u => u.IdUser == currentUser).FirstOrDefault();
            db.Carts.Remove(userCart);
            db.Users.Remove(user);
            db.SaveChanges();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult OrderHistory()
        {
            var currentUser = int.Parse(User.Identity.Name);
            var orders = db.Orders.Where(o => o.IdUser == currentUser).Include(o => o.OrderDetails).OrderByDescending(o => o.DateOrder);
            var ordersCount = orders.Count();
            ViewBag.OrdersCount = ordersCount;
            return View(orders.ToList());
        }
    }
}