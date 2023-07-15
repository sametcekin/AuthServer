using AuthServer.Core.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using SharedLibrary.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthServer.Core.Services
{
    public interface IUserService
    {
        Task<Response<UserAppDto>> CreateUserAsync(CreateUserDto createUserDto);
        Task<Response<NoContent>> CreateUserRoles(Guid userId, CreateUserRoleDto createUserRoleDto);
        Task<Response<UserAppDto>> GetUserByNameAsync(string userName);
        Task<Response<List<UserAppDto>>> GetListAsync();
    }
}
