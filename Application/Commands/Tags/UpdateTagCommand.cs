using Application.Repositories;
using Domain.Exceptions;
using Domain.Models;
using MediatR;

namespace Application.Commands.Tags
{
    public record UpdateTagCommand : IRequest<Tag>
    {
        public int TagId { get; init; }
        public string Name { get; init; }
        public UpdateTagCommand(int tagId, string name)
        {
            TagId = tagId;
            Name = name;
        }
    }

    public class UpdateTagHandler : IRequestHandler<UpdateTagCommand, Tag>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTagHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Tag> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
        {
            Tag? tag = new Tag()
            {
                TagId = request.TagId,
                Name = request.Name
            };

            if (!await _unitOfWork.Tags.UpdateAsync(tag))
            {
                throw new TagNotFoundException(tag.TagId);
            }

            await _unitOfWork.SaveChangesAsync();
            return tag;
        }
    }
}
