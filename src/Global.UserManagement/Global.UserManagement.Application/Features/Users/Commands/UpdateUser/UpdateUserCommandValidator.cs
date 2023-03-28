using FluentValidation;

namespace Global.UserManagement.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
            RuleFor(x => x.DateBirth).NotEmpty().WithMessage("DateBirth is required");
            RuleFor(x => x.Profile).NotEmpty().WithMessage("Profile is required");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Name).Length(1, 200).WithMessage("Name only allows up to 200 characters");
        }
    }
}
