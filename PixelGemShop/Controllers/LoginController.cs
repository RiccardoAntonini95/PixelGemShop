using PixelGemShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PixelGemShop.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                TempData["Message"] = "You must log out before you can log in with another account.";
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string email, string password)
        {
            using (var db = new DBContext())
            {
                if (ModelState.IsValid)
                {
                    try
                    {   //query select che cerca l'utente giusto
                        var loggedUser = db.Users.Where(model => model.Email == email && model.Password == password).FirstOrDefault();
                        if (loggedUser != null)
                        {
                            FormsAuthentication.SetAuthCookie(loggedUser.IdUser.ToString(), true);//salvo l'id ottenuto dalla select e lo passo al rolemanager
                            TempData["Message"] = "Logged in successfully";
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    catch (Exception ex)
                    {
                        return View(ex.Message);
                    }
                }
                TempData["Message"] = "Invalid credentials";
                return View();
            }
        }
        public ActionResult SignUp()
        {
            if (HttpContext.User.Identity.IsAuthenticated) // se sei già loggato torna indietro
            {
                TempData["Fail"] = "You must log out before you can sign up a new account";
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp([Bind(Include = "Username, Password, Email, Role, FirstName, LastName, Phone, Address")] Users newUser)
        {
            using (var db = new DBContext())
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        db.Users.Add(newUser);
                        Carts cart = new Carts();
                        cart.IdUser = newUser.IdUser;
                        db.Carts.Add(cart);
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        return View(ex.Message);
                    }
                    TempData["Message"] = "You have signed up for a new account";
                    return RedirectToAction("Index", "Login");
                }
                return View();
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            TempData["Message"] = "Logged Out successfully";
            return RedirectToAction("Index", "Login");
        }
    }
}