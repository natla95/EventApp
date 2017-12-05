using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace EventApplication.Models
{
    public class MyPrincipal : GenericPrincipal
    {
        public MyPrincipal(IIdentity identity, string[] roles)
            : base(identity, roles)
        {
        }
        public AuthUser UserDetails { get; set; }

    }
}