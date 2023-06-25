using AuthServer.Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.Dtos;
using System.Threading.Tasks;
namespace AuthServer.Core.Services
{
    public interface IAuthenticationService
    {
        Task<Response<TokenDto>> CreateToken(LoginDto loginDto);
        Task<Response<TokenDto>> CreateTokenByRefreshToken(string refreshToken);
        Task<Response<NoContentResult>> RevokeRefreshToken(string refreshToken);
        Response<ClientTokenDto> CreateTokenByClient(ClientLoginDto clientLoginDto);
    }
}
