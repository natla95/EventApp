using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventApplication.Models.ViewModels
{
    public class GuestListViewModel
    {
        public List<GuestViewModel> guestsList = new List<GuestViewModel>();
        public List<OptionViewModel> optionsList = new List<OptionViewModel>();
    }
}