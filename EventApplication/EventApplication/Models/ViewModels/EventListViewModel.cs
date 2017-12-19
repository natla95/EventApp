using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventApplication.Models.ViewModels
{
    public class EventListViewModel
    {
        public List<EventViewModel> EventList = new List<EventViewModel>();
        public List<OptionViewModel> OptionsList = new List<OptionViewModel>();

    }
}