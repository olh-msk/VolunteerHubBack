using Application.Repositories;
using Domain.Models;
using MediatR;

namespace Application.Queries.PostConnections
{
    public record GetPostConnectionsQuery : IRequest<IEnumerable<PostConnection>>;

    public class GetPostConnectionsQueryHandler : IRequestHandler<GetPostConnectionsQuery, IEnumerable<PostConnection>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPostConnectionsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<PostConnection>> Handle(GetPostConnectionsQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.PostConnections.GetAsync(includeProperties: new string[] { "VolunteerPost", "NeedfulPost" });
        }
    }
}
