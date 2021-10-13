using FoodDelivery.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using static FoodDelivery.Shared.Enums;

namespace FoodDelivery.Helpers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly IList<UsersRoles> _roles;

        public AuthorizeAttribute(params UsersRoles[] roles)
        {
            _roles = roles ?? new UsersRoles[] { };
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = (User)context.HttpContext.Items["User"];
            var role = ConvertStringToEnum(user?.Role);

            if (user is null || (_roles.Any() && !_roles.Contains(role)))
            {
                // not logged in or role is not authorized
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }

        private UsersRoles ConvertStringToEnum(string role)
        {
            switch(role)
            {
                case "Admin":
                    return UsersRoles.Admin;
                case "Customer":
                    return UsersRoles.Customer;
                case "Couirer":
                    return UsersRoles.Couirer;
                default:
                    return UsersRoles.Customer;
            }
        }
    }
}
