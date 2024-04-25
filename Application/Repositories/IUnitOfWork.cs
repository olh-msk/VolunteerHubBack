using Application.Repositories.Abstractions;

namespace Application.Repositories
{
    public interface IUnitOfWork
    {
        public ITagRepository Tags { get; }
        public IUserRepository Users { get; }
        public IPostRepository Posts { get; }
        public IPostConnectionRepository PostConnections { get; }
        public IImageRepository Images { get; }
        public Task SaveChangesAsync();
    }
}
