using Application.Repositories;
using Application.Services;
using Domain.Models;
using MediatR;

namespace Application.Queries.Posts
{
    public record GetPostsByTokenQuery : IRequest<IEnumerable<Post>>
    {
        public string? Token { get; init; }
        public GetPostsByTokenQuery(string? token)
        {
            Token = token;
        }
    }

    public class GetPostsByTokenHandler : IRequestHandler<GetPostsByTokenQuery, IEnumerable<Post>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetPostsByTokenHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Post>> Handle(GetPostsByTokenQuery request, CancellationToken cancellationToken)
        {
            var userFromToken = new GetUserFromTokenService(request.Token);

            return await _unitOfWork.Posts.GetAsync(
                filter: post => post.UserId == userFromToken.UserId,
                includeProperties: new string[] { "PostImage", "Tags", "User.ProfileImage" });
        }
    }
}
