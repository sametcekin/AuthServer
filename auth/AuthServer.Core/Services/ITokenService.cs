using AuthServer.Core.Configuration;
using AuthServer.Core.DTOs;
using AuthServer.Core.Entities;
using System.Threading.Tasks;

namespace AuthServer.Core.Services
{
    public interface ITokenService
    {
        Task<TokenDto> CreateTokenAsync(UserApp userApp);
        ClientTokenDto CreateTokenByClient(Client client);
    }
}
