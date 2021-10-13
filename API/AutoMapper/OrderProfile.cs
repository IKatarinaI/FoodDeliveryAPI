using AutoMapper;
using FoodDelivery.DTOs.CreateObjects;
using FoodDelivery.DTOs.ReadObjects;
using FoodDelivery.Models;

namespace FoodDelivery.AutoMapper
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<CreateOrderObject, Order>();
            CreateMap<Order, ReadOrderObject>();
        }
    }
}
