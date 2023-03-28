using Global.UserAudit.Application.Models.Events.Users;
using Global.UserManagement.Application.Entities;

namespace Global.UserManagement.Application.Models.Events.Users.Maps
{
    public static class UserUpdatedMapper
    {
        public static UserUpdatedEvent MapFrom(User user)
            => new(user.Id, user.Name, user.DateBirth, user.Profile, user.Active);
    }
}
