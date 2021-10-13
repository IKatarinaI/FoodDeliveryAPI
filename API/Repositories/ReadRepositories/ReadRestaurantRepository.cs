using FoodDelivery.DBAccess;
using FoodDelivery.Models;
using FoodDelivery.Repositories.ReadRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodDelivery.Repositories.ReadRepositories
{
    public class ReadRestaurantRepository : IReadRestaurantRepository
    {
        private readonly FoodDeliveryContext _context;
        public ReadRestaurantRepository(FoodDeliveryContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Restaurant GetRestaurant(Guid restaurantId)
        {
            return _context.Restaurants.FirstOrDefault(x => x.Id == restaurantId);
        }

        public IEnumerable<Restaurant> GetRestaurants()
        {
            return _context.Restaurants.ToList<Restaurant>();
        }
    }
}
