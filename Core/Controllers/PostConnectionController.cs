using Application.Commands.PostConnections;
using Application.Queries.PostConnections;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostConnectionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PostConnectionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetPostConnections()
        {
            return Ok(await _mediator.Send(new GetPostConnectionsQuery()));
        }

        [HttpGet("current-user")]
        [Authorize(Roles = "Volunteer,Needful")]
        public async Task<IActionResult> GetPostConnectionsOfAuthorizedUser()
        {
            return Ok(await _mediator.Send(new GetPostConnectionsByUserQuery(Request.Cookies["token"])));
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetPostConnectionOfAuthorizedUser(int id)
        {
            return Ok(await _mediator.Send(new GetPostConnectionByIdQuery(Request.Cookies["token"], id)));
        }

        [HttpPost]
        [Authorize(Roles = "Volunteer,Needful")]
        public async Task<IActionResult> Post([FromBody] CreatePostConnectionRequest post)
        {
            return Ok(await _mediator.Send(new CreatePostConnectionCommand(post.Title, post.Message, post.VolunteerPostId,
                post.NeedfulPostId, Request.Cookies["token"])));
        }

        [HttpPut("revision")]
        [Authorize(Roles = "Volunteer,Needful")]
        public async Task<IActionResult> UpdatePostConnectionRevision([FromBody] UpdatePostConnectionRevisionRequest postConnection)
        {
            return Ok(await _mediator.Send(new UpdatePostConnectionRevisionCommand(
                Request.Cookies["token"],
                postConnection.PostConnectionIds)));
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeletePostConnection(int id)
        {
            return Ok(await _mediator.Send(new DeletePostConnectionCommand(id, Request.Cookies["token"])));
        }
    }
}
