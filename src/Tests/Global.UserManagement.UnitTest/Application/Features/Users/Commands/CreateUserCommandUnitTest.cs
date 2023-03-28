using Global.UserManagement.Application.Features.Users.Commands.CreateUser;
using AutoFixture;

namespace Global.UserManagement.UnitTest.Application.Features.Users.Commands
{
    public class CreateUserCommandUnitTest
    {
        readonly Fixture _fixture;

        public CreateUserCommandUnitTest()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void Command_Should_Be_Valid()
        {
            var command = _fixture.Create<CreateUserCommand>();

            var result = command.Validate();

            Assert.True(result);
        }

        [Fact]
        public void Command_Should_Be_Invalid()
        {
            var command = new CreateUserCommand(string.Empty, DateTime.Now, 
                UserManagement.Application.Entities.EProfile.Administrator);

            var result = command.Validate();

            Assert.False(result);
        }
    }
}
