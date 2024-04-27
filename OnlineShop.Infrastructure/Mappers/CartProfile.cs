
using AutoMapper;
using OnlineShop.Core.DTOs.CartDTOs;
using OnlineShop.Core.Entities;

namespace OnlineShop.Infrastructure.Mappers
{
    public class CartProfile : Profile
    {
        public CartProfile()
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

            /*return new GetCartItemsDTO
            {
                userId = userId,
                cartId = Cart.Id,
                CartItems = _mapper.Map<IEnumerable<BaseCartItemDTO>>(Cart.CartItems)
            };*/
            CreateMap<Cart, GetCartItemsDTO>()
                .ForMember(dest=>dest.cartId, opt=>opt.MapFrom(src=>src.Id))
                .ForMember(dest=>dest.CartItems, opt=>opt.MapFrom(src=>
                    src.CartItems.Select(i=>
                        new GetCartItemDTO
                        {
                            ProductId = i.ProductId,
                            Quantity = i.Quantity,
                            ProductName = i.Product.Name,
                            image = i.Product.ProductFiles.FirstOrDefault().FileName,
                            price = i.Product.Price - (i.Product.Price * i.Product.discount / 100)
                        }
                    ).ToList()
                ));

            CreateMap<CartItem, BaseCartItemDTO>().ReverseMap();

        }
    }
}
