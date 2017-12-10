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
    public class InvitationController : Controller
    {
        [HttpGet]
        [ActionName("InvitationsList")]
        public ActionResult Index()
        {
            ViewBag.IconNr = 4;
            var user = User as MyPrincipal;
            ViewBag.UserName = user.UserDetails.Email;


            return View();
        }
    }
}