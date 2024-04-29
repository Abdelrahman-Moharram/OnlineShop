using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Core.DTOs.AuthDTOs;
using OnlineShop.Core.DTOs.ResponsesDTOs;
using OnlineShop.Core.IServices;
using OnlineShop.Infrastructure;

namespace OnlineShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAuthServices _authService;
        public AccountsController(IAuthServices authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterDTO register)
        {
            if (ModelState.IsValid)
            {

                FileUpload fileUpload = new FileUpload();
                var file = fileUpload.uploadUserImage(register.Image, register.Username);
                var result = await _authService.Register(register, file);
                if (!result.IsSuccessed)
                    return BadRequest(result.Message);

                SetRefreshTokenInCookies(result.RefreshToken, result.RefreshTokenExpiretion);
                return Ok(result);
            }
            return BadRequest(ModelState);
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            if (ModelState.IsValid)
            {
                var result = await _authService.Login(login);
                if (!result.IsSuccessed)
                    return Unauthorized(result.Message);


                SetRefreshTokenInCookies(result.RefreshToken, result.RefreshTokenExpiretion);
                return Ok(result);
            }
            return BadRequest(ModelState);
        }

        [AllowAnonymous]
        [HttpGet("refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["user_refresh_token"];
            if (string.IsNullOrEmpty(refreshToken))
            {
                return Unauthorized();
            }
            var result = await _authService.GenerateNewRefreshTokenAsync(refreshToken);
            if (!result.IsSuccessed)
                return Unauthorized(result);

            SetRefreshTokenInCookies(result.RefreshToken, result.RefreshTokenExpiretion);
            return Ok(result);
        }
        
        [AllowAnonymous]
        [HttpGet("revoke-token")]
        public async Task<IActionResult> RevokeToken()
        {
            var refreshToken = Request.Cookies["user_refresh_token"];
            if (string.IsNullOrEmpty(refreshToken))
            {
                return Unauthorized(new BaseResponseDTO
                {
                    Message = "Invalid Token"
                });
            }
            var result = await _authService.RevokeTokenAsync(refreshToken);
            if (!result)
                return BadRequest(new BaseResponseDTO
                {
                    Message = "Invalid Token"
                });

            return Ok();
        }

        private void SetRefreshTokenInCookies(string RToken, DateTime RefreshTokenExpiretion)
        {
            CookieOptions cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = RefreshTokenExpiretion.ToLocalTime(),
                SameSite = SameSiteMode.None,
                Secure = true,
                Path = "/",
                IsEssential = true,
            };

            Response.Cookies.Append("user_refresh_token", RToken, cookieOptions);
        }
    }
}
