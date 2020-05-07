using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RodBrosEntertainment.Models;
using static RodBrosEntertainment.Models.Enums;

namespace RodBrosEntertainment.ViewModels
{
    public class OrderFullViewModel
    {
        [Key]
        public int OrderId { get; set; }

        public int UserId { get; set; }

        public OrderStatus StatusId { get; set; }

        public DateTime OrderDate { get; set; }

        [Column(TypeName = "decimal")]
        public decimal Subtotal { get; set; }

        [Column(TypeName = "decimal")]
        public decimal Tax { get; set; }

        [Column(TypeName = "decimal")]
        public decimal ShippingCost { get; set; }

        [Column(TypeName = "decimal")]
        public decimal ShippingMiles { get; set; }

        [MaxLength(50)]
        public string Street1 { get; set; }

        [MaxLength(50)]
        public string Street2 { get; set; }

        [MaxLength(50)]
        public string City { get; set; }

        [MaxLength(2)]
        public string State { get; set; }

        [MaxLength(25)]
        public string Country { get; set; }

        public int AddUserId { get; set; }

        public DateTime AddDateTime { get; set; }

        public int ChangeUserId { get; set; }

        public DateTime ChangeDateTime { get; set; }

        public List<OrderProduct> OrderProducts { get; set; }
    }

    public class OrderCheckOutViewModel
    {
        [Required]
        [Key]
        public int OrderId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [Column(TypeName = "decimal")]
        public decimal Subtotal { get; set; }

        [Required]
        [Column(TypeName = "decimal")]
        public decimal Tax { get; set; }

        [Required]
        [Column(TypeName = "decimal")]
        public decimal ShippingCost { get; set; }

        [Required]
        [Column(TypeName = "decimal")]
        public decimal ShippingMiles { get; set; }

        [Required]
        [MaxLength(50)]
        public string Street1 { get; set; }

        [MaxLength(50)]
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

        public List<OrderProduct> OrderProducts { get; set; }
    }
}
