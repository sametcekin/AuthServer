﻿using AuthServer.Core.DTOs;
using AuthServer.Core.Entities;
using AuthServer.Core.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Dtos;
using SharedLibrary.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.Service.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<RoleApp> _roleManager;
        public RoleService(RoleManager<RoleApp> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<Response<NoContent>> CreateAsync(CreateRoleDto createRoleDto)
        {
            var result = await _roleManager.CreateAsync(new RoleApp { Name = createRoleDto.Name });
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList();
                return Response<NoContent>.Fail(new ErrorDto(errors, true), 400);
            }
            return Response<NoContent>.Success(200);
        }

        public async Task<Response<List<RoleAppDto>>> GetListAsync()
        {
            var roleList = await _roleManager.Roles.ToListAsync();
            return Response<List<RoleAppDto>>.Success(ObjectMapper.Mapper.Map<List<RoleAppDto>>(roleList), 200);
        }
    }
}