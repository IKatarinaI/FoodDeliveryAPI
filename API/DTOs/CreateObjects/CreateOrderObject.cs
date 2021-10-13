using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.DTOs.CreateObjects
{
    public class CreateOrderObject
    {
        [Required]
        public IEnumerable<CreateFoodObject> OrderedFood { get; set; }
    }
}
