namespace EventApplication.Models.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserEvent")]
    public partial class UserEvent
    {
        public int ID { get; set; }

        public int UserID { get; set; }

        public int EventID { get; set; }

        public virtual Event Event { get; set; }

        public virtual User User { get; set; }
    }
}
