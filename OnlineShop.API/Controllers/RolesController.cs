using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Core.DTOs.AuthDTOs;
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

        [Authorize(Roles= "Basic, Admin, SuperAdmin")]
        [HttpGet("")]
        public async Task<IActionResult> AllRoles()
        {
            return Ok(await _roleService.AllRoles());
        }



        // add new role
        [Authorize(Roles = "SuperAdmin")]
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

        [Authorize(Roles = "SuperAdmin")]
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
        [Authorize(Roles = "SuperAdmin")]
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
        [Authorize(Roles = "SuperAdmin")]
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
