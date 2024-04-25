using Application.Repositories;
using Application.Services;
using Domain.Exceptions;
using MediatR;

namespace Application.Commands.Posts
{
    public record DeletePostCommand : IRequest
    {
        public string? Token { get; init; }
        public int PostId { get; init; }
        public DeletePostCommand(int postId, string? token)
        {
            PostId = postId;
            Token = token;
        }
    }
    public class DeletePostHandler : IRequestHandler<DeletePostCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeletePostHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            var userFromToken = new GetUserFromTokenService(request.Token);
            var postToDelete = await _unitOfWork.Posts.GetByIdAsync(request.PostId);
            if (postToDelete == null ||
                (userFromToken.Role != Domain.Enums.UserRole.Admin &&
                postToDelete.UserId != userFromToken.UserId))
            {
                throw new PostNotFoundException(request.PostId.ToString());
            }

            await _unitOfWork.Posts.DeleteAsync(request.PostId);
            await DeleteRelatedPostConnections(request.PostId);
            await _unitOfWork.SaveChangesAsync();
            return Unit.Value;
        }
        private async Task DeleteRelatedPostConnections(int postId)
        {
            foreach (var postConnection in await _unitOfWork.PostConnections.GetAsync(postConnection => postConnection.NeedfulPost.PostId == postId || postConnection.VolunteerPost.PostId == postId))
            {
                await _unitOfWork.PostConnections.DeleteAsync(postConnection.PostConnectionId);
            }
        }
    }
}
