using FoodDelivery.Models;
using System;

namespace FoodDelivery.Repositories.WriteRepositories.Interfaces
{
    public interface IWriteFoodRepository
    {
        void AddFood(Food food);
        void DeleteFood(Food food);
        bool Save();
        void UpdateFood(Food food);
    }
}
