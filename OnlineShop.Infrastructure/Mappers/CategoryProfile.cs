using AutoMapper;
using OnlineShop.Core.DTOs.CategoryDTOs;
using OnlineShop.Core.DTOs;
using OnlineShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Infrastructure.Mappers
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            /*CreateMap<Category, GetCategoryDTO>()
                .ForMember(dest => dest.CategoryProducts,
                    opt => opt.MapFrom(src =>
                        src.Products.Select(
                            prodbase => new SimpleModule { Id = prodbase.Id, Name = prodbase.Name }
                            ).ToList()
                        )
                    ).ReverseMap();

            CreateMap<Category, SimpleModule>()
                .ForMember(dest => dest.Id,
                        opt => opt.MapFrom(src => new SimpleModule { Id = src.Id, Name = src.Name })
                    ).ReverseMap();*/

            CreateMap<AddCategoryDTO, Category>().ReverseMap();
            /*CreateMap<UpdateCategoryDTO, Category>().ReverseMap();*/

        }
    }
}
