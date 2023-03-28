using AutoFixture;
using Global.UserManagement.Application.Features.Users.Commands.UpdateUser;

namespace Global.UserManagement.UnitTest.Application.Features.Users.Commands
{
    public class UpdateUserCommandUnitTest
    {
        readonly Fixture _fixture;

        public UpdateUserCommandUnitTest()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void Command_Should_Be_Valid()
        {
            var command = _fixture.Create<UpdateUserCommand>();

            var result = command.Validate();

            Assert.True(result);
        }

        [Fact]
        public void Command_Should_Be_Invalid()
        {
            var command = new UpdateUserCommand(Guid.Empty, string.Empty, DateTime.Now, 
                UserManagement.Application.Entities.EProfile.Administrator, true);

            var result = command.Validate();

            Assert.False(result);
        }
    }
}
