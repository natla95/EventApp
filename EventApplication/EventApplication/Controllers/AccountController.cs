using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventApplication.Models;
using EventApplication.Models.DB;
using EventApplication.Models.ViewModels;
using System.Web.Security;

namespace EventApplication.Controllers
{
    
    public class AccountController : Controller
    {
        private EventDbContext _db;
        public AccountController()
        {
            _db = new EventDbContext();
        }   

        [HttpGet]
        [ActionName("Home")]
        [Authorize]
        public ActionResult Index()
        {
            var user = User as MyPrincipal;
            var login = user.UserDetails.Email;
            var itemUser = _db.Users.FirstOrDefault(u => u.Email.Equals(login));
            ViewBag.UserName = user.UserDetails.Email;
            ViewBag.IconNumber = 0;
            ViewBag.Role = itemUser.RoleID;
            return View("Index");
        }

        [HttpGet]
        [ActionName("Register")]
        public ActionResult Register()
        {
            ViewBag.PageNumber = 1;
            return View();
        }

        [HttpPost]
        [ActionName("Register")]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserViewModel _model)
        {
            ViewBag.PageNumber = 1;
            if (ModelState.IsValid){
                using(_db){
                    var item = _db.Users.FirstOrDefault(x => x.Email == _model.Email);
                    if (item == null){
                        User user = new User();
                        user.FirstName = _model.FirstName;
                        user.LastName = _model.LastName;
                        user.Email = _model.Email;
                        user.Password = Security.Hash(_model.Password);
                        user.RoleID = 2;
                        _db.Users.Add(user);
                        _db.SaveChanges();
                        return RedirectToAction("Login");
                    }
                    ModelState.AddModelError("Email", "Użytkownik o podanym loginie już istnieje");
                }  
            }
            return View(_model);
        }

        [HttpGet]
        [ActionName("Login")]
        public ActionResult Login()
        {
            ViewBag.PageNumber = 2;
            return View();
        }

        [HttpPost]
        [ActionName("Login")]
        public ActionResult Login(LoginViewModel _model)
        {
            ViewBag.PageNumber = 2;
            if (ModelState.IsValid)
            {
                string login = _model.Email;
                string password = Security.Hash(_model.Password);

                User user = null;

                using (_db){
                    user = _db.Users.FirstOrDefault(u => u.Email.Equals(login) && u.Password.Equals(password));
                }
                if(user != null){
                    FormsAuthentication.SetAuthCookie(user.Email, false);
                    var authTicket = new FormsAuthenticationTicket(1, user.Email, DateTime.UtcNow, DateTime.UtcNow.AddMinutes(30), false, "");
                    var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(authTicket));
                    authCookie.Expires = DateTime.UtcNow.AddMinutes(30);
                    Response.SetCookie(authCookie);
                    return RedirectToAction("Home", "Account");
                }
                else {
                    ModelState.AddModelError("Password", "Niepoprawny login lub hasło");
                    return View(_model);
                }
            }
            return View(_model);
        }

        [HttpGet]
        [ActionName("Logout")]
        public ActionResult Loginout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}