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
    [Authorize]
    public class GuestController : Controller
    {
        [HttpGet]
        [ActionName("GuestsList")]
        public ActionResult Index()
        {
            ViewBag.IconNr = 3;
            var user = User as MyPrincipal;
            ViewBag.UserName = user.UserDetails.Email;
            return View();
        }

        [HttpGet]
        [ActionName("AddGuest")]
        public ActionResult AddGuest()
        {
            ViewBag.IconNr = 3;
            var user = User as MyPrincipal;
            ViewBag.UserName = user.UserDetails.Email;

            ViewBag.AgeList = new List<SelectListItem>
                        {
                            new SelectListItem { Text = "Dorosły", Value = "Dorosły" },
                            new SelectListItem { Text = "Dziecko",  Value = "Dziecko" },
                        };
            return View();
        }

        [HttpPost]
        [ActionName("AddGuest")]
        public ActionResult AddGuest(GuestViewModel _model)
        {
       
            if (ModelState.IsValid)
            {

            }
            return View();

        }

        [HttpGet]
        [ActionName("EditGuest")]
        public ActionResult EditGuest()
        {
            ViewBag.IconNr = 3;
            var user = User as MyPrincipal;
            ViewBag.UserName = user.UserDetails.Email;

            ViewBag.AgeList = new List<SelectListItem>
                        {
                            new SelectListItem { Text = "Dorosły", Value = "Dorosły" },
                            new SelectListItem { Text = "Dziecko",  Value = "Dziecko" },
                        };
            return View("EditGuest");
        }

        [HttpPost]
        [ActionName("EditGuest")]
        public ActionResult EditGuest(GuestViewModel _model)
        {
            return RedirectToAction("GuestsList");
        }

    }
}