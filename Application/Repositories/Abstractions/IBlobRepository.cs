using Microsoft.AspNetCore.Http;

namespace Application.Repositories.Abstractions
{
    public interface IBlobRepository
    {
        Task<IBlobInfo> GetImageByName(string name);
        Task<IBlobInfo> UploadImage(IFormFile imageFile, string name);
        Task<bool> DeleteImage(string name);
    }
}
