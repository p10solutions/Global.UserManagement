using Global.UserAudit.Application.Models.Events.Users;
using Global.UserManagement.Application.Contracts.Events;
using MassTransit;

namespace Global.UserManagement.Infra.Events
{
    public class UserProducer : IUserProducer
    {
        readonly IPublishEndpoint _publisher;

        public UserProducer(IPublishEndpoint publisher)
        {
            _publisher = publisher;
        }

        public async Task SendUserInsertedAsync(UserInsertedEvent userInsertedEvent)
        {
            await _publisher.Publish(userInsertedEvent);
        }

        public async Task SendUserUpdatedAsync(UserUpdatedEvent userUpdatedEvent)
        {
            await _publisher.Publish(userUpdatedEvent);
        }
    }
}
