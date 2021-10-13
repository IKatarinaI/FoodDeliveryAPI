using FoodDelivery.Models;

namespace FoodDelivery.Repositories.WriteRepositories.Interfaces
{
    public interface IWriteOrderRepository
    {
        void AddOrder(Order order);
        bool Save();
    }
}
