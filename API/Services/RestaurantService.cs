using FoodDelivery.Helpers;
using FoodDelivery.Models;
using FoodDelivery.Repositories.ReadRepositories.Interfaces;
using FoodDelivery.Repositories.WriteRepositories.Interfaces;
using FoodDelivery.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace FoodDelivery.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IReadRestaurantRepository _readRepository;
        private readonly IWriteRestaurantRepository _writeRepository;

        public RestaurantService(IReadRestaurantRepository readRepository, IWriteRestaurantRepository writeRepository)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
        }

        public void AddRestaurant(Restaurant restaurant)
        {
            _writeRepository.AddRestaurant(restaurant);
        }

        public Restaurant GetRestaurant(Guid restaurantId)
        {
            var restaurant = _readRepository.GetRestaurant(restaurantId);

            if (restaurant is null)
                throw new KeyNotFoundException("Restaurant not found");

            return restaurant;
        }

        public IEnumerable<Restaurant> GetRestaurants()
        {
            return _readRepository.GetRestaurants();
        }

        public void DeleteRestaurant(Guid restaurantId)
        {
            var restaurant = _readRepository.GetRestaurant(restaurantId);

            if (restaurant is null)
                throw new KeyNotFoundException("Restaurant not found");

            if (!restaurant.IsAvailableForOrdering)
                throw new AppException("Restaurant cannot be deleted while it is delivering order.");

            _writeRepository.DeleteRestaurant(restaurant);
        }

        public bool Save()
        {
            return _writeRepository.Save();
        }
    }
}
