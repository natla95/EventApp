namespace EventApplication.Models.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EventOption
    {
        public int ID { get; set; }

        public int EventID { get; set; }

        public int OptionID { get; set; }

        public virtual Event Event { get; set; }

        public virtual Option Option { get; set; }
    }
}
