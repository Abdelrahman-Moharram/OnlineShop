using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Core.DTOs.ProductsDTOs;
using OnlineShop.Core.DTOs.ResponsesDTOs;
using OnlineShop.Core.IServices;
using OnlineShop.Infrastructure;

namespace OnlineShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        IProductServices _productServices;
        public ProductsController(IProductServices productServices)
        {
            _productServices = productServices;
        }
        [HttpGet("")]
        public async Task<IActionResult> ProductList() 
        {
            return Ok(await _productServices.ListProducts());
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddProduct([FromForm] FormProductDTO productDTO)
        {
            if(ModelState.IsValid)
            {
                FileUpload fileUpload = new ();
                var userId = User.Claims.FirstOrDefault(i => i.Type == "userId")?.Value;
                var uploadedFiles = fileUpload.UploadProductImages(productDTO.Files, userId);

                BaseResponseDTO responseDTO = await _productServices.AddProduct(productDTO, uploadedFiles, userId);
                if(responseDTO.IsSuccessed)
                    return BadRequest(responseDTO);
                return Ok(responseDTO);
            }
            return BadRequest(ModelState);
        }
    }
}
