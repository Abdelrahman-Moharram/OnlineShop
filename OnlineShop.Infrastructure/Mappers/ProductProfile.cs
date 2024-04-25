using AutoMapper;
using OnlineShop.Core.DTOs.ProductsDTOs;
using OnlineShop.Core.Entities;

namespace OnlineShop.Infrastructure.Mappers
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            Random rnd = new Random();

            CreateMap<Product, ListProductsDTO>()
                .ForMember(dest=>dest.Image, opt=>opt.MapFrom(src=>src.ProductFiles.Select(i=> i.FileName).ToList()));

            CreateMap<Product, ProductDetailsDTO>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.ProductFiles.Select(i => i.FileName).ToList()))
                .ForMember(dest => dest.CategoryName, opt=>opt.MapFrom(src=>src.Category.Name))
                .ForMember(dest => dest.BrandName, opt=>opt.MapFrom(src=>src.Brand.Name));

            

            CreateMap<FormProductDTO, Product>()
            .ForMember(dest => dest.ProductFiles, opt => opt.MapFrom(src => 
                src.Files.Select(i => 
                    new ProductFile { 
                        FileName = $"/Products/{i.FileName}", 
                        ContentType = i.ContentType,
                        CreatedAt = DateTime.Now
                    }).ToList()))
                ;
            CreateMap<SeedProductDTO, Product>()
                .ForMember(dest=>dest.Category, opt => opt.Ignore())
                .ForMember(dest=>dest.Brand, opt => opt.Ignore())
                .ForMember(dest=>dest.discount, opt => opt.MapFrom(src=> rnd.Next(20)))
                .ForMember(dest => dest.ProductFiles, opt => opt.MapFrom(src =>
                    src.Images.Select(i =>
                        new ProductFile
                        {
                            FileName = i,
                            ContentType = "image/webp",
                            CreatedAt = DateTime.Now
                        }).ToList()))
                ;


        }
    }
}
