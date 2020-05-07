using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RodBrosEntertainment.Models.Enums;

namespace RodBrosEntertainment.Models
{
    public class Product
    {
        [Required]
        [Key]
        [DisplayName("Product ID")]
        public int ProductId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        [MaxLength(1000)]
        [DisplayName("Image URL")]
        public string ImageUrl { get; set; }

        [Required]
        [Column(TypeName = "decimal")]
        public decimal Price { get; set; }

        [Required]
        [DisplayName("Product Type")]
        public ProductType ProductType { get; set; }

        [Required]
        public ActiveStatus Active { get; set; }

        [Required]
        [DisplayName("Add User ID")]
        public int AddUserId { get; set; }

        [Required]
        [DisplayName("Add Date/Time")]
        public DateTime AddDateTime { get; set; }

        [DisplayName("Change User ID")]
        public int ChangeUserId { get; set; }

        [DisplayName("Change Date/Time")]
        public DateTime ChangeDateTime { get; set; }

        public virtual List<OrderProduct> OrderProducts { get; set; }
    }
}
