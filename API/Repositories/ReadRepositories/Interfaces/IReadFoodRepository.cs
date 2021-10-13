using FoodDelivery.Models;
using System;
using System.Collections.Generic;

namespace FoodDelivery.Repositories.ReadRepositories.Interfaces
{
    public interface IReadFoodRepository
    {
        IEnumerable<Food> GetFoods();
        Food GetFood(Guid foodId);
    }
}
