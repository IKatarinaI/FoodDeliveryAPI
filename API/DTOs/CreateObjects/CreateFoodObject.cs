using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.DTOs.CreateObjects
{
    public class CreateFoodObject
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        public string Picture { get; set; }
    }
}
