using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventApplication.Models;
using EventApplication.Models.DB;
using EventApplication.Models.ViewModels;
using System.Web.Security;

namespace EventApplication.Controllers
{
    
    public class AccountController : Controller
    {
        private EventDbContext _db;
        public AccountController()
        {
            _db = new EventDbContext();
        }   

        [HttpGet]
        [Authorize]
        [ActionName("Home")]
        [Route("")]
        public ActionResult Index()
        {
            var user = User as MyPrincipal;
            var login = user.UserDetails.Email;
            var itemUser = _db.Users.FirstOrDefault(u => u.Email.Equals(login));
            ViewBag.UserName = user.UserDetails.Email;
            ViewBag.IconNumber = 0;
            ViewBag.Role = itemUser.RoleID;
            using(EventDbContext _db = new EventDbContext())
            {
                var ev = _db.Events.Where(e => e.UserEvents.Any(u => u.User.UserID == itemUser.UserID)).FirstOrDefault();
                List<GuestViewModel> guests = new List<GuestViewModel>();

                if (ev != null)
                {
                    var evInvitations = (from li in _db.Invitations
                                         where li.EventID.Equals(ev.EventID)
                                         select li).ToList();

                    foreach (var i in evInvitations)
                    {
                        var currentId = i.InvitationID;
                        var g = _db.Guests.Where(a => a.InvitationID == i.InvitationID).ToList();

                        foreach (var guest in g)
                        {
                            GuestViewModel added = new GuestViewModel() { GuestID = guest.GuestID };
                            guests.Add(added);
                        }
                    }
                    ViewBag.InvitationsCount = evInvitations.Count();
                    ViewBag.GuestsCount = guests.Count();
                }
                else
                {
                    ViewBag.InvitationsCount = 0;
                    ViewBag.GuestsCount = 0;
                }

            }
                
            return View("Index");
        }

        [HttpGet]
        [ActionName("Register")]
        [Route("Register")]
        public ActionResult Register()
        {
            ViewBag.PageNumber = 1;
            return View();
        }

        [HttpPost]
        [ActionName("Register")]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserViewModel _model)
        {
            ViewBag.PageNumber = 1;
            if (ModelState.IsValid){
                using(_db){
                    var item = _db.Users.FirstOrDefault(x => x.Email == _model.Email);
                    if (item == null){
                        User user = new User();
                        user.FirstName = _model.FirstName;
                        user.LastName = _model.LastName;
                        user.Email = _model.Email;
                        user.Password = Security.Hash(_model.Password);
                        user.RoleID = 2;
                        _db.Users.Add(user);
                        _db.SaveChanges();
                        return RedirectToAction("Login");
                    }
                    ModelState.AddModelError("Email", "Użytkownik o podanym loginie już istnieje");
                }  
            }
            return View(_model);
        }

        [HttpGet]
        [ActionName("Login")]
        [Route("Login")]
        public ActionResult Login()
        {
            ViewBag.PageNumber = 2;
            return View();
        }

        [HttpPost]
        [ActionName("Login")]
        public ActionResult Login(LoginViewModel _model)
        {
            ViewBag.PageNumber = 2;
            if (ModelState.IsValid)
            {
                string login = _model.Email;
                string password = Security.Hash(_model.Password);

                User user = null;

                using (_db){
                    user = _db.Users.FirstOrDefault(u => u.Email.Equals(login) && u.Password.Equals(password));
                }
                if(user != null){
                    FormsAuthentication.SetAuthCookie(user.Email, false);
                    var authTicket = new FormsAuthenticationTicket(1, user.Email, DateTime.UtcNow, DateTime.UtcNow.AddMinutes(30), false, "");
                    var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(authTicket));
                    authCookie.Expires = DateTime.UtcNow.AddMinutes(30);
                    Response.SetCookie(authCookie);
                    return RedirectToAction("Home", "Account");
                }
                else {
                    ModelState.AddModelError("Password", "Niepoprawny login lub hasło");
                    return View(_model);
                }
            }
            return View(_model);
        }

        [HttpGet]
        [ActionName("Logout")]
        public ActionResult Loginout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        [HttpGet]
        [ActionName("UserDetails")]
        public ActionResult UserDetails()
        {
            var user = User as MyPrincipal;
            var login = user.UserDetails.Email;
            var itemUser = _db.Users.FirstOrDefault(u => u.Email.Equals(login));
            ViewBag.UserName = user.UserDetails.Email;
            ViewBag.IconNumber = 6;
            UserNewPasswordViewModel item = null;

            using(EventDbContext _db = new EventDbContext())
            {
                var logged = _db.Users.Where(a => a.Email == login).FirstOrDefault();
                ViewBag.Role = logged.RoleID;
                item = new UserNewPasswordViewModel()
                {
                    FirstName = logged.FirstName,
                    LastName = logged.LastName,
                    Email = logged.Email
                };

            }
            return View("UserDetails", item);
        }

        [HttpPost]
        [ActionName("UserDetails")]
        public ActionResult UserDetails(UserNewPasswordViewModel _model)
        {
            if (ModelState.IsValid)
            {
                var item = _db.Users.Where(x => x.Email == _model.Email).FirstOrDefault();
                if (item != null)
                {
                    item.FirstName = _model.FirstName ?? "";
                    item.LastName = _model.LastName ?? "";
                    item.Email = _model.Email ?? "";
                    if (!string.IsNullOrEmpty(_model.NewPassword))
                    {
                        item.Password = Security.Hash(_model.NewPassword);
                    }
                    _db.SaveChanges();
                    return RedirectToAction("UserDetails");
                }
                else
                {
                    return RedirectToAction("UserDetails");
                }
            }
            return RedirectToAction("UserDetails");
        }
    }
}