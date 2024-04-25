using Application.Repositories;
using Application.Repositories.Abstractions;
using Domain.Exceptions;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Application.Commands.Users
{
    public record UpdateUserImageCommand : IRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public IFormFile ProfileImageFile { get; set; }
    }

    public class UpdateUserImageHandler : IRequestHandler<UpdateUserImageCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBlobRepository _blobRepository;

        public UpdateUserImageHandler(IUnitOfWork unitOfWork, IBlobRepository blobRepository)
        {
            _unitOfWork = unitOfWork;
            _blobRepository = blobRepository;
        }
        public async Task<Unit> Handle(UpdateUserImageCommand request, CancellationToken cancellationToken)
        {
            User? userToUpdate = (await _unitOfWork.Users.GetAsync(user => user.Email == request.Email)).FirstOrDefault();

            if (userToUpdate == null)
            {
                throw new UserNotFoundException(request.Email);
            }

            await _unitOfWork.Images.UpdateAsync(new Image() { ImageId = userToUpdate.ProfileImageId, Format = request.ProfileImageFile.ContentType.Split('/')[1] });
            userToUpdate.ProfileImage = await _unitOfWork.Images.GetByIdAsync(userToUpdate.ProfileImageId);

            await _blobRepository.UploadImage(request.ProfileImageFile, userToUpdate.ProfileImage.ToString());
            await _unitOfWork.Users.UpdateAsync(userToUpdate);
            await _unitOfWork.Images.UpdateAsync(userToUpdate.ProfileImage);
            await _unitOfWork.SaveChangesAsync();

            return default;
        }
    }
}
