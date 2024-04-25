using Application.Repositories;
using Application.Repositories.Abstractions;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Threading.Tasks;
using Xunit;


namespace WebApi.Tests
{
    public class PoCTest
    {
        [Fact]
        public async Task ShouldStartTheAppAsync()
        {
            var application = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        MockDatabase(services);
                        MockBlob(services);
                    });
                });


            // Setup data via http calls

            // Get data with aggregation

            // Verify

            var client = application.CreateClient();

            var response = await client.GetAsync("/api/tags");

            Assert.Equal(200, (int)response.StatusCode);

        }

        private void MockDatabase(IServiceCollection services)
        {
            RemoveService<DbContextOptions<ApplicationContext>>(services);
            services.AddDbContext<ApplicationContext>(options => { options.UseInMemoryDatabase("InMemoryDbForTesting"); });
        }

        private void MockBlob(IServiceCollection services)
        {
            RemoveService<IBlobRepository>(services);
            services.AddSingleton<IBlobRepository, FakeBlobRepository>();
        }

        private void RemoveService<TService>(IServiceCollection services)
        {
            var descriptor = services.SingleOrDefault(descriptor => descriptor.ServiceType == typeof(TService));
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }
        }

        private class FakeBlobRepository : IBlobRepository
        {
            Task<bool> IBlobRepository.DeleteImage(string name)
            {
                throw new System.NotImplementedException();
            }

            Task<IBlobInfo> IBlobRepository.GetImageByName(string name)
            {
                throw new System.NotImplementedException();
            }

            Task<IBlobInfo> IBlobRepository.UploadImage(IFormFile imageFile, string name)
            {
                throw new System.NotImplementedException();
            }
        }

        //private string GenerateJwtTokenForVolunteer()
        //{
        //    SymmetricSecurityKey? securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
        //    SigningCredentials? credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        //    List<Claim>? claims = new List<Claim>
        //    {
        //        new Claim("Id", "100"),
        //        new Claim(ClaimTypes.Email, "test@gmail.com"),
        //        new Claim(ClaimTypes.Role, "Volunteer")
        //    };

        //    JwtSecurityToken? token = new JwtSecurityToken(
        //        claims: claims,
        //        expires: DateTime.Now.AddDays(1),
        //        signingCredentials: credentials);

        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}
    }
}