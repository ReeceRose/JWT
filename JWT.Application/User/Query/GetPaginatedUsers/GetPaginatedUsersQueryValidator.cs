using FluentValidation;

namespace JWT.Application.User.Query.GetPaginatedUsers
{
    public class GetPaginatedUsersQueryValidator : AbstractValidator<GetPaginatedUsersQuery>
    {
        public GetPaginatedUsersQueryValidator()
        {
            RuleFor(p => p.PaginationModel)
                .NotNull().WithMessage("Pagination model required");
        }
    }
}
