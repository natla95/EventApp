namespace EventApplication.Models.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class InvitationOption
    {
        public int ID { get; set; }

        public int InvitationID { get; set; }

        public int OptionID { get; set; }

        public virtual Invitation Invitation { get; set; }

        public virtual Option Option { get; set; }
    }
}
