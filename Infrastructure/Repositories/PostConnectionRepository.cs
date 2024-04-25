using Application.Repositories.Abstractions;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    internal class PostConnectionRepository : SqlGenericRepository<PostConnection, int>, IPostConnectionRepository
    {
        public PostConnectionRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
        }
        public override async Task<bool> UpdateAsync(PostConnection entityToUpdate)
        {
            PostConnection? postToUpdate = await _entity.FindAsync(entityToUpdate.PostConnectionId);

            if (postToUpdate != null)
            {
                postToUpdate.Title = entityToUpdate.Title;
                postToUpdate.Message = entityToUpdate.Message;
                postToUpdate.VolunteerPost = entityToUpdate.VolunteerPost;
                postToUpdate.NeedfulPost = entityToUpdate.NeedfulPost;
                postToUpdate.SenderId = entityToUpdate.SenderId;
                postToUpdate.SenderHasSeen = entityToUpdate.SenderHasSeen;
                postToUpdate.ReceiverHasSeen = entityToUpdate.ReceiverHasSeen;
                return true;
            }
            return false;
        }

        public override async Task<PostConnection?> GetByIdAsync(int id)
        {
            return await _entity.Include(pc => pc.VolunteerPost)
                                .Include(pc => pc.NeedfulPost)
                                .FirstOrDefaultAsync(pc => pc.PostConnectionId == id);
        }
    }
}
