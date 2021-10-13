using System;

namespace FoodDelivery.DTOs.ReadObjects
{
    public class ReadUserObject
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
