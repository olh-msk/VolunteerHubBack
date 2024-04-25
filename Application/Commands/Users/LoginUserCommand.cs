using Application.Repositories;
using Application.Services;
using Domain.Exceptions;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Commands.Users
{
    public record LoginUserCommand : IRequest<string>
    {
        [Required(ErrorMessage = "Login is required")]
        [EmailAddress]
        public string Login { get; init; }
        [Required(ErrorMessage = "Password is reqired")]
        [StringLength(20, ErrorMessage = "Password must be between 8 and 20 characters", MinimumLength = 8)]
        public string Password { get; init; }
        public LoginUserCommand(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }
    public class LoginUserHandler : IRequestHandler<LoginUserCommand, string>
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHashingService _hashingService;
        public LoginUserHandler(IUnitOfWork unitOfWork, IHashingService hashingService, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _hashingService = hashingService;
            _configuration = configuration;
        }
        public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            byte[]? passwordHash = _hashingService.GetHash(request.Password);
            User? user = (await _unitOfWork.Users.GetAsync(user => user.Email == request.Login && user.Password == passwordHash)).FirstOrDefault();

            if (user == null)
            {
                throw new WrongUserEmailOrPasswordException();
            }

            return GenerateJwtToken(user);
        }
        private string GenerateJwtToken(User user)
        {
            SymmetricSecurityKey? securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            SigningCredentials? credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            List<Claim>? claims = new List<Claim>
            {
                new Claim("Id", user.UserId.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            JwtSecurityToken? token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
