using Application.Models;
using Application.Repositories;
using Application.Services;
using Domain.Enums;
using Domain.Exceptions;
using MediatR;

namespace Application.Queries.PostConnections
{
    public record GetPostConnectionByIdQuery : IRequest<PostConnectionResponse>
    {
        public string? Token { get; init; }

        public int PostConnectionId { get; init; }
        public GetPostConnectionByIdQuery(string? token, int postConnectionId)
        {
            Token = token;
            PostConnectionId = postConnectionId;
        }
    }

    public class GetPostConnectionByIdHandler : IRequestHandler<GetPostConnectionByIdQuery, PostConnectionResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetPostConnectionByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<PostConnectionResponse> Handle(GetPostConnectionByIdQuery request, CancellationToken cancellationToken)
        {
            var userFromToken = new GetUserFromTokenService(request.Token);

            var postConnection = await _unitOfWork.PostConnections.GetByIdAsync(request.PostConnectionId)
                ?? throw new PostConnectionNotFoundException(request.PostConnectionId.ToString());

            if (userFromToken.Role == UserRole.Admin ||
                postConnection.VolunteerPost.UserId == userFromToken.UserId ||
                postConnection.NeedfulPost.UserId == userFromToken.UserId)
            {
                return new PostConnectionResponse(
                    postConnection.PostConnectionId,
                    userFromToken.Role == UserRole.Admin ?
                        "Сповіщення" :
                        userFromToken.UserId == postConnection.SenderId ?
                            "Ви відправили повідомлення" :
                            "Ви отримали повідомлення",
                    postConnection.Title,
                    postConnection.Message,
                    postConnection.VolunteerPost,
                    postConnection.NeedfulPost,
                    userFromToken.UserId == postConnection.SenderId ? postConnection.SenderHasSeen : postConnection.ReceiverHasSeen
                    );
            }
            throw new PostConnectionNotFoundException(request.PostConnectionId.ToString());
        }
    }
}
