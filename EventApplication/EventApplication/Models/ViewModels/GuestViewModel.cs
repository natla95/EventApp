using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventApplication.Models.ViewModels
{
    public class GuestViewModel
    {
        public int GuestID { get; set; }

        [StringLength(50)]
        [Display(Name = "Imię")]
        [Required(ErrorMessage = "Imię jest wymagane", AllowEmptyStrings = false)]
        [RegularExpression(@"^[A-Z-a-ząóęćłśńżźĄÓĘĆŁŚŃŻŹ]{2,50}$", ErrorMessage = "Niepoprawne imię")]
        public string FirstName { get; set; }

        [StringLength(50)]
        [Display(Name = "Nazwisko")]
        [Required(ErrorMessage = "Nazwisko jest wymagane", AllowEmptyStrings = false)]
        [RegularExpression(@"^[A-Z-a-ząóęćłśńżźĄÓĘĆŁŚŃŻŹ\-]{2,50}$", ErrorMessage = "Niepoprawne nazwisko")]
        public string LastName { get; set; }

        [StringLength(50)]
        [Display(Name = "Wiek")]
        [Required(ErrorMessage = "Nazwisko jest wymagane", AllowEmptyStrings = false)]
        [RegularExpression(@"^[A-Z-a-ząóęćłśńżźĄÓĘĆŁŚŃŻŹ]{2,50}$", ErrorMessage = "Niepoprawna wartość")]
        public string Age { get; set; }

        public int InvitationID { get; set; }
    }
}