using Application.Repositories.Abstractions;
using Domain.Models;

namespace Infrastructure.Repositories
{
    public class TagRepository : SqlGenericRepository<Tag, int>, ITagRepository
    {
        public TagRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
        }

        public override async Task<bool> UpdateAsync(Tag entityToUpdate)
        {
            Tag? tagToUpdate = await _entity.FindAsync(entityToUpdate.TagId);

            if (tagToUpdate != null)
            {
                tagToUpdate.Name = entityToUpdate.Name;
                tagToUpdate.Posts = entityToUpdate.Posts;
                return true;
            }
            return false;
        }
    }
}
