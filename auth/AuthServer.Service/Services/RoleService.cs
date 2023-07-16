using AuthServer.Core.DTOs;
using AuthServer.Core.Entities;
using AuthServer.Core.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RedisCache.Services;
using SharedLibrary.Dtos;
using SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.Service.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<RoleApp> _roleManager;
        private readonly ICacheService _cacheService;
        public RoleService(RoleManager<RoleApp> roleManager, ICacheService cacheService)
        {
            _roleManager = roleManager;
            _cacheService = cacheService;
        }

        public async Task<Response<NoContent>> CreateAsync(CreateRoleDto createRoleDto)
        {
            var result = await _roleManager.CreateAsync(new RoleApp { Name = createRoleDto.Name });
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList();
                return Response<NoContent>.Fail(new ErrorDto(errors, true), 400);
            }
            await _cacheService.SetValueAsync("roles", async () => { return await _roleManager.Roles.ToListAsync(); });
            return Response<NoContent>.Success(200);
        }

        public async Task<Response<NoContent>> DeleteAsync(Guid roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role is null)
                return Response<NoContent>.Fail("Role not found", 400, true);
            var result = await _roleManager.DeleteAsync(role);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList();
                return Response<NoContent>.Fail(new ErrorDto(errors, true), 400);
            }
            await _cacheService.SetValueAsync("roles", async () => { return await _roleManager.Roles.ToListAsync(); });
            return Response<NoContent>.Success(200);
        }

        public async Task<Response<List<RoleAppDto>>> GetListAsync()
        {
            var roles = await _cacheService.GetOrSetAsync("roles", async () => { return await _roleManager.Roles.ToListAsync(); });
            return Response<List<RoleAppDto>>.Success(ObjectMapper.Mapper.Map<List<RoleAppDto>>(roles), 200);
        }
    }
}
