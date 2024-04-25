using Application.Repositories;
using Domain.Models;
using MediatR;

namespace Application.Queries.Users
{
    public record GetUsersQuery : IRequest<IEnumerable<User>>;
    public class GetUsersHandler : IRequestHandler<GetUsersQuery, IEnumerable<User>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUsersHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<User>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Users.GetAsync();
        }
    }
}
