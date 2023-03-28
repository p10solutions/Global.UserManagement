using Global.UserManagement.Application.Entities;

namespace Global.UserManagement.Application.Features.Users.Queries.GetUser
{
    public class GetUserMapper
    {
        public static GetUserResponse MapFrom(User user)
            => new(user.Id, user.Name, user.DateBirth, user.Profile, user.Active);

        public static IEnumerable<GetUserResponse> MapFrom(IEnumerable<User> taskItems)
            => taskItems.Select(x => MapFrom(x));
    }
}
