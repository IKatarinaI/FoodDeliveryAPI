using FoodDelivery.Models;
using System;

namespace FoodDelivery.Authorization.Interface
{
    public interface IJwtUtils
    {
        public string GenerateToken(User user);
        public Guid ValidateToken(string token);
    }
}
