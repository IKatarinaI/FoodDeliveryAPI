using System;

namespace FoodDelivery.DTOs.ReadObjects
{
    public class ReadRestaurantObject
    {
        public Guid Id { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
