using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RodBrosEntertainment.Models.Enums;

namespace RodBrosEntertainment.ViewModels
{
    //Products
    public class ProductListViewModel
    {
        [Required]
        [Key]
        [DisplayName("Product ID")]
        public int ProductId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "decimal")]
        public decimal Price { get; set; }

        [Required]
        [DisplayName("Product Type")]
        public ProductType ProductType { get; set; }

        [Required]
        public ActiveStatus Active { get; set; }
    }

    public class ProductCreateViewModel
    {
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
    }

    public class ProductEditViewModel
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
    }

    //Accounts
    public class AccountListViewModel
    {
        [Required]
        [Key]
        [DisplayName("User ID")]
        public int UserId { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DisplayName("User Type")]
        public UserType UserType { get; set; }

        [Required]
        [DisplayName("Active Status")]
        public ActiveStatus Active { get; set; }
    }

    public class AccountEditViewModel
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
    }

    public class AccountCreateViewModel
    {
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
    }

    public class AccountDeleteViewModel
    {
        [Required]
        [Key]
        [DisplayName("User ID")]
        public int UserId { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DisplayName("User Type")]
        public UserType UserType { get; set; }
    }

    //Orders

    public class OrderListViewModel
    {
        [Required]
        [Key]
        [DisplayName("Order ID")]
        public int OrderId { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DisplayName("Order Date")]
        public DateTime OrderDate { get; set; }

        [Required]
        [Column(TypeName = "decimal")]
        public decimal Subtotal { get; set; }

        [Required]
        [DisplayName("Order Status")]
        public OrderStatus StatusId { get; set; }
    }

    public class OrderProdListViewModel
    {
        [Required]
        [Key]
        [DisplayName("Order Product ID")]
        public int OrderProductId { get; set; }

        [DisplayName("Product ID")]
        public int ProductId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "decimal")]
        public decimal Price { get; set; }

        [Required]
        [DisplayName("Product Type")]
        public ProductType ProductType { get; set; }

        [Required]
        public int Quantity { get; set; }

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
    }
}
