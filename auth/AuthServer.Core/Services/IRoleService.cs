using AuthServer.Core.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using SharedLibrary.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthServer.Core.Services
{
    public interface IRoleService
    {
        Task<Response<NoContent>> CreateAsync(CreateRoleDto createRoleDto);
        Task<Response<NoContent>> DeleteAsync(Guid roleId);
        Task<Response<List<RoleAppDto>>> GetListAsync();

    }
}
