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
            CreateMap<RegisterUserCommand, ApplicationUserDto>().ForMember(d => d.UserName, o => o.MapFrom(s => s.Email));
//                ..ForMember(m => m.Email, src => src.e)
//                .ForAllOtherMembers(opt => opt.Ignore());
            CreateMap<LoginUserQuery, ApplicationUserDto>().ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}
