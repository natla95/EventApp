using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventApplication.Models.ViewModels
{
    public class InvitationOptionListViewModel
    {
        List<InvitationViewModel> InvitationList = new List<InvitationViewModel>();
        List<OptionViewModel> OptionList = new List<OptionViewModel>();
    }
}