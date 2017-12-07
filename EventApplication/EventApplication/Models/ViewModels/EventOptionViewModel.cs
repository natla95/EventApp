using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

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
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data uroczystości")]
        [RegularExpression(@"^\d{2}\.\d{2}.\d{4}$", ErrorMessage = "Niepoprawny format godziny")]
        public DateTime? EventDate { get; set; }

        [Display(Name = "Godzina uroczystości")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        [RegularExpression(@"^\d{2}\:\d{2}$", ErrorMessage = "Niepoprawny format daty (wymagane: dd.mm.rrrr)")]
        public DateTime? EventTime { get; set; }

        [StringLength(150)]
        [Display(Name = "Miejsce ślubu")]
        [RegularExpression(@"^[A-Z-a-z0-9ąóęćłśńżźĄÓĘĆŁŚŃŻŹ\s\-\.\/\,]{5,150}$", ErrorMessage = "Niepoprawny adres")]
        public string ChurchAddress { get; set; }

        [StringLength(150)]
        [Display(Name = "Miejsce wesela")]
        [RegularExpression(@"^[A-Z-a-z0-9ąóęćłśńżźĄÓĘĆŁŚŃŻŹ\s\-\.\/\,]{5,150}$", ErrorMessage = "Niepoprawny adres")]
        public string WeddingAddress { get; set; }

        public List<OptionViewModel> EventOptions { get; set; }
        public bool IsSelected { get; set; }
    }
}