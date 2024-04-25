using Application.Repositories;
using Domain.Exceptions;
using Domain.Models;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Commands.Users
{
    public record UpdateUserInfoCommand : IRequest
    {
        [Required(ErrorMessage = "Name is reqired")]
        [StringLength(50, ErrorMessage = "Name must be between 2 and 50 characters", MinimumLength = 2)]
        public string Name { get; init; }
        [Required(ErrorMessage = "Surname is reqired")]
        [StringLength(50, ErrorMessage = "Surname must be between 2 and 50 characters", MinimumLength = 2)]
        public string Surname { get; init; }
        [Required]
        [EmailAddress]
        public string Email { get; init; }
        [Required]
        [Phone]
        public string PhoneNumber { get; init; }
        public string Address { get; init; }
        public UpdateUserInfoCommand(string name, string surname, string email, string phoneNumber, string address)
        {
            Name = name;
            Surname = surname;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
        }
    }


    public class UpdateUserInfoHandler : IRequestHandler<UpdateUserInfoCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateUserInfoHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(UpdateUserInfoCommand request, CancellationToken cancellationToken)
        {
            User? userToUpdate = (await _unitOfWork.Users.GetAsync(user => user.Email == request.Email)).FirstOrDefault();

            if (userToUpdate == null)
            {
                throw new UserNotFoundException(request.Email); 
            }

            userToUpdate.Name = request.Name;
            userToUpdate.Surname = request.Surname;
            userToUpdate.Address = request.Address;
            userToUpdate.PhoneNumber = request.PhoneNumber;

            await _unitOfWork.Users.UpdateAsync(userToUpdate);
            await _unitOfWork.SaveChangesAsync();
            return default;
        }
    }
}
