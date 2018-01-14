using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventApplication.Models.ViewModels
{
    public class InvitationOptionViewModel
    {
        public int InvitationId { get; set; }

        [StringLength(100)]
        [Display(Name = "Nazwa")]
        [Required(ErrorMessage = "Nazwa jest wymagana", AllowEmptyStrings = false)]
        [RegularExpression(@"^[A-Z-a-z0-9ąóęćłśńżźĄÓĘĆŁŚŃŻŹ\s\-\ \.\,_\!]{5,100}$", ErrorMessage = "Niepoprawna nazwa")]
        public string InvitationName { get; set; }

        [StringLength(100)]
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email jest wymagany", AllowEmptyStrings = false)]
        [RegularExpression(@"^[A-Za-z0-9_\-\.]{1,}@[a-z0-9_\.]{1,}\.[a-z]{2,5}$", ErrorMessage = "Niepoprawny email")]
        public string Email { get; set; }

        [Display(Name = "Email wysłany")]
        public bool IsEmailSent { get; set; }

        [Display(Name = "Konto aktywowane")]
        public bool IsAccountActivated { get; set; }

        public int OptionID { get; set; }
        public string OptionName { get; set; }
        public List<SelectListItem> Options { set; get; }
        public IEnumerable<int> SelectedOptionsId { set; get; }

    }
}