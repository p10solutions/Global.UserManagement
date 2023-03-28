using Global.UserManagement.Application.Contracts.Notifications;
using Global.UserManagement.Application.Contracts.Repositories;
using Global.UserManagement.Application.Models.Notifications;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Global.UserManagement.Application.Features.Users.Queries.GetUserById
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, GetUserByIdResponse>
    {
        readonly IUserRepository _userRepository;
        readonly ILogger<GetUserByIdHandler> _logger;
        readonly INotificationsHandler _notificationsHandler;

        public GetUserByIdHandler(IUserRepository userRepository, ILogger<GetUserByIdHandler> logger, INotificationsHandler notificationsHandler)
        {
            _userRepository = userRepository;
            _logger = logger;
            _notificationsHandler = notificationsHandler;
        }

        public async Task<GetUserByIdResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetAsync(request.Id);

                var response = GetUserByIdMapper.MapFrom(user);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred when trying to get the user: {exception}", ex.Message);

                return _notificationsHandler
                        .AddNotification("An error occurred when trying to get the user", ENotificationType.InternalError)
                        .ReturnDefault<GetUserByIdResponse>();
            }
        }
    }
}
