using FoodDelivery.DBAccess;
using FoodDelivery.Models;
using FoodDelivery.Repositories.ReadRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodDelivery.Repositories.ReadRepositories
{
    public class ReadUserRepository : IReadUserRepository
    {
        private readonly FoodDeliveryContext _context;
        public ReadUserRepository(FoodDeliveryContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public User GetUser(Guid userId)
        {
            return _context.Users.FirstOrDefault(x => x.Id == userId);
        }

        public User GetUserByUsername(string username)
        {
            return _context.Users.FirstOrDefault(x => x.Username == username);
        }

        public IEnumerable<User> GetUsers()
        {
            return _context.Users.ToList<User>();
        }

        public bool IsUsernameUnique(string username)
        {
            return _context.Users.Any(x => x.Username == username);
        }
    }
}
