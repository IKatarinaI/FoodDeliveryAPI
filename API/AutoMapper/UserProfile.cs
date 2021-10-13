using AutoMapper;
using FoodDelivery.DTOs.CreateObjects;
using FoodDelivery.DTOs.ReadObjects;
using FoodDelivery.Models;

namespace FoodDelivery.AutoMapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateLoginObject, User>();
            CreateMap<CreateUserObject, User>();
            CreateMap<ReadUserObject, User>();
            CreateMap<User, ReadUserObject>();
            CreateMap<User, ReadLoginObject>();
        }
    }
}
