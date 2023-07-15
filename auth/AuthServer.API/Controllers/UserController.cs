using AuthServer.Core.DTOs;
using AuthServer.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthServer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateUserDto createUserDto)
        {
            return ActionResultInstance(await _userService.CreateUserAsync(createUserDto));
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetListAsync()
        {
            return ActionResultInstance(await _userService.GetListAsync());
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetLoggedUserAsync()
        {
            return ActionResultInstance(await _userService.GetUserByNameAsync(HttpContext.User.Identity.Name));
        }

        [HttpPost("{userId}/roles")]
        public async Task<IActionResult> CreateUserRoles(Guid userId, [FromBody] CreateUserRoleDto createUserRoleDto)
        {
            return ActionResultInstance(await _userService.CreateUserRoles(userId, createUserRoleDto));
        }
    }
}
