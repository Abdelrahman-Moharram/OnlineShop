using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Core.DTOs.CartDTOs;
using OnlineShop.Core.DTOs.ResponsesDTOs;
using OnlineShop.Core.IServices;

namespace OnlineShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ICartServices _cartServices;
        public CartsController(ICartServices cartServices)
        {
            _cartServices = cartServices;
        }

        [HttpGet("")]
        [Authorize(Policy = "Permissions.Read.Cart")]
        public async Task<IActionResult> GetCartWithItem()
        {
            var userId = User.Claims.FirstOrDefault(i => i.Type == "userId")?.Value;

             return Ok(await _cartServices.GetCartByUserId(userId));
        }
        // AddCartDTO(BaseCartItemDTO cartDTO, string userId)

        [HttpPost("update")]
        [Authorize(Policy = "Permissions.Update.CartItem")]
        public async Task<IActionResult> UpdateCart([FromBody] BaseCartItemDTO cartDTO)
        {
            if(ModelState.IsValid)
            {
                BaseResponseDTO response =  await _cartServices.UpdateCart(cartDTO, User.Claims.FirstOrDefault(i => i.Type == "userId")?.Value);
                if(response.IsSuccessed)
                    return Ok(response);

                return BadRequest(response);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Policy = "Permissions.Update.CartItem")]
        public async Task<IActionResult> DeleteCartItem([FromRoute] string id)
        {
            var response = await _cartServices.DeleteCartItem(id, User.Claims.FirstOrDefault(i => i.Type == "userId")?.Value);
            
            if(response.IsSuccessed)
                return Ok(response);
            
            return BadRequest(response);
        }

    }
}
