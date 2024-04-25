using Application.Repositories;
using Domain.Models;
using MediatR;

namespace Application.Queries.Posts
{
    public record GetPostsByTagsQuery : IRequest<IEnumerable<Post>>
    {
        public IEnumerable<int> TagIds { get; init; }
        public GetPostsByTagsQuery(IEnumerable<int> tagIds)
        {
            TagIds = tagIds;
        }
    }

    public class GetPostsByTagsHandler : IRequestHandler<GetPostsByTagsQuery, IEnumerable<Post>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPostsByTagsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Post>> Handle(GetPostsByTagsQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Posts.GetAsync(
                filter: post => post.Tags.Where(tag => request.TagIds.Contains(tag.TagId)).Any(),
                includeProperties: new string[] { "User.ProfileImage", "Tags", "PostImage" });
        }
    }
}
