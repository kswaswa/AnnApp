using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AnnApp.Models
{
    public class Ann
    {
        public int ID { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9_\\s]+")]
        [StringLength(30, MinimumLength = 3)] //Client side validation
        public string Title { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9_\\s]+")]
        [StringLength(1000, MinimumLength = 5)]
        public string Content { get; set; }

        [DataType(DataType.Date)] //Data Annotation
        [DisplayFormat(DataFormatString = "{0:dd/MM}", ApplyFormatInEditMode = true)]
        public DateTime PostDate { get; set; }

        // Which user has posted or seen the ann
        public virtual ApplicationUser User { get; set; }

        // Which users have clicked on "details" next to announcement.
        public List<ApplicationUser> ViewedAnn { get; set; }
    }
}