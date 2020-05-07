using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RodBrosEntertainment.Models.Enums;

namespace RodBrosEntertainment.Models
{
    public class Email
    {
        [Required]
        [Display(Name = "Your name")]
        public string FromName { get; set; }

        [Required]
        [Display(Name = "Your email")]
        [EmailAddress]
        public string FromEmail { get; set; }

        [Required]
        public string Message { get; set; }
    }
}
