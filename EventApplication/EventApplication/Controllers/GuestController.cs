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
    public class GuestController : Controller
    {
        [HttpGet]
        [ActionName("GuestsList")]
        public ActionResult Index()
        {
            ViewBag.IconNumber = 3;
            return View();
        }

        [HttpGet]
        [ActionName("AddGuest")]
        public ActionResult AddGuest()
        {
            return View();
        }

        [HttpPost]
        [ActionName("AddGuest")]
        public ActionResult AddGuest(GuestViewModel _model)
        {
            return View();

        }


    }
}