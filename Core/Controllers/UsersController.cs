using Application.Commands.Tags;
using Application.Commands.Users;
using Application.Queries.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserCommand loginUserCommand)
        {
            string? token = await _mediator.Send(loginUserCommand);
            HttpContext.Response.Cookies.Append("token", token, new CookieOptions
            {
                MaxAge = TimeSpan.FromDays(1)
            });
            return Ok("Successfully logged in");
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm]RegisterUserCommand registerUserCommand)
        {
            await _mediator.Send(registerUserCommand);
            return Ok("Successfully registered!");
        }

        [Authorize]
        [HttpPost("logout")]
        public IActionResult LogOut()
        {
            HttpContext.Response.Cookies.Delete("token");
            return Ok("Successfully logged out");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsers(CancellationToken token)
        {
            return Ok(await _mediator.Send(new GetUsersQuery(), token));
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUser(int id)
        {
            return Ok(await _mediator.Send(new GetUserByIdQuery(id)));
        }

        [HttpGet("by-token")]
        [Authorize]
        public async Task<IActionResult> GetUserByToken()
        {
            int userId = int.Parse(new JwtSecurityTokenHandler()
                .ReadJwtToken(Request.Cookies["token"])
                .Claims
                .First(claim => claim.Type == "Id")
                .Value);

            return Ok(await _mediator.Send(new GetUserByIdQuery(userId)));
        }

        [HttpGet("{email}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            return Ok(await _mediator.Send(new GetUserByEmailQuery(email)));
        }
        [AllowAnonymous]
        [HttpGet("if-authorize")]
        public IActionResult GetIfUserExist()
        {
            return Ok(HttpContext.Request.Cookies["token"] != null);
        }
        [HttpPut("info")]
        [Authorize]
        public async Task<IActionResult> UpdateInfo([FromBody] UpdateUserInfoCommand userToUpdate)
        {
            return Ok(await _mediator.Send(userToUpdate));
        }

        [HttpPut("image")]
        [Authorize]
        public async Task<IActionResult> UpdateImage([FromForm] UpdateUserImageCommand userToUpdate)
        {
            return Ok(await _mediator.Send(userToUpdate));
        }
    }
}
