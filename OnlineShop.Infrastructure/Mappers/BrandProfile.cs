
using AutoMapper;
using OnlineShop.Core.DTOs.BrandDTOs;
using OnlineShop.Core.DTOs;
using OnlineShop.Core.Entities;

namespace OnlineShop.Infrastructure.Mappers
{
    public class BrandProfile : Profile
    {
        public BrandProfile()
        {
            CreateMap<Brand, GetBrandDTO>()
                /*.ForMember(dest => dest.BrandProducts,
                    opt => opt.MapFrom(src =>
                        src.Products.Select(
                            prodbase => new SimpleModule { Id = prodbWase.Id, Name = prodbase.Name }
                            ).ToList()
                        )
                    )*/
                .ForMember(dest => dest.size, opt => opt.MapFrom(src => src.Products.Count()))
                .ReverseMap();

            CreateMap<Brand, SimpleModule>()
                .ForMember(dest => dest.Id,
                        opt => opt.MapFrom(src => new SimpleModule { Id = src.Id, Name = src.Name })
                    ).ReverseMap();

            CreateMap<AddBrandDTO, Brand>().ReverseMap();
            /*CreateMap<UpdateBrandDTO, Brand>().ReverseMap();*/
        }
    }
}
