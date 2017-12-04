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
            ViewBag.IconNr = 1;
            List<EventViewModel> list;
            using (_db)
            {
                list = (from li in _db.Events.AsNoTracking()
                        select new EventViewModel()
                        {
                            EventName = li.EventName,
                            OrganizerName1 = li.OrganizerName1,
                            OrganizerName2 = li.OrganizerName2,
                            EventDate = li.EventDate,
                            EventTime = li.EventTime,
                            ChurchAddress = li.ChurchAddress,
                            WeddingAddress = li.WeddingAddress           
                        }).ToList();

                ViewBag.Organizators = 
                    (from li in _db.Users.AsNoTracking()
                    select new UserViewModel()
                    {
                        FirstName = li.FirstName,
                        LastName = li.LastName,
                        Email = li.Email
                    }).ToList();
            }
            return View("EventList",list);    
        }

        [HttpGet]
        [ActionName("AddEvent")]
        public ActionResult AddEvent()
        {
            ViewBag.IconNr = 1;
            using (_db)
            {
                ViewBag.Options = (from li in _db.Options.AsNoTracking()
                                   select new OptionViewModel()
                                   {
                                      OptionName = li.OptionName
                                   }).ToList();
            }
            return View();
        }

        [HttpPost]
        [ActionName("AddEvent")]
        public ActionResult AddEvent(EventViewModel _model)
        {

            return View();
        }

        [HttpGet]
        [ActionName("EditEvent")]
        public ActionResult EditEvent()
        {
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