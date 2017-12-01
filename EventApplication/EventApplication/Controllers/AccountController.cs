using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventApplication.Models;
using EventApplication.Models.DB;
using EventApplication.Models.ViewModels;

namespace EventApplication.Controllers
{
    public class AccountController : Controller
    {
        private EventDbContext _db;
        public AccountController()
        {
            _db = new EventDbContext();
        }   

        public ActionResult Index()
        {
            ViewBag.PageNumber = 1;
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            ViewBag.PageNumber = 1;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserViewModel _model)
        {
            if (ModelState.IsValid){
                using(_db){
                    var item = _db.Users.SingleOrDefault(x => x.Email == _model.Email);
                    if (item == null){
                        User user = new User();
                        user.FirstName = _model.FirstName;
                        user.LastName = _model.LastName;
                        user.Email = _model.Email;
                        user.Password = _model.Password;
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
        public ActionResult Login()
        {
            ViewBag.PageNumber = 2;
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel _model)
        {
            return View();
        }
    }
}