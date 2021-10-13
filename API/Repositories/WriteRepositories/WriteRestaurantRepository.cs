using FoodDelivery.DBAccess;
using FoodDelivery.Models;
using FoodDelivery.Repositories.WriteRepositories.Interfaces;
using System;

namespace FoodDelivery.Repositories.WriteRepositories
{
    public class WriteRestaurantRepository : IWriteRestaurantRepository
    {
        private readonly FoodDeliveryContext _context;
        public WriteRestaurantRepository(FoodDeliveryContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddRestaurant(Restaurant restaurant)
        {
            if (restaurant is null)
            {
                throw new ArgumentNullException(nameof(restaurant));
            }

            restaurant.Id = Guid.NewGuid();

            _context.Restaurants.Add(restaurant);
        }

        public void DeleteRestaurant(Restaurant restaurant)
        {
            if (restaurant is null)
            {
                throw new ArgumentNullException(nameof(restaurant));
            }

            _context.Restaurants.Remove(restaurant);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateRestaurant(Restaurant restaurant)
        {
            _context.Restaurants.Update(restaurant);
        }
    }
}
