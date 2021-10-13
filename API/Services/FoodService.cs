using FoodDelivery.Models;
using FoodDelivery.Repositories.ReadRepositories.Interfaces;
using FoodDelivery.Repositories.WriteRepositories.Interfaces;
using FoodDelivery.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace FoodDelivery.Services
{
    public class FoodService : IFoodService
    {
        private readonly IReadFoodRepository _readRepository;
        private readonly IWriteFoodRepository _writeRepository;

        public FoodService(IReadFoodRepository readRepository, IWriteFoodRepository writeRepository)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
        }

        public void AddFood(Food food)
        {
            _writeRepository.AddFood(food);
        }

        public Food GetFood(Guid foodId)
        {
            var food = _readRepository.GetFood(foodId);

            if (food is null)
                throw new KeyNotFoundException("Food not found");

            return food;
        }

        public IEnumerable<Food> GetFoods()
        {
            return _readRepository.GetFoods();
        }

        public void DeleteFood(Guid foodId)
        {
            var food = _readRepository.GetFood(foodId);

            if (food is null)
                throw new KeyNotFoundException("Food not found");

            _writeRepository.DeleteFood(food);
        }

        public bool Save()
        {
            return _writeRepository.Save();
        }

        public void UpdateFood(Food food)
        {
            var tempFood = _readRepository.GetFood(food.Id);

            if (tempFood is null)
                throw new KeyNotFoundException("Food not found");

            tempFood.Rating = food.Rating;

            _writeRepository.UpdateFood(tempFood);
        }
    }
}
