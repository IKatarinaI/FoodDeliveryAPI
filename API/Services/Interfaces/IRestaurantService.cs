using FoodDelivery.Models;
using System;
using System.Collections.Generic;

namespace FoodDelivery.Services.Interfaces
{
    public interface IRestaurantService
    {
        IEnumerable<Restaurant> GetRestaurants();
        Restaurant GetRestaurant(Guid restaurantId);
        void AddRestaurant(Restaurant restaurant);
        void DeleteRestaurant(Guid restaurantId);
        bool Save();
    }
}
