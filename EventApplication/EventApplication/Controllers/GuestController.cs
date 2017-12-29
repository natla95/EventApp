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
            var user = User as MyPrincipal;
            var login = user.UserDetails.Email;
            ViewBag.IconNr = 3;
            ViewBag.UserName = user.UserDetails.Email;

            List<OptionViewModel> options = new List<OptionViewModel>();
            List<GuestViewModel> guests = new List<GuestViewModel>();
            GuestListViewModel model = new GuestListViewModel();

            using (EventDbContext _db = new EventDbContext())
            {
                var itemUser = _db.Users.FirstOrDefault(u => u.Email.Equals(login));
                ViewBag.Role = itemUser.RoleID;
                var ev = _db.Events.Where(e => e.UserEvents.Any(u => u.User.UserID == itemUser.UserID)).FirstOrDefault();
                var evIvitations = (from li in _db.Invitations
                                    where li.EventID.Equals(ev.EventID)
                                    select li).ToList();
                if(evIvitations.Count != 0)
                {
                    foreach (var i in evIvitations)
                    {
                        var currentId = i.InvitationID;

                        var invitationsOptions = _db.Options.Where(o => o.InvitationOptions.Any(a => a.InvitationID == currentId)).ToList();
                        foreach(var o in invitationsOptions)
                        {
                            OptionViewModel option = new OptionViewModel()
                            {
                                OptionID = o.OptionID,
                                OptionName = o.OptionName
                            };
                            options.Add(option);
                        }

                        var invitationGuests = (from li in _db.Guests
                                                where li.InvitationID.Equals(currentId)
                                                select li).ToList();

                        if(invitationGuests.Count > 0)
                        {
                            ViewBag.AreGuests = "yes";
                            foreach (var g in invitationGuests)
                            {
                                GuestViewModel guest = new GuestViewModel()
                                {
                                    GuestID = g.GuestID,
                                    InvitationID = currentId,
                                    FirstName = g.FirstName,
                                    LastName = g.LastName,
                                    Age = g.Age,
                                    
                                };
                                guests.Add(guest);
                            }
                        }
                        else
                            ViewBag.AreGuests = "no";
                    }
                }    
            }
            model.guestsList = guests;
            model.optionsList = options;

            return View("GuestsList", model);
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
            return View();
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