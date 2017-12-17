using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Mvc;

namespace EventApplication.Models.ViewModels
{
    public class EventOptionViewModel
    {
        public int EventID { get; set; }

        [StringLength(100)]
        [Display(Name = "Nazwa")]
        [Required(ErrorMessage = "Nazwa jest wymagana", AllowEmptyStrings = false)]
        [RegularExpression(@"^[A-Z-a-z0-9ąóęćłśńżźĄÓĘĆŁŚŃŻŹ\s\-\ \.\,_\!]{5,100}$", ErrorMessage = "Niepoprawna nazwa uroczystości")]
        public string EventName { get; set; }

        [StringLength(100)]
        [Display(Name = "Nazwa organizatora nr1")]
        [Required(ErrorMessage = "Nazwa organizatora jest wymagana", AllowEmptyStrings = false)]
        [RegularExpression(@"^[A-Z-a-ząóęćłśńżźĄÓĘĆŁŚŃŻŹ\s\-]{2,100}$", ErrorMessage = "Niepoprawna nazwa")]
        public string OrganizerName1 { get; set; }

        [StringLength(100)]
        [Display(Name = "Nazwa organizatora nr2")]
        [Required(ErrorMessage = "Nazwa organizatora jest wymagana", AllowEmptyStrings = false)]
        [RegularExpression(@"^[A-Z-a-ząóęćłśńżźĄÓĘĆŁŚŃŻŹ\s\-]{2,100}$", ErrorMessage = "Niepoprawna nazwa")]
        public string OrganizerName2 { get; set; }

        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [Display(Name = "Data uroczystości")]
        [Required(ErrorMessage = "Data jest wymagana", AllowEmptyStrings = false)]
        public DateTime? EventDate { get; set; }

        [Display(Name = "Godzina uroczystości")]
        [DataType(DataType.Time)]
        [Required(ErrorMessage = "Godzina jest wymagana", AllowEmptyStrings = false)]
        public TimeSpan? EventTime { get; set; }

        [StringLength(150)]
        [Display(Name = "Miejsce ślubu")]
        [RegularExpression(@"^[A-Z-a-z0-9ąóęćłśńżźĄÓĘĆŁŚŃŻŹ\s\-\.\/\,]{5,150}$", ErrorMessage = "Niepoprawny adres")]
        public string ChurchAddress { get; set; }

        [StringLength(150)]
        [Display(Name = "Miejsce wesela")]
        [RegularExpression(@"^[A-Z-a-z0-9ąóęćłśńżźĄÓĘĆŁŚŃŻŹ\s\-\.\/\,]{5,150}$", ErrorMessage = "Niepoprawny adres")]
        public string WeddingAddress { get; set; }

        public int OptionID { get; set; }
        public string OptionName { get; set; }
        public List<SelectListItem> Options { set; get; }
        public IEnumerable<int> SelectedOptionsId { set; get; }
    }
}