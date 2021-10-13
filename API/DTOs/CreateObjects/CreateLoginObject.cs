using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.DTOs.CreateObjects
{
    public class CreateLoginObject
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
