using System.Collections.Generic;
using JWT.Domain.Entities;

namespace JWT.Application.User.Model
{
    public class PaginatedUsersDto
    {
        public List<ApplicationUserDto> Users { get; set; }
        public PaginationModel PaginationModel { get; set; }
    }
}
