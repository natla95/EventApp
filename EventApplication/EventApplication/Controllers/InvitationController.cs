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
    }
}