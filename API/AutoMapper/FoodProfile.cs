using AutoMapper;
using FoodDelivery.DTOs.CreateObjects;
using FoodDelivery.DTOs.ReadObjects;
using FoodDelivery.DTOs.UpdateObjects;
using FoodDelivery.Models;

namespace FoodDelivery.AutoMapper
{
    public class FoodProfile : Profile
    {
        public FoodProfile()
        {
            CreateMap<CreateFoodObject, Food>();
            CreateMap<Food, ReadFoodObject>();
            CreateMap<ReadFoodObject, Food>();
            CreateMap<UpdateFoodRatingObject, Food>();
        }
    }
}
