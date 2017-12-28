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

            List<Event> events = new List<Event>();
            List<Option> options = new List<Option>();
            EventListViewModel model = new EventListViewModel();

            using (EventDbContext _db = new EventDbContext())
            {
                var itemUser = _db.Users.FirstOrDefault(u => u.Email.Equals(login));
                ViewBag.Role = itemUser.RoleID;
                if (itemUser != null)
                {
                    var loggedID = itemUser.UserID;
                    events = _db.Events.Where(e => e.UserEvents.Any(u => u.User.UserID == loggedID)).ToList();
                    foreach (var e in events)
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

                    foreach (var eventss in model.EventList)
                    {
                        var currentId = eventss.EventID;
                        options = _db.Options.Where(e => e.EventOptions.Any(o => o.Event.EventID == currentId)).ToList();
                        foreach (var o in options)
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
            return View("EventList", model);
        }

        [HttpGet]
        [ActionName("AddEvent")]
        public ActionResult AddEvent()
        {
            var user = User as MyPrincipal;
            ViewBag.UserName = user.UserDetails.Email;
            ViewBag.IconNr = 1;
            ViewBag.HaveEvent = "no";
            EventOptionViewModel model = new EventOptionViewModel();
            model.Options = OptionsQuery();
            return View(model);
        }

        private static List<SelectListItem> OptionsQuery()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            using (EventDbContext _db = new EventDbContext())
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
            var login = user.UserDetails.Email;

            if (ModelState.IsValid)
            {
                using (EventDbContext _db = new EventDbContext())
                {
                    var logged = _db.Users.FirstOrDefault(u => u.Email.Equals(login));
                    var loggedId = logged.UserID;
                    logged.RoleID = 5;
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
            var login = user.UserDetails.Email;
            ViewBag.UserName = user.UserDetails.Email;
            ViewBag.IconNr = 1;

            EventOptionViewModel item = null;
            using (EventDbContext _db = new EventDbContext())
            {
                var itemUser = _db.Users.FirstOrDefault(u => u.Email.Equals(login));
                ViewBag.Role = itemUser.RoleID;
                var ev = _db.Events.Where(e => e.UserEvents.Any(u => u.User.UserID == itemUser.UserID)).FirstOrDefault();
                item = new EventOptionViewModel()
                {
                    EventID = ev.EventID,
                    EventName = ev.EventName,
                    EventDate = ev.EventDate,
                    EventTime = ev.EventTime,
                    OrganizerName1 = ev.OrganizerName1,
                    OrganizerName2 = ev.OrganizerName2,
                    WeddingAddress = ev.WeddingAddress,
                    ChurchAddress = ev.ChurchAddress
                };

                if (ev != null)
                    ViewBag.HaveEvent = "no";
                else
                    ViewBag.HaveEvent = "yes";
            }
            return View(item);
        }

        [HttpPost]
        [ActionName("EditEvent")]
        public ActionResult EditEvent(EventOptionViewModel _model)
        {
            var user = User as MyPrincipal;
            var login = user.UserDetails.Email;

            if (ModelState.IsValid)
            {
                using (EventDbContext _db = new EventDbContext())
                {
                    var itemUser = _db.Users.FirstOrDefault(u => u.Email.Equals(login));
                    var ev = _db.Events.Where(e => e.UserEvents.Any(u => u.User.UserID == itemUser.UserID)).FirstOrDefault();
                    if (ev != null)
                    {
                        ev.EventName = _model.EventName ?? "";
                        ev.EventDate = _model.EventDate;
                        ev.EventTime = _model.EventTime;
                        ev.OrganizerName1 = _model.OrganizerName1 ?? "";
                        ev.OrganizerName2 = _model.OrganizerName2 ?? "";
                        ev.WeddingAddress = _model.WeddingAddress;
                        ev.ChurchAddress = _model.ChurchAddress;
                    }
                    _db.SaveChanges();
                }
            }

            return RedirectToAction("EventList");
        }
        [HttpGet]
        [ActionName("DeleteEvent")]
        public ActionResult DeleteEvent()
        {
            var user = User as MyPrincipal;
            var login = user.UserDetails.Email;

            using (EventDbContext _db = new EventDbContext())
            {
                var itemUser = _db.Users.FirstOrDefault(u => u.Email.Equals(login));
                var ev = _db.Events.Where(e => e.UserEvents.Any(u => u.User.UserID == itemUser.UserID)).FirstOrDefault();
                var evOptions = _db.Options.Where(o => o.EventOptions.Any(e => e.EventID == ev.EventID)).ToList();
                var evInvitations = (from li in _db.Invitations
                                     where li.EventID.Equals(ev.EventID)
                                     select li).ToList();

                var eventOptions = _db.EventOptions.Where(x => x.EventID == ev.EventID).ToList();

                if (eventOptions.Count > 0)
                {
                    _db.EventOptions.RemoveRange(eventOptions);
                    _db.SaveChanges();
                }

                if (evInvitations.Count != 0)
                {
                    _db.Invitations.RemoveRange(evInvitations);
                    _db.SaveChanges();
                }

                var userEvents = _db.UserEvents.Where(x => x.EventID == ev.EventID).ToList();
                if (userEvents.Count > 0)
                {
                    _db.UserEvents.RemoveRange(userEvents);
                    _db.SaveChanges();
                }

                _db.Events.Remove(ev);
                itemUser.RoleID = 2;
                _db.SaveChanges();
            }

            return RedirectToAction("EventList");
        }

        [HttpGet]
        [ActionName("EventInformation")]
        public ActionResult GetEventInformation()
        {
            var user = User as MyPrincipal;
            var login = user.UserDetails.Email;
            ViewBag.UserName = user.UserDetails.Email;
            ViewBag.IconNr = 2;

            EventViewModel item = null;
            using (EventDbContext _db = new EventDbContext())
            {
                var itemUser = _db.Users.FirstOrDefault(u => u.Email.Equals(login));
                ViewBag.Role = itemUser.RoleID;
                if (itemUser != null)
                {
                    var loggedID = itemUser.UserID;
                    var ev = _db.Events.Where(e => e.UserEvents.Any(u => u.User.UserID == itemUser.UserID)).FirstOrDefault();
                    if(ev != null)
                    {
                        item = new EventViewModel()
                        {
                            EventID = ev.EventID,
                            EventName = ev.EventName,
                            EventDate = ev.EventDate,
                            EventTime = ev.EventTime,
                            OrganizerName1 = ev.OrganizerName1,
                            OrganizerName2 = ev.OrganizerName2,
                            WeddingAddress = ev.WeddingAddress,
                            ChurchAddress = ev.ChurchAddress
                        };
                    }
               
                }
            }
            return View("GetEventInformation",item);
        }
    }
}
