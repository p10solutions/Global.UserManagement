using Global.UserManagement.Application.Entities;

namespace Global.UserManagement.Application.Features.Users.Queries.GetUserById
{
    public class GetUserByIdMapper
    {
        public static GetUserByIdResponse MapFrom(User user)
            => new(user.Id, user.Name, user.DateBirth, user.Profile, user.Active);
    }
}
