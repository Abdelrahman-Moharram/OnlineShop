using AutoMapper;
using OnlineShop.Core.DTOs.ProductsDTOs;
using OnlineShop.Core.Entities;

namespace OnlineShop.Infrastructure.Mappers
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ListProductsDTO>()
                .ForMember(dest=>dest.Image, opt=>opt.MapFrom(src=>src.UploadedFiles.Select(i=> $"/Products/{i.FileName}").ToList()));

            CreateMap<FormProductDTO, Product>()
            .ForMember(dest => dest.UploadedFiles, opt => opt.MapFrom(src => src.Files.Select(i => $"/Products/{i.FileName}").ToList()))
                ;

        }
    }
}
