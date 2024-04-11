using PixelGemShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PixelGemShop.Controllers
{
    public class UserController : Controller
    {
        private DBContext db = new DBContext();
        // GET: User
        public ActionResult Index()
        {
            var currentUser = int.Parse(User.Identity.Name);
            var user = db.Users.Where(u => u.IdUser == currentUser).FirstOrDefault();
            return View(user);
        }

        public ActionResult OrderHistory()
        {
            var currentUser = int.Parse(User.Identity.Name);
            var orders = db.Orders.Where(o => o.IdUser == currentUser).Include(o => o.OrderDetails);
            return View(orders.ToList());
        }
    }
}