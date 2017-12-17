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
    public class EventController : Controller
    {
        [HttpGet]
        [ActionName("EventList")]
        public ActionResult Index()
        {
            var user = User as MyPrincipal;
            var login = user.UserDetails.Email;
            ViewBag.UserName = user.UserDetails.Email;
            ViewBag.IconNr = 1;

            User userLogged = new User();
            using (EventDbContext _db = new EventDbContext())
            {
                var itemUser = _db.Users.FirstOrDefault(u => u.Email.Equals(login));
                if (itemUser != null)
                {
                    var loggedID = itemUser.UserID;
                    var eventList = _db.Events.Where(e => e.UserEvents.Any(u => u.User.UserID == loggedID)).ToList();
                }
            }
            return View("EventList");    
        }

        [HttpGet]
        [ActionName("AddEvent")]
        public ActionResult AddEvent()
        {
            var user = User as MyPrincipal;
            ViewBag.UserName = user.UserDetails.Email;
            ViewBag.IconNr = 1;

            EventOptionViewModel model = new EventOptionViewModel();
            model.Options = OptionsQuery();
            return View(model);
        }

        private static List<SelectListItem> OptionsQuery()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            using(EventDbContext _db = new EventDbContext())
                {
                    items = (from li in _db.Options.AsNoTracking()
                             select new SelectListItem
                             {
                                 Text = li.OptionName,
                                 Value = li.OptionID.ToString()
                             }).ToList();
                }
            return items;
        }

    [HttpPost]
        [ActionName("AddEvent")]
        public ActionResult AddEvent(EventOptionViewModel _model)
        {
            var user = User as MyPrincipal;
            ViewBag.UserName = user.UserDetails.Email;
            var login = user.UserDetails.Email;
            if (ModelState.IsValid)
            {
                using (EventDbContext _db = new EventDbContext())
                {
                    var logged = _db.Users.FirstOrDefault(u => u.Email.Equals(login));
                    var loggedId = logged.UserID;
                    var item = _db.Events.FirstOrDefault(e => e.EventName.Equals(_model.EventName) && e.OrganizerName1.Equals(_model.OrganizerName1) && e.OrganizerName2.Equals(_model.OrganizerName2));
                    if (item == null)
                    {
                            Event added = new Event()
                        {
                            EventName = _model.EventName,
                            OrganizerName1 = _model.OrganizerName1,
                            OrganizerName2 = _model.OrganizerName2,
                            EventDate = _model.EventDate,
                            EventTime = _model.EventTime,
                            ChurchAddress = _model.ChurchAddress,
                            WeddingAddress = _model.WeddingAddress
                        };

                        UserEvent userEvent = new UserEvent()
                        {
                            UserID = loggedId,
                            Event = added
                        };

                        _db.UserEvents.Add(userEvent);
                        _db.SaveChanges();
                        return RedirectToAction("EventList");                    
                    } 
                }
            }
            return View();
        }

        [HttpGet]
        [ActionName("EditEvent")]
        public ActionResult EditEvent()
        {
            var user = User as MyPrincipal;
            ViewBag.UserName = user.UserDetails.Email;
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