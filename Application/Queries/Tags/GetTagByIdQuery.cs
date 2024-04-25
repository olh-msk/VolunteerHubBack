using Application.Repositories;
using Domain.Exceptions;
using Domain.Models;
using MediatR;

namespace Application.Queries.Tags
{
    public record GetTagByIdQuery : IRequest<Tag>
    {
        public int TagId { get; init; }
        public GetTagByIdQuery(int tagId)
        {
            TagId = tagId;
        }
    }

    public class GetTagByIdHandler : IRequestHandler<GetTagByIdQuery, Tag>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetTagByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Tag> Handle(GetTagByIdQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Tags.GetByIdAsync(request.TagId) ?? throw new TagNotFoundException(request.TagId);
        }
    }
}
