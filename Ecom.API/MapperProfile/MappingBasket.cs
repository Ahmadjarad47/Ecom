using AutoMapper;
using Ecom.Core.Dto;
using Ecom.Core.Entities;

namespace Ecom.API.MapperProfile
{
    public class MappingBasket:Profile
    {
        public MappingBasket()
        {
            CreateMap<CustomerBasket,CustomerBasketDTO>().ReverseMap();
            CreateMap<BasketItem, BasketItemDTO>().ReverseMap();    
        }
    }
}
