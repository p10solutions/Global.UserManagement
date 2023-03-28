using Global.UserManagement.Application.Entities;

namespace Global.UserManagement.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommandMapper
    {
        public static User MapTo(UpdateUserCommand command)
            => new(command.Name, command.DateBirth, command.Profile) { Id = command.Id, Active = command.Active };
    }
}
