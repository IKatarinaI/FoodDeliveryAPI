using FoodDelivery.Models;

namespace FoodDelivery.Repositories.WriteRepositories.Interfaces
{
    public interface IWriteUserRepository
    {
        void AddUser(User user);
        bool Save();
    }
}
