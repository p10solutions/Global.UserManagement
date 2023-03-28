using FluentValidation;

namespace Global.UserManagement.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.DateBirth).NotEmpty().WithMessage("DateBirth is required");
            RuleFor(x => x.Profile).NotEmpty().WithMessage("Profile is required");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Name).Length(2, 200).WithMessage("Name only allows up to 200 characters");
        }
    }
}
