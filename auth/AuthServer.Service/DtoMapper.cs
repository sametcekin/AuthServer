using AuthServer.Core.DTOs;
using AuthServer.Core.Entities;
using AutoMapper;

namespace AuthServer.Service
{
    internal class DtoMapper : Profile
    {
        public DtoMapper()
        {
            CreateMap<UserAppDto, UserApp>().ReverseMap();
            CreateMap<RoleAppDto, RoleApp>().ReverseMap();
        }
    }
}
