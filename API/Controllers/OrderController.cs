using AutoMapper;
using FoodDelivery.DTOs.CreateObjects;
using FoodDelivery.DTOs.ReadObjects;
using FoodDelivery.Helpers;
using FoodDelivery.Models;
using FoodDelivery.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace FoodDelivery.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService ??
                throw new ArgumentNullException(nameof(orderService));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Adds new <see cref="Order"/> with provided parameters.
        /// Only logged in user can create an order.
        /// </summary>
        /// <param><see cref="CreateOrderObject"/> object.</param>
        /// <returns><see cref="ReadOrderObject"/> object which has been added to database.</returns>
        [Authorize]
        [HttpPost]
        public IActionResult CreateOrder(CreateOrderObject createOrderObject)
        {
            var orderObject = _mapper.Map<Order>(createOrderObject);
            _orderService.AddOrder(orderObject, User.Id);
            _orderService.Save();

            return Ok(_mapper.Map<ReadOrderObject>(orderObject));
        }
    }
}
