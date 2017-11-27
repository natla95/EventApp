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
        [HttpGet]
        public ActionResult Login()
        {
            ViewBag.PageNumber = 2;
            return View();
        }
    }
}