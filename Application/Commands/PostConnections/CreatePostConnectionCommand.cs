using Application.Repositories;
using Application.Services;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Models;
using MediatR;

namespace Application.Commands.PostConnections
{
    public record CreatePostConnectionCommand : IRequest<PostConnection>
    {
        public string? Token { get; init; }
        public string Title { get; init; }
        public string Message { get; init; }
        public int VolunteerPostId { get; init; }
        public int NeedfulPostId { get; init; }
        public CreatePostConnectionCommand(string title, string message, int volunteerPostId, int needfulPostId, string? token)
        {
            Title = title;
            Message = message;
            VolunteerPostId = volunteerPostId;
            NeedfulPostId = needfulPostId;
            Token = token;
        }
    }

    public class CreatePostConnectionHandler : IRequestHandler<CreatePostConnectionCommand, PostConnection>
    {
        private readonly IUnitOfWork _unitOfWork;


        public CreatePostConnectionHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<PostConnection> Handle(CreatePostConnectionCommand request, CancellationToken cancellationToken)
        {
            var userFromToken = new GetUserFromTokenService(request.Token);
            PostConnection? postConnection = new PostConnection()
            {
                Title = request.Title,
                Message = request.Message,
                VolunteerPost = await PostValidationAsync(request.VolunteerPostId, PostType.Proposition),
                NeedfulPost = await PostValidationAsync(request.NeedfulPostId, PostType.Request),
                SenderId = (int)userFromToken.UserId!,
                SenderHasSeen = false,
                ReceiverHasSeen = false,
            };


            await _unitOfWork.PostConnections.InsertAsync(postConnection);
            await _unitOfWork.SaveChangesAsync();
            return postConnection;
        }
        private async Task<Post> PostValidationAsync(int id, PostType expectedType)
        {
            Post? post = await _unitOfWork.Posts.GetByIdAsync(id);

            if (post == null)
            {
                throw new PostNotFoundException(id.ToString());
            }
            else if (post.PostType != expectedType)
            {
                throw new WrongPostTypeException(id.ToString(), post.PostType.ToString(), expectedType.ToString());
            }

            return post;
        }
    }
}
