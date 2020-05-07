using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RodBrosEntertainment.Models.Enums;

namespace RodBrosEntertainment.Models
{
    public class OrderProduct
    {
        [Required]
        [Key]
        [DisplayName("Order Product ID")]
        public int OrderProductId { get; set; }

        [Required]
        [ForeignKey("Order")]
        [DisplayName("Order ID")]
        public int OrderId { get; set; }

        [Required]
        [ForeignKey("Product")]
        [DisplayName("Product ID")]
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }
    }
}
