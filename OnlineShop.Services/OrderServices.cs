
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineShop.Core.DTOs.OrderDTOs;
using OnlineShop.Core.DTOs.ResponsesDTOs;
using OnlineShop.Core.Entities;
using OnlineShop.Core.Interfaces;
using OnlineShop.Core.IServices;

namespace OnlineShop.Services
{
    public class OrderServices : IOrderServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderServices> _logger;
        public OrderServices(IUnitOfWork unitOfWork, IMapper mapper, ILogger<OrderServices> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<BaseResponseDTO> PlaceOrder(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return new BaseResponseDTO { Message = "Can't assign Transacation To user, user id is empty", IsSuccessed= false };



            var cart = await _unitOfWork.Carts.FindAsync(i=>i.userId == userId, 
                include:i=>i.Include(c=>c.CartItems));


            try
            {
                if (cart.CartItems.Count == 0)
                    return new BaseResponseDTO
                    {
                        Message = "User Cart Has no Items"
                    };
                Order newOrder = new Order
                {
                    CreatedBy = userId,
                    CreatedAt = DateTime.Now,
                    CustomerId = userId,
                    OrderItems = new List<OrderItem>()
                };
                
                foreach (var item in cart.CartItems)
                {

                    var prodItems =  await _unitOfWork.ProductItems.FindAllAsync(i=>(i.ProductId == item.ProductId), take: item.Quantity, skip:0);
                    var product = await _unitOfWork.Products.GetById(item.ProductId);
                    if (prodItems.Count() != item.Quantity)
                        return new BaseResponseDTO { Message =$"{product.Name} stock quantity is less than the quantity you wanted" };

                    foreach (var prodItem in prodItems)
                    {
                        newOrder.OrderItems.Add(new OrderItem
                        {
                            CreatedBy = userId,
                            CreatedAt = DateTime.Now,
                            OrderId = newOrder.Id,
                            ProductItemId = prodItem.Id,
                        });
                        // todo -> make is selled products item 
                        prodItem.IsSelled = true;
                        newOrder.TotalPrice += (product.Price - (product.Price * product.discount / 100));
                    }
                }
                cart.CartItems = new List<CartItem>();
                await _unitOfWork.Orders.AddAsync(newOrder);

                // _unitOfWork.Carts.FindAsync(i=>i.userId == CreatedBy).Result.CartItems = new List<CartItem>();
                await _unitOfWork.SaveAsync();

                return new BaseResponseDTO { Message ="Order Placed Successfully!", IsSuccessed=true };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return new BaseResponseDTO { Message = "Something went wrong while placing the Order", IsSuccessed = false };
            }
        }



        public async Task<BaseResponseDTO> NewOrder(AddOrderDTO orderDTO, string CreatedBy)
        {
            if (string.IsNullOrEmpty(CreatedBy))
                return new BaseResponseDTO { Message = "Can't assign Transacation To user, user id is empty", IsSuccessed = false };

            if (orderDTO.cartItems == null || orderDTO.cartItems.Count() == 0)
                return new BaseResponseDTO { Message = "Invalid Order Product List is Empty", IsSuccessed = false };

            try
            {
                Order newOrder = new Order
                {
                    CreatedBy = CreatedBy,
                    CreatedAt = DateTime.Now,
                    CustomerId = CreatedBy,
                    OrderItems = new List<OrderItem>()
                };
                foreach (var item in orderDTO.cartItems)
                {

                    var prodItems = await _unitOfWork.ProductItems.FindAllAsync(i => (i.ProductId == item.ProductId), take: item.Quantity, skip: 0);
                    var product = await _unitOfWork.Products.GetById(item.ProductId);
                    if (prodItems.Count() != item.Quantity)
                        return new BaseResponseDTO { Message = $"{product.Name} stock quantity is less than the quantity you wanted" };

                    foreach (var prodItem in prodItems)
                    {
                        newOrder.OrderItems.Add(new OrderItem
                        {
                            CreatedBy = CreatedBy,
                            CreatedAt = DateTime.Now,
                            OrderId = newOrder.Id,
                            ProductItemId = prodItem.Id,
                        });
                        // todo -> make is selled products item 
                        prodItem.IsSelled = true;
                        newOrder.TotalPrice += (product.Price - (product.Price * product.discount / 100));
                    }
                }
                await _unitOfWork.Orders.AddAsync(newOrder);

                // _unitOfWork.Carts.FindAsync(i=>i.userId == CreatedBy).Result.CartItems = new List<CartItem>();
                await _unitOfWork.SaveAsync();

                return new BaseResponseDTO { Message = "Order Placed Successfully!", IsSuccessed = true };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return new BaseResponseDTO { Message = "Something went wrong while placing the Order", IsSuccessed = false };
            }
        }
    }
}
