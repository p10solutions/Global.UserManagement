using AutoFixture;
using Global.UserManagement.Application.Contracts.Notifications;
using Global.UserManagement.Application.Contracts.Repositories;
using Global.UserManagement.Application.Entities;
using Global.UserManagement.Application.Features.Users.Queries.GetUserById;
using Global.UserManagement.Application.Models.Notifications;
using Microsoft.Extensions.Logging;
using Moq;

namespace Global.UserManagement.UnitTest.Application.Features.Users.Queries
{
    public class GetUserByIdUnitTest
    {
        readonly Mock<IUserRepository> _userRepository;
        readonly Mock<ILogger<GetUserByIdHandler>> _logger;
        readonly Mock<INotificationsHandler> _notificationsHandler;
        readonly Fixture _fixture;
        readonly GetUserByIdHandler _handler;

        public GetUserByIdUnitTest()
        {
            _userRepository = new Mock<IUserRepository>();
            _logger = new Mock<ILogger<GetUserByIdHandler>>();
            _notificationsHandler = new Mock<INotificationsHandler>();
            _fixture = new Fixture();
            _handler = new GetUserByIdHandler(_userRepository.Object, _logger.Object, _notificationsHandler.Object);
        }

        [Fact]
        public async Task Task_Should_Be_Geted_Successfully_When_All_Information_Has_Been_Submitted()
        {
            var taskQuery = _fixture.Create<GetUserByIdQuery>();
            var task = _fixture.Create<User>();
            _userRepository.Setup(x => x.GetAsync(It.IsAny<Guid>())).ReturnsAsync(task);

            var response = await _handler.Handle(taskQuery, CancellationToken.None);

            Assert.NotNull(response);
            _userRepository.Verify(x => x.GetAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task Task_Should_Not_Be_Geted_When_An_Exception_Was_Thrown()
        {
            var taskQuery = _fixture.Create<GetUserByIdQuery>();
            _userRepository.Setup(x => x.GetAsync(It.IsAny<Guid>())).Throws(new Exception());
            _notificationsHandler
                .Setup(x => x.AddNotification(It.IsAny<string>(), It.IsAny<ENotificationType>(), It.IsAny<object>()))
                    .Returns(_notificationsHandler.Object);
            _notificationsHandler.Setup(x => x.ReturnDefault<Guid>()).Returns(Guid.Empty);

            var response = await _handler.Handle(taskQuery, CancellationToken.None);

            Assert.Null(response);
            _userRepository.Verify(x => x.GetAsync(It.IsAny<Guid>()), Times.Once);
        }
    }
}