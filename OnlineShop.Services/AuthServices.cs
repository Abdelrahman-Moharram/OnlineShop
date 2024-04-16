using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OnlineShop.Core.DTOs.AuthDTOs;
using OnlineShop.Core.DTOs.ResponsesDTOs;
using OnlineShop.Core.Entities;
using OnlineShop.Core.IServices;
using OnlineShop.Core.Settings;
using OnlineShop.Infrastructure.Helper;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OnlineShop.Services
{
    public class AuthServices : IAuthServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JWTSettings _jwt;
        private readonly ILogger<AuthServices> _logger;

        public AuthServices(UserManager<ApplicationUser> userManager, IOptions<JWTSettings> jwt, ILogger<AuthServices> logger)
        {

            _userManager = userManager;
            _logger = logger;
            _jwt = jwt.Value;
        }


        public async Task<ApplicationUser> AddUser(RegisterDTO userDTO)
        {
            if (await _userManager.FindByNameAsync(userDTO.Username) != null)
                return null;

            if (await _userManager.FindByEmailAsync(userDTO.Email) != null)
                return null;
            ApplicationUser user = new ApplicationUser
            {
                Email = userDTO.Email,
                UserName = userDTO.Username,
            };
            var result = await _userManager.CreateAsync(user, userDTO.Password);
            if (result.Succeeded)
                return user;
            return null;
        }

        public async Task<BaseResponseDTO> Login(LoginDTO loginDTO)
        {
            try
            {
                ApplicationUser user;

                if (loginDTO.Username.IsEmail())
                    user = await _userManager.FindByEmailAsync(loginDTO.Username);
                else
                    user = await _userManager.FindByNameAsync(loginDTO.Username);

                if (user == null)
                    return new BaseResponseDTO
                    {
                        IsSuccessed = false,
                        Message = "Username or Password is incorrect !"
                    };

                JwtSecurityToken token = await CreateJWT(user);

                return new AuthResponseDTO
                {
                    IsSuccessed = true,
                    Email = user.Email,
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    UserName = user.UserName,
                    Message = $"{user.UserName} Loogedin Successfully"
                };
            }
            catch (Exception ex)
            {

                _logger.LogError($"Some Thing went wrong While signing in for {loginDTO.Username} +  ", ex);
                return new BaseResponseDTO 
                {
                    IsSuccessed = false,
                    Message = "Some Thing went wrong While signing in",
                };
            }
                
        }

        public async Task<BaseResponseDTO> Register(RegisterDTO userDTO)
        {
            try
            {
                if (await _userManager.FindByNameAsync(userDTO.Username) != null)
                    return new BaseResponseDTO { Message = userDTO.Username + " is already exists !" };

                if (await _userManager.FindByEmailAsync(userDTO.Email) != null)
                    return new BaseResponseDTO { Message = userDTO.Email + " is already exists !" };


                ApplicationUser user = await AddUser(userDTO);
                if (user == null)
                    return new BaseResponseDTO { Message = $"something went wrong while creating {userDTO.Username}" };

                var roleResult = await _userManager.AddToRoleAsync(user, "Basic");
                if (!roleResult.Succeeded)
                    return new BaseResponseDTO { Message = $"something went wrong while completing your account data" };

                JwtSecurityToken token = await CreateJWT(user);

                return new AuthResponseDTO
                {
                    IsSuccessed = true,
                    Message = "Account Created Successfully",
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Email = userDTO.Email,
                    UserName = userDTO.Username
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Some Thing went wrong While signing in for {userDTO.Username} +  ", ex);
                return new BaseResponseDTO
                {
                    IsSuccessed = false,
                    Message = "Some Thing went wrong While signing in",
                };
            }

        }


        private async Task<JwtSecurityToken> CreateJWT(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
            {
                roleClaims.Add(new Claim("roles", role));
                /*foreach (var claim in _roleService.GetRoleClaimsPermissions(role).Result)
                    if (roleClaims.FirstOrDefault(i => i.Value == claim) == null)
                        roleClaims.Add(new Claim(OtherConstants.Permissions.ToString(), claim));*/
            }



            var Claims = new List<Claim>
                {
                       new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                       new Claim("username", user.UserName),
                       new Claim("email", user.Email),
                       new Claim(ClaimTypes.NameIdentifier, user.Id),
                       new Claim("userId", user.Id),
                }
            .Union(userClaims)
            .Union(roleClaims);



            SigningCredentials credentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.SECRETKEY)),
                    SecurityAlgorithms.HmacSha256
                );


            return new JwtSecurityToken(
                    claims: Claims,
                    signingCredentials: credentials,
                    issuer: _jwt.Issuer,
                    audience: _jwt.Audience,
                    expires: DateTime.Now.AddMinutes(_jwt.DurationInMinutes)
                );
        }
    }
}
