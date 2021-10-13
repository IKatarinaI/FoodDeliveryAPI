using AutoMapper;
using FoodDelivery.DTOs.CreateObjects;
using FoodDelivery.DTOs.ReadObjects;
using FoodDelivery.Models;

namespace FoodDelivery.AutoMapper
{
    public class RestaurantProfil : Profile
    {
        public RestaurantProfil()
        {
            CreateMap<CreateRestaurantObject, Restaurant>();
            CreateMap<Restaurant, ReadRestaurantObject>();
            CreateMap<ReadRestaurantObject, Restaurant>();
        }
    }
}
