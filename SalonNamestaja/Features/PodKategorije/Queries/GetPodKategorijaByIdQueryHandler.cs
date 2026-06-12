using MediatR;
using SalonNamestaja.Domain;
using SalonNamestaja.Domain.Repositories;

namespace SalonNamestajaAPI.Features.PodKategorije.Queries
{
    public class GetPodKategorijaByIdQueryHandler : IRequestHandler<GetPodKategorijaByIdQuery, PodKategorija?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPodKategorijaByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<PodKategorija?> Handle(GetPodKategorijaByIdQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_unitOfWork.PodKategorije.GetById(request.Id));
        }
    }
}
