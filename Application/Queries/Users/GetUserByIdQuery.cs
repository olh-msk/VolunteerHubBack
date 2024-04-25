using Application.Repositories;
using Domain.Exceptions;
using Domain.Models;
using MediatR;

namespace Application.Queries.Users
{
    public record GetUserByIdQuery : IRequest<User>
    {
        public int UserId { get; init; }
        public GetUserByIdQuery(int userId)
        {
            UserId = userId;
        }
    }

    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, User>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUserByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Users.GetByIdAsync(request.UserId) ?? throw new UserNotFoundException(request.UserId);
        }
    }
}
