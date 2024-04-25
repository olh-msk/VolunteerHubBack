using Application.Repositories;

namespace Infrastructure
{
    internal record BlobInfo : IBlobInfo
    {
        public BlobInfo(Stream content, string contentType)
        {
            Content = content;
            ContentType = contentType;
        }

        public Stream Content { get; init; }
        public string ContentType { get; init; }
    }
}
