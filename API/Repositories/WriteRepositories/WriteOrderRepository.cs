using FoodDelivery.DBAccess;
using FoodDelivery.Models;
using FoodDelivery.Repositories.WriteRepositories.Interfaces;
using System;

namespace FoodDelivery.Repositories.WriteRepositories
{
    public class WriteOrderRepository : IWriteOrderRepository
    {
        private readonly FoodDeliveryContext _context;
        public WriteOrderRepository(FoodDeliveryContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddOrder(Order order)
        {
            if (order is null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            order.Id = Guid.NewGuid();

            _context.Orders.Add(order);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
