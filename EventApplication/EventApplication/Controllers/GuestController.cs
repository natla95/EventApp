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
        [Route("")]
        public ActionResult Index()
        {
            var user = User as MyPrincipal;
            var login = user.UserDetails.Email;
            ViewBag.IconNr = 3;
            ViewBag.UserName = user.UserDetails.Email;
            ViewBag.AreGuests = "no";

            List<GuestViewModel> guests = new List<GuestViewModel>();

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
                        var iGuests = _db.Guests.Where(g => g.InvitationID == i.InvitationID).ToList();
                        var iName = i.InvitationName;
                        if(iGuests.Count() > 0)
                        {
                            ViewBag.AreGuests = "yes";
                            foreach (var g in iGuests)
                            {
                                GuestViewModel guest = new GuestViewModel()
                                {
                                    GuestID = g.GuestID,
                                    FirstName = g.FirstName,
                                    LastName = g.LastName,
                                    Age = g.Age,
                                    InvitationName = iName
                                };
                                guests.Add(guest);
                            } 
                        }
                    }
                }
                else
                {
                    ViewBag.AreGuests = "no";
                }
            }

            return View("GuestsList", guests);
        }

        [HttpGet]
        [ActionName("AddGuest")]
        [Route("AddGuest")]
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
        [Route("AddGuest")]
        public ActionResult AddGuest(GuestViewModel _model, int id)
        {
            if (ModelState.IsValid)
            {
                using(EventDbContext _db = new EventDbContext())
                {
                    var inv = _db.Invitations.Where(i => i.InvitationID == id).FirstOrDefault();
                    if(inv != null)
                    {
                        var checkInDb = _db.Guests.Where(g => g.FirstName == _model.FirstName && g.LastName == _model.LastName && g.InvitationID == id).FirstOrDefault();
                        if(checkInDb == null)
                        {
                            Guest added = new Guest()
                            {
                                FirstName = _model.FirstName,
                                LastName = _model.LastName,
                                Age = _model.Age,
                                InvitationID = id
                            };
                            _db.Guests.Add(added);
                            _db.SaveChanges();
                        }
                    }
                }
            }
            return RedirectToAction("InvitationDetails", "Invitation", new { id = id });
        }

        [HttpGet]
        [ActionName("EditGuest")]
        [Route("EditGuest/{id}")]
        public ActionResult EditGuest(int id)
        {
            var user = User as MyPrincipal;
            var login = user.UserDetails.Email;
            ViewBag.UserName = user.UserDetails.Email;
            ViewBag.IconNr = 3;

            GuestViewModel item = null;
            ViewBag.AgeList = new List<SelectListItem>
                        {
                            new SelectListItem { Text = "Dorosły", Value = "Dorosły" },
                            new SelectListItem { Text = "Dziecko",  Value = "Dziecko" },
                        };
            using (EventDbContext _db = new EventDbContext())
            {
                var itemUser = _db.Users.FirstOrDefault(u => u.Email.Equals(login));
                ViewBag.Role = itemUser.RoleID;
                var edited = _db.Guests.Where(g => g.GuestID == id).FirstOrDefault();
                if(edited != null)
                {
                    item = new GuestViewModel()
                    {
                        FirstName = edited.FirstName,
                        LastName = edited.LastName,
                        Age = edited.Age,
                        GuestID = id
                    };
                }
            }
            return View("EditGuest",item);
        }

        [HttpPost]
        [ActionName("EditGuest")]
        [Route("EditGuest/{id}")]
        public ActionResult EditGuest(GuestViewModel _model)
        {
            if (ModelState.IsValid)
            {
                using(EventDbContext _db = new EventDbContext())
                {
                    var edited = _db.Guests.Where(g => g.GuestID == _model.GuestID).FirstOrDefault();
                    if(edited != null)
                    {
                        edited.FirstName = _model.FirstName ?? "";
                        edited.LastName = _model.LastName ?? "";
                        edited.Age = _model.Age;
                    }
                    _db.SaveChanges();
                }
            }
            return RedirectToAction("GuestsList");
        }

        [HttpGet]
        [ActionName("DeleteGuest")]
        [Route("DeleteGuest/{id}")]
        public ActionResult DeleteGuest(int id)
        {
            using(EventDbContext _db = new EventDbContext())
            {
                var guestToDelete = _db.Guests.Where(x => x.GuestID == id).FirstOrDefault();

                var guestOptions = _db.GuestOptions.Where(x => x.GuestID == id).ToList();
                if (guestOptions.Count() > 0)
                {
                    _db.GuestOptions.RemoveRange(guestOptions);
                    _db.SaveChanges();
                }
                _db.Guests.Remove(guestToDelete);
                _db.SaveChanges();
            }
            return RedirectToAction("GuestsList");
        }

    }
}