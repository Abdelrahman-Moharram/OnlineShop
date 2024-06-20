using Microsoft.AspNetCore.Authorization;
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
        private readonly IProductServices _productServices;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductServices productServices, ILogger<ProductsController> logger)
        {
            _productServices = productServices;
            _logger = logger;
        }

        [HttpGet("")]
        [AllowAnonymous]
        public async Task<IActionResult> ProductList([FromQuery] int take, [FromQuery] int skip, string? sort, decimal? minprice, decimal? maxprice) 
            => Ok(await _productServices.ListProducts(take, skip, sort, minprice, maxprice));
        
        
        [HttpGet("{id}")]
        [AllowAnonymous]

        public async Task<IActionResult> ProductDetails(string id)
            => Ok(await _productServices.ProductDetails(id));


        [HttpPost("add")]
        [Authorize(Policy = "Permissions.Create.Product")]
        public async Task<IActionResult> AddProduct([FromForm] FormProductDTO productDTO)
        {
            if(ModelState.IsValid)
            {
                FileUpload fileUpload = new ();
                var userId = User.Claims.FirstOrDefault(i => i.Type == "userId")?.Value;
                var uploadedFiles = fileUpload.UploadProductImages(productDTO.Files);

                BaseResponseDTO responseDTO = await _productServices.AddProduct(productDTO, uploadedFiles, userId);
                if(responseDTO.IsSuccessed)
                    return BadRequest(responseDTO);
                return Ok(responseDTO);
            }
            return BadRequest(ModelState);
        }

        [HttpPost("seed-product")]
        [Authorize(Policy = "Permissions.Create.Product")]
        public async Task<IActionResult> ProductSeeding([FromBody] SeedProductDTO productDTO)
        {
            if (ModelState.IsValid)
            {
                FileUpload fileUpload = new();
                var userId = User.Claims.FirstOrDefault(i => i.Type == "userId")?.Value;

                BaseResponseDTO responseDTO = await _productServices.SeedProduct(productDTO, userId);
                if (responseDTO.IsSuccessed)
                    return BadRequest(responseDTO);
                return Ok(responseDTO);
            }
            return BadRequest(ModelState);
        }

        [HttpGet("search")]
        [AllowAnonymous]

        public async Task<IActionResult> SearchProduct([FromQuery] string query)
            => Ok(await _productServices.Search(query, take: 5));

        [HttpGet("suggestions-category-brand")]
        [AllowAnonymous]

        public async Task<IActionResult> GetProductByCategoryOrBrand([FromQuery] string id, [FromQuery] string productid)
            => Ok(await _productServices.GetProductByCategoryIdOrBrandId(id, productid));


        [HttpGet("seed-product-item")]
        [Authorize(Policy = "Permissions.Create.ProductItem")]
        public async Task<IActionResult> SeedProductItems()
        {
            var userId = User.Claims.FirstOrDefault(i => i.Type == "userId")?.Value;
            return Ok(await _productServices.SeedProductItem(User.Claims.FirstOrDefault(i => i.Type == "userId")?.Value));

        }
        

    }
}
