using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EventApplication.Models.ViewModels
{
    public class UserViewModel
    {
        public int UserId { get; set; }


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


        [StringLength(100)]
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email jest wymagany", AllowEmptyStrings = false)]
        [RegularExpression(@"^[A-Za-z0-9_\-\.]{1,}@[a-z0-9_\.]{1,}\.[a-z]{2,5}$", ErrorMessage = "Niepoprawny email")]
        public string Email { get; set; }


        [StringLength(50)]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        [Required(ErrorMessage = "Hasło jest wymagane", AllowEmptyStrings = false)]
        [RegularExpression(@"^[A-Z-a-z0-9_\@\.\!]{8,16}$", ErrorMessage = "Niepoprawne hasło (co najmniej 8 znaków)")]
        public string Password { get; set; }

        public int RoleID { get; set; }

    }
}