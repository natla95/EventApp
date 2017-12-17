﻿using EventApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace EventApplication
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            try
            {
                var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                if (authCookie != null)
                {
                    FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                    if (authTicket != null && !authTicket.Expired)
                    {
                        var usr = new MyPrincipal(new FormsIdentity(authTicket), new string[] { });
                        usr.UserDetails = new AuthUser() { Email = authTicket.Name };
                        HttpContext.Current.User = usr;

                        TimeSpan span = authTicket.Expiration.Subtract(DateTime.UtcNow);
                        if (span.Minutes < 10)
                        {
                            var authTicket1 = new FormsAuthenticationTicket(1, authTicket.Name, DateTime.UtcNow, DateTime.UtcNow.AddMinutes(30), false, "");
                            var authCookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(authTicket1));
                            authCookie.Expires = DateTime.UtcNow.AddMinutes(30);
                            Response.SetCookie(authCookie);
                        }
                    }
                }
            }
            catch (Exception h)
            {
                HttpContext.Current.Response.Redirect("/Account/Login");
            }
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            if(Response.StatusCode == 401)
            {
                Response.ClearContent();
                Response.Redirect("/Account/Login");
            }
        }
    }
}
