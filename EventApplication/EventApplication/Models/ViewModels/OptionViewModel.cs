using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventApplication.Models.ViewModels
{
    public class OptionViewModel
    {
        public int OptionID { get; set; }
        public string OptionName { get; set; }
    }
}