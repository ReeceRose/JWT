using AutoMapper;
using Core.Models.Transfer;
using Microsoft.AspNetCore.Identity;

namespace Core.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<IdentityUser, ApplicationUser>();
        }
    }
}
