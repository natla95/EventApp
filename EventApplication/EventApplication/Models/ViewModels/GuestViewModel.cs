using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventApplication.Models.ViewModels
{
    public class GuestViewModel
    {
        [StringLength(50)]
        [Display(Name = "Imię")]
        [Required(ErrorMessage = "Imię jest wymagane", AllowEmptyStrings = false)]
        [RegularExpression(@"^[A-Z-a-ząóęćłśńżźĄÓĘĆŁŚŃŻŹ\s\-]{2,50}$", ErrorMessage = "Niepoprawne imię")]
        public string FirstName { get; set; }


        [StringLength(50)]
        [Display(Name = "Nazwisko")]
        [Required(ErrorMessage = "Nazwisko jest wymagane", AllowEmptyStrings = false)]
        [RegularExpression(@"^[A-Z-a-ząóęćłśńżźĄÓĘĆŁŚŃŻŹ\s\-]{2,50}$", ErrorMessage = "Niepoprawne nazwisko")]
        public string LastName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Wiek")]
        public string Age { get; set; }
    }
}