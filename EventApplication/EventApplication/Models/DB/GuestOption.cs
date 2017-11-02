namespace EventApplication.Models.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class GuestOption
    {
        public int ID { get; set; }

        public int GuestID { get; set; }

        public int OptionID { get; set; }

        public virtual Guest Guest { get; set; }

        public virtual Option Option { get; set; }
    }
}
