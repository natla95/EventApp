using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventApplication.Controllers
{
    public class InvitationController : Controller
    {
        [HttpGet]
        [ActionName("InvitationsList")]
        public ActionResult Index()
        {
            ViewBag.IconNumber = 4;
            return View();
        }
    }
}