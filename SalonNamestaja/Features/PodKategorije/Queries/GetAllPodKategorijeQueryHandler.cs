using MediatR;
using SalonNamestaja.Domain;
using SalonNamestaja.Domain.Repositories;

namespace SalonNamestajaAPI.Features.PodKategorije.Queries
{
    public class GetAllPodKategorijeQueryHandler : IRequestHandler<GetAllPodKategorijeQuery, IEnumerable<PodKategorija>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllPodKategorijeQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<IEnumerable<PodKategorija>> Handle(GetAllPodKategorijeQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_unitOfWork.PodKategorije.GetAll());
        }
    }
}
