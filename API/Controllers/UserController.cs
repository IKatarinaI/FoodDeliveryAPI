using AutoMapper;
using FoodDelivery.Authorization.Interface;
using FoodDelivery.DTOs.CreateObjects;
using FoodDelivery.DTOs.ReadObjects;
using FoodDelivery.Helpers;
using FoodDelivery.Models;
using FoodDelivery.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using static FoodDelivery.Shared.Enums;
using AuthorizeAttribute = FoodDelivery.Helpers.AuthorizeAttribute;

namespace FoodDelivery.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private IJwtUtils _jwtUtils;

        public UserController(IUserService userService, IMapper mapper, IJwtUtils jwtUtils)
        {
            _userService = userService ??
                throw new ArgumentNullException(nameof(userService));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
            _jwtUtils = jwtUtils ??
                throw new ArgumentNullException(nameof(jwtUtils));
        }

        /// <summary>
        /// Gets <see cref="User"/> with provided identifier.
        /// Only Admins can get all users.
        /// </summary>
        /// <param name="id"><see cref="User"/> identifier.</param>
        /// <returns><see cref="ReadUserObject"/> object satisfying the provided identifier.</returns>
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetUser(Guid id)
        {
            if (id == Guid.Empty)
                throw new AppException("Id is empty");

            if (id != User.Id && User.Role != UsersRoles.Admin.ToString())
                return Unauthorized(new { message = "Unauthorized" });

            var user = _userService.GetUser(id);

            return Ok(_mapper.Map<ReadUserObject>(user));
        }

        /// <summary>
        /// Signs up <see cref="User"/> with provided parameters.
        /// </summary>
        /// <param><see cref="CreateUserObject"/> object.</param>
        /// <returns><see cref="ReadUserObject"/> object which has been added to database.</returns>
        [HttpPost("signup")]
        public IActionResult SignUp(CreateUserObject createUserObject)
        {
            var user = _mapper.Map<User>(createUserObject);
            _userService.AddUser(user);
            _userService.Save();

            return Ok(_mapper.Map<ReadUserObject>(user));
        }

        /// <summary>
        /// Logs in <see cref="User"/> with provided parameters.
        /// </summary>
        /// <param><see cref="CreateLoginObject"/> object.</param>
        /// <returns><see cref="ReadLoginObject"/> object which has been logged ins.</returns>
        [HttpPost("login")]
        public IActionResult Login(CreateLoginObject createLoginObject)
        {
            var userObject = _mapper.Map<User>(createLoginObject);
            var authenticatedUser = _userService.Authenticate(userObject);
            var user = _mapper.Map<ReadLoginObject>(authenticatedUser);
            user.JwtToken = _jwtUtils.GenerateToken(authenticatedUser);

            return Ok(user);
        }
    }
}
