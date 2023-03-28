using Global.UserManagement.Application.Entities;
using Global.UserManagement.Application.Features.Common;
using MediatR;

namespace Global.UserManagement.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommand : CommandBase<UpdateUserCommand>, IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DateBirth { get; set; }
        public EProfile Profile { get; set; }
        public bool Active { get; set; }

        public UpdateUserCommand(Guid id, string name, DateTime dateBirth, EProfile profile, bool active)
            : base(new UpdateUserCommandValidator())
        {
            Id = id;
            Name = name;
            DateBirth = dateBirth;
            Profile = profile;
            Active = active;
        }
    }
}
