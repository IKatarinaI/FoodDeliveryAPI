using FoodDelivery.Helpers;
using FoodDelivery.Models;
using FoodDelivery.Repositories.ReadRepositories.Interfaces;
using FoodDelivery.Repositories.WriteRepositories.Interfaces;
using FoodDelivery.Services.Interfaces;
using FoodDelivery.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodDelivery.Services
{
    public class OrderService : IOrderService
    {
        struct DistanceBetweenRestaurantAndUser
        {
            public Restaurant Restaurant { get; }
            public double Distance { get; }
            public DistanceBetweenRestaurantAndUser(Restaurant restaurant, double distance) 
            { 
                Restaurant = restaurant; 
                Distance = distance;
            }
        }

        private readonly IReadOrderRepository _readRepository;
        private readonly IWriteOrderRepository _writeRepository;
        private readonly IReadRestaurantRepository _readRestaurantRepository;
        private readonly IWriteRestaurantRepository _writeRestaurantRepository;
        private readonly IReadUserRepository _readUserRepository;
        private static object lockObj = new object();

        public OrderService(IReadOrderRepository readRepository, IWriteOrderRepository writeRepository,
                            IReadRestaurantRepository readRestaurantRepository, IWriteRestaurantRepository writeRestaurantRepository,
                            IReadUserRepository readUserRepository)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
            _readRestaurantRepository = readRestaurantRepository;
            _writeRestaurantRepository = writeRestaurantRepository;
            _readUserRepository = readUserRepository;
        }

        public void AddOrder(Order order, Guid userId)
        {
            var user = _readUserRepository.GetUser(userId);

            var restaurant = FindClosestRestaurantForOrder(user);
            
            if (restaurant is null)
                throw new AppException("Order is canceled because there are no available restaurants at the moment.");

            lock (lockObj)
            {
                restaurant.IsAvailableForOrdering = false;
                _writeRestaurantRepository.UpdateRestaurant(restaurant);

                // uncomment next line for real-life simulation of delivery
                //Thread.Sleep(Constants.DeliveryTime * 60000);
            }

            restaurant.IsAvailableForOrdering = true;
            _writeRestaurantRepository.UpdateRestaurant(restaurant);

            order.Restaurant = restaurant;
            order.Status = Enums.OrdersStatus.Finished.ToString();
            order.User = user;
            _writeRepository.AddOrder(order);
        }

        private Restaurant FindClosestRestaurantForOrder(User user)
        {
            var restaurants = _readRestaurantRepository.GetRestaurants();
            
            // eliminates all currently unavailable restaurants
            var availableRestaurants = restaurants.Where(restaurant => restaurant.IsAvailableForOrdering);
            if (!availableRestaurants.Any())
                return null;

            // represents a List of Restaurant object and calculated distance between user and restaurant
            var calculatedDistances = new List<DistanceBetweenRestaurantAndUser>();

            foreach(var res in availableRestaurants)
            {
                // distance is calculated without altitude
                // if altitude is needed, it just needs to be added in the equation
                var distance = Math.Sqrt(Math.Pow(res.Latitude - user.Latitude, 2)
                                    + Math.Pow(res.Longitude - user.Longitude, 2));

                calculatedDistances.Add(new DistanceBetweenRestaurantAndUser(res, distance));
            }

            var restaurant = calculatedDistances.OrderBy(x => x.Distance).FirstOrDefault();

            return restaurant.Restaurant;
        }

        public Order GetOrder(Guid orderId)
        {
            var order = _readRepository.GetOrder(orderId);

            if (order is null)
                throw new KeyNotFoundException("Order not found");

            return order;
        }

        public IEnumerable<Order> GetOrders()
        {
            return _readRepository.GetOrders();
        }

        public bool Save()
        {
            return _writeRepository.Save();
        }
    }
}
