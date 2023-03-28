using FluentValidation;

namespace Global.UserManagement.Application.Features.Users.Queries.GetUser
{
    public class GetUserQueryValidator : AbstractValidator<GetUserQuery>
    {
        public GetUserQueryValidator()
        {
        }
    }
}
