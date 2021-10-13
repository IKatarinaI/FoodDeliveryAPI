using FoodDelivery.DBAccess;
using FoodDelivery.Models;
using FoodDelivery.Repositories.WriteRepositories.Interfaces;
using System;

namespace FoodDelivery.Repositories.WriteRepositories
{
    public class WriteUserRepository : IWriteUserRepository
    {
        private readonly FoodDeliveryContext _context;
        public WriteUserRepository(FoodDeliveryContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddUser(User user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            user.Id = Guid.NewGuid();

            _context.Users.Add(user);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
