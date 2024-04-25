using Microsoft.AspNetCore.StaticFiles;

namespace Infrastructure.Services
{
    internal static class FileExtensions
    {
        private static readonly FileExtensionContentTypeProvider _provider = new();
        public static string GetContentType(this string fileName)
        {
            if (!_provider.TryGetContentType(fileName, out string? contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }
    }
}
