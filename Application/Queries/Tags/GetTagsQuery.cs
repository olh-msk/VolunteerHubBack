using Application.Repositories;
using Domain.Models;
using MediatR;

namespace Application.Queries.Tags
{
    public record GetTagsQuery : IRequest<IEnumerable<Tag>>;

    public class GetTagsHandler : IRequestHandler<GetTagsQuery, IEnumerable<Tag>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTagsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Tag>> Handle(GetTagsQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Tags.GetAsync();
        }
    }
}
