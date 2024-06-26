﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Core.DTOs.ResponsesDTOs;
using OnlineShop.Core.Entities;
using OnlineShop.Core.IServices;
using OnlineShop.Infrastructure;

namespace OnlineShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminServices _adminServices;
        public AdminController(IAdminServices adminServices)
        {
            _adminServices = adminServices;
        }
        [HttpPost("add-banner-images")]
        [Authorize(Policy = "Permissions.Create.SiteSettings")]
        public async Task<IActionResult> AddBannerImages([FromForm] List<IFormFile> files)
        {
            if (ModelState.IsValid)
            {
                FileUpload fileUpload = new FileUpload();
                List<Banner> Banners =  fileUpload.UploadBannerImages(files);
                BaseResponseDTO response =  await _adminServices.AddBannerImages(Banners, User.Claims.FirstOrDefault(i => i.Type == "userId")?.Value);
                if (response.IsSuccessed) 
                    return Ok(response);
                return BadRequest(response);
            }
            return BadRequest(ModelState);
        }
    }
}
