using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventApplication.Controllers
{
    public class EventController : Controller
    {
        [HttpGet]
        [ActionName("EventList")]
        public ActionResult Index()
        {
            ViewBag.IconNr = 1;
            return View("Index");
        }
    }
}