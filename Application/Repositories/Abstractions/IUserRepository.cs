using Domain.Models;

namespace Application.Repositories.Abstractions
{
    public interface IUserRepository : IGenericRepository<User, int>
    {
    }
}
