using AutoMapper;
using JWT.Application.User.Command.RegisterUser;
using JWT.Application.User.Model;
using JWT.Application.User.Query.LoginUser;
using JWT.Domain.Entities;

namespace JWT.Application.Utilities
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, ApplicationUserDto>();

            CreateMap<ApplicationUserDto, ApplicationUser>();
        }
    }
}
