using System;
using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Models
{
    public class Food
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }

        public string Picture { get; set; }

        public int Rating { get; set; }

        public Food()
        {
            Rating = 0;
        }
    }
}
