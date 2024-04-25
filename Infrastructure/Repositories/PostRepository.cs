using Application.Repositories.Abstractions;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PostRepository : SqlGenericRepository<Post, int>, IPostRepository
    {
        public PostRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
        }

        public override async Task<bool> UpdateAsync(Post entityToUpdate)
        {
            Post? postToUpdate = await _entity.FindAsync(entityToUpdate.UserId);

            if (postToUpdate != null)
            {
                postToUpdate.Title = entityToUpdate.Title;
                postToUpdate.Description = entityToUpdate.Description;
                postToUpdate.PostImage = entityToUpdate.PostImage;
                postToUpdate.UserId = entityToUpdate.UserId;
                postToUpdate.User = entityToUpdate.User;
                postToUpdate.Tags = entityToUpdate.Tags;
                return true;
            }
            return false;
        }
        public override async Task<Post?> GetByIdAsync(int id)
        {
            return await _entity.Include("User")
                                .Include(post => post.Tags)
                                .Include(post => post.PostImage)
                                .FirstOrDefaultAsync(post => post.PostId == id);
        }
    }
}
