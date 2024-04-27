using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Core.DTOs.BrandDTOs;
using OnlineShop.Core.DTOs.ResponsesDTOs;
using OnlineShop.Core.Entities;
using OnlineShop.Core.IServices;

namespace OnlineShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandServices _brandService;
        private readonly IMapper _mapper;
        public BrandsController(IMapper mapper, IBrandServices brandService)
        {
            _brandService = brandService;
            _mapper = mapper;
        }

        [HttpGet("top")]
        public async Task<IActionResult> GetTopBrands()
        {
            return Ok(await _brandService.GetAllWithBaseIncludes(size: 10));
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] AddBrandDTO BrandDTO)
        {
            if (ModelState.IsValid)
            {
                BaseResponseDTO response = await _brandService.AddNew(_mapper.Map<Brand>(BrandDTO), User.Claims.FirstOrDefault(i => i.Type == "userId")?.Value);
                if (response.IsSuccessed)
                    return Ok(response.Message);

                return BadRequest(response.Message);
            }
            return BadRequest(BrandDTO);
        }

    }
}
