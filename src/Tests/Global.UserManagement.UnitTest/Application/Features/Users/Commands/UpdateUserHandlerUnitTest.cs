using AutoFixture;
using Global.UserAudit.Application.Models.Events.Users;
using Global.UserManagement.Application.Contracts.Events;
using Global.UserManagement.Application.Contracts.Notifications;
using Global.UserManagement.Application.Contracts.Repositories;
using Global.UserManagement.Application.Entities;
using Global.UserManagement.Application.Features.Users.Commands.UpdateUser;
using Global.UserManagement.Application.Models.Notifications;
using Microsoft.Extensions.Logging;
using Moq;

namespace Global.UserManagement.UnitTest.Application.Features.Users.Commands
{
    public class UpdateUserHandlerUnitTest
    {
        readonly Mock<IUserRepository> _userRepository;
        readonly Mock<IUserProducer> _userProducer;
        readonly Mock<ILogger<UpdateUserHandler>> _logger;
        readonly Mock<INotificationsHandler> _notificationsHandler;
        readonly Fixture _fixture;
        readonly UpdateUserHandler _handler;

        public UpdateUserHandlerUnitTest()
        {
            _userRepository = new Mock<IUserRepository>();
            _userProducer = new Mock<IUserProducer>();
            _logger = new Mock<ILogger<UpdateUserHandler>>();
            _notificationsHandler = new Mock<INotificationsHandler>();
            _fixture = new Fixture();
            _handler = new UpdateUserHandler(_userRepository.Object, _logger.Object, _notificationsHandler.Object, _userProducer.Object);
        }

        [Fact]
        public async Task Task_Should_Be_Updated_Successfully_When_All_Information_Has_Been_Submitted()
        {
            var taskCommand = _fixture.Create<UpdateUserCommand>();

            var response = await _handler.Handle(taskCommand, CancellationToken.None);

            Assert.False(Guid.Empty == response);
            _userRepository.Verify(x => x.UpdateAsync(It.IsAny<User>()), Times.Once);
            _userProducer.Verify(x => x.SendUserUpdatedAsync(It.IsAny<UserUpdatedEvent>()), Times.Once);
        }

        [Fact]
        public async Task Task_Should_Not_Be_Updated_When_An_Exception_Was_Thrown()
        {
            var taskCommand = _fixture.Create<UpdateUserCommand>();
            _userRepository.Setup(x => x.UpdateAsync(It.IsAny<User>())).Throws(new Exception());
            _notificationsHandler
                .Setup(x => x.AddNotification(It.IsAny<string>(), It.IsAny<ENotificationType>(), It.IsAny<object>()))
                    .Returns(_notificationsHandler.Object);
            _notificationsHandler.Setup(x => x.ReturnDefault<Guid>()).Returns(Guid.Empty);

            var response = await _handler.Handle(taskCommand, CancellationToken.None);

            Assert.True(Guid.Empty == response);
            _userRepository.Verify(x => x.UpdateAsync(It.IsAny<User>()), Times.Once);
            _userProducer.Verify(x => x.SendUserUpdatedAsync(It.IsAny<UserUpdatedEvent>()), Times.Never);
        }
    }
}