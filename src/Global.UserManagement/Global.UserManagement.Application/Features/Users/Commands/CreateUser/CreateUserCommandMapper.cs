using Global.UserManagement.Application.Entities;

namespace Global.UserManagement.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandMapper
    {
        public static User MapTo(CreateUserCommand command)
            => new(command.Name, command.DateBirth, command.Profile);
    }
}
