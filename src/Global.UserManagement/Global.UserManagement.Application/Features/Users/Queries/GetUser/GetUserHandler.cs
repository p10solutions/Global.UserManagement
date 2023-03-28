using Global.UserManagement.Application.Contracts.Notifications;
using Global.UserManagement.Application.Contracts.Repositories;
using Global.UserManagement.Application.Models.Notifications;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Global.UserManagement.Application.Features.Users.Queries.GetUser
{
    public class GetUserHandler : IRequestHandler<GetUserQuery, IEnumerable<GetUserResponse>>
    {
        readonly IUserRepository _userRepository;
        readonly ILogger<GetUserHandler> _logger;
        readonly INotificationsHandler _notificationsHandler;

        public GetUserHandler(IUserRepository userRepository, ILogger<GetUserHandler> logger, INotificationsHandler notificationsHandler)
        {
            _userRepository = userRepository;
            _logger = logger;
            _notificationsHandler = notificationsHandler;
        }

        public async Task<IEnumerable<GetUserResponse>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var users = await _userRepository.GetAsync();
                var response = GetUserMapper.MapFrom(users);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred when trying to get the user: {exception}", ex.Message);

                return _notificationsHandler
                        .AddNotification("An error occurred when trying to get the user", ENotificationType.InternalError)
                        .ReturnDefault<IEnumerable<GetUserResponse>>();
            }
        }
    }
}
