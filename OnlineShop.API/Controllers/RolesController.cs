using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Core.DTOs.AuthDTOs;
using OnlineShop.Core.DTOs.ResponsesDTOs;
using OnlineShop.Core.IServices;

namespace OnlineShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleServices _roleService;
        public RolesController(IRoleServices roleService)
        {
            _roleService = roleService;
        }


        // List of roles

        [Authorize(Policy = "Permissions.Read.Accounts")]
        [HttpGet("")]
        public async Task<IActionResult> AllRoles()
        {
            return Ok(await _roleService.AllRoles());
        }


        [Authorize(Policy = "Permissions.Read.Accounts")]
        [HttpGet("{roleId}")]
        public async Task<IActionResult> GetRolePermissions([FromRoute] string roleId)
        {
            var roles =  await _roleService.GetRoleClaimsPermissions(roleId);
            if (roles == null) 
                return NotFound(new BaseResponseDTO { IsSuccessed = false, Message = "this role is not found"});
            return Ok(roles);
        }

        // add new role
        [Authorize(Policy = "Permissions.Create.Accounts")]
        [HttpPost("Add")]
        public async Task<IActionResult> AddRole([FromBody] RoleDTO roleDTO)
        {
            if (ModelState.IsValid)
            {
                var response = await _roleService.AddRole(roleDTO.RoleName);
                if (response.IsSuccessed)
                    return Ok(response.Message);
                return BadRequest(response.Message);
            }
            return BadRequest(ModelState);
        }



        // remove role

        [Authorize(Policy = "Permissions.Delete.Accounts")]
        [HttpPost("Remove")]
        public async Task<IActionResult> RemoveRole([FromBody] RoleDTO roleDTO)
        {
            if (ModelState.IsValid)
            {
                var response = await _roleService.RemoveRole(roleDTO.RoleName);
                if (response.IsSuccessed)
                    return Ok(response.Message);
                return BadRequest(response.Message);
            }
            return BadRequest(ModelState);
        }


        // add Add User to Role
        [Authorize(Policy = "Permissions.Update.Accounts")]
        [HttpPost("Users/Add")]
        public async Task<IActionResult> AddUserRole([FromBody] AddUserToRoleDTO roleDTO)
        {
            if (ModelState.IsValid)
            {
                var response = await _roleService.AddToRoleAsync(roleDTO);
                if (response.IsSuccessed)
                    return Ok(response.Message);
                return BadRequest(response.Message);
            }
            return BadRequest(ModelState);
        }

        // Remove User from Role
        [Authorize(Policy = "Permissions.Update.Accounts")]
        [HttpPost("Users/Remove")]
        public async Task<IActionResult> RemoveUserRole([FromBody] AddUserToRoleDTO roleDTO)
        {
            if (ModelState.IsValid)
            {
                var response = await _roleService.RemoveFromRoleAsync(roleDTO);
                if (response.IsSuccessed)
                    return Ok(response.Message);
                return BadRequest(response.Message);
            }
            return BadRequest(ModelState);
        }


        

    }
}
