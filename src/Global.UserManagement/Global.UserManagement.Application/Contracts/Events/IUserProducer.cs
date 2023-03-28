using Global.UserAudit.Application.Models.Events.Users;

namespace Global.UserManagement.Application.Contracts.Events
{
    public interface IUserProducer
    {
        Task SendUserInsertedAsync(UserInsertedEvent userInsertedEvent);
        Task SendUserUpdatedAsync(UserUpdatedEvent userUpdatedEvent);
    }
}
