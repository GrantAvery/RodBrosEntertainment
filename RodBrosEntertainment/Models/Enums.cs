using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RodBrosEntertainment.Models
{
    public class Enums
    {
        public enum OrderStatus
        {
            [Display(Name = "Cart")]
            Cart = 0,

            [Display(Name = "Created")]
            Created = 1,

            [Display(Name = "In Transit")]
            InTransit = 2,

            [Display(Name = "Delivered")]
            Delivered = 3
        }

        public enum ProductType // TODO should these types be more specific?
        {
            Online = 0,
            Physical = 1
        }

        public enum ActiveStatus
        {
            Inactive = 0,
            Active = 1
        }

        public enum UserType
        {
            Customer = 0,
            Admin = 1,
        }
    }
}
