using Application.Models;
using Application.Repositories;
using Application.Services;
using Domain.Enums;
using Domain.Models;
using MediatR;
using System.Linq.Expressions;

namespace Application.Queries.PostConnections
{
    public record GetPostConnectionsByUserQuery : IRequest<IEnumerable<PostConnectionResponse>>
    {
        public string? Token { get; init; }
        public GetPostConnectionsByUserQuery(string? token)
        {
            Token = token;
        }
    }

    public class GetPostConnectionsByUserHandler : IRequestHandler<GetPostConnectionsByUserQuery, IEnumerable<PostConnectionResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetPostConnectionsByUserHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<PostConnectionResponse>> Handle(GetPostConnectionsByUserQuery request, CancellationToken cancellationToken)
        {
            var userFromToken = new GetUserFromTokenService(request.Token);

            Expression<Func<PostConnection, bool>> function =
                userFromToken.Role == UserRole.Volunteer ?
                pc => pc.VolunteerPost.UserId == userFromToken.UserId :
                pc => pc.NeedfulPost.UserId == userFromToken.UserId;


            return (await _unitOfWork.PostConnections.GetAsync(filter: function, includeProperties: new string[] { "VolunteerPost.PostImage", "VolunteerPost.Tags", "VolunteerPost.User.ProfileImage", "NeedfulPost.PostImage", "NeedfulPost.Tags", "NeedfulPost.User.ProfileImage" }))
                .Select(postConnection => new PostConnectionResponse(
                    postConnection.PostConnectionId,
                    userFromToken.UserId == postConnection.SenderId ?
                        $"Ви відправили повідомлення до {(userFromToken.Role == UserRole.Needful ? $"{postConnection.VolunteerPost.User.Name} {postConnection.VolunteerPost.User.Surname}" : $"{postConnection.NeedfulPost.User.Name} {postConnection.NeedfulPost.User.Surname}")}" :
                        $"Ви отримали повідомлення від {(userFromToken.Role == UserRole.Needful ? $"{postConnection.VolunteerPost.User.Name} {postConnection.VolunteerPost.User.Surname}" : $"{postConnection.NeedfulPost.User.Name} {postConnection.NeedfulPost.User.Surname}")}",
                    postConnection.Title,
                    postConnection.Message,
                    postConnection.VolunteerPost,
                    postConnection.NeedfulPost,
                    userFromToken.UserId == postConnection.SenderId ? postConnection.SenderHasSeen : postConnection.ReceiverHasSeen
                ));
        }
    }
}