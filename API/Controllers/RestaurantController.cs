using AutoMapper;
using FoodDelivery.DTOs.CreateObjects;
using FoodDelivery.DTOs.ReadObjects;
using FoodDelivery.Helpers;
using FoodDelivery.Models;
using FoodDelivery.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using static FoodDelivery.Shared.Enums;
using AuthorizeAttribute = FoodDelivery.Helpers.AuthorizeAttribute;

namespace FoodDelivery.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RestaurantController : BaseController
    {
        private readonly IRestaurantService _restaurantService;
        private readonly IMapper _mapper;

        public RestaurantController(IRestaurantService restaurantService, IMapper mapper)
        {
            _restaurantService = restaurantService ??
                throw new ArgumentNullException(nameof(restaurantService));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Gets <see cref="Restaurant"/> with provided identifier.
        /// </summary>
        /// <param name="id"><see cref="Restaurant"/> identifier.</param>
        /// <returns><see cref="ReadRestaurantObject"/> object satisfying the provided identifier.</returns>
        [HttpGet("{id}")]
        public IActionResult GetRestaurant(Guid id)
        {
            if (id == Guid.Empty)
                throw new AppException("Id is empty");

            var restaurant = _restaurantService.GetRestaurant(id);

            return Ok(_mapper.Map<ReadRestaurantObject>(restaurant));
        }

        /// <summary>
        /// Gets all <see cref="Restaurant"/> objects.
        /// </summary>
        /// <returns>List of <see cref="ReadRestaurantObject"/> objects.</returns>
        [HttpGet]
        public IActionResult GetRestaurants()
        {
            var restaurants = _restaurantService.GetRestaurants();
            return Ok(_mapper.Map<IEnumerable<ReadRestaurantObject>>(restaurants));
        }

        /// <summary>
        /// Adds new <see cref="Restaurant"/> with provided parameters.
        /// Only Admins can add new restaurant.
        /// </summary>
        /// <param><see cref="CreateRestaurantObject"/> object.</param>
        /// <returns><see cref="ReadRestaurantObject"/> object which has been added to database.</returns>
        [Authorize(UsersRoles.Admin)]
        [HttpPost]
        public IActionResult CreateRestaurant(CreateRestaurantObject createRestaurantObject)
        {
            var restaurantObject = _mapper.Map<Restaurant>(createRestaurantObject);
            _restaurantService.AddRestaurant(restaurantObject);
            _restaurantService.Save();

            return Ok(_mapper.Map<ReadRestaurantObject>(restaurantObject));
        }

        /// <summary>
        /// Deletes <see cref="Restaurant"/> with provided identifier.
        /// Only Admins can delete restaurant.
        /// </summary>
        /// <param name="id"><see cref="Restaurant"/> identifier.</param>
        /// <returns>message to let user know that object has been successfully deleted.</returns>
        [Authorize(UsersRoles.Admin)]
        [HttpDelete("{id}")]
        public IActionResult DeleteRestaurant(Guid id)
        {
            if (id == Guid.Empty)
                throw new AppException("Id is empty");

            _restaurantService.DeleteRestaurant(id);
            _restaurantService.Save();

            return Ok(new { message = "Restaurant deleted successfully" });
        }
    }
}
