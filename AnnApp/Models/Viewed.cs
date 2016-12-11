using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnnApp.Models
{
    // can store who has viewed which ann in the database
    public class Viewed
    {
        public int ID { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual Ann Ann { get; set; }
    }
}