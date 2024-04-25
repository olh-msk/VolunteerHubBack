using Application.Commands.Posts;
using Application.Commands.Users;
using Application.Queries.Posts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PostsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _mediator.Send(new GetPostsQuery()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _mediator.Send(new GetPostByIdQuery(id)));
        }

        [HttpGet("by-tags")]
        public async Task<IActionResult> GetByTags([FromQuery(Name = "ids")] string tagIds)
        {
            return Ok(await _mediator.Send(
                new GetPostsByTagsQuery(tagIds
                .Split(",", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Where(tag => int.TryParse(tag, out _))
                .Select(tag => int.Parse(tag)))));
        }

        [HttpGet("current-user")]
        [Authorize(Roles = "Volunteer,Needful")]
        public async Task<IActionResult> GetByToken()
        {
            return Ok(await _mediator.Send(new GetPostsByTokenQuery(Request.Cookies["token"])));
        }

        [HttpPost]
        [Authorize(Roles = "Volunteer,Needful")]
        public async Task<IActionResult> Post([FromForm] CreatePostCommand post)
        {
            return Ok(await _mediator.Send(post));
        }
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateInfo([FromForm] UpdatePostCommand postToUpdate)
        {
            return Ok(await _mediator.Send(postToUpdate));
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeletePost(int id)
        {
            return Ok(await _mediator.Send(new DeletePostCommand(id, Request.Cookies["token"])));
        }
    }
}
