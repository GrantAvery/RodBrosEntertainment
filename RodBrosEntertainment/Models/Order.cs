using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RodBrosEntertainment.Models.Enums;

namespace RodBrosEntertainment.Models
{
    public class Order
    {
        [Required]
        [Key]
        [DisplayName("Order ID")]
        public int OrderId { get; set; }

        [Required]
        [ForeignKey("User")]
        [DisplayName("User ID")]
        public int UserId { get; set; }

        [Required]
        [DisplayName("Order Status")]
        public OrderStatus StatusId { get; set; }

        [DisplayName("Order Date")]
        public DateTime OrderDate { get; set; }

        [Column(TypeName = "decimal")]
        public decimal Subtotal { get; set; }

        [Column(TypeName = "decimal")]
        public decimal Tax { get; set; }

        [Column(TypeName = "decimal")]
        [DisplayName("Shipping Cost")]
        public decimal ShippingCost { get; set; }

        [Column(TypeName = "decimal")]
        [DisplayName("Shipping Miles")]
        public decimal ShippingMiles { get; set; }

        [MaxLength(50)]
        [DisplayName("Street Line 1")]
        public string Street1 { get; set; }

        [MaxLength(50)]
        [DisplayName("Street Line 2")]
        public string Street2 { get; set; }

        [MaxLength(50)]
        public string City { get; set; }

        [MaxLength(2)]
        public string State { get; set; }

        [MaxLength(25)]
        public string Country { get; set; }

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

        public virtual User User { get; set; }

        public virtual List<OrderProduct> OrderProducts { get; set; }
    }
}
