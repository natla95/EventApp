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
        [ActionName("EventsList")]
        public ActionResult Index()
        {
            return View();
        }
    }
}