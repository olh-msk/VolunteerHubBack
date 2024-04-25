using Application.Repositories;
using Application.Services;
using Domain.Exceptions;
using MediatR;

namespace Application.Commands.PostConnections
{
    public record DeletePostConnectionCommand : IRequest
    {
        public int PostConnectionId { get; init; }
        public string? Token { get; init; }
        public DeletePostConnectionCommand(int postConnectionId, string? token)
        {
            PostConnectionId = postConnectionId;
            Token = token;
        }
    }

    public class DeletePostConnectionHandler : IRequestHandler<DeletePostConnectionCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeletePostConnectionHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(DeletePostConnectionCommand request, CancellationToken cancellationToken)
        {
            var userFromToken = new GetUserFromTokenService(request.Token);
            var postConnectionToDelete = await _unitOfWork.PostConnections.GetByIdAsync(request.PostConnectionId);

            if (postConnectionToDelete == null ||
                (userFromToken.Role != Domain.Enums.UserRole.Admin &&
                userFromToken.UserId != postConnectionToDelete.VolunteerPost.UserId &&
                userFromToken.UserId != postConnectionToDelete.NeedfulPost.UserId))
            {
                throw new PostConnectionNotFoundException(request.PostConnectionId.ToString());
            }

            await _unitOfWork.PostConnections.DeleteAsync(request.PostConnectionId);
            await _unitOfWork.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
