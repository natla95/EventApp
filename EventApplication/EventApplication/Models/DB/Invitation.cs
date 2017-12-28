namespace EventApplication.Models.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Invitation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Invitation()
        {
            Guests = new HashSet<Guest>();
            InvitationOptions = new HashSet<InvitationOption>();
        }

        public int InvitationID { get; set; }

        [Required]
        [StringLength(100)]
        public string InvitationName { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        public string ConfirmLink { get; set; }

        public bool IsEmailSent { get; set; }

        public bool IsAccountActivated { get; set; }

        public int EventID { get; set; }

        public int RoleID { get; set; }

        public virtual Event Event { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Guest> Guests { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InvitationOption> InvitationOptions { get; set; }

        public virtual Role Role { get; set; }
    }
}
