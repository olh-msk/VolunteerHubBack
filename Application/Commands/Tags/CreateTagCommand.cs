using Application.Repositories;
using Domain.Models;
using MediatR;

namespace Application.Commands.Tags
{
    public record CreateTagCommand : IRequest<Tag>
    {
        public string Name { get; init; }
        public CreateTagCommand(string name)
        {
            Name = name;
        }
    }

    public class CreateTagHandler : IRequestHandler<CreateTagCommand, Tag>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateTagHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Tag> Handle(CreateTagCommand request, CancellationToken cancellationToken)
        {
            Tag? tag = new Tag()
            {
                Name = request.Name
            };

            await _unitOfWork.Tags.InsertAsync(tag);
            await _unitOfWork.SaveChangesAsync();
            return tag;
        }
    }
}
