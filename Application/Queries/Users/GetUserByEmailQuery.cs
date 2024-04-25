using Application.Repositories;
using Domain.Exceptions;
using Domain.Models;
using MediatR;

namespace Application.Queries.Users
{
    public record GetUserByEmailQuery : IRequest<User>
    {
        public string UserEmail { get; init; }
        public GetUserByEmailQuery(string userEmail)
        {
            UserEmail = userEmail;
        }
    }

    public class GetUserByEmailHandler : IRequestHandler<GetUserByEmailQuery, User>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUserByEmailHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<User> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            return (await _unitOfWork.Users.GetAsync(user => user.Email.Equals(request.UserEmail))).FirstOrDefault() ?? throw new UserNotFoundException(request.UserEmail);
        }
    }
}
