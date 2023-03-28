using Global.UserManagement.Application.Contracts.Notifications;
using Global.UserManagement.Application.Contracts.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using AutoFixture;
using Global.UserManagement.Application.Entities;
using Global.UserManagement.Application.Models.Notifications;
using Global.UserManagement.Application.Features.Users.Commands.CreateUser;
using Global.UserManagement.Application.Contracts.Events;

namespace Global.UserManagement.UnitTest.Application.Features.Users.Commands
{
    public class CreateUserUnitTest
    {
        readonly Mock<IUserRepository> _userRepository;
        readonly Mock<IUserProducer> _userProducer;
        readonly Mock<ILogger<CreateUserHandler>> _logger;
        readonly Mock<INotificationsHandler> _notificationsHandler;
        readonly Fixture _fixture;
        readonly CreateUserHandler _handler;

        public CreateUserUnitTest()
        {
            _userRepository = new Mock<IUserRepository>();
            _userProducer = new Mock<IUserProducer>();
            _logger = new Mock<ILogger<CreateUserHandler>>();
            _notificationsHandler = new Mock<INotificationsHandler>();
            _fixture = new Fixture();
            _handler = new CreateUserHandler(_userRepository.Object, _logger.Object, _notificationsHandler.Object, _userProducer.Object);
        }

        [Fact]
        public async Task Task_Should_Be_Created_Successfully_When_All_Information_Has_Been_Submitted()
        {
            var taskCommand = _fixture.Create<CreateUserCommand>();

            var response = await _handler.Handle(taskCommand, CancellationToken.None);

            Assert.False(Guid.Empty == response);
            _userRepository.Verify(x => x.AddAsync(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task Task_Should_Not_Be_Created_When_An_Exception_Was_Thrown()
        {
            var taskCommand = new CreateUserCommand(string.Empty, DateTime.Now, EProfile.Administrator);
            _userRepository.Setup(x => x.AddAsync(It.IsAny<User>())).Throws(new Exception());
            _notificationsHandler
                .Setup(x => x.AddNotification(It.IsAny<string>(), It.IsAny<ENotificationType>(), It.IsAny<object>()))
                    .Returns(_notificationsHandler.Object);
            _notificationsHandler.Setup(x => x.ReturnDefault<Guid>()).Returns(Guid.Empty);

            var response = await _handler.Handle(taskCommand, CancellationToken.None);

            Assert.True(Guid.Empty == response);
            _userRepository.Verify(x => x.AddAsync(It.IsAny<User>()), Times.Once);
        }
    }
}