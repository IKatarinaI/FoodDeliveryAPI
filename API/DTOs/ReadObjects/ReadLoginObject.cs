using System;

namespace FoodDelivery.DTOs.ReadObjects
{
    public class ReadLoginObject
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string JwtToken { get; set; }
    }
}
