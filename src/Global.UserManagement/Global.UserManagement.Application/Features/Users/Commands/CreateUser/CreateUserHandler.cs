using Global.UserManagement.Application.Contracts.Events;
using Global.UserManagement.Application.Contracts.Notifications;
using Global.UserManagement.Application.Contracts.Repositories;
using Global.UserManagement.Application.Models.Events.Users.Maps;
using Global.UserManagement.Application.Models.Notifications;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Global.UserManagement.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        readonly IUserRepository _userRepository;
        readonly IUserProducer _userProducer;
        readonly ILogger<CreateUserHandler> _logger;
        readonly INotificationsHandler _notificationsHandler;

        public CreateUserHandler(IUserRepository userRepository, ILogger<CreateUserHandler> logger, 
            INotificationsHandler notificationsHandler, IUserProducer userProducer)
        {
            _userRepository = userRepository;
            _logger = logger;
            _notificationsHandler = notificationsHandler;
            _userProducer = userProducer;
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = CreateUserCommandMapper.MapTo(request);

            try
            {
                await _userRepository.AddAsync(user);
                var userInsertedEvent = UserInsertedMapper.MapFrom(user);
                await _userProducer.SendUserInsertedAsync(userInsertedEvent);       

                return user.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred when trying to create the user: {exception}", ex.Message);
                return _notificationsHandler
                        .AddNotification("An error occurred when trying to create a new user", ENotificationType.InternalError)
                        .ReturnDefault<Guid>();
            }
        }
    }
}
