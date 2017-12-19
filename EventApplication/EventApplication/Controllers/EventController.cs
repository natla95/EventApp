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
            List<Event> events = new List<Event>();
            List<Option> options = new List<Option>();
            EventListViewModel model = new EventListViewModel();

            using (EventDbContext _db = new EventDbContext())
            {
                var itemUser = _db.Users.FirstOrDefault(u => u.Email.Equals(login));
                if (itemUser != null)
                {
                    var loggedID = itemUser.UserID;

                    events = _db.Events.Where(e => e.UserEvents.Any(u => u.User.UserID == loggedID)).ToList();
                    foreach(var e in events)
                    {
                        EventViewModel ev = new EventViewModel()
                        {
                            EventID = e.EventID,
                            EventName = e.EventName,
                            EventDate = e.EventDate,
                            EventTime = e.EventTime,
                            OrganizerName1 = e.OrganizerName1,
                            OrganizerName2 = e.OrganizerName2,
                            WeddingAddress = e.WeddingAddress,
                            ChurchAddress = e.ChurchAddress
                        };
                        model.EventList.Add(ev);
                    }

                   foreach(var eventss in model.EventList)
                    {
                        var currentId = eventss.EventID;
                        options = _db.Options.Where(e => e.EventOptions.Any(o => o.Event.EventID == currentId)).ToList();
                        foreach(var o in options)
                        {
                            OptionViewModel ov = new OptionViewModel()
                            {
                                OptionID = o.OptionID,
                                OptionName = o.OptionName
                            };
                            model.OptionsList.Add(ov);
                        }
                    }

                    if (events.Count == 0)
                        ViewBag.HaveEvent = "no";
                    else
                        ViewBag.HaveEvent = "yes";  
                }              
            }
            return View("EventList",model);    
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
                        if (_model.SelectedOptionsId != null)
                        {
                            foreach (var option in _model.SelectedOptionsId)
                            {
                                _db.EventOptions.Add(new EventOption()
                                {
                                    OptionID = option,
                                    Event = added
                                });
                            }
                        }
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

        public ActionResult GetEventInformation()
        {
            var user = User as MyPrincipal;
            ViewBag.UserName = user.UserDetails.Email;
            ViewBag.IconNr = 2;

            var login = user.UserDetails.Email;
            

            return View();
        }
    }
}