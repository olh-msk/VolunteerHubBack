using Application.Repositories;
using Application.Services;
using Domain.Models;
using MediatR;

namespace Application.Commands.PostConnections
{
    public record UpdatePostConnectionRevisionCommand : IRequest
    {
        public string? Token { get; init; }
        public int[] PostConnectionIds { get; init; }
        public UpdatePostConnectionRevisionCommand(string? token, int[] postConnectionIds)
        {
            Token = token;
            PostConnectionIds = postConnectionIds;
        }
    }
    public class UpdatePostConnectionRevisionHandler : IRequestHandler<UpdatePostConnectionRevisionCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdatePostConnectionRevisionHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(UpdatePostConnectionRevisionCommand request, CancellationToken cancellationToken)
        {
            var userFromToken = new GetUserFromTokenService(request.Token);

            foreach (var postConnectionId in request.PostConnectionIds)
            {
                var postConnectionToUpdate = await _unitOfWork.PostConnections.GetByIdAsync(postConnectionId);
                if (postConnectionToUpdate != null && (postConnectionToUpdate.VolunteerPost.UserId == userFromToken.UserId ||
                postConnectionToUpdate.NeedfulPost.UserId == userFromToken.UserId))
                {
                    await _unitOfWork.PostConnections.UpdateAsync(new PostConnection()
                    {
                        PostConnectionId = postConnectionToUpdate.PostConnectionId,
                        Title = postConnectionToUpdate.Title,
                        Message = postConnectionToUpdate.Message,
                        VolunteerPost = postConnectionToUpdate.VolunteerPost,
                        NeedfulPost = postConnectionToUpdate.NeedfulPost,
                        SenderId = postConnectionToUpdate.SenderId,
                        SenderHasSeen = userFromToken.UserId == postConnectionToUpdate.SenderId ? true : postConnectionToUpdate.SenderHasSeen,
                        ReceiverHasSeen = userFromToken.UserId == postConnectionToUpdate.SenderId ? postConnectionToUpdate.ReceiverHasSeen : true
                    });
                }
            }

            await _unitOfWork.SaveChangesAsync();
            return default;
        }
    }

}
