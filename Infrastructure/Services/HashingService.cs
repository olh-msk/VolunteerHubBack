using Application.Services;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;

namespace Infrastructure.Services
{
    public class HashingService : IHashingService
    {
        private readonly IConfiguration _configuration;
        public HashingService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public byte[] GetHash(string password)
        {
            using HMACSHA512? hmac = new HMACSHA512(System.Text.Encoding.UTF8.GetBytes(_configuration["PasswordSalt"]));
            return hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }
}
