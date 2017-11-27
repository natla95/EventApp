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
        // GET: Account
        public ActionResult Index()
        {
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
                try {
                    using(EventDbContext db = new EventDbContext()){
                        var item = (from li in db.Users
                                    where li.Email.Equals(_model.Email)
                                    select li).FirstOrDefault();
                        if (item == null){
                            User user = new User();
                            user.FirstName = _model.FirstName;
                            user.LastName = _model.LastName;
                            user.Email = _model.Email;
                            user.Password = _model.Password;
                            db.Users.Add(user);
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }
                        else {
                            Console.WriteLine("Niepoprawny model");
                        }
                    }
                }
                catch(Exception e) {
                    Console.WriteLine(e);
                }    
            }
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            ViewBag.PageNumber = 2;
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserViewModel _model)
        {
            return View();
        }
    }
}