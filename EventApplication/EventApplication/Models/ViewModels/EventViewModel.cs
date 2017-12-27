﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EventApplication.Models.ViewModels
{
    public class EventViewModel
    {

        public int EventID { get; set; }
        
        [StringLength(100)]
        [Display(Name ="Nazwa")]
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
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",ApplyFormatInEditMode = true)]
        public DateTime? EventDate { get; set; }

        [Display(Name = "Godzina uroczystości")]
        [DataType(DataType.Time)]
        public TimeSpan? EventTime { get; set; }

        [StringLength(150)]
        [Display(Name = "Miejsce ślubu")]
        [RegularExpression(@"^[A-Z-a-z0-9ąóęćłśńżźĄÓĘĆŁŚŃŻŹ\s\-\.\/\,]{5,150}$", ErrorMessage = "Niepoprawny adres")]
        public string ChurchAddress { get; set; }

        [StringLength(150)]
        [Display(Name = "Miejsce wesela")]
        [RegularExpression(@"^[A-Z-a-z0-9ąóęćłśńżźĄÓĘĆŁŚŃŻŹ\s\-\.\/\,]{5,150}$", ErrorMessage = "Niepoprawny adres")]
        public string WeddingAddress { get; set; }

    }
}