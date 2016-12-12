using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AnnApp.Models
{
    // can store each comment in a database connected to 
    // which ann it is under
    public class Comment
    {
        public int ID { get; set; }

        [RegularExpression("^[a-zA-Z0-9_\\s]+")]
        [Display(Name = "Comment")]
        public string comment { get; set; }

        public virtual Ann Ann { get; set; }
    }
}