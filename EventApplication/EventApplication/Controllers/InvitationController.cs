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
            var user = User as MyPrincipal;
            var login = user.UserDetails.Email;
            ViewBag.UserName = user.UserDetails.Email;
            ViewBag.IconNr = 4;

            List<InvitationViewModel> list = new List<InvitationViewModel>();
            using (EventDbContext _db = new EventDbContext())
            {
                var itemUser = _db.Users.FirstOrDefault(u => u.Email.Equals(login));
                ViewBag.Role = itemUser.RoleID;
                var ev = _db.Events.Where(e => e.UserEvents.Any(u => u.User.UserID == itemUser.UserID)).FirstOrDefault();
                var evInvitations = (from li in _db.Invitations
                                     where li.EventID.Equals(ev.EventID)
                                     select li).ToList();

                if(evInvitations.Count != 0)
                {
                    foreach( var i in evInvitations)
                    {
                        var g = _db.Guests.Where(a => a.InvitationID == i.InvitationID).ToList();
                        InvitationViewModel item = new InvitationViewModel()
                        {
                            InvitationID = i.InvitationID,
                            InvitationName = i.InvitationName,
                            Email = i.Email,
                            IsEmailSent = i.IsEmailSent,
                            IsAccountActivated = i.IsAccountActivated,
                            GuestCount = g.Count()
                        };
                        list.Add(item);            
                    }
                    ViewBag.HaveInvitations = "yes";
                }
                else
                    ViewBag.HaveInvitations = "no";
            }
            return View("Invitations", list);
        }

        [HttpGet]
        [ActionName("InvitationDetails")]
        public ActionResult InvitationDetails(int id)
        {

            return View();

        }


        [HttpGet]
        [ActionName("NewInvitation")]
        public ActionResult AddInvitation()
        {
            var user = User as MyPrincipal;
            var login = user.UserDetails.Email;
            ViewBag.UserName = user.UserDetails.Email;
            ViewBag.IconNr = 4;
            InvitationOptionViewModel model = new InvitationOptionViewModel();
            List<SelectListItem> items = new List<SelectListItem>();

            using(EventDbContext _db = new EventDbContext())
            {
                var evUser = _db.Users.FirstOrDefault(u => u.Email.Equals(login));
                var ev = _db.Events.Where(e => e.UserEvents.Any(u => u.UserID == evUser.UserID)).FirstOrDefault();
                if(ev != null)
                {
                    var evOptions = _db.Options.Where(o => o.EventOptions.Any(e => e.EventID == ev.EventID)).ToList();
                    foreach(var o in evOptions)
                    {
                        SelectListItem item = new SelectListItem()
                        {
                            Value = o.OptionID.ToString(),
                            Text = o.OptionName
                        };
                        items.Add(item);
                    }
                }
            }
            model.Options = items;
            return View("NewInvitation", model);
        }

        [HttpPost]
        [ActionName("NewInvitation")]
        public ActionResult AddInvitation(InvitationOptionViewModel _model)
        {
            var user = User as MyPrincipal;
            var login = user.UserDetails.Email;

            if (ModelState.IsValid)
            {
                using (EventDbContext _db = new EventDbContext())
                {
                    var logged = _db.Users.FirstOrDefault(u => u.Email.Equals(login));
                    var loggedEvent = _db.Events.Where(e => e.UserEvents.Any(u => u.UserID == logged.UserID)).FirstOrDefault();
                    var invitationCheck = _db.Invitations.FirstOrDefault(i => i.InvitationName.Equals(_model.InvitationName) && i.EventID.Equals(loggedEvent.EventID) && i.Email.Equals(_model.Email));

                    if(invitationCheck == null)
                    {
                        Invitation added = new Invitation()
                        {
                            InvitationName = _model.InvitationName,
                            Email = _model.Email,
                            IsEmailSent = false,
                            IsAccountActivated = false,
                            EventID = loggedEvent.EventID,
                            RoleID = 3
                        };

                        if (_model.SelectedOptionsId != null)
                        {
                            foreach (var option in _model.SelectedOptionsId)
                            {
                                _db.InvitationOptions.Add(new InvitationOption()
                                {
                                    OptionID = option,
                                    Invitation = added
                                });
                            }
                        }
                    }
                    _db.SaveChanges();
                }
                    
            }
            return RedirectToAction("InvitationsList");
        }

        [HttpGet]
        [ActionName("EditInvitation")]
        [Route("EditAdminUser/{id}")]
        public ActionResult EditInvitation(int id)
        {
            var user = User as MyPrincipal;
            var login = user.UserDetails.Email;
            ViewBag.UserName = user.UserDetails.Email;
            ViewBag.IconNr = 4;
            InvitationOptionViewModel item = null;
            List<SelectListItem> options = new List<SelectListItem>();
            using(EventDbContext _db = new EventDbContext())
            {
                var invOptions = _db.Options.Where(o => o.InvitationOptions.Any(i => i.InvitationID == id)).ToList();
                foreach( var o in invOptions)
                {
                    SelectListItem select = new SelectListItem()
                    {
                        Value = o.OptionID.ToString(),
                        Text = o.OptionName
                    };
                    options.Add(select);

                }
               var inv = _db.Invitations.Where(x => x.InvitationID == id).FirstOrDefault();
                item = new InvitationOptionViewModel()
                {
                    InvitationName = inv.InvitationName,
                    Email = inv.Email,
                    IsEmailSent = inv.IsEmailSent,
                    IsAccountActivated = inv.IsAccountActivated,
                    Options = options
                };
            }
            return View("EditInvitation", item);
        }

        [HttpPost]
        [ActionName("EditInvitation")]
        [Route("EditAdminUser/{id}")]
        public ActionResult EditInvitation(InvitationOptionViewModel _model)
        {
            if (ModelState.IsValid)
            {
                using(EventDbContext _db = new EventDbContext())
                {
                    var edited = _db.Invitations.Where(i => i.InvitationName == _model.InvitationName).FirstOrDefault();
                    if(edited != null)
                    {
                        edited.InvitationName = _model.InvitationName ?? "";
                        edited.Email = _model.Email ?? "";
                        edited.IsEmailSent = _model.IsEmailSent;
                        edited.IsAccountActivated = _model.IsAccountActivated;
                    }

                    var iOptions = _db.Options.Where(o => o.InvitationOptions.Any(a => a.InvitationID == edited.InvitationID)).ToList();
                    var selectedOptions = _model.SelectedOptionsId;
                    if (_model.SelectedOptionsId != null)
                    {
                        foreach (var selected in selectedOptions)
                        {
                            var optionSelected = _db.Options.FirstOrDefault(u => u.OptionID.Equals(selected));
                            if (!(iOptions.Contains(optionSelected)))
                            {
                                _db.InvitationOptions.Add(new InvitationOption()
                                {
                                    OptionID = selected,
                                    Invitation = edited
                                });
                            }
                        }
                        foreach (var option in iOptions)
                        {
                            var currentId = option.OptionID;
                            if (!(selectedOptions.Contains(currentId)))
                            {
                                var optionToRemove = _db.InvitationOptions.FirstOrDefault(u => u.OptionID.Equals(currentId) && u.InvitationID.Equals(edited.InvitationID));
                                _db.InvitationOptions.Remove(optionToRemove);
                            }
                        }
                    }
                    _db.SaveChanges();
                }
            }
            return RedirectToAction("InvitationsList");
        }

        [HttpGet]
        [ActionName("DeleteInvitation")]
        public ActionResult DeleteInvitation(int id)
        {
            using(EventDbContext _db = new EventDbContext())
            {
                var invToRemove = _db.Invitations.Where(i => i.InvitationID == id).FirstOrDefault();
                var invitationGuests = _db.Guests.Where(g => g.InvitationID == id).ToList();

                if(invitationGuests.Count() > 0)
                {
                    foreach(var g in invitationGuests)
                    {
                       
                        var guestOptions = _db.GuestOptions.Where(a => a.GuestID == g.GuestID).ToList();
                        if(guestOptions.Count() > 0)
                        {
                            _db.GuestOptions.RemoveRange(guestOptions);
                            _db.SaveChanges();
                        }
                        _db.Guests.Remove(g);
                    }
                    _db.SaveChanges();
                }
                var invitationOptions = _db.InvitationOptions.Where(x => x.InvitationID == id).ToList();
                if (invitationOptions.Count > 0)
                {
                    foreach(var o in invitationOptions)
                    {
                        _db.InvitationOptions.Remove(o);
                    }
                    _db.SaveChanges();
                }
                _db.Invitations.Remove(invToRemove);
                _db.SaveChanges();
            }
            return RedirectToAction("InvitationsList");

        }
    }
}