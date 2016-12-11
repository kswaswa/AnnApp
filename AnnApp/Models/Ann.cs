using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AnnApp.Models
{
    public class Ann
    {
        public int ID { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)] //Client side validation
        public string Title { get; set; }

        [Required]
        [StringLength(1000, MinimumLength = 5)]
        public string Content { get; set; }

        [DataType(DataType.Date)] //Data Annotation
        [DisplayFormat(DataFormatString = "{0:dd/MM}", ApplyFormatInEditMode = true)]
        public DateTime PostDate { get; set; }

        public virtual ApplicationUser User { get; set; }

        // Which users have clicked on "details" next to announcement.
        public List<ApplicationUser> ViewedAnn { get; set; }
    }
}