using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RodBrosEntertainment.Models.Enums;

namespace RodBrosEntertainment.Models
{
    public class User
    {
        [Required]
        [Key]
        [DisplayName("User ID")]
        public int UserId { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; } // TODO should ideally be encrypted and not a string

        [Required]
        public string Name { get; set; }

        [Required]
        [DisplayName("User Type")]
        public UserType UserType { get; set; }

        [Required]
        [MaxLength(50)]
        [DisplayName("Street Line 1")]
        public string Street1 { get; set; }

        [MaxLength(50)]
        [DisplayName("Street Line 2")]
        public string Street2 { get; set; }

        [Required]
        [MaxLength(50)]
        public string City { get; set; }

        [Required]
        [MaxLength(2)]
        public string State { get; set; }

        [Required]
        [MaxLength(25)]
        public string Country { get; set; }

        [Required]
        [DisplayName("Active Status")]
        public ActiveStatus Active { get; set; }

        public virtual List<Order> Orders { get; set; }
    }
}
