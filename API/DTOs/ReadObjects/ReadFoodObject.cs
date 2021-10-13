using System;

namespace FoodDelivery.DTOs.ReadObjects
{
    public class ReadFoodObject
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Picture { get; set; }
        public int Rating { get; set; }
    }
}
