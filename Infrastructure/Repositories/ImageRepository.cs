using Application.Repositories.Abstractions;
using Domain.Models;

namespace Infrastructure.Repositories
{
    internal class ImageRepository : SqlGenericRepository<Image, int>, IImageRepository
    {
        public ImageRepository(ApplicationContext applicationContext) : base(applicationContext)
        { }

        public override async Task<bool> UpdateAsync(Image entityToUpdate)
        {
            Image? imageToUpdate = await _entity.FindAsync(entityToUpdate.ImageId);

            if (imageToUpdate != null)
            {
                imageToUpdate.Format = entityToUpdate.Format;
                return true;
            }
            return false;
        }
    }
}
