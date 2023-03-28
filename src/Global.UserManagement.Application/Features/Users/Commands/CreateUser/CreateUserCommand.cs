using Global.UserManagement.Application.Entities;
using Global.UserManagement.Application.Features.Common;
using MediatR;

namespace Global.UserManagement.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommand : CommandBase<CreateUserCommand>, IRequest<Guid>
    {
        public string Name { get; set; }
        public DateTime DateBirth { get; set; }
        public EProfile Profile { get; set; }

        public CreateUserCommand(string name, DateTime dateBirth, EProfile profile)
            : base(new CreateUserCommandValidator())
        {
            Name = name;
            DateBirth = dateBirth;
            Profile = profile;
        }
    }
}
