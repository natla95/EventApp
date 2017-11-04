using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventApplication.Models.ViewModels
{
    public class InvitationViewModel
    {
        public int InvitationID { get; set; }

        [StringLength(100)]
        [Display(Name = "Nazwa zaproszenia")]
        [Required(ErrorMessage = "Nazwa jest wymagana", AllowEmptyStrings = false)]
        [RegularExpression(@"^[A-Z-a-z0-9ąóęćłśńżźĄÓĘĆŁŚŃŻŹ\s\-\_\.\,]{5,100}$", ErrorMessage = "Niepoprawna nazwa")]
        public string InvitationName { get; set; }

        [StringLength(100)]
        [Display(Name = "Email do przesłania potwierdzenia")]
        [Required(ErrorMessage = "Email jest wymagany", AllowEmptyStrings = false)]
        [RegularExpression(@"^[A-Za-z0-9_\-\.]{1,}@[a-z0-9_\.]{1,}\.[a-z]{2,5}$", ErrorMessage = "Niepoprawny email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        public string ConfirmLink { get; set; }

        [Display(Name = "Czy email przesłany?")]
        public bool IsEmailSent { get; set; }

        [Display(Name = "Czy gość aktywował konto?")]
        public bool IsAccountActivated { get; set; }

        public int EventID { get; set; }

        public int RoleID { get; set; }

    }
}