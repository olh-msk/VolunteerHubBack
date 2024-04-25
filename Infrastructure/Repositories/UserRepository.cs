using Application.Repositories.Abstractions;
using Domain.Exceptions;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : SqlGenericRepository<User, int>, IUserRepository
    {
        public UserRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
        }

        public override async Task<bool> UpdateAsync(User entityToUpdate)
        {
            User? userToUpdate = await _entity.FindAsync(entityToUpdate.UserId);

            if (userToUpdate != null)
            {
                userToUpdate.Name = entityToUpdate.Name;
                userToUpdate.Surname = entityToUpdate.Surname;
                userToUpdate.PhoneNumber = entityToUpdate.PhoneNumber;
                userToUpdate.Address = entityToUpdate.Address;
                userToUpdate.Role = entityToUpdate.Role;
                userToUpdate.Posts = entityToUpdate.Posts;
                return true;
            }
            return false;
        }

        public override async Task<User?> GetByIdAsync(int id)
        {
            return await _entity
                .Include(user => user.ProfileImage)
                .FirstOrDefaultAsync(user => user.UserId == id) ?? throw new UserNotFoundException(id);
        }
    }
}
