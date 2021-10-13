using FoodDelivery.Models;
using System;
using System.Collections.Generic;

namespace FoodDelivery.Repositories.ReadRepositories.Interfaces
{
    public interface IReadOrderRepository
    {
        IEnumerable<Order> GetOrders();
        Order GetOrder(Guid orderId);
    }
}
