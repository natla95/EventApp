using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventApplication.Models.ViewModels
{
    public class LoginViewModel
    {
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
    }
}