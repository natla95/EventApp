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
                        InvitationViewModel item = new InvitationViewModel()
                        {
                            InvitationName = i.InvitationName,
                            Email = i.Email,
                            IsEmailSent = i.IsEmailSent,
                            IsAccountActivated = i.IsAccountActivated
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
                            ConfirmLink = Security.Hash(_model.Email),
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
    }
}