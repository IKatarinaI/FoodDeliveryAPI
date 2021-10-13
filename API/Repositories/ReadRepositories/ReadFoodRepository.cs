using FoodDelivery.DBAccess;
using FoodDelivery.Models;
using FoodDelivery.Repositories.ReadRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodDelivery.Repositories.ReadRepositories
{
    public class ReadFoodRepository : IReadFoodRepository
    {
        private readonly FoodDeliveryContext _context;
        public ReadFoodRepository(FoodDeliveryContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Food GetFood(Guid foodId)
        {
            return _context.Foods.FirstOrDefault(x => x.Id == foodId);
        }

        public IEnumerable<Food> GetFoods()
        {
            return _context.Foods.ToList<Food>();
        }
    }
}
