using Application.Repositories;
using Application.Repositories.Abstractions;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Commands.Images
{
    public record CreateImageCommand : IRequest<Image>
    {
        public IFormFile ImageFile { get; init; }

        public CreateImageCommand(IFormFile imageFile)
        {
            ImageFile = imageFile;
        }
    }
    public class CreateImageHandler : IRequestHandler<CreateImageCommand, Image>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBlobRepository _blobRepository;

        public CreateImageHandler(IUnitOfWork unitOfWork, IBlobRepository blobRepository)
        {
            _unitOfWork = unitOfWork;
            _blobRepository = blobRepository;
        }

        public async Task<Image> Handle(CreateImageCommand request, CancellationToken cancellationToken)
        {
            Image image = new()
            {
                Format = request.ImageFile.ContentType.Split('/')[1]
            };
            await _unitOfWork.Images.InsertAsync(image);
            await _unitOfWork.SaveChangesAsync();
            await _blobRepository.UploadImage(request.ImageFile, image.ToString());
            return image;

        }
    }
}
