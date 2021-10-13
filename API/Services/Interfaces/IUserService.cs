using FoodDelivery.Models;
using System;
using System.Collections.Generic;

namespace FoodDelivery.Services.Interfaces
{
    public interface IUserService
    {
        IEnumerable<User> GetUsers();
        User GetUser(Guid userId);
        void AddUser(User user);
        User Authenticate(User user);
        bool IsUsernameUnique(string username);
        bool Save();
    }
}
