using AutoMapper;
using Ecom.API.Helper;
using Ecom.Core.Dto;
using Ecom.Core.Entities;

namespace Ecom.API.MapperProfile
{
    public class ProdectMapper:Profile
    {
        public ProdectMapper()
        {
            CreateMap<Prodect, ProdectDTO>()
                .ForMember(m=>m.CategoryName,
                op=>op.MapFrom(s=>s.Category.Name))
              .ForMember(m=>m.Picture,o=>o.MapFrom<ProdectResoling>())
                .ReverseMap();
            CreateMap<CreateProdectDTO, Prodect>().ReverseMap();
            CreateMap<UpdateProdct,Prodect>().ReverseMap();
        }
    }
}
