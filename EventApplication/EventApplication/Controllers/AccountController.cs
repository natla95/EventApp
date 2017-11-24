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

        public ActionResult Register()
        {
            var user = new UserViewModel()
            {
                FirstName = "Jan",
                LastName = "Gawęda"
            };
            return View(user);
        }

        public ActionResult Login()
        {
            return View();
        }
    }
}