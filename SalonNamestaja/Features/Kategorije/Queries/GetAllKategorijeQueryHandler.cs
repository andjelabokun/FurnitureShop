using MediatR;
using SalonNamestaja.Domain;
using SalonNamestaja.Domain.Repositories;

namespace SalonNamestajaAPI.Features.Kategorije.Queries
{
    public class GetAllKategorijeQueryHandler
        : IRequestHandler<GetAllKategorijeQuery, IEnumerable<Kategorija>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllKategorijeQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<IEnumerable<Kategorija>> Handle(
            GetAllKategorijeQuery request,
            CancellationToken cancellationToken)
        {
            return Task.FromResult(_unitOfWork.Kategorije.GetAll());
        }
    }
}
