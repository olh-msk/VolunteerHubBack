using Application.Repositories;
using Domain.Exceptions;
using MediatR;

namespace Application.Commands.Tags
{
    public record DeleteTagCommand : IRequest
    {
        public int TagId { get; init; }
        public DeleteTagCommand(int tagId)
        {
            TagId = tagId;
        }
    }

    public class DeleteTagHandler : IRequestHandler<DeleteTagCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTagHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
        {
            if (!await _unitOfWork.Tags.DeleteAsync(request.TagId))
            {
                throw new TagNotFoundException(request.TagId);
            }

            await _unitOfWork.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
