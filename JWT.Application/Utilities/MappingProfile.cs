using AutoMapper;
using JWT.Application.User.Command.RegisterUser;
using JWT.Application.User.Query.LoginUser;
using JWT.Domain.Entities;

namespace JWT.Application.Utilities
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterUserCommand, ApplicationUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
            CreateMap<LoginUserQuery, ApplicationUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
        }
    }
}
