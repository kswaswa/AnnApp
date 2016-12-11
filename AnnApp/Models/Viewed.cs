using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnnApp.Models
{
    public class Viewed
    {
        public int ID { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Ann Ann { get; set; }
    }
}