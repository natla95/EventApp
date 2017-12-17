namespace EventApplication.Models.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Event
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Event()
        {
            EventOptions = new HashSet<EventOption>();
            Invitations = new HashSet<Invitation>();
            UserEvents = new HashSet<UserEvent>();
        }

        public int EventID { get; set; }

        [Required]
        [StringLength(100)]
        public string EventName { get; set; }

        [Required]
        [StringLength(100)]
        public string OrganizerName1 { get; set; }

        [Required]
        [StringLength(100)]
        public string OrganizerName2 { get; set; }

        [Column(TypeName = "date")]
        public DateTime? EventDate { get; set; }

        public TimeSpan? EventTime { get; set; }

        [StringLength(150)]
        public string ChurchAddress { get; set; }

        [StringLength(150)]
        public string WeddingAddress { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EventOption> EventOptions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Invitation> Invitations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserEvent> UserEvents { get; set; }
    }
}
