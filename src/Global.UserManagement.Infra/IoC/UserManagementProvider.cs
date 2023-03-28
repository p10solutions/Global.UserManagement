using Global.UserManagement.Application.Contracts.Events;
using Global.UserManagement.Application.Contracts.Notifications;
using Global.UserManagement.Application.Contracts.Repositories;
using Global.UserManagement.Application.Features.Users.Commands.CreateUser;
using Global.UserManagement.Application.Features.Users.Commands.UpdateUser;
using Global.UserManagement.Application.Features.Users.Queries.GetUser;
using Global.UserManagement.Application.Features.Users.Queries.GetUserById;
using Global.UserManagement.Infra.Data;
using Global.UserManagement.Infra.Events;
using Global.UserManagement.Infra.Validation;
using GlobalUserManagement.Infra.Data.Repositories;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Global.UserManagement.Infra.IoC
{
    public static class UserManagementProvider
    {
        public static IServiceCollection AddProviders(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("UserManagement");
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetUserByIdHandler).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetUserHandler).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateUserCommand).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(UpdateUserHandler).Assembly));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(FailFastValidator<,>));
            services.AddScoped<INotificationsHandler, NotificationHandler>();
            services.AddDbContextPool<UserManagementContext>(opt => opt.UseSqlServer(connectionString));
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserProducer, UserProducer>();
            
            services.AddMassTransit(bus =>
            {
                bus.UsingRabbitMq((ctx, busConfigurator) =>
                {
                    busConfigurator.Host(configuration.GetConnectionString("RabbitMq"));
                });
            });

            return services;
        }
    }
}
