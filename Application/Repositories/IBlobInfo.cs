namespace Application.Repositories
{
    public interface IBlobInfo
    {
        public Stream Content { get; init; }
        public string ContentType { get; init; }
    }
}
