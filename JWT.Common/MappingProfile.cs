using AutoMapper;
using JWT.Application.Users.Models;
using Microsoft.AspNetCore.Identity;

namespace JWT.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<IdentityUser, ApplicationUserDto>();
        }
    }
}
