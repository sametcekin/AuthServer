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
    public class UserService : IUserService
    {
        private readonly UserManager<UserApp> _userManager;
        private readonly RoleManager<RoleApp> _roleManager;
        private readonly ICacheService _cacheService;
        public UserService(UserManager<UserApp> userManager, ICacheService cacheService, RoleManager<RoleApp> roleManager)
        {
            _userManager = userManager;
            _cacheService = cacheService;
            _roleManager = roleManager;
        }

        public async Task<Response<UserAppDto>> CreateUserAsync(CreateUserDto createUserDto)
        {
            var user = new UserApp
            {
                Email = createUserDto.Email,
                UserName = createUserDto.UserName,
            };

            var result = await _userManager.CreateAsync(user, createUserDto.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList();

                return Response<UserAppDto>.Fail(new ErrorDto(errors, true), 400);
            }

            return Response<UserAppDto>.Success(ObjectMapper.Mapper.Map<UserAppDto>(user), 200);
        }

        public async Task<Response<NoContent>> CreateUserRoles(Guid userId, CreateUserRoleDto createUserRoleDto)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null)
                return Response<NoContent>.Fail("User not found", 404, true);
            var userRoles = await _userManager.GetRolesAsync(user);

            var roles = _roleManager.Roles.Where(x => createUserRoleDto.RoleIds.Any(a => a == x.Id)).Select(a => a.Name).ToList();
            if (roles is null)
                return Response<NoContent>.Fail("Role not found", 404, true);

            var addedRoles = roles.Except(userRoles);
            var addRoleResult = await _userManager.AddToRolesAsync(user, addedRoles);

            return Response<NoContent>.Success(200);
        }

        public async Task<Response<UserAppDto>> GetUserByNameAsync(string userName)
        {
            var user = await _cacheService.GetOrSetAsync("user", async () => { return await _userManager.FindByNameAsync(userName); });
            if (user is null)
                return Response<UserAppDto>.Fail("Username not found", 404, true);

            return Response<UserAppDto>.Success(ObjectMapper.Mapper.Map<UserAppDto>(user), 200);
        }

        public async Task<Response<List<UserAppDto>>> GetListAsync()
        {
            var userList = await _userManager.Users.ToListAsync();
            return Response<List<UserAppDto>>.Success(ObjectMapper.Mapper.Map<List<UserAppDto>>(userList), 200);
        }
    }
}
