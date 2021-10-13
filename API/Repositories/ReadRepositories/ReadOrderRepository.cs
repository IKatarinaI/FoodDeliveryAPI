using FoodDelivery.DBAccess;
using FoodDelivery.Models;
using FoodDelivery.Repositories.ReadRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodDelivery.Repositories.ReadRepositories
{
    public class ReadOrderRepository : IReadOrderRepository
    {
        private readonly FoodDeliveryContext _context;
        public ReadOrderRepository(FoodDeliveryContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Order GetOrder(Guid orderId)
        {
            return _context.Orders.FirstOrDefault(x => x.Id == orderId);
        }

        public IEnumerable<Order> GetOrders()
        {
            return _context.Orders.ToList<Order>();
        }
    }
}
