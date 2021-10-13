using System.ComponentModel.DataAnnotations;
using static FoodDelivery.Shared.Enums;

namespace FoodDelivery.DTOs.CreateObjects
{
    public class CreateUserObject
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [EnumDataType(typeof(UsersRoles))]
        public string Role { get; set; }
        [Required]
        public double Longitude { get; set; }
        [Required]
        public double Latitude { get; set; }
    }
}
