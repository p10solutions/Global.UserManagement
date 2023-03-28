using Global.UserManagement.Application.Contracts.Repositories;
using Global.UserManagement.Application.Entities;
using Global.UserManagement.Infra.Data;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Dapper;

namespace GlobalUserManagement.Infra.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        readonly UserManagementContext _context;
        readonly IDbConnection _connection;

        const string insertSql = @"
            INSERT INTO [User]
            (Id, Profile, Name, DateBirth, Active)
            VALUES
            (@Id, @Profile, @Name, @DateBirth, @Active)
        ";

        const string updateSql = @"
           Update [User]
            Set  
                Profile = @Profile, 
                Name = @Name, 
                DateBirth = @DateBirth,
                Active = @Active    
            Where Id = @Id
        ";

        const string querySql = @"
            Select 
                Id, Profile, Name, DateBirth, Active
            From [User]
            Where Active = 1
        ";

        const string queryByIdSql = @"
            Select 
                Id, Profile, Name, DateBirth, Active
            From [User]
            Where Id = @id
        ";

        public UserRepository(UserManagementContext context)
        {
            _context = context;
            _connection = _context.Database.GetDbConnection();
        }

        public async Task AddAsync(User user)
        {
            await _connection.ExecuteAsync(insertSql, user);
        }

        public async Task<IEnumerable<User>> GetAsync()
        {
            return await _connection.QueryAsync<User>(querySql);
        }

        public async Task<User> GetAsync(Guid id)
        {
            return await _connection.QueryFirstAsync<User>(queryByIdSql, new { id });
        }

        public async Task UpdateAsync(User user)
        {
           await _connection.ExecuteAsync(updateSql, user);
        }
    }
}
