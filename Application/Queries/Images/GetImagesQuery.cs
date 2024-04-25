using Application.Repositories;
using Domain.Models;
using MediatR;

namespace Application.Queries.Images
{
    public record GetImagesQuery : IRequest<IEnumerable<Image>>;
    public class GetImagesHandler : IRequestHandler<GetImagesQuery, IEnumerable<Image>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetImagesHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Image>> Handle(GetImagesQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Images.GetAsync();
        }
    }
}
