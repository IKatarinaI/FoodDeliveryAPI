using FoodDelivery.Models;
using System;
using System.Collections.Generic;

namespace FoodDelivery.Services.Interfaces
{
    public interface IFoodService
    {
        IEnumerable<Food> GetFoods();
        Food GetFood(Guid foodId);
        void AddFood(Food food);
        void DeleteFood(Guid foodId);
        bool Save();
        void UpdateFood(Food food);
    }
}
