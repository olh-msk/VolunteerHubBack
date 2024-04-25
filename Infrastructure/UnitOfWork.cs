using Application.Repositories;
using Application.Repositories.Abstractions;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _applicationContext;
        private readonly IUserRepository _userRepository;
        private readonly IPostRepository _postRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IPostConnectionRepository _postConnectionRepository;
        private readonly IImageRepository _imageRepository;

        public UnitOfWork(
            ApplicationContext applicationContext,
            IUserRepository userRepository,
            IPostRepository postRepository,
            ITagRepository tagRepository,
            IPostConnectionRepository postConnectionRepository,
            IImageRepository imageRepository)
        {
            _applicationContext = applicationContext;
            _userRepository = userRepository;
            _postRepository = postRepository;
            _tagRepository = tagRepository;
            _postConnectionRepository = postConnectionRepository;
            _imageRepository = imageRepository;
        }

        public IUserRepository Users => _userRepository;
        public IPostRepository Posts => _postRepository;
        public ITagRepository Tags => _tagRepository;
        public IPostConnectionRepository PostConnections => _postConnectionRepository;
        public IImageRepository Images => _imageRepository;

        public async Task SaveChangesAsync()
        {
            await _applicationContext.SaveChangesAsync();
        }
    }
}
