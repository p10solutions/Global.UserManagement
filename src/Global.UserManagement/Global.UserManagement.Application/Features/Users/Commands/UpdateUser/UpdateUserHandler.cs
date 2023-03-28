using Global.UserManagement.Application.Contracts.Events;
using Global.UserManagement.Application.Contracts.Notifications;
using Global.UserManagement.Application.Contracts.Repositories;
using Global.UserManagement.Application.Models.Events.Users.Maps;
using Global.UserManagement.Application.Models.Notifications;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Global.UserManagement.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, Guid>
    {
        readonly IUserRepository _userRepository;
        readonly IUserProducer _userProducer;
        readonly ILogger<UpdateUserHandler> _logger;
        readonly INotificationsHandler _notificationsHandler;

        public UpdateUserHandler(IUserRepository userRepository, ILogger<UpdateUserHandler> logger, 
            INotificationsHandler notificationsHandler, IUserProducer userProducer)
        {
            _userRepository = userRepository;
            _logger = logger;
            _notificationsHandler = notificationsHandler;
            _userProducer = userProducer;
        }

        public async Task<Guid> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = UpdateUserCommandMapper.MapTo(request);

            try
            {
                await _userRepository.UpdateAsync(user);
                var userUpdatedEvent = UserUpdatedMapper.MapFrom(user);
                await _userProducer.SendUserUpdatedAsync(userUpdatedEvent);

                return user.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred when trying to Update the user: {exception}", ex.Message);
                return _notificationsHandler
                        .AddNotification("An error occurred when trying to Update the user", ENotificationType.InternalError)
                        .ReturnDefault<Guid>();
            }
        }
    }
}
