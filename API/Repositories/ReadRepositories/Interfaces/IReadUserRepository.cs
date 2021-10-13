using FoodDelivery.Models;
using System;
using System.Collections.Generic;

namespace FoodDelivery.Repositories.ReadRepositories.Interfaces
{
    public interface IReadUserRepository
    {
        IEnumerable<User> GetUsers();
        User GetUser(Guid userId);
        User GetUserByUsername(string username);
        bool IsUsernameUnique(string username);
    }
}
