using FoodDelivery.Models;

namespace FoodDelivery.Repositories.WriteRepositories.Interfaces
{
    public interface IWriteRestaurantRepository
    {
        void AddRestaurant(Restaurant restaurant);
        void DeleteRestaurant(Restaurant restaurant);
        bool Save();
        void UpdateRestaurant(Restaurant restaurant);
    }
}
