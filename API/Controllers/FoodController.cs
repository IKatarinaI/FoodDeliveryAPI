using AutoMapper;
using FoodDelivery.DTOs.CreateObjects;
using FoodDelivery.DTOs.ReadObjects;
using FoodDelivery.DTOs.UpdateObjects;
using FoodDelivery.Helpers;
using FoodDelivery.Models;
using FoodDelivery.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using static FoodDelivery.Shared.Enums;
using AuthorizeAttribute = FoodDelivery.Helpers.AuthorizeAttribute;

namespace FoodDelivery.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FoodController : BaseController
    {
        private readonly IFoodService _foodService;
        private readonly IMapper _mapper;

        public FoodController(IFoodService foodService, IMapper mapper)
        {
            _foodService = foodService ??
                throw new ArgumentNullException(nameof(foodService));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Gets all <see cref="Food"/> objects.
        /// </summary>
        /// <returns>List of <see cref="ReadFoodObject"/> objects.</returns>
        [HttpGet]
        public IActionResult GetFoods()
        {
            var foods = _foodService.GetFoods();
            return Ok(_mapper.Map<IEnumerable<ReadFoodObject>>(foods));
        }

        /// <summary>
        /// Gets <see cref="Food"/> with provided identifier.
        /// </summary>
        /// <param name="id"><see cref="Food"/> identifier.</param>
        /// <returns><see cref="ReadFoodObject"/> object satisfying the provided identifier.</returns>
        [HttpGet("{id}")]
        public IActionResult GetFood(Guid id)
        {
            if (id == Guid.Empty)
                throw new AppException("Id is empty");

            var food = _foodService.GetFood(id);

            return Ok(_mapper.Map<ReadFoodObject>(food));
        }

        /// <summary>
        /// Adds new <see cref="Food"/> with provided parameters.
        /// Only Admins can add new food.
        /// </summary>
        /// <param><see cref="CreateFoodObject"/> object.</param>
        /// <returns><see cref="ReadFoodObject"/> object which has been added to database.</returns>
        [Authorize(UsersRoles.Admin)]
        [HttpPost]
        public IActionResult CreateFood(CreateFoodObject createFoodObject)
        {
            var foodObject = _mapper.Map<Food>(createFoodObject);
            _foodService.AddFood(foodObject);
            _foodService.Save();

            return Ok(_mapper.Map<ReadFoodObject>(foodObject));
        }

        /// <summary>
        /// Deletes <see cref="Food"/> with provided identifier.
        /// Only Admins can delete food.
        /// </summary>
        /// <param name="id"><see cref="Food"/> identifier.</param>
        /// <returns>message to let user know that object has been successfully deleted.</returns>
        [Authorize(UsersRoles.Admin)]
        [HttpDelete("{id}")]
        public IActionResult DeleteFood(Guid id)
        {
            if (id == Guid.Empty)
                throw new AppException("Id is empty");

            _foodService.DeleteFood(id);
            _foodService.Save();

            return Ok(new { message = "Food deleted successfully" });
        }

        /// <summary>
        /// Rates <see cref="Food"/> with provided identifier.
        /// Only logged in user can rate food.
        /// </summary>
        /// <param name="id"><see cref="Food"/> identifier.</param>
        /// <param name="rating"><see cref="Food"/> rate.</param>
        /// <returns>message to let user know that object has been successfully rated.</returns>
        [Authorize]
        [HttpPatch("rate")]
        public IActionResult AddRating(UpdateFoodRatingObject updateFoodRatingObject)
        {
            if (updateFoodRatingObject.Id == Guid.Empty)
                throw new AppException("Id is empty");

            var food = _mapper.Map<Food>(updateFoodRatingObject);

            _foodService.UpdateFood(food);
            _foodService.Save();

            return Ok(new { message = "Rating has been added successfully" });
        }

        /// <summary>
        /// Gets all <see cref="Food"/> objects sorted by rating.
        /// </summary>
        /// <returns>List of <see cref="ReadFoodObject"/> objects.</returns>
        [HttpGet("/sortedbyrating")]
        public IActionResult GetSortedFoods()
        {
            var foods = _foodService.GetFoods().OrderBy(x => x.Rating);

            return Ok(_mapper.Map<IEnumerable<ReadFoodObject>>(foods));
        }
    }
}
