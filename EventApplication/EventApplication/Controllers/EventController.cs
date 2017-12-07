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
    //[Authorize]
    public class EventController : Controller
    {
        private EventDbContext _db;
        public EventController()
        {
            _db = new EventDbContext();
        }

        [HttpGet]
        [ActionName("EventList")]
        public ActionResult Index()
        {
            var user = User as MyPrincipal;
            var login = user.UserDetails.Email;
            ViewBag.UserName = user.UserDetails.Email;
            ViewBag.IconNr = 1;

            EventOptionViewModel model = new EventOptionViewModel();
            User userLogged = new User();
            using (_db)
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
            using (_db)
            {
                ViewBag.Options = (from li in _db.Options.AsNoTracking()
                                   select new OptionViewModel()
                                   {
                                        OptionID = li.OptionID,
                                        OptionName = li.OptionName
                                   }).ToList();
            }
            return View();
        }

        [HttpPost]
        [ActionName("AddEvent")]
        public ActionResult AddEvent(EventOptionViewModel _model)
        {
            if (ModelState.IsValid)
            {
                using (_db)
                {
                    var item = _db.Events.FirstOrDefault(e => e.EventName.Equals(_model.EventName) && e.OrganizerName1.Equals(_model.OrganizerName1) && e.OrganizerName2.Equals(_model.OrganizerName2));
                    if (item != null)
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
                        _db.Events.Add(added);
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