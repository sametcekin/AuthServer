﻿using AuthServer.Core.Configuration;
using AuthServer.Core.DTOs;
using AuthServer.Core.Entities;
using AuthServer.Core.Repositories;
using AuthServer.Core.Services;
using AuthServer.Core.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SharedLibrary.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.Service.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly List<Client> _clients;
        private readonly ITokenService _tokenService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<UserRefreshToken> _userRefrehTokenRepository;

        public AuthenticationService(IOptions<List<Client>> clients,
                                     ITokenService tokenService,
                                     UserManager<ApplicationUser> userManager,
                                     IUnitOfWork unitOfWork,
                                     IGenericRepository<UserRefreshToken> userRefrehTokenRepository)
        {
            _clients = clients.Value;
            _tokenService = tokenService;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _userRefrehTokenRepository = userRefrehTokenRepository;
        }

        public async Task<Response<TokenDto>> CreateToken(LoginDto loginDto)
        {
            if (loginDto is null) throw new ArgumentNullException();

            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user is null) return Response<TokenDto>.Fail("Email or password is wrong", 400, true);

            if (!await _userManager.CheckPasswordAsync(user, loginDto.Password))
                return Response<TokenDto>.Fail("Email or passwrod is wrong", 400, true);

            var token = await _tokenService.CreateTokenAsync(user);

            var userRefreshToken = await _userRefrehTokenRepository.Where(x => x.UserId == user.Id).SingleOrDefaultAsync();

            if (userRefreshToken is null)
                await _userRefrehTokenRepository.AddAsync(new UserRefreshToken { UserId = user.Id, Value = token.RefreshToken, Expiration = token.RefreshTokenExpiration });
            else
            {
                userRefreshToken.Value = token.RefreshToken;
                userRefreshToken.Expiration = token.RefreshTokenExpiration;
            }

            await _unitOfWork.CommitAsync();

            return Response<TokenDto>.Success(token, 200);
        }

        public Response<ClientTokenDto> CreateTokenByClient(ClientLoginDto clientLoginDto)
        {
            var client = _clients.SingleOrDefault(x => x.Id == clientLoginDto.ClientId && x.Secret == clientLoginDto.ClientSecret);

            if (client is null)
                return Response<ClientTokenDto>.Fail("ClientId or Client Secret not found", 404, true);

            var token = _tokenService.CreateTokenByClient(client);

            return Response<ClientTokenDto>.Success(token, 200);
        }

        public async Task<Response<TokenDto>> CreateTokenByRefreshToken(string refreshToken)
        {
            var existRefreshToken = await _userRefrehTokenRepository.Where(x => x.Value == refreshToken).FirstOrDefaultAsync();

            if (existRefreshToken is null)
                return Response<TokenDto>.Fail("Refresht token not found", 404, true);

            var user = await _userManager.FindByIdAsync(existRefreshToken.UserId.ToString());

            if (user is null)
                return Response<TokenDto>.Fail("User id not found", 404, true);

            var tokenDto = await _tokenService.CreateTokenAsync(user);

            existRefreshToken.Value = tokenDto.RefreshToken;
            existRefreshToken.Expiration = tokenDto.RefreshTokenExpiration;

            await _unitOfWork.CommitAsync();

            return Response<TokenDto>.Success(tokenDto, 200);
        }

        public async Task<Response<NoContentResult>> RevokeRefreshToken(string refreshToken)
        {
            var existRefreshToken = await _userRefrehTokenRepository.Where(x => x.Value == refreshToken).FirstOrDefaultAsync();

            if (existRefreshToken is null)
                return Response<NoContentResult>.Fail("Refresh token not found", 404, true);

            _userRefrehTokenRepository.Remove(existRefreshToken);

            await _unitOfWork.CommitAsync();

            return Response<NoContentResult>.Success(204);
        }
    }
}
