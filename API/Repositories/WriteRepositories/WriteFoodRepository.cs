using FoodDelivery.DBAccess;
using FoodDelivery.Models;
using FoodDelivery.Repositories.WriteRepositories.Interfaces;
using System;

namespace FoodDelivery.Repositories.WriteRepositories
{
    public class WriteFoodRepository : IWriteFoodRepository
    {
        private readonly FoodDeliveryContext _context;
        public WriteFoodRepository(FoodDeliveryContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddFood(Food food)
        {
            if (food is null)
            {
                throw new ArgumentNullException(nameof(food));
            }

            food.Id = Guid.NewGuid();

            _context.Foods.Add(food);
        }

        public void DeleteFood(Food food)
        {
            if (food is null)
            {
                throw new ArgumentNullException(nameof(food));
            }

            _context.Foods.Remove(food);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateFood(Food food)
        {
            _context.Foods.Update(food);
        }
    }
}
