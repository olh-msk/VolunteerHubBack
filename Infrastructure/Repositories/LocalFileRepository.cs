using Application.Repositories;
using Application.Repositories.Abstractions;
using HeyRed.Mime;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Repositories
{
    internal class LocalFileRepository : IBlobRepository
    {
        private readonly string _baseDirectory;

        public LocalFileRepository()
        {
            _baseDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SiteImages");
            EnsureDirectoryExists();
        }

        private void EnsureDirectoryExists()
        {
            if (!Directory.Exists(_baseDirectory))
            {
                Directory.CreateDirectory(_baseDirectory);
            }
        }

        public async Task<bool> DeleteImage(string name)
        {
            string filePath = Path.Combine(_baseDirectory, name);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return true;
            }
            return false;
        }

        public async Task<IBlobInfo> GetImageByName(string name)
        {
            string filePath = Path.Combine(_baseDirectory, name);
            if (File.Exists(filePath))
            {
                var content = await File.ReadAllBytesAsync(filePath);
                string contentType = MimeTypesMap.GetMimeType(filePath); // Assuming MimeMapping exists or implement a similar function.
                return new BlobInfo(new MemoryStream(content), contentType);
            }
            throw new FileNotFoundException($"Image '{name}' not found.");
        }

        public async Task<IBlobInfo> UploadImage(IFormFile imageFile, string name)
        {
            string filePath = Path.Combine(_baseDirectory, name);

            using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                await imageFile.CopyToAsync(stream);
            }

            var content = await File.ReadAllBytesAsync(filePath);
            string contentType = MimeTypesMap.GetMimeType(filePath); // Assuming MimeMapping exists or implement a similar function.
            return new BlobInfo(new MemoryStream(content), contentType);
        }
    }
}
