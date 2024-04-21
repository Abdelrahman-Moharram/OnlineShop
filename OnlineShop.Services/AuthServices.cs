using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
using System.Security.Cryptography;
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

        public async Task<AuthResponseDTO> Login(LoginDTO loginDTO)
        {
            try
            {
                ApplicationUser user;

                if (loginDTO.Username.IsEmail())
                    user = await _userManager.FindByEmailAsync(loginDTO.Username);
                else
                    user = await _userManager.FindByNameAsync(loginDTO.Username);


                if (user == null || ! await _userManager.CheckPasswordAsync(user, loginDTO.Password))
                    return new AuthResponseDTO
                    {
                        IsSuccessed = false,
                        Message = "Username or Password is incorrect !"
                    };
                JwtSecurityToken token = await CreateJWT(user);

                var Response = new AuthResponseDTO
                {
                    IsSuccessed = true,
                    Email = user.Email,
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    UserName = user.UserName,
                    Message = $"{user.UserName} Loggedin Successfully"
                };
                RefreshToken refreshtoken = GenerateRefreshToken();

                Response.RefreshToken = refreshtoken.Token;
                Response.RefreshTokenExpiretion = refreshtoken.ExpiresOn;
                user.RefreshTokens?.Add(refreshtoken);
                await _userManager.UpdateAsync(user);

                return Response;

            }
            catch (Exception ex)
            {

                _logger.LogError($"Some Thing went wrong While signing in for {loginDTO.Username} +  ", ex);
                return new AuthResponseDTO
                {
                    IsSuccessed = false,
                    Message = "Some Thing went wrong While signing in",
                };
            }
                
        }

        public async Task<AuthResponseDTO> Register(RegisterDTO userDTO)
        {
            try
            {
                if (await _userManager.FindByNameAsync(userDTO.Username) != null)
                    return new AuthResponseDTO { Message = userDTO.Username + " is already exists !" };

                if (await _userManager.FindByEmailAsync(userDTO.Email) != null)
                    return new AuthResponseDTO { Message = userDTO.Email + " is already exists !" };


                ApplicationUser user = await AddUser(userDTO);
                if (user == null)
                    return new AuthResponseDTO { Message = $"something went wrong while creating {userDTO.Username}" };

                var roleResult = await _userManager.AddToRoleAsync(user, "Basic");
                if (!roleResult.Succeeded)
                    return new AuthResponseDTO { Message = $"something went wrong while completing your account data" };

                JwtSecurityToken token = await CreateJWT(user);
                var refreshToken = GenerateRefreshToken();
                user.RefreshTokens?.Add(refreshToken);
                await _userManager.UpdateAsync(user);

                return new AuthResponseDTO
                {
                    IsSuccessed = true,
                    Message = "Account Created Successfully",
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Email = userDTO.Email,
                    UserName = userDTO.Username,
                    RefreshToken = refreshToken?.Token,
                    RefreshTokenExpiretion = refreshToken.ExpiresOn
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Some Thing went wrong While Registeration in for {userDTO.Username} +  ", ex);
                return new AuthResponseDTO
                {
                    IsSuccessed = false,
                    Message = "Some Thing went wrong While Registeration",
                };
            }

        }

        public async Task<AuthResponseDTO> GenerateNewRefreshTokenAsync(string token)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u=>u.RefreshTokens.Any(t=>t.Token == token));
            if(user == null)
            {
                return new AuthResponseDTO
                {
                    Message= "Invalid Token"
                };
            }

            var refreshToken = user.RefreshTokens.Single(t=>t.Token == token);
            if (!refreshToken.IsActive)
            {
                return new AuthResponseDTO
                {
                    Message = "Inactive Token"
                };
            }

            refreshToken.RevokedOn = DateTime.UtcNow;
            var newRefreshToken = GenerateRefreshToken();
            user.RefreshTokens.Add(newRefreshToken);

            await _userManager.UpdateAsync(user);

            var accessToken = await CreateJWT(user);

            return new AuthResponseDTO
            {
                Email = user.Email,
                UserName = user.UserName,
                IsSuccessed = true,
                Token = new JwtSecurityTokenHandler().WriteToken(accessToken),
                RefreshToken = newRefreshToken.Token,
                RefreshTokenExpiretion = newRefreshToken.ExpiresOn,
            };

        }

        public async Task<bool> RevokeTokenAsync(string token)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token));
            if (user == null)
            {
                return false;
            }

            var refreshToken = user.RefreshTokens.Single(t => t.Token == token);
            if (!refreshToken.IsActive)
            {
                return false;
            }
            refreshToken.RevokedOn = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);
            return true;
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
                       new Claim("username", user?.UserName),
                       new Claim("email", user?.Email),
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

        private RefreshToken GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var generator = RandomNumberGenerator.Create();
            generator.GetBytes(randomNumber);
            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomNumber),
                ExpiresOn = DateTime.UtcNow.AddDays(10),
                CreatedOn = DateTime.UtcNow
            };
        }
    }
}
