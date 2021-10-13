using FoodDelivery.Models;
using System;
using System.Collections.Generic;

namespace FoodDelivery.Services.Interfaces
{
    public interface IOrderService
    {
        IEnumerable<Order> GetOrders();
        Order GetOrder(Guid orderId);
        void AddOrder(Order order, Guid userId);
        bool Save();
    }
}
