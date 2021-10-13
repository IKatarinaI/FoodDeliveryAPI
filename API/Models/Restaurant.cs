using System;
using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Models
{
    public class Restaurant
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public double Longitude { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public bool IsAvailableForOrdering { get; set; }

        [Required]
        public User Courier { get; set; }

        public Restaurant()
        {
            IsAvailableForOrdering = true;
        }
    }
}
