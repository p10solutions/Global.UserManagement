using AutoFixture;
using Global.UserManagement.Application.Features.Users.Queries.GetUserById;

namespace Global.UserManagement.UnitTest.Application.Features.Users.Queries
{
    public class GetUserByIdQueryUnitTest
    {
        readonly Fixture _fixture;

        public GetUserByIdQueryUnitTest()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void Command_Should_Be_Valid()
        {
            var command = _fixture.Create<GetUserByIdQuery>();

            var result = command.Validate();

            Assert.True(result);
        }

        [Fact]
        public void Command_Should_Be_Invalid()
        {
            var command = new GetUserByIdQuery(Guid.Empty);

            var result = command.Validate();

            Assert.False(result);
        }
    }
}
