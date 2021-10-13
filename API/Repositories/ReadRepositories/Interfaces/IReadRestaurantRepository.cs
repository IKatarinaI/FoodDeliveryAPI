using FoodDelivery.Models;
using System;
using System.Collections.Generic;

namespace FoodDelivery.Repositories.ReadRepositories.Interfaces
{
    public interface IReadRestaurantRepository
    {
        IEnumerable<Restaurant> GetRestaurants();
        Restaurant GetRestaurant(Guid restaurantId);
    }
}
