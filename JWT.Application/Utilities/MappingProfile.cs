using AutoMapper;
using JWT.Application.User.Command.RegisterUser;
using JWT.Application.User.Query.LoginUser;
using Microsoft.AspNetCore.Identity;

namespace JWT.Application.Utilities
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterUserCommand, IdentityUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
            CreateMap<LoginUserQuery, IdentityUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
        }
    }
}
