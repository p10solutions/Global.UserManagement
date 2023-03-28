using AutoFixture;
using Global.UserManagement.Application.Contracts.Notifications;
using Global.UserManagement.Application.Contracts.Repositories;
using Global.UserManagement.Application.Entities;
using Global.UserManagement.Application.Features.Users.Queries.GetUser;
using Global.UserManagement.Application.Models.Notifications;
using Microsoft.Extensions.Logging;
using Moq;

namespace Global.UserManagement.UnitTest.Application.Features.Users.Queries
{
    public class GetUserUnitTest
    {
        readonly Mock<IUserRepository> _userRepository;
        readonly Mock<ILogger<GetUserHandler>> _logger;
        readonly Mock<INotificationsHandler> _notificationsHandler;
        readonly Fixture _fixture;
        readonly GetUserHandler _handler;

        public GetUserUnitTest()
        {
            _userRepository = new Mock<IUserRepository>();
            _logger = new Mock<ILogger<GetUserHandler>>();
            _notificationsHandler = new Mock<INotificationsHandler>();
            _fixture = new Fixture();
            _handler = new GetUserHandler(_userRepository.Object, _logger.Object, _notificationsHandler.Object);
        }

        [Fact]
        public async Task Task_Should_Be_Geted_Successfully_When_All_Information_Has_Been_Submitted()
        {
            var taskQuery = _fixture.Create<GetUserQuery>();
            var tasks = _fixture.CreateMany<User>();
            _userRepository.Setup(x => x.GetAsync()).ReturnsAsync(tasks);

            var response = await _handler.Handle(taskQuery, CancellationToken.None);

            Assert.NotNull(response);
            _userRepository.Verify(x => x.GetAsync(), Times.Once);
        }

        [Fact]
        public async Task Task_Should_Not_Be_Geted_When_An_Exception_Was_Thrown()
        {
            var taskQuery = _fixture.Create<GetUserQuery>();
            _userRepository.Setup(x => x.GetAsync()).Throws(new Exception());
            _notificationsHandler
                .Setup(x => x.AddNotification(It.IsAny<string>(), It.IsAny<ENotificationType>(), It.IsAny<object>()))
                    .Returns(_notificationsHandler.Object);
            _notificationsHandler.Setup(x => x.ReturnDefault<Guid>()).Returns(Guid.Empty);

            var response = await _handler.Handle(taskQuery, CancellationToken.None);

            Assert.Empty(response);
            _userRepository.Verify(x => x.GetAsync(), Times.Once);
        }
    }
}