using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Core.DTOs.OrderDTOs;
using OnlineShop.Core.DTOs.ResponsesDTOs;
using OnlineShop.Core.IServices;

namespace OnlineShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IOrderServices _orderServices;
        public OrdersController(IMapper mapper, IOrderServices orderServices)
        {
            _mapper = mapper;
            _orderServices = orderServices;
        }

        [HttpPost("checkout")]
        [Authorize(Policy = "Permissions.Create.Order")]
        public async Task<IActionResult> Checkout()
        {
            BaseResponseDTO response = await _orderServices.PlaceOrder(User.Claims.FirstOrDefault(i => i.Type == "userId")?.Value);
            if(response.IsSuccessed)
                return Ok(response);
            return BadRequest(response);
        }
    }
}
