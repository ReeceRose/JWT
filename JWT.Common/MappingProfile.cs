using AutoMapper;
using JWT.Application.Users.Commands.RegisterUser;
using Microsoft.AspNetCore.Identity;

namespace JWT.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterUserCommand, IdentityUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
        }
    }
}
