using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventApplication.Models.ViewModels
{
    public class UserNewPasswordViewModel
    {

        [StringLength(50)]
        [Display(Name = "Imię")]
        [RegularExpression(@"^[A-Z-a-ząóęćłśńżźĄÓĘĆŁŚŃŻŹ]{2,50}$", ErrorMessage = "Niepoprawne imię")]
        public string FirstName { get; set; }

        [StringLength(50)]
        [Display(Name = "Nazwisko")]
        [RegularExpression(@"^[A-Z-a-ząóęćłśńżźĄÓĘĆŁŚŃŻŹ\-]{2,50}$", ErrorMessage = "Niepoprawne nazwisko")]
        public string LastName { get; set; }

        [StringLength(100)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Stare hasło")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Nowe hasło")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^[A-Z-a-z0-9_\@]{8,16}$", ErrorMessage = "Nowe hasło niepoprawne")]
        public string NewPassword { get; set; }

        [Display(Name = "Powtorz hasło")]
        [DataType(DataType.Password)]
        [Compare("NewPassword")]
        [RegularExpression(@"^[A-Z-a-z0-9_\@]{8,16}$", ErrorMessage = "Hasło niepoprawne")]
        public string ComfirmPassword { get; set; }
    }
}