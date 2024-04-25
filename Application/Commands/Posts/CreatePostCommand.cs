using Application.Commands.Images;
using Application.Repositories;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Commands.Posts
{
    public record CreatePostCommand : IRequest<Post>
    {
        // can there be a post without an image or tags?
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<int> TagIds { get; set; }
        public IFormFile ImageFile { get; set; }

    }

    public class CreatePostHandler : IRequestHandler<CreatePostCommand, Post>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public CreatePostHandler(IUnitOfWork unitOfWork, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }
        public async Task<Post> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            User? postOwner = await _unitOfWork.Users.GetByIdAsync(request.UserId);

            if (postOwner == null)
            {
                throw new UserNotFoundException(request.UserId);
            }

            PostType postType = postOwner.Role == UserRole.Needful ? PostType.Request : PostType.Proposition;

            Post? post = new Post()
            {
                Title = request.Title,
                Description = request.Description,
                UserId = request.UserId,
                User = postOwner,
                Tags = await GetTagsByIdsAsync(request.TagIds),
                PostImage = await _mediator.Send(new CreateImageCommand(request.ImageFile), cancellationToken),
                PostType = postType
            };

            await _unitOfWork.Posts.InsertAsync(post);
            await _unitOfWork.SaveChangesAsync();
            return post;
        }

        private async Task<ICollection<Tag>> GetTagsByIdsAsync(ICollection<int> tagIds)
        {
            List<Tag>? tags = new List<Tag>();
            foreach (int tagId in tagIds)
            {
                Tag? tag = await _unitOfWork.Tags.GetByIdAsync(tagId);
                if (tag != null)
                {
                    tags.Add(tag);
                }
            }
            return tags;
        }
    }
}
