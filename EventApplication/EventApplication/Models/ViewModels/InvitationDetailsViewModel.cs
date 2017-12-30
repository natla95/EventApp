using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventApplication.Models.ViewModels
{
    public class InvitationDetailsViewModel
    {
        public InvitationViewModel invitation = new InvitationViewModel();
        public List<OptionViewModel> invitationOptions = new List<OptionViewModel>();
        public List<GuestViewModel> invitationGuests = new List<GuestViewModel>();
    }
}