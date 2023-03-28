using Microsoft.AspNetCore.Mvc;
using MediatR;
using Global.UserManagement.Api.Controllers.Base;
using Global.UserManagement.Application.Contracts.Notifications;
using System.Net;
using Global.UserManagement.Application.Features.Users.Queries.GetUserById;
using Global.UserManagement.Application.Features.Users.Queries.GetUser;
using Global.UserManagement.Application.Features.Users.Commands.UpdateUser;
using Global.UserManagement.Application.Features.Users.Commands.CreateUser;

namespace Global.UserManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ApiControllerBase
    {
        public UserController(IMediator mediator, INotificationsHandler notificationsHandler) : base(mediator, notificationsHandler)
        {
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GetUserResponse>))]
        public async Task<IActionResult> GetAsync()
            => await SendAsync(new GetUserQuery());

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetUserByIdResponse))]
        public async Task<IActionResult> GetAsync(Guid id)
            => await SendAsync(new GetUserByIdQuery(id));

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostAsync(CreateUserCommand createTaskCommand)
            => await SendAsync(createTaskCommand, HttpStatusCode.Created);

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutAsync(UpdateUserCommand updateTaskCommand)
            => await SendAsync(updateTaskCommand);
    }
}