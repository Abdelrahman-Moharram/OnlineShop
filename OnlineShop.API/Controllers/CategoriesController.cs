using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Core.DTOs.CategoryDTOs;
using OnlineShop.Core.DTOs.ResponsesDTOs;
using OnlineShop.Core.Entities;
using OnlineShop.Core.IServices;

namespace OnlineShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryServices _categoryService;
        private readonly IMapper _mapper;
        public CategoriesController(IMapper mapper, ICategoryServices CategoryService)
        {
            _categoryService = CategoryService;
            _mapper = mapper;
        }

        [HttpGet("top")]
        public async Task<IActionResult> GetTopCategories()
        {
            return Ok(await _categoryService.GetAllWithBaseIncludes(size: 10));
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] AddCategoryDTO CategoryDTO)
        {
            if (ModelState.IsValid)
            {
                BaseResponseDTO response = await _categoryService.AddNew(_mapper.Map<Category>(CategoryDTO), User.Claims.FirstOrDefault(i => i.Type == "userId")?.Value);
                if (response.IsSuccessed)
                    return Ok(response.Message);

                return BadRequest(response.Message);
            }
            return BadRequest(CategoryDTO);
        }

        
    }
}
