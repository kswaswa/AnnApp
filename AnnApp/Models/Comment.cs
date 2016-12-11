using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AnnApp.Models
{
    public class Comment
    {
        //matches that of ann?
        public int ID { get; set; }

        [Display(Name = "Comment")]
        public string comment { get; set; }

        public virtual Ann Ann { get; set; }
    }
}