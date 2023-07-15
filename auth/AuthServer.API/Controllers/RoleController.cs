using AuthServer.Core.DTOs;
using AuthServer.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuthServer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : BaseController
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync(CreateRoleDto createRoleDto)
        {
            return ActionResultInstance(await _roleService.CreateAsync(createRoleDto));
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetListAsync()
        {
            return ActionResultInstance(await _roleService.GetListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateRoleDto createRoleDto)
        {
            return ActionResultInstance(await _roleService.CreateAsync(createRoleDto));
        }
    }
}
