using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.DTOs.CreateObjects
{
    public class CreateRestaurantObject
    {
        [Required]
        public double Longitude { get; set; }
        [Required]

        public double Latitude { get; set; }
    }
}
