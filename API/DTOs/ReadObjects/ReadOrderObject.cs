using System;
using System.Collections.Generic;

namespace FoodDelivery.DTOs.ReadObjects
{
    public class ReadOrderObject
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
        public ReadRestaurantObject Restaurant { get; set; }
        public IEnumerable<ReadFoodObject> OrderedFood { get; set; }
    }
}
