using System;
using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.DTOs.UpdateObjects
{
    public class UpdateFoodRatingObject
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public int Rating { get; set; }
    }
}
