using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineShop.Core.DTOs.CartDTOs;
using OnlineShop.Core.DTOs.ResponsesDTOs;
using OnlineShop.Core.Entities;
using OnlineShop.Core.Interfaces;
using OnlineShop.Core.IServices;

namespace OnlineShop.Services
{
    public class CartServices : ICartServices
    {
        private readonly ILogger<AuthServices> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CartServices(UserManager<ApplicationUser> userManager, ILogger<AuthServices> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetCartItemsDTO> GetCartByUserId(string userId) 
        {
            var Cart = await _unitOfWork.Carts.FindAsync(
                
                expression:i=>i.userId == userId,
                include:
                    i=>i.Include(a=>a.CartItems)
                    .ThenInclude(b=>b.Product)
                    .ThenInclude(p=>p.ProductFiles)
                );

            return _mapper.Map<GetCartItemsDTO>(Cart);
        }

        public async Task<BaseResponseDTO> UpdateCart(BaseCartItemDTO cartDTO, string userId)
        {
            var cart =  await _unitOfWork.Carts.FindAsync(
                    i=>i.userId == userId, 
                    include: i => i.Include(a => a.CartItems)
                );
            var cartItem = _mapper.Map<CartItem>(cartDTO);
            if (await _unitOfWork.Products.FindAsync(i=>i.Id == cartDTO.ProductId) == null)
                return new BaseResponseDTO
                {
                    IsSuccessed = false,
                    Message = "Product Not Found"
                };

            if (cart == null)
            {
                cart = new Cart
                {
                    userId = userId,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = userId,
                };
                cartItem.CreatedBy = userId;
                cartItem.CreatedAt = DateTime.UtcNow;
                cartItem.CartId = cart.Id;
                if(cart.CartItems == null)
                    cart.CartItems = new List<CartItem>();
                cart.CartItems.Add(cartItem);
                
                await _unitOfWork.Carts.AddAsync(cart);
            }
            else
            {

                
                if(cart.CartItems.Any(x=>x.ProductId == cartItem.ProductId)) 
                {
                    var updatedCartItem = cart.CartItems.Single(x => x.ProductId == cartItem.ProductId);
                    if (cartItem.Quantity == 0)
                    {
                        cart.CartItems.Single(x => x.ProductId == cartItem.ProductId).IsDeleted = true;
                        cart.CartItems.Single(x => x.ProductId == cartItem.ProductId).DeletedBy = userId;
                    }
                    else
                    {
                        cart.CartItems.Single(x => x.ProductId == cartItem.ProductId).Quantity = cartItem.Quantity;
                        cart.CartItems.Single(x => x.ProductId == cartItem.ProductId).UpdatedBy = userId;
                    }

                }
                else
                {
                    cartItem.CreatedBy = userId;
                    cartItem.CreatedAt = DateTime.UtcNow;
                    cartItem.CartId = cart.Id;
                    if (cart.CartItems == null)
                        cart.CartItems = new List<CartItem>();
                    await _unitOfWork.CartItems.AddAsync(cartItem);
                }
                await _unitOfWork.Carts.UpdateAsync(cart);
            }
            await _unitOfWork.SaveAsync();
            return new BaseResponseDTO 
            {
                IsSuccessed = true,
                Message= "Cart Updated Successfully"
            };
             

        }

        public async Task<BaseResponseDTO> DeleteCartItem(string productId, string userId)
        {
            var cart = await _unitOfWork.Carts.FindAsync(
                    i => (i.userId == userId),
                    include: i => i.Include(a => a.CartItems)
                );
            if (cart.CartItems.Any(x => x.ProductId == productId))
            {
                cart.CartItems.Single(x => x.ProductId == productId).IsDeleted = true;
                cart.CartItems.Single(x => x.ProductId == productId).DeletedBy = userId;
                await _unitOfWork.Carts.UpdateAsync(cart);
                await _unitOfWork.SaveAsync();
                return new BaseResponseDTO 
                {
                    IsSuccessed = true,
                    Message="Item Deleted Successfully"
                };
            }
            return new BaseResponseDTO 
            {
                IsSuccessed = false,
                Message = "Cart doesn't have this product"
            };
        }
    }
}
