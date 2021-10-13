using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static FoodDelivery.Shared.Enums;

namespace FoodDelivery.Models
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }

        [EnumDataType(typeof(OrdersStatus))]
        public string Status { get; set; }

        [Required]
        public Restaurant Restaurant { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public IEnumerable<Food> OrderedFood { get; set; }
    }
}
