using Global.UserManagement.Application.Entities;

namespace Global.UserManagement.Application.Contracts.Repositories
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task<User> GetAsync(Guid id);
        Task<IEnumerable<User>> GetAsync();
    }
}
