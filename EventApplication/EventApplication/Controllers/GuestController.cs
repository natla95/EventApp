﻿using System;
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
            var user = User as MyPrincipal;
            var login = user.UserDetails.Email;
            ViewBag.IconNr = 3;
            ViewBag.UserName = user.UserDetails.Email;

            using (EventDbContext _db = new EventDbContext())
            {
                var itemUser = _db.Users.FirstOrDefault(u => u.Email.Equals(login));
                ViewBag.Role = itemUser.RoleID;
            }
                return View();
        }

        [HttpGet]
        [ActionName("AddGuest")]
        public ActionResult AddGuest()
        {
            var user = User as MyPrincipal;
            var login = user.UserDetails.Email;
            ViewBag.UserName = user.UserDetails.Email;
            ViewBag.IconNr = 3;

            ViewBag.AgeList = new List<SelectListItem>
                        {
                            new SelectListItem { Text = "Dorosły", Value = "Dorosły" },
                            new SelectListItem { Text = "Dziecko",  Value = "Dziecko" },
                        };
            using (EventDbContext _db = new EventDbContext())
            {
                var itemUser = _db.Users.FirstOrDefault(u => u.Email.Equals(login));
                ViewBag.Role = itemUser.RoleID;
            }
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
            var user = User as MyPrincipal;
            var login = user.UserDetails.Email;
            ViewBag.UserName = user.UserDetails.Email;
            ViewBag.IconNr = 3;

            ViewBag.AgeList = new List<SelectListItem>
                        {
                            new SelectListItem { Text = "Dorosły", Value = "Dorosły" },
                            new SelectListItem { Text = "Dziecko",  Value = "Dziecko" },
                        };
            using (EventDbContext _db = new EventDbContext())
            {
                var itemUser = _db.Users.FirstOrDefault(u => u.Email.Equals(login));
                ViewBag.Role = itemUser.RoleID;
            }
            return View("EditGuest");
        }

        [HttpPost]
        [ActionName("EditGuest")]
        public ActionResult EditGuest(GuestViewModel _model)
        {
            return RedirectToAction("GuestsList");
        }

        [HttpPost]
        [ActionName("DeleteGuest")]
        public ActionResult DeleteGuest()
        {
            return RedirectToAction("GuestsList");
        }

    }
}