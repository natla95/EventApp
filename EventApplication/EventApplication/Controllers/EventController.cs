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
    public class EventController : Controller
    {
        [HttpGet]
        [ActionName("EventList")]
        public ActionResult Index()
        {
            ViewBag.IconNr = 1;
            return View("EventList");
        }

        [HttpGet]
        [ActionName("AddEvent")]
        public ActionResult AddEvent()
        {
            ViewBag.IconNr = 1;
            return View();
        }


        [HttpPost]
        [ActionName("AddEvent")]
        public ActionResult AddEvent(EventViewModel _model)
        {
            return View();
        }

        [HttpGet]
        [ActionName("EditEvent")]
        public ActionResult EditEvent()
        {
            ViewBag.IconNr = 1;
            return View();
        }

        [HttpPost]
        [ActionName("EditEvent")]
        public ActionResult EditEvent(EventViewModel _model)
        {
            return View();
        }

        [HttpGet]
        [ActionName("DeleteEvent")]
        public ActionResult DeleteEvent()
        {
            return RedirectToAction("EventList");
        }

    }
}